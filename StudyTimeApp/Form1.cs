using StudyTimeApp;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System;

namespace StudyTimeApp
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        internal struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        private Stopwatch stopwatch = new Stopwatch();
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private DateTime startTime;
        private DateTime endTime;
        BindingSource StudyDayBindingSource = new BindingSource();
        BindingSource StudyTimeBindingSource = new BindingSource();

        List<StudyDay> days = new List<StudyDay>();

        public Form1()
        {
            InitializeComponent();

            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private void btn_startstop_Click(object sender, EventArgs e)
        {
            if (btn_startstop.Text == "Start Studying")
            {
                btn_startstop.Text = "Stop Studying";
                stopwatch.Restart();
                timer.Start();
                groupBox1.Visible = false;
                startTime = DateTime.Now;
            }
            else
            {
                btn_startstop.Text = "Start Studying";
                txt_timer.Text = String.Empty;
                stopwatch.Stop();
                timer.Stop();
                groupBox1.Visible = true;
                txt_totaltime.Text = FormatTime(stopwatch.Elapsed);
                txt_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
                endTime = DateTime.Now;
                txt_starttime.Text = startTime.ToString("HH:mm:ss");
                txt_endtime.Text = endTime.ToString("HH:mm:ss");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            GetLastInputInfo(ref lastInputInfo);

            long idleTime = Environment.TickCount - lastInputInfo.dwTime;

            if (idleTime > 60000)
            {
                stopwatch.Stop();
            }
            else
            {
                stopwatch.Start();
            }
            TimeSpan elapsed = stopwatch.Elapsed;
            txt_timer.Text = "Elapsed time: " + FormatTime(elapsed);
        }

        public static string FormatTime(TimeSpan time)
        {
            if (time.TotalHours >= 1)
            {
                return $"{(int)time.TotalHours}hr {time.Minutes:00}min {time.Seconds:00}sec";
            }
            else if (time.TotalMinutes >= 1)
            {
                return $"{time.Minutes}min {time.Seconds:00}sec";
            }
            else
            {
                return $"{time.Seconds}sec";
            }
        }

        private void btn_showtimes_Click(object sender, EventArgs e)
        {
            StudyTimeDAO timeDAO = new StudyTimeDAO();
            days = timeDAO.getAllDays();
            // Connect list to grid view controll
            StudyDayBindingSource.DataSource = days;
            dataGridView1.DataSource = StudyDayBindingSource;
            dataGridView1.Columns["ID"].Visible = false;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            StudyTimeDAO timeDAO = new StudyTimeDAO();
            // Connect list to grid view controll
            StudyDayBindingSource.DataSource = timeDAO.searchDaysText(txt_search.Text);
            dataGridView1.DataSource = StudyDayBindingSource;
            dataGridView2.DataSource = null;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            string formattedDate = monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd");

            StudyTimeDAO timeDAO = new StudyTimeDAO();
            // Connect list to grid view controll
            StudyDayBindingSource.DataSource = timeDAO.searchDates(formattedDate);
            dataGridView1.DataSource = StudyDayBindingSource;
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            // Add new item to database
            StudyDay newDay = new StudyDay
            {
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                Time = FormatTime(stopwatch.Elapsed),
            };
            Studies newStudy = new Studies
            {
                TotalTime = FormatTime(stopwatch.Elapsed),
                StartTime = startTime.ToString("HH:mm:ss"),
                EndTime = endTime.ToString("HH:mm:ss"),
                Summary = txt_summary.Text,
                Notes = txt_notes.Text
            };
            StudyTimeDAO newTimeDAO = new StudyTimeDAO();
            newTimeDAO.addStudyDay(newDay, newStudy);
            //MessageBox.Show("New study added successfully!");
            groupBox1.Visible = false;

            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            days = newTimeDAO.getAllDays();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btn_delete.Visible = true;

            DataGridView dataGridView = (DataGridView)sender;
            int rowClicked = dataGridView.CurrentRow.Index;

            // Connect list to grid view controll
            StudyTimeBindingSource.DataSource = days[rowClicked].Studies;
            dataGridView2.DataSource = StudyTimeBindingSource;
            if (dataGridView2.Rows.Count > 0)
            {
                dataGridView2.Columns["ID"].Visible = false;
                dataGridView2.Columns["days_ID"].Visible = false;
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            int daysClicked = (dataGridView1.CurrentRow != null) ? dataGridView1.CurrentRow.Index : -1;
            int studyClicked = (dataGridView2.CurrentRow != null) ? dataGridView2.CurrentRow.Index : -1;
            StudyTimeDAO deleted = new StudyTimeDAO();

            if (daysClicked != -1 && studyClicked != -1)
            {
                //MessageBox.Show("Studies Clicked!");
                int studiesID = (int)dataGridView2.Rows[studyClicked].Cells[0].Value;
                int result = deleted.deleteStudy(studiesID);

                dataGridView2.DataSource = null;
                days = deleted.getAllDays();
            }
            else if (daysClicked != -1)
            {
                //MessageBox.Show("Days clicked");
                int daysID = (int)dataGridView1.Rows[daysClicked].Cells[0].Value;
                int result = deleted.deleteDay(daysID);

                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                days = deleted.getAllDays();
            }
            else
            {
                MessageBox.Show("How did you do this...? How did you click Delete Selected with nothing selected WTF");
            }
        }
    }
}
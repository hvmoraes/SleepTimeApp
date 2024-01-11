using StudyTimeApp;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SleepTimeApp
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
        BindingSource StudyTimeBindingSource = new BindingSource();

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
            }
            else
            {
                btn_startstop.Text = "Start Studying";
                txt_timer.Text = String.Empty;
                stopwatch.Stop();
                timer.Stop();
                groupBox1.Visible = true;
                txt_time.Text = FormatTime(stopwatch.Elapsed);
                txt_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
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

        private string FormatTime(TimeSpan time)
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

            // Connect list to grid view controll
            StudyTimeBindingSource.DataSource = timeDAO.getAllTimes();
            dataGridView1.DataSource = StudyTimeBindingSource;
            dataGridView1.Columns["ID"].Visible = false;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            StudyTimeDAO timeDAO = new StudyTimeDAO();

            // Connect list to grid view controll
            StudyTimeBindingSource.DataSource = timeDAO.searchStudies(txt_search.Text);
            dataGridView1.DataSource = StudyTimeBindingSource;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            string formattedDate = monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd");
            StudyTimeDAO timeDAO = new StudyTimeDAO();

            // Connect list to grid view controll
            StudyTimeBindingSource.DataSource = timeDAO.searchDates(formattedDate);
            dataGridView1.DataSource = StudyTimeBindingSource;
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            // Add new item to database
            StudyTime newStudy = new StudyTime
            {
                Date = DateTime.Now,
                Time = FormatTime(stopwatch.Elapsed),
                Notes = txt_notes.Text,
                Summary = txt_summary.Text
            };
            StudyTimeDAO newTimeDAO = new StudyTimeDAO();
            newTimeDAO.addStudy(newStudy);
            MessageBox.Show("New study added successfully!");
            groupBox1.Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btn_delete.Visible = true;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            int rowClicked = dataGridView.CurrentRow.Index;
            int id = (int)dataGridView1.Rows[rowClicked].Cells[0].Value;

            StudyTimeDAO deletedStudy = new StudyTimeDAO();
            int result = deletedStudy.deleteStudy(id);
            MessageBox.Show("Study times from ");
        }
    }
}
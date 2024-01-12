namespace SleepTimeApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btn_startstop = new Button();
            dataGridView1 = new DataGridView();
            txt_timer = new Label();
            btn_showtimes = new Button();
            monthCalendar1 = new MonthCalendar();
            txt_search = new TextBox();
            btn_search = new Button();
            groupBox1 = new GroupBox();
            btn_submit = new Button();
            txt_summary = new TextBox();
            txt_notes = new TextBox();
            txt_time = new TextBox();
            txt_date = new TextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            btn_delete = new Button();
            dataGridView2 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(528, 26);
            label1.Name = "label1";
            label1.Size = new Size(201, 37);
            label1.TabIndex = 0;
            label1.Text = "Study Time app";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // btn_startstop
            // 
            btn_startstop.Location = new Point(249, 503);
            btn_startstop.Name = "btn_startstop";
            btn_startstop.Size = new Size(123, 23);
            btn_startstop.TabIndex = 1;
            btn_startstop.Text = "Start Studying";
            btn_startstop.UseVisualStyleBackColor = true;
            btn_startstop.Click += btn_startstop_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(249, 118);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(728, 162);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // txt_timer
            // 
            txt_timer.AutoSize = true;
            txt_timer.Location = new Point(249, 529);
            txt_timer.Name = "txt_timer";
            txt_timer.Size = new Size(0, 15);
            txt_timer.TabIndex = 3;
            // 
            // btn_showtimes
            // 
            btn_showtimes.Location = new Point(249, 88);
            btn_showtimes.Name = "btn_showtimes";
            btn_showtimes.Size = new Size(123, 23);
            btn_showtimes.TabIndex = 4;
            btn_showtimes.Text = "Show Study Times";
            btn_showtimes.UseVisualStyleBackColor = true;
            btn_showtimes.Click += btn_showtimes_Click;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(992, 118);
            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 5;
            monthCalendar1.DateChanged += monthCalendar1_DateChanged;
            // 
            // txt_search
            // 
            txt_search.Location = new Point(515, 88);
            txt_search.Name = "txt_search";
            txt_search.Size = new Size(169, 23);
            txt_search.TabIndex = 6;
            // 
            // btn_search
            // 
            btn_search.Location = new Point(690, 88);
            btn_search.Name = "btn_search";
            btn_search.Size = new Size(71, 23);
            btn_search.TabIndex = 7;
            btn_search.Text = "Search";
            btn_search.UseVisualStyleBackColor = true;
            btn_search.Click += btn_search_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btn_submit);
            groupBox1.Controls.Add(txt_summary);
            groupBox1.Controls.Add(txt_notes);
            groupBox1.Controls.Add(txt_time);
            groupBox1.Controls.Add(txt_date);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(13, 89);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(215, 459);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "New Study Time";
            groupBox1.Visible = false;
            // 
            // btn_submit
            // 
            btn_submit.BackColor = SystemColors.MenuHighlight;
            btn_submit.ForeColor = SystemColors.ControlLightLight;
            btn_submit.Location = new Point(118, 423);
            btn_submit.Name = "btn_submit";
            btn_submit.Size = new Size(75, 23);
            btn_submit.TabIndex = 8;
            btn_submit.Text = "Submit";
            btn_submit.UseVisualStyleBackColor = false;
            btn_submit.Click += btn_submit_Click;
            // 
            // txt_summary
            // 
            txt_summary.Location = new Point(70, 261);
            txt_summary.Multiline = true;
            txt_summary.Name = "txt_summary";
            txt_summary.Size = new Size(123, 154);
            txt_summary.TabIndex = 7;
            // 
            // txt_notes
            // 
            txt_notes.Location = new Point(70, 86);
            txt_notes.Multiline = true;
            txt_notes.Name = "txt_notes";
            txt_notes.Size = new Size(123, 169);
            txt_notes.TabIndex = 6;
            // 
            // txt_time
            // 
            txt_time.Location = new Point(70, 57);
            txt_time.Name = "txt_time";
            txt_time.ReadOnly = true;
            txt_time.Size = new Size(123, 23);
            txt_time.TabIndex = 5;
            // 
            // txt_date
            // 
            txt_date.Location = new Point(70, 29);
            txt_date.Name = "txt_date";
            txt_date.ReadOnly = true;
            txt_date.Size = new Size(123, 23);
            txt_date.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 264);
            label6.Name = "label6";
            label6.Size = new Size(58, 15);
            label6.TabIndex = 3;
            label6.Text = "Summary";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 89);
            label5.Name = "label5";
            label5.Size = new Size(38, 15);
            label5.TabIndex = 2;
            label5.Text = "Notes";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 62);
            label4.Name = "label4";
            label4.Size = new Size(33, 15);
            label4.TabIndex = 1;
            label4.Text = "Time";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 32);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 0;
            label3.Text = "Date";
            // 
            // btn_delete
            // 
            btn_delete.Location = new Point(879, 503);
            btn_delete.Name = "btn_delete";
            btn_delete.Size = new Size(98, 23);
            btn_delete.TabIndex = 9;
            btn_delete.Text = "Delete Selected";
            btn_delete.UseVisualStyleBackColor = true;
            btn_delete.Visible = false;
            btn_delete.Click += btn_delete_Click;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(249, 295);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(728, 192);
            dataGridView2.TabIndex = 10;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1237, 631);
            Controls.Add(dataGridView2);
            Controls.Add(btn_delete);
            Controls.Add(groupBox1);
            Controls.Add(btn_search);
            Controls.Add(txt_search);
            Controls.Add(monthCalendar1);
            Controls.Add(btn_showtimes);
            Controls.Add(txt_timer);
            Controls.Add(dataGridView1);
            Controls.Add(btn_startstop);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btn_startstop;
        private DataGridView dataGridView1;
        private Label txt_timer;
        private Button btn_showtimes;
        private MonthCalendar monthCalendar1;
        private TextBox txt_search;
        private Button btn_search;
        private GroupBox groupBox1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txt_date;
        private TextBox txt_time;
        private TextBox txt_notes;
        private TextBox txt_summary;
        private Button btn_submit;
        private Button btn_delete;
        private DataGridView dataGridView2;
    }
}

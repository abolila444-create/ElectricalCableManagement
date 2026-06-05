using System;
using System.Windows.Forms;
using System.Drawing;

namespace ElectricalCableManagement
{
    public partial class ClockControlPanel : Form
    {
        private Button btnDigitalClock;
        private Button btnTimeZoneClock;
        private Button btnAnalogClock;
        private Button btnExit;
        private Label titleLabel;

        public ClockControlPanel()
        {
            InitializeComponent();
            SetupPanel();
        }

        private void SetupPanel()
        {
            // Title
            titleLabel = new Label
            {
                Text = "Clock Applications",
                Location = new Point(50, 30),
                Size = new Size(300, 40),
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204)
            };
            this.Controls.Add(titleLabel);

            // Digital Clock Button
            btnDigitalClock = new Button
            {
                Text = "Digital Clock\n(Multiple Time Zones)",
                Location = new Point(50, 100),
                Size = new Size(300, 80),
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 153, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDigitalClock.Click += BtnDigitalClock_Click;
            this.Controls.Add(btnDigitalClock);

            // Time Zone Clock Button
            btnTimeZoneClock = new Button
            {
                Text = "Time Zone Clock\n(Advanced)",
                Location = new Point(50, 200),
                Size = new Size(300, 80),
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 204, 102),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnTimeZoneClock.Click += BtnTimeZoneClock_Click;
            this.Controls.Add(btnTimeZoneClock);

            // Analog Clock Button
            btnAnalogClock = new Button
            {
                Text = "Analog Clock\n(Classic Style)",
                Location = new Point(50, 300),
                Size = new Size(300, 80),
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 102, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAnalogClock.Click += BtnAnalogClock_Click;
            this.Controls.Add(btnAnalogClock);

            // Exit Button
            btnExit = new Button
            {
                Text = "Close",
                Location = new Point(50, 400),
                Size = new Size(300, 40),
                Font = new Font("Arial", 11),
                BackColor = Color.FromArgb(192, 192, 192),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnExit.Click += BtnExit_Click;
            this.Controls.Add(btnExit);
        }

        private void BtnDigitalClock_Click(object sender, EventArgs e)
        {
            DigitalClockForm clockForm = new DigitalClockForm();
            clockForm.Show();
        }

        private void BtnTimeZoneClock_Click(object sender, EventArgs e)
        {
            TimeZoneClockForm clockForm = new TimeZoneClockForm();
            clockForm.Show();
        }

        private void BtnAnalogClock_Click(object sender, EventArgs e)
        {
            AnalogClockForm clockForm = new AnalogClockForm();
            clockForm.Show();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(400, 480);
            this.Name = "ClockControlPanel";
            this.Text = "Clock Applications Menu";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.ResumeLayout(false);
        }
    }
}

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace ElectricalCableManagement
{
    public partial class DigitalClockForm : Form
    {
        private Timer clockTimer;
        private List<TimeZoneInfo> displayedTimeZones;
        private List<Label> timeZoneLabels;
        private List<Label> timeLabels;

        public DigitalClockForm()
        {
            InitializeComponent();
            SetupClock();
        }

        private void SetupClock()
        {
            displayedTimeZones = new List<TimeZoneInfo>
            {
                TimeZoneInfo.FindSystemTimeZoneById("UTC"),
                TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"),
                TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"),
                TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"),
                TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time")
            };

            timeZoneLabels = new List<Label>();
            timeLabels = new List<Label>();

            // Create panels for each time zone
            int yPosition = 30;
            for (int i = 0; i < displayedTimeZones.Count; i++)
            {
                // Time Zone Name Label
                Label tzLabel = new Label
                {
                    Text = displayedTimeZones[i].DisplayName,
                    Location = new Point(20, yPosition),
                    Size = new Size(350, 30),
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(0, 102, 204)
                };
                this.Controls.Add(tzLabel);
                timeZoneLabels.Add(tzLabel);

                // Time Display Label
                Label timeLabel = new Label
                {
                    Text = "00:00:00",
                    Location = new Point(370, yPosition),
                    Size = new Size(250, 30),
                    Font = new Font("Courier New", 16, FontStyle.Bold),
                    ForeColor = Color.FromArgb(51, 51, 51),
                    BackColor = Color.FromArgb(230, 230, 230),
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                this.Controls.Add(timeLabel);
                timeLabels.Add(timeLabel);

                yPosition += 50;
            }

            // Setup Timer
            clockTimer = new Timer();
            clockTimer.Interval = 1000; // Update every second
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();

            // Initial update
            UpdateClocks();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            UpdateClocks();
        }

        private void UpdateClocks()
        {
            for (int i = 0; i < displayedTimeZones.Count; i++)
            {
                DateTime utcTime = DateTime.UtcNow;
                DateTime localTime = TimeZoneInfo.ConvertTime(utcTime, displayedTimeZones[i]);
                timeLabels[i].Text = localTime.ToString("HH:mm:ss");
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.ClientSize = new Size(650, 320);
            this.Name = "DigitalClockForm";
            this.Text = "Digital Clock - Multiple Time Zones";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.FormClosing += DigitalClockForm_FormClosing;

            // Title Label
            Label titleLabel = new Label
            {
                Text = "World Time Zones",
                Location = new Point(20, 10),
                Size = new Size(300, 20),
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 0, 0)
            };
            this.Controls.Add(titleLabel);

            this.ResumeLayout(false);
        }

        private void DigitalClockForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clockTimer != null)
            {
                clockTimer.Stop();
                clockTimer.Dispose();
            }
        }
    }
}

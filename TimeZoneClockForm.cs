using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace ElectricalCableManagement
{
    public partial class TimeZoneClockForm : Form
    {
        private Timer updateTimer;
        private ComboBox timeZoneComboBox;
        private Label mainClockLabel;
        private Label dateLabel;
        private Label timeZoneInfoLabel;
        private Button addTimeZoneBtn;
        private Button removeTimeZoneBtn;
        private DataGridView timeZoneGridView;
        private List<TimeZoneDisplay> displayedZones;

        private class TimeZoneDisplay
        {
            public string DisplayName { get; set; }
            public TimeZoneInfo TimeZone { get; set; }
            public DateTime LocalTime { get; set; }
        }

        public TimeZoneClockForm()
        {
            InitializeComponent();
            displayedZones = new List<TimeZoneDisplay>();
            SetupForm();
            StartClock();
        }

        private void SetupForm()
        {
            // Main Clock Display
            mainClockLabel = new Label
            {
                Text = "00:00:00",
                Location = new Point(50, 30),
                Size = new Size(400, 80),
                Font = new Font("Courier New", 48, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                BackColor = Color.FromArgb(240, 240, 240),
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(mainClockLabel);

            // Date Label
            dateLabel = new Label
            {
                Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy"),
                Location = new Point(50, 120),
                Size = new Size(400, 30),
                Font = new Font("Arial", 12),
                ForeColor = Color.FromArgb(102, 102, 102),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(dateLabel);

            // Time Zone Info
            timeZoneInfoLabel = new Label
            {
                Text = "Local Time",
                Location = new Point(50, 160),
                Size = new Size(400, 20),
                Font = new Font("Arial", 10, FontStyle.Italic),
                ForeColor = Color.FromArgb(102, 102, 102)
            };
            this.Controls.Add(timeZoneInfoLabel);

            // Time Zone Selection
            Label tzLabel = new Label
            {
                Text = "Select Time Zone:",
                Location = new Point(50, 200),
                Size = new Size(120, 25),
                Font = new Font("Arial", 10)
            };
            this.Controls.Add(tzLabel);

            timeZoneComboBox = new ComboBox
            {
                Location = new Point(180, 200),
                Size = new Size(270, 25),
                Font = new Font("Arial", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            PopulateTimeZones();
            this.Controls.Add(timeZoneComboBox);

            // Add Button
            addTimeZoneBtn = new Button
            {
                Text = "Add",
                Location = new Point(460, 200),
                Size = new Size(60, 25),
                Font = new Font("Arial", 9)
            };
            addTimeZoneBtn.Click += AddTimeZoneBtn_Click;
            this.Controls.Add(addTimeZoneBtn);

            // Remove Button
            removeTimeZoneBtn = new Button
            {
                Text = "Remove",
                Location = new Point(530, 200),
                Size = new Size(70, 25),
                Font = new Font("Arial", 9)
            };
            removeTimeZoneBtn.Click += RemoveTimeZoneBtn_Click;
            this.Controls.Add(removeTimeZoneBtn);

            // Grid View for Multiple Time Zones
            timeZoneGridView = new DataGridView
            {
                Location = new Point(50, 240),
                Size = new Size(550, 200),
                Font = new Font("Arial", 9),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            timeZoneGridView.Columns.Add("TimeZone", "Time Zone");
            timeZoneGridView.Columns.Add("Time", "Current Time");
            timeZoneGridView.Columns.Add("Date", "Date");
            timeZoneGridView.Columns[0].Width = 250;
            timeZoneGridView.Columns[1].Width = 150;
            timeZoneGridView.Columns[2].Width = 150;
            this.Controls.Add(timeZoneGridView);
        }

        private void PopulateTimeZones()
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones();
            foreach (var tz in timeZones)
            {
                timeZoneComboBox.Items.Add(tz.DisplayName);
            }
            timeZoneComboBox.SelectedIndex = 0;
        }

        private void AddTimeZoneBtn_Click(object sender, EventArgs e)
        {
            if (timeZoneComboBox.SelectedIndex >= 0)
            {
                string selectedTz = timeZoneComboBox.SelectedItem.ToString();
                var timeZone = TimeZoneInfo.GetSystemTimeZones()
                    .FirstOrDefault(tz => tz.DisplayName == selectedTz);

                if (timeZone != null && !displayedZones.Any(z => z.DisplayName == selectedTz))
                {
                    displayedZones.Add(new TimeZoneDisplay
                    {
                        DisplayName = selectedTz,
                        TimeZone = timeZone
                    });
                    UpdateGrid();
                }
            }
        }

        private void RemoveTimeZoneBtn_Click(object sender, EventArgs e)
        {
            if (timeZoneGridView.SelectedRows.Count > 0)
            {
                int index = timeZoneGridView.SelectedRows[0].Index;
                displayedZones.RemoveAt(index);
                UpdateGrid();
            }
        }

        private void StartClock()
        {
            updateTimer = new Timer();
            updateTimer.Interval = 1000;
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            mainClockLabel.Text = now.ToString("HH:mm:ss");
            dateLabel.Text = now.ToString("dddd, MMMM dd, yyyy");
            timeZoneInfoLabel.Text = TimeZoneInfo.Local.DisplayName;

            UpdateGrid();
        }

        private void UpdateGrid()
        {
            timeZoneGridView.Rows.Clear();
            foreach (var zone in displayedZones)
            {
                DateTime utcTime = DateTime.UtcNow;
                DateTime zoneTime = TimeZoneInfo.ConvertTime(utcTime, zone.TimeZone);
                timeZoneGridView.Rows.Add(
                    zone.DisplayName,
                    zoneTime.ToString("HH:mm:ss"),
                    zoneTime.ToString("yyyy-MM-dd")
                );
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(650, 480);
            this.Name = "TimeZoneClockForm";
            this.Text = "Advanced Digital Clock with Time Zones";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.FormClosing += TimeZoneClockForm_FormClosing;
            this.ResumeLayout(false);
        }

        private void TimeZoneClockForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (updateTimer != null)
            {
                updateTimer.Stop();
                updateTimer.Dispose();
            }
        }
    }
}

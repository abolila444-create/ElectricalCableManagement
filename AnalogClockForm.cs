using System;
using System.Windows.Forms;
using System.Drawing;

namespace ElectricalCableManagement
{
    public partial class AnalogClockForm : Form
    {
        private Timer clockTimer;
        private Bitmap clockBitmap;
        private Graphics clockGraphics;
        private Label digitalTimeLabel;

        public AnalogClockForm()
        {
            InitializeComponent();
            SetupClock();
        }

        private void SetupClock()
        {
            clockTimer = new Timer();
            clockTimer.Interval = 1000;
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawClock(e.Graphics);
        }

        private void DrawClock(Graphics g)
        {
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int centerX = 200;
            int centerY = 200;
            int radius = 150;

            // Draw Clock Face
            g.DrawEllipse(new Pen(Color.Black, 3), centerX - radius, centerY - radius, radius * 2, radius * 2);
            g.FillEllipse(new SolidBrush(Color.White), centerX - radius, centerY - radius, radius * 2, radius * 2);

            // Draw Hour Numbers
            for (int i = 1; i <= 12; i++)
            {
                double angle = (i * 30 - 90) * Math.PI / 180;
                int x = centerX + (int)(Math.Cos(angle) * (radius - 30));
                int y = centerY + (int)(Math.Sin(angle) * (radius - 30));
                g.DrawString(i.ToString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, x - 10, y - 10);
            }

            // Draw Hour Markers
            for (int i = 0; i < 60; i++)
            {
                double angle = (i * 6) * Math.PI / 180;
                int x1 = centerX + (int)(Math.Cos(angle) * radius);
                int y1 = centerY + (int)(Math.Sin(angle) * radius);
                int x2, y2;

                if (i % 5 == 0)
                {
                    x2 = centerX + (int)(Math.Cos(angle) * (radius - 15));
                    y2 = centerY + (int)(Math.Sin(angle) * (radius - 15));
                    g.DrawLine(new Pen(Color.Black, 2), x1, y1, x2, y2);
                }
                else
                {
                    x2 = centerX + (int)(Math.Cos(angle) * (radius - 8));
                    y2 = centerY + (int)(Math.Sin(angle) * (radius - 8));
                    g.DrawLine(new Pen(Color.Gray, 1), x1, y1, x2, y2);
                }
            }

            // Get current time
            DateTime now = DateTime.Now;
            double second = now.Second + now.Millisecond / 1000.0;
            double minute = now.Minute + second / 60.0;
            double hour = (now.Hour % 12) + minute / 60.0;

            // Draw Hour Hand
            double hourAngle = (hour * 30 - 90) * Math.PI / 180;
            int hourX = centerX + (int)(Math.Cos(hourAngle) * 80);
            int hourY = centerY + (int)(Math.Sin(hourAngle) * 80);
            g.DrawLine(new Pen(Color.Black, 6), centerX, centerY, hourX, hourY);

            // Draw Minute Hand
            double minuteAngle = (minute * 6 - 90) * Math.PI / 180;
            int minuteX = centerX + (int)(Math.Cos(minuteAngle) * 110);
            int minuteY = centerY + (int)(Math.Sin(minuteAngle) * 110);
            g.DrawLine(new Pen(Color.Black, 4), centerX, centerY, minuteX, minuteY);

            // Draw Second Hand
            double secondAngle = (second * 6 - 90) * Math.PI / 180;
            int secondX = centerX + (int)(Math.Cos(secondAngle) * 120);
            int secondY = centerY + (int)(Math.Sin(secondAngle) * 120);
            g.DrawLine(new Pen(Color.Red, 2), centerX, centerY, secondX, secondY);

            // Draw Center Dot
            g.FillEllipse(Brushes.Black, centerX - 5, centerY - 5, 10, 10);

            // Draw Digital Time
            string digitalTime = now.ToString("HH:mm:ss");
            g.DrawString(digitalTime, new Font("Arial", 14, FontStyle.Bold),
                Brushes.Blue, centerX - 60, centerY + 170);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(450, 450);
            this.Name = "AnalogClockForm";
            this.Text = "Analog Clock";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.DoubleBuffered = true;
            this.FormClosing += AnalogClockForm_FormClosing;
            this.ResumeLayout(false);
        }

        private void AnalogClockForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clockTimer != null)
            {
                clockTimer.Stop();
                clockTimer.Dispose();
            }
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ElectricalCableManagement.Database;

namespace ElectricalCableManagement
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            try
            {
                using (SqlConnection con = DbConnection.GetConnection())
                {
                    con.Open();

                    // Total Records
                    SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM RoadLengths", con);
                    int totalRecords = (int)cmd1.ExecuteScalar();
                    lblTotalRecords.Text = $"Total Records: {totalRecords}";

                    // Total Length
                    SqlCommand cmd2 = new SqlCommand("SELECT SUM(TotalLength) FROM RoadLengths", con);
                    object result = cmd2.ExecuteScalar();
                    decimal totalLength = result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                    lblTotalLength.Text = $"Total Length: {totalLength:F2} units";

                    // Total Drums Used
                    SqlCommand cmd3 = new SqlCommand("SELECT COUNT(DISTINCT DrumNo) FROM RoadLengths", con);
                    int totalDrums = (int)cmd3.ExecuteScalar();
                    lblTotalDrums.Text = $"Total Drums Used: {totalDrums}";

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading statistics: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.lblTotalLength = new System.Windows.Forms.Label();
            this.lblTotalDrums = new System.Windows.Forms.Label();
            this.dgvRoadStatistics = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoadStatistics)).BeginInit();
            this.SuspendLayout();

            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalRecords.Location = new System.Drawing.Point(20, 20);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(150, 20);
            this.lblTotalRecords.TabIndex = 0;
            this.lblTotalRecords.Text = "Total Records: 0";

            this.lblTotalLength.AutoSize = true;
            this.lblTotalLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalLength.Location = new System.Drawing.Point(20, 60);
            this.lblTotalLength.Name = "lblTotalLength";
            this.lblTotalLength.Size = new System.Drawing.Size(150, 20);
            this.lblTotalLength.TabIndex = 1;
            this.lblTotalLength.Text = "Total Length: 0";

            this.lblTotalDrums.AutoSize = true;
            this.lblTotalDrums.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalDrums.Location = new System.Drawing.Point(20, 100);
            this.lblTotalDrums.Name = "lblTotalDrums";
            this.lblTotalDrums.Size = new System.Drawing.Size(150, 20);
            this.lblTotalDrums.TabIndex = 2;
            this.lblTotalDrums.Text = "Total Drums: 0";

            this.dgvRoadStatistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoadStatistics.Location = new System.Drawing.Point(20, 150);
            this.dgvRoadStatistics.Name = "dgvRoadStatistics";
            this.dgvRoadStatistics.Size = new System.Drawing.Size(700, 300);
            this.dgvRoadStatistics.TabIndex = 3;

            this.ClientSize = new System.Drawing.Size(740, 480);
            this.Controls.Add(this.dgvRoadStatistics);
            this.Controls.Add(this.lblTotalDrums);
            this.Controls.Add(this.lblTotalLength);
            this.Controls.Add(this.lblTotalRecords);
            this.Name = "ReportsForm";
            this.Text = "Reports and Statistics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoadStatistics)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.Label lblTotalLength;
        private System.Windows.Forms.Label lblTotalDrums;
        private System.Windows.Forms.DataGridView dgvRoadStatistics;
    }
}

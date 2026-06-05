using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ElectricalCableManagement.Database;

namespace ElectricalCableManagement
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
        }

        private void btnSearchRoadName_Click(object sender, EventArgs e)
        {
            SearchByRoadName();
        }

        private void btnSearchDrumNo_Click(object sender, EventArgs e)
        {
            SearchByDrumNo();
        }

        private void btnSearchDate_Click(object sender, EventArgs e)
        {
            SearchByDate();
        }

        private void SearchByRoadName()
        {
            if (string.IsNullOrWhiteSpace(txtSearchRoadName.Text))
            {
                MessageBox.Show("Please enter a road name", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = DbConnection.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT * FROM RoadLengths WHERE RoadName LIKE @RoadName", con);
                    da.SelectCommand.Parameters.AddWithValue("@RoadName", "%" + txtSearchRoadName.Text + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvResults.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchByDrumNo()
        {
            if (string.IsNullOrWhiteSpace(txtSearchDrumNo.Text))
            {
                MessageBox.Show("Please enter a drum number", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = DbConnection.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT * FROM RoadLengths WHERE DrumNo LIKE @DrumNo", con);
                    da.SelectCommand.Parameters.AddWithValue("@DrumNo", "%" + txtSearchDrumNo.Text + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvResults.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchByDate()
        {
            try
            {
                using (SqlConnection con = DbConnection.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT * FROM RoadLengths WHERE CAST(WorkDate AS DATE) = @WorkDate", con);
                    da.SelectCommand.Parameters.AddWithValue("@WorkDate", dtpSearchDate.Value.Date);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvResults.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.txtSearchRoadName = new System.Windows.Forms.TextBox();
            this.txtSearchDrumNo = new System.Windows.Forms.TextBox();
            this.dtpSearchDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearchRoadName = new System.Windows.Forms.Button();
            this.btnSearchDrumNo = new System.Windows.Forms.Button();
            this.btnSearchDate = new System.Windows.Forms.Button();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();

            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Road Name:";

            this.txtSearchRoadName.Location = new System.Drawing.Point(93, 12);
            this.txtSearchRoadName.Name = "txtSearchRoadName";
            this.txtSearchRoadName.Size = new System.Drawing.Size(150, 20);
            this.txtSearchRoadName.TabIndex = 1;

            this.btnSearchRoadName.Location = new System.Drawing.Point(249, 12);
            this.btnSearchRoadName.Name = "btnSearchRoadName";
            this.btnSearchRoadName.Size = new System.Drawing.Size(75, 23);
            this.btnSearchRoadName.TabIndex = 2;
            this.btnSearchRoadName.Text = "Search";
            this.btnSearchRoadName.UseVisualStyleBackColor = true;
            this.btnSearchRoadName.Click += new System.EventHandler(this.btnSearchRoadName_Click);

            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Drum No:";

            this.txtSearchDrumNo.Location = new System.Drawing.Point(93, 42);
            this.txtSearchDrumNo.Name = "txtSearchDrumNo";
            this.txtSearchDrumNo.Size = new System.Drawing.Size(150, 20);
            this.txtSearchDrumNo.TabIndex = 4;

            this.btnSearchDrumNo.Location = new System.Drawing.Point(249, 42);
            this.btnSearchDrumNo.Name = "btnSearchDrumNo";
            this.btnSearchDrumNo.Size = new System.Drawing.Size(75, 23);
            this.btnSearchDrumNo.TabIndex = 5;
            this.btnSearchDrumNo.Text = "Search";
            this.btnSearchDrumNo.UseVisualStyleBackColor = true;
            this.btnSearchDrumNo.Click += new System.EventHandler(this.btnSearchDrumNo_Click);

            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Work Date:";

            this.dtpSearchDate.Location = new System.Drawing.Point(93, 72);
            this.dtpSearchDate.Name = "dtpSearchDate";
            this.dtpSearchDate.Size = new System.Drawing.Size(150, 20);
            this.dtpSearchDate.TabIndex = 7;

            this.btnSearchDate.Location = new System.Drawing.Point(249, 72);
            this.btnSearchDate.Name = "btnSearchDate";
            this.btnSearchDate.Size = new System.Drawing.Size(75, 23);
            this.btnSearchDate.TabIndex = 8;
            this.btnSearchDate.Text = "Search";
            this.btnSearchDate.UseVisualStyleBackColor = true;
            this.btnSearchDate.Click += new System.EventHandler(this.btnSearchDate_Click);

            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(12, 110);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.Size = new System.Drawing.Size(680, 300);
            this.dgvResults.TabIndex = 9;

            this.ClientSize = new System.Drawing.Size(704, 422);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.btnSearchDate);
            this.Controls.Add(this.dtpSearchDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSearchDrumNo);
            this.Controls.Add(this.txtSearchDrumNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSearchRoadName);
            this.Controls.Add(this.txtSearchRoadName);
            this.Controls.Add(this.label1);
            this.Name = "SearchForm";
            this.Text = "Search Records";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtSearchRoadName;
        private System.Windows.Forms.TextBox txtSearchDrumNo;
        private System.Windows.Forms.DateTimePicker dtpSearchDate;
        private System.Windows.Forms.Button btnSearchRoadName;
        private System.Windows.Forms.Button btnSearchDrumNo;
        private System.Windows.Forms.Button btnSearchDate;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

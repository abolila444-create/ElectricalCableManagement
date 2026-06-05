using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ElectricalCableManagement.Database;
using ElectricalCableManagement.Utilities;

namespace ElectricalCableManagement
{
    public partial class MainForm : Form
    {
        private int selectedRowId = -1;

        public MainForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection con = DbConnection.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM RoadLengths", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvRoadLengths.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearFields();
            selectedRowId = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using (SqlConnection con = DbConnection.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(
                        @"INSERT INTO RoadLengths
                        (SpaceName, FromLocation, ToLocation, StartPoint, EndPoint, TotalLength,
                         DrumNo, DrumSerial, RoadName, MV, WorkDate, Remarks)
                        VALUES
                        (@SpaceName, @FromLocation, @ToLocation, @StartPoint, @EndPoint, @TotalLength,
                         @DrumNo, @DrumSerial, @RoadName, @MV, @WorkDate, @Remarks)", con);

                    AddParameters(cmd);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedRowId == -1)
            {
                MessageBox.Show("Please select a record to update", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput())
                return;

            try
            {
                using (SqlConnection con = DbConnection.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(
                        @"UPDATE RoadLengths SET
                        SpaceName=@SpaceName, FromLocation=@FromLocation, ToLocation=@ToLocation,
                        StartPoint=@StartPoint, EndPoint=@EndPoint, TotalLength=@TotalLength,
                        DrumNo=@DrumNo, DrumSerial=@DrumSerial, RoadName=@RoadName, MV=@MV,
                        WorkDate=@WorkDate, Remarks=@Remarks WHERE ID=@ID", con);

                    AddParameters(cmd);
                    cmd.Parameters.AddWithValue("@ID", selectedRowId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Data Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedRowId == -1)
            {
                MessageBox.Show("Please select a record to delete", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                using (SqlConnection con = DbConnection.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM RoadLengths WHERE ID=@ID", con);
                    cmd.Parameters.AddWithValue("@ID", selectedRowId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Data Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm();
            searchForm.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearFields();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveDialog.FileName = $"RoadLengths_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                ExcelExport.ExportToExcel(dgvRoadLengths, saveDialog.FileName);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print functionality to be implemented", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvRoadLengths_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowId = Convert.ToInt32(dgvRoadLengths.Rows[e.RowIndex].Cells["ID"].Value);
                txtSpaceName.Text = dgvRoadLengths.Rows[e.RowIndex].Cells["SpaceName"].Value?.ToString() ?? "";
                txtFrom.Text = dgvRoadLengths.Rows[e.RowIndex].Cells["FromLocation"].Value?.ToString() ?? "";
                txtTo.Text = dgvRoadLengths.Rows[e.RowIndex].Cells["ToLocation"].Value?.ToString() ?? "";
                txtStart.Text = dgvRoadLengths.Rows[e.RowIndex].Cells["StartPoint"].Value?.ToString() ?? "";
                txtEnd.Text = dgvRoadLengths.Rows[e.RowIndex].Cells["EndPoint"].Value?.ToString() ?? "";
                txtTotal.Text = dgvRoadLengths.Rows[e.RowIndex].Cells["TotalLength"].Value?.ToString() ?? "";
                txtDrumNo.Text = dgvRoadLengths.Rows[e.RowIndex].Cells["DrumNo"].Value?.ToString() ?? "";
                txtDrumSerial.Text = dgvRoadLengths.Rows[e.RowIndex].Cells["DrumSerial"].Value?.ToString() ?? "";
                txtRoadName.Text = dgvRoadLengths.Rows[e.RowIndex].Cells["RoadName"].Value?.ToString() ?? "";
                txtMV.Text = dgvRoadLengths.Rows[e.RowIndex].Cells["MV"].Value?.ToString() ?? "";
                dtpDate.Value = Convert.ToDateTime(dgvRoadLengths.Rows[e.RowIndex].Cells["WorkDate"].Value);
                txtRemarks.Text = dgvRoadLengths.Rows[e.RowIndex].Cells["Remarks"].Value?.ToString() ?? "";
            }
        }

        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@SpaceName", txtSpaceName.Text);
            cmd.Parameters.AddWithValue("@FromLocation", txtFrom.Text);
            cmd.Parameters.AddWithValue("@ToLocation", txtTo.Text);
            cmd.Parameters.AddWithValue("@StartPoint", decimal.Parse(txtStart.Text));
            cmd.Parameters.AddWithValue("@EndPoint", decimal.Parse(txtEnd.Text));
            cmd.Parameters.AddWithValue("@TotalLength", decimal.Parse(txtTotal.Text));
            cmd.Parameters.AddWithValue("@DrumNo", txtDrumNo.Text);
            cmd.Parameters.AddWithValue("@DrumSerial", txtDrumSerial.Text);
            cmd.Parameters.AddWithValue("@RoadName", txtRoadName.Text);
            cmd.Parameters.AddWithValue("@MV", txtMV.Text);
            cmd.Parameters.AddWithValue("@WorkDate", dtpDate.Value);
            cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtSpaceName.Text))
            {
                MessageBox.Show("Space Name is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtStart.Text, out _) || !decimal.TryParse(txtEnd.Text, out _) || !decimal.TryParse(txtTotal.Text, out _))
            {
                MessageBox.Show("Start Point, End Point, and Total Length must be numeric", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearFields()
        {
            txtSpaceName.Clear();
            txtFrom.Clear();
            txtTo.Clear();
            txtStart.Clear();
            txtEnd.Clear();
            txtTotal.Clear();
            txtDrumNo.Clear();
            txtDrumSerial.Clear();
            txtRoadName.Clear();
            txtMV.Clear();
            dtpDate.Value = DateTime.Now;
            txtRemarks.Clear();
            selectedRowId = -1;
        }

        private void InitializeComponent()
        {
            this.dgvRoadLengths = new System.Windows.Forms.DataGridView();
            this.txtSpaceName = new System.Windows.Forms.TextBox();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtDrumNo = new System.Windows.Forms.TextBox();
            this.txtDrumSerial = new System.Windows.Forms.TextBox();
            this.txtRoadName = new System.Windows.Forms.TextBox();
            this.txtMV = new System.Windows.Forms.TextBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoadLengths)).BeginInit();
            this.SuspendLayout();

            // DataGridView
            this.dgvRoadLengths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoadLengths.Location = new System.Drawing.Point(12, 250);
            this.dgvRoadLengths.Name = "dgvRoadLengths";
            this.dgvRoadLengths.Size = new System.Drawing.Size(900, 300);
            this.dgvRoadLengths.TabIndex = 0;
            this.dgvRoadLengths.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRoadLengths_CellClick);

            // Text boxes
            this.txtSpaceName.Location = new System.Drawing.Point(12, 30);
            this.txtSpaceName.Name = "txtSpaceName";
            this.txtSpaceName.Size = new System.Drawing.Size(200, 20);
            this.txtSpaceName.TabIndex = 1;

            this.txtFrom.Location = new System.Drawing.Point(220, 30);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(150, 20);
            this.txtFrom.TabIndex = 2;

            this.txtTo.Location = new System.Drawing.Point(380, 30);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(150, 20);
            this.txtTo.TabIndex = 3;

            this.txtStart.Location = new System.Drawing.Point(540, 30);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(100, 20);
            this.txtStart.TabIndex = 4;

            this.txtEnd.Location = new System.Drawing.Point(650, 30);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(100, 20);
            this.txtEnd.TabIndex = 5;

            this.txtTotal.Location = new System.Drawing.Point(760, 30);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 6;

            this.txtDrumNo.Location = new System.Drawing.Point(12, 70);
            this.txtDrumNo.Name = "txtDrumNo";
            this.txtDrumNo.Size = new System.Drawing.Size(150, 20);
            this.txtDrumNo.TabIndex = 7;

            this.txtDrumSerial.Location = new System.Drawing.Point(170, 70);
            this.txtDrumSerial.Name = "txtDrumSerial";
            this.txtDrumSerial.Size = new System.Drawing.Size(150, 20);
            this.txtDrumSerial.TabIndex = 8;

            this.txtRoadName.Location = new System.Drawing.Point(328, 70);
            this.txtRoadName.Name = "txtRoadName";
            this.txtRoadName.Size = new System.Drawing.Size(150, 20);
            this.txtRoadName.TabIndex = 9;

            this.txtMV.Location = new System.Drawing.Point(486, 70);
            this.txtMV.Name = "txtMV";
            this.txtMV.Size = new System.Drawing.Size(100, 20);
            this.txtMV.TabIndex = 10;

            this.dtpDate.Location = new System.Drawing.Point(594, 70);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(150, 20);
            this.dtpDate.TabIndex = 11;

            this.txtRemarks.Location = new System.Drawing.Point(12, 110);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Size = new System.Drawing.Size(732, 60);
            this.txtRemarks.TabIndex = 12;

            // Buttons
            this.btnAdd.Location = new System.Drawing.Point(12, 180);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnSave.Location = new System.Drawing.Point(93, 180);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnUpdate.Location = new System.Drawing.Point(174, 180);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 15;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            this.btnDelete.Location = new System.Drawing.Point(255, 180);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnSearch.Location = new System.Drawing.Point(336, 180);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 17;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.btnRefresh.Location = new System.Drawing.Point(417, 180);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 18;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.btnExportExcel.Location = new System.Drawing.Point(498, 180);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(100, 23);
            this.btnExportExcel.TabIndex = 19;
            this.btnExportExcel.Text = "Export Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

            this.btnPrint.Location = new System.Drawing.Point(604, 180);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 20;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);

            this.btnExit.Location = new System.Drawing.Point(685, 180);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 21;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            this.ClientSize = new System.Drawing.Size(924, 571);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.txtMV);
            this.Controls.Add(this.txtRoadName);
            this.Controls.Add(this.txtDrumSerial);
            this.Controls.Add(this.txtDrumNo);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.txtSpaceName);
            this.Controls.Add(this.dgvRoadLengths);
            this.Name = "MainForm";
            this.Text = "Electrical Cable Management System";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoadLengths)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvRoadLengths;
        private System.Windows.Forms.TextBox txtSpaceName;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtDrumNo;
        private System.Windows.Forms.TextBox txtDrumSerial;
        private System.Windows.Forms.TextBox txtRoadName;
        private System.Windows.Forms.TextBox txtMV;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExit;
    }
}

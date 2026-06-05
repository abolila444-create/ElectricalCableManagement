using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace ElectricalCableManagement.Utilities
{
    public class ExcelExport
    {
        public static void ExportToExcel(DataGridView dgv, string fileName)
        {
            try
            {
                Application excelApp = new Application();
                Workbook workbook = excelApp.Workbooks.Add();
                Worksheet worksheet = workbook.ActiveSheet;

                // Add headers
                for (int col = 0; col < dgv.Columns.Count; col++)
                {
                    worksheet.Cells[1, col + 1] = dgv.Columns[col].HeaderText;
                }

                // Add data
                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    for (int col = 0; col < dgv.Columns.Count; col++)
                    {
                        worksheet.Cells[row + 2, col + 1] = dgv.Rows[row].Cells[col].Value;
                    }
                }

                worksheet.Columns.AutoFit();
                workbook.SaveAs(fileName);
                workbook.Close();
                excelApp.Quit();

                MessageBox.Show($"Data exported successfully to {fileName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to Excel: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

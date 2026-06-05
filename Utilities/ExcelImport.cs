using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ElectricalCableManagement.Utilities
{
    public class ExcelImport
    {
        public static DataTable ImportFromExcel(string filePath)
        {
            try
            {
                string connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                    
                    OleDbDataAdapter adapter = new OleDbDataAdapter($"SELECT * FROM [{sheetName}]", conn);
                    DataTable result = new DataTable();
                    adapter.Fill(result);
                    
                    return result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing Excel file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}

using System;
using System.Data.SqlClient;

namespace ElectricalCableManagement.Database
{
    public class DbConnection
    {
        private static string connectionString = @"Server=.\SQLEXPRESS;Database=ElectricalCableDB;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    con.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

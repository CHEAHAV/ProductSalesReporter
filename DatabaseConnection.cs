using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProductSalesReporter
{
    internal class DatabaseConnection
    {
        string str = "Data Source=MSI\\DBMS;Initial Catalog=DB_PRODUCTSALES;Integrated Security=True";
        public SqlConnection con;

        public void SystemConnection()
        {
            try
            {
                con = new SqlConnection(str);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.LogError($"SQL Connection Error: Could not open connection to database. " +
                                $"Connection String: {str}. Error: {sqlEx.Message}\nStackTrace: {sqlEx.StackTrace}");
                MessageBox.Show($"Database connection failed. Please check your connection string and server availability. Details logged to errors.txt. Error: {sqlEx.Message}",
                                "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Logger.LogError($"General Connection Error: An unexpected error occurred while opening database connection. " +
                                $"Connection String: {str}. Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                MessageBox.Show($"An unexpected error occurred during database connection. Details logged to errors.txt. Error: {ex.Message}",
                                "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
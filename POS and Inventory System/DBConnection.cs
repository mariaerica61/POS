using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace POS_and_Inventory_System
{
    internal class DBConnection
    {
        private SqlConnection cn;
        private SqlCommand cm;
        private SqlDataReader dr;
        

        public string MyConnection()
        {
            return @"Data Source=MARII\SQLEXPRESS01;Initial Catalog=POS;Integrated Security=True;Encrypt=False";
        }

        public double DailySales()
        {
            double dailysales = 0;
            string query = "SELECT ISNULL(SUM(total), 0) FROM tblCart WHERE sdate BETWEEN @startDate AND @endDate AND status = 'Sold'";

            using (cn = new SqlConnection(MyConnection()))
            {
                cn.Open();
                using (cm = new SqlCommand(query, cn))
                {
                    // Use today's date for both start and end
                    DateTime today = DateTime.Today;
                    cm.Parameters.AddWithValue("@startDate", today);
                    cm.Parameters.AddWithValue("@endDate", today);

                    dailysales = Convert.ToDouble(cm.ExecuteScalar());
                }
            }

            return dailysales;
        }

        public int ProductLine()
        {
            int productline = 0;
            string query = "SELECT COUNT(*) FROM tblProduct";

            using (cn = new SqlConnection(MyConnection()))
            {
                cn.Open();
                using (cm = new SqlCommand(query, cn))
                {
                    productline = Convert.ToInt32(cm.ExecuteScalar());
                }
            }

            return productline;
        }

        public int StockOnHand()
        {
            int stockonhand = 0;
            string query = "SELECT ISNULL(SUM(qty), 0) FROM tblProduct";

            using (cn = new SqlConnection(MyConnection()))
            {
                cn.Open();
                using (cm = new SqlCommand(query, cn))
                {
                    stockonhand = Convert.ToInt32(cm.ExecuteScalar());
                }
            }

            return stockonhand;
        }

        public int CriticalItems()
        {
            int criticalitems = 0;
            string query = "SELECT COUNT(*) FROM vwCriticalItems";

            using (cn = new SqlConnection(MyConnection()))
            {
                cn.Open();
                using (cm = new SqlCommand(query, cn))
                {
                    criticalitems = Convert.ToInt32(cm.ExecuteScalar());
                }
            }

            return criticalitems;
        }

        public double GetVal()
        {
            double vat = 0;

            // Initialize the connection inside a using statement to ensure it's disposed after use
            using (cn = new SqlConnection(MyConnection()))
            {
                cn.Open();  // Open the connection inside the using block

                cm = new SqlCommand("SELECT * FROM tblVat", cn);
                dr = cm.ExecuteReader();  // Execute the reader

                while (dr.Read())
                {
                    vat = Double.Parse(dr["vat"].ToString());
                }

                dr.Close();  // Always close the reader after use
            }

            return vat;
        }

        public string GetPassword(string user)
        {
            string password = " ";

            // Initialize the connection inside a using statement to ensure it's disposed after use
            using (cn = new SqlConnection(MyConnection()))
            {
                cn.Open();  // Open the connection inside the using block

                cm = new SqlCommand("SELECT * FROM tblUser WHERE username = @username", cn);
                cm.Parameters.AddWithValue("@username", user);

                dr = cm.ExecuteReader();  // Execute the reader

                if (dr.Read())  // Check if data is returned
                {
                    password = dr["password"].ToString();
                }

                dr.Close();  // Always close the reader after use
            }

            return password;
        }

    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmLookUp : Form
    {
        frmPOS f;
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        string stitle = "POS and Inventory System";

        public frmLookUp(frmPOS frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            f = frm;
            this.KeyPreview = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadProduct()
        {
            int i = 0;
            dataGridView3.Rows.Clear();

            try
            {
                cn.Open();

                // Enhanced search query that searches across barcode, description, brand, and category
                string searchQuery = @"
            SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty 
            FROM tblProduct as p 
            INNER JOIN tblBrand as b ON b.id = p.bid 
            INNER JOIN tblCategory as c ON c.id = p.cid 
            WHERE p.barcode LIKE @searchTerm 
               OR p.pdesc LIKE @searchTerm 
               OR b.brand LIKE @searchTerm 
               OR c.category LIKE @searchTerm";

                cm = new SqlCommand(searchQuery, cn);
                cm.Parameters.AddWithValue("@searchTerm", "%" + txtSearch.Text + "%");

                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView3.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(),
                                           dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmLookUp_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView3.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                frmQty frm = new frmQty(f);
                frm.ProductDetails(dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString(), Double.Parse(dataGridView3.Rows[e.RowIndex].Cells[6].Value.ToString()), f.lblTransno.Text, int.Parse(dataGridView3.Rows[e.RowIndex].Cells[7].Value.ToString()));
                frm.ShowDialog();
            }

        }

        private void frmLookUp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }

        }
    }
}

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

    public partial class frmProductList : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;

        public frmProductList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct(this);
            frm.btnSave.Enabled = true;
            frm.btnUpdate.Enabled = false;
            frm.LoadBrand();
            frm.LoadCategory();
            frm.ShowDialog();
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
            SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.reorder 
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
                MessageBox.Show("Error loading products: " + ex.Message, "POS and Inventory System",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView3.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                frmProduct frm = new frmProduct(this);
                frm.btnSave.Enabled = false;
                frm.btnUpdate.Enabled = true;
                frm.txtPcode.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.txtBarcode.Text = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtPdesc.Text = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.txtPrice.Text = dataGridView3.Rows[e.RowIndex].Cells[6].Value.ToString();
                frm.cboBrand.Text = dataGridView3.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm.cboCategory.Text = dataGridView3.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm.txtReorder.Text = dataGridView3.Rows[e.RowIndex].Cells[7].Value.ToString();
                frm.ShowDialog();

            }
            else if(colName =="Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this product?", "Delete Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    Console.WriteLine(dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString());
                    cm = new SqlCommand("DELETE FROM tblProduct where pcode like '" + dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadProduct();
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }

        private void frmProductList_Load(object sender, EventArgs e)
        {
 

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtSearch_Click_1(object sender, EventArgs e)
        {

        }
    }
}

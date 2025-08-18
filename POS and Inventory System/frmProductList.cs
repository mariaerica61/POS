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
using System.IO;

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

                // Enhanced query with multiple ordering criteria to ensure newest products appear first
                string searchQuery = @"
            SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.reorder, p.image, p.date_created
            FROM tblProduct p 
            INNER JOIN tblBrand b ON b.id = p.bid 
            INNER JOIN tblCategory c ON c.id = p.cid 
            WHERE ISNULL(p.barcode, '') LIKE @searchTerm 
               OR ISNULL(p.pdesc, '') LIKE @searchTerm 
               OR ISNULL(b.brand, '') LIKE @searchTerm 
               OR ISNULL(c.category, '') LIKE @searchTerm
            ORDER BY p.date_created DESC, p.pcode DESC";

                cm = new SqlCommand(searchQuery, cn);
                cm.Parameters.AddWithValue("@searchTerm", "%" + txtSearch.Text + "%");

                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;

                    // Handle image loading
                    Image image = null;
                    try
                    {
                        if (dr["image"] != DBNull.Value)
                        {
                            byte[] imgBytes = (byte[])dr["image"];
                            if (imgBytes != null && imgBytes.Length > 0)
                            {
                                using (MemoryStream ms = new MemoryStream(imgBytes))
                                {
                                    image = Image.FromStream(ms);
                                }
                            }
                        }
                    }
                    catch (Exception imgEx)
                    {
                        Console.WriteLine("Error loading product image: " + imgEx.Message);
                        image = null;
                    }

                    // Add row to DataGridView
                    dataGridView3.Rows.Add(i,
                        dr["pcode"].ToString(),
                        dr["barcode"].ToString(),
                        dr["pdesc"].ToString(),
                        dr["brand"].ToString(),
                        dr["category"].ToString(),
                        dr["price"].ToString(),
                        dr["reorder"].ToString(),
                        image
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message,
                                "POS and Inventory System",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                    dr.Close();

                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string colName = dataGridView3.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                frmProduct frm = new frmProduct(this);
                frm.btnSave.Enabled = false;
                frm.btnUpdate.Enabled = true;
                frm.LoadBrand();
                frm.LoadCategory();

                frm.txtPcode.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.txtBarcode.Text = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtPdesc.Text = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.txtPrice.Text = dataGridView3.Rows[e.RowIndex].Cells[6].Value.ToString();
                frm.cboBrand.Text = dataGridView3.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm.cboCategory.Text = dataGridView3.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm.txtReorder.Text = dataGridView3.Rows[e.RowIndex].Cells[7].Value.ToString();

                // Load image into PictureBox2 - FIXED: Changed index from 10 to 8
                if (dataGridView3.Rows[e.RowIndex].Cells[8].Value != null)
                {
                    frm.PictureBox2.Image = (Image)dataGridView3.Rows[e.RowIndex].Cells[8].Value;
                    frm.PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    // Clear the picture box if no image
                    frm.PictureBox2.Image = null;
                }

                frm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this product?", "Delete Product",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        // Ensure connection is closed before opening
                        if (cn.State == ConnectionState.Open)
                            cn.Close();

                        cn.Open();
                        cm = new SqlCommand("DELETE FROM tblProduct where pcode=@pcode", cn);
                        cm.Parameters.AddWithValue("@pcode", dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString());
                        cm.ExecuteNonQuery();

                        MessageBox.Show("Product deleted successfully!", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting product: " + ex.Message, "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // Ensure connection is properly closed
                        if (cn.State == ConnectionState.Open)
                            cn.Close();
                    }

                    // Now reload the products with a clean connection state
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
            LoadProduct();

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

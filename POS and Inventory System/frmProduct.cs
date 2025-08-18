using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Data;



namespace POS_and_Inventory_System
{
    public partial class frmProduct : Form
    {
        SqlConnection cn = new SqlConnection("Data Source=MARII\\SQLEXPRESS01;Initial Catalog=POS_DEMO_DB;Integrated Security=True;Encrypt=False");
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        frmProductList flist;
        string selectedImagePath = string.Empty;


        public frmProduct(frmProductList frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            flist = frm;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCategory()
        {
            try
            {
                DataTable dt = new DataTable();
                cn.Open();

                // Using your exact database schema
                cm = new SqlCommand("SELECT id, category FROM tblCategory ORDER BY category", cn);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                da.Fill(dt);

                cboCategory.DataSource = dt;
                cboCategory.DisplayMember = "category";
                cboCategory.ValueMember = "id";
                cboCategory.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading categories", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public void LoadBrand()
        {
            try
            {
                DataTable dt = new DataTable();
                cn.Open();

                // Using your exact database schema
                cm = new SqlCommand("SELECT id, brand FROM tblBrand ORDER BY brand", cn);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                da.Fill(dt);

                cboBrand.DataSource = dt;
                cboBrand.DisplayMember = "brand";
                cboBrand.ValueMember = "id";
                cboBrand.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading brands", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] imgData = null;

                if (PictureBox2.Image != null)
                {
                    // Create a new bitmap to avoid GDI+ errors
                    using (Bitmap bmp = new Bitmap(PictureBox2.Image))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // Save as PNG to avoid format issues
                            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            imgData = ms.ToArray();
                        }
                    }
                }

                cn.Open();
                // Updated query to include date_created with GETDATE()
                string query = "INSERT INTO tblProduct (pcode, barcode, pdesc, bid, cid, price, reorder, image, date_created) " +
                               "VALUES (@pcode, @barcode, @pdesc, @bid, @cid, @price, @reorder, @image, GETDATE())";

                cm = new SqlCommand(query, cn);
                cm.Parameters.AddWithValue("@pcode", txtPcode.Text);
                cm.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                cm.Parameters.AddWithValue("@pdesc", txtPdesc.Text);
                cm.Parameters.AddWithValue("@bid", cboBrand.SelectedValue);
                cm.Parameters.AddWithValue("@cid", cboCategory.SelectedValue);
                cm.Parameters.AddWithValue("@price", Convert.ToDouble(txtPrice.Text));
                cm.Parameters.AddWithValue("@reorder", Convert.ToInt32(txtReorder.Text));
                cm.Parameters.AddWithValue("@image", (object)imgData ?? DBNull.Value);

                cm.ExecuteNonQuery();

                MessageBox.Show("Product has been successfully saved.", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Clear();
                flist.LoadProduct(); // Refresh the product list
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
            }
        }

        public void Clear()
        {
            txtPcode.Clear();
            txtBarcode.Clear();
            txtPdesc.Clear();
            txtPrice.Clear();
            txtReorder.Clear();
            cboBrand.SelectedIndex = -1;
            cboCategory.SelectedIndex = -1;
            PictureBox2.Image = null;
            selectedImagePath = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrEmpty(txtPcode.Text))
                {
                    MessageBox.Show("Product code is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                byte[] imgData = null;

                if (PictureBox2.Image != null)
                {
                    // Create a new bitmap to avoid GDI+ errors
                    using (Bitmap bmp = new Bitmap(PictureBox2.Image))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // Save as PNG to avoid format issues
                            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            imgData = ms.ToArray();
                        }
                    }
                }

                // Ensure connection is closed before opening
                if (cn.State == ConnectionState.Open)
                    cn.Close();

                cn.Open();

                // Debug: Check if product exists
                string checkQuery = "SELECT COUNT(*) FROM tblProduct WHERE pcode=@pcode";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, cn))
                {
                    checkCmd.Parameters.AddWithValue("@pcode", txtPcode.Text);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count == 0)
                    {
                        MessageBox.Show("Product not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string query = "UPDATE tblProduct SET barcode=@barcode, pdesc=@pdesc, bid=@bid, cid=@cid, " +
                               "price=@price, reorder=@reorder, image=@image WHERE pcode=@pcode";

                cm = new SqlCommand(query, cn);
                cm.Parameters.AddWithValue("@barcode", txtBarcode.Text ?? "");
                cm.Parameters.AddWithValue("@pdesc", txtPdesc.Text ?? "");
                cm.Parameters.AddWithValue("@bid", cboBrand.SelectedValue ?? DBNull.Value);
                cm.Parameters.AddWithValue("@cid", cboCategory.SelectedValue ?? DBNull.Value);
                cm.Parameters.AddWithValue("@price", string.IsNullOrEmpty(txtPrice.Text) ? 0 : Convert.ToDouble(txtPrice.Text));
                cm.Parameters.AddWithValue("@reorder", string.IsNullOrEmpty(txtReorder.Text) ? 0 : Convert.ToInt32(txtReorder.Text));
                cm.Parameters.AddWithValue("@image", (object)imgData ?? DBNull.Value);
                cm.Parameters.AddWithValue("@pcode", txtPcode.Text);

                int rowsAffected = cm.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Product has been successfully updated.", "Update Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flist.LoadProduct(); // Refresh the product list
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("No changes were made to the product.", "Update Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 46)
            {
                //accept.character
            }
            else if (e.KeyChar == 8)
            {
                //accept backspace
            }
            else if ((e.KeyChar < 48) || (e.KeyChar > 57)) //ascii code 48-57 between 0 - 9
            {
                e.Handled = true;
            }
        }

        private void txtPcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    PictureBox2.Image = Image.FromFile(ofd.FileName);
                    PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void LoadProductImage(string pcode)
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT image FROM tblProduct WHERE pcode=@pcode", cn);
                cm.Parameters.AddWithValue("@pcode", pcode);

                dr = cm.ExecuteReader();
                if (dr.Read() && dr["image"] != DBNull.Value)
                {
                    byte[] imgData = (byte[])dr["image"];
                    using (MemoryStream ms = new MemoryStream(imgData))
                    {
                        PictureBox2.Image = Image.FromStream(ms);
                        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                    dr.Close();
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
            }
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DebugProductData()
        {
            string debugMessage = $"Product Code: {txtPcode.Text}\n" +
                                 $"Barcode: {txtBarcode.Text}\n" +
                                 $"Description: {txtPdesc.Text}\n" +
                                 $"Brand Selected: {cboBrand.SelectedValue} ({cboBrand.Text})\n" +
                                 $"Category Selected: {cboCategory.SelectedValue} ({cboCategory.Text})\n" +
                                 $"Price: {txtPrice.Text}\n" +
                                 $"Reorder: {txtReorder.Text}\n" +
                                 $"Has Image: {(PictureBox2.Image != null ? "Yes" : "No")}";

            MessageBox.Show(debugMessage, "Debug Product Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}


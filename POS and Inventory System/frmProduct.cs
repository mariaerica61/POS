using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Drawing;
using System.IO;



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
            cboCategory.Items.Clear();
            List<string> categories = new List<string>();

            cn.Open();
            cm = new SqlCommand("SELECT category FROM tblCategory", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                categories.Add(dr[0].ToString());
            }
            dr.Close();
            cn.Close();

            categories.Sort();
            cboCategory.Items.AddRange(categories.ToArray());
        }

        public void LoadBrand()
        {
            cboBrand.Items.Clear();
            List<string> brands = new List<string>();

            cn.Open();
            cm = new SqlCommand("SELECT brand FROM tblBrand", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                brands.Add(dr[0].ToString());
            }
            dr.Close();
            cn.Close();

            brands.Sort();
            cboBrand.Items.AddRange(brands.ToArray());

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
                    using (MemoryStream ms = new MemoryStream())
                    {
                        PictureBox2.Image.Save(ms, PictureBox2.Image.RawFormat);
                        imgData = ms.ToArray();
                    }
                }

                cn.Open();
                string query = "INSERT INTO tblProduct (pcode, barcode, pdesc, bid, cid, price, reorder, image) " +
                               "VALUES (@pcode, @barcode, @pdesc, @bid, @cid, @price, @reorder, @image)";

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
                byte[] imgData = null;

                if (PictureBox2.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        PictureBox2.Image.Save(ms, PictureBox2.Image.RawFormat);
                        imgData = ms.ToArray();
                    }
                }

                cn.Open();
                string query = "UPDATE tblProduct SET barcode=@barcode, pdesc=@pdesc, bid=@bid, cid=@cid, " +
                               "price=@price, reorder=@reorder, image=@image WHERE pcode=@pcode";

                cm = new SqlCommand(query, cn);
                cm.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                cm.Parameters.AddWithValue("@pdesc", txtPdesc.Text);
                cm.Parameters.AddWithValue("@bid", cboBrand.SelectedValue);
                cm.Parameters.AddWithValue("@cid", cboCategory.SelectedValue);
                cm.Parameters.AddWithValue("@price", Convert.ToDouble(txtPrice.Text));
                cm.Parameters.AddWithValue("@reorder", Convert.ToInt32(txtReorder.Text));
                cm.Parameters.AddWithValue("@image", (object)imgData ?? DBNull.Value);
                cm.Parameters.AddWithValue("@pcode", txtPcode.Text);

                cm.ExecuteNonQuery();

                MessageBox.Show("Product has been successfully updated.", "Update Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
    }
}


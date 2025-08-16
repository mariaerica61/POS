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
    public partial class frmAuthor : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        frmAuthorList frmlist;

        public frmAuthor(frmAuthorList flist)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            frmlist = flist;

            
        }

        

        private void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtAuthor.Clear();
            txtAuthor.Focus();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (MessageBox.Show("Are you sure you want to save this author?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTo tblAuthor(author)VALUEs(@author)", cn);
                    cm.Parameters.AddWithValue("@author", txtAuthor.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Author has been successfully saved.");
                    Clear();
                    frmlist.LoadRecord();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAuthor.Text))
                {
                    MessageBox.Show("Author field is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAuthor.Focus();
                    return;
                }

                if (MessageBox.Show("Are you sure you want to update this author?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("update tblAuthor set author = @author where id like '" + lblID.Text + "'", cn);
                    cm.Parameters.AddWithValue("@author", txtAuthor.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Author has been successfully updated. ");
                    Clear();
                    frmlist.LoadRecord();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtAuthor_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmAuthor_Load(object sender, EventArgs e)
        {

        }
    }
}


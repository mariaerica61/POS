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
    public partial class frmChangePassword : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        string stitle = "POS and Inventory System";
        frmPOS f;

        public frmChangePassword(frmPOS frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            f = frm;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtOld.Text) || string.IsNullOrWhiteSpace(txtNew.Text) || string.IsNullOrWhiteSpace(txtConfirm.Text))
                {
                    MessageBox.Show("Please fill in all the fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }

                string _oldpass = dbcon.GetPassword(f.lblName.Text);

                if (_oldpass != txtOld.Text)
                {
                    MessageBox.Show("Old password did not matched!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtNew.Text != txtConfirm.Text)
                {
                    MessageBox.Show("Confirm new password did not matched!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    {

                    }
                }
                else
                {
                    if (MessageBox.Show("Change Password?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("Update tblUser set password = @password where username = @username", cn);
                        cm.Parameters.AddWithValue("@password", txtNew.Text);
                        cm.Parameters.AddWithValue("@username", f.lblName.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Password has been successfully savedl", "Save Changes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}


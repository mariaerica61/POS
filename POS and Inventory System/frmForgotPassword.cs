using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_and_Inventory_System
{
    public partial class frmForgotPassword : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();

        public frmForgotPassword()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void frmForgotPassword_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void txtNEW_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRetype_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUser_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            cn.Open();
            cm = new SqlCommand("select username, password from tblUser where username = '" + txtUser.Text + "'", cn);
            dr = cm.ExecuteReader();
            if (dr.Read()) 
            {
                txtForgot.Text = dr[1].ToString();
            }
            else
            {
                MessageBox.Show("Username is not registered");
                txtForgot.Text = "";
            }
            dr.Close();
            cn.Close();
        }

        private void txtReset_Click(object sender, EventArgs e)
        {
            frmSecurity frm = new frmSecurity();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
   

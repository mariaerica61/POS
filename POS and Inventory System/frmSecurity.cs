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
    public partial class frmSecurity : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        public string _pass, _username = "";

        public frmSecurity()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("EXIT APPLICATION?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string _role = "", _name = "";
            try
            {
                if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text))
                {
                    MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool found = false;
                cn.Open();
                cm = new SqlCommand("select * from tblUser where username = @username and password = @password", cn);
                cm.Parameters.AddWithValue("@username", txtUser.Text);
                cm.Parameters.AddWithValue("@password", txtPass.Text);
                dr = cm.ExecuteReader();

                if (dr.HasRows) // Account exists
                {
                    dr.Read();
                    found = true;
                    _username = dr["username"].ToString();
                    _role = dr["role"].ToString();
                    _name = dr["name"].ToString();
                    _pass = dr["password"].ToString();
                   
                }

                dr.Close();
                cn.Close();

                if (found)
                {
                   
                    if (_role == "Cashier")
                    {
                        MessageBox.Show("Welcome " + _name + "!", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPass.Clear();
                        txtUser.Clear();
                        this.Hide();
                        frmPOS frm = new frmPOS(this);
                        frm.lblName.Text = _username;
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Welcome " + _name + "!", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPass.Clear();
                        txtUser.Clear();
                        this.Hide();
                        Form1 frm = new Form1();
                        frm.lblname.Text = _name;
                        frm._pass = _pass;
                        frm._user = _username;
                        frm.Dashboard();
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Account is unregistered or invalid username/password.", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnForgot_Click(object sender, EventArgs e)
        {
            
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtReset_Click(object sender, EventArgs e)
        {
            frmForgotPassword forgotPasswordForm = new frmForgotPassword();
            this.Hide();
            forgotPasswordForm.ShowDialog();
            this.Show();

        }

        private void frmSecurity_Load(object sender, EventArgs e)
        {

        }
    }
}

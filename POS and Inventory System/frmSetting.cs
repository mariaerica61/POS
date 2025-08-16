using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace POS_and_Inventory_System
{
    public partial class frmSetting : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        Form1 f;

        public frmSetting(Form1 f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.f = f;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void metroTabPage1_Resize(object sender, EventArgs e)
        {

        }

        private void frmSetting_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        private void Clear()
        {
            txtName.Clear();
            txtPass.Clear();
            txtRetype.Clear();
            txtUser.Clear();
            cboRole.Text = " ";
            txtUser.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text) ||
           string.IsNullOrEmpty(txtRetype.Text) || string.IsNullOrEmpty(cboRole.Text) ||
           string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Please fill in all fields before saving.", "Validation Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtPass.Text != txtRetype.Text)
                {
                    MessageBox.Show("Password did not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                cn.Open();

                cm = new SqlCommand("insert into tblUser (username, password, role, name) values(@username, @password, @role, @name)", cn);

                cm.Parameters.AddWithValue("@username", txtUser.Text);
                cm.Parameters.AddWithValue("@password", txtPass.Text);
                cm.Parameters.AddWithValue("@role", cboRole.Text);
                cm.Parameters.AddWithValue("@name", txtName.Text);
                cm.ExecuteNonQuery();
                cn.Close();

                MessageBox.Show("New account has saved!");
                Clear();

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void LoadRecords()
        {
            cn.Open();
            cm = new SqlCommand("select * from tblStore", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)

            {
                txtAddress.Text = dr["address"].ToString();
                txtStore.Text = dr["store"].ToString();
            }
            else
            {
                txtStore.Clear();
                txtAddress.Clear();
            }
            dr.Close();
            cn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtStore.Text) || string.IsNullOrEmpty(txtAddress.Text))
                {
                    MessageBox.Show("Please fill in all store details before saving.", "Validation Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("SAVE STORE DETAILS?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) ;
                {
                    int count;
                    cn.Open();
                    cm = new SqlCommand("select count(*) from tblStore", cn);
                    count = int.Parse(cm.ExecuteScalar().ToString());
                    cn.Close();

                    if (count > 0)
                    {
                        cn.Open();
                        cm = new SqlCommand("update tblStore set store=@store, address=@address", cn);
                        cm.Parameters.AddWithValue("@store", txtStore.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.ExecuteNonQuery();
                    }
                    else
                    {
                        cn.Open();
                        cm = new SqlCommand("insert into tblStore (store, address) values (@store,@address)", cn);
                        cm.Parameters.AddWithValue("@store", txtStore.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.ExecuteNonQuery();
                    }
                    MessageBox.Show("STORE DETAILS HAS BEEN SUCCESSFULLY SAVED!", "SAVE RECORD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOld1.Text) || string.IsNullOrEmpty(txtNew1.Text) || string.IsNullOrEmpty(txtRetype1.Text))
                {
                    MessageBox.Show("Please fill in all password fields before proceeding.", "Validation Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtOld1.Text != f._pass)
                {
                    MessageBox.Show("Old password did not match!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtNew1.Text != txtRetype1.Text)
                {
                    MessageBox.Show("Confirm new password did not match!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                cn.Open();
                cm = new SqlCommand("UPDATE tblUser SET password=@password WHERE username=@username", cn);
                cm.Parameters.AddWithValue("@password", txtNew1.Text);
                cm.Parameters.AddWithValue("@username", txtUser1.Text);
                cm.ExecuteNonQuery();
                cn.Close();

                MessageBox.Show("Password has been successfully changed!", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtRetype1.Clear();
                txtNew1.Clear();
                txtOld1.Clear();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void cboQ3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtAnswer3_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboQ4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtAnswer4_TextChanged(object sender, EventArgs e)
        {

        }

        private void metroTabPage3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("select * from tblUser where username =@username", cn);
                cm.Parameters.AddWithValue("@username", txtUser2.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    checkBox1.Checked = bool.Parse(dr["i    sactive"].ToString());
                }
                else
                {
                    checkBox1.Checked = false;
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUser2.Text))
                {
                    MessageBox.Show("Please enter a username to update the status.", "Validation Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool found = true;
                cn.Open();
                cm = new SqlCommand("select * from tblUser where username =@username", cn); cm.Parameters.AddWithValue("@username", txtUser2.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows) { found = true; }
                else { found = false; }
                dr.Close();
                cn.Close();

                if (found == true)
                {
                    cn.Open();
                    cm = new SqlCommand("update tblUser set isactive = @isactive where username = @username", cn);
                    cm.Parameters.AddWithValue("@isactive", checkBox1.Checked.ToString()); cm.Parameters.AddWithValue("@username", txtUser2.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Account status has been successfully updated.", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUser2.Clear();
                    checkBox1.Checked = false;
                }
                else
                {
                    MessageBox.Show("Account not exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtStore_TextChanged(object sender, EventArgs e)
        {

        }
    }

}


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
    public partial class frmStockIn : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        string stitle = "POS and Inventory System";

        public frmStockIn()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadVendor();
        }

        public void LoadStockIn()
        {
            int i = 0;
            dataGridView5.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM vwStockIn where refno like '" + txtRefNo.Text + "' and status like 'Pending'", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView5.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr["vendor"].ToString());
            }
            dr.Close();
            cn.Close();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmStockIn_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_FontChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView5.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Remove this item?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tblStockIn WHERE id = '" + dataGridView5.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Item has been successfully removed", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStockIn();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSearchProductStockIn frm = new frmSearchProductStockIn(this);
            frm.LoadStockEntry();
            frm.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void Clear()
        {
            txtBy.Clear();
            txtRefNo.Clear();
            dt1.Value = DateTime.Now;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRefNo.Text) || cboVendor.SelectedIndex == -1 || string.IsNullOrEmpty(txtBy.Text))
                {
                    MessageBox.Show("Please fill in all required fields before saving.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dataGridView5.Rows.Count > 0)
                {
                    if(MessageBox.Show("Are you sure you want to save this records?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for (int i = 0; i < dataGridView5.Rows.Count; i++)
                        {
                            //update tblProduct qty
                            cn.Open();
                            cm = new SqlCommand("UPDATE tblProduct set qty = qty + " + int.Parse(dataGridView5.Rows[i].Cells[5].Value.ToString()) + " where pcode like '" + dataGridView5.Rows[i].Cells[3].Value.ToString() + "'", cn);
                            cm.ExecuteNonQuery();
                            cn.Close();

                            //update tblStockIn qty
                            cn.Open();
                            cm = new SqlCommand("UPDATE tblStockIn set qty = qty + " + int.Parse(dataGridView5.Rows[i].Cells[5].Value.ToString()) + ", status = 'Done' where id like '" + dataGridView5.Rows[i].Cells[1].Value.ToString() + "'", cn);
                            cm.ExecuteNonQuery();
                            cn.Close();
                        }
                        Clear();
                        LoadStockIn();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadStockInHistory()
        {
            try
            {
                int i = 0;
                dataGridView6.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("SELECT * FROM vwStockIn WHERE CAST(sdate AS date) BETWEEN @startDate AND @endDate AND status LIKE 'Done'", cn);
                cm.Parameters.AddWithValue("@startDate", date1.Value.Date);
                cm.Parameters.AddWithValue("@endDate", date2.Value.Date);
                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    i++;
                    dataGridView6.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), DateTime.Parse(dr[5].ToString()).ToShortDateString(), dr[6].ToString(), dr["vendor"].ToString());
                }
                dr.Close();
                cn.Close();

                if (dataGridView6.Rows.Count == 0)
                {
                    MessageBox.Show("No records found for the selected date range.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadStockInHistory();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
          
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadStockInHistory();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboVendor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public void LoadVendor()
        {
            cboVendor.Items.Clear();
            cn.Open();
            cm = new SqlCommand("select * from tblVendor", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboVendor.Items.Add(dr["vendor"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void cboVendor_TextChanged(object sender, EventArgs e)
        {
            cn.Open();
            cm = new SqlCommand("select * from tblVendor where vendor like @vendor", cn);
            cm.Parameters.AddWithValue("@vendor", cboVendor.Text);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                lblVendorID.Text = dr["id"].ToString();
                txtPerson.Text = dr["contactperson"].ToString();
                txtAddress.Text = dr["address"].ToString();
            }
            dr.Close();
            cn.Close();

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Random rnd = new Random();
            txtRefNo.Clear();

            for (int i = 0; i < 10; i++)
            {
                txtRefNo.Text += rnd.Next(0, 10); 
            }
        }
    }
}

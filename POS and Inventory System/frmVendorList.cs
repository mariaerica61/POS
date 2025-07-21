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
    public partial class frmVendorList : Form
    {
        SqlConnection cn; 
        SqlCommand cm;
        SqlDataReader dr;
        DBConnection db = new DBConnection();

        public frmVendorList()
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.MyConnection();
            LoadRecords();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmVendor f = new frmVendor(this);
            f.btnSave.Enabled = true;
            f.btnUpdate.Enabled = false; 
            f.ShowDialog();
        }
        public void LoadRecords()
        {
            dataGridView2.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("select * from tblVendor", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView2.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dataGridView2.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                frmVendor f = new frmVendor(this);
                f.lblID.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                f.txtVendor.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                f.txtAddress.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                f.txtPerson.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
                f.txtTel.Text = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
                f.txtEmail.Text = dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
                f.txtFax.Text = dataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString();
                f.btnSave.Enabled = false;
                f.btnUpdate.Enabled = true;
                f.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("DELETE THIS RECORD? CLICK YES TO CONFIRM", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("delete from tblVendor where id like '" + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("RECORD HAS BEEN SUCCESSFULLY DELETED.", "DELETE RECORD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRecords();
                }
            }
        }

        private void frmVendorList_Load(object sender, EventArgs e)
        {

        }
    }

}

    
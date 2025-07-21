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
    public partial class frmSearchProductStockIn : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        string stitle = "POS and Inventory System";
        frmStockIn slist;

        public frmSearchProductStockIn(frmStockIn flist)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            slist = flist;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadStockEntry()
        {
            int i = 0;
            dataGridView4.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT pcode, pdesc, qty FROM tblProduct where pdesc like '%" + txtSearch.Text + "%' order by pdesc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView4.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            cn.Close();
        }

        private void frmSearchProductStockIn_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView4.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                if (slist.txtRefNo.Text == string.Empty) { MessageBox.Show("Please enter reference no", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning); slist.txtRefNo.Focus(); return; }
                if (slist.txtBy.Text == string.Empty) { MessageBox.Show("Please enter stock in by", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning); slist.txtBy.Focus(); return; }

                if (MessageBox.Show("Add this item?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblStockIn (refno, pcode, sdate, stockinby, vid) VALUES (@refno, @pcode, @sdate, @stockinby, @vid)", cn);
                    cm.Parameters.AddWithValue("@refno", slist.txtRefNo.Text);
                    cm.Parameters.AddWithValue("@pcode", dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString());
                    cm.Parameters.AddWithValue("@sdate", slist.dt1.Value);
                    cm.Parameters.AddWithValue("@stockinby", slist.txtBy.Text);
                    cm.Parameters.AddWithValue("@vid", slist.lblVendorID.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    MessageBox.Show("Successfully added!", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    slist.LoadStockIn();
                }

            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadStockEntry();
        }
    }
}

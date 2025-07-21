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
using System.Runtime.Versioning;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace POS_and_Inventory_System
{
    public partial class frmSoldItems : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        public string suser;
        

        public frmSoldItems()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            dt1.Value = DateTime.Now;
            dt2.Value = DateTime.Now;
            LoadRecord();
            LoadCashier();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadRecord()
        {
            int i = 0;
            double _total = 0;
            cn.Open();
            dataGridView6.Rows.Clear();

            if (cboCashier.Text == "All Cashier")
            {
                cm = new SqlCommand("select c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc, c.total from tblCart as c inner join tblProduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" + dt1.Value.ToString("yyyy-MM-dd") + "' and '" + dt2.Value.ToString("yyyy-MM-dd") + "'", cn);

            }
            else
            {
                cm = new SqlCommand("select c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc, c.total from tblCart as c inner join tblProduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" + dt1.Value.ToString("yyyy-MM-dd") + "' and '" + dt2.Value.ToString("yyyy-MM-dd") + "' and cashier like '" + cboCashier.Text + "'", cn);
            }
        
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                _total += double.Parse(dr["total"].ToString());
                dataGridView6.Rows.Add(i, dr["id"].ToString(), dr["transno"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), dr["total"].ToString());
            }
            dr.Close();
            cn.Close();

            lblTotal.Text = _total.ToString("#,##0.00");

        }
        private void dt1_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dt1_ValueChanged_1(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dt2_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void frmSoldItems_Load(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmReportSold frm = new frmReportSold(this);
            frm.LoadReport();
            frm.ShowDialog();
        }

        private void cboCashier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        public void LoadCashier()
        {
            cboCashier.Items.Clear();
            cboCashier.Items.Add("All Cashier");
            cn.Open();
            cm = new SqlCommand("Select * from tblUser where role like 'Cashier'", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboCashier.Items.Add(dr["username"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void cboCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView6.Columns[e.ColumnIndex].Name;
            if (colName == "colCancel")
            {

                frmCancelDetails f = new frmCancelDetails(this);
                f.txtID.Text = dataGridView6.Rows[e.RowIndex].Cells[1].Value.ToString();
                f.txtTransNo.Text = dataGridView6.Rows[e.RowIndex].Cells[2].Value.ToString();
                f.txtPcode.Text = dataGridView6.Rows[e.RowIndex].Cells[3].Value.ToString();
                f.txtDescription.Text = dataGridView6.Rows[e.RowIndex].Cells[4].Value.ToString();
                f.txtPrice.Text = dataGridView6.Rows[e.RowIndex].Cells[5].Value.ToString();
                f.txtQty.Text = dataGridView6.Rows[e.RowIndex].Cells[6].Value.ToString();
                f.txtDiscount.Text = dataGridView6.Rows[e.RowIndex].Cells[7].Value.ToString();
                f.txtTotal.Text = dataGridView6.Rows[e.RowIndex].Cells[8].Value.ToString();

                f.txtCancel.Text = suser;
                f.ShowDialog();
            }
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }
    }

}
    

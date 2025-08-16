using Microsoft.Reporting.WinForms;
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
    public partial class frmRecords : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        string stitle = "POS and Inventory System";

        public frmRecords()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmRecords_Load(object sender, EventArgs e)
        {

        }
        public void LoadRecord()
        {
            int i = 0;
            cn.Open();
            dataGridView6.Rows.Clear();
            if(cboTopSelect.Text == "SORT BY QUANTITY")
            {
                cm = new SqlCommand("select top 10 pcode, pdesc, isnull(sum(qty),0) as  qty, isnull(sum(total),0) as total  from vwSoldItems where sdate between '" + dtp1.Value.ToString("yyyy-MM-dd") + "' and '" + dtp2.Value.ToString("yyyy-MM-dd") + "' and status like 'Sold' group by pcode, pdesc order by qty desc", cn);
            }
            else if (cboTopSelect.Text == "SORT BY TOTAL AMOUNT")
            {
                cm = new SqlCommand("select top 10 pcode, pdesc, isnull(sum(qty),0) as  qty, isnull(sum(total),0) as total  from vwSoldItems where sdate between '" + dtp1.Value.ToString("yyyy-MM-dd") + "' and '" + dtp2.Value.ToString("yyyy-MM-dd") + "' and status like 'Sold' group by pcode, pdesc order by total desc", cn);
            }

            cm = new SqlCommand("select top 10 pcode, pdesc, isnull(sum(qty),0) as  qty, isnull(sum(total),0) as total  from vwSoldItems where sdate between '" + dtp1.Value.ToString("yyyy-MM-dd") + "' and '" + dtp2.Value.ToString("yyyy-MM-dd") + "' and status like 'Sold' group by pcode, pdesc order by qty desc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView6.Rows.Add(i, dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["qty"].ToString(), double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
            }
            dr.Close();
            cn.Close();
        }

        public void LoadCancelledOrders()
        {
            int i = 0;
            dataGridView4.Rows.Clear();

            cn.Open();
            cm = new SqlCommand("select * from vwCancelledOrder where sdate between '" + dtp5.Value.ToString("yyyy-MM-dd")  + "' and '" +  dtp6.Value.ToString("yyyy-MM-dd") + "'", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView4.Rows.Add(i, dr["transno"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["total"].ToString(), dr["sdate"].ToString(), dr["voidby"].ToString(), dr["cancelledby"].ToString(), dr["reason"].ToString(), dr["action"].ToString());
            }
            dr.Close();
            cn.Close();
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            if(cboTopSelect.Text == String.Empty)
            {
                MessageBox.Show("Please select from the dropdown list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            LoadRecord();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView3.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("SELECT c.pcode, p.pdesc, c.price, SUM(c.qty) AS tot_qty, SUM(c.disc) AS tot_disc " +
                                    "FROM tblCart AS c INNER JOIN tblProduct AS p ON c.pcode = p.pcode " +
                                    "WHERE status LIKE 'Sold' AND sdate BETWEEN @startDate AND @endDate " +
                                    "GROUP BY c.pcode, p.pdesc, c.price", cn);
                cm.Parameters.AddWithValue("@startDate", dtp3.Value.ToString("yyyy-MM-dd"));
                cm.Parameters.AddWithValue("@endDate", dtp4.Value.ToString("yyyy-MM-dd"));
                dr = cm.ExecuteReader();
                double total = 0;

                while (dr.Read())
                {
                    i++;
                    double price = double.Parse(dr["price"].ToString());
                    int qty = int.Parse(dr["tot_qty"].ToString());
                    double lineTotal = price * qty;

                    total += lineTotal;
                    dataGridView3.Rows.Add(i, dr["pcode"].ToString(), dr["pdesc"].ToString(),
                                           price.ToString("#,##0.00"), qty.ToString(),
                                           dr["tot_disc"].ToString(), lineTotal.ToString("#,##0.00"));
                }
                dr.Close();
                cn.Close();
               
                lblTotal.Text = total.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        public void LoadInventory()
        {

            int i = 0;
            dataGridView2.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty, p.reorder from tblProduct as p inner join tblBrand as b on p.bid = b.id inner join tblCategory as c on p.cid = c.id", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView2.Rows.Add(i, dr["pcode"].ToString(), dr["barcode"].ToString(), dr["pdesc"].ToString(), dr["brand"].ToString(), dr["category"].ToString(), dr["price"].ToString(), dr["reorder"].ToString(), dr["qty"].ToString());

            }

            cn.Close();
            dr.Close();
        }

        public void LoadCriticalItems()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("select * from vwCriticalItems", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[7].ToString(), dr[5].ToString(), dr[4].ToString(), dr[6].ToString());
                }
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInventoryReport frm = new frmInventoryReport();
            frm.LoadReport();
            frm.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadCancelledOrders();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadStockInHistory();
        }

        public void LoadStockInHistory()
        {
            try
            {
                int i = 0;
                dataGridView5.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("SELECT * FROM vwStockIn WHERE CAST(sdate AS date) BETWEEN '" + dtp7.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp8.Value.ToString("yyyy-MM-dd") + "' AND status LIKE 'Done'", cn);
                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    i++;
                    dataGridView5.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), DateTime.Parse(dr[5].ToString()).ToShortDateString(), dr[6].ToString());
                }
                dr.Close();
                cn.Close();

                if (dataGridView5.Rows.Count == 0)
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

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInventoryReport f = new frmInventoryReport();
            if (cboTopSelect.Text == "SORT BY QTY")
            {
                f.LoadTopSelling("select top 10 pcode, pdesc, isnull(sum(qty),0) as  qty, isnull(sum(total),0) as total  from vwSoldItems where sdate between '" + dtp1.Value.ToString("yyyy-MM-dd") + "' and '" + dtp2.Value.ToString("yyyy-MM-dd") + "' and status like 'Sold' group by pcode, pdesc order by qty desc", "From : " + dtp1.Value.ToString() + " To : " + dtp2.Value.ToString(), "TOP SELLING ITEMS SORT BY QUANTITY");
               
            }
            else if (cboTopSelect.Text == "SORT BY TOTAL AMOUNT")
            {
                f.LoadTopSelling("select top 10 pcode, pdesc, isnull(sum(qty),0) as  qty, isnull(sum(total),0) as total  from vwSoldItems where sdate between '" + dtp1.Value.ToString("yyyy-MM-dd") + "' and '" + dtp2.Value.ToString("yyyy-MM-dd") + "' and status like 'Sold' group by pcode, pdesc order by total desc", "From : " + dtp1.Value.ToString() + " To : " + dtp2.Value.ToString(), "TOP SELLING ITEMS SORT BY TOTAL AMOUNT");
                
            }
            
           f.ShowDialog();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInventoryReport f = new frmInventoryReport();
            f.LoadSoldItems("select  c.pcode, p.pdesc, c.price, sum(c.qty) as tot_qty, sum(c.disc) as tot_disc, sum(c.total) as total from tblCart as c inner join tblProduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" + dtp3.Value.ToString("yyyy-MM-dd") + "' and '" + dtp4.Value.ToString("yyyy-MM-dd") + "' group by c.pcode, p.pdesc, c.price", "From : " + dtp3.Value.ToString() + " To : " + dtp4.Value.ToString());
            f.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboTopSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string param = "Date Covered:" + dtp7.Value.ToShortDateString() + " - " + dtp8.Value.ToShortDateString();
            frmInventoryReport frm = new frmInventoryReport();
            frm.LoadStockInReport("SELECT * FROM vwStockIn WHERE CAST(sdate AS date) BETWEEN '" + dtp7.Value.ToString("yyyy-MM-dd") + "' AND '" + dtp8.Value.ToString("yyyy-MM-dd") + "' AND status LIKE 'Done'", param);
            frm.ShowDialog();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel5_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string param = "Date Covered:" + dtp5.Value.ToString("yyyy-MM-dd") + "' - '" + dtp6.Value.ToString("yyyy-MM-dd");
            frmInventoryReport frm = new frmInventoryReport();
            frm.LoadCancelledReport("select * from vwCancelledOrder where sdate between '" + dtp5.Value.ToString("yyyy-MM-dd") + "' and '" + dtp6.Value.ToString("yyyy-MM-dd") + "'", param);
            frm.ShowDialog();
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

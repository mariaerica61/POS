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
    public partial class frmQty : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;

        private String pcode;
        private double price;
        private String transno;
        private int qty;
        string stitle = "POS and Inventory System";
        frmPOS fpos;


        public frmQty(frmPOS frmpos)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            fpos = frmpos;
        }

        private void frmQty_Load(object sender, EventArgs e)
        {

        }

        public void ProductDetails(String pcode, double price, String transno, int qty)
        {
            this.pcode = pcode;
            this.price = price;
            this.transno = transno;
            this.qty = qty;
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ONLY ADDITION: Block letters and special characters, allow only numbers, backspace, and enter
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter)
            {
                e.Handled = true; // This prevents the character from being entered
                return;
            }

            // YOUR ORIGINAL CODE UNCHANGED:
            if ((e.KeyChar == 13) && (txtQty.Text != String.Empty))
            {
                String id = "";
                int cart_qty = 0;
                bool found = false;

                cn.Open();
                cm = new SqlCommand("Select * from tblcart where transno = @transno and pcode = @pcode", cn);
                cm.Parameters.AddWithValue("@transno", fpos.lblTransno.Text);
                cm.Parameters.AddWithValue("@pcode", pcode);
                dr = cm.ExecuteReader();
                dr.Read();

                if (dr.HasRows)
                {
                    found = true;
                    id = dr["id"].ToString();
                    cart_qty = int.Parse(dr["qty"].ToString());

                }
                else { found = false; }
                dr.Close();
                cn.Close();

                if (found == true)
                {
                    if (qty < (int.Parse(txtQty.Text) + cart_qty))
                    {
                        MessageBox.Show("Unable to proceed. Remaining quantity on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    cn.Open();
                    cm = new SqlCommand("update tblCart set qty = (qty + " + int.Parse(txtQty.Text) + ") where id = '" + id + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    fpos.txtSearch.Clear();
                    fpos.txtSearch.Focus();
                    fpos.LoadCart();
                    this.Dispose();

                }
                else
                {
                    if (qty < int.Parse(txtQty.Text))
                    {
                        MessageBox.Show("Unable to proceed. Remaining quantity on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblCart (transno, pcode, price, qty, sdate, cashier) VALUES (@transno, @pcode, @price, @qty, @sdate, @cashier)", cn);
                    cm.Parameters.AddWithValue("@transno", transno);
                    cm.Parameters.AddWithValue("@pcode", pcode);
                    cm.Parameters.AddWithValue("@price", price);
                    cm.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text));
                    cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cm.Parameters.AddWithValue("@cashier", fpos.lblName.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    fpos.txtSearch.Clear();
                    fpos.txtSearch.Focus();
                    fpos.LoadCart();
                    this.Dispose();

                }
            }
        }
    }
}

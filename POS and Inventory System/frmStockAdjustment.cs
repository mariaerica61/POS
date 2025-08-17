using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_and_Inventory_System
{
    public partial class frmStockAdjustment : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        DBConnection db = new DBConnection();
        Form1 f;
        string stitle = "POS and Inventory System";
        int _qty = 0;


        public frmStockAdjustment(Form1 f)
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.MyConnection();
            this.f = f;

        }

        private void frmStockAdjustment_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadProduct()
        {
            int i = 0;
            dataGridView3.Rows.Clear();

            try
            {
                cn.Open();

                // Enhanced search query that searches across barcode, description, brand, and category
                string searchQuery = @"
            SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty 
            FROM tblProduct as p 
            INNER JOIN tblBrand as b ON b.id = p.bid 
            INNER JOIN tblCategory as c ON c.id = p.cid 
            WHERE p.barcode LIKE @searchTerm 
               OR p.pdesc LIKE @searchTerm 
               OR b.brand LIKE @searchTerm 
               OR c.category LIKE @searchTerm";

                cm = new SqlCommand(searchQuery, cn);
                cm.Parameters.AddWithValue("@searchTerm", "%" + txtSearch.Text + "%");

                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView3.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(),
                                           dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message, stitle,
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                LoadProduct();
            }
        }

        private void txtRef_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDesc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void ReferenceNo()
        {
            Random rnd = new Random();
            txtRef.Text = rnd.Next().ToString();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dataGridView3.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
               
                txtPcode.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDesc.Text = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString() + " " + dataGridView3.Rows[e.RowIndex].Cells[4].Value.ToString() + " " + dataGridView3.Rows[e.RowIndex].Cells[5].Value.ToString();
                _qty = int.Parse(dataGridView3.Rows[e.RowIndex].Cells[7].Value.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPcode.Text) || string.IsNullOrEmpty(txtDesc.Text) || string.IsNullOrEmpty(txtQty.Text) || string.IsNullOrEmpty(cboCommand.Text) || string.IsNullOrEmpty(txtRemarks.Text))
                {
                    MessageBox.Show("Please fill in all fields before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (int.Parse(txtQty.Text) > _qty)
                {
                    MessageBox.Show("STOCK ON HAND QUANTITY SHOULD BE GREATER THAN FROM ADJUSTMENT QUANTITY.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboCommand.Text == "REMOVE FROM INVENTORY")
                {
                    SqlStatement("update tblProduct set qty = (qty - " + int.Parse(txtQty.Text) + " ) where pcode like '" + txtPcode.Text + "'");
                }
                else if (cboCommand.Text == "ADD TO INVENTORY")
                {
                    SqlStatement("update tblProduct set qty = (qty + " + int.Parse(txtQty.Text) + " ) where pcode like '" + txtPcode.Text + "'");
                }


                SqlStatement("insert into tblAdjustment(referenceno, pcode, qty, action, remarks, sdate, [user]) values ('" + txtRef.Text + "','" + txtPcode.Text + "','" + int.Parse(txtQty.Text) + "','" + cboCommand.Text + "','" + txtRemarks.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + txtUser.Text + "')");


                MessageBox.Show("STOCK HAS BEEN SUCCESSFULLY ADJUSTED.", "PROCESS COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProduct();
                Clear();
            }
            catch (Exception ex)
            {

                cn.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void Clear()
        {
            txtDesc.Clear();
            txtPcode.Clear();
            txtQty.Clear();
            txtRef.Clear();
            txtRemarks.Clear();
            cboCommand.Text = "";
            ReferenceNo();

        }

        public void SqlStatement(String _sql)
        {
            cn.Open();
            cm = new SqlCommand(_sql, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

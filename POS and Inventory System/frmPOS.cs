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
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Tulpep.NotificationWindow;
using System.IO;
using Microsoft.Reporting.Map.WebForms.BingMaps;

namespace POS_and_Inventory_System
{
    public partial class frmPOS : Form
    {
        // Class-level variables for managing state and database connection
        string id;
        String price;
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        string stitle = "POS and Inventory System";
        int qty;
        frmSecurity f;

        private Button btnCategory;
        private string _filter = "";
        private PictureBox pic;
        private Label lblDesc;
        private Label lblPrice;



        public frmPOS(frmSecurity frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            lblDate.Text = DateTime.Now.ToLongDateString();
            this.KeyPreview = true;
            f = frm;
            lblName.Text = f._displayName;
            NotifyCriticalItems();
        }

        // Displays notifications for items with critical stock levels
        public void NotifyCriticalItems()
        {
            string critical = "";
            cn.Open();
            cm = new SqlCommand("select count(*) from vwCriticalItems", cn);
            string count = cm.ExecuteScalar().ToString();
            cn.Close();

            int i = 0;
            cn.Open();
            cm = new SqlCommand("select * from vwCriticalItems", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                critical += i + ". " + dr["pdesc"].ToString() + Environment.NewLine;
            }
            dr.Close();
            cn.Close();


            PopupNotifier popup = new PopupNotifier();
            popup.Image = Properties.Resources.error;
            popup.TitleText = count + " CRITICAL ITEM(S)";
            popup.ContentText = critical;
            popup.Popup();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            LoadCategory();
            AddAllCategoryButton();
        }

        // Generates a unique transaction number based on the current date
        public void GetTransNo()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                string transno;
                int count;

                cn.Open();
                cm = new SqlCommand("SELECT top 1 transno from tblCart WHERE transno like '" + sdate + "%' order by id desc", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    transno = dr[0].ToString();
                    count = int.Parse(transno.Substring(8, 4));
                    lblTransno.Text = sdate + (count + 1);
                }
                else
                {
                    transno = sdate + "1001";
                    lblTransno.Text = transno;
                }
                dr.Close();
                cn.Close();

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Event handler for starting a new transaction
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("Please complete or clear the current transaction first.",
                               "Pending Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Generate new transaction number
            GetTransNo();

            // Enable controls for transaction
            txtSearch.Enabled = true;
            txtSearch.Focus();
            txtSearch.Clear();

            // Load all products when starting new transaction
            _filter = "";
            LoadMenu();

            // Highlight the "ALL" button
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is Button btn)
                {
                    if (btn.Text == "ALL")
                    {
                        btn.BackColor = Color.FromArgb(47, 68, 66); // highlight ALL
                    }
                    else if (btn.Tag is Color originalColor)
                    {
                        btn.BackColor = originalColor; // restore each category’s original color
                    }
                }
            }


            // Show confirmation message
            MessageBox.Show("New transaction started!\nTransaction No: " + lblTransno.Text,
                           "New Transaction", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
    }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }

        // Searches for products by barcode
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text == string.Empty) { return; }

                // Check if a new transaction has been started
                if (string.IsNullOrEmpty(lblTransno.Text) || lblTransno.Text == "00000000000000")
                {
                    MessageBox.Show("Please start a new transaction first by clicking the NEW TRANSACTION button (F1)",
                                   "No Active Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSearch.Clear();
                    return;
                }

                String _pcode;
                double _price;
                int availableQty;

                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblProduct WHERE barcode like '" + txtSearch.Text + "'", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    availableQty = int.Parse(dr["qty"].ToString());
                    _pcode = dr["pcode"].ToString();
                    _price = double.Parse(dr["price"].ToString());

                    dr.Close();
                    cn.Close();

                    // Open frmQty for barcode scanning as well for consistency
                    frmQty frm = new frmQty(this);
                    frm.ProductDetails(_pcode, _price, lblTransno.Text, availableQty);
                    frm.ShowDialog();
                }
                else
                {
                    dr.Close();
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Adds a product to the cart
        private void AddToCart(String _pcode, double _price, int _qty)
        {
            String id = "";
            bool found = false;
            int cart_qty = 0;

            cn.Open();
            cm = new SqlCommand("Select * from tblCart where transno = @transno and pcode = @pcode", cn);
            cm.Parameters.AddWithValue("@transno", lblTransno.Text);
            cm.Parameters.AddWithValue("@pcode", _pcode);
            dr = cm.ExecuteReader();
            dr.Read();

            if (dr.HasRows)
            {
                found = true;
                id = dr["id"].ToString();
                cart_qty = int.Parse(dr["qty"].ToString());
            }
            dr.Close();
            cn.Close();

            if (found == true)
            {
                // Update existing cart item
                cn.Open();
                cm = new SqlCommand("update tblCart set qty = (qty + " + _qty + ") where id = '" + id + "'", cn);
                cm.ExecuteNonQuery();
                cn.Close();

                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
                LoadCart();
            }
            else
            {
                // Insert new item into the cart
                cn.Open();
                cm = new SqlCommand("INSERT INTO tblCart (transno, pcode, price, qty, sdate, cashier) VALUES (@transno, @pcode, @price, @qty, @sdate, @cashier)", cn);
                cm.Parameters.AddWithValue("@transno", lblTransno.Text);
                cm.Parameters.AddWithValue("@pcode", _pcode);
                cm.Parameters.AddWithValue("@price", _price);
                cm.Parameters.AddWithValue("@qty", _qty);
                cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                cm.Parameters.AddWithValue("@cashier", lblName.Text);
                cm.ExecuteNonQuery();
                cn.Close();

                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
                LoadCart();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;

            if (colName == "Delete")
            {
                if (MessageBox.Show("Remove this item?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tblCart WHERE id = @id", cn);
                    cm.Parameters.AddWithValue("@id", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    cm.ExecuteNonQuery();
                    cn.Close();

                    MessageBox.Show("Item has been successfully removed", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCart();
                }
            }
            else if (colName == "colAdd")
            {
                int availableStock = 0;
                string pcode = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                string id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                // Check stock
                cn.Open();
                cm = new SqlCommand("SELECT qty FROM tblProduct WHERE pcode = @pcode", cn);
                cm.Parameters.AddWithValue("@pcode", pcode);
                availableStock = Convert.ToInt32(cm.ExecuteScalar());
                cn.Close();

                int currentCartQty = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());

                if (currentCartQty < availableStock)
                {
                    cn.Open();
                    cm = new SqlCommand(@"
                UPDATE tblCart
                SET qty = qty + 1,
                    disc = (price * (qty + 1) * disc_percent / 100),
                    total = (price * (qty + 1)) - (price * (qty + 1) * disc_percent / 100)
                WHERE id = @id", cn);
                    cm.Parameters.AddWithValue("@id", id);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    LoadCart();
                }
                else
                {
                    MessageBox.Show("Remaining quantity on hand is " + availableStock + " !", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (colName == "colRemove")
            {
                string id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                int currentCartQty = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());

                if (currentCartQty > 1)
                {
                    cn.Open();
                    cm = new SqlCommand(@"
                UPDATE tblCart
                SET qty = qty - 1,
                    disc = (price * (qty - 1) * disc_percent / 100),
                    total = (price * (qty - 1)) - (price * (qty - 1) * disc_percent / 100)
                WHERE id = @id", cn);
                    cm.Parameters.AddWithValue("@id", id);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    LoadCart();
                }
                else
                {
                    MessageBox.Show("Remaining quantity in cart is " + currentCartQty + " !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public void LoadCart()
        {
            try
            {
                Boolean hasrecord = false;
                dataGridView1.Rows.Clear();
                int i = 0;
                double total = 0;
                double discount = 0;
                cn.Open();
                cm = new SqlCommand("select c.id, c.pcode, p.pdesc, c.price,c.qty,c.disc,c.total from tblCart as c inner join tblProduct as p on c.pcode = p.pcode WHERE transno like '" + lblTransno.Text + "'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    total += Double.Parse(dr["total"].ToString());
                    discount += Double.Parse(dr["disc"].ToString());
                    dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), Double.Parse(dr["total"].ToString()).ToString("#,##0.00"), "ADD MORE", "REMOVE");
                    hasrecord = true;
                }
                dr.Close();
                cn.Close();
                lblTotal.Text = total.ToString("#,##0.00");
                lblDiscount.Text = discount.ToString("#,##0.00");
                GetCartTotal();
                if (hasrecord == true)
                {
                    btnSettle.Enabled = true; btnDiscount.Enabled = true; btnClear.Enabled = true;
                }
                else { btnSettle.Enabled = false; btnDiscount.Enabled = false; btnClear.Enabled = false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cn.Close();
            }
        }

        public void GetCartTotal()
        {
            double sales = Double.Parse(lblTotal.Text);
            double discount = Double.Parse(lblDiscount.Text);

            // Calculate the net sales after discount
            double netSales = sales - discount;

            // Calculate VAT based on net sales (after discount)
            double vat = netSales * dbcon.GetVal();
            double vatable = netSales - vat;

            lblVat.Text = vat.ToString("#,##0.00");
            lblVatable.Text = vatable.ToString("#,##0.00");

            // Display the final total (net sales after discount)
            lblDisplayTotal.Text = netSales.ToString("#,##0.00");
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblTransno.Text) || lblTransno.Text == "00000000000000")
            {
                MessageBox.Show("Please start a new transaction first by clicking the NEW TRANSACTION button (F1)",
                               "No Active Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmLookUp frm = new frmLookUp(this);
            frm.LoadProduct();
            frm.ShowDialog();



        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblTransno.Text) || lblTransno.Text == "00000000000000")
            {
                MessageBox.Show("Please start a new transaction first by clicking the NEW TRANSACTION button (F1)",
                               "No Active Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if there are items in the cart
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No items in cart to apply discount", "Empty Cart",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if a row is selected
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select an item to apply discount", "No Item Selected",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get current row details
            int currentRowIndex = dataGridView1.CurrentRow.Index;
            string selectedId = dataGridView1[1, currentRowIndex].Value.ToString();
            double unitPrice = Double.Parse(dataGridView1[4, currentRowIndex].Value.ToString());
            int quantity = int.Parse(dataGridView1[5, currentRowIndex].Value.ToString());
            double totalRowPrice = unitPrice * quantity;

            frmDiscount frm = new frmDiscount(this);
            frm.lblID.Text = selectedId;
            frm.txtPrice.Text = totalRowPrice.ToString(); // Pass total row price
            frm.ShowDialog();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int i = dataGridView1.CurrentRow.Index;
                id = dataGridView1[1, i].Value.ToString();

                // Calculate the total for this row (price × quantity)
                double unitPrice = Double.Parse(dataGridView1[4, i].Value.ToString());
                int quantity = int.Parse(dataGridView1[5, i].Value.ToString());
                double totalPrice = unitPrice * quantity;

                price = totalPrice.ToString(); // Pass the total price, not unit price
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh : mm : ss tt");
            lblDate1.Text = DateTime.Now.ToLongDateString();
        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblTransno.Text) || lblTransno.Text == "00000000000000")
            {
                MessageBox.Show("Please start a new transaction first by clicking the NEW TRANSACTION button (F1)",
                               "No Active Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if there are items in the cart
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No items in cart to settle", "Empty Cart",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmSettlePayment frm = new frmSettlePayment(this);
            frm.txtSale.Text = lblDisplayTotal.Text;
            frm.ShowDialog();
        }

        private void lblDate1_Click(object sender, EventArgs e)
        {

        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }

        private void btnDaily_Click(object sender, EventArgs e)
        {
            frmSoldItems frm = new frmSoldItems();
            frm.dt1.Enabled = false;
            frm.dt2.Enabled = false;
            frm.suser = lblName.Text;
            frm.cboCashier.Enabled = false;
            frm.cboCashier.Text = lblName.Text;
            frm.ShowDialog();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("Unable to logout. Please cancel the transaction.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Logout Application?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                frmSecurity frm = new frmSecurity();
                frm.ShowDialog();
            }

        }

        private void frmPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnNew_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSearch_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnDiscount_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnSettle_Click(sender, e);
            }

            else if (e.KeyCode == Keys.F6)
            {
                btnDaily_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F7)
            {
                btnChangePass_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F8)
            {
                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
            }


        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(this);
            frm.ShowDialog();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblTransno.Text) || lblTransno.Text == "00000000000000")
            {
                MessageBox.Show("Please start a new transaction first by clicking the NEW TRANSACTION button (F1)",
                               "No Active Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if there are items in the cart
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No items in cart to clear", "Empty Cart",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Remove all items from cart?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cn.Open();
                cm = new SqlCommand("delete from tblCart where transno like '" + lblTransno.Text + "'", cn);
                cm.ExecuteNonQuery();
                cn.Close();

                MessageBox.Show("All items have been successfully removed!", "Remove Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCart();
            }
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadCategory()
        {
            try
            {
                // Clear existing controls in flowLayoutPanel1
                flowLayoutPanel1.Controls.Clear();

                // FlowLayoutPanel settings for centering
                flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
                flowLayoutPanel1.WrapContents = true;
                flowLayoutPanel1.AutoScroll = true;
                flowLayoutPanel1.Padding = new Padding(10);
                flowLayoutPanel1.Anchor = AnchorStyles.None;

                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblCategory ORDER BY category", cn);
                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    btnCategory = new Button();
                    btnCategory.Width = 168;
                    btnCategory.Height = 49;
                    btnCategory.Text = dr["category"].ToString();
                    btnCategory.FlatStyle = FlatStyle.Flat;
                    btnCategory.BackColor = Color.FromArgb(114, 132, 130);
                    btnCategory.ForeColor = Color.White;
                    btnCategory.Cursor = Cursors.Hand;
                    btnCategory.Font = new Font("Palatino Linotype", 10F, FontStyle.Bold);
                    btnCategory.Tag = btnCategory.BackColor;

                    // Centering
                    btnCategory.Anchor = AnchorStyles.None;
                    btnCategory.Margin = new Padding(10);

                    flowLayoutPanel1.Controls.Add(btnCategory);

                    btnCategory.Click += Filter_Click;
                }

                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Filter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblTransno.Text) || lblTransno.Text == "00000000000000")
            {
                MessageBox.Show("Please start a new transaction first by clicking the NEW TRANSACTION button (F1)",
                               "No Active Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Button clickedButton = sender as Button;
            _filter = clickedButton.Text;

            // Reset all category buttons back to their original colors (saved in Tag)
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is Button btn)
                {
                    if (btn.Tag is Color originalColor)
                    {
                        btn.BackColor = originalColor;
                    }
                }
            }

            // Highlight the selected category button
            clickedButton.BackColor = Color.FromArgb(30, 136, 229);

            // Load products for selected category
            LoadMenu();
        }

        private void AddAllCategoryButton()
        {
            Button btnAll = new Button();
            btnAll.Width = 168;
            btnAll.Height = 49;
            btnAll.Text = "ALL";
            btnAll.FlatStyle = FlatStyle.Flat;
            btnAll.BackColor = Color.FromArgb(47, 68, 66);
            btnAll.ForeColor = Color.White;
            btnAll.Cursor = Cursors.Hand;
            btnAll.Font = new Font("Palatino Linotype", 10F, FontStyle.Bold);
            btnAll.Tag = btnAll.BackColor;

            // Centering
            btnAll.Anchor = AnchorStyles.None;
            btnAll.Margin = new Padding(10);

            flowLayoutPanel1.Controls.Add(btnAll);
            flowLayoutPanel1.Controls.SetChildIndex(btnAll, 0);

            btnAll.Click += FilterAll_Click;
        }

        // Event handler for "ALL" category button
        private void FilterAll_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblTransno.Text) || lblTransno.Text == "00000000000000")
            {
                MessageBox.Show("Please start a new transaction first by clicking the NEW TRANSACTION button (F1)",
                               "No Active Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Button clickedButton = sender as Button;
            _filter = ""; // Empty filter shows all products

            // Reset all category buttons to default color
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is Button btn)
                {
                    btn.BackColor = Color.FromArgb(75, 207, 250);
                }
            }

            // Highlight the "ALL" button
            clickedButton.BackColor = Color.FromArgb(47, 68, 66);

            // Load all products
            LoadMenu();
        }

        private void LoadMenu()
        {
            try
            {
                flowLayoutPanel2.Controls.Clear();

                // FlowLayoutPanel settings for centering
                flowLayoutPanel2.FlowDirection = FlowDirection.LeftToRight;
                flowLayoutPanel2.WrapContents = true;
                flowLayoutPanel2.AutoScroll = true;
                flowLayoutPanel2.Padding = new Padding(15);
                flowLayoutPanel2.Anchor = AnchorStyles.None;

                string query = "SELECT pcode, pdesc, price, cid, image FROM tblProduct WHERE qty > 0";

                if (!string.IsNullOrEmpty(_filter))
                {
                    query += " AND cid IN (SELECT id FROM tblCategory WHERE category LIKE @category)";
                }

                query += " ORDER BY pdesc";

                cn.Open();
                cm = new SqlCommand(query, cn);

                if (!string.IsNullOrEmpty(_filter))
                {
                    cm.Parameters.AddWithValue("@category", "%" + _filter + "%");
                }

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    // Product container panel
                    Panel productPanel = new Panel();
                    productPanel.Width = 160;
                    productPanel.Height = 200;
                    productPanel.BorderStyle = BorderStyle.FixedSingle;
                    productPanel.BackColor = Color.White;
                    productPanel.Cursor = Cursors.Hand;
                    productPanel.Tag = dr["pcode"].ToString();
                    productPanel.Margin = new Padding(15);
                    productPanel.Anchor = AnchorStyles.None;

                    // Picture
                    pic = new PictureBox();
                    pic.Width = 148;
                    pic.Height = 120;
                    pic.BackColor = Color.FromArgb(240, 240, 240);
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    pic.Tag = dr["pcode"].ToString();
                    pic.Cursor = Cursors.Hand;
                    pic.Anchor = AnchorStyles.None;
                    pic.Location = new System.Drawing.Point(
                    (productPanel.Width - pic.Width) / 2,
                        5
                    );

                    try
                    {
                        if (dr["image"] != DBNull.Value)
                        {
                            byte[] imageData = (byte[])dr["image"];
                            if (imageData != null && imageData.Length > 0)
                            {
                                using (MemoryStream ms = new MemoryStream(imageData))
                                {
                                    pic.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                pic.BackColor = Color.LightGray;
                            }
                        }
                        else
                        {
                            pic.BackColor = Color.LightGray;
                        }
                    }
                    catch
                    {
                        pic.BackColor = Color.LightGray;
                    }

                    // Description label
                    lblDesc = new Label();
                    lblDesc.Text = dr["pdesc"].ToString();
                    lblDesc.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                    lblDesc.TextAlign = ContentAlignment.MiddleCenter;
                    lblDesc.Dock = DockStyle.Bottom;
                    lblDesc.Height = 30;
                    lblDesc.BackColor = Color.FromArgb(47, 68, 66);
                    lblDesc.ForeColor = Color.White;
                    lblDesc.Tag = dr["pcode"].ToString();
                    lblDesc.Cursor = Cursors.Hand;

                    // Price label
                    lblPrice = new Label();
                    lblPrice.Text = "₱" + double.Parse(dr["price"].ToString()).ToString("#,##0.00");
                    lblPrice.Font = new Font("Palatino Linotype", 9, FontStyle.Bold);
                    lblPrice.TextAlign = ContentAlignment.MiddleCenter;
                    lblPrice.Dock = DockStyle.Bottom;
                    lblPrice.Height = 30;
                    lblPrice.BackColor = Color.FromArgb(202, 155, 53);
                    lblPrice.ForeColor = Color.White;
                    lblPrice.Tag = dr["pcode"].ToString();
                    lblPrice.Cursor = Cursors.Hand;

                    // Add controls
                    productPanel.Controls.Add(lblPrice);
                    productPanel.Controls.Add(lblDesc);
                    productPanel.Controls.Add(pic);

                    flowLayoutPanel2.Controls.Add(productPanel);

                    // Events
                    productPanel.Click += ProductSelect_Click;
                    pic.Click += ProductSelect_Click;
                    lblDesc.Click += ProductSelect_Click;
                    lblPrice.Click += ProductSelect_Click;
                }

                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProductSelect_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a new transaction has been started
                if (string.IsNullOrEmpty(lblTransno.Text) || lblTransno.Text == "00000000000000")
                {
                    MessageBox.Show("Please start a new transaction first by clicking the NEW TRANSACTION button (F1)",
                                   "No Active Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Control clickedControl = sender as Control;
                string pcode = clickedControl.Tag.ToString();

                // Get product details from database
                string pdesc = "";
                double price = 0;
                int availableQty = 0;

                cn.Open();
                cm = new SqlCommand("SELECT pdesc, price, qty FROM tblProduct WHERE pcode = @pcode", cn);
                cm.Parameters.AddWithValue("@pcode", pcode);
                dr = cm.ExecuteReader();

                if (dr.Read())
                {
                    pdesc = dr["pdesc"].ToString();
                    price = double.Parse(dr["price"].ToString());
                    availableQty = int.Parse(dr["qty"].ToString());
                }

                dr.Close();
                cn.Close();

                // Check if item is in stock
                if (availableQty <= 0)
                {
                    MessageBox.Show("Item is out of stock!", "Out of Stock",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Open frmQty dialog similar to frmLookUp flow
                frmQty frm = new frmQty(this);
                frm.ProductDetails(pcode, price, lblTransno.Text, availableQty);
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

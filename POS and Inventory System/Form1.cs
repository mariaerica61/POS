using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace POS_and_Inventory_System
{
    public partial class Form1 : Form
    {

        // SQL connection and command objects
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        string stitle = "POS and Inventory System";
        public string _pass, _user = "";

        public Form1()
        {
            // Instance of DBConnection for database operations
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            NotifyCriticalItems();
        }

        // Method to notify about critical items in inventory
        public void NotifyCriticalItems()
        {
            string critical = "";
            cn.Open();
            cm = new SqlCommand("select count(*) from vwCriticalItems", cn); // Query to get the count of critical items
            string count = cm.ExecuteScalar().ToString();
            cn.Close();

            int i = 0;
            cn.Open();
            cm = new SqlCommand("select * from vwCriticalItems", cn); // Query to get details of critical items
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                critical += i + ". " + dr["pdesc"].ToString() + Environment.NewLine; // Append item details to the string
            }
            dr.Close();
            cn.Close();

            // Create and configure a popup notification
            PopupNotifier popup = new PopupNotifier();
            popup.Image = Properties.Resources.error;
            popup.TitleText = count + " CRITICAL ITEM(S)";
            popup.ContentText = critical;
            popup.Popup();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }





        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        // Load the category management form into a panel
        private void btnCategory_Click(object sender, EventArgs e)
        {
            frmCategoryList frm = new frmCategoryList();
            frm.TopLevel = false;
            panel5.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadCategory();
            frm.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        // Load the product management form into a panel
        private void btnProduct_Click(object sender, EventArgs e)
        {
            frmProductList frm = new frmProductList();
            frm.TopLevel = false;
            panel5.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadProduct();
            frm.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        // Load the stock-in form into a panel
        private void btnStockIn_Click(object sender, EventArgs e)
        {
            frmStockIn frm = new frmStockIn();
            frm.TopLevel = false;
            panel5.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {

        }

        // Open the settings form and load user-specific data
        private void button20_Click(object sender, EventArgs e)
        {
            frmSetting frm = new frmSetting(this);
            frm.LoadRecords();
            frm.txtUser1.Text = _user;
            frm.ShowDialog(); // Open as dialog
        }

        // Open the sales history form as a dialog
        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            frmSoldItems frm = new frmSoldItems();
            frm.ShowDialog();
        }

        // Load various records into the record management form
        private void btnRecords_Click(object sender, EventArgs e)
        {
            frmRecords frm = new frmRecords();
            frm.TopLevel = false;
            frm.LoadCriticalItems();
            frm.LoadInventory();
            frm.LoadStockInHistory();
            frm.LoadCancelledOrders();
            panel5.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();

        }

        // Log out and open the security form
        private void button22_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("LOGOUT APPLICATION?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                frmSecurity f = new frmSecurity();
                f.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard();

        }

        // Load dashboard statistics into a form
        public void Dashboard()
        {
            frmDashboard frm = new frmDashboard();
            frm.TopLevel = false;
            panel5.Controls.Add(frm);
            frm.lblDailySales.Text = dbcon.DailySales().ToString("#,##0.00");
            frm.lblProduct.Text = dbcon.ProductLine().ToString("#,##0");
            frm.lblStockOnHand.Text = dbcon.StockOnHand().ToString("#,##0");
            frm.lblCritical.Text = dbcon.CriticalItems().ToString("#,##0");
            frm.BringToFront();
            frm.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        // Load the vendor management form into a panel
        private void btnVendor_Click(object sender, EventArgs e)
        {
            frmVendorList frm = new frmVendorList();
            frm.TopLevel = false;
            frm.LoadRecords();
            panel5.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        // Open the stock adjustment form and initialize its fields
        private void btnAdjustment_Click(object sender, EventArgs e)
        {
            frmStockAdjustment f = new frmStockAdjustment(this);
            f.LoadProduct();
            f.txtUser.Text = lblname.Text;
            f.ReferenceNo();
            f.ShowDialog();

        }

        // Load the author management form into a panel
        private void btnAuthor_Click(object sender, EventArgs e)
        {
            frmBrandList frm = new frmBrandList();
            frm.TopLevel = false;
            panel5.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
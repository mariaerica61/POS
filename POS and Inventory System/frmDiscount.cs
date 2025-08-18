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
    public partial class frmDiscount : Form
    {
        frmPOS f;
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        string stitle = "POS and Inventory System";

        public frmDiscount(frmPOS frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            f = frm;
            this.KeyPreview = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtPercent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs before parsing
                if (!string.IsNullOrEmpty(txtPrice.Text) && !string.IsNullOrEmpty(txtPercent.Text))
                {
                    // Use invariant culture to ensure consistent decimal parsing
                    if (double.TryParse(txtPrice.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double totalPrice) &&
                        double.TryParse(txtPercent.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double percent))
                    {
                        // Ensure we have valid positive values
                        if (totalPrice >= 0 && percent >= 0 && percent <= 100)
                        {
                            // Calculate discount amount (how much will be deducted)
                            double discountAmount = totalPrice * (percent / 100);
                            txtAmount.Text = discountAmount.ToString("#,##0.00");
                        }
                        else
                        {
                            txtAmount.Text = "0.00";
                        }
                    }
                    else
                    {
                        txtAmount.Text = "0.00";
                    }
                }
                else
                {
                    txtAmount.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                txtAmount.Text = "0.00";
            }
        }
         
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs before saving
                if (string.IsNullOrEmpty(txtAmount.Text) || string.IsNullOrEmpty(txtPercent.Text) || string.IsNullOrEmpty(lblID.Text))
                {
                    MessageBox.Show("Please ensure all fields are filled correctly.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Use invariant culture for consistent parsing
                if (!double.TryParse(txtAmount.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double discountAmount) ||
                    !double.TryParse(txtPercent.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double discountPercent) ||
                    !int.TryParse(lblID.Text, out int cartId))
                {
                    MessageBox.Show("Invalid input values. Please check your entries.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Additional validation
                if (discountAmount < 0 || discountPercent < 0 || discountPercent > 100)
                {
                    MessageBox.Show("Please enter valid discount values. Percentage should be between 0-100.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Add discount? Click yes to confirm.", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(dbcon.MyConnection()))
                    {
                        connection.Open();

                        // Use the discount amount directly from txtAmount (which now shows the deduction)
                        using (SqlCommand command = new SqlCommand("UPDATE tblCart SET disc = @disc WHERE id = @id", connection))
                        {
                            command.Parameters.AddWithValue("@disc", discountAmount);
                            command.Parameters.AddWithValue("@id", cartId);
                            command.ExecuteNonQuery();
                        }
                    }

                    f.LoadCart();
                    this.Dispose();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error applying discount: " + ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmDiscount_Load(object sender, EventArgs e)
        {
            // Prevent manual editing
            txtPrice.ReadOnly = true;
            txtAmount.ReadOnly = true;

            // Optional: make them look like labels (cursor won't appear inside)
            txtPrice.TabStop = false;
            txtAmount.TabStop = false;
        }

        private void frmDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            txtPercent_TextChanged(sender, e);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_and_Inventory_System
{
    public partial class frmCancelDetails : Form
    {
        frmSoldItems f;
        public frmCancelDetails(frmSoldItems frm)
        {
            InitializeComponent();
            f = frm;
        }

        private void frmCancelDetails_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtCancelQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cboAction.Text) || string.IsNullOrWhiteSpace(txtQty.Text) || string.IsNullOrWhiteSpace(txtReason.Text))
                {
                    MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }

                if ((cboAction.Text != String.Empty && txtQty.Text != String.Empty) && (txtReason.Text != String.Empty))
                {
                    if(int.Parse(txtQty.Text) >= int.Parse(txtCancelQty.Text))
                    {
                        frmVoid f = new frmVoid(this);
                        f.ShowDialog();
                    }
                }
                   
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void RefreshList()
        {
            f.LoadRecord();
        }

        private void txtCancelQty_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

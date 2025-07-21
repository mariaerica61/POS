﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmSettlePayment : Form
    {
        frmPOS fpos;
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        string stitle = "POS and Inventory System";

        public frmSettlePayment(frmPOS fp)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            fpos = fp;
            this.KeyPreview = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                double sale = 0, cash = 0;
                if (double.TryParse(txtSale.Text, out sale) && double.TryParse(txtCash.Text, out cash))
                {
                    double change = cash - sale;
                    txtChange.Text = change.ToString("#,##0.00"); 
                }
                else
                {
                   
                    txtChange.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                
                txtChange.Text = "0.00";
            }

        }

        private void txtChange_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSale_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn9.Text;
        }

        private void btnc_Click(object sender, EventArgs e)
        {
            txtCash.Clear();
            txtCash.Focus();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn4.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn5.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn6.Text;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn0.Text;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn1.Text;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn3.Text;

        }

        private void btn00_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn00.Text;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if ((double.Parse(txtChange.Text) < 0) || (txtCash.Text == String.Empty))
                {
                    MessageBox.Show("Insufficient amount. Please enter the correct amount!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    for (int i = 0; i < fpos.dataGridView1.Rows.Count; i++)
                    {
                        cn.Open();
                        cm = new SqlCommand("update tblProduct set qty = qty - " + int.Parse(fpos.dataGridView1.Rows[i].Cells[5].Value.ToString()) + " where pcode = '" + fpos.dataGridView1.Rows[i].Cells[2].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();

                        cn.Open();
                        cm = new SqlCommand("update tblCart set status = 'Sold' where id = '" + fpos.dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();


                    }

                    frmReceipt frm = new frmReceipt(fpos);
                    frm.LoadReport(txtCash.Text, txtChange.Text);
                    frm.ShowDialog();


                    MessageBox.Show("Payment amount successfully saved!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fpos.GetTransNo();
                    fpos.LoadCart();
                    this.Dispose();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insufficient amount. Please enter the correct amount!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void frmSettlePayment_Load(object sender, EventArgs e)
        {

        }

        private void frmSettlePayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }else if (e.KeyCode == Keys.Enter) 
            { 
                btnEnter_Click(sender, e); 
            }
        }
    }


    }



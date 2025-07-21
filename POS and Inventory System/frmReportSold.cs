using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmReportSold : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        frmSoldItems f;

        public frmReportSold(frmSoldItems frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            f = frm;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmReportSold_Load(object sender, EventArgs e)
        {

            this.reportViewer2.RefreshReport();
        }


        public void LoadReport()
        {

            try
            {
                ReportDataSource rptDS;

                this.reportViewer2.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report2.rdlc";
                this.reportViewer2.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                if (f.cboCashier.Text == "All Cashier") 
                { 
                    da.SelectCommand = new SqlCommand("select c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc, c.total from tblCart as c inner join tblProduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" + f.dt1.Value.ToString("yyyy-MM-dd") + "' and '" + f.dt2.Value.ToString("yyyy-MM-dd") + "'", cn); 

                }else 
                { 
                    da.SelectCommand = new SqlCommand("select c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc, c.total from tblCart as c inner join tblProduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" + f.dt1.Value.ToString("yyyy-MM-dd") + "' and '" + f.dt2.Value.ToString("yyyy-MM-dd") + "' and cashier like '" + f.cboCashier.Text + "'", cn); 
                }
                da.Fill(ds.Tables["dtSoldReport"]);
                cn.Close();

                ReportParameter pDate = new ReportParameter("pDate", "Date From: " + f.dt1.Value.ToShortDateString() + " To: " + f.dt2.Value.ToShortDateString());
                ReportParameter pCashier = new ReportParameter("pCashier", "Cashier: " + f.cboCashier.Text);
                ReportParameter pHeader = new ReportParameter("pHeader", "SALES REPORT");

                reportViewer2.LocalReport.SetParameters(pDate);
                reportViewer2.LocalReport.SetParameters(pCashier);
                reportViewer2.LocalReport.SetParameters(pHeader);

                rptDS = new ReportDataSource("DataSet2", ds.Tables["dtSoldReport"]);
                reportViewer2.LocalReport.DataSources.Add(rptDS);
                reportViewer2.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer2.ZoomMode = ZoomMode.Percent;
                reportViewer2.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                cn.Close();

                MessageBox.Show(ex.Message);
            }

        }

        private void reportViewer2_Load(object sender, EventArgs e)
        {

        }
    }
}

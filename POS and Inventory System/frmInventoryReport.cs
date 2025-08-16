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
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmInventoryReport : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        public frmInventoryReport()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmInventoryReport_Load(object sender, EventArgs e)
        {

            this.reportViewer3.RefreshReport();
        }

        public void LoadTopSelling(string sql, string param, string header)
        {
            try
            {
                ReportDataSource rptDS;
                this.reportViewer3.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report4.rdlc";
                this.reportViewer3.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                da.SelectCommand = new SqlCommand(sql, cn);
                da.Fill(ds.Tables["dtTopSelling"]);
                cn.Close();

                ReportParameter pDate = new ReportParameter("pDate", param);
                ReportParameter pHeader = new ReportParameter("pHeader", header);
                reportViewer3.LocalReport.SetParameters(pDate);
                reportViewer3.LocalReport.SetParameters(pHeader);

                rptDS = new ReportDataSource("DataSet4", ds.Tables["dtTopSelling"]);
                reportViewer3.LocalReport.DataSources.Add(rptDS);
                reportViewer3.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer3.ZoomMode = ZoomMode.Percent;
                reportViewer3.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void LoadSoldItems(string sql, string param)
        {
            try
            {
                ReportDataSource rptDS;
                this.reportViewer3.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report5.rdlc";
                this.reportViewer3.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                da.SelectCommand = new SqlCommand(sql, cn);
                da.Fill(ds.Tables["dtSoldItems"]);
                cn.Close();

                ReportParameter pDate = new ReportParameter("pDate", param);
                reportViewer3.LocalReport.SetParameters(pDate);

                rptDS = new ReportDataSource("DataSet5", ds.Tables["dtSoldItems"]);
                reportViewer3.LocalReport.DataSources.Add(rptDS);
                reportViewer3.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer3.ZoomMode = ZoomMode.Percent;
                reportViewer3.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadReport()
        {
            ReportDataSource rptDS;
            try
            {
                reportViewer3.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report3.rdlc";
                reportViewer3.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                da.SelectCommand = new SqlCommand("select p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty, p.reorder from tblProduct as p inner join tblBrand as b on p.bid = b.id inner join tblCategory as c on p.cid= c.id", cn);
                da.Fill(ds.Tables["dtInventory"]);
                cn.Close();

                rptDS = new ReportDataSource("DataSet3", ds.Tables["dtInventory"]);
                reportViewer3.LocalReport.DataSources.Add(rptDS);
                reportViewer3.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer3.ZoomMode = ZoomMode.Percent;
                reportViewer3.ZoomPercent = 100;


            }
            catch (Exception ex)

            {

                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        public void LoadStockInReport(string psql, string param)
        {
            ReportDataSource rptDS;
            try
            {
                reportViewer3.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report6.rdlc";
                reportViewer3.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                ReportParameter pDate = new ReportParameter("pDate", param);
                reportViewer3.LocalReport.SetParameters(pDate);

                cn.Open();
                da.SelectCommand = new SqlCommand(psql, cn);
                da.Fill(ds.Tables["dtStockIn"]);
                cn.Close();

                rptDS = new ReportDataSource("DataSet6", ds.Tables["dtStockIn"]);
                reportViewer3.LocalReport.DataSources.Add(rptDS);
                reportViewer3.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer3.ZoomMode = ZoomMode.Percent;
                reportViewer3.ZoomPercent = 100;


            }
            catch (Exception ex)

            {

                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        public void LoadCancelledReport(string psql, string param)
        {
            ReportDataSource rptDS;
            try
            {
                reportViewer3.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report7.rdlc";
                reportViewer3.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                ReportParameter pDate = new ReportParameter("pDate", param);
                reportViewer3.LocalReport.SetParameters(pDate);

                cn.Open();
                da.SelectCommand = new SqlCommand(psql, cn);
                da.Fill(ds.Tables["dtCancelled"]);
                cn.Close();

                rptDS = new ReportDataSource("DataSet7", ds.Tables["dtCancelled"]);
                reportViewer3.LocalReport.DataSources.Add(rptDS);
                reportViewer3.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer3.ZoomMode = ZoomMode.Percent;
                reportViewer3.ZoomPercent = 100;


            }
            catch (Exception ex)

            {

                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void reportViewer3_Load(object sender, EventArgs e)
        {

        }
    }
}

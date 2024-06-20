using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms;
using DoAn.DBconfig;
using DoAn.Model;

namespace DoAn
{
    public partial class ReportFrm : Form
    {
        public ReportFrm()
        {
            InitializeComponent();
        }
        EnumState type;
        public ReportFrm(EnumState type)
        {
            InitializeComponent();
            this.type = type;
        }
        Modify db = new Modify();
        private void ReportFrm_Load(object sender, EventArgs e)
        {
            switch (type)
            {
                case EnumState.Product:
                    try
                    {
                        reportViewer.LocalReport.ReportEmbeddedResource = "DoAn.ReportProduct.rdlc";
                        ReportDataSource reportDataSoure = new ReportDataSource();
                        reportDataSoure.Name = "DataSetProduct";
                        reportDataSoure.Value = db.GetDataTable("Select * from Product");
                        reportViewer.LocalReport.DataSources.Add(reportDataSoure);
                        this.reportViewer.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case EnumState.Discount:
                    try
                    {
                        reportViewer.LocalReport.ReportEmbeddedResource = "DoAn.ReportDiscount.rdlc";
                        ReportDataSource reportDataSoure = new ReportDataSource();
                        reportDataSoure.Name = "DataSetDiscount";
                        reportDataSoure.Value = db.GetDataTable("Select * from Discount");
                        reportViewer.LocalReport.DataSources.Add(reportDataSoure);
                        this.reportViewer.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case EnumState.Customer:
                    try
                    {
                        reportViewer.LocalReport.ReportEmbeddedResource = "DoAn.ReportCustomer.rdlc";
                        ReportDataSource reportDataSoure = new ReportDataSource();
                        reportDataSoure.Name = "DataSetCustomer";
                        reportDataSoure.Value = db.GetDataTable("Select * from Customer");
                        reportViewer.LocalReport.DataSources.Add(reportDataSoure);
                        this.reportViewer.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case EnumState.Provide:
                    try
                    {
                        reportViewer.LocalReport.ReportEmbeddedResource = "DoAn.ReportProvide.rdlc";
                        ReportDataSource reportDataSoure = new ReportDataSource();
                        reportDataSoure.Name = "DataSetProvide";
                        reportDataSoure.Value = db.GetDataTable("Select * from Provide");
                        reportViewer.LocalReport.DataSources.Add(reportDataSoure);
                        this.reportViewer.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case EnumState.Staff:
                    try
                    {
                        reportViewer.LocalReport.ReportEmbeddedResource = "DoAn.ReportStaff.rdlc";
                        ReportDataSource reportDataSoure = new ReportDataSource();
                        reportDataSoure.Name = "DataSetStaff";
                        reportDataSoure.Value = db.GetDataTable("Select * from Staff");
                        reportViewer.LocalReport.DataSources.Add(reportDataSoure);
                        this.reportViewer.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
           
        }
    }
}

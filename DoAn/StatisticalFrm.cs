using DoAn.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn
{
    public partial class StatisticalFrm : Form
    {
        Product DBProduct = new Product();
        Customer DBCustomer = new Customer();
        Discount DBDiscount = new Discount();
        Provide DBProvide = new Provide();
        Staff DBStaff = new Staff();
        BillDetail DBBillDetail  =new BillDetail();
        List<int> typeDiscount = new List<int>();    
        public StatisticalFrm()
        {
            InitializeComponent();
        }

        EnumState type;
        public StatisticalFrm(EnumState type)
        {
            InitializeComponent();
            this.type = type;
        }

        private void EventStatisticalChart()
        {
            switch (type)
            {
                case EnumState.Product:
                    panelCustomer.Visible = false;
                    pnlDiscount.Visible = false;
                    pnlProvide.Visible = false;
                    pnlStaff.Visible = false;
                    pnlBillDetail.Visible = false;
                    ChartPriceProduct.BackSecondaryColor = Color.FromArgb(64, 173, 202);
                    ChartPriceProduct.ChartAreas.FirstOrDefault().AxisX.Interval = 1;
                    this.BackColor = Color.FromArgb(5, 152, 98);
                    lblTitleStatisticalFrm.ForeColor = Color.White;
                    foreach (var item in DBProduct.ListProduct())
                    {
                        ChartPriceProduct.Series["PriceProduct"].Points.AddXY(item.Name, item.Price);
                    }
                    break;

                case EnumState.Customer:
                    pnlDiscount.Visible = false;
                    pnlProvide.Visible = false;
                    pnlStaff.Visible = false;
                    pnlBillDetail.Visible = false;
                    chartScoreCustomer.ChartAreas.FirstOrDefault().AxisX.Interval = 1;
                    this.BackColor = Color.FromArgb(0, 166, 233);
                    lblTitleStatisticalFrm.ForeColor = Color.White;
                    foreach (var item in DBCustomer.ListCustomer())
                    {
                        chartScoreCustomer.Series["CustomerScore"].Points.AddXY(item.Name, item.Score);
                    }
                    break;
                case EnumState.Discount:
                    CountTypePercentDiscount();
                    pnlProvide.Visible = false;
                    pnlStaff.Visible = false;
                    pnlBillDetail.Visible = false;
                    chartNumberPercentDiscount.ChartAreas.FirstOrDefault().AxisX.Interval = 1;
                    this.BackColor = Color.FromArgb(62, 84, 183);
                    lblTitleStatisticalFrm.ForeColor= Color.White;
                    foreach(var item in DBDiscount.ListDiscount())
                    {
                        chartNumberPercentDiscount.Series["NumberPercent"].Points.AddXY(item.Name, item.Number);
                    }

                    chartTypePercentDiscount.Series["TypePercent"].Points.AddXY("Phần trăm", typeDiscount[0].ToString());
                    chartTypePercentDiscount.Series["TypePercent"].Points.AddXY("Trừ thẳng", typeDiscount[1].ToString());
                    break;

                case EnumState.Provide:
                    pnlStaff.Visible = false;
                    pnlBillDetail.Visible = false;
                    chartTotalPriceProvide.ChartAreas.FirstOrDefault().AxisX.Interval = 1;
                    this.BackColor = Color.FromArgb(133, 4, 120);
                    lblTitleStatisticalFrm.ForeColor = Color.White;
                    foreach (var item in DBProvide.ListProvide())
                    {
                        chartTotalPriceProvide.Series["TotalPrice"].Points.AddXY(item.Name, item.Price);
                    }
                    break;

                case EnumState.Staff:
                    pnlBillDetail.Visible = false;
                    chartYOEStaff.ChartAreas.FirstOrDefault().AxisX.Interval = 1;
                    chartTotalSalaryStaff.ChartAreas.FirstOrDefault().AxisX.Interval = 1;
                    this.BackColor = Color.FromArgb(52, 68, 94);
                    lblTitleStatisticalFrm.ForeColor= Color.White;
                    foreach (var item in DBStaff.ListStaff())
                    {
                        chartYOEStaff.Series["YOE"].Points.AddXY(item.ConvertNameStaff(), item.YOE);
                    }

                    foreach (var item in DBStaff.ListStaff())
                    {
                        chartTotalSalaryStaff.Series["TotalSalary"].Points.AddXY(item.ConvertNameStaff(), item.CalculTotalSalary);
                    }
                    break;
                case EnumState.BillDetail:
                    chartBillDetail.ChartAreas.FirstOrDefault().AxisX.Interval = 1;
                    this.BackColor = Color.FromArgb(75, 34, 22);
                    lblTitleStatisticalFrm.ForeColor = Color.White;
                    foreach (var item in DBBillDetail.BillList())
                    {
                        chartBillDetail.Series["Sanpham"].Points.AddXY(item.ProductName, item.Quantity);
                    }

                    break;
                    
            }
            
        }

        private void CountTypePercentDiscount()
        {
            int countPercent = 0;
            int countNumber = 0;
            int countNoDiscount = 0;
            foreach (var item in DBDiscount.ListDiscount())
            {
                if (item.IsPercent)
                {
                    countPercent++;
                }
                else if (item.IsMoney)
                {
                    countNumber++;
                }
                else
                {
                    countNoDiscount++;
                }
            }
            typeDiscount.Add(countPercent);
            typeDiscount.Add(countNumber);
            typeDiscount.Add(countNoDiscount);


        }
        private void StatisticalFrm_Load(object sender, EventArgs e)
        {
            EventStatisticalChart();
        }

    }
}

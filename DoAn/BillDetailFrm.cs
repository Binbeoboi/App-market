using DoAn.Model;
using OfficeOpenXml.ConditionalFormatting;
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
    public partial class BillDetailFrm : Form
    {
        private string id;
        BillDetail DBBillDetail = new BillDetail();
        Bill DBBill = new Bill();
        public BillDetailFrm()
        {
            InitializeComponent();
        }

        public BillDetailFrm(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void BillDetailFrm_Load(object sender, EventArgs e)
        {

            dgvBillDetail.DataSource = DBBillDetail.BillList(id);
            lblTotalPrice.Text += " " + TotalPrice() + " đồng";
            SetDefaultBill();
        }
        private void SetDefaultBill()
        {
            var data = DBBill.Billslst().SingleOrDefault(n => n.ID.ToUpper().Trim().CompareTo(id.ToUpper().Trim()) == 0);
            if (data == null) return;
            lblIDBill.Text += data.ID;
            lblCustomerName.Text += data.CustomerName;
            lblStaffName.Text += data.StaffName;
            lblTime.Text += data.Date;   
        }
        private double TotalPrice()
        {
            double total = 0;
            foreach(var item in DBBillDetail.BillList(id))
            {
                total += item.TotalPrice;
            }
            return total;   
        }

        
    }
}

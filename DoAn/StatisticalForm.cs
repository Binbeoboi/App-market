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
    public partial class StatisticalForm : Form
    {
        Customer DBCustomer = new Customer();
        Staff DBStaff = new Staff();
        Bill DBBill = new Bill();
        EnumState type;
        public StatisticalForm()
        {
            InitializeComponent();
        }

        public StatisticalForm(EnumState type)
        {
            InitializeComponent();
            this.type = type;
        }

        private void StatisticalForm_Load(object sender, EventArgs e)
        {
            switch (type)
            {
                case EnumState.Customer: StatisticalCustomer(); break;
                case EnumState.Staff: StatisticalStaff(); break;
            }
            
        }

        private void StatisticalCustomer()
        {
            pnlStatisticalStaff.Visible = false;
            var maxScoreCustomer = DBCustomer.ListCustomer().OrderByDescending(x => x.Score).FirstOrDefault();
            var minScoreCustomer = DBCustomer.ListCustomer().OrderBy(x => x.Score).FirstOrDefault();

            lblMaxScore.Text += $"   {maxScoreCustomer.Name}";
            lblMinScore.Text += $"   {minScoreCustomer.Name}";

            var maxBirthDateCustomer = DBCustomer.ListCustomer().OrderByDescending(x => x.BirthDate).FirstOrDefault();
            var minBirthDateCustomer = DBCustomer.ListCustomer().OrderBy(x => x.BirthDate).FirstOrDefault();

            lblMinAge.Text += $"   {minBirthDateCustomer.Name}";
            lblMaxAge.Text += $"   {maxBirthDateCustomer.Name}";

            var maxBack = DBBill.Billslst().GroupBy(x => x.CustomerID.Trim())
                .Select(g => new { CustomerID = g.Key.Trim(), Count = g.Count() })
                .OrderByDescending(x => x.Count).FirstOrDefault();
            
            lblMaxBack.Text += $"   {DBCustomer.ListCustomer().FirstOrDefault(x => x.ID.Trim() == maxBack.CustomerID.Trim()).Name}";
        }

        private void StatisticalStaff()
        {
            var maxYOEStaff = DBStaff.ListStaff().OrderByDescending(x => x.YOE).FirstOrDefault();
            var minYOEStaff = DBStaff.ListStaff().OrderBy(x => x.YOE).FirstOrDefault();

            lblMaxYOE.Text += $"   {maxYOEStaff.Name}";
            lblMinYOE.Text += $"   {minYOEStaff.Name}";

            var maxBirthDateStaff = DBStaff.ListStaff().OrderByDescending(x => x.BirthDate).FirstOrDefault();
            var minBirthDateStaff = DBStaff.ListStaff().OrderBy(x => x.BirthDate).FirstOrDefault();

            lblMaxAgeStaff.Text += $"   {minBirthDateStaff.Name}";
            lblMinAgeStaff.Text += $"   {maxBirthDateStaff.Name}";

            var maxSalary = DBStaff.ListStaff().OrderByDescending(x => x.CalculTotalSalary).FirstOrDefault();
            var minSalary = DBStaff.ListStaff().OrderBy(x => x.CalculTotalSalary).FirstOrDefault();

            lblMaxSalary.Text += $"   {maxSalary.Name}";
            lblMinSalary.Text += $"   {minSalary.Name}";


        }


    }
}

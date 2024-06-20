
using DoAn.Model;
using OfficeOpenXml.Drawing.Style.Coloring;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace DoAn
{
    public partial class Form1 : Form
    {
        Product DBProduct = new Product();
        TypeProduct DBTypeProduct = new TypeProduct();
        Discount DBDiscount = new Discount();
        Customer DBCustomer = new Customer();
        Provide DBProvide = new Provide();
        TypeStaff DBTypeStaff = new TypeStaff();
        Staff DBStaff = new Staff();    
        BillDetail DBBillDetail = new BillDetail();
        Bill DBBill = new Bill();   
        List<Product> lstProduct = new List<Product>(); 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvProduct.DataSource = DBProduct.ListProduct();
            cbbTypeProduct.DataSource = DBTypeProduct.ListTypeProduct();
            cbbDiscountID.DataSource = DBDiscount.ListDiscount();
            cbbProvideID.DataSource = DBProvide.ListProvide();
            timerTitleRun.Start();
        }

        private Product GetProduct()
        {
            return new Product
            {
                ID = txtProductID.Text,
                Name = txtProductName.Text,
                EndTime = dtpEndTime.Value,
                DiscountID = cbbDiscountID.SelectedValue.ToString(),
                Quantity = int.Parse(txtProductQuantity.Text),
                Price = int.Parse(txtProductPrice.Text),
                TypeID = cbbTypeProduct.SelectedValue.ToString(),
                ProvideID = cbbProvideID.SelectedValue.ToString(),
            };
        }

        private Discount GetDiscount()
        {
            Discount discount = new Discount();
            discount.ID = txtMDiscountID.Text;
            discount.Name = txtDiscountName.Text;
            discount.StartTime = dtpStartTimeDiscount.Value;
            discount.EndTime = dtpEndTimeDiscount.Value;
            if(rbtnIsDiscountMoney.Checked)
            {
                discount.IsMoney = true;
                discount.IsPercent = false;
            }
            else
            {
                discount.IsMoney = false;
                discount.IsPercent = true;
            }
            discount.Number =int.Parse(txtNumber.Text);
                    
            return discount;
        }

        private bool IsValidateProduct()
        {
            if (txtProductID.Text.Trim() == "")
            {
                MessageBox.Show("Mã sản phẩm không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (txtProductName.Text.Trim() == "")
            {
                MessageBox.Show("Tên sản phẩm không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (cbbTypeProduct.SelectedIndex == -1)
            {
                MessageBox.Show("Mời chọn thể loại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (txtProductPrice.Text.Trim() == "")
            {
                MessageBox.Show("Giá sản phẩm không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (txtProductQuantity.Text.Trim() == "")
            {
                MessageBox.Show("Số lượng không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (cbbDiscountID.SelectedIndex == -1)
            {
                MessageBox.Show("Mã giảm giá không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private bool IsValidateDiscount()
        {
            if (txtMDiscountID.Text.Trim() == "")
            {
                MessageBox.Show("Mã khuyến mãi không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (txtDiscountName.Text.Trim() == "")
            {
                MessageBox.Show("Tên khuyến mãi không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (txtNumber.Text.Trim() == "")
            {
                MessageBox.Show("Con số khuyến mại không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            
            return true;
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (!IsValidateProduct()) return;

            if (MessageBox.Show("Bạn muốn thêm sản phẩm không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            Product item = GetProduct();

            if (lstProduct.Any(x => x.ID == item.ID))
            {
                MessageBox.Show($"Mã sản phẩm {item.ID} này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DBProduct.AddProduct(item))
            {
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Form1_Load(sender, e);
                dgvProduct.DataSource = DBProduct.ListProduct();
            }
            else
            {
                MessageBox.Show("Thêm không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            //Product item = lstProduct[e.RowIndex];
            //txtProductID.Text = item.ID;
            //txtProductName.Text = item.Name;
            //txtProductPrice.Text = item.Price.ToString();
            //txtProductQuantity.Text = item.Quantity.ToString();
            //cbbDiscountID.SelectedValue = item.DiscountID;
            //cbbTypeProduct.SelectedValue = item.TypeID;
            //dtpDiscountEndTime.Value = DateTime.Parse(item.EndTime.ToString("dd/MM/yyyy"));
            txtProductID.Text = dgvProduct.SelectedRows[0].Cells[0].Value.ToString();
            txtProductName.Text = dgvProduct.SelectedRows[0].Cells[1].Value.ToString();
            dtpEndTime.Value = DateTime.Parse(dgvProduct.SelectedRows[0].Cells[2].Value.ToString());
            cbbDiscountID.Text = dgvProduct.SelectedRows[0].Cells[3].Value.ToString();
            txtProductQuantity.Text = dgvProduct.SelectedRows[0].Cells[4].Value.ToString();
            txtProductPrice.Text = dgvProduct.SelectedRows[0].Cells[5].Value.ToString();
            cbbTypeProduct.Text = dgvProduct.SelectedRows[0].Cells[6].Value.ToString();
            cbbProvideID.Text = dgvProduct.SelectedRows[0].Cells[7].Value.ToString();


        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (txtProductID.Text.Trim() == "")
            {
                MessageBox.Show("Mã sản phẩm không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn muốn xoá sản phẩm không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            //if (!lstProduct.Any(x => x.ID.Trim() == txtProductID.Text.Trim()))
            //{
            //    MessageBox.Show($"Mã sản phẩm {txtProductID.Text.Trim()} này không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            if (DBProduct.RemoveProduct(txtProductID.Text.Trim()))
            {
                MessageBox.Show("Xoá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Form1_Load(sender, e);
                dgvProduct.DataSource = DBProduct.ListProduct();
            }
            else
            {
                MessageBox.Show("Xoá không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!IsValidateProduct()) return;
            
            if (MessageBox.Show("Bạn muốn sửa sản phẩm không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            Product item = GetProduct();
            //kiểm tra mã sản phẩm có trong danh sách chưa
            //if (!lstProduct.Any(x => x.ID == item.ID))
            //{
            //    MessageBox.Show($"Mã sản phẩm {txtProductID.Text.Trim()} này không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            if (DBProduct.EditProduct(item))
            {
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Form1_Load(sender, e);
                dgvProduct.DataSource = DBProduct.ListProduct();
            }
            else
            {
                MessageBox.Show("Sửa không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProductQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else e.Handled = true;
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            if(txtSearchProduct.Text == "")
            {
                dgvProduct.DataSource = DBProduct.ListProduct();
            }

            var data = DBProduct.ListProduct().Where(n => n.Name.ToUpper().Contains(txtSearchProduct.Text.ToUpper())).ToList();

            if (data.Count == 0)
            {
                MessageBox.Show("Không có đối tượng tìm kiếm! Vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvProduct.DataSource = data;   

        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl.SelectedIndex == 0)
            {
                Form1_Load(sender, e);
                lblTitle.ForeColor = Color.FromArgb(5, 152, 98);
            }
            else if(tabControl.SelectedIndex == 1)
            {
                dgvDiscount.DataSource = DBDiscount.ListDiscount();
                lblTitle.ForeColor = Color.FromArgb(62, 84, 183);
            }
            else if(tabControl.SelectedIndex == 2)
            {
                dgvCustomer.DataSource = DBCustomer.ListCustomer();
                lblTitle.ForeColor = Color.FromArgb(0, 166, 233);
            }
            else if(tabControl.SelectedIndex == 3)
            {
                dgvProvide.DataSource = DBProvide.ListProvide();
                cbbTypeProductProvide.DataSource = DBTypeProduct.ListTypeProduct();
                lblTitle.ForeColor = Color.FromArgb(133, 4, 120);
            }
            else if(tabControl.SelectedIndex == 4)
            {
                dgvStaff.DataSource = DBStaff.ListStaff();
                cbbTypeStaff.DataSource = DBTypeStaff.ListTypeStaff();
                lblTitle.ForeColor = Color.FromArgb(52, 68, 94);
            }
            else if(tabControl.SelectedIndex == 5)
            {
                dgvBill.DataSource = DBBill.Billslst();
                cbbCustomerNameBill.DataSource = DBCustomer.ListCustomer();
                cbbNameStaffBill.DataSource = DBStaff.ListCashier();
                lblTitle.ForeColor = Color.FromArgb(242, 210, 72);
            }
            else if(tabControl.SelectedIndex == 6)
            {
                dgvBillDetail.DataSource = DBBillDetail.BillList();
                cbbProductBillDetail.DataSource = DBProduct.ListProduct();
                cbbBillID.DataSource = DBBill.Billslst();
                lblTitle.ForeColor = Color.FromArgb(75, 34, 22);
            }
        }

        private void btnAddDiscount_Click(object sender, EventArgs e)
        {
            if (!IsValidateDiscount()) return;


            if (MessageBox.Show("Bạn muốn thêm khuyến mãi không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            Discount item = GetDiscount();
           

            if (lstProduct.Any(x => x.ID == item.ID))
            {
                MessageBox.Show($"Mã khuyến mại {item.ID} này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DBDiscount.AddDiscount(item))
            {
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDiscount.DataSource = DBDiscount.ListDiscount();
            }
            else
            {
                MessageBox.Show("Thêm không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDiscount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }

            txtMDiscountID.Text = dgvDiscount.SelectedRows[0].Cells[0].Value.ToString();
            txtDiscountName.Text = dgvDiscount.SelectedRows[0].Cells[1].Value.ToString();
            txtNumber.Text = dgvDiscount.SelectedRows[0].Cells[6].Value.ToString();
            dtpStartTimeDiscount.Value = DateTime.Parse(dgvDiscount.SelectedRows[0].Cells[2].Value.ToString());
            dtpEndTimeDiscount.Value = DateTime.Parse(dgvDiscount.SelectedRows[0].Cells[3].Value.ToString());
            rbtnIsDiscountMoney.Checked = bool.Parse(dgvDiscount.SelectedRows[0].Cells[5].Value.ToString());
            rbtnIsDiscountPercent.Checked = bool.Parse(dgvDiscount.SelectedRows[0].Cells[4].Value.ToString());
        }

         
       

        private void btnRemoveDiscount_Click(object sender, EventArgs e)
        {
            if (txtMDiscountID.Text == "") return;


            if (MessageBox.Show("Bạn muốn xóa khuyến mãi không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }


            if (lstProduct.Any(x => x.ID == txtMDiscountID.Text.Trim()))
            {
                MessageBox.Show($"Mã khuyến mại {txtMDiscountID.Text.Trim()} này không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DBDiscount.RemoveDiscount(txtMDiscountID.Text.Trim()))
            {
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDiscount.DataSource = DBDiscount.ListDiscount();
            }
            else
            {
                MessageBox.Show("Xóa không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditDiscount_Click(object sender, EventArgs e)
        {
            if (!IsValidateDiscount()) return;


            if (MessageBox.Show("Bạn muốn sửa khuyến mãi không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            Discount item = GetDiscount();


            if (lstProduct.Any(x => x.ID == item.ID))
            {
                MessageBox.Show($"Mã khuyến mại {item.ID} này không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DBDiscount.EditDiscount(item))
            {
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDiscount.DataSource = DBDiscount.ListDiscount();
            }
            else
            {
                MessageBox.Show("Sửa không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchDiscount_Click(object sender, EventArgs e)
        {
            if (txtSearchDiscount.Text == "")
            {
                dgvDiscount.DataSource = DBDiscount.ListDiscount();
            }

            var data = DBDiscount.ListDiscount().Where(n => n.Name.ToUpper().Contains(txtSearchDiscount.Text.ToUpper())).ToList();

            if (data.Count == 0)
            {
                MessageBox.Show("Không có đối tượng tìm kiếm! Vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvDiscount.DataSource = data;
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCustomerID.Text = dgvCustomer.SelectedRows[0].Cells[0].Value.ToString();
            txtCustomerName.Text = dgvCustomer.SelectedRows[0].Cells[1].Value.ToString();
            txtCustomerAddress.Text = dgvCustomer.SelectedRows[0].Cells[4].Value.ToString();
            txtCustomerPhoneNumber.Text = dgvCustomer.SelectedRows[0].Cells[5].Value.ToString();
            txtCustomerEmail.Text = dgvCustomer.SelectedRows[0].Cells[6].Value.ToString();
            dtpCustomerBirthDate.Value = DateTime.Parse(dgvCustomer.SelectedRows[0].Cells[3].Value.ToString());
            var gender = dgvCustomer.SelectedRows[0].Cells[2].Value.ToString();
            nudCustomerScore.Value = int.Parse(dgvCustomer.SelectedRows[0].Cells[7].Value.ToString());
            if(gender.ToUpper().Trim().CompareTo("nam".ToUpper()) == 0)
            {
                rbtnMale.Checked = true;
                rbtnFemale.Checked = false;
                rbtnOther.Checked = false;
            }
            else if(gender.ToUpper().Trim().CompareTo("Nữ".ToUpper()) == 0)
            {
                rbtnMale.Checked = false;
                rbtnFemale.Checked = true;
                rbtnOther.Checked = false;
            }
            else
            {
                rbtnMale.Checked = false;
                rbtnFemale.Checked = false;
                rbtnOther.Checked = true;
            }
        }

        private bool IsValidateCustomer()
        {
            if (txtCustomerID.Text.Trim() == "")
            {
                MessageBox.Show("Mã khách hàng không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (txtCustomerID.Text.Trim() == "")
            {
                MessageBox.Show("Tên khách hàng không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (txtCustomerAddress.Text.Trim() == "")
            {
                MessageBox.Show("Địa chỉ khách hàng không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (txtCustomerPhoneNumber.Text.Trim() == "")
            {
                MessageBox.Show("Số điện thoại không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            
            return true;
        }

        public Customer GetCustomer()
        {
            Customer customer = new Customer();
            customer.ID = txtCustomerID.Text;
            customer.Name = txtCustomerName.Text;
            if (rbtnMale.Checked)
            {
                customer.Gender = "Nam";
            }
            else if (rbtnFemale.Checked)
            {
                customer.Gender = "Nữ";
            }
            else
            {
                customer.Gender = "khác";
            }
            customer.Address = txtCustomerAddress.Text;
            customer.PhoneNumber = txtCustomerPhoneNumber.Text;
            customer.Email = txtCustomerEmail.Text;
            customer.BirthDate = dtpCustomerBirthDate.Value;
            customer.Score = int.Parse(nudCustomerScore.Value.ToString());
            
            return customer;
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if(!IsValidateCustomer()) { return; }

            if(MessageBox.Show("Bạn có muốn thêm khách hàng không!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            Customer customer = GetCustomer();

            if(DBCustomer.ListCustomer().Any(n => n.ID == customer.ID))
            {
                MessageBox.Show($"Mã {customer.ID.Trim()} đã tồn tại. Vui lòng nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DBCustomer.AddCustomer(customer))
            {
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvCustomer.DataSource = DBCustomer.ListCustomer();
            }
            else
            {
                MessageBox.Show("Thêm khách hàng không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveCustomer_Click(object sender, EventArgs e)
        {
            if (txtCustomerID.Text.Trim() == "") return;

            if (MessageBox.Show("Bạn có muốn xóa khách hàng không!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            string id = txtCustomerID.Text.Trim();  
            if(DBCustomer.ListCustomer().Any(n => n.ID == id))
            {
                MessageBox.Show("Mã sản phẩm này không tôn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(DBCustomer.RemoveCustomer(id))
            {
                MessageBox.Show($"Sản phẩm có mã {id} đã xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvCustomer.DataSource = DBCustomer.ListCustomer();
            }
            else
            {
                MessageBox.Show($"Sản phẩm có mã {id} đã không xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if (!IsValidateCustomer()) { return; }

            if (MessageBox.Show("Bạn có muốn sửa khách hàng không!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            Customer customer = GetCustomer();

            if (!DBCustomer.ListCustomer().Any(n => n.ID == customer.ID))
            {
                MessageBox.Show($"Mã {customer.ID.Trim()} không tồn tại. Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(DBCustomer.EditCustomer(customer))
            {
                MessageBox.Show("Sửa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvCustomer.DataSource = DBCustomer.ListCustomer();
            }
            else
            {
                MessageBox.Show("Sửa khách hàng không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            if (txtSearchCustomer.Text == "")
            {
                dgvCustomer.DataSource = DBCustomer.ListCustomer();
            }

            var data = DBCustomer.ListCustomer().Where(n => n.Name.ToUpper().Contains(txtSearchCustomer.Text.ToUpper())).ToList();

            if (data.Count == 0)
            {
                MessageBox.Show("Không có đối tượng tìm kiếm! Vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dgvCustomer.DataSource = data;
        }

        private void dgvProvide_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            txtProvideID.Text = dgvProvide.SelectedRows[0].Cells[0].Value.ToString();
            txtProvideName.Text = dgvProvide.SelectedRows[0].Cells[1].Value.ToString();
            cbbTypeProductProvide.Text = dgvProvide.SelectedRows[0].Cells[2].Value.ToString();
            txtProvideAddress.Text = dgvProvide.SelectedRows[0].Cells[3].Value.ToString();
            txtQuantityProvide.Text = dgvProvide.SelectedRows[0].Cells[4].Value.ToString();
            txtTotalPrice.Text = dgvProvide.SelectedRows[0].Cells[5].Value.ToString();
            txtPhoneNumberProvide.Text = dgvProvide.SelectedRows[0].Cells[6].Value.ToString();
            txtEmailProvide.Text = dgvProvide.SelectedRows[0].Cells[7].Value.ToString();
        }

        private bool IsValidProvide()
        {
            if(txtProvideID.Text == "")
            {
                MessageBox.Show("Mã nhà cung cấp không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;   
            }
            else if(txtProvideName.Text == "")
            {
                MessageBox.Show("Tên nhà cung cấp không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if(cbbTypeProductProvide.Text == "")
            {
                MessageBox.Show("Thể loại nhà cung cấp không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if(txtProvideAddress.Text == "")
            {
                MessageBox.Show("Địa chỉ nhà cung cấp không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if(txtPhoneNumberProvide.Text == "")
            {
                MessageBox.Show("Số điện thoại nhà cung cấp không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private Provide GetProvide()
        {
            return new Provide
            {
                ID = txtProvideID.Text,
                Name = txtProvideName.Text,
                Type = cbbTypeProductProvide.SelectedValue.ToString(),
                Address = txtProvideAddress.Text,
                Email = txtEmailProvide.Text,
                PhoneNumber = txtPhoneNumberProvide.Text,
                Price = long.Parse(txtTotalPrice.Text),
                Quantity = int.Parse(txtQuantityProvide.Text)
            };
        }
        private void btnAddProvide_Click(object sender, EventArgs e)
        {
            if (!IsValidProvide()) return;

            if(MessageBox.Show("Bạn có muốn thêm nhà cung cấp không.", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) 
            {
                return;
            }
            Provide provide = GetProvide();

            if(DBProvide.ListProvide().Any(n => n.ID == provide.ID))
            {
                MessageBox.Show("Mã nhà cung cấp này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

            if (DBProvide.AddProvide(provide))
            {
                MessageBox.Show("Thêm nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvProvide.DataSource = DBProvide.ListProvide();
            }
            else
            {
                MessageBox.Show("Thêm nhà cung cấp không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void btnRemoveProvide_Click(object sender, EventArgs e)
        {
            if(txtProvideID.Text.Trim() == "")
            {
                MessageBox.Show("Ô mã nhà cung cấp không để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(MessageBox.Show("Bạn có muốn xóa nhà cung cấp không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if(!DBProvide.ListProvide().Any(n => n.ID.ToLower() == txtProvideID.Text.ToLower().Trim()))
            {
                MessageBox.Show($"Mã {txtProvideID.Text.Trim() }cung cấp không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DBProvide.RemoveProvide(txtProvideID.Text.Trim()))
            {
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvProvide.DataSource = DBProvide.ListProvide();
            }
            else
            {
                MessageBox.Show("Xóa không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnEditProvide_Click(object sender, EventArgs e)
        {
            if (!IsValidProvide()) return;

            if(MessageBox.Show("Bạn có muốn sửa nhà cung cấp không?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            Provide provide = GetProvide();
            if(!DBProvide.ListProvide().Any(n => n.ID == provide.ID))
            {
                MessageBox.Show($"Không tồn tại mã {provide.ID}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DBProvide.EditProvide(provide))
            {
                MessageBox.Show($"Sửa thành công nhà cung cấp {provide.ID}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvProvide.DataSource = DBProvide.ListProvide();
            }
            else
            {
                MessageBox.Show($"Sửa không thành công nhà cung cấp {provide.ID}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnSearchProvide_Click(object sender, EventArgs e)
        {
            if(txtSearchProvide.Text == "")
            {
                dgvProvide.DataSource = DBProvide.ListProvide();
            }

            var data = DBProvide.ListProvide().Where(n => n.Name.ToLower().Trim().Contains(txtSearchProvide.Text.ToLower().Trim())).ToList();

            if (data.Count == 0)
            {
                MessageBox.Show("Không có đối tượng tìm kiếm! Vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dgvProvide.DataSource = data;
        }

        private Staff GetStaff()
        {
            Staff staff = new Staff();
            staff.ID = txtIDStaff.Text;
            staff.Name = txtStaffName.Text;
            staff.Address = txtStaffAddress.Text;
            staff.BirthDate = dtpStaffBirthDate.Value;
            staff.Email = txtStaffEmail.Text;
            staff.PhoneNumber = txtStaffPhoneNumber.Text;
            staff.Type = cbbTypeStaff.Text;
            staff.YOE = int.Parse(txtStaffYOE.Text);
            if (rbtnStaffFemale.Checked)
            {
                staff.Gender = rbtnStaffFemale.Text;
            }
            else if(rbtnStaffMale.Checked)
            {
                staff.Gender = rbtnStaffMale.Text;
            }
            else if(rbtnStaffOther.Checked)
            {
                staff.Gender = rbtnStaffOther.Text;
            }
            return staff;
        }

        private bool IsValidStaff()
        {
            if (txtIDStaff.Text == "")
            {
                MessageBox.Show("Mã nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtStaffName.Text == "")
            {
                MessageBox.Show("Tên nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (cbbTypeStaff.Text == "")
            {
                MessageBox.Show("Thể loại nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtStaffAddress.Text == "")
            {
                MessageBox.Show("Địa chỉ nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtStaffPhoneNumber.Text == "")
            {
                MessageBox.Show("Số điện thoại nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            if (!IsValidStaff()) return;

            if(MessageBox.Show("Bạn có muốn thêm nhân viên không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            Staff staff = GetStaff();
            if(DBStaff.ListStaff().Any(n => n.ID == staff.ID))
            {
                MessageBox.Show("Mã nhân viên trên đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(DBStaff.AddStaff(staff))
            {
                MessageBox.Show("Thêm thành công nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvStaff.DataSource = DBStaff.ListStaff();
            }
            else
            {
                MessageBox.Show("Thêm nhân viên không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnRemoveStaff_Click(object sender, EventArgs e)
        {
            if (txtIDStaff.Text == "") return;

            if (MessageBox.Show("Bạn có muốn xóa nhân viên không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if(!DBStaff.ListStaff().Any(n => n.ID.ToLower().Trim() == txtIDStaff.Text.ToLower().Trim()))
            {
                MessageBox.Show("Mã nhân viên trên không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(DBStaff.RemoveStaff(txtIDStaff.Text.Trim()))
            {
                MessageBox.Show("Xóa thành công nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvStaff.DataSource = DBStaff.ListStaff();
            }
            else
            {
                MessageBox.Show("Xóa nhân viên không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnSearchStaff_Click(object sender, EventArgs e)
        {
            if (txtSearchStaff.Text == "")
            {
                dgvStaff.DataSource = DBStaff.ListStaff();
            }

            var data = DBStaff.ListStaff().Where(n => n.Name.ToLower().Trim().Contains(txtSearchStaff.Text.ToLower().Trim())).ToList();
            if (data.Count == 0)
            {
                MessageBox.Show("Không có đối tượng tìm kiếm! Vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dgvStaff.DataSource = data;
        }

        private void btnEditStaff_Click(object sender, EventArgs e)
        {
            if (!IsValidStaff()) return;

            if (MessageBox.Show("Bạn có muốn sửa nhân viên không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            Staff staff = GetStaff();
            if (!DBStaff.ListStaff().Any(n => n.ID == staff.ID))
            {
                MessageBox.Show("Mã nhân viên cần sửa không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DBStaff.EditStaff(staff))
            {
                MessageBox.Show("Sửa thành công nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvStaff.DataSource = DBStaff.ListStaff();
            }
            else
            {
                MessageBox.Show("Sửa nhân viên không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dgvStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            txtIDStaff.Text = dgvStaff.SelectedRows[0].Cells[0].Value.ToString();
            txtStaffName.Text = dgvStaff.SelectedRows[0].Cells[1].Value.ToString();
            var gender = dgvStaff.SelectedRows[0].Cells[2].Value.ToString().ToLower();

            if (gender == "Nam".ToLower())
            {
                rbtnStaffMale.Checked = true;
            }
            else if (gender == "Nữ".ToLower())
            {
                rbtnStaffFemale.Checked = true;
            }
            else if (gender == "Khác".ToLower())
            {
                rbtnStaffOther.Checked = true;
            }

            dtpStaffBirthDate.Value = DateTime.Parse(dgvStaff.SelectedRows[0].Cells[3].Value.ToString());
            txtStaffAddress.Text = dgvStaff.SelectedRows[0].Cells["ColStaffAddress"].Value.ToString();
            txtStaffYOE.Text = dgvStaff.SelectedRows[0].Cells["ColStaffYOE"].Value.ToString();
            cbbTypeStaff.Text = dgvStaff.SelectedRows[0].Cells["ColStaffTypeStaff"].Value.ToString();
            txtStaffPhoneNumber.Text = dgvStaff.SelectedRows[0].Cells["ColStaffPhoneNumber"].Value.ToString();
            txtStaffEmail.Text = dgvStaff.SelectedRows[0].Cells["ColStaffEmail"].Value.ToString();
            
        }

        private void ExportExcelProduct(string path)
        {
            Excel.Application app = new Excel.Application();
            app.Application.Workbooks.Add(Type.Missing);
            for(int i = 0; i < dgvProduct.Columns.Count; i++)
            {
                app.Cells[1, i + 1] = dgvProduct.Columns[i].HeaderText;
            }

            for(int  i = 0; i < dgvProduct.Rows.Count; i++)
            {
                for(int j = 0; j < dgvProduct.Columns.Count; j++)
                {
                    app.Cells[i + 2, j + 1] = dgvProduct.Rows[i].Cells[j].Value;   
                }
            }

            app.Columns.AutoFit();
            app.ActiveWorkbook.SaveCopyAs(path);
            app.ActiveWorkbook.Saved = true;
        }

        private void ExportExcelCustomer(string path)
        {
            Excel.Application app = new Excel.Application();
            app.Application.Workbooks.Add(Type.Missing);
            for (int i = 0; i < dgvCustomer.Columns.Count; i++)
            {
                app.Cells[1, i + 1] = dgvCustomer.Columns[i].HeaderText;
            }

            for (int i = 0; i < dgvCustomer.Rows.Count; i++)
            {
                for (int j = 0; j < dgvCustomer.Columns.Count; j++)
                {
                    app.Cells[i + 2, j + 1] = dgvCustomer.Rows[i].Cells[j].Value;
                }
            }

            app.Columns.AutoFit();
            app.ActiveWorkbook.SaveCopyAs(path);
            app.ActiveWorkbook.Saved = true;
        }

        private void ExportExcelProvide(string path)
        {
            Excel.Application app = new Excel.Application();
            app.Application.Workbooks.Add(Type.Missing);
            for (int i = 0; i < dgvProvide.Columns.Count; i++)
            {
                app.Cells[1, i + 1] = dgvProvide.Columns[i].HeaderText;
            }

            for (int i = 0; i < dgvProvide.Rows.Count; i++)
            {
                for (int j = 0; j < dgvProvide.Columns.Count; j++)
                {
                    app.Cells[i + 2, j + 1] = dgvProvide.Rows[i].Cells[j].Value;
                }
            }

            app.Columns.AutoFit();
            app.ActiveWorkbook.SaveCopyAs(path);
            app.ActiveWorkbook.Saved = true;
        }
        private void ExportExcelStaff(string path)
        {
            Excel.Application app = new Excel.Application();
            app.Application.Workbooks.Add(Type.Missing);
            for (int i = 0; i < dgvStaff.Columns.Count; i++)
            {
                app.Cells[1, i + 1] = dgvStaff.Columns[i].HeaderText;
            }

            for (int i = 0; i < dgvStaff.Rows.Count; i++)
            {
                for (int j = 0; j < dgvStaff.Columns.Count; j++)
                {
                    app.Cells[i + 2, j + 1] = dgvStaff.Rows[i].Cells[j].Value;
                }
            }

            app.Columns.AutoFit();
            app.ActiveWorkbook.SaveCopyAs(path);
            app.ActiveWorkbook.Saved = true;
        }
        private void ExportExcelDiscount(string path)
        {
            Excel.Application app = new Excel.Application();
            app.Application.Workbooks.Add(Type.Missing);
            for (int i = 0; i < dgvDiscount.Columns.Count; i++)
            {
                app.Cells[1, i + 1] = dgvDiscount.Columns[i].HeaderText;
            }

            for (int i = 0; i < dgvDiscount.Rows.Count; i++)
            {
                for (int j = 0; j < dgvDiscount.Columns.Count; j++)
                {
                    app.Cells[i + 2, j + 1] = dgvDiscount.Rows[i].Cells[j].Value;
                }
            }

            app.Columns.AutoFit();
            app.ActiveWorkbook.SaveCopyAs(path);
            app.ActiveWorkbook.Saved = true;
        }
        
        private void btnExcelProduct_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Export Excel";
            save.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";

            if(save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportExcelProduct(save.FileName);
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Xuất file không thành công. Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExcelCustomer_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Export Excel";
            save.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";

            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportExcelCustomer(save.FileName);
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Xuất file không thành công. Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExcelDiscount_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Export Excel";
            save.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";

            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportExcelDiscount(save.FileName);
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Xuất file không thành công. Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnProvideExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Export Excel";
            save.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";

            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportExcelProvide(save.FileName);
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Xuất file không thành công. Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnStaffExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Export Excel";
            save.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";

            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportExcelStaff(save.FileName);
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Xuất file không thành công. Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnStatisticalProduct_Click(object sender, EventArgs e)
        {
            StatisticalFrm statisticalFrm = new StatisticalFrm(EnumState.Product);
            statisticalFrm.ShowDialog();
        }

        private void btnCustomerStatistical_Click(object sender, EventArgs e)
        {
            StatisticalFrm statisticalFrm = new StatisticalFrm(EnumState.Customer);
            statisticalFrm.ShowDialog();
        }

        private void rbtnFemale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnReportProduct_Click(object sender, EventArgs e)
        {
            ReportFrm reportFrm = new ReportFrm(EnumState.Product);
            reportFrm.ShowDialog();
        }

        private void btnDiscountStatistical_Click(object sender, EventArgs e)
        {
            StatisticalFrm statisticalFrm = new StatisticalFrm(EnumState.Discount);
            statisticalFrm.ShowDialog();
        }

        private void btnStatisticalProvide_Click(object sender, EventArgs e)
        {
            StatisticalFrm statisticalFrm = new StatisticalFrm(EnumState.Provide);
            statisticalFrm.ShowDialog();
        }

        private void btnStatisticalStaff_Click(object sender, EventArgs e)
        {
            StatisticalFrm statisticalFrm = new StatisticalFrm(EnumState.Staff);
            statisticalFrm.ShowDialog();
        }

        private void btnReportDiscount_Click(object sender, EventArgs e)
        {
            ReportFrm reportFrm = new ReportFrm(EnumState.Discount);
            reportFrm.ShowDialog();
        }

        private void btnReportCustomer_Click(object sender, EventArgs e)
        {
            ReportFrm reportFrm = new ReportFrm(EnumState.Customer);
            reportFrm.ShowDialog();
        }

        private void btnReportProvide_Click(object sender, EventArgs e)
        {
            ReportFrm reportFrm = new ReportFrm(EnumState.Provide);
            reportFrm.ShowDialog();
        }

        private void btnReportStaff_Click(object sender, EventArgs e)
        {
            ReportFrm reportFrm = new ReportFrm(EnumState.Staff);
            reportFrm.ShowDialog();
        }

        private void timerTitleRun_Tick(object sender, EventArgs e)
        {
            lblTitle.Text = lblTitle.Text.Substring(1, lblTitle.Text.Length - 1) + lblTitle.Text.Substring(0, 1);   
        }

        private void ptbRefreshProduct_Click(object sender, EventArgs e)
        {
            gbProduct.Controls.OfType<TextBox>().ToList().ForEach(n => n.Text = string.Empty);
            cbbTypeProduct.SelectedIndex = -1;
            cbbProvideID.SelectedIndex = -1;
            cbbDiscountID.SelectedIndex = -1;
            dtpEndTime.Value = DateTime.Now;
        }

        private void ptbRefreshDiscount_Click(object sender, EventArgs e)
        {
            gbDiscount.Controls.OfType<TextBox>().ToList().ForEach(n => n.Text= string.Empty);
            gbDiscount.Controls.OfType<DateTimePicker>().ToList().ForEach(n => n.Value = DateTime.Now);
            gbTypeDiscount.Controls.OfType<RadioButton>().ToList().ForEach(n => n.Checked = false);
        }

        private void ptbRefreshCustomer_Click(object sender, EventArgs e)
        {
            gbCustomer.Controls.OfType<TextBox>().ToList().ForEach(n => n.Text = string.Empty);
            gbGenderCustomer.Controls.OfType<RadioButton>().ToList().ForEach(n => n.Checked = false);
            nudCustomerScore.Value = 0;
            dtpCustomerBirthDate.Value = DateTime.Now;  
        }

        private void ptbRefeshProvide_Click(object sender, EventArgs e)
        {
            gbProvide.Controls.OfType<TextBox>().ToList().ForEach(n => n.Text = string.Empty);
            cbbTypeProductProvide.SelectedIndex = -1;
        }

        private void ptbRefreshStaff_Click(object sender, EventArgs e)
        {
            gbStaff.Controls.OfType<TextBox>().ToList().ForEach(n => n.Text = string.Empty);
            cbbTypeStaff.SelectedIndex = -1;
            gbGenderStaff.Controls.OfType<RadioButton>().ToList().ForEach(n => n.Checked = false);  
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebViewFrm frm = new WebViewFrm("https://mail.google.com/");
            frm.ShowDialog();
        }

        private void facebookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebViewFrm frm = new WebViewFrm("https://facebook.com/");
            frm.ShowDialog();
        }

        private void InstagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebViewFrm frm = new WebViewFrm("https://instagram.com/");
            frm.ShowDialog();
        }

        private void tikTokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebViewFrm frm = new WebViewFrm("https://tiktok.com/");
            frm.ShowDialog();
        }

        private void LoginManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersManager userfrm = new UsersManager();
            userfrm.ShowDialog();
        }

        private void TypeProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TypeProductFrm frm = new TypeProductFrm();
            frm.ShowDialog();
        }

        private void dgvBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvBill.Rows.Count == -1) 
            {
                return;
            }
            if(e.ColumnIndex == dgvBill.Columns["ColDetail"].Index)
            {
                var id = dgvBill.SelectedRows[0].Cells["ColIDBill"].Value.ToString();
                BillDetailFrm billFrm = new BillDetailFrm(id);
                billFrm.ShowDialog();   
            }
            else
            {
                txtBillID.Text = dgvBill.SelectedRows[0].Cells["ColIDBill"].Value.ToString();
                cbbCustomerNameBill.Text = dgvBill.SelectedRows[0].Cells["ColCustomerBill"].Value.ToString();
                cbbNameStaffBill.Text = dgvBill.SelectedRows[0].Cells["ColStaffBill"].Value.ToString();
                dtpDateBill.Value = DateTime.Parse(dgvBill.SelectedRows[0].Cells["ColDateBill"].Value.ToString());
            }

        }

        private void btnRefreshBill_Click(object sender, EventArgs e)
        {
            gbBill.Controls.OfType<TextBox>().ToList().ForEach(n => n.Text = string.Empty);
            cbbNameStaffBill.Text = string.Empty;
            cbbCustomerNameBill.Text = string.Empty;
            dtpDateBill.Value = DateTime.Now;
        }
        private Bill GetBill()
        {
            return new Bill()
            {
                ID = txtBillID.Text,
                CustomerID = cbbCustomerNameBill.SelectedValue.ToString(),
                StaffID = cbbNameStaffBill.SelectedValue.ToString(),
                Date = dtpDateBill.Value,
                CustomerName = cbbCustomerNameBill.Text,
                StaffName = cbbNameStaffBill.Text,
            };
        }

        private bool IsValidBill()
        {
            if(txtBillID.Text.CompareTo(string.Empty) == 0)
            {
                MessageBox.Show("Ô mã hóa đơn không để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbbNameStaffBill.Text.CompareTo(string.Empty) == 0)
            {
                MessageBox.Show("Ô nhân viên không để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void btnAddBill_Click(object sender, EventArgs e)
        {
            if (!IsValidBill()) return;

            if(MessageBox.Show("Bạn có muốn thêm hóa đơn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            Bill bill = GetBill();
            if(DBBill.Billslst().Any(n => n.ID == bill.ID))
            {
                MessageBox.Show("Mã này đã bị trùng. Hãy thay mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DBBill.AddBill(bill))
            {
                MessageBox.Show("Thêm thành công hóa đơn mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBCustomer.EditCustomerByScore(bill.CustomerID);
                dgvBill.DataSource = DBBill.Billslst();
            }
            else
            {
                MessageBox.Show("Thêm không thành công hóa đơn mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnRemoveBill_Click(object sender, EventArgs e)
        {
            if(txtBillID.Text.CompareTo(string.Empty) == 0 )
            {
                MessageBox.Show("Ô mã hóa đơn không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"Bạn có muốn xóa hóa đơn {txtBillID.Text} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (!DBBill.Billslst().Any(n => n.ID == txtBillID.Text.ToUpper().Trim()))
            {
                MessageBox.Show("Mã này chưa tồn tại. Hãy thay mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (DBBill.DeleteBill(txtBillID.Text.ToUpper().Trim()))
            {
                MessageBox.Show("Xóa thành công hóa đơn mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvBill.DataSource = DBBill.Billslst();
            }
            else
            {
                MessageBox.Show("Xóa không thành công hóa đơn mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnEditBill_Click(object sender, EventArgs e)
        {
            if (!IsValidBill()) return;

            if (MessageBox.Show("Bạn có muốn sửa hóa đơn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            Bill bill = GetBill();
            if (!DBBill.Billslst().Any(n => n.ID.ToUpper().Trim() == bill.ID))
            {
                MessageBox.Show("Mã này chưa tồn tại. Hãy thay mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DBBill.UpdateBill(bill))
            {
                MessageBox.Show("Sửa thành công hóa đơn mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvBill.DataSource = DBBill.Billslst();
            }
            else
            {
                MessageBox.Show("Sửa không thành công hóa đơn mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnSearchBill_Click(object sender, EventArgs e)
        {
            if(txtSearchBill.Text.CompareTo(string.Empty) == 0)
            {
                dgvBill.DataSource = DBBill.Billslst();
                return;
            }
            var data = DBBill.Billslst().Where(n => n.ID.ToUpper().Contains(txtSearchBill.Text.ToUpper()) ||
                 n.CustomerName.ToUpper().Contains(txtSearchBill.Text.ToUpper()) ||
                 n.StaffName.ToUpper().Contains(txtSearchBill.Text.ToUpper())).ToList();
            if (data.Count == 0)
            {
                MessageBox.Show("Không có đối tượng tìm kiếm! Vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dgvBill.DataSource = data;

        }
        private bool IsvalidBillDetail()
        {
            if(cbbProductBillDetail.SelectedIndex == -1)
            {
                MessageBox.Show("Ô sản phẩm không được để trống", "Thống báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cbbBillID.SelectedIndex == -1)
            {
                MessageBox.Show("Ô mã hóa đơn không được để trống", "Thống báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtQuantityBillDetail.Text == string.Empty)
            {
                MessageBox.Show("Ô số lượng không được để trống", "Thống báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private BillDetail GetBillDetail()
        {
            return new BillDetail()
            {
                ProductID = cbbProductBillDetail.SelectedValue.ToString(),
                Quantity = int.Parse(txtQuantityBillDetail.Text),
                BillID = cbbBillID.SelectedValue.ToString(),
            };
        }
        private void btnAddBillDetail_Click(object sender, EventArgs e)
        {
            if(!IsvalidBillDetail())
            {
                return;
            }
            if(MessageBox.Show("Bạn có muốn thêm không!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) 
            {
                return;
            }
            BillDetail bill = GetBillDetail();
            var itemProduct = DBProduct.ListProduct().SingleOrDefault(n => n.ID.ToLower().Trim().CompareTo(bill.ProductID.ToLower().Trim()) == 0);

            if (itemProduct.Quantity < bill.Quantity)
            {
                MessageBox.Show($"Mặt hàng {bill.ProductName} Không đủ hàng cần thiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DBBillDetail.AddBillDetail(bill))
            {
                MessageBox.Show("Thêm thành công chi tiết hóa đơn", "Thống báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBProduct.EditProductByQuantity(itemProduct, bill.Quantity);
                dgvBillDetail.DataSource = DBBillDetail.BillList();
            }
            else
            {
                MessageBox.Show("Thêm không thành công", "Thống báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnRemoveBillDetail_Click(object sender, EventArgs e)
        {
            if (!IsvalidBillDetail())
            {
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa chi tiết hóa đơn không!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            BillDetail bill = GetBillDetail();
            
            if (DBBillDetail.RemoveBillDetail(bill))
            {
                MessageBox.Show("Xóa thành công chi tiết hóa đơn", "Thống báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvBillDetail.DataSource = DBBillDetail.BillList();
            }
            else
            {
                MessageBox.Show("Xóa không thành công", "Thống báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dgvBillDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }

            cbbProductBillDetail.Text = dgvBillDetail.SelectedRows[0].Cells[1].Value.ToString();
            cbbBillID.Text = dgvBillDetail.SelectedRows[0].Cells[4].Value.ToString();
            txtQuantityBillDetail.Text = dgvBillDetail.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btnEditBillDetail_Click(object sender, EventArgs e)
        {
            if (!IsvalidBillDetail())
            {
                return;
            }
            if (MessageBox.Show("Bạn có muốn sửa chi tiết hóa đơn không!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            BillDetail bill = GetBillDetail();
            if (DBBillDetail.EditBillDetail(bill))
            {
                MessageBox.Show("Sửa thành công chi tiết hóa đơn", "Thống báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvBillDetail.DataSource = DBBillDetail.BillList();
            }
            else
            {
                MessageBox.Show("Sửa không thành công", "Thống báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnSearchBillDetail_Click(object sender, EventArgs e)
        {
            if (txtSearchBillDetail.Text.CompareTo(string.Empty) == 0)
            {
                dgvBillDetail.DataSource = DBBillDetail.BillList();
                return;
            }
            var data = DBBillDetail.BillList().Where(n => n.ProductName.ToUpper().Contains(txtSearchBillDetail.Text.ToUpper()) ||
                 n.BillID.ToUpper().Contains(txtSearchBillDetail.Text.ToUpper())).ToList();
            if(data.Count == 0)
            {
                MessageBox.Show("Không có đối tượng tìm kiếm! Vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
            dgvBillDetail.DataSource = data;
        }

        private void txtSearchBillDetail_TextChanged(object sender, EventArgs e)
        {
            if(txtSearchBillDetail.Text.CompareTo(string.Empty) == 0)
            {
                dgvBillDetail.DataSource = DBBillDetail.BillList();
            }
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchProduct.Text.CompareTo(string.Empty) == 0)
            {
                dgvProduct.DataSource = DBProduct.ListProduct();
            }
            else
            {
                var data = DBProduct.ListProduct().Where(n => n.Name.ToUpper().Contains(txtSearchProduct.Text.ToUpper())).ToList();

                //if (data.Count == 0)
                //{
                //    MessageBox.Show("Không có đối tượng tìm kiếm! Vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                dgvProduct.DataSource = data;
            }
            
        }

        private void txtSearchDiscount_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchDiscount.Text.CompareTo(string.Empty) == 0)
            {
                dgvDiscount.DataSource = DBDiscount.ListDiscount();
            }
        }

        private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchCustomer.Text.CompareTo(string.Empty) == 0)
            {
                dgvCustomer.DataSource = DBCustomer.ListCustomer();
            }
        }

        private void txtSearchProvide_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchProvide.Text.CompareTo(string.Empty) == 0)
            {
                dgvProvide.DataSource = DBProvide.ListProvide();
            }
        }

        private void txtSearchStaff_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchStaff.Text.CompareTo(string.Empty) == 0)
            {
                dgvStaff.DataSource = DBStaff.ListStaff();
            }
        }

        private void txtSearchBill_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchBill.Text.CompareTo(string.Empty) == 0)
            {
                dgvBill.DataSource = DBBill.Billslst();
            }
        }

        private void btnRefreshBillDetail_Click(object sender, EventArgs e)
        {
            cbbProductBillDetail.SelectedIndex = -1;
            cbbBillID.SelectedIndex = -1;
            txtQuantityBillDetail.Text = string.Empty; 
        }

        private void TypeStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TypeStaffFrm frm = new TypeStaffFrm();
            frm.ShowDialog();
        }

        private void btnStatisticalBillDetail_Click(object sender, EventArgs e)
        {
            StatisticalFrm frm = new StatisticalFrm(EnumState.BillDetail);
            frm.ShowDialog();
        }

        private void StatisticalCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatisticalForm frm = new StatisticalForm(EnumState.Customer);
            frm.ShowDialog();
        }

        private void CustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatisticalForm frm = new StatisticalForm(EnumState.Staff);
            frm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

            if(MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) 
            {
                return;
            }
            this.Hide();
            LoginFrm frm = new LoginFrm();
            frm.ShowDialog();
            
        }
        private void ExportExcelBill(string path)
        {
            Excel.Application app = new Excel.Application();
            app.Application.Workbooks.Add(Type.Missing);
            for (int i = 1; i < dgvBill.Columns.Count; i++)
            {
                app.Cells[1, i + 1] = dgvBill.Columns[i].HeaderText;
            }

            for (int i = 0; i < dgvBill.Rows.Count; i++)
            {
                for (int j = 1; j < dgvBill.Columns.Count; j++)
                {
                    app.Cells[i + 2, j + 1] = dgvBill.Rows[i].Cells[j].Value;
                }
            }

            app.Columns.AutoFit();
            app.ActiveWorkbook.SaveCopyAs(path);
            app.ActiveWorkbook.Saved = true;
        }
        private void btnExcelBill_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Export Excel";
            save.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";

            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportExcelBill(save.FileName);
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Xuất file không thành công. Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OpenDarkUI()
        {
            this.BackColor = Color.FromArgb(62, 64, 71);
            gbProduct.BackColor = Color.FromArgb(62, 64, 71);
            gbTableProduct.BackColor = Color.FromArgb(62, 64, 71);
            lblSearchProduct.ForeColor = Color.White;
            gbProduct.Controls.OfType<Label>().ToList().ForEach(n => n.ForeColor = Color.White);
            gbProduct.Controls.OfType<Button>().ToList().ForEach(n => n.ForeColor = Color.White);
            gbProduct.Controls.OfType<Button>().ToList().ForEach(n => n.BackColor = Color.FromArgb(62, 64, 71));
            gbProduct.Controls.OfType<Button>().ToList().ForEach(n => n.FlatAppearance.BorderColor = Color.White);
        }

        private void DarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDarkUI();
        }
    }
}

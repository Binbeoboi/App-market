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
    public partial class TypeStaffFrm : Form
    {
        TypeStaff DB = new TypeStaff();
        public TypeStaffFrm()
        {
            InitializeComponent();
        }

        private void ptbRefresh_Click(object sender, EventArgs e)
        {
            this.Controls.OfType<TextBox>().ToList().ForEach(t => t.Text = string.Empty);
        }
        private TypeStaff GetType()
        {
            return new TypeStaff()
            {
                ID = txtID.Text,
                Name = txtName.Text,
                BaseSalary = int.Parse(txtbaseSalary.Text),
            };
        }

        private bool IsvalidType()
        {
            if (txtID.Text == string.Empty)
            {
                MessageBox.Show("Ô mã không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Ô tên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtbaseSalary.Text == string.Empty)
            {
                MessageBox.Show("Ô lương cơ bản không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void TypeStaffFrm_Load(object sender, EventArgs e)
        {
            dgvType.DataSource = DB.ListTypeStaff();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (txtID.Text == string.Empty)
            {
                return;
            }

            if (MessageBox.Show("Bạn có muốn xóa thể loại này không!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            TypeStaff item = GetType();
            if (!DB.ListTypeStaff().Any(n => n.ID == item.ID))
            {
                MessageBox.Show("Mã này không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (DB.RemoveType(txtID.Text))
            {
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvType.DataSource = DB.ListTypeStaff();
            }
            else
            {
                MessageBox.Show("Xóa không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!IsvalidType())
            {
                return;
            }

            if (MessageBox.Show("Bạn có muốn thêm thể loại không!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            TypeStaff item = GetType();
            if (DB.ListTypeStaff().Any(n => n.ID == item.ID))
            {
                MessageBox.Show("Mã này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (DB.AddType(item))
            {
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvType.DataSource = DB.ListTypeStaff();
            }
            else
            {
                MessageBox.Show("Thêm không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!IsvalidType())
            {
                return;
            }

            if (MessageBox.Show("Bạn có muốn sửa thể loại này không!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            TypeStaff item = GetType();
            if (!DB.ListTypeStaff().Any(n => n.ID == item.ID))
            {
                MessageBox.Show("Mã này không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (DB.EditType(item))
            {
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvType.DataSource = DB.ListTypeStaff();
            }
            else
            {
                MessageBox.Show("Sửa không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dgvType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }
            
            txtID.Text = dgvType.SelectedRows[0].Cells[0].Value.ToString();
            txtName.Text = dgvType.SelectedRows[0].Cells[1].Value.ToString();
            txtbaseSalary.Text = dgvType.SelectedRows[0].Cells[2].Value.ToString();
        }
    }
}

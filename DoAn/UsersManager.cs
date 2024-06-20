using DoAn.DBconfig;
using DoAn.Model;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
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
    public partial class UsersManager : Form
    {
        List<UserPassword> lst = new List<UserPassword> ();
        string path = "Users.txt";
        public UsersManager()
        {
            InitializeComponent();
        }

        private void UsersManager_Load(object sender, EventArgs e)
        {
            lst = WorkWithFile.ReadFileUser(path);
            dgvUserManager.DataSource = lst;
            
        }

        private UserPassword GetUser()
        {
            return new UserPassword()
            {
                UserName = txtUsername.Text,
                Password = txtPassword.Text,
                ID = txtID.Text,
            };
        }
        private void dgvUserManager_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dgvUserManager.SelectedRows[0].Cells[0].Value.ToString();
            txtUsername.Text = dgvUserManager.SelectedRows[0].Cells[1].Value.ToString();
            txtPassword.Text = dgvUserManager.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (!IsValid()) return;

            if (MessageBox.Show($"Bạn có muốn xóa mật khẩu có mã {txtID.Text} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            lst.Remove(lst.SingleOrDefault(n => n.ID == txtID.Text));
            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            WorkWithFile.WriteFileUser(path, lst);
            dgvUserManager.DataSource = lst;

        }

        private bool IsValid()
        {
            if(txtUsername.Text == string.Empty)
            {
                MessageBox.Show("Ô tên tài khoản không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Ô mật khẩu không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(txtID.Text == string .Empty) 
            {
                MessageBox.Show("Ô mã tài khoản không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;    
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!IsValid()) return;

            if(MessageBox.Show($"Bạn có muốn sửa mật khẩu có mã {txtID.Text} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            int index = lst.IndexOf(lst.SingleOrDefault(n => n.ID == txtID.Text));
            lst.Remove(lst.SingleOrDefault(n => n.ID == txtID.Text));
            lst.Insert(index, GetUser());  
            MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            WorkWithFile.WriteFileUser(path, lst);
            dgvUserManager.DataSource = lst.ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == string.Empty)
            {
                MessageBox.Show("Ô tên tài khoản không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Ô mật khẩu không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"Bạn có muốn thêm mật khẩu mới không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            lst.Add(new UserPassword() { ID = "USER" + lst.Count.ToString(), UserName = txtUsername.Text, Password = txtPassword.Text });
            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            WorkWithFile.WriteFileUser(path, lst);
            dgvUserManager.DataSource = lst.ToList();
        }

        private void ptbRefresh_Click(object sender, EventArgs e)
        {
            this.Controls.OfType<TextBox>().ToList().ForEach(t => t.Text = string.Empty);
        }
    }
}

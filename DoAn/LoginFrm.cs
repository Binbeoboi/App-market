using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn.DBconfig;
using DoAn.Model;
using Newtonsoft.Json;

namespace DoAn
{
    public partial class LoginFrm : Form
    {
        string file = "Users.txt";
        List<UserPassword> users = new List<UserPassword>();
        public LoginFrm()
        {
            InitializeComponent();
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {
            users.AddRange(WorkWithFile.ReadFileUser(file));
            DefaultFrm();
        }

        private void DefaultFrm()
        {
            pnlRegister.Visible = false;
            txtUsernameLogin.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!IsValidLogin()) return;

            if (IsLogin())
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 frmmain = new Form1();
                frmmain.ShowDialog();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private bool IsValidLogin()
        {
            if(txtUsernameLogin.Text.CompareTo(string.Empty) == 0)
            {
                MessageBox.Show("Ô tài khoản không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(txtPasswordLogin.Text.CompareTo(string.Empty) == 0)
            {
                MessageBox.Show("Ô tài khoản không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;    
        }

        private bool IsValidRegister()
        {
            if (txtUserNameRegister.Text.CompareTo(string.Empty) == 0)
            {
                MessageBox.Show("Ô tài khoản không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtPasswordRegister.Text.CompareTo(string.Empty) == 0)
            {
                MessageBox.Show("Ô mật khẩu không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtPasswordRegisterAgain.Text.CompareTo(string.Empty) == 0)
            {
                MessageBox.Show("Ô mật khẩu không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtPasswordRegister.Text.CompareTo(txtPasswordRegisterAgain.Text) != 0)
            {
                MessageBox.Show("Mật khẩu và mật khẩu nhập lại phải trùng nhau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private bool IsLogin()
        {
            foreach(var item in  users)
            {
                if(txtUsernameLogin.Text == item.UserName && txtPasswordLogin.Text == item.Password)
                {
                    return true;
                }
            }
            return false;
        }
        private void btnClearLogin_Click(object sender, EventArgs e)
        {
            txtUsernameLogin.Text = string.Empty;
            txtPasswordLogin.Text = string.Empty;
            txtUsernameLogin.Focus();
        }

        private void cbLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLogin.Checked)
            {
                txtPasswordLogin.UseSystemPasswordChar = false;
            }
            else
            {
                txtPasswordLogin.UseSystemPasswordChar = true;
            }
            
        }

        private void llblToRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlRegister.Visible = true; 
        }

        private void llblToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlRegister.Visible = false;
            this.Text = "Đăng ký";
        }

        private void btnClearRegister_Click(object sender, EventArgs e)
        {
            pnlRegister.Controls.OfType<TextBox>().ToList().ForEach(n => n.Text = string.Empty);
            txtUserNameRegister.Focus();
        }

        private void cbRegister_CheckedChanged(object sender, EventArgs e)
        {
            if(cbRegister.Checked)
            {
                txtPasswordRegister.UseSystemPasswordChar = false;
                txtPasswordRegisterAgain.UseSystemPasswordChar = false;
            }
            else
            {
                txtPasswordRegister.UseSystemPasswordChar = true;
                txtPasswordRegisterAgain.UseSystemPasswordChar = true;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!IsValidRegister()) return;

            users.Add(new UserPassword() {ID = "USER" + users.Count.ToString() ,Password = txtPasswordRegister.Text, UserName = txtUserNameRegister.Text });
            WorkWithFile.WriteFileUser(file, users);
            MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

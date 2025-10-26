using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TcpUserServer
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            btnRegister.Enabled = false;
            txtStatus.Text = "Đang gửi đăng ký...";

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirm = txtConfirm.Text;
            string email = txtEmail.Text.Trim();
            string fullname = txtFullName.Text.Trim();
            string birthday = dtBirthday.Value.ToString("yyyy-MM-dd");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                txtStatus.Text = "Username và password không được rỗng";
                btnRegister.Enabled = true;
                return;
            }
            if (password != confirm)
            {
                txtStatus.Text = "Mật khẩu không khớp";
                btnRegister.Enabled = true;
                return;
            }
            if (!string.IsNullOrEmpty(email) && !Regex.IsMatch(email, @"^\S+@\S+\.\S+$"))
            {
                txtStatus.Text = "Email không hợp lệ";
                btnRegister.Enabled = true;
                return;
            }

            string passHash = Utils.Sha256Hash(password);
            var payload = new
            {
                Username = username,
                Password = passHash,
                Email = email,
                FullName = fullname,
                Birthday = birthday
            };

            var req = new Request { Action = "Register", Data = payload }; 
            try
            {
                var resp = await ClientHelper.SendRequestAsync(req);
                txtStatus.Text = resp.Message;
                if (resp.Success)
                {
                    MessageBox.Show("Đăng ký thành công. Mời đăng nhập.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                txtStatus.Text = "Lỗi: " + ex.Message;
            }
            finally
            {
                btnRegister.Enabled = true;
            }
        }
    }
}

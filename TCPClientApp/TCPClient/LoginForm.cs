using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedLibrary;

namespace TCPClient
{
    public partial class LoginForm : Form
    {
        private readonly ClientSocket client = new ClientSocket();

        public LoginForm()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đủ tên đăng nhập và mật khẩu!");
                return;
            }

            // Kết nối đến server
            bool connected = await client.ConnectAsync("127.0.0.1", 5000);
            if (!connected)
            {
                MessageBox.Show("Không thể kết nối tới server.");
                return;
            }

            // Băm mật khẩu
            string hashed = SecurityHelper.ComputeSHA256(password);

            // Gửi thông tin đăng nhập
            string message = $"LOGIN|{username}|{hashed}";
            await client.SendAsync(message);

            // Nhận phản hồi
            string response = await client.ReceiveAsync();
            if (response == "OK")
            {
                MessageBox.Show("Đăng nhập thành công!");
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
            }

            client.Close();
        }
    }
}

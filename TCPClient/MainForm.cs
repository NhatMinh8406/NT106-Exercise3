using System;
using System.Windows.Forms;
using TCPClient;

namespace TCPClient
{
    public partial class MainForm : Form
    {
        private Form _loginForm;
        private string _userName;
        private string _userEmail;
        private string _userBirthday;

        public MainForm()
        {
            InitializeComponent();

            _userName = "";
            _userEmail = "";
            _userBirthday = "";
            _loginForm = null;

            this.Load += new System.EventHandler(this.MainForm_Load);


            this.button1.Click += (sender, e) => { Application.Exit(); };


            this.FormClosed += (sender, e) => { Application.Exit(); };
        }
        public MainForm(Form loginForm, string userName, string email, string birthday)
        {
            InitializeComponent();

            _loginForm = loginForm;
            _userName = userName;
            _userEmail = email;
            _userBirthday = birthday;

            this.Load += new System.EventHandler(this.MainForm_Load);

            this.button1.Click += new System.EventHandler(this.button1_Click_Real);

            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed_Real);
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = _userName;
            textBox2.Text = _userEmail;
            textBox3.Text = _userBirthday;

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
        }

        private void button1_Click_Real(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosed_Real(object sender, FormClosedEventArgs e)
        {
            if (_loginForm != null)
            {
                _loginForm.Show();
            }
        }
    }
}
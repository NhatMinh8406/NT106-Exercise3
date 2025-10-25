using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TCPClient
{
    public partial class RegisterForm : Form
    {
        private void RoundCorners(Control c, int radius, bool topLeft, bool topRight, bool bottomRight, bool bottomLeft)
        {
            if (c.Width <= 0 || c.Height <= 0)
                return;

            int w = c.Width;
            int h = c.Height;

            GraphicsPath path = new GraphicsPath();
            if (topLeft)
            {
                path.AddArc(0, 0, radius, radius, 180, 90);
            }
            else
            {
                path.AddLine(0, 0, 0, 0);
            }

            if (topRight)
            {
                path.AddArc(w - radius, 0, radius, radius, 270, 90);
            }
            else
            {
                path.AddLine(w, 0, w, 0);
            }

            if (bottomRight)
            {
                path.AddArc(w - radius, h - radius, radius, radius, 0, 90);
            }
            else
            {
                path.AddLine(w, h, w, h);
            }

            if (bottomLeft)
            {
                path.AddArc(0, h - radius, radius, radius, 90, 90);
            }
            else
            {
                path.AddLine(0, h, 0, h);
            }

            path.CloseFigure();

            c.Region = new Region(path);
        }

        private bool PasswordAvailable()
        {
            bool num = false, lwcase = false, upcase = false;
            foreach (char c in password_textbox.Text)
            {
                if (c >= '0' && c <= '9') num = true;
                else if (c >= 'a' && c <= 'z') lwcase = true;
                else if (c >= 'A' && c <= 'Z') upcase = true;
                else return false;
            }
            if (password_textbox.Text.Length >= 8 && num && lwcase && upcase)
            {
                return true;
            }
            return false;
        }
        private bool PasswordConfirmCorrect()
        {
            return password_textbox.Text == password_confirm_textbox.Text;
        }
        public RegisterForm()
        {
            InitializeComponent();
            password_warning.Visible = false;
            password_confirm_warning.Visible = false;
            register_fail_warning.Visible = false;
            register_label.FlatAppearance.BorderSize = 0;
            register_label.FlatAppearance.MouseOverBackColor = register_label.BackColor;
            register_label.FlatAppearance.MouseDownBackColor = register_label.BackColor;
            register_confirm_button.FlatAppearance.BorderSize = 0;
            RoundCorners(register_box, 20, true, true, true, true);
            RoundCorners(register_label, 20, true, true, false, false);
            RoundCorners(username_textbox, 5, true, true, true, true);
            RoundCorners(password_textbox, 5, true, true, true, true);
            RoundCorners(password_confirm_textbox, 5, true, true, true, true);
            RoundCorners(register_confirm_button, 20, true, true, true, true);
        }

        private void username_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pasword_textbox_TextChanged(object sender, EventArgs e)
        {
            password_warning.Visible = false;
            if (!PasswordAvailable())
            {
                password_warning.Visible = true;
            }
            if (!PasswordConfirmCorrect())
            {
                password_confirm_warning.Visible = true;
            }
        }
        private void password_confirm_textbox_TextChanged(object sender, EventArgs e)
        {
            password_confirm_warning.Visible = false;
            if (!PasswordConfirmCorrect())
            {
                password_confirm_warning.Visible = true;
            }
        }
        private void to_login_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        private void register_confirm_button_Click(object sender, EventArgs e)
        {
            if (PasswordAvailable() && PasswordConfirmCorrect())
            {
                string type = "REGISTER";
                string username = username_textbox.Text;
                string password = password_textbox.Text;
                
                string encrypted_password = SecurityHelper.Encrypt(password);

                 request = $"{type}|{username}|{encrypted_password}";
                ClientSocket.SendData(request);
                bool response = ClientSocket.ReceiveData();

                if (response)
                {
                    
                }
                else
                {
                    register_fail_warning.Visible = true;
                }
            }
        }
    }
}

namespace TCPClient
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.register_box = new System.Windows.Forms.Panel();
            this.to_login = new System.Windows.Forms.LinkLabel();
            this.account_already = new System.Windows.Forms.Label();
            this.password_confirm_warning = new System.Windows.Forms.Label();
            this.password_warning = new System.Windows.Forms.Label();
            this.register_confirm_button = new System.Windows.Forms.Button();
            this.password_confirm_textbox = new System.Windows.Forms.TextBox();
            this.password_confirm_label = new System.Windows.Forms.Label();
            this.password_textbox = new System.Windows.Forms.TextBox();
            this.password_label = new System.Windows.Forms.Label();
            this.username_textbox = new System.Windows.Forms.TextBox();
            this.username_label = new System.Windows.Forms.Label();
            this.register_label = new System.Windows.Forms.Button();
            this.register_fail_warning = new System.Windows.Forms.Label();
            this.register_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // register_box
            // 
            this.register_box.BackColor = System.Drawing.Color.White;
            this.register_box.Controls.Add(this.register_fail_warning);
            this.register_box.Controls.Add(this.to_login);
            this.register_box.Controls.Add(this.account_already);
            this.register_box.Controls.Add(this.password_confirm_warning);
            this.register_box.Controls.Add(this.password_warning);
            this.register_box.Controls.Add(this.register_confirm_button);
            this.register_box.Controls.Add(this.password_confirm_textbox);
            this.register_box.Controls.Add(this.password_confirm_label);
            this.register_box.Controls.Add(this.password_textbox);
            this.register_box.Controls.Add(this.password_label);
            this.register_box.Controls.Add(this.username_textbox);
            this.register_box.Controls.Add(this.username_label);
            this.register_box.Controls.Add(this.register_label);
            this.register_box.Location = new System.Drawing.Point(211, 81);
            this.register_box.Name = "register_box";
            this.register_box.Size = new System.Drawing.Size(612, 425);
            this.register_box.TabIndex = 1;
            // 
            // to_login
            // 
            this.to_login.AutoSize = true;
            this.to_login.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.to_login.Location = new System.Drawing.Point(161, 367);
            this.to_login.Name = "to_login";
            this.to_login.Size = new System.Drawing.Size(95, 23);
            this.to_login.TabIndex = 11;
            this.to_login.TabStop = true;
            this.to_login.Text = "Đăng nhập";
            this.to_login.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.to_login_LinkClicked);
            // 
            // account_already
            // 
            this.account_already.AutoSize = true;
            this.account_already.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.account_already.Location = new System.Drawing.Point(35, 367);
            this.account_already.Name = "account_already";
            this.account_already.Size = new System.Drawing.Size(138, 23);
            this.account_already.TabIndex = 10;
            this.account_already.Text = "Đã có tài khoản?";
            // 
            // password_confirm_warning
            // 
            this.password_confirm_warning.ForeColor = System.Drawing.Color.Red;
            this.password_confirm_warning.Location = new System.Drawing.Point(435, 314);
            this.password_confirm_warning.Name = "password_confirm_warning";
            this.password_confirm_warning.Size = new System.Drawing.Size(144, 20);
            this.password_confirm_warning.TabIndex = 9;
            this.password_confirm_warning.Text = "*Mật khẩu không khớp";
            this.password_confirm_warning.Visible = false;
            // 
            // password_warning
            // 
            this.password_warning.ForeColor = System.Drawing.Color.Red;
            this.password_warning.Location = new System.Drawing.Point(435, 198);
            this.password_warning.Name = "password_warning";
            this.password_warning.Size = new System.Drawing.Size(144, 67);
            this.password_warning.TabIndex = 8;
            this.password_warning.Text = "*Mật khẩu cần tối thiểu 8 kí tự bao gồm ít nhất 1 kí tự viết thường, 1 kí tự viết" +
    " hoa và 1 chữ số";
            this.password_warning.Visible = false;
            // 
            // register_confirm_button
            // 
            this.register_confirm_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.register_confirm_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.register_confirm_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.register_confirm_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.register_confirm_button.ForeColor = System.Drawing.Color.White;
            this.register_confirm_button.Location = new System.Drawing.Point(459, 345);
            this.register_confirm_button.Name = "register_confirm_button";
            this.register_confirm_button.Size = new System.Drawing.Size(131, 53);
            this.register_confirm_button.TabIndex = 7;
            this.register_confirm_button.Text = "Đăng ký";
            this.register_confirm_button.UseVisualStyleBackColor = false;
            this.register_confirm_button.Click += new System.EventHandler(this.register_confirm_button_Click);
            // 
            // password_confirm_textbox
            // 
            this.password_confirm_textbox.Location = new System.Drawing.Point(35, 308);
            this.password_confirm_textbox.MaxLength = 75;
            this.password_confirm_textbox.Name = "password_confirm_textbox";
            this.password_confirm_textbox.Size = new System.Drawing.Size(384, 25);
            this.password_confirm_textbox.TabIndex = 6;
            this.password_confirm_textbox.UseSystemPasswordChar = true;
            this.password_confirm_textbox.TextChanged += new System.EventHandler(this.password_confirm_textbox_TextChanged);
            // 
            // password_confirm_label
            // 
            this.password_confirm_label.AutoSize = true;
            this.password_confirm_label.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_confirm_label.Location = new System.Drawing.Point(35, 276);
            this.password_confirm_label.Name = "password_confirm_label";
            this.password_confirm_label.Size = new System.Drawing.Size(176, 25);
            this.password_confirm_label.TabIndex = 5;
            this.password_confirm_label.Text = "Xác nhận mật khẩu";
            // 
            // password_textbox
            // 
            this.password_textbox.Location = new System.Drawing.Point(35, 223);
            this.password_textbox.MaxLength = 75;
            this.password_textbox.Name = "password_textbox";
            this.password_textbox.Size = new System.Drawing.Size(384, 25);
            this.password_textbox.TabIndex = 4;
            this.password_textbox.UseSystemPasswordChar = true;
            this.password_textbox.TextChanged += new System.EventHandler(this.pasword_textbox_TextChanged);
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_label.Location = new System.Drawing.Point(35, 191);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(93, 25);
            this.password_label.TabIndex = 3;
            this.password_label.Text = "Mật khẩu";
            // 
            // username_textbox
            // 
            this.username_textbox.Location = new System.Drawing.Point(35, 138);
            this.username_textbox.MaxLength = 75;
            this.username_textbox.Name = "username_textbox";
            this.username_textbox.Size = new System.Drawing.Size(384, 25);
            this.username_textbox.TabIndex = 2;
            this.username_textbox.TextChanged += new System.EventHandler(this.username_textbox_TextChanged);
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_label.Location = new System.Drawing.Point(35, 106);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(138, 25);
            this.username_label.TabIndex = 1;
            this.username_label.Text = "Tên đăng nhập";
            // 
            // register_label
            // 
            this.register_label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.register_label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.register_label.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.register_label.ForeColor = System.Drawing.Color.White;
            this.register_label.Location = new System.Drawing.Point(0, 0);
            this.register_label.Name = "register_label";
            this.register_label.Size = new System.Drawing.Size(612, 90);
            this.register_label.TabIndex = 0;
            this.register_label.Text = "ĐĂNG KÝ";
            this.register_label.UseVisualStyleBackColor = false;
            // 
            // register_fail_warning
            // 
            this.register_fail_warning.ForeColor = System.Drawing.Color.Red;
            this.register_fail_warning.Location = new System.Drawing.Point(435, 131);
            this.register_fail_warning.Name = "register_fail_warning";
            this.register_fail_warning.Size = new System.Drawing.Size(144, 42);
            this.register_fail_warning.TabIndex = 12;
            this.register_fail_warning.Text = "*Đăng ký không thành công vui lòng thử lại";
            this.register_fail_warning.Visible = false;
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 588);
            this.Controls.Add(this.register_box);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "RegisterForm";
            this.Text = "RegisterForm";
            this.register_box.ResumeLayout(false);
            this.register_box.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel register_box;
        private System.Windows.Forms.LinkLabel to_login;
        private System.Windows.Forms.Label account_already;
        private System.Windows.Forms.Label password_confirm_warning;
        private System.Windows.Forms.Label password_warning;
        private System.Windows.Forms.Button register_confirm_button;
        private System.Windows.Forms.TextBox password_confirm_textbox;
        private System.Windows.Forms.Label password_confirm_label;
        private System.Windows.Forms.TextBox password_textbox;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.TextBox username_textbox;
        private System.Windows.Forms.Label username_label;
        private System.Windows.Forms.Button register_label;
        private System.Windows.Forms.Label register_fail_warning;
    }
}
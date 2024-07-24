using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ui_DuAn;
using MimeKit;
using MailKit.Net.Smtp;
<<<<<<< Updated upstream
=======
using UIDuAn1.Models;
>>>>>>> Stashed changes
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace UIDuAn1
{
    public partial class FormQuenMatKhau : Form
    {
        public FormQuenMatKhau()
        {
            InitializeComponent();
        }
<<<<<<< Updated upstream
=======

        private void btnThoat_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
        // gui matkhau ve mail

        private void RetrievePassword(string email)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var nhanVien = context.NhanVien.FirstOrDefault(nv => nv.Gmail == email);
                if (nhanVien == null)
                {
                    MessageBox.Show("Email không tồn tại.");
                    return;
                }

                try
                {
                    SendEmail(nhanVien.Gmail, "Mật khẩu của bạn", $"Mật khẩu của bạn là: {nhanVien.MatKhau}");
                    DialogResult result = MessageBox.Show
                    ("Mật khẩu đã được gửi đến email của bạn.");
                    if (result == DialogResult.OK)
                    {
                        LoginForm formdangNhap = new LoginForm();
                        formdangNhap.Show();
                        this.Close();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi");
                }
            }
        }

        private void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("nguyenncpc09256", "nguyenchinguyen7925@gmail.com"));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("nguyenchinguyen7925@gmail.com", "zkfe bzvm qkam uklk"); // Thay bằng mật khẩu ứng dụng
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private bool IsValidEmail(string email)
        {
            return email.EndsWith("@gmail.com");
        }
        private bool IsNullOrWhiteSpace(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string Email = txtEmail.Text;
            if (!IsValidEmail(Email)|| IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("Định dạng Email phải là: username@gmail.com");
                txtEmail.Focus();
                return;
            }
            RetrievePassword(Email);
        }
>>>>>>> Stashed changes
    }
}

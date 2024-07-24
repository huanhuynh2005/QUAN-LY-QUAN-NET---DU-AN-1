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
<<<<<<< Updated upstream
=======
using UIDuAn1.Models;
>>>>>>> Stashed changes

namespace UIDuAn1
{
    public partial class UC_TaiKhoan : UserControl
    {
        public UC_TaiKhoan()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
<<<<<<< Updated upstream
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {

=======
        public static string CurrentUserEmail { get; set; }
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string matkhaucu = txtMatKhauCu.Text;
            string matkhaumoi = txtMatKhauMoi.Text;
            string nhaplaiMK = txtNhapLaiMatKhau.Text;

            using (var context = new QUANLYQUANNETContext())
            {
                var query = from NhanVien in context.NhanVien
                            where NhanVien.Gmail == CurrentUserEmail && NhanVien.MatKhau == matkhaucu
                            select NhanVien;
                var result = query.Count();

                // Kiểm tra kết quả trả về từ truy vấn
                if (result > 0 ||
                    txtMatKhauMoi.Text == "" ||
                    txtMatKhauCu.Text == "" ||
                    txtMatKhauCu.Text == ""
                    )
                {
                    // Kiểm tra mật khẩu mới và nhập lại mật khẩu mới có khớp nhau
                    if (matkhaumoi == nhaplaiMK)
                    {
                        // Cập nhật mật khẩu mới vào cơ sở dữ liệu
                        var nhanVien = query.FirstOrDefault();
                        nhanVien.MatKhau = matkhaumoi;
                        context.SaveChanges();

                        // Hiển thị MainForm và ẩn FormDoiMatKhau
                        LoginForm loginform = new LoginForm();
                        loginform.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Nhập lại mật khẩu mới không khớp !");
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác !");
                }
            }
>>>>>>> Stashed changes
        }
    }
}

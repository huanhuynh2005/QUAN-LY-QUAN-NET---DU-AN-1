using System;
using System.Linq;
using System.Windows.Forms;
using UIDuAn1;
<<<<<<< Updated upstream
=======
using UIDuAn1.Models;

>>>>>>> Stashed changes
namespace Ui_DuAn
{
    public partial class LoginForm : Form
    {
        // Biến lưu vai trò người dùng
        public static int VaiTroNguoiDung { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // sdf.SetShadowForm(this);
        }
<<<<<<< Updated upstream
        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
=======

        private bool IsValidEmail(string email)
        {
            return email.EndsWith("@gmail.com");
        }
        private bool IsNullOrWhiteSpace(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
            string Email = txtEmail.Text;
            string matKhau = txtMatKhau.Text;

            if (!IsValidEmail(Email)||IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("Định dạng Email phải là: username@gmail.com");
                txtEmail.Focus();
                return;
            }

            using (var context = new QUANLYQUANNETContext())
            {
                var query = from NhanVien in context.NhanVien
                            where NhanVien.Gmail == Email && NhanVien.MatKhau == matKhau
                            select NhanVien;

                var nhanVien = query.FirstOrDefault();

                // Kiểm tra kết quả trả về từ truy vấn
                if (nhanVien != null)
                {
                    VaiTroNguoiDung = nhanVien.VaiTro; // Chỉnh sửa thành kiểu int, bool không được

                    MainForm mainform = new MainForm();
                    mainform.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!");
                }
            }
>>>>>>> Stashed changes
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
        }
        private void lbQuenMatKhau_Click(object sender, EventArgs e)
        {
<<<<<<< Updated upstream
=======
            FormQuenMatKhau formQuenMatKhau = new FormQuenMatKhau();
            formQuenMatKhau.Show();
            this.Hide();
>>>>>>> Stashed changes
        }
    }
}

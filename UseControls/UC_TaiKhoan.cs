﻿using System;
using System.Linq;
using System.Windows.Forms;
using UIDuAn1.Model;
using Ui_DuAn;
using static Ui_DuAn.LoginForm;

namespace UIDuAn1
{
    public partial class UC_TaiKhoan : UserControl
    {

        public UC_TaiKhoan()
        {
            InitializeComponent();
            loadData();
        }
        private void loadData()
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var nhanVien = context.NhanVien
                    .FirstOrDefault(nv => nv.Gmail == CurrentUser.Instance.Email);

                if (nhanVien != null)
                {
                    txtEmail.Text = nhanVien.Gmail;
                    txtMaVaTenNV.Text = $"{nhanVien.MaNhanVien} | {nhanVien.HoTen}";
                    txtDiaChi.Text = nhanVien.DiaChi;
                    txtVaiTro.Text = nhanVien.TenVaiTro;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin nhân viên!");
                }
            }
        }
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string matkhaucu = txtMatKhauCu.Text;
            string matkhaumoi = txtMatKhauMoi.Text;
            string nhaplaiMK = txtNhapLaiMatKhau.Text;

            if (string.IsNullOrWhiteSpace(matkhaucu) || string.IsNullOrWhiteSpace(matkhaumoi) || string.IsNullOrWhiteSpace(nhaplaiMK))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            using (var context = new QUANLYQUANNETContext())
            {
                var query = from NhanVien in context.NhanVien
                            where NhanVien.Gmail == CurrentUser.Instance.Email && NhanVien.MatKhau == matkhaucu
                            select NhanVien;
                var nhanVien = query.FirstOrDefault();

                if (nhanVien != null)
                {
                    if (matkhaucu == matkhaumoi)
                    {
                        MessageBox.Show("Mật khẩu mới không được trùng mật khẩu cũ");
                    }
                    else if (matkhaumoi == nhaplaiMK)
                    {
                        nhanVien.MatKhau = matkhaumoi;
                        context.SaveChanges();

                        MessageBox.Show("Đổi mật khẩu thành công!");

                        CurrentUser.Instance.MatKhau = matkhaumoi;

                        // Tạo và hiển thị form đăng nhập
                        LoginForm loginform = new LoginForm();
                        loginform.Show();

                        // Đóng form cha của user control
                        Form parentForm = this.FindForm();
                        if (parentForm != null)
                        {
                            parentForm.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nhập lại mật khẩu mới không khớp!");
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu không chính xác!");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIDuAn1.Models;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace UIDuAn1
{
    public partial class UC_KhachHang : UserControl
    {
        public UC_KhachHang()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var query = from KhachHang in context.KhachHang
                            select new
                            {
                                KhachHang.MaKhachHang,
                                KhachHang.TaiKhoan,
                                KhachHang.MatKhau,
                                KhachHang.SoGioSuDung,
                                KhachHang.MaNhanVien,
                            };

                dtgKhachHang.DataSource = query.ToList();

                dtgKhachHang.Columns[0].HeaderText = "Mã Khách Hàng";
                dtgKhachHang.Columns[1].HeaderText = "Tài Khoản";
                dtgKhachHang.Columns[2].HeaderText = "Mật Khẩu";
                dtgKhachHang.Columns[3].HeaderText = "Thời Gian Sử Dụng";
                dtgKhachHang.Columns[4].HeaderText = "Mã Nhân Viên";
            }
        }


        private void UC_KhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void Reset()
        {
            txtMaKhachHang.Clear();
            txtTaiKhoan.Clear();
            txtMatkhau.Clear();
            txtThoiGianSuDung.Clear();
            txtTimKiem.Clear();
            cbMaNhanVien.Items.Clear();
        }

        private void dtgKhachHang_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectRow = dtgKhachHang.Rows[e.RowIndex];

                string makh = selectRow.Cells["MaKhachHang"].Value.ToString();
                string taikhoan = selectRow.Cells["TaiKhoan"].Value.ToString();
                string matkhau = selectRow.Cells["MatKhau"].Value.ToString();
                string sogiosd = selectRow.Cells["SoGioSuDung"].Value.ToString();
                string manv = selectRow.Cells["MaNhanVien"].Value.ToString();

                txtMaKhachHang.Text = makh;
                txtTaiKhoan.Text = taikhoan;
                txtMatkhau.Text = matkhau;
                txtThoiGianSuDung.Text = sogiosd;
                cbMaNhanVien.Text = manv;

            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                int thoigiansudung;

                // Kiểm tra các trường bắt buộc
                if (string.IsNullOrWhiteSpace(txtMaKhachHang.Text) ||
                    string.IsNullOrWhiteSpace(txtTaiKhoan.Text) ||
                    string.IsNullOrWhiteSpace(txtMatkhau.Text) ||
                    string.IsNullOrWhiteSpace(txtThoiGianSuDung.Text) ||
                    string.IsNullOrWhiteSpace(cbMaNhanVien.Text) ||
                    !int.TryParse(txtThoiGianSuDung.Text, out thoigiansudung)
                    )

                {
                    MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng thông tin.");
                    return;
                }

                //San pham tu tang KH + 000
                int customerCount = context.KhachHang.Count();
                string newCustomerID = $"SP{(customerCount + 1).ToString("D3")}";

                // Tạo đối tượng khách hàng mới
                KhachHang newKH = new KhachHang
                {
                    MaKhachHang = newCustomerID,
                    TaiKhoan = txtTaiKhoan.Text,
                    MatKhau = txtMatkhau.Text,
                    SoGioSuDung = int.Parse(txtThoiGianSuDung.Text),
                    MaNhanVien = cbMaNhanVien.SelectedValue.ToString()
                };

                try
                {
                    context.KhachHang.Add(newKH);
                    context.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                    LoadData();

                    Reset();
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgKhachHang.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dtgKhachHang.SelectedRows[0].Index;
                string MaSelected = dtgKhachHang.Rows[selectedRowIndex].Cells["Masp"].Value.ToString();

                using (var context = new QUANLYQUANNETContext())
                {
                    int thoiGianSuDung;

                    KhachHang SuaKH = context.KhachHang.FirstOrDefault(c => c.MaKhachHang == MaSelected);
                    if (SuaKH == null)
                    {
                        MessageBox.Show("Mã sản phẩm không tồn tại");
                        return;
                    }

                    // Kiểm tra các trường thông tin bắt buộc
                    if (
                        string.IsNullOrWhiteSpace(txtMatkhau.Text) ||
                        string.IsNullOrWhiteSpace(txtTaiKhoan.Text) ||
                        string.IsNullOrWhiteSpace(txtThoiGianSuDung.Text) ||
                        !int.TryParse(txtThoiGianSuDung.Text, out thoiGianSuDung)
                        )

                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng thông tin.");
                        return;
                    }
                    // Cập nhật thông tin sản phẩm
                    SuaKH.TaiKhoan = txtTaiKhoan.Text;
                    SuaKH.MatKhau = txtMatkhau.Text;
                    SuaKH.SoGioSuDung = int.Parse(txtThoiGianSuDung.Text);
                    Reset();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần cập nhật");
            }

        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.
                Show("Bạn chắc chắn muốn xóa ?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                if (dtgKhachHang.SelectedRows.Count > 0)
                {
                    string id = dtgKhachHang.SelectedRows[0].Cells["MaKhachHang"].Value.ToString();

                    using (var context = new QUANLYQUANNETContext())
                    {
                        KhachHang DeleteKH = context.KhachHang.FirstOrDefault(c => c.MaKhachHang == id);

                        if (DeleteKH != null)
                        {
                            context.KhachHang.Remove(DeleteKH);
                            context.SaveChanges();
                            MessageBox.Show("Xóa thành công");
                            LoadData();
                            Reset();
                        }
                    }
                }
            }
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                string CTimKiem = txtTimKiem.Text.Trim();

                var query = from kh in context.KhachHang
                            join nv in context.NhanVien on kh.MaNhanVien equals nv.MaNhanVien
                            where kh.MaKhachHang.Contains(CTimKiem) ||
                                  kh.TaiKhoan.Contains(CTimKiem) ||
                                  kh.MatKhau.ToString().Contains(CTimKiem) ||
                                  kh.SoGioSuDung.ToString().Contains(CTimKiem) ||
                                  nv.MaNhanVien.Contains(CTimKiem)
                            select new
                            {
                                kh.MaKhachHang,
                                kh.TaiKhoan,
                                kh.MatKhau,
                                kh.SoGioSuDung,
                                nv.MaNhanVien,
                            };

                dtgKhachHang.DataSource = query.ToList();
                Reset();
            }
        }

    }

}







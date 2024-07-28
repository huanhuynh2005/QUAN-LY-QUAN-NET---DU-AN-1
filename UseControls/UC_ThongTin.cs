using Org.BouncyCastle.Asn1.X500;
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
using UIDuAn1.Models;

namespace UIDuAn1
{
    public partial class UC_ThongTin : UserControl
    {
        public UC_ThongTin()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            using (var context = new QUANLYQUANNETContext())
            {
                var query = from NhanVien in context.NhanVien
                            select new
                            {
                                NhanVien.MaNhanVien,
                                NhanVien.HoTen,
                                NhanVien.Gmail,
                                NhanVien.DiaChi,
                                NhanVien.TenVaiTro,
                                NhanVien.TrangThai
                            };

                dtgThongTinNV.DataSource = query.ToList();

                dtgThongTinNV.Columns[0].HeaderText = "MaNhanVien";
                dtgThongTinNV.Columns[1].HeaderText = "HoTen";
                dtgThongTinNV.Columns[2].HeaderText = "Email";
                dtgThongTinNV.Columns[3].HeaderText = "DiaChi";
                dtgThongTinNV.Columns[4].HeaderText = "VaiTro";
                dtgThongTinNV.Columns[5].HeaderText = "TrangThai";
            }
        }

        private void UC_ThongTin_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void Reset()
        {
            txtHoVaTen.Clear();
            txtEmail.Clear();
            txtMaNV.Clear();
            txtDiaChi.Clear();
            txtTimKiem.Clear();
            rdoHoatDong.Checked = false;
            rdoKhongHoatDong.Checked = false;
            rdoAdmin.Checked = false;
            rdoNhanVien.Checked = false;
            rdoTruongCa.Checked = false;
        }

        private void dtgThongTinNV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectRow = dtgThongTinNV.Rows[e.RowIndex];

                string manv = selectRow.Cells["MaNhanVien"].Value.ToString();
                string hoten = selectRow.Cells["HoTen"].Value.ToString();
                string gmail = selectRow.Cells["Gmail"].Value.ToString();
                string diachi = selectRow.Cells["DiaChi"].Value.ToString();
                string tenvaitro = selectRow.Cells["TenVaiTro"].Value.ToString();
                string trangthai = selectRow.Cells["TrangThai"].Value.ToString();

                txtMaNV.Text = manv;
                txtEmail.Text = gmail;
                txtDiaChi.Text = diachi;
                txtHoVaTen.Text = hoten;
                // Thiết lập trạng thái của radio button dựa trên giá trị từ DataGridView
                if (tenvaitro == "1")
                {
                    rdoAdmin.Checked = true;
                }
                else if (tenvaitro == "2")
                {
                    rdoTruongCa.Checked = true;
                }
                else if (tenvaitro == "3")
                {
                    rdoNhanVien.Checked = true;
                }

                if (trangthai == "1")
                {
                    rdoHoatDong.Checked = true;
                }
                else if (trangthai == "2")
                {
                    rdoKhongHoatDong.Checked = true;
                }
            }

        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private string GenerateNewEmployeeID(QUANLYQUANNETContext context)
        {
            // Truy xuất tất cả các `mNV` từ cơ sở dữ liệu
            var allIds = context.NhanVien
                                .Select(nv => nv.MaNhanVien)
                                .ToList();

            // Thực hiện xử lý trên phía client
            var maxID = allIds
                        .Select(id => Regex.Match(id, @"\d+").Value)
                        .Where(x => !string.IsNullOrEmpty(x))
                        .Select(int.Parse)
                        .DefaultIfEmpty(0)
                        .Max();

            return $"NV{(maxID + 1).ToString("D3")}";
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                // Tạo mã nhân viên mới
                string newCustomerID = GenerateNewEmployeeID(context);

                // Kiểm tra các trường bắt buộc
                string tennv = txtHoVaTen.Text;
                if (string.IsNullOrWhiteSpace(txtHoVaTen.Text) ||
                    string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    !Regex.IsMatch(tennv, "^[a-zA-ZÀ-ỹ ]+$"))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng thông tin.");
                    return;
                }

                // Kiểm tra rdo
                if (!rdoHoatDong.Checked && !rdoKhongHoatDong.Checked)
                {
                    MessageBox.Show("Vui lòng chọn tình trạng.");
                    return;
                }
                if (!rdoNhanVien.Checked && !rdoAdmin.Checked && !rdoTruongCa.Checked)
                {
                    MessageBox.Show("Vui lòng chọn vai trò.");
                    return;
                }

                // Tạo đối tượng nhân viên mới
                NhanVien newNV = new NhanVien
                {
                    MaNhanVien = newCustomerID,
                    HoTen = txtHoVaTen.Text,
                    Gmail = txtEmail.Text,
                    DiaChi = txtDiaChi.Text,
                    //TrangThai = rdoTruongCa.Checked ? Convert.ToByte(1) : Convert.ToByte(2),
                    //TenVaiTro = rdoKhongHoatDong.Checked ? Convert.ToByte(1) : Convert.ToByte(2),

                };
                try
                {

                    // Thêm nhân viên vào cơ sở dữ liệu sau khi gửi email thành công
                    context.NhanVien.Add(newNV);
                    context.SaveChanges();

                    // Thêm nhân viên mới vào DataGridView
                    var newEmployee = new
                    {
                        newNV.MaNhanVien,
                        newNV.HoTen,
                        newNV.Gmail,
                        newNV.DiaChi,
                        newNV.TenVaiTro,
                        newNV.TrangThai
                    };
                    dtgThongTinNV.Rows.Add(newEmployee);
                    Reset();
                    LoadData();
                }
                catch (Exception)
                {
                    LoadData();
                }
            }



        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgThongTinNV.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dtgThongTinNV.SelectedRows[0].Index;
                string MaSelected = dtgThongTinNV.Rows[selectedRowIndex].Cells["MaNhanVien"].Value.ToString();

                using (var context = new QUANLYQUANNETContext())
                {
                    string tennv = txtHoVaTen.Text;
                    NhanVien SuaNV = context.NhanVien.FirstOrDefault(c => c.MaNhanVien == MaSelected);
                    if (SuaNV == null)
                    {
                        MessageBox.Show("Mã nhân viên không tồn tại");
                        return;
                    }

                    // Kiểm tra các trường thông tin bắt buộc
                    if (string.IsNullOrWhiteSpace(txtHoVaTen.Text) ||
                        string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                        string.IsNullOrWhiteSpace(txtEmail.Text) ||
                        !Regex.IsMatch(tennv, "^[a-zA-ZÀ-ỹ ]+$"))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng thông tin.");
                        return;
                    }

                    if (SuaNV != null)
                    {
                        SuaNV.Gmail = txtEmail.Text;
                        SuaNV.HoTen = txtHoVaTen.Text;
                        SuaNV.DiaChi = txtDiaChi.Text;
                        // SuaNV.TrangThai = rdoHoatDong.Checked ? Convert.ToByte(1) : Convert.ToByte(2);
                        // SuaNV.TenVaiTro = rdoNhanVien.Checked ? Convert.ToByte(1) : Convert.ToByte(2) ;
                        context.SaveChanges();
                        MessageBox.Show("Cập nhật thành công");
                        LoadData();
                        Reset();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần cập nhật");
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (dtgThongTinNV.SelectedRows.Count > 0)
                {
                    string id = dtgThongTinNV.SelectedRows[0].Cells["Manv"].Value.ToString();

                    using (var context = new QUANLYQUANNETContext())
                    {
                        NhanVien DeleteNV = context.NhanVien.FirstOrDefault(c => c.MaNhanVien == id);

                        if (DeleteNV != null)
                        {
                            try
                            {
                                context.NhanVien.Remove(DeleteNV);
                                context.SaveChanges();
                                MessageBox.Show("Xóa thành công");
                                LoadData();
                                Reset();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Không thể xóa nhân viên này vì còn liên kết với dữ liệu khác (sản phẩm, khách hàng)");
                            }
                        }
                    }
                }
            }

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (var context = new QUANLYQUANNETContext())
            {
                string CTimKiem = txtTimKiem.Text.Trim();

                var query = from NV in context.NhanVien
                            where NV.MaNhanVien.Contains(CTimKiem) ||
                                  NV.HoTen.Contains(CTimKiem) ||
                                  NV.Gmail.Contains(CTimKiem) ||
                                  NV.DiaChi.Contains(CTimKiem) ||
                                  NV.TenVaiTro.ToString().Contains(CTimKiem) ||
                                  NV.TrangThai.ToString().Contains(CTimKiem)
                            select new
                            {
                                NV.MaNhanVien,
                                NV.HoTen,
                                NV.Gmail,
                                NV.DiaChi,
                                NV.TenVaiTro,
                                NV.TrangThai
                            };

                dtgThongTinNV.DataSource = query.ToList();
                Reset();
            }

        }
    }
}


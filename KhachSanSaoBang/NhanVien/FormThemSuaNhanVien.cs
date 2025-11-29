using KhachSanSaoBang.Models;
using KhachSanSaoBang.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang.NhanVien
{
    public partial class FormThemSuaNhanVien : Form
    {
        Models.XulyNV xl_nv = new Models.XulyNV();
        private int currentNhanVienId;
        private bool isEditMode = false;

        public FormThemSuaNhanVien(int maNV)
        {
            InitializeComponent();
            this.currentNhanVienId = maNV;
            if (currentNhanVienId != 0) isEditMode = true;

            this.Load += FormThemSuaNhanVien_Load;
            this.btn_Luu.Click += Btn_Luu_Click;
            this.btn_Huy.Click += Btn_Huy_Click;
        }

        private void FormThemSuaNhanVien_Load(object sender, EventArgs e)
        {
            // Khởi tạo các ComboBox
            KhoiTaoComboBox();

            if (isEditMode)
            {
                this.Text = "Sửa thông tin Nhân viên";
                txt_MSNV.ReadOnly = true; // Không cho sửa khóa chính

                // Lấy thông tin cũ
                tblNhanVien nv = xl_nv.LayThongTinNhanVien(currentNhanVienId);
                if (nv != null)
                {
                    txt_MSNV.Text = nv.ma_nv.ToString();
                    txt_HoTen.Text = nv.ho_ten;
                    cbo_GioiTinh.Text = nv.gioi_tinh;
                    txt_QueQuan.Text = nv.que_quan;
                    cbo_NamBD.Text = nv.nam_bd.ToString();
                    txt_Luong.Text = nv.luong.ToString();
                    txt_DiaChi.Text = nv.dia_chi;
                    txt_sdt.Text = nv.sdt;

                    if (nv.ngay_sinh.HasValue)
                    {
                        cbo_NgaySinh.Text = nv.ngay_sinh.Value.Day.ToString();
                        cbo_ThangSinh.Text = nv.ngay_sinh.Value.Month.ToString();
                        cbo_NamSinh.Text = nv.ngay_sinh.Value.Year.ToString();
                    }
                }
            }
            else
            {
                this.Text = "Thêm Nhân viên mới";
                cbo_GioiTinh.SelectedIndex = 0; // Chọn mặc định
            }
        }

        private void Btn_Luu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra dữ liệu
            if (!int.TryParse(txt_MSNV.Text, out int msnv))
            {
                MessageBox.Show("Mã nhân viên phải là số!");
                return;
            }
            if (!int.TryParse(txt_Luong.Text, out int luong))
            {
                MessageBox.Show("Lương phải là số!");
                return;
            }

            // 2. Tạo đối tượng nhân viên
            tblNhanVien nv = new tblNhanVien();
            nv.ho_ten = txt_HoTen.Text;
            nv.gioi_tinh = cbo_GioiTinh.Text;
            nv.que_quan = txt_QueQuan.Text;
            nv.luong = luong;
            nv.dia_chi = txt_DiaChi.Text;
            nv.sdt = txt_sdt.Text;
            if (int.TryParse(cbo_NamBD.Text, out int namBD)) nv.nam_bd = namBD;

            // Xử lý ngày sinh
            try
            {
                int d = int.Parse(cbo_NgaySinh.Text);
                int m = int.Parse(cbo_ThangSinh.Text);
                int y = int.Parse(cbo_NamSinh.Text);
                nv.ngay_sinh = new DateTime(y, m, d);
            }
            catch
            {
                MessageBox.Show("Ngày sinh không hợp lệ!");
                return;
            }

            bool ketQua = false;

            // 3. Gọi hàm xử lý
            if (isEditMode)
            {
                nv.ma_nv = currentNhanVienId; // ID để tìm và sửa
                ketQua = xl_nv.SuaNhanVien(nv);
            }
            else
            {
                nv.ma_nv = msnv; // ID mới để thêm
                ketQua = xl_nv.ThemNhanVien(nv);
            }

            // 4. Thông báo và đóng form
            if (ketQua)
            {
                string msg = isEditMode ? "Sửa thành công!" : "Thêm thành công!";
                MessageBox.Show(msg);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                string msg = isEditMode ? "Sửa thất bại!" : "Thêm thất bại (có thể trùng mã NV)!";
                MessageBox.Show(msg);
            }
        }

        private void Btn_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void KhoiTaoComboBox()
        {
            for (int i = 1; i <= 31; i++) cbo_NgaySinh.Items.Add(i);
            for (int i = 1; i <= 12; i++) cbo_ThangSinh.Items.Add(i);
            for (int i = DateTime.Now.Year - 60; i <= DateTime.Now.Year; i++) cbo_NamSinh.Items.Add(i);
            for (int i = 2000; i <= DateTime.Now.Year; i++) cbo_NamBD.Items.Add(i);
            cbo_GioiTinh.Items.AddRange(new string[] { "Nam", "Nữ" });
        }
    }
}
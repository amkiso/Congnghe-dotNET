using KhachSanSaoBang.Models.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace KhachSanSaoBang.NhanVien
{
    public partial class FormThemSuaNhanVien : Form
    {
        XuLyNhanVien xl_nv = new XuLyNhanVien();

        private int currentNhanVienId;
        private bool isEditMode;

        public FormThemSuaNhanVien(int maNV)
        {
            InitializeComponent();

            currentNhanVienId = maNV;
            isEditMode = maNV != 0;

            this.Load += FormThemSuaNhanVien_Load;
            btn_Luu.Click += Btn_Luu_Click;
            btn_Huy.Click += Btn_Huy_Click;
        }

        // =====================================================
        // LOAD FORM
        // =====================================================
        private void FormThemSuaNhanVien_Load(object sender, EventArgs e)
        {
            KhoiTaoComboBox();

            if (isEditMode)
            {
                this.Text = "Sửa thông tin Nhân viên";

                lbl_MSNV.Visible = true;
                txt_MSNV.Visible = true;
                txt_MSNV.ReadOnly = true;

                DataRow nv = xl_nv.LayThongTinNhanVien(currentNhanVienId);
                if (nv == null)
                {
                    MessageBox.Show("Không tìm thấy nhân viên!");
                    this.Close();
                    return;
                }

                txt_MSNV.Text = nv["ma_nv"].ToString();
                txt_HoTen.Text = nv["ho_ten"].ToString();
                cbo_GioiTinh.Text = nv["gioi_tinh"].ToString();
                txt_QueQuan.Text = nv["que_quan"].ToString();
                txt_DiaChi.Text = nv["dia_chi"].ToString();
                txt_sdt.Text = nv["sdt"].ToString();
                txt_Luong.Text = nv["luong"].ToString();
                cbo_NamBD.Text = nv["nam_bd"].ToString();

                if (nv["ngay_sinh"] != DBNull.Value)
                {
                    DateTime ns = (DateTime)nv["ngay_sinh"];
                    cbo_NgaySinh.Text = ns.Day.ToString();
                    cbo_ThangSinh.Text = ns.Month.ToString();
                    cbo_NamSinh.Text = ns.Year.ToString();
                }
            }
            else
            {
                this.Text = "Thêm Nhân viên mới";

                // 👉 KHÔNG CHO NHẬP MÃ NV KHI THÊM
                lbl_MSNV.Visible = false;
                txt_MSNV.Visible = false;

                cbo_GioiTinh.SelectedIndex = 0;
            }
        }

        // =====================================================
        // LƯU
        // =====================================================
        private void Btn_Luu_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txt_Luong.Text, out int luong))
            {
                MessageBox.Show("Lương phải là số!");
                return;
            }

            tblNhanVien nv = new tblNhanVien
            {
                ho_ten = txt_HoTen.Text.Trim(),
                gioi_tinh = cbo_GioiTinh.Text,
                que_quan = txt_QueQuan.Text.Trim(),
                dia_chi = txt_DiaChi.Text.Trim(),
                sdt = txt_sdt.Text.Trim(),
                luong = luong
            };

            if (int.TryParse(cbo_NamBD.Text, out int namBD))
                nv.nam_bd = namBD;

            // Ngày sinh
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

            bool ketQua;

            if (isEditMode)
            {
                nv.ma_nv = currentNhanVienId; // 👉 CHỈ GÁN KHI SỬA
                ketQua = xl_nv.SuaNhanVien(nv);
            }
            else
            {
                ketQua = xl_nv.ThemNhanVien(nv); // 👉 KHÔNG GÁN ma_nv
            }

            if (ketQua)
            {
                MessageBox.Show(isEditMode ? "Sửa thành công!" : "Thêm thành công!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Lưu thất bại!");
            }
        }

        // =====================================================
        // HỦY
        // =====================================================
        private void Btn_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // =====================================================
        // KHỞI TẠO COMBOBOX
        // =====================================================
        private void KhoiTaoComboBox()
        {
            cbo_NgaySinh.Items.Clear();
            cbo_ThangSinh.Items.Clear();
            cbo_NamSinh.Items.Clear();
            cbo_NamBD.Items.Clear();
            cbo_GioiTinh.Items.Clear();

            for (int i = 1; i <= 31; i++) cbo_NgaySinh.Items.Add(i);
            for (int i = 1; i <= 12; i++) cbo_ThangSinh.Items.Add(i);
            for (int i = DateTime.Now.Year - 60; i <= DateTime.Now.Year; i++) cbo_NamSinh.Items.Add(i);
            for (int i = 2000; i <= DateTime.Now.Year; i++) cbo_NamBD.Items.Add(i);

            cbo_GioiTinh.Items.AddRange(new string[] { "Nam", "Nữ" });
        }
    }
}

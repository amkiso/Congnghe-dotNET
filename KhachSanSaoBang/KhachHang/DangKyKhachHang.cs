using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang.KhachHang
{
    public partial class DangKyKhachHang : Form
    {
        DanhSachKhachHang dsKH = new DanhSachKhachHang();
        string maKhTimDuoc = null;
        public DangKyKhachHang()
        {
            InitializeComponent();
            this.Load += UcKhachHang_Load;
            this.btnTim.Click += BtnTim_Click;
            this.btnDangKy.Click += BtnDangKy_Click;
        }

        private void BtnTim_Click(object sender, EventArgs e)
        {
            string sdt = txtSearchSDT.Text.Trim();

            if (sdt == "")
            {
                MessageBox.Show("Nhập số điện thoại để tìm khách!", "Thông báo");
                return;
            }

            DataRow row = dsKH.TimKhachTheoSDT(sdt);

            if (row == null)
            {
                MessageBox.Show("Không tìm thấy khách đã đặt phòng!", "Thông báo");
                return;
            }

            // Lấy mã khách hàng
            maKhTimDuoc = row["ma_kh"].ToString();

            // Fill dữ liệu
            txtTen.Text = row["ho_ten"].ToString();
            txtCMT.Text = row["cmt"].ToString();
            txtEmail.Text = row["mail"].ToString();
            dtNS.Value = row["ngsinh"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["ngsinh"]);
        }

        // =====================================================
        // ĐĂNG KÝ THÀNH VIÊN
        // =====================================================
        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            if (maKhTimDuoc == null)
            {
                MessageBox.Show("Hãy tìm khách hàng trước!", "Lỗi");
                return;
            }

            // Lấy khách hàng từ DB
            var kh = dsKH.GetKhachByMa(maKhTimDuoc);

            if (kh == null)
            {
                MessageBox.Show("Không tìm thấy khách!", "Lỗi");
                return;
            }

            // Nếu KH đã là thành viên → thông báo
            if (kh.Field<bool>("member") == true)
            {
                MessageBox.Show("Khách hàng này đã là thành viên!", "Thông báo");
                return;
            }

            // Gọi phương thức đăng ký thành viên
            if (dsKH.DangKyThanhVien(maKhTimDuoc))
            {
                MessageBox.Show("Đăng ký thành viên thành công!");
                RefreshData();
            }
            else
            {
                MessageBox.Show("Lỗi hệ thống, không đăng ký được!", "Lỗi");
            }
        }

        private void UcKhachHang_Load(object sender, EventArgs e)
        {
            dgvTV.DataSource = dsKH.LoadThanhVien();
        }

        public void RefreshData()
        {
            dgvTV.DataSource = dsKH.LoadThanhVien();
        }
    }
}

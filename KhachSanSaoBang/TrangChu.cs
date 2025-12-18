using KhachSanSaoBang.DoanhThu;
using KhachSanSaoBang.KhachHang;
using KhachSanSaoBang.Models;
using KhachSanSaoBang.NhanVien;
using KhachSanSaoBang.ThongKe.BaoCao;
using System;
using System.Windows.Forms;

namespace KhachSanSaoBang
{
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();

            // =============== GÁN SỰ KIỆN MENU CHÍNH ===============
            btnKhachSan.Click += btnKhachSan_Click;
            btnDichVu.Click += btnDichVu_Click;
            btnKhachHang.Click += BtnKhachHang_Click;
            btnDoanhThu.Click += btnDoanhThu_Click;
            btnNhanVien.Click += BtnNhanVien_Click;

            // =============== GÁN SỰ KIỆN SUBMENU ===============
            btnQLNhanVien.Click += BtnQLNhanVien_Click;
            btnDangKiThanhVien.Click += BtnDangKiThanhVien_Click;
            btnQLDoanhThu.Click += BtnQLDoanhThu_Click1;
            btnBaoCao.Click += BtnBaoCao_Click;
            btnDangKiKhachHang.Click += Btn_dangkyKH_Click;
            btnDatPhong.Click += Btn_datphong_Click;
            btnTraCuuKhachHang.Click += Btn_TracuuKhachHang_Click;
            btnQLPhong.Click += btnDoiKhuVuc_Click;
            btn_quanlytang.Click += Btn_quanlytang_Click;
            btn_quanlyphong.Click += Btn_quanlyphong_Click;
            btnQLDichVu.Click += BtnQLDichVu_Click;
            btnQLTienIch.Click += BtnQLTienIch_Click;
        }

        private void BtnQLTienIch_Click(object sender, EventArgs e)
        {
            QuanLyTienIch tienIch = new QuanLyTienIch();
            this.Hide();
            tienIch.ShowDialog();
            this.Close();
        }

        private void BtnQLDichVu_Click(object sender, EventArgs e)
        {
           QuanLyDichVu dichvu = new QuanLyDichVu();
            dichvu.ShowDialog();
            this.Close();
        }

        private void Btn_quanlyphong_Click(object sender, EventArgs e)
        {
            QuanLyPhong phong = new QuanLyPhong();
            phong.ShowDialog();
            this.Close();
        }

        private void Btn_quanlytang_Click(object sender, EventArgs e)
        {
           QuanLyTang tang = new QuanLyTang();
            tang.ShowDialog();
            this.Close();
        }

        private void BtnNhanVien_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlSub_NhanVien);
        }

        // =============== SUBMENU CLICK ===============
        private void BtnKhachHang_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlSub_KhachHang);
        }

        private void btnKhachSan_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlSub_KhachSan);
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlSub_DichVu);
        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlSub_DoanhThu);
        }


        // =============== LOAD FORM VÀ CONTROL ===============
        private void LoadFormToPanel(Form frm)
        {
            pnl_main.Controls.Clear();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            pnl_main.Controls.Add(frm);
            frm.Show();
        }

        private void LoadControlVaoPanel(Control control)
        {
            pnl_main.Controls.Clear();
            control.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(control);
        }

        // =============== CLICK XỬ LÝ NGHIỆP VỤ (GIỮ NGUYÊN LOGIC) ===============
        private void BtnQLDoanhThu_Click1(object sender, EventArgs e)
        {
            LoadFormToPanel(new QuanLyThongKe());
        }

        private void BtnDangKiThanhVien_Click(object sender, EventArgs e)
        {
            LoadFormToPanel(new DangKyKhachHang());
        }

        private void BtnQLNhanVien_Click(object sender, EventArgs e)
        {
            LoadFormToPanel(new DanhSachNhanVien());
        }


        private void DoiTrang()
        {
            this.Hide();
            TrangLamViec tc = new TrangLamViec();
            tc.ShowDialog();
            this.Close();
        }

        private void Btn_TracuuKhachHang_Click(object sender, EventArgs e)
        {
            AcctionForm tracuu = new AcctionForm(1);
            tracuu.ShowDialog();
        }

        private void Btn_datphong_Click(object sender, EventArgs e)
        {
            int acction = 0;
            AcctionForm datp = new AcctionForm(acction);
            Session.Acction_status = false;
            datp.ShowDialog();

            if (Session.Acction_status == false)
            {
                MessageBox.Show("Bạn đã hủy đặt phòng !", "Thông báo");
            }
        }

        private void Btn_dangkyKH_Click(object sender, EventArgs e)
        {
            int acction = 3;  // đăng ký tài khoản khách hàng
            Session.Acction_status = false;
            AcctionForm dk = new AcctionForm(acction);
            dk.ShowDialog();
        }

        private void BtnBaoCao_Click(object sender, EventArgs e)
        {
            LoadControlVaoPanel(new ucBaoCao());
        }

        // =============== SUBMENU TOGGLE ===============
        public void ToggleSubMenu(Panel submenu)
        {
            submenu.Visible = !submenu.Visible;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDoiKhuVuc_Click(object sender, EventArgs e)
        {
            DialogResult rel = MessageBox.Show("Xác nhận đổi khu vực làm việc ?", "Thông báo", MessageBoxButtons.YesNo);
            if (rel == DialogResult.Yes)
            {
                DoiTrang();
            }
        }
    }
}

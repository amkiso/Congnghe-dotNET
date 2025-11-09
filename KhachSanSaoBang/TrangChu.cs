using KhachSanSaoBang.DoanhThu;
using KhachSanSaoBang.NhanVien;
using KhachSanSaoBang.ThongKe.BaoCao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang
{
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
            this.btn_QLNhanVien.Click += Btn_QLNhanVien_Click;
            this.btnQLKhachHang.Click += BtnQLKhachHang_Click;
            this.btnQLDoanhThu.Click += BtnQLDoanhThu_Click;
            this.btnBaoCao.Click += BtnBaoCao_Click;
        }

        private void BtnBaoCao_Click(object sender, EventArgs e)
        {
            ucBaoCao userControlBC = new ucBaoCao();

            LoadControlVaoPanel(userControlBC);
        }

        private void BtnQLDoanhThu_Click(object sender, EventArgs e)
        {
            ucDoanhThu userControlDT = new ucDoanhThu();

            LoadControlVaoPanel(userControlDT);
        }

        private void BtnQLKhachHang_Click(object sender, EventArgs e)
        {
            ucKhachHang userControlNV = new ucKhachHang();

            LoadControlVaoPanel(userControlNV);
        }

        private void Btn_QLNhanVien_Click(object sender, EventArgs e)
        {
            ucNhanVien userControlNV = new ucNhanVien();

            LoadControlVaoPanel(userControlNV);
        }

        private void LoadControlVaoPanel(Control control)
        {
            // Xóa mọi control đang có trên "sân khấu"
            pnl_main.Controls.Clear();

            // Thêm control mới vào và cho nó lấp đầy "sân khấu"
            control.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(control);
        }
        


        public void ToggleSubMenu(Panel submenu)
        {
            if (!submenu.Visible)
            {
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }
        }

        private void btnKhachSan_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlKhachSanSub);
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlDichVuSub);
        }

        private void pnlKhachhang_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlKhachHangSub);
        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlDoanhThuSub);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlThongKeSub);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlNhanVienSub);
            
           
        }
    }
}

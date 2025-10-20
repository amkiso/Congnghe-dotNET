using KhachSanSaoBang.NhanVien;
using KhachSanSaoBang.ThongKe;
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
        ucNhanVien userControlNV;
        ucHoaDon userControlHD;
        ucBaoCao userControlBC;
        public TrangChu()
        {
            InitializeComponent();
            this.btn_QLNhanVien.Click += Btn_QLNhanVien_Click;
            this.btn_DSHoaDon.Click += Btn_DSHoaDon_Click;
            this.btn_BaoCao.Click += Btn_BaoCao_Click;
        }

        private void Btn_BaoCao_Click(object sender, EventArgs e)
        {
            LoadControlVaoPanel(userControlBC);
        }

        private void Btn_DSHoaDon_Click(object sender, EventArgs e)
        {
            LoadControlVaoPanel(userControlHD);
        }

        private void Btn_QLNhanVien_Click(object sender, EventArgs e)
        {
             userControlNV = new ucNhanVien();
             userControlHD = new ucHoaDon();
            userControlBC = new ucBaoCao();

            userControlNV.Dock = DockStyle.Fill;
            userControlHD.Dock = DockStyle.Fill;
            userControlBC.Dock = DockStyle.Fill;

            // Hiển thị một control mặc định khi chương trình mới chạy (nếu muốn)
            pnl_main.Controls.Add(userControlNV);
            LoadControlVaoPanel(userControlNV);
        }


        private void LoadControlVaoPanel(Control control)
        {
            // Xóa mọi control đang có trên "sân khấu"
            pnl_main.Controls.Clear();

            // Thêm control mới vào và cho nó lấp đầy "sân khấu"
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

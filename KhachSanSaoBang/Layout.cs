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

namespace KhachSanSaoBang
{
    public partial class Layout : UserControl
    {




        private void ToggleSubMenu(Panel submenu)
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
        private void Btn_Toggle_menu_Click(object sender, EventArgs e)
        {
            if (pnl_menu.Visible) { pnl_menu.Visible = false; }
            else pnl_menu.Visible = true;
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
        public Layout()
        {
            InitializeComponent();

            // Toggle menu chính
            this.btn_Toggle_menu.Click += Btn_Toggle_menu_Click;
            lbl_Nhanvien.Text = Session.UserName;
            lbl_chucvu.Text = Session.Role;
            // Các nút con
            btnKhachSan.Click += btnKhachSan_Click;
            btnDichVu.Click += btnDichVu_Click;
            pnlKhachhang.Click += pnlKhachhang_Click;
            btnDoanhThu.Click += btnDoanhThu_Click;
            btnThongKe.Click += btnThongKe_Click;
            btn_nhanvien.Click += button1_Click;
            btn_tracuu.Click += Btn_tracuu_Click;
        }

        private void Btn_tracuu_Click(object sender, EventArgs e)
        {
            TraCuuKhachHang tracuu = new TraCuuKhachHang();
            tracuu.ShowDialog();
        }
    }
}

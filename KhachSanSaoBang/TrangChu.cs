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
            this.Click += TrangChu_Click;
        }

        //tạo 1  form cha 
        public  void TrangChu_Click(object sender, EventArgs e)
        {
            ShowFormNhanVien();

            
        }

        //phương thức public để gọi form Nhân Viên
        public void ShowFormNhanVien()
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.GetType() == typeof(TrangChu))
                {
                    // Thì kích hoạt (focus) nó lên và không làm gì nữa
                    frm.Activate();
                    return; // Thoát khỏi hàm
                }
            }
            TrangChu frmTrangChu = new TrangChu();
            frmTrangChu.MdiParent = this;
            frmTrangChu.Show();
        }

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

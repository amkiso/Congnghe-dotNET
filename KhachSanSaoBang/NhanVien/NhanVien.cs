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
    public partial class NhanVien : Form
    {
        public NhanVien()
        {
            InitializeComponent();
            this.btn_ThemNV.Click += Btn_ThemNV_Click;
        }

        private void Btn_ThemNV_Click(object sender, EventArgs e)
        {
            // 1. Lấy tham chiếu đến Form Cha qua thuộc tính MdiParent
            // Dùng "as MainForm" để ép kiểu nó về đúng loại MainForm của bạn
            TrangChu mainForm = this.MdiParent as TrangChu;

            // 2. Kiểm tra xem có lấy được Form Cha không (để tránh lỗi)
            if (mainForm != null)
            {
                // 3. Gọi phương thức public của Form Cha để mở Form Con B
                mainForm.ShowFormNhanVien();

                // (Tùy chọn) Có thể đóng form hiện tại nếu muốn
                // this.Close(); 
            }
        }
    }
}

using KhachSanSaoBang.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KhachSanSaoBang.Models.Data;

namespace KhachSanSaoBang
{
    public partial class Dangnhap : Form
    { 
        Xuly xl = new Xuly();
        public Dangnhap()
        {
            InitializeComponent();
            btn_dangnhap.Click += Btn_dangnhap_Click;
            lbl_quenmatkhau.Click += Lbl_quenmatkhau_Click;
            lb_Register.Click += Lb_Register_Click;
            
        }

        private void Lb_Register_Click(object sender, EventArgs e)
        {
            Dangky  dk = new Dangky();
            dk.ShowDialog();
            MessageBox.Show("Cảm ơn bạn đã đăng ký ! ");
        }

        private void Lbl_quenmatkhau_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Btn_dangnhap_Click(object sender, EventArgs e)
        {
            tblNhanVien nv =new tblNhanVien();
            nv.tai_khoan = txt_taikhoan.Text;
            nv.mat_khau = txt_matkhau.Text;
            if (!xl.Dangnhap(nv)) { MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Thông báo"); }
            else
            {
                MessageBox.Show("Đăng nhập thành công, Chào mừng "+ Session.UserName+"!\n \t Nhấn OK để bắt đầu làm việc", "Thông báo");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
           

        }

        private void Dangnhap_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(120, 0, 0, 0);
            txt_matkhau.UseSystemPasswordChar = true;
            panel1.Visible = true;
        }
    }
}

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
    public partial class Dangky : Form
    {
        Xuly xl = new Xuly();
        public Dangky()
        {
            InitializeComponent();
            this.Load += Dangky_Load;
            this.lbl_back_to_login.Click += Lbl_back_to_login_Click;
            btn_dangky.Click += Btn_dangky_Click;
        }

        private void Btn_dangky_Click(object sender, EventArgs e)
        {
            string gioitinh;
            if (rbt_nu.Checked) { gioitinh = "Nữ"; }
            else { gioitinh = "Nam"; }
            if (xl.EmailChecking(txt_email.Text))
            {
                if (txt_matkhau.Text == txt_matkhau2.Text)
                {
                    tblNhanVien nv = new tblNhanVien();
                    nv.ho_ten = txt_hoten.Text;
                    nv.tai_khoan = txt_tendangnhap.Text;
                    nv.mat_khau = txt_matkhau.Text;
                    nv.ngay_sinh = dtp_ngsinh.Value;
                    nv.Email = txt_email.Text;
                    nv.gioi_tinh = gioitinh;
                    nv.ma_chuc_vu = 3;//Mặc định sẽ là nhân viên
                    if (xl.DangKy(nv)) { MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo"); Dangnhapf(); } else { MessageBox.Show("Đã có lỗi xảy ra, đăng ký thất bại!", "Lỗi"); }
                }
                else { MessageBox.Show("Mật khẩu xác nhận không chính xác !", "Thông báo"); }
            }
            else MessageBox.Show("Email không hợp lệ!", "Thông báo");

        }

        private void Lbl_back_to_login_Click(object sender, EventArgs e)
        {
            Dangnhapf();
        }
        private void Dangnhapf()
        {
            this.Hide();
            Dangnhap dn = new Dangnhap();
            dn.ShowDialog();
            this.Close();
        }
        private void Dangky_Load(object sender, EventArgs e)
        {
            txt_matkhau.UseSystemPasswordChar = true;
            main_container.BackColor = Color.FromArgb(120, 0, 0, 0);
            main_container.Visible = true;
        }
    }
}

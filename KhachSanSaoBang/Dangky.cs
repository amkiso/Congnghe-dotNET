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

namespace KhachSanSaoBang
{
    public partial class Dangky : Form
    {
        SQLDataDataContext db = new SQLDataDataContext();

        public Dangky()
        {
            InitializeComponent();
            btn_Register.Click += Btn_Register_Click;
            lb_Home.Click += Lb_Home_Click;
        }

        private void Lb_Home_Click(object sender, EventArgs e)
        {
            Dangky dk = new Dangky();
            dk.ShowDialog();
            MessageBox.Show("Cảm ơn bạn đã đăng ký ! ");
        }

        private void Btn_Register_Click(object sender, EventArgs e)
        {
            string ma = txt_TenTK.Text.Trim();
            string mk = txt_MatKhau.Text.Trim();
            string hoten = txt_HoTen.Text.Trim();
            string cmt = txt_SoCCCD.Text.Trim();
            string sdt = txt_SDT.Text.Trim();
            string mail = txt_Email.Text.Trim();
            if (ma == "" || mk == "" || hoten == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            
            var kh = db.tblKhachHangs.FirstOrDefault(x => x.ma_kh == ma);
            if (kh != null)
            {
                MessageBox.Show("Tên tài khoản đã tồn tại!");
                return;
            }

            tblKhachHang newKH = new tblKhachHang
            {
                ma_kh = ma,
                mat_khau = mk,
                ho_ten = hoten,
                cmt = cmt,
                sdt = sdt,
                mail = mail
            };

            db.tblKhachHangs.InsertOnSubmit(newKH);
            db.SubmitChanges();

            MessageBox.Show("Đăng ký thành công!");
            this.Close();
        }

        
       
       
        

       

      
    }
}

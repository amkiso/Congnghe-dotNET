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
    public partial class BieuMauThuePhong : Form
    {
        Thongtinchung tt = new Thongtinchung();
        ThongtinTracuu ttr = new ThongtinTracuu();
        bool dataisload = false;
        Xuly xl = new Xuly();
        bool pstatus = false;
        public BieuMauThuePhong(Thongtinchung phong)
        {
            tt = phong;
            InitializeComponent();
            btn_XacNhan.Click += Btn_XacNhan_Click;
            btn_Huy.Click += Btn_Huy_Click;
            this.Load += BieuMauThuePhong_Load;
            txt_sokhach.KeyPress += Txt_sokhach_KeyPress;
            txt_Cccd.Leave += Txt_Cccd_Leave;
            txt_Sdt.Leave += Txt_Sdt_Leave;
            this.FormClosing += BieuMauThuePhong_FormClosing;
        }

        private void BieuMauThuePhong_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!pstatus)
            {
                DialogResult result = MessageBox.Show(
            "Bạn có chắc muốn thoát không?",
            "Xác nhận thoát",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Hủy việc đóng form
                }
                else
                {
                    Session.golbal_Status = false;
                }
            }
        }

        private void Txt_Sdt_Leave(object sender, EventArgs e)
        {
            if (!dataisload)
            {
                if (xl.ChecKH(txt_Cccd.Text))
                {
                    //Lấy thông tin khách hàng
                    ttr = xl.GetThongtinKH(txt_Cccd.Text);
                    //đổ thông tin khách hàng vào các ô tương ứng
                    txt_diem.Text = ttr.Diem_tich_luy.ToString();
                    txt_hoten.Text = ttr.Hoten;
                    txt_Sdt.Text = ttr.Sdt;
                    DisableText();
                    dataisload = true;
                }
                else
                {
                    dataisload = true;
                    //Hỏi chưa có tài khoản, có muốn tạo mới ?
                }

            }
            else { return; }
        }

        private void Txt_Cccd_Leave(object sender, EventArgs e)
        {
            if (!dataisload) {
            if(xl.ChecKH(txt_Cccd.Text))
                {
                    //Lấy thông tin khách hàng
                    ttr = xl.GetThongtinKH(txt_Cccd.Text);
                    //đổ thông tin khách hàng vào các ô tương ứng
                    txt_diem.Text = ttr.Diem_tich_luy.ToString();
                    txt_hoten.Text = ttr.Hoten;
                    txt_Sdt.Text = ttr.Sdt;
                    DisableText();
                    dataisload = true;
                }
                else
                {
                    dataisload = false;
                    //Hỏi chưa có tài khoản, có muốn tạo mới ?
                }
            
            }
            else { return; }
        }
        private void DisableText()
        {
            txt_Sdt.Enabled =  txt_hoten.Enabled = false;
        }
        private void Txt_sokhach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập ký tự không hợp lệ
            }
        }

        private void Btn_Huy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn hủy đặt phòng ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
                Session.golbal_Status = false;
            }
            else return;
        }

        private void Btn_XacNhan_Click(object sender, EventArgs e)
        {
            tblPhieuDatPhong pdp = new tblPhieuDatPhong();
            List<KhachThue> kt = new List<KhachThue>
            {
                new KhachThue { Tenkh = txt_hoten.Text,  Cccd = txt_Cccd.Text },
                new KhachThue { Tenkh = txt_hoten1.Text, Cccd = txt_cccd_1.Text },
                new KhachThue { Tenkh = txt_hoten2.Text, Cccd = txt_cccd_2.Text }
            };

            try
            {
                pdp.ma_kh = ttr.Makh;
                pdp.ngay_dat = DateTime.Now;
                pdp.ngay_vao = dt_ngayvao.Value;
                pdp.ngay_ra = dt_ngayra.Value;
                pdp.ma_phong = tt.Maphong;
                pdp.thong_tin_khach_thue = xl.ThongtinKH_To_Json(kt);
                pdp.ma_tinh_trang = 1;
                if (xl.CreateNewPhieuDatPhong(pdp)){
                    MessageBox.Show("Tạo phiếu thuê phòng thành công !","Thông báo");
                    Session.golbal_Status = true;
                    pstatus = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi tạo mới phiếu đặt phòng !", "Thông báo");
                    return;
                }
                
            }
            catch (Exception ex) {
                MessageBox.Show("Có lỗi xảy ra khi tạo mới phiếu đặt phòng !", "Thông báo");
                return;
            }
        }

        private void BieuMauThuePhong_Load(object sender, EventArgs e)
        {
            txt_SoPhong.Text = tt.Tenphong;
            txt_SoTang.Text = tt.Matang.ToString();
            txt_GiaThue.Text = tt.Giathue.ToString();
            txt_LoaiP.Text = tt.Loaiphong;
            txt_SoPhong.Enabled = txt_SoTang.Enabled = txt_GiaThue.Enabled = txt_LoaiP.Enabled =false;
        }

        
    }
}

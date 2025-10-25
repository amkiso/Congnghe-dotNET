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
    public partial class TraCuuKhachHang : Form
    {
        Xuly xl = new Xuly();
        //Hướng hoạt động:
        //Kiểm tra rỗng ( bắt buộc nhập những thông tin cần thiết)
        //Kiểm tra số CCCD (Kiểm tra số CCCD trong csdl xem đã có chưa, chưa có ( lần đầu ) thì tiến hành tạo tài khoản mới
        // Nếu số CCCD đã tồn tại thì thông báo lên màn hình, hỏi xác nhận có muốn dùng thông tin cũ hay không(sau đó nhân viên tại quầy sẽ hỏi ý kiến khách hàng) ?
        public TraCuuKhachHang()
        {
            InitializeComponent();
            btn_Huy.Click += Btn_Huy_Click;
            btn_XacNhan.Click += Btn_XacNhan_Click;
        }

        private void Btn_XacNhan_Click(object sender, EventArgs e)
        {
            //Tiến hành gọi phương thức tra cứu
            //nếu giá trị trả về null thì hiện thông báo khách hàng chưa có thông tin
            //Nếu giá trị trả về != null thì hiển thị thông tin khách hàng đã được tra cứu
            if (xl.ChecKH(txt_input.Text))
            {
                string listChoXN, listchk;
                ThongtinTracuu kh = xl.Tracuuthongtin(txt_input.Text);
                if (kh != null)
                {
                    if (kh.Phong_cho_xac_nhan == null) { listChoXN = "0"; }
                    else
                    {
                        listChoXN = string.Join(", ", kh.Phong_cho_xac_nhan);
                    }
                    if (kh.Phong_cho_xac_nhan == null) { listchk = "0"; }
                    else
                    {
                        listchk = string.Join(", ", kh.Phong_dang_dat);
                    }
                    string Thongtin = $"Tên khách hàng:{kh.Hoten}\t " +
                        $"Số điện thoại:{kh.Sdt}\n Số CCCD:{kh.Cccd}\n" +
                        $"Số phòng đã đặt:{kh.So_phong_da_dat}\n" +
                        $" Số phòng chờ check-in:{kh.So_phong_dang_dat}    Điểm tích lũy:{kh.Diem_tich_luy}\n" +
                        $"Số phòng chưa xác nhận:{kh.So_Phong_chua_Xac_nhan}    Các phòng đợi check-in:{listchk}\n" +
                        $"Các phòng chưa xác nhận:{listChoXN}";
                    MessageBox.Show(Thongtin, "Thông tin");
                    txt_input.Focus();
                }
                else
                {
                    DialogResult rel = MessageBox.Show("Không tìm thấy thông tin khách hàng !, chuyển đến tạo tài khoản và thuê phòng ?", "Thông báo", MessageBoxButtons.YesNo);
                    if (rel == DialogResult.Yes)
                    {
                    }
                    else { this.Close(); }
                }
            }
            else
            {
                DialogResult rel = MessageBox.Show("Không tìm thấy thông tin khách hàng !, chuyển đến tạo tài khoản và thuê phòng ?", "Thông báo", MessageBoxButtons.YesNo);
                if (rel == DialogResult.Yes)
                {
                }
                else { this.Close(); }
            }

        }

        private void Btn_Huy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Hủy tra cứu thông tin khách hàng ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) { this.Close(); }
            else return;
        }
    }
}

using KhachSanSaoBang.ChatBox;
using KhachSanSaoBang.Models;
using KhachSanSaoBang.Models.Client;
using KhachSanSaoBang.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang
{
    public partial class QuanLyPhong : Form
    {
        public Xuly xl = new Xuly();
        int acction = 0; // 1: them, 2: sua, 3: xoa
        private static List<Thongtinchung> DanhsachPhong;
        int counter = 0;

        public QuanLyPhong()
        {
            InitializeComponent();
            this.Load += QuanLyPhong_Load;
            btn_save.Click += Btn_save_Click;
            btn_add.Click += Btn_add_Click;
            btn_change.Click += Btn_change_Click;
            btn_delete.Click += Btn_delete_Click;
            btn_clear_all.Click += Btn_clear_all_Click;
            btn_exit.Click += Btn_exit_Click;
            btn_reload.Click += Btn_reload_Click;
            timer1.Tick += Timer1_Tick;
            //Menu
            btn_datphong.Click += Btn_datphong_Click;
            btn_tracuukhachhang.Click += Btn_tracuukhachhang_Click;
            btn_dangkykhachhang.Click += Btn_dangkykhachhang_Click;
            btn_thongke.Click += DoiKhuVucLamViec;
            btn_dangkythanhvien.Click += DoiKhuVucLamViec;
            btn_danhsachhoadon.Click += DoiKhuVucLamViec;
            btn_chatbot.Click += Btn_chatbot_Click;
            btn_quanlydv.Click += Btn_quanlydv_Click;
            btn_quanlytienich.Click += Btn_quanlytienich_Click;
            btn_quanlytang.Click += Btn_quanlytang_Click;
            btn_Trangchu.Click += Btn_Trangchu_Click;
            btn_quanlyloaiphong.Click += Btn_quanlyloaiphong_Click;
        }
        private void Btn_quanlyloaiphong_Click(object sender, EventArgs e)
        {
            if (Session.Roleid != 1 && Session.Roleid != 2)
            {
                MessageBox.Show("Bạn không có quyền thức hiện thao tác này !", "Thông báo");
                return;
            }
            this.Hide();
            QuanLyLoaiPhong tienIch = new QuanLyLoaiPhong();
            tienIch.ShowDialog();
            this.Close();
        }
        private void Btn_Trangchu_Click(object sender, EventArgs e)
        {
            TrangLamViec tlv = new TrangLamViec();
            this.Hide();
            tlv.ShowDialog();
            this.Close();
        }
        private void Btn_datphong_Click(object sender, EventArgs e)
        {
            int acction = 0;
            AcctionForm datp = new AcctionForm(acction);
            Session.Acction_status = false;
            datp.ShowDialog();
            LoadData();
            if (Session.Acction_status == false)
            {
                MessageBox.Show("Bạn đã hủy đặt phòng !", "Thông báo");
            }
        }
        private void Btn_quanlytang_Click(object sender, EventArgs e)
        {
            if (Session.Roleid != 1 && Session.Roleid != 2)
            {
                MessageBox.Show("Bạn không có quyền thức hiện thao tác này !", "Thông báo");
                return;
            }
            this.Hide();
            QuanLyTang tienIch = new QuanLyTang();
            tienIch.ShowDialog();
            this.Close();
        }

        private void Btn_quanlytienich_Click(object sender, EventArgs e)
        {
            if (Session.Roleid != 1 && Session.Roleid != 2)
            {
                MessageBox.Show("Bạn không có quyền thức hiện thao tác này !", "Thông báo");
                return;
            }
            this.Hide();
            QuanLyTienIch tienIch = new QuanLyTienIch();
            tienIch.ShowDialog();
            this.Close();

        }

        private void Btn_quanlydv_Click(object sender, EventArgs e)
        {
            if (Session.Roleid != 1 && Session.Roleid != 2)
            {
                MessageBox.Show("Bạn không có quyền thức hiện thao tác này !", "Thông báo");
                return;
            }
            this.Hide();
            QuanLyDichVu qlp = new QuanLyDichVu
                ();
            qlp.ShowDialog();
            this.Close();
        }


        private void Btn_chatbot_Click(object sender, EventArgs e)
        {
            this.Hide();
            Chat tc = new Chat();
            tc.ShowDialog();
            this.Close();
        }

        private void DoiKhuVucLamViec(object sender, EventArgs e)
        {
            DialogResult rel = MessageBox.Show("Để thực hiện chức năng này cần phải đổi khu vực làm việc, Đồng ý ?", "Thông báo", MessageBoxButtons.YesNo);
            if (rel == DialogResult.Yes)
            {
                DoiTrang();
            }
            else return;
        }
        private void DoiTrang()
        {
            this.Hide();
            TrangChu tc = new TrangChu();
            tc.ShowDialog();
            this.Close();
        }
        private void Btn_tracuukhachhang_Click(object sender, EventArgs e)
        {
            AcctionForm tracuu = new AcctionForm(1);
            tracuu.ShowDialog();
        }

        private void Btn_dangkykhachhang_Click(object sender, EventArgs e)
        {
            int acction = 3;//đăng ký tài khoản khách hàng
            Session.Acction_status = false;
            AcctionForm dk = new AcctionForm(acction);
            dk.ShowDialog();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            lbl_timer.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            if (cb_lammoi5s.Checked)
            {
                counter++;

                // 5 giây (vì timer tick mỗi 0.5 giây)
                if (counter >= 50)
                {
                    counter = 0; // reset
                    LoadData();
                }
            }
            else
            {
                // Nếu tắt checkbox thì reset
                counter = 0;
            }

        }
        
        private void Btn_reload_Click(object sender, EventArgs e)
        {
            Btn_clear_all_Click(sender, e);
            LoadData();
        }

        private void Btn_clear_all_Click(object sender, EventArgs e)
        {
            Session.maphonghientai = 0;
            Session.tenphonghientai = "Chưa chọn phòng";
            lbl_phonghientai.Text = "Chưa chọn phòng";
            cbo_tinhtrang.SelectedIndex = 0;
            cbo_tang.SelectedIndex = 0;
            cbo_loaiphong.SelectedIndex = 0;
            txt_tenphong.Clear();
            txt_sohoadon.Clear();
            txt_sophieudat.Clear();
        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
                return;
            else
            {
                var phongh = DanhsachPhong.FirstOrDefault(t => t.Maphong == Session.maphonghientai);
                if (phongh.Sohoadon > 0 || phongh.Sophieudat > 0)
                {
                    MessageBox.Show("Phòng đã có hóa đơn hoặc phiếu đặt, không thể xóa", "Thông báo");

                }
                else
                {
                    if (xl.DeletePhong(Session.maphonghientai))
                    {
                        MessageBox.Show("Xóa phòng thành công", "Thành công");
                    }
                    else
                    {
                        MessageBox.Show("Xóa phòng thất bại", "Thông báo");
                    }
                }
            }
            LoadData();
        }

        private void Btn_change_Click(object sender, EventArgs e)
        {
            acction = 2;
            enableitem();
            btn_save.Enabled = true;
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {


            tblPhong newp = new tblPhong();
            newp.loai_phong = cbo_loaiphong.SelectedIndex;
            newp.ma_tang = cbo_tang.SelectedIndex;
            newp.ma_tinh_trang = 1;
            newp.ma_tienich = cbo_tienich.SelectedIndex;
            newp.so_phong = txt_tenphong.Text;

            disableitem(); ; switch (acction)
                {
                    case 1:
                        
                    if (newp.ma_tang == 0)
                    {
                        MessageBox.Show("Vui lòng chọn tầng cho phòng", "Thông báo");

                    }
                    else if (newp.loai_phong == 0)
                    {
                        MessageBox.Show("Vui lòng chọn loại phòng", "Thông báo");

                    }
                    else if (newp.ma_tienich == 0)
                    {
                        MessageBox.Show("Vui lòng chọn tiện ích cho phòng", "Thông báo");

                    }
                    if (txt_tenphong.Text.Length == 0)
                        {
                            MessageBox.Show("Tên phòng không được trống", "Thông báo");
                            break;
                        }
                        string sophong = txt_tenphong.Text;
                        int count = DanhsachPhong.Where(t => t.Tenphong == sophong).Count();
                        if (count > 0)
                        {
                            MessageBox.Show("Tên phòng đã tồn tại, vui lòng chọn tên khác", "Thông báo");
                            break;
                        }
                        else
                        {
                            if (xl.AddPhong(newp))
                            {
                                MessageBox.Show("Thêm phòng thành công", "Thành công");
                            }
                            else MessageBox.Show("Thêm phòng thất bại", "Thông báo");
                        }
                        break;
                    case 2:
                        newp.ma_phong = Session.maphonghientai;
                        if (xl.ChangeRoomInfomation(newp))
                        {
                            MessageBox.Show("Cập nhật thông tin phòng thành công", "Thành công");
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thông tin phòng thất bại", "Thông báo");
                        }
                        break;

                }
            
            LoadData();
        }

        private void Btn_add_Click(object sender, EventArgs e)
        {
            enableitem();
            btn_save.Enabled = true;
            cbo_loaiphong.SelectedIndex = 0;
            cbo_tinhtrang.Enabled = false;
            cbo_tang.SelectedIndex = 0;
            cbo_tinhtrang.SelectedIndex = 1;
            cbo_tienich.SelectedIndex = 0;
            txt_tenphong.Clear();
            txt_sophieudat.Clear();
            lbl_phonghientai.Text = "đang thêm phòng";
            txt_sohoadon.Clear();
            btn_add.Enabled = false;
            acction = 1;
        }

        private void LoadData()
        {
            var ttlp = xl.getloaiphong();
            tblLoaiPhong nd = new tblLoaiPhong();
            nd.loai_phong = 0;
            nd.gia = 0;
            nd.mo_ta = "Chọn loại phòng";
            ttlp.Insert(0, nd);
            cbo_loaiphong.DataSource = ttlp;
            cbo_loaiphong.DisplayMember = "mo_ta";
            cbo_loaiphong.ValueMember = "loai_phong";
            var tttang = xl.gettang();
            tblTang ndt = new tblTang();
            ndt.ma_tang = 0;
            ndt.ten_tang = "Chọn tầng";
            tttang.Insert(0, ndt);
            cbo_tang.DataSource = tttang;
            cbo_tang.DisplayMember = "ten_tang";
            cbo_tang.ValueMember = "ma_tang";
            List<tblTinhTrangPhong> ttps = xl.tinhtrangp();
            tblTinhTrangPhong nds = new tblTinhTrangPhong();
            nds.ma_tinh_trang = 0;
            nds.mo_ta = "Chọn trạng thái";
            ttps.Insert(0, nds);
            cbo_tinhtrang.DataSource = ttps;
            cbo_tinhtrang.ValueMember = "ma_tinh_trang";
            cbo_tinhtrang.DisplayMember = "mo_ta";
            var tti = xl.gettienich();
            tblTienich ti = new tblTienich();
            ti.ma_tienich = 0;
            ti.ten_tienich = "Chọn tiện ích";
            tti.Insert(0, ti);
            cbo_tienich.DataSource = tti;
            cbo_tienich.DisplayMember = "ten_tienich";
            cbo_tienich.ValueMember = "ma_tienich";
            var dsPhong = xl.GetAllThongtinphong();
            DanhsachPhong = dsPhong;
            LoadPhong(dsPhong);
            cbo_tienich.SelectedIndex = 0;
            cbo_tinhtrang.SelectedIndex = 0;
            cbo_tang.SelectedIndex = 0;
            cbo_loaiphong.SelectedIndex = 0;
            txt_giathue.DataBindings.Clear();
            txt_giathue.DataBindings.Add("Text", cbo_loaiphong.DataSource, "gia");
            


        }
        private void enableitem()
        {
            txt_tenphong.Enabled = true;
            cbo_loaiphong.Enabled = true;
            cbo_tang.Enabled = true;
            cbo_tienich.Enabled = true;
            cbo_tinhtrang.Enabled = true;

        }
        private void disableitem()
        {
            txt_tenphong.Enabled = false;
            cbo_loaiphong.Enabled = false;
            cbo_tang.Enabled = false;
            cbo_tienich.Enabled = false;
            cbo_tinhtrang.Enabled = false;
        }
        private void LoadDuLieuPhong(string maPhong)
        {
            int maphong = int.Parse(maPhong);
            string tenphong = xl.Gettenphong(maphong);
            var thongtin = DanhsachPhong.FirstOrDefault(t => t.Maphong == maphong);
            cbo_tinhtrang.SelectedValue = thongtin.Trangthai;
            cbo_loaiphong.SelectedValue = thongtin.Maloaiphong;
            cbo_tienich.SelectedValue = thongtin.Matienich;
            txt_tenphong.Text = thongtin.Tenphong;
            txt_sohoadon.Text = thongtin.Sohoadon.ToString();
            txt_sophieudat.Text = thongtin.Sophieudat.ToString();
            btn_change.Enabled = true;
            btn_delete.Enabled = true;
            var gannut = xl.GetCauhinhNutDoiTrangthai(thongtin.Trangthai);
            Session.maphonghientai = maphong;
            Session.tenphonghientai = tenphong;
            Session.trangthaiphong = thongtin.Trangthai;
            lbl_phonghientai.Text = $"Phòng {tenphong}";
            cbo_tang.SelectedValue = thongtin.Matang;


        }

        private void LoadPhong(List<Thongtinchung> dsPhong)
        {
            flow_ds_phong.Controls.Clear();


            foreach (var item in dsPhong)
            {
                UCPhong uc = new UCPhong();

                // Gán số thứ tự để nhìn cho rõ
                uc.Tag = item.Maphong;
                uc.TenPhong = $"Phòng {item.Tenphong}";
                uc.BackColor = Dataloader.LayMauTheoTinhTrang(item.Trangthai);
                uc.OnPhongSelect += (maPhong) =>
                {

                    LoadDuLieuPhong(maPhong);
                };

                // Add vào flowlayout
                flow_ds_phong.Controls.Add(uc);
            }
        }
        private void QuanLyPhong_Load(object sender, EventArgs e)
        {
            LoadData();
            ActiveControl = null;
            timer1.Start();
        }
        
        private void Btn_exit_Click(object sender, EventArgs e)
        {
            DialogResult rels = MessageBox.Show("Xác nhận thoát?", "Thông báo", MessageBoxButtons.YesNo);
            if (rels == DialogResult.Yes)
            {
                
                Session.Reset();
                this.Hide();
                Dangnhap dn = new Dangnhap();
                dn.ShowDialog();
                this.Close();
            }
        }
    }
}

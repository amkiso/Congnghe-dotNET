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
using static KhachSanSaoBang.Models.Dataloader;

namespace KhachSanSaoBang
{
    public partial class TrangLamViec : Form
    {
        Xuly xl = new Xuly();
        private static List<Thongtinchung> DanhsachPhong;
        int counter = 0;
        public TrangLamViec()
        {
            InitializeComponent();
            this.timer1.Tick += Timer1_Tick;
            this.Load += Form1_Load;
            btn_clear_all.Click += Btn_clear_all_Click;
            btn_thoatca.Click += Btn_thoatca_Click1;
            btn_tradv.Click += Btn_tradv_Click;
            btn_goidv.Click += Btn_goidv_Click;
            btn_thoatca.Click += Btn_thoatca_Click;
            btn_reload.Click += Btn_reload_Click;
            btn_doitrangthai.Click += Btn_doitrangthai_Click;
            btn_thanhtoan.Click += Btn_thanhtoan_Click;
            //Lọc phòng nhanh bằng nút bấm
            btn_pTrong.Click += Loc_theo_trang_thai;
            btn_pQuaHan.Click += Loc_theo_trang_thai;
            btn_pChoXn.Click += Loc_theo_trang_thai;
            btn_pDangdung.Click += Loc_theo_trang_thai;
            btn_tatcaphong.Click += Loc_theo_trang_thai;
            btn_chocheck.Click += Loc_theo_trang_thai;
            btn_tatcaphong.Click += Loc_theo_trang_thai;
            btn_dangsua.Click += Loc_theo_trang_thai;
            btn_Dungdung.Click += Loc_theo_trang_thai;
            btn_dangdon.Click += Loc_theo_trang_thai;
            //Menu
            btn_datphong.Click += Btn_datphong_Click;
            btn_tracuukhachhang.Click += Btn_tracuukhachhang_Click;
            btn_dangkykhachhang.Click += Btn_dangkykhachhang_Click;
            btn_thongke.Click += DoiKhuVucLamViec;
            btn_dangkythanhvien.Click += DoiKhuVucLamViec;
            btn_danhsachhoadon.Click += DoiKhuVucLamViec;
            btn_chatbot.Click += Btn_chatbot_Click;
            btn_quanlyphong.Click += Btn_quanlyphong_Click;
            btn_quanlydv.Click += Btn_quanlydv_Click;
        }

        private void Btn_quanlydv_Click(object sender, EventArgs e)
        {
            this.Hide();
            QuanLyDichVu qlp = new QuanLyDichVu
                ();
            qlp.ShowDialog();
            this.Close();
        }

        private void Btn_quanlyphong_Click(object sender, EventArgs e)
        {
            this.Hide();
            QuanLyPhong qlp = new QuanLyPhong
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

        private void Btn_thoatca_Click1(object sender, EventArgs e)
        {
            DialogResult rels = MessageBox.Show("Xác nhận thoát ca ?", "Thông báo", MessageBoxButtons.YesNo);
            if (rels == DialogResult.Yes)
            {
                Session.Reset();
                this.Hide();
                Dangnhap dn = new Dangnhap();
                dn.ShowDialog();
                this.Close();
            }
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

        private void Loc_theo_trang_thai(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                int trangthai = Convert.ToInt32(btn.Tag); // lấy giá trị từ Tag
                if (trangthai > 0)
                {
                    var dsLoc = DanhsachPhong.Where(t => t.Trangthai == trangthai).ToList();
                    LoadPhong(dsLoc);
                }
                else if (trangthai == 0) {
                    LoadPhong(DanhsachPhong);
                }
                else {
                    var dsLoc = DanhsachPhong.Where(t => t.Ngayra!= "-" && DateTime.Parse(t.Ngayra) < DateTime.Now).ToList();
                    LoadPhong(dsLoc);
                }
            }
            
        }

        private void Btn_thanhtoan_Click(object sender, EventArgs e)
        {
            if (Session.maphonghientai != 0)
            {
                if (Session.trangthaiphong != 2) { MessageBox.Show("Trạng thái phòng hiện tại không cho phép thanh toán !", "Thông báo"); }
                else
                {
                    DialogResult rls = MessageBox.Show($"Xác nhận chuyển đến trang thanh toán phòng {Session.maphonghientai} ?", "Thông báo", MessageBoxButtons.YesNo);
                    if (rls == DialogResult.Yes)
                    {
                        Session.Acction_status = false;
                        var thanhtoans = xl.GetThongtinThanhToan(Session.maphonghientai);
                        if (thanhtoans == null) { MessageBox.Show("Lấy thông tin thanh toán thất bại", "Lỗi"); return; }
                        else
                        {
                            ThanhToan pm = new ThanhToan(thanhtoans);
                            pm.ShowDialog(this);
                            if (!Session.Acction_status)
                            {
                                MessageBox.Show("Bạn đã hủy thanh toán !", "Thông báo");
                            }
                            else
                            {
                                LoadData();
                            }
                        }
                    }
                    else return;
                }
            }
            else { MessageBox.Show("Bạn chưa chọn phòng để thanh toán!", "Thông báo"); }
        }

        private void Btn_doitrangthai_Click(object sender, EventArgs e)
        {
            int newtrangthai = (int)btn_doitrangthai.Tag;
            int currenttrangthai = cbo_tinhtrang.SelectedIndex;

            if (newtrangthai ==2 && currenttrangthai == 2)//Thanh toán
            {
                DialogResult rls = MessageBox.Show($"Xác nhận chuyển đến trang thanh toán phòng {Session.maphonghientai} ?", "Thông báo", MessageBoxButtons.YesNo);
                if (rls == DialogResult.Yes)
                {
                    Session.Acction_status = false;
                    var thanhtoans = xl.GetThongtinThanhToan(Session.maphonghientai);
                    if (thanhtoans == null) { MessageBox.Show("Lấy thông tin thanh toán thất bại", "Lỗi"); return; }
                    else
                    {
                        ThanhToan pm = new ThanhToan(thanhtoans);
                        pm.ShowDialog(this);
                        if (!Session.Acction_status)
                        {
                            MessageBox.Show("Bạn đã hủy thanh toán !", "Thông báo");
                        }
                        else
                        {
                            LoadPhong(DanhsachPhong);
                            Loadbuttons();
                        }
                    }
                }
                else return;
            }
            else if(newtrangthai ==7 && currenttrangthai == 6) //Xác nhận phòng
            {
                DialogResult rel = MessageBox.Show("Xác nhận nhận phòng khách đặt ?", "Thông báo", MessageBoxButtons.YesNo);
                if (rel == DialogResult.Yes)
                {
                    //Đổi trạng thái phòng, trạng thái phiếu đặt, tạo hóa đơn mới
                    if (xl.XacNhanPhong(Session.maphonghientai))
                    {

                        MessageBox.Show("Đã thay đổi trạng thái phòng thành công !", "Thông báo");
                    }
                    else { MessageBox.Show("Lỗi khi thay đổi trạng thái phòng !", "Thông báo"); }
                }
                else
                {
                    return;
                }
            }
            else if(newtrangthai==2 && currenttrangthai == 7)//Checkin
            {
                Session.Acction_status = false;
                int acction = 2;
                AcctionForm ktc = new AcctionForm(acction);
                ktc.ShowDialog();
                if (Session.Acction_status)
                {
                    string json = ktc.jsonstring;
                    if (xl.AddThongtinKhachHangThuePhong(Session.maphonghientai, json))
                    {
                        MessageBox.Show("Nhập thông tin khách thuê thành công!", "Thông báo");
                        xl.ChangeRoomStatus(Session.maphonghientai, newtrangthai);

                    }
                }
                else
                {
                    MessageBox.Show("Bạn đã hủy nhập thông tin khách thuê !", "Thông báo");
                }
            }
            else if (newtrangthai==6 && currenttrangthai ==1)//Đặt phòng
            {
               DialogResult Rels = MessageBox.Show("Mở cửa sổ đặt phòng ?", "Thông báo", MessageBoxButtons.YesNo);
                if (Rels == DialogResult.Yes)
                    Btn_datphong_Click(sender, e);
                else return;
            }    
            //Dọn phòng xong & sửa chữa xong & sử dụng lại phòng
            else
            {
                DialogResult rels = MessageBox.Show("Xác nhận đổi trạng thái phòng ?", "Thông báo", MessageBoxButtons.YesNo);
                if (rels == DialogResult.No) return;
                else
                {
                    if (xl.ChangeRoomStatus(Session.maphonghientai, newtrangthai))
                    {
                        MessageBox.Show("Đổi trạng thái phòng thành công !", "Thông báo");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Đổi trạng thái phòng thất bại !", "Thông báo");
                    }
                }
            }
            DanhsachPhong = xl.GetAllThongtinphong();
            LoadPhong(DanhsachPhong);
            Loadbuttons();
            Btn_clear_all_Click(sender, e);
        }

        private void Btn_thoatca_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void Btn_goidv_Click(object sender, EventArgs e)
        {
            if (Session.maphonghientai == 0) { MessageBox.Show("Bạn chưa chọn phòng !", "Thông báo"); }
            else
            {
                //Kiểm tra số lượng nhập vào:
                if (nb_sl_goi.Value <= int.Parse(txt_sl_ton_dv.Text))
                {
                    if(nb_sl_goi.Value <= 0)
                    {
                        MessageBox.Show("Số lượng gọi phải lớn hơn 0", "Thông báo");
                        return;
                    }
                    int so = (int)nb_sl_goi.Value;
                    if (xl.GoiDichVu(Session.maphonghientai, Session.Madv,so))
                    {
                        MessageBox.Show("Gọi dịch vụ thành công !", "Thông báo");
                        da_dv_su_dung.DataSource = xl.listdichvup(Session.maphonghientai);
                        dsDichVu.TruSoLuong(Session.Madv, so);
                        LoadDichVu(Session.Madv.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Gọi dịch vụ thất bại do phòng chưa có khách đặt hoặc do lỗi không xác định !", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Số lượng gọi không được vượt quá số lượng tồn", "Thông báo");
                }
            }

        }
        private void Btn_tradv_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn trả hàng ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (Session.Madv == 0)
                {
                    MessageBox.Show("Vui lòng chọn một dịch vụ đã gọi để trả hàng.",
                                    "Chưa chọn dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    int soluongtra = (int)nb_sl_goi.Value;
                    // 2. Kiểm tra số lượng nhập vào
                    if (soluongtra < 1)
                    {
                        MessageBox.Show("Số lượng trả phải là một số nguyên lớn hơn 0.",
                                        "Số lượng không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        nb_sl_goi.Focus();

                        return;
                    }
                    // 4. Xử lý logic nghiệp vụ
                    int soLuongSeTra = soluongtra;
                    bool seTraHang = false;      // Cờ để xác định có thực hiện trả hàng không
                    if (soluongtra > int.Parse(da_dv_su_dung.CurrentRow.Cells["col_SL_dv"].Value.ToString()))
                    {
                        DialogResult Cnp = MessageBox.Show("Số lượng bạn trả nhiều hơn số lượng đã gọi nên thao tác này sẽ trả hết bạn đồng ý chứ ?", "Thông báo", MessageBoxButtons.YesNo);
                        if (Cnp == DialogResult.Yes)
                        {
                            soLuongSeTra = int.Parse(da_dv_su_dung.CurrentRow.Cells["col_SL_dv"].Value.ToString());
                            seTraHang = true;
                        }
                        else
                        {
                            // Hủy trả hàng
                            nb_sl_goi.Focus();
                            return;
                        }
                    }
                    else
                    {
                        soLuongSeTra = (int)nb_sl_goi.Value;
                        seTraHang = true;
                    }
                    // 5. Thực hiện trả hàng nếu cờ seTraHang là true
                    if (seTraHang)
                    {
                        if (xl.Tradichvu(Session.maphonghientai, Session.Madv, soLuongSeTra))
                        {
                            MessageBox.Show("Trả hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Cập nhật CACHE: Cộng trả lại số lượng
                            dsDichVu.TraLaiSoLuong(Session.Madv, soLuongSeTra);

                            // Tải lại grid "dịch vụ đã sử dụng"
                            da_dv_su_dung.DataSource = xl.listdichvup(Session.maphonghientai);

                            // Cập nhật lại số tồn kho đang hiển thị (nếu đang chọn đúng dịch vụ đó)
                            LoadDichVu(Session.Madv.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Trả hàng thất bại. Đã có lỗi xảy ra !.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    // Đặt focus lại để dễ thao tác
                    nb_sl_goi.Focus();
                }
            }
            else return;
        }
        private void Btn_clear_all_Click(object sender, EventArgs e)
        {
            Session.maphonghientai = 0;
            Session.tenphonghientai = "Chưa chọn phòng";
            da_dv_su_dung.DataSource = null;
            lbl_phonghientai.Text = "Chưa chọn phòng";
            txt_gia_dv.Text = txt_sl_ton_dv.Text = txt_tendv.Text = txt_donvi_dv.Text = "";
            nb_sl_goi.Value = 0;
            txt_diemtichluy.Text = txt_donvi_dv.Text = txt_giathue.Text = txt_Hang.Text = txt_LoaiP.Text = txt_ngayra.Text = txt_ngayvao.Text = txt_SoNguoi.Text = txt_tang.Text = txt_tenphong.Text = txt_tenkhach.Text = txt_tientamtinh.Text = "";
            cbo_tinhtrang.SelectedIndex = 0;
            btn_doitrangthai.Enabled = false;
            btn_doitrangthai.Text = "Chờ chọn phòng";
            btn_doitrangthai.Tag = 0;

        }
       
        private void LoadDuLieuPhong(string maPhong)
        {
            int maphong = int.Parse(maPhong);
            string tenphong = xl.Gettenphong(maphong);
            var thongtin = DanhsachPhong.FirstOrDefault(t => t.Maphong == maphong);
            txt_giathue.Text = thongtin.Giathue.ToString("N0");
            txt_LoaiP.Text = thongtin.Loaiphong;
            txt_tang.Text = thongtin.Matang.ToString();
            txt_tenkhach.Text = thongtin.Khachhang;
            txt_ngayra.Text = thongtin.Ngayra;
            txt_tientamtinh.Text = xl.TienTamTinh(maphong);
            txt_SoNguoi.Text = thongtin.Sokhach.ToString();
            txt_ngayvao.Text = thongtin.Ngayvao;
            txt_Hang.Text = thongtin.Hangthanhvien;
            txt_diemtichluy.Text = thongtin.Diemtichluy.ToString();
            txt_tenphong.Text = thongtin.Tenphong;
            cbo_tinhtrang.SelectedValue = thongtin.Trangthai;
            btn_doitrangthai.Enabled = true;
            var gannut = xl.GetCauhinhNutDoiTrangthai(thongtin.Trangthai);
            btn_doitrangthai.Tag = gannut.tag;;
            btn_doitrangthai.Text = gannut.text;
            Session.maphonghientai = maphong;
            Session.tenphonghientai = tenphong;
            Session.trangthaiphong = thongtin.Trangthai;
            da_dv_su_dung.DataSource = xl.listdichvup(maphong);
            lbl_phonghientai.Text = $"Phòng {tenphong}";
            

        }

        private void LoadDichVu(string madv)
        {
            tblDichVu dv = xl.GetDichVuTheoMa(int.Parse(madv));
            txt_donvi_dv.Text = dv.don_vi;
            txt_sl_ton_dv.Text = dv.ton_kho.ToString();
            txt_tendv.Text = dv.ten_dv;
            txt_gia_dv.Text = ((int)dv.gia).ToString("N0") ;
            Session.Madv = dv.ma_dv;
            nb_sl_goi.Value = 1;

        }
        private void Loadbuttons()
        {
            ThongkePhong ttp = xl.GetThongKePhong();
            btn_pTrong.Text = $"Trống ({ttp.Trong})";
            btn_pChoXn.Text = $"Chờ xác nhận ({ttp.ChoXacNhan})";
            btn_tatcaphong.Text = $"Tất cả ({ttp.TatCa})";
            btn_pDangdung.Text = $"Đang dùng ({ttp.DangDung})";
            btn_pQuaHan.Text = $"Quá hạn ({ttp.QuaHan})";
            btn_chocheck.Text = $"Chờ checkin ({ttp.ChoCheckIn})";
            btn_dangsua.Text = $"Dang sửa ({ttp.BaoTri})";
            btn_dangdon.Text = $"Đang dọn ({ttp.DangDon})";
            btn_Dungdung.Text = $"Đừng sử dụng ({ttp.DungSuDung})";
        }
        private void LoadData()
        {
            Loadbuttons();
            List<tblTinhTrangPhong> ttps = xl.tinhtrangp();
            tblTinhTrangPhong nds = new tblTinhTrangPhong();
            nds.ma_tinh_trang = 0;
            nds.mo_ta = "Chọn trạng thái";
            ttps.Insert(0, nds);
            cbo_tinhtrang.DataSource = ttps;
            cbo_tinhtrang.ValueMember = "ma_tinh_trang";
            cbo_tinhtrang.DisplayMember = "mo_ta";
            
            flow_ds_phong.Controls.Clear();
            btn_doitrangthai.Enabled = false;
            btn_doitrangthai.Text = "Chờ chọn phòng";
            flow_ds_dich_vu.Controls.Clear();
            LoadDichVu();
            var dsPhong = xl.GetAllThongtinphong();
            DanhsachPhong = dsPhong;
            LoadPhong(dsPhong);
            
        }
        private void LoadDichVu()
        {
            flow_ds_dich_vu.Controls.Clear();
            var ttdv = xl.dsDichVu();
            foreach (var item in ttdv)
            {
                UCdichvu uc = new UCdichvu();
                uc.Tag = item.ma_dv;
                // Gán số thứ tự để nhìn cho rõ
                uc.ServiceName = $"{item.ten_dv}";
                uc.Price = ((int)item.gia).ToString("N0") + " VND";
                uc.ServiceImage = PathHelper.LoadFromDb(item.anh);
                uc.DichVuSelect += (madv) =>
                {
                    LoadDichVu(madv);
                };
                // Add vào flowlayout
                flow_ds_dich_vu.Controls.Add(uc);
            }
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
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            lbl_chucvu.Text = Session.Role ?? "None";
            lbl_tennb.Text = Session.UserName ?? "Chưa đăng nhập";
            timer1.Start();
        }
        
        
    }
}

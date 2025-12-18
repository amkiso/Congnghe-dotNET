using KhachSanSaoBang.ChatBox;
using KhachSanSaoBang.Models;
using KhachSanSaoBang.Models.Client;
using KhachSanSaoBang.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static KhachSanSaoBang.Models.Dataloader;


namespace KhachSanSaoBang
{
    public partial class QuanLyDichVu : Form
    {
        Xuly xl = new Xuly();
        int acction = 0; //1: them, 2: sua
        static int madv = 0;
        private static List<tblDichVu> DanhsachDv;
        private string Duongdan = "";

        // Tên ảnh sẽ lưu (vd: abc.jpg)
        private string tenanh = "";
        public QuanLyDichVu()
        {
            InitializeComponent();
            this.Load += QuanLyDichVu_Load;
            timer1.Tick += Timer1_Tick;
            btn_image.Click += Btn_image_Click;
            btn_add.Click += Btn_add_Click;
            btn_change.Click += Btn_change_Click;
            btn_delete.Click += Btn_delete_Click;
            btn_save.Click += Btn_save_Click;
            btn_exit.Click += Btn_exit_Click;
            //Menu
            btn_datphong.Click += Btn_datphong_Click;
            btn_tracuukhachhang.Click += Btn_tracuukhachhang_Click;
            btn_dangkykhachhang.Click += Btn_dangkykhachhang_Click;
            btn_thongke.Click += DoiKhuVucLamViec;
            btn_dangkythanhvien.Click += DoiKhuVucLamViec;
            btn_danhsachhoadon.Click += DoiKhuVucLamViec;
            btn_chatbot.Click += Btn_chatbot_Click;
            btn_quanlyphong.Click += Btn_quanlyphong_Click;
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

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dangnhap dn = new Dangnhap();
            dn.ShowDialog();
            this.Close();
        }

        private void Btn_Trangchu_Click(object sender, EventArgs e)
        {
            this.Hide();
            TrangLamViec tlv = new TrangLamViec();
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
            if (    Session.Roleid != 1 && Session.Roleid != 2)
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


        private void Btn_quanlyphong_Click(object sender, EventArgs e)
        {
            if (Session.Roleid != 1 && Session.Roleid != 2)
            {
                MessageBox.Show("Bạn không có quyền thức hiện thao tác này !", "Thông báo");
                return;
            }
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

        private void Btn_image_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn ảnh dịch vụ";
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Duongdan = ofd.FileName;
                tenanh = Path.GetFileName(ofd.FileName);

                // Load ảnh không bị lock file
                using (var imgTemp = Image.FromFile(Duongdan))
                {
                    picImage.Image = new Bitmap(imgTemp);
                }

                picImage.SizeMode = PictureBoxSizeMode.Zoom;
            }
            string imageDbPath = "/Content/images/DichVu/" + tenanh;
            txt_duongdananh.Text = imageDbPath;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            lbl_timer.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            
        }
        private void LoadData()
        {
            
            var ttdv = xl.dsDichVu();
            DanhsachDv = ttdv;
            LoadDichVu(ttdv);
        }
        private void LoadDichVu(List<tblDichVu> ttdv)
        {
            flow_ds_dich_vu.Controls.Clear();
            
            foreach (var item in ttdv)
            {
                UCdichvu uc = new UCdichvu();
                uc.Tag = item.ma_dv;
                uc.ServiceName = $"{item.ten_dv}";
                uc.Price = ((int)item.gia).ToString("N0") + " VND";
                uc.ServiceImage = PathHelper.LoadFromDb(item.anh);
                uc.DichVuSelect += (madv) =>
                {
                    LoadDichVu(madv);
                };
                // Add vào flowlayout
                flow_ds_dich_vu.Controls.Add(uc);
                btn_change.Enabled = true;
                btn_delete.Enabled = true;
            }
        }
        private void Btn_reload_Click(object sender, EventArgs e)
        {
            Btn_clear_all_Click(sender, e);
            LoadData();
        }

        private void Btn_clear_all_Click(object sender, EventArgs e)
        {
            //Session.maphonghientai = 0;
            //Session.tenphonghientai = "Chưa chọn phòng";
            //lbl_phonghientai.Text = "Chưa chọn phòng";
            //cbo_tinhtrang.SelectedIndex = 0;
            //cbo_tang.SelectedIndex = 0;
            //cbo_loaiphong.SelectedIndex = 0;
            //txt_tenphong.Clear();
            //txt_sohoadon.Clear();
            //txt_sophieudat.Clear();
        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ này không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
                return;
            else
            {
                var phongh = DanhsachDv.FirstOrDefault(t => t.ma_dv == Session.Madv);
                if (xl.Check_Delete_DichVu(Session.Madv))
                {
                    MessageBox.Show("Dịch vụ đã từng được sử dụng, không thể xóa", "Thông báo");

                }
                else
                {
                    if (xl.DeleteDichVu(Session.Madv))
                    {
                        MessageBox.Show("Xóa thành công", "Thành công");
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại", "Thông báo");
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


            disableitem();
            tblDichVu dv = new tblDichVu();
            dv.ten_dv = txt_tendv.Text;
            dv.don_vi = txt_dv_tinh.Text;
            dv.anh = txt_duongdananh.Text;
            if (!int.TryParse(txt_slton.Text, out int tonKho) || !int.TryParse(txt_giaban.Text, out int gia))
            {
                MessageBox.Show("Số lượng tồn hoặc giá bán không hợp lệ!");
                return;
            }
            else { dv.gia = gia; dv.ton_kho = tonKho; }
            btn_save.Enabled = false;
            switch (acction)
            {
                case 1://Thêm
                    {
                        if (LuuAnh())
                        {
                            dv.anh = txt_duongdananh.Text;
                            if (xl.AddDichVu(dv))
                            {
                                MessageBox.Show("Thêm dịch vụ thành công", "Thành công");
                            }
                            else MessageBox.Show("Thêm dịch vụ thất bại", "Thông báo");
                        }
                        
                        break;
                    }
                case 2://Sửa
                    {
                        dv.ma_dv = Session.Madv;
                        if(xl.SuaThongTinDichVu(dv))
                        {
                            MessageBox.Show("Cập nhật thông tin thành công", "Thành công");
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thông tin thất bại", "Thông báo");
                        }
                        break;
                    }
            }
            LoadData();
        }
        private bool LuuAnh()
        {
            if (string.IsNullOrWhiteSpace(Duongdan))
            {
                MessageBox.Show("Chưa chọn ảnh!", "Thông báo");
                return false;
            }

            // Thư mục Content gốc (KHÔNG phải bin\Debug)
            string folderPath = Path.Combine(
                PathHelper.ContentRoot, "Content","Images", "DichVu");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            string extension = Path.GetExtension(Duongdan);
            tenanh = $"{Guid.NewGuid():N}{extension}";
            string destPath = Path.Combine(folderPath, tenanh);
            File.Copy(Duongdan, destPath, true);
            txt_duongdananh.Text = $"/Content/Images/DichVu/{tenanh}";

            return true;
        }
        private void enableitem()
        {
            txt_dv_tinh.Enabled = true;
            txt_giaban.Enabled = true;
            txt_slton.Enabled = true;
            txt_tendv.Enabled = true;

        }
        private void disableitem()
        {
            txt_dv_tinh.Enabled = false;
            txt_giaban.Enabled = false;
            txt_slton.Enabled = false;
            txt_tendv.Enabled = false;
        }
        private void Btn_add_Click(object sender, EventArgs e)
        {
            enableitem();
            btn_save.Enabled = true;
            txt_duongdananh.Clear();
            txt_dv_tinh.Clear(); txt_giaban.Clear();
            txt_slton.Clear();
            txt_tendv.Clear();
            picImage.Image = null;
            btn_add.Enabled = false;
            acction = 1;
        }
        private void LoadDichVu(string madv)
        {
            tblDichVu dv = xl.GetDichVuTheoMa(int.Parse(madv));
            txt_dv_tinh.Text = dv.don_vi;
            txt_slton.Text = dv.ton_kho.ToString();
            txt_tendv.Text = dv.ten_dv;
            txt_giaban.Text = ((int)dv.gia).ToString("N0");
            Session.Madv = dv.ma_dv;
            txt_duongdananh.Text = dv.anh;
            picImage.Image = PathHelper.LoadFromDb(dv.anh);

        }
        private void QuanLyDichVu_Load(object sender, EventArgs e)
        {
            
            LoadData();
            lbl_chucvu.Text = Session.Role;
            lbl_tennb.Text = Session.UserName;
            timer1.Start();
        }
    }
}

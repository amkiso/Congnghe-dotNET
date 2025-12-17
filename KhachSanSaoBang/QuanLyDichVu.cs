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
    public partial class QuanLyDichVu : Form
    {
        Xuly xl = new Xuly();
        int acction = 0; //1: them, 2: sua
        static int madv = 0;
        private static List<tblDichVu> DanhsachDv;
        public QuanLyDichVu()
        {
            InitializeComponent();
            this.Load += QuanLyDichVu_Load;
            timer1.Tick += Timer1_Tick;
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
            //DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này không?", "Xác nhận", MessageBoxButtons.YesNo);
            //if (dr == DialogResult.No)
            //    return;
            //else
            //{
            //    var phongh = DanhsachPhong.FirstOrDefault(t => t.Maphong == Session.maphonghientai);
            //    if (phongh.Sohoadon > 0 || phongh.Sophieudat > 0)
            //    {
            //        MessageBox.Show("Phòng đã có hóa đơn hoặc phiếu đặt, không thể xóa", "Thông báo");

            //    }
            //    else
            //    {
            //        if (xl.DeletePhong(Session.maphonghientai))
            //        {
            //            MessageBox.Show("Xóa phòng thành công", "Thành công");
            //        }
            //        else
            //        {
            //            MessageBox.Show("Xóa phòng thất bại", "Thông báo");
            //        }
            //    }
            //}
            //LoadData();
        }

        private void Btn_change_Click(object sender, EventArgs e)
        {
            acction = 2;
            enableitem();
            btn_save.Enabled = true;
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {


            //tblPhong newp = new tblPhong();
            //newp.loai_phong = cbo_loaiphong.SelectedIndex;
            //newp.ma_tang = cbo_tang.SelectedIndex;
            //newp.ma_tinh_trang = 1;
            //newp.ma_tienich = cbo_tienich.SelectedIndex;
            //newp.so_phong = txt_tenphong.Text;

            //disableitem(); ; switch (acction)
            //{
            //    case 1:

            //        if (newp.ma_tang == 0)
            //        {
            //            MessageBox.Show("Vui lòng chọn tầng cho phòng", "Thông báo");

            //        }
            //        else if (newp.loai_phong == 0)
            //        {
            //            MessageBox.Show("Vui lòng chọn loại phòng", "Thông báo");

            //        }
            //        else if (newp.ma_tienich == 0)
            //        {
            //            MessageBox.Show("Vui lòng chọn tiện ích cho phòng", "Thông báo");

            //        }
            //        if (txt_tenphong.Text.Length == 0)
            //        {
            //            MessageBox.Show("Tên phòng không được trống", "Thông báo");
            //            break;
            //        }
            //        string sophong = txt_tenphong.Text;
            //        int count = DanhsachPhong.Where(t => t.Tenphong == sophong).Count();
            //        if (count > 0)
            //        {
            //            MessageBox.Show("Tên phòng đã tồn tại, vui lòng chọn tên khác", "Thông báo");
            //            break;
            //        }
            //        else
            //        {
            //            if (xl.AddPhong(newp))
            //            {
            //                MessageBox.Show("Thêm phòng thành công", "Thành công");
            //            }
            //            else MessageBox.Show("Thêm phòng thất bại", "Thông báo");
            //        }
            //        break;
            //    case 2:
            //        newp.ma_phong = Session.maphonghientai;
            //        if (xl.ChangeRoomInfomation(newp))
            //        {
            //            MessageBox.Show("Cập nhật thông tin phòng thành công", "Thành công");
            //        }
            //        else
            //        {
            //            MessageBox.Show("Cập nhật thông tin phòng thất bại", "Thông báo");
            //        }
            //        break;

            //}

            LoadData();
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
            //cbo_loaiphong.SelectedIndex = 0;
            //cbo_tinhtrang.Enabled = false;
            //cbo_tang.SelectedIndex = 0;
            //cbo_tinhtrang.SelectedIndex = 1;
            //cbo_tienich.SelectedIndex = 0;
            //txt_tenphong.Clear();
            //txt_sophieudat.Clear();
            //lbl_phonghientai.Text = "đang thêm phòng";
            //txt_sohoadon.Clear();
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
            timer1.Start();
        }
    }
}

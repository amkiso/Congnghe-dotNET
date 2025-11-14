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
    public partial class ThanhToan : Form
    {
        ThongtinThanhToan data = new ThongtinThanhToan();
        Xuly xl = new Xuly();
        int mahinhthuc = 0;
        public ThanhToan(ThongtinThanhToan dt)
        {
            InitializeComponent();
            data = dt;
            data.Tongtien = dt.Thanhtien + dt.Thanhtienphuthu;
            data.Tennv = Session.UserName;
            this.Load += ThanhToan_Load;
            btn_apdungvoucher.Click += Btn_apdungvoucher_Click;
            btn_inhoadon.Click += Btn_inhoadon_Click;
            btn_xacnhan.Click += Btn_xacnhan_Click;
            btn_clear.Click += Btn_clear_Click;
            btn_1k.Click += Btn_Menhgia_click;
            btn_2k.Click += Btn_Menhgia_click;
            btn_5k.Click += Btn_Menhgia_click;
            btn_10k.Click += Btn_Menhgia_click;
            btn_20k.Click += Btn_Menhgia_click;
            btn_50k.Click += Btn_Menhgia_click;
            btn_thanhtoan.Click += Btn_thanhtoan_Click;
            btn_100k.Click += Btn_Menhgia_click;
            btn_200k.Click += Btn_Menhgia_click;
            btn_500k.Click += Btn_Menhgia_click;
            btn_vuadu.Click += Btn_vuadu_Click;
            btn_chuyenkhoan.Click += HinhThucThanhToan_Click;
            btn_tienmat.Click += HinhThucThanhToan_Click;
            btn_quethe.Click += HinhThucThanhToan_Click;
        }

        private void Btn_thanhtoan_Click(object sender, EventArgs e)
        {
            DialogResult xacnhan = MessageBox.Show("Xác nhận thanh toán", "Thông báo", MessageBoxButtons.YesNo);
            if(xacnhan == DialogResult.Yes)
            {
                if (xl.ThanhToanHoaDon(data, mahinhthuc))
                {
                    Inhoadon(1);
                    MessageBox.Show("Thanh Toán thành công !", "thông báo");
                }
                else
                {
                    MessageBox.Show("Thanh Toán thất bại !", "Lỗi");
                }
            }
            else MessageBox.Show("Người dùng hủy thanh Toán !", "Thông báo");

        }

        private void HinhThucThanhToan_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            resetmau();
            btn.BackColor = Color.FromArgb(192, 255, 255);
            mahinhthuc = Convert.ToInt32(btn.Tag);
            btn_xacnhan.Enabled = true;
            checkval(mahinhthuc);
        }
        void checkval(int ma)
        {
            if (ma == 2) {
                data.Tienkhachdua = data.Tongtien;
                btn_thanhtoan.Enabled = false;
                txt_tienkhachdua.Text = data.Tienkhachdua.ToString("N0") + "VNĐ";
            }
            else if (ma == 3) {
                data.Tienkhachdua = data.Tongtien;
                btn_thanhtoan.Enabled = false;
                txt_tienkhachdua.Text = data.Tienkhachdua.ToString("N0") + "VNĐ";
            }
            
        }
        private void resetmau()
        {
            btn_tienmat.BackColor = SystemColors.Window;
            btn_quethe.BackColor = SystemColors.Window;
            btn_chuyenkhoan.BackColor = SystemColors.Window;
        }

        private void Btn_vuadu_Click(object sender, EventArgs e)
        {
            data.Tienkhachdua = data.Tongtien;
            btn_thanhtoan.Enabled = false;
            txt_tienkhachdua.Text = data.Tienkhachdua.ToString("N0") + "VNĐ";
        }

        private void Btn_Menhgia_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                int menhgia = Convert.ToInt32(btn.Tag);
                float tienkhachdua = data.Tienkhachdua + menhgia;
                data.Tienkhachdua = tienkhachdua;
                txt_tienkhachdua.Text = data.Tienkhachdua.ToString("N0") + "VNĐ";
                txt_tienkhachdua.SelectionAlignment = HorizontalAlignment.Right;
            }
        }
        private void Btn_clear_Click(object sender, EventArgs e)
        {
            data.Tienkhachdua = 0;
            resetmau();
            mahinhthuc = 0;
            btn_xacnhan.Enabled = false;
            btn_thanhtoan.Enabled = false;
            txt_tienkhachdua.Text =  "0VNĐ";
            txt_tienthua.Text = "0VNĐ";
        }

        private void Btn_xacnhan_Click(object sender, EventArgs e)
        {

            txt_tienthua.Text = (data.Tienkhachdua - data.Tongtien).ToString("N0") + "VNĐ";
            txt_tienthua.SelectionAlignment = HorizontalAlignment.Right;
            btn_thanhtoan.Enabled = true;
        }

        private void Btn_apdungvoucher_Click(object sender, EventArgs e)
        {
            string makm = txt_mavoucher.Text.Trim();
            if (xl.CheckMaKM(makm))
            {
                float newtongtien = xl.GetTienChietKhau(makm, data.Tongtien);
                data.Tongtien = data.Tongtien - newtongtien;
                data.Tienchietkhau = newtongtien;
                txt_tongtien.Text = data.Tongtien.ToString("N0") + "VNĐ";

                MessageBox.Show($"Đã áp dụng voucher, đã giảm {newtongtien}Đ !", "Thông báo");
                data.Tilechietkhau = xl.GetTileChietKhau(makm);
            }
            else
            {
                MessageBox.Show("Mã voucher không hợp lệ hoặc đã hết hạn!");
                return;
            }
        }

        private void Btn_inhoadon_Click(object sender, EventArgs e)
        {
            Inhoadon(0);
        }
        private void Inhoadon(int loaihd)
        {
            MauInHoaDon printer = new MauInHoaDon(data, "~/icon/gui_cancel.png", loaihd);
            printer.PrintPreview();
            printer.Dispose();
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            DataLoader();
            tbl_trai.CellPaint += (s, ev) => Dataloader.DrawTableCellBorder(s, ev, Color.Azure);
            tbl_topleft.CellPaint += (s, ev) => Dataloader.DrawTableCellBorder(s, ev, Color.Azure);
            container_left.CellPaint += (s, ev) => Dataloader.DrawTableCellBorder(s, ev, Color.Azure);
            tbl_phai.CellPaint += (s, ev) => Dataloader.DrawTableCellBorder(s, ev, Color.Azure);
            tbl_phai.BackColor = Color.FromArgb(120, 0, 0, 0);
            tbl_phai.Visible = true;
            container_left.BackColor = Color.FromArgb(120, 0, 0, 0);
            container_left.Visible = true;
            lbl_tennv.Text = Session.UserName;
            data_phuthu.ClearSelection();
            data_tienphong.ClearSelection();
            da_dv_su_dung.ClearSelection();
            btn_thanhtoan.Enabled=false;
            this.WindowState = FormWindowState.Maximized;

        }

        
        
        private void DataLoader()
        {
            try
            {
                lbl_mahd.Text = data.Mahd.ToString();
                lbl_tennv.Text = Session.UserName;
                lbl_hoten_kh.Text = data.Tenkh;
                lbl_ngaydukienra.Text = data.Ngaydukienra.ToString();
                lbl_ngayra.Text = data.Ngayra.ToString();
                lbl_ngayvao.Text = data.Ngayvao.ToString();
                lbl_sophong.Text = data.Sophong;
                lbl_khachdicung.Text = data.sokhach.ToString();
                da_dv_su_dung.DataSource = data.Dichvusudung;
                txt_tongtien.Text = data.Tongtien.ToString("N0") + "VNĐ";
                data_tienphong.Rows.Add();
                data_phuthu.Rows.Add();
                data_tienphong.Rows[0].Cells["col_giaphong"].Value = data.Giaphong.ToString("N0");
                data_phuthu.Rows[0].Cells["col_giaphong2"].Value = data.Giaphong.ToString("N0");
                data_tienphong.Rows[0].Cells["col_thanhtien"].Value = data.Thanhtien.ToString("N0");
                data_tienphong.Rows[0].Cells["col_tiendv"].Value = data.Tiendichvu.ToString("N0");
                data_tienphong.Rows[0].Cells["col_ngaythue"].Value = data.Songaythue.ToString("N0");
                data_phuthu.Rows[0].Cells["col_tienphuthu"].Value = data.Thanhtienphuthu.ToString("N0");
                data_phuthu.Rows[0].Cells["col_tilephuthu"].Value = data.Tilephuthu.ToString("N0") + "%";
                data_phuthu.Rows[0].Cells["col_ngayphuthu"].Value = data.Songayphuthu.ToString();
                txt_tongtien.SelectionAlignment = HorizontalAlignment.Right;
                lbl_header_phai.TextAlign = ContentAlignment.MiddleCenter;
                txt_tienkhachdua.SelectionAlignment = HorizontalAlignment.Right;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

    }
}

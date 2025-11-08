using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhachSanSaoBang.Models;
using System.Windows.Forms;
using KhachSanSaoBang.Models.Data;
using System.Drawing.Printing;
using System.Xml;

namespace KhachSanSaoBang
{
    public partial class ThanhToan : Form
    {
        ThongtinThanhToan data = new ThongtinThanhToan();
        Xuly xl = new Xuly();
        public ThanhToan(ThongtinThanhToan dt)
        {
            InitializeComponent();
            data = dt;
            data.Tongtien = dt.Thanhtien + dt.Thanhtienphuthu;
            data.Tennv = Session.UserName;
            this.Load += ThanhToan_Load;
            btn_apdungvoucher.Click += Btn_apdungvoucher_Click;
            btn_inhoadon.Click += Btn_inhoadon_Click;
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
            else { 
                MessageBox.Show("Mã voucher không hợp lệ hoặc đã hết hạn!");
                return;
            }
        }

        private void Btn_inhoadon_Click(object sender, EventArgs e)
        {
           MauInHoaDon printer = new MauInHoaDon(data, "~/icon/gui_cancel.png", 0);

            printer.PrintPreview();
            printer.Dispose();
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            DataLoader();
            tbl_trai.CellPaint += (s, ev) => DrawTableCellBorder(s, ev, Color.Azure);
            tbl_topleft.CellPaint += (s, ev) => DrawTableCellBorder(s, ev, Color.Azure);
            container_left.CellPaint += (s, ev) => DrawTableCellBorder(s, ev, Color.Azure);
            tbl_phai.CellPaint += (s, ev) => DrawTableCellBorder(s, ev, Color.Azure);
            tbl_phai.BackColor = Color.FromArgb(120, 0, 0, 0);
            tbl_phai.Visible = true;
            container_left.BackColor = Color.FromArgb(120, 0, 0, 0);
            container_left.Visible = true;
            lbl_tennv.Text = Session.UserName;
            data_phuthu.ClearSelection();
            data_tienphong.ClearSelection();
            da_dv_su_dung.ClearSelection();
            this.WindowState = FormWindowState.Maximized;
            
        }

        private void DrawTableCellBorder(object sender, TableLayoutCellPaintEventArgs e, Color c)
        {
            var g = e.Graphics;
            var r = e.CellBounds;

            using (Pen p = new Pen(c))
            {
                g.DrawRectangle(p, r.X, r.Y, r.Width - 1, r.Height - 1);
            }
        }
        private void DataLoader()
        {
            try {
                lbl_mahd.Text = data.Mahd.ToString();
                lbl_tennv.Text = Session.UserName;
                lbl_hoten_kh.Text = data.Tenkh;
                lbl_ngaydukienra.Text = data.Ngaydukienra.ToString();
                lbl_ngayra.Text = data.Ngayra.ToString();
                lbl_ngayvao.Text = data.Ngayvao.ToString();
                lbl_sophong.Text = data.Sophong;
                lbl_khachdicung.Text = data.sokhach.ToString();
                da_dv_su_dung.DataSource = data.Dichvusudung;
                txt_tongtien.Text = data.Tongtien.ToString("N0")+"VNĐ";
                data_tienphong.Rows.Add();
                data_phuthu.Rows.Add();
                data_tienphong.Rows[0].Cells["col_giaphong"].Value = data.Giaphong.ToString("N0");
                data_phuthu.Rows[0].Cells["col_giaphong2"].Value = data.Giaphong.ToString("N0");
                data_tienphong.Rows[0].Cells["col_thanhtien"].Value = data.Thanhtien.ToString("N0");
                data_tienphong.Rows[0].Cells["col_tiendv"].Value = data.Tiendichvu.ToString("N0");
                data_tienphong.Rows[0].Cells["col_ngaythue"].Value = data.Songaythue.ToString("N0");
                data_phuthu.Rows[0].Cells["col_tienphuthu"].Value = data.Thanhtienphuthu.ToString("N0");
                data_phuthu.Rows[0].Cells["col_tilephuthu"].Value = data.Tilephuthu.ToString("N0")+"%";
                data_phuthu.Rows[0].Cells["col_ngayphuthu"].Value = data.Songayphuthu.ToString();
                txt_tongtien.SelectionAlignment = HorizontalAlignment.Right;
                lbl_header_phai.TextAlign = ContentAlignment.MiddleCenter;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

       
    }
}

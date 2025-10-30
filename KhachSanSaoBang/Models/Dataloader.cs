using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KhachSanSaoBang.Models.Data;
namespace KhachSanSaoBang.Models
{
    public class Dataloader
    {
        
        public static bool loaddulieu(Thongtinchung tt, Form f)
        {
            try
            {
                f.Controls["panel2"].Controls["panel1"].Controls["txt_tenphong"].Text = tt.Tenphong;
                f.Controls["panel2"].Controls["panel1"].Controls["txt_loaiphong"].Text = tt.Loaiphong;
                f.Controls["panel2"].Controls["panel1"].Controls["txt_giathue"].Text = tt.Giathue.ToString();
                f.Controls["panel2"].Controls["panel1"].Controls["txt_sotang"].Text = tt.Matang.ToString();
                f.Controls["panel2"].Controls["panel1"].Controls["txt_ngayra"].Text = tt.Ngayra.ToString();
                f.Controls["panel2"].Controls["panel1"].Controls["txt_ngaydatphong"].Text = tt.Ngayvao.ToString();
                f.Controls["panel2"].Controls["panel1"].Controls["txt_gionhanphong"].Text = tt.Thoigian_nhanphong.ToString();
                f.Controls["panel2"].Controls["panel1"].Controls["txt_tenkhach"].Text = tt.Khachhang;
                f.Controls["panel2"].Controls["panel1"].Controls["txt_sokhachthue"].Text = tt.Sokhach.ToString();
                
                return true;
            }
            catch (Exception e) { return false; }
        }

        public static bool ToMauPhong(Form form, List<tblPhong> danhSachPhong)
        {
            if (form == null || danhSachPhong == null) return false;
            try
            {
                foreach (var phong in danhSachPhong)
                {
                    string tenButton = "btn_p" + phong.so_phong;

                    // Tìm button theo tên trong toàn bộ form (bao gồm cả tabcontrol)
                    Button btn = TimButtonTheoTen(form, tenButton);

                    if (btn != null)
                    {
                        btn.BackColor = LayMauTheoTinhTrang((int)phong.ma_tinh_trang);
                    }
                }return true;
            }
            catch { return false; }
        }

        private static Button TimButtonTheoTen(Control parent, string ten)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button b && b.Name.Equals(ten, StringComparison.OrdinalIgnoreCase))
                    return b;

                // nếu control có chứa control con (như TabControl, Panel, GroupBox)
                Button found = TimButtonTheoTen(c, ten);
                if (found != null)
                    return found;
            }
            return null;
        }

        private static Color LayMauTheoTinhTrang(int ma)
        {
            if (ma == 1)
                return Color.LightGreen;
            else if (ma == 2)
                return Color.Red;
            else if ((ma == 3))
                return Color.Yellow;
            else if ((ma == 6)) return Color.Orange;
            else if (ma == 7) return Color.LightPink;
            else if ((ma == 5)) return Color.Gray;
            else if(ma==4) return Color.DarkOrange;
            else return SystemColors.Control;
        }
    }
}


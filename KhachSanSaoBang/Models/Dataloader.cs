using KhachSanSaoBang.Models.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace KhachSanSaoBang.Models
{
    public static class Dataloader
    {

        public static bool loaddulieu(Thongtinchung tt, Form f)
        {
            try
            {
                f.Controls["tableLayoutPanel1"].Controls["tableLayoutPanel2"].Controls["main_left_container"].Controls["main_left_bot_container"].Controls["row2"].Controls["txt_tenphong"].Text = tt.Tenphong;
                f.Controls["tableLayoutPanel1"].Controls["tableLayoutPanel2"].Controls["main_left_container"].Controls["main_left_bot_container"].Controls["row2"].Controls["txt_loaiphong"].Text = tt.Loaiphong;
                f.Controls["tableLayoutPanel1"].Controls["tableLayoutPanel2"].Controls["main_left_container"].Controls["main_left_bot_container"].Controls["tableLayoutPanel3"].Controls["row3"].Controls["txt_giathue"].Text = tt.Giathue.ToString();
                f.Controls["tableLayoutPanel1"].Controls["tableLayoutPanel2"].Controls["main_left_container"].Controls["main_left_bot_container"].Controls["row2"].Controls["txt_sotang"].Text = tt.Matang.ToString();
                f.Controls["tableLayoutPanel1"].Controls["tableLayoutPanel2"].Controls["main_left_container"].Controls["main_left_bot_container"].Controls["tableLayoutPanel3"].Controls["row5"].Controls["txt_ngayra"].Text = tt.Ngayra.ToString();
                f.Controls["tableLayoutPanel1"].Controls["tableLayoutPanel2"].Controls["main_left_container"].Controls["main_left_bot_container"].Controls["tableLayoutPanel3"].Controls["row4"].Controls["txt_ngaydatphong"].Text = tt.Ngayvao.ToString();
                f.Controls["tableLayoutPanel1"].Controls["tableLayoutPanel2"].Controls["main_left_container"].Controls["main_left_bot_container"].Controls["tableLayoutPanel3"].Controls["row6"].Controls["txt_gionhanphong"].Text = tt.Thoigian_nhanphong.ToString();
                f.Controls["tableLayoutPanel1"].Controls["tableLayoutPanel2"].Controls["main_left_container"].Controls["main_left_bot_container"].Controls["tableLayoutPanel3"].Controls["row4"].Controls["txt_tenkhach"].Text = tt.Khachhang;
                f.Controls["tableLayoutPanel1"].Controls["tableLayoutPanel2"].Controls["main_left_container"].Controls["main_left_bot_container"].Controls["tableLayoutPanel3"].Controls["row5"].Controls["txt_sokhachthue"].Text = tt.Sokhach.ToString();

                return true;
            }
            catch (Exception e) { return false; }
        }

        public static bool ToMauPhong(Form form)
        {
            Xuly xl = new Xuly();
            var danhSachPhong = xl.tomau();
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
                }
                return true;
            }
            catch { return false; }
        }

        public static Button TimButtonTheoTen(Control parent, string ten)
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

        public static Color LayMauTheoTinhTrang(int ma)
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
            else if (ma == 4) return Color.DarkOrange;
            else return SystemColors.Control;
        }
        /// <summary>
        /// Vẽ border cho TableLayoutPanel.
        /// </summary>
        public static void DrawTableCellBorder(object sender, PaintEventArgs e, Color borderColor)
        {
            if (sender is TableLayoutPanel panel)
            {
                using (Pen pen = new Pen(borderColor))
                {
                    e.Graphics.DrawRectangle(
                        pen,
                        0,
                        0,
                        panel.ClientSize.Width - 1,
                        panel.ClientSize.Height - 1
                    );
                }
            }
        }
        public static void DrawTableFullCellBorder(object sender, TableLayoutCellPaintEventArgs e, Color c)
        {
            var g = e.Graphics;
            var r = e.CellBounds;

            using (Pen p = new Pen(c))
            {
                g.DrawRectangle(p, r.X, r.Y, r.Width - 1, r.Height - 1);
            }
        }
        public static class PathHelper
        {
            public static string ProjectRoot
            {
                get
                {
                    var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    var dir = new DirectoryInfo(baseDir);

                    while (dir != null && !Directory.Exists(Path.Combine(dir.FullName, "Content")))
                    {
                        dir = dir.Parent;
                    }

                    return dir?.FullName;
                }
            }
            public static string ContentRoot
            {
                get
                {
                    var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

                    while (dir != null)
                    {
                        string contentPath = Path.Combine(dir.FullName, "Content");
                        if (Directory.Exists(contentPath))
                            return dir.FullName;

                        dir = dir.Parent;
                    }

                    throw new DirectoryNotFoundException("Không tìm thấy thư mục Content");
                }
            }

            public static Image LoadFromDb(string dbPath)
            {
                if (string.IsNullOrWhiteSpace(dbPath))
                    return Properties.Resources.no_image;

                // bỏ / đầu
                dbPath = dbPath.TrimStart('/', '\\')
                               .Replace('/', Path.DirectorySeparatorChar);

                string fullPath = Path.Combine(
                    PathHelper.ProjectRoot,
                    dbPath
                );

                if (!File.Exists(fullPath))
                    return Properties.Resources.no_image;

                // tránh lock file
                using (var img = Image.FromFile(fullPath))
                {
                    return new Bitmap(img);
                }
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KhachSanSaoBang.ThongKe.BaoCao
{
    public partial class ucBaoCao : UserControl
    {
        DBBaoCao dbBaoCao = new DBBaoCao();

        public ucBaoCao()
        {
            InitializeComponent();
            this.Load += ucBaoCao_Load;
            this.btnLamMoi.Click += BtnLamMoi_Click;
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            VeBieuDoDoanhThuThang();
    VeBieuDoNhanVien();
    VeBieuDoDichVuPhoBien();
            VeBieuDoDatPhongTheoLoaiPhong();

            MessageBox.Show("Biểu đồ đã được cập nhật!", "Cập nhật thành công",
        MessageBoxButtons.OK, MessageBoxIcon.Information);;
        }

        private void ucBaoCao_Load(object sender, EventArgs e)
        {
            VeBieuDoDoanhThuThang();
            VeBieuDoNhanVien();
            VeBieuDoDichVuPhoBien();
            VeBieuDoDatPhongTheoLoaiPhong();
        }

        // 🔸 1. Biểu đồ doanh thu theo tháng
        // 🔸 1. Biểu đồ Doanh thu theo Tháng
        private void VeBieuDoDoanhThuThang()
        {
            DataTable tbl = dbBaoCao.LayDoanhThuTheoThang();

            chartDoanhThuThang.Series[0].Points.Clear();

            foreach (DataRow r in tbl.Rows)
            {
                chartDoanhThuThang.Series[0].Points.AddXY(
                    "Tháng " + r["Thang"], r["TongDoanhThu"]);
            }
        }

        // 🔸 2. Biểu đồ Doanh thu theo Nhân viên
        private void VeBieuDoNhanVien()
        {
            DataTable tbl = dbBaoCao.LayDoanhThuTheoNhanVien();

            chartDoanhThuNhanVien.Series[0].Points.Clear();

            foreach (DataRow r in tbl.Rows)
            {
                chartDoanhThuNhanVien.Series[0].Points.AddXY(
                    r["TenNhanVien"].ToString(), Convert.ToDecimal(r["TongDoanhThu"]));
            }
        }



        // 🔸 3. Biểu đồ dịch vụ được sử dụng nhiều nhất
        private void VeBieuDoDichVuPhoBien()
        {
            DataTable tbl = dbBaoCao.LayDichVuPhoBien();

            chartDichVuPhoBien.Series.Clear();
            chartDichVuPhoBien.Titles.Clear();
            chartDichVuPhoBien.ChartAreas.Clear();

            ChartArea area = new ChartArea();
            area.BackColor = Color.WhiteSmoke;
            chartDichVuPhoBien.ChartAreas.Add(area);

            chartDichVuPhoBien.Titles.Add("Top Dịch vụ được sử dụng nhiều nhất");
            chartDichVuPhoBien.Titles[0].Font = new Font("Segoe UI", 11, FontStyle.Bold);

            Series s = chartDichVuPhoBien.Series.Add("Dịch vụ");
            s.ChartType = SeriesChartType.Pie;
            s.IsValueShownAsLabel = true;
            s.Label = "#VALX\n#PERCENT{P0}";
            s.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            s["PieLabelStyle"] = "Outside";
            s["PieLineColor"] = "Gray";
            chartDichVuPhoBien.ChartAreas[0].Area3DStyle.Enable3D = true;

            foreach (DataRow r in tbl.Rows)
            {
                s.Points.AddXY(r["TenDichVu"], r["SoLanDung"]);
            }

            chartDichVuPhoBien.Legends[0].Docking = Docking.Right;
            chartDichVuPhoBien.Legends[0].Font = new Font("Segoe UI", 9);
        }

        // 🔸 4. Biểu đồ Số lượt đặt phòng theo Loại phòng
        private void VeBieuDoDatPhongTheoLoaiPhong()
        {
            DataTable tbl = dbBaoCao.LaySoLuotDatPhongTheoLoaiPhong();

            chartDatPhongLoaiPhong.Series[0].Points.Clear();

            foreach (DataRow r in tbl.Rows)
            {
                chartDatPhongLoaiPhong.Series[0].Points.AddXY(
                    r["LoaiPhong"].ToString(), Convert.ToInt32(r["SoLuotDat"]));
            }
        }
    }
}

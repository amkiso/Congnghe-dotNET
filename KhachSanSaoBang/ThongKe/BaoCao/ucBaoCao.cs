using System;
using System.Data;
using System.Drawing;
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
            this.cboNam.SelectedIndexChanged += CboNam_SelectedIndexChanged;
            this.cboThang.SelectedIndexChanged += CboThang_SelectedIndexChanged;
        }

        private void ucBaoCao_Load(object sender, EventArgs e)
        {
            LoadNam();

            // Thêm tháng 1–12
            cboThang.Items.Clear();
            for (int i = 1; i <= 12; i++)
                cboThang.Items.Add(i);
            cboThang.SelectedIndex = -1;

            VeTatCaBieuDo();
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            VeTatCaBieuDo();
            MessageBox.Show("Biểu đồ đã được cập nhật!", "Cập nhật thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void VeTatCaBieuDo()
        {
            VeBieuDoDoanhThuThang();
            VeBieuDoNhanVien();
            VeBieuDoDichVuPhoBien();
            VeBieuDoDatPhongTheoLoaiPhong();
        }

        private void LoadNam()
        {
            DataTable tbl = dbBaoCao.LayDanhSachNam();
            cboNam.DataSource = tbl;
            cboNam.DisplayMember = "Nam";
            cboNam.ValueMember = "Nam";
            cboNam.SelectedIndex = -1;
        }

        // Khi chọn năm → vẽ lại toàn bộ biểu đồ
        private void CboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNam.SelectedValue is DataRowView) return;
            VeTatCaBieuDo();
        }

        // Khi chọn tháng → chỉ vẽ lại biểu đồ dịch vụ
        private void CboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            VeBieuDoDichVuPhoBien();
        }

        // 🔸 1. Biểu đồ Doanh thu theo Tháng (hoặc theo Năm nếu chưa chọn)
        private void VeBieuDoDoanhThuThang()
        {
            int nam = 0;
            if (cboNam.SelectedValue != null && int.TryParse(cboNam.SelectedValue.ToString(), out int n))
                nam = n;

            DataTable tbl;

            if (nam == 0)
                tbl = dbBaoCao.LayDoanhThuTheoNam();  // nếu chưa chọn năm -> theo năm
            else
                tbl = dbBaoCao.LayDoanhThuTheoThang(nam); // chọn năm -> theo tháng

            chartDoanhThuThang.Series[0].Points.Clear();
            chartDoanhThuThang.Titles.Clear();

            string tieuDe = nam == 0
                ? "Biểu đồ Doanh thu theo Năm"
                : $"Biểu đồ Doanh thu theo Tháng (Năm {nam})";

            chartDoanhThuThang.Titles.Add(tieuDe);
            chartDoanhThuThang.Titles[0].Font = new Font("Segoe UI", 11, FontStyle.Bold);
            chartDoanhThuThang.Titles[0].ForeColor = Color.DarkGoldenrod;

            foreach (DataRow r in tbl.Rows)
            {
                if (nam == 0)
                {
                    chartDoanhThuThang.Series[0].Points.AddXY(
                        "Năm " + r["Nam"], r["TongDoanhThu"]);
                }
                else
                {
                    chartDoanhThuThang.Series[0].Points.AddXY(
                        "Tháng " + r["Thang"], r["TongDoanhThu"]);
                }
            }
        }

        // 🔸 2. Biểu đồ Doanh thu theo Nhân viên (lọc theo năm)
        private void VeBieuDoNhanVien()
        {
            int nam = 0;
            if (cboNam.SelectedValue != null && int.TryParse(cboNam.SelectedValue.ToString(), out int n))
                nam = n;

            DataTable tbl = dbBaoCao.LayDoanhThuTheoNhanVienTheoNam(nam);

            chartDoanhThuNhanVien.Series[0].Points.Clear();
            chartDoanhThuNhanVien.Titles.Clear();

            string tieuDe = nam == 0
                ? "Biểu đồ Doanh thu theo Nhân viên (Tất cả năm)"
                : $"Biểu đồ Doanh thu theo Nhân viên - Năm {nam}";

            chartDoanhThuNhanVien.Titles.Add(tieuDe);
            chartDoanhThuNhanVien.Titles[0].Font = new Font("Segoe UI", 11, FontStyle.Bold);
            chartDoanhThuNhanVien.Titles[0].ForeColor = Color.DarkGoldenrod;

            foreach (DataRow r in tbl.Rows)
            {
                chartDoanhThuNhanVien.Series[0].Points.AddXY(
                    r["TenNhanVien"].ToString(),
                    Convert.ToDecimal(r["TongDoanhThu"]));
            }
        }

        // 🔸 3. Biểu đồ Dịch vụ phổ biến (ảnh hưởng bởi năm + tháng)
        private void VeBieuDoDichVuPhoBien()
        {
            // Nếu chưa chọn hoặc chọn 1 trong 2 thì không vẽ
            if (cboNam.SelectedIndex < 0 || cboThang.SelectedIndex < 0)
            {
                chartDichVuPhoBien.Series.Clear();
                chartDichVuPhoBien.Titles.Clear();
                chartDichVuPhoBien.ChartAreas.Clear();
                chartDichVuPhoBien.Titles.Add("⚠ Vui lòng chọn CẢ Năm và Tháng để xem biểu đồ dịch vụ");
                chartDichVuPhoBien.Titles[0].Font = new Font("Segoe UI", 11, FontStyle.Bold);
                chartDichVuPhoBien.Titles[0].ForeColor = Color.DarkGoldenrod;
                return;
            }

            // Lấy giá trị năm & tháng
            if (!int.TryParse(cboNam.SelectedValue.ToString(), out int nam) || nam == 0 ||
                !int.TryParse(cboThang.SelectedItem.ToString(), out int thang) || thang == 0)
            {
                chartDichVuPhoBien.Series.Clear();
                chartDichVuPhoBien.Titles.Clear();
                chartDichVuPhoBien.ChartAreas.Clear();
                chartDichVuPhoBien.Titles.Add("⚠ Vui lòng chọn CẢ Năm và Tháng hợp lệ");
                chartDichVuPhoBien.Titles[0].Font = new Font("Segoe UI", 11, FontStyle.Bold);
                chartDichVuPhoBien.Titles[0].ForeColor = Color.DarkGoldenrod;
                return;
            }

            // Lấy dữ liệu
            DataTable tbl = dbBaoCao.LayDichVuPhoBienTheoNamThang(nam, thang);

            if (tbl.Rows.Count == 0)
            {
                chartDichVuPhoBien.Series.Clear();
                chartDichVuPhoBien.Titles.Clear();
                chartDichVuPhoBien.ChartAreas.Clear();
                chartDichVuPhoBien.Titles.Add($"Không có dữ liệu cho Tháng {thang}/{nam}");
                chartDichVuPhoBien.Titles[0].Font = new Font("Segoe UI", 11, FontStyle.Bold);
                chartDichVuPhoBien.Titles[0].ForeColor = Color.DarkGoldenrod;
                return;
            }

            // Vẽ biểu đồ
            chartDichVuPhoBien.Series.Clear();
            chartDichVuPhoBien.Titles.Clear();
            chartDichVuPhoBien.ChartAreas.Clear();

            ChartArea area = new ChartArea();
            area.BackColor = Color.WhiteSmoke;
            chartDichVuPhoBien.ChartAreas.Add(area);

            chartDichVuPhoBien.Titles.Add($"Top Dịch vụ được sử dụng nhiều nhất (Tháng {thang}/{nam})");
            chartDichVuPhoBien.Titles[0].Font = new Font("Segoe UI", 11, FontStyle.Bold);
            chartDichVuPhoBien.Titles[0].ForeColor = Color.DarkGoldenrod;

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
            chartDichVuPhoBien.Legends[0].ForeColor = Color.DarkGoldenrod;
        }


        // 🔸 4. Biểu đồ Số lượt đặt phòng theo loại phòng (lọc theo năm)
        private void VeBieuDoDatPhongTheoLoaiPhong()
        {
            int nam = 0;
            if (cboNam.SelectedValue != null && int.TryParse(cboNam.SelectedValue.ToString(), out int n))
                nam = n;

            DataTable tbl = dbBaoCao.LaySoLuotDatPhongTheoLoaiPhongTheoNam(nam);

            chartDatPhongLoaiPhong.Series[0].Points.Clear();
            chartDatPhongLoaiPhong.Titles.Clear();

            string tieuDe = nam == 0
                ? "Biểu đồ Số lượt đặt phòng theo Loại phòng (Tất cả năm)"
                : $"Biểu đồ Số lượt đặt phòng theo Loại phòng - Năm {nam}";

            chartDatPhongLoaiPhong.Titles.Add(tieuDe);
            chartDatPhongLoaiPhong.Titles[0].Font = new Font("Segoe UI", 11, FontStyle.Bold);
            chartDatPhongLoaiPhong.Titles[0].ForeColor = Color.DarkGoldenrod;

            foreach (DataRow r in tbl.Rows)
            {
                chartDatPhongLoaiPhong.Series[0].Points.AddXY(
                    r["LoaiPhong"].ToString(),
                    Convert.ToInt32(r["SoLuotDat"]));
            }
        }
    }
}

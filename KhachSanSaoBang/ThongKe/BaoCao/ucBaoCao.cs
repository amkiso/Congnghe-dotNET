using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KhachSanSaoBang.ThongKe.BaoCao
{
    public partial class ucBaoCao : UserControl
    {
        DBBaoCao db = new DBBaoCao();

        public ucBaoCao()
        {
            InitializeComponent();
            this.Load += ucBaoCao_Load;
            btnLamMoi.Click += BtnLamMoi_Click;
            rdoNgay.CheckedChanged += Radio_CheckedChanged;
            rdoThang.CheckedChanged += Radio_CheckedChanged;
            rdoNam.CheckedChanged += Radio_CheckedChanged;
            tabMain.SelectedIndexChanged += TabMain_SelectedIndexChanged;
        }

        private void ucBaoCao_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDenNgay.Value = DateTime.Now;
            rdoThang.Checked = true;

            // Tùy chỉnh giao diện tab
            tabMain.Padding = new Point(10, 5);
            tabMain.Appearance = TabAppearance.FlatButtons;
            tabMain.ItemSize = new Size(120, 35);
            tabMain.SizeMode = TabSizeMode.Fixed;
            tabMain.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            tabMain.BackColor = Color.White;

            AnTatCaBieuDo();
            CapNhatBieuDo();
        }

        private void TabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatBieuDo();
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            CapNhatBieuDo();
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                CapNhatBieuDo();
        }

        private void AnTatCaBieuDo()
        {
            chartDoanhThuNgay.Visible = chartDoanhThuThang.Visible = chartDoanhThuNam.Visible = false;
            chartNVNgay.Visible = chartNVThang.Visible = chartNVNam.Visible = false;
            chartDVNgay.Visible = chartDVNam.Visible = false;
            chartPhongNgay.Visible = chartPhongThang.Visible = false;
        }

        private void CapNhatBieuDo()
        {
            AnTatCaBieuDo();

            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;
            int nam = dtpTuNgay.Value.Year;
            string tab = tabMain.SelectedTab.Text.Trim();

            string thangNam = $"Tháng {tuNgay.Month} / {tuNgay.Year}";
            string khoangNgay = $"({tuNgay:dd/MM/yyyy} - {denNgay:dd/MM/yyyy})";
            string namText = $"Năm {nam}";

            // ------------------- DOANH THU -------------------
            if (tab == "Doanh Thu")
            {
                if (rdoNgay.Checked)
                    VeBieuDo(chartDoanhThuNgay, db.LayDoanhThuTheoNgay(tuNgay, denNgay),
                        "Ngay", "TongDoanhThu", $"Doanh thu theo ngày {khoangNgay}", SeriesChartType.Line);
                else if (rdoThang.Checked)
                    VeBieuDo(chartDoanhThuThang, db.LayDoanhThuTheoThang(nam),
                        "Thang", "TongDoanhThu", $"Doanh thu theo tháng – {namText}", SeriesChartType.Column);
                else
                    VeBieuDo(chartDoanhThuNam, db.LayDoanhThu3NamGanNhat(),
                        "Nam", "TongDoanhThu", "Doanh thu 3 năm gần nhất", SeriesChartType.Column);
            }

            // ------------------- NHÂN VIÊN -------------------
            else if (tab == "Nhân viên")
            {
                if (rdoNgay.Checked)
                    VeBieuDo(chartNVNgay, db.LayDoanhThuNhanVienTheoKhoang(tuNgay, denNgay),
                        "TenNV", "TongDoanhThu", $"Doanh thu nhân viên {khoangNgay}", SeriesChartType.Column);
                else if (rdoThang.Checked)
                    VeBieuDo(chartNVThang, db.LayDoanhThuNhanVienTheoKhoang(
                        new DateTime(nam, tuNgay.Month, 1),
                        new DateTime(nam, tuNgay.Month, DateTime.DaysInMonth(nam, tuNgay.Month))),
                        "TenNV", "TongDoanhThu", $"Doanh thu nhân viên – {thangNam}", SeriesChartType.Column);
                else
                    VeBieuDo(chartNVNam, db.LayDoanhThuNhanVienTheoNam(nam),
                        "TenNV", "TongDoanhThu", $"Doanh thu nhân viên – {namText}", SeriesChartType.Column);
            }

            // ------------------- DỊCH VỤ -------------------
            else if (tab == "Dịch vụ")
            {
                if (rdoNgay.Checked)
                    VeBieuDo(chartDVNgay, db.LayDichVuPhoBienTheoNgay(tuNgay, denNgay),
                        "TenDV", "SoLanDung", $"Dịch vụ phổ biến {khoangNgay}", SeriesChartType.Pie);
                else
                    VeBieuDo(chartDVNam, db.LayDichVuPhoBienTheoNgay(
                        new DateTime(nam, 1, 1), new DateTime(nam, 12, 31)),
                        "TenDV", "SoLanDung", $"Dịch vụ phổ biến – {namText}", SeriesChartType.Pie);
            }

            // ------------------- PHÒNG -------------------
            else if (tab == "Phòng")
            {
                if (rdoNgay.Checked)
                    VeBieuDo(chartPhongNgay, db.LayDoanhThuTheoLoaiPhong(tuNgay, denNgay),
                        "LoaiPhong", "TongDoanhThu", $"Doanh thu loại phòng {khoangNgay}", SeriesChartType.Column);
                else
                    VeBieuDo(chartPhongThang, db.LaySoLuotDatPhongTheoLoaiPhong(nam),
                        "LoaiPhong", "SoLuotDat", $"Số lượt đặt phòng – {namText}", SeriesChartType.Column);
            }
        }

        private void VeBieuDo(Chart chart, DataTable tbl, string xCol, string yCol, string title, SeriesChartType type)
        {
            chart.Visible = true;
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();

            ChartArea area = new ChartArea();
            chart.ChartAreas.Add(area);

            if (tbl == null || tbl.Rows.Count == 0)
            {
                chart.Titles.Add("Không có dữ liệu để hiển thị");
                chart.Titles[0].Font = new Font("Segoe UI", 10, FontStyle.Italic);
                chart.Titles[0].ForeColor = Color.Gray;
                return;
            }

            Series s = new Series("Dữ liệu");
            s.ChartType = type;
            s.IsValueShownAsLabel = true;
            s.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            s.LabelForeColor = Color.Black;

            // 🔸 Đổi màu từng cột khác nhau
            Random rnd = new Random();
            foreach (DataRow r in tbl.Rows)
            {
                int index = s.Points.AddXY(r[xCol].ToString(), Convert.ToDouble(r[yCol]));
                s.Points[index].Color = Color.FromArgb(rnd.Next(100, 255), rnd.Next(120, 200), rnd.Next(50, 160));
            }

            chart.Series.Add(s);
            chart.Titles.Add(title);
            chart.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);
            chart.Titles[0].ForeColor = Color.FromArgb(180, 120, 0);

            Title sub = new Title($"Cập nhật đến: {DateTime.Now:dd/MM/yyyy}",
                Docking.Bottom, new Font("Segoe UI", 8, FontStyle.Italic), Color.DimGray);
            chart.Titles.Add(sub);

            CaiDatBieuDo(chart, type);

            // Căn giữa trên form
            chart.Dock = DockStyle.None;
            chart.Anchor = AnchorStyles.None;
            chart.Width = 800;
            chart.Height = 450;

            Form mainForm = this.FindForm();
            void CenterChart()
            {
                if (mainForm == null || chart == null) return;
                int centerX = (mainForm.ClientSize.Width - chart.Width) / 2;
                int centerY = (mainForm.ClientSize.Height - chart.Height) / 2;
                chart.Left = centerX;
                chart.Top = centerY - 60;
            }
            CenterChart();
            if (mainForm != null) mainForm.Resize += (SE, e) => CenterChart();
            tabMain.Resize += (SE, e) => CenterChart();
        }

        private void CaiDatBieuDo(Chart chart, SeriesChartType type)
        {
            chart.BackColor = Color.White;
            chart.BorderlineColor = Color.LightGray;
            chart.BorderlineDashStyle = ChartDashStyle.Solid;

            var area = chart.ChartAreas[0];
            area.BackColor = Color.FromArgb(250, 250, 250);
            area.AxisX.LineColor = Color.FromArgb(200, 160, 80);
            area.AxisY.LineColor = Color.FromArgb(200, 160, 80);
            area.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
            area.AxisX.LabelStyle.ForeColor = Color.DimGray;
            area.AxisY.LabelStyle.ForeColor = Color.DimGray;
            area.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

            chart.Series[0].BorderWidth = 3;
            chart.Series[0].ShadowOffset = 1;
            chart.Series[0].ToolTip = "#AXISLABEL: #VALY VNĐ";
            chart.AntiAliasing = AntiAliasingStyles.All;

            if (type == SeriesChartType.Line)
            {
                chart.Series[0].MarkerStyle = MarkerStyle.Circle;
                chart.Series[0].MarkerSize = 7;
                chart.Series[0].MarkerColor = Color.White;
            }

            Legend legend = new Legend()
            {
                Docking = Docking.Right,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Black
            };
            chart.Legends.Add(legend);
        }
    }
}

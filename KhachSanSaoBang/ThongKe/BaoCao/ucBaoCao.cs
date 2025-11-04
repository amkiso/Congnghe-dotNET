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
        }

        private void ucBaoCao_Load(object sender, EventArgs e)
        {
            DataTable tblNam = db.LayDanhSachNam();
            cboNam.DataSource = tblNam;
            cboNam.DisplayMember = "Nam";
            cboNam.ValueMember = "Nam";
            cboNam.SelectedIndex = tblNam.Rows.Count > 0 ? 0 : -1;

            for (int i = 1; i <= 12; i++) cboThang.Items.Add(i);
            cboThang.SelectedIndex = -1;

            dtpTuNgay.Format = DateTimePickerFormat.Short;
            dtpDenNgay.Format = DateTimePickerFormat.Short;
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                int nam = cboNam.SelectedValue != null ? Convert.ToInt32(cboNam.SelectedValue) : DateTime.Now.Year;
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date;

                // Doanh thu
                DoChart(chartDoanhThuNgay, db.LayDoanhThuTheoNgay(tuNgay, denNgay), "Ngay", "TongDoanhThu", "Doanh thu theo ngày", SeriesChartType.Line);
                DoChart(chartDoanhThuThang, db.LayDoanhThuTheoThang(nam), "Thang", "TongDoanhThu", "Doanh thu theo tháng", SeriesChartType.Column);
                DoChart(chartDoanhThuNam, db.LayDoanhThu3NamGanNhat(), "Nam", "TongDoanhThu", "Doanh thu 3 năm gần nhất", SeriesChartType.Column);

                // Nhân viên
                DoChart(chartNVNgay, db.LayDoanhThuTheoNgay(tuNgay, denNgay), "Ngay", "TongDoanhThu", "Doanh thu nhân viên theo ngày", SeriesChartType.Line);
                DoChart(chartNVThang, db.LayDoanhThuTheoThang(nam), "Thang", "TongDoanhThu", "Doanh thu nhân viên theo tháng", SeriesChartType.Column);
                DoChart(chartNVNam, db.LayDoanhThuNhanVienTheoNam(nam), "TenNV", "TongDoanhThu", "Doanh thu nhân viên theo năm", SeriesChartType.Column);

                // Dịch vụ
                DoChart(chartDVNgay, db.LayDichVuPhoBienTheoNgay(tuNgay, denNgay), "TenDV", "SoLanDung", "Dịch vụ phổ biến (Từ - Đến)", SeriesChartType.Pie);
                DoChart(chartDVNam, db.LayDichVuPhoBienTheoNgay(new DateTime(nam, 1, 1), new DateTime(nam, 12, 31)), "TenDV", "SoLanDung", "Dịch vụ phổ biến trong năm", SeriesChartType.Pie);

                // Phòng
                DoChart(chartPhongNgay, db.LayDoanhThuTheoNgay(tuNgay, denNgay), "Ngay", "TongDoanhThu", "Doanh thu phòng theo ngày", SeriesChartType.Column);
                DoChart(chartPhongThang, db.LaySoLuotDatPhongTheoLoaiPhong(nam), "LoaiPhong", "SoLuotDat", "Số lượt đặt phòng theo loại phòng", SeriesChartType.Column);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải biểu đồ: " + ex.Message);
            }
        }

        private void DoChart(Chart chart, DataTable tbl, string xCol, string yCol, string title, SeriesChartType type)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();

            ChartArea area = new ChartArea();
            chart.ChartAreas.Add(area);

            Series s = new Series("Dữ liệu");
            s.ChartType = type;
            s.IsValueShownAsLabel = true;
            s.LabelForeColor = Color.Black;
            s.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            foreach (DataRow r in tbl.Rows)
                s.Points.AddXY(r[xCol].ToString(), Convert.ToDouble(r[yCol]));

            chart.Series.Add(s);
            chart.Titles.Add(title);
            chart.Titles[0].Font = new Font("Segoe UI", 11, FontStyle.Bold);
            chart.Titles[0].ForeColor = Color.FromArgb(180, 120, 0);

            SetChartStyle(chart, type);
        }

        private void SetChartStyle(Chart chart, SeriesChartType type)
        {
            chart.BackColor = Color.WhiteSmoke;
            chart.ChartAreas[0].BackColor = Color.White;
            chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 9);
            chart.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 9);
            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;
            chart.ChartAreas[0].InnerPlotPosition = new ElementPosition(5, 5, 90, 85);

            Legend legend = new Legend()
            {
                Docking = Docking.Right,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Black,
            };
            chart.Legends.Add(legend);

            if (type == SeriesChartType.Pie)
            {
                chart.Series[0]["PieLabelStyle"] = "Outside";
                chart.Series[0]["PieLineColor"] = "Gray";
                chart.Series[0].Label = "#VALX\n#PERCENT{P0}";
                chart.ChartAreas[0].Area3DStyle.Enable3D = true;
                chart.Legends[0].Docking = Docking.Right;
            }
        }
    }
}

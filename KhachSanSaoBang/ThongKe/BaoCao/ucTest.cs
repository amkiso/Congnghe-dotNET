using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KhachSanSaoBang.ThongKe.BaoCao
{
    public partial class ucTest : UserControl
    {
        private Panel pnlFilter;
        private Label lblTuNgay, lblDenNgay, lblNam, lblThang;
        private DateTimePicker dtpTuNgay, dtpDenNgay;
        private ComboBox cboNam, cboThang;
        private Button btnLamMoi;
        private TabControl tabMain;
        private TabPage tabDoanhThu, tabNhanVien, tabDichVu, tabPhong;

        private Chart chartDoanhThuNgay, chartDoanhThuThang, chartDoanhThuNam;
        private Chart chartNVNgay, chartNVThang, chartNVNam;
        private Chart chartDVNgay, chartDVNam;
        private Chart chartPhongNgay, chartPhongThang;

        public ucTest()
        {
            InitializeComponent();
            this.Load += UcTest_Load;
        }

        private void UcTest_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            // ========== PANEL FILTER (chung cho cả 4 tab) ==========
            pnlFilter = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.White
            };

            lblTuNgay = new Label() { Text = "Từ ngày:", Location = new Point(10, 18), AutoSize = true };
            dtpTuNgay = new DateTimePicker() { Location = new Point(70, 15), Width = 120, Format = DateTimePickerFormat.Short };

            lblDenNgay = new Label() { Text = "Đến ngày:", Location = new Point(200, 18), AutoSize = true };
            dtpDenNgay = new DateTimePicker() { Location = new Point(270, 15), Width = 120, Format = DateTimePickerFormat.Short };

            lblNam = new Label() { Text = "Năm:", Location = new Point(410, 18), AutoSize = true };
            cboNam = new ComboBox() { Location = new Point(450, 15), Width = 80, DropDownStyle = ComboBoxStyle.DropDownList };

            lblThang = new Label() { Text = "Tháng:", Location = new Point(550, 18), AutoSize = true };
            cboThang = new ComboBox() { Location = new Point(600, 15), Width = 80, DropDownStyle = ComboBoxStyle.DropDownList };

            btnLamMoi = new Button()
            {
                Text = "🔄 Làm mới",
                Location = new Point(700, 12),
                Width = 100,
                Height = 30,
                BackColor = Color.DarkGoldenrod,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLamMoi.FlatAppearance.BorderSize = 0;

            pnlFilter.Controls.AddRange(new Control[] {
                lblTuNgay, dtpTuNgay, lblDenNgay, dtpDenNgay,
                lblNam, cboNam, lblThang, cboThang, btnLamMoi
            });

            // ========== TAB CONTROL ==========
            tabMain = new TabControl()
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            tabDoanhThu = new TabPage("Doanh thu");
            tabNhanVien = new TabPage("Nhân viên");
            tabDichVu = new TabPage("Dịch vụ");
            tabPhong = new TabPage("Phòng");

            tabMain.TabPages.AddRange(new TabPage[] { tabDoanhThu, tabNhanVien, tabDichVu, tabPhong });

            // ========== DOANH THU ==========
            chartDoanhThuNgay = TaoBieuDo("Doanh thu theo Ngày", SeriesChartType.Line);
            chartDoanhThuThang = TaoBieuDo("Doanh thu theo Tháng", SeriesChartType.Column);
            chartDoanhThuNam = TaoBieuDo("So sánh Doanh thu 3 năm gần nhất", SeriesChartType.Column);
            tabDoanhThu.Controls.Add(TaoLayout3Chart(chartDoanhThuNgay, chartDoanhThuThang, chartDoanhThuNam));

            // ========== NHÂN VIÊN ==========
            chartNVNgay = TaoBieuDo("Doanh thu Nhân viên theo Ngày", SeriesChartType.Line);
            chartNVThang = TaoBieuDo("Doanh thu Nhân viên theo Tháng", SeriesChartType.Column);
            chartNVNam = TaoBieuDo("So sánh Doanh thu Nhân viên theo Năm", SeriesChartType.Column);
            tabNhanVien.Controls.Add(TaoLayout3Chart(chartNVNgay, chartNVThang, chartNVNam));

            // ========== DỊCH VỤ ==========
            chartDVNgay = TaoBieuDo("Dịch vụ phổ biến (Từ - Đến)", SeriesChartType.Pie);
            chartDVNam = TaoBieuDo("Dịch vụ phổ biến trong Năm", SeriesChartType.Pie);
            tabDichVu.Controls.Add(TaoLayout2Chart(chartDVNgay, chartDVNam));

            // ========== PHÒNG ==========
            chartPhongNgay = TaoBieuDo("Số lượt đặt phòng theo Ngày", SeriesChartType.Column);
            chartPhongThang = TaoBieuDo("Số lượt đặt phòng theo Tháng", SeriesChartType.Column);
            tabPhong.Controls.Add(TaoLayout2Chart(chartPhongNgay, chartPhongThang));

            // ========== ADD TO MAIN ==========
            this.Controls.Add(tabMain);
            this.Controls.Add(pnlFilter);
        }

        // ======== HÀM HỖ TRỢ ========
        private Chart TaoBieuDo(string title, SeriesChartType type)
        {
            Chart chart = new Chart() { Dock = DockStyle.Fill, BackColor = Color.White };

            ChartArea area = new ChartArea();
            area.BackColor = Color.WhiteSmoke;
            area.AxisX.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8);
            area.InnerPlotPosition = new ElementPosition(5, 5, 90, 85);
            chart.ChartAreas.Add(area);

            Series s = new Series("Dữ liệu")
            {
                ChartType = type,
                Color = Color.DarkGoldenrod,
                BorderWidth = 3,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 8, FontStyle.Bold)
            };
            chart.Series.Add(s);

            chart.Titles.Add(new Title(title, Docking.Top, new Font("Segoe UI", 10, FontStyle.Bold), Color.DarkGoldenrod));

            chart.Legends.Add(new Legend()
            {
                Docking = Docking.Bottom,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.DarkGoldenrod
            });

            if (type == SeriesChartType.Pie)
                chart.ChartAreas[0].Area3DStyle.Enable3D = true;

            return chart;
        }

        private TableLayoutPanel TaoLayout3Chart(Chart c1, Chart c2, Chart c3)
        {
            TableLayoutPanel layout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                Padding = new Padding(5)
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33f));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33f));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 34f));

            layout.Controls.Add(c1, 0, 0);
            layout.Controls.Add(c2, 0, 1);
            layout.Controls.Add(c3, 0, 2);

            return layout;
        }

        private TableLayoutPanel TaoLayout2Chart(Chart c1, Chart c2)
        {
            TableLayoutPanel layout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                Padding = new Padding(5)
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));

            layout.Controls.Add(c1, 0, 0);
            layout.Controls.Add(c2, 0, 1);

            return layout;
        }
    }
}

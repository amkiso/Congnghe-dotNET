using KhachSanSaoBang.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms; // Thêm using này vào đầu file
using System.Windows.Forms.DataVisualization.Charting;

namespace KhachSanSaoBang.ThongKe
{
    public partial class ucBaoCao : UserControl
    {
        Models.XuLyBaoCao xl_bc = new Models.XuLyBaoCao();

        public ucBaoCao()
        {
            InitializeComponent();
            this.btn_ThongKe.Click += Btn_ThongKe_Click;
        }

        private void Btn_ThongKe_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtp_TuNgay.Value.Date;
            DateTime denNgay = dtp_DenNgay.Value.Date;
            chartDoanhThuThoiGian.Series.Clear();
            chartLuongKhach.Series.Clear();

            // 2. Tạo Series (dòng dữ liệu) mới cho từng biểu đồ
            Series seriesDoanhThu = new Series("DoanhThu")
            {
                ChartType = SeriesChartType.Column // Biểu đồ cột
            };
            Series seriesLuongKhach = new Series("SoLuongKhach")
            {
                ChartType = SeriesChartType.Line, // Biểu đồ đường
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8
            };

            chartDoanhThuThoiGian.Series.Add(seriesDoanhThu);
            chartLuongKhach.Series.Add(seriesLuongKhach);

            // 3. Kiểm tra RadioButton và tải dữ liệu tương ứng
            if (rdb_TheoNgay.Checked)
            {
                // Tải dữ liệu Doanh thu
                chartDoanhThuThoiGian.DataSource = xl_bc.LayDoanhThuTheoNgay(tuNgay, denNgay);
                seriesDoanhThu.XValueMember = "Ngay";
                seriesDoanhThu.YValueMembers = "TongDoanhThu";

                // Tải dữ liệu Lượng khách
                chartLuongKhach.DataSource = xl_bc.LaySoLuongKhachTheoNgay(tuNgay, denNgay);
                seriesLuongKhach.XValueMember = "Ngay";
                seriesLuongKhach.YValueMembers = "SoLuongKhach";
            }
            else if (rdb_TheoThang.Checked)
            {
                // Tải dữ liệu Doanh thu
                chartDoanhThuThoiGian.DataSource = xl_bc.LayDoanhThuTheoThang(tuNgay, denNgay);
                seriesDoanhThu.XValueMember = "ThangNam";
                seriesDoanhThu.YValueMembers = "TongDoanhThu";

                // Tải dữ liệu Lượng khách
                chartLuongKhach.DataSource = xl_bc.LaySoLuongKhachTheoThang(tuNgay, denNgay);
                seriesLuongKhach.XValueMember = "ThangNam";
                seriesLuongKhach.YValueMembers = "SoLuongKhach";
            }
            else if (rdb_TheoNam.Checked)
            {
                // Tải dữ liệu Doanh thu
                chartDoanhThuThoiGian.DataSource = xl_bc.LayDoanhThuTheoNam(tuNgay, denNgay);
                seriesDoanhThu.XValueMember = "Nam";
                seriesDoanhThu.YValueMembers = "TongDoanhThu";

                // Tải dữ liệu Lượng khách
                chartLuongKhach.DataSource = xl_bc.LaySoLuongKhachTheoNam(tuNgay, denNgay);
                seriesLuongKhach.XValueMember = "Nam";
                seriesLuongKhach.YValueMembers = "SoLuongKhach";
            }
            chartDoanhThuThoiGian.DataBind();
            chartLuongKhach.DataBind();


        }
        

        // Biểu đồ doanh thu theo tháng

        private void cbo_LoaiThongKe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucBaoCao_Load(object sender, EventArgs e)
        {

        }
    }
}

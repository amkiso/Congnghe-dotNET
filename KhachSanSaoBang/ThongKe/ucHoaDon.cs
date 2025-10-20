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

namespace KhachSanSaoBang.ThongKe
{
    public partial class ucHoaDon : UserControl
    {
        Models.XuLyHoaDon xl_hd = new Models.XuLyHoaDon();
        public ucHoaDon()
        {
            InitializeComponent();
            this.Load += UcHoaDon_Load;
            this.btn_print.Click += Btn_print_Click;
        }

        private void Btn_print_Click(object sender, EventArgs e)
        {
            if (dgv_DSHHD.SelectedRows.Count > 0)
            {
                try
                {
                    // 1. Lấy dữ liệu từ dòng đang được chọn
                    // Tên cột "TenKhachHang" và "TongTien" phải khớp với tên trong 'select new' của bạn
                    string tenKH = dgv_DSHHD.SelectedRows[0].Cells["TenKhachHang"].Value.ToString();
                    decimal tongTienValue = Convert.ToDecimal(dgv_DSHHD.SelectedRows[0].Cells["TongTien"].Value);

                    // Định dạng lại số tiền cho đẹp
                    string tongTienFormatted = tongTienValue.ToString("N0") + " ₫";

                    // 2. Chuẩn bị các tham số để truyền vào báo cáo
                    ReportParameter[] parameters = new ReportParameter[2];
                    parameters[0] = new ReportParameter("TenKhachHang", tenKH);
                    parameters[1] = new ReportParameter("TongTien", tongTienFormatted);

                    // 3. Nạp báo cáo vào ReportViewer
                    reportViewer1.LocalReport.ReportPath = "ThongKe\\SimpleInvoice.rdlc"; // Đường dẫn tới file .rdlc
                    reportViewer1.LocalReport.SetParameters(parameters);

                    // Xóa các nguồn dữ liệu cũ để tránh lỗi
                    reportViewer1.LocalReport.DataSources.Clear();

                    // 4. Hiển thị báo cáo
                    reportViewer1.RefreshReport();
                }
                catch (Exception ex)
                {
                    // Hiện lỗi chi tiết (bao gồm inner exception)
                    var msg = new StringBuilder();
                    msg.AppendLine("Lỗi: " + ex.Message);
                    Exception e2 = ex;
                    while (e2.InnerException != null)
                    {
                        e2 = e2.InnerException;
                        msg.AppendLine("Inner: " + e2.Message);
                    }
                    MessageBox.Show(msg.ToString(), "Lỗi khi render report");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để in.");
            }
        }

        private void UcHoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgv_DSHHD.DataSource = xl_hd.LayDanhSachHoaDon();
        }
    }
}

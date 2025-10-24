using KhachSanSaoBang.DoanhThu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang
{
    public partial class ucDoanhThu : UserControl
    {
        DBDoanhThu dsDoanhThu = new DBDoanhThu();

        public ucDoanhThu()
        {
            InitializeComponent();
            this.Load += UcDoanhThu_Load;
            this.btnThongKe.Click += BtnThongKe_Click;
        }

        private void BtnThongKe_Click(object sender, EventArgs e)
        {
            HienThiTheoThoiGian();
        }
        private void LoadNhanVien()
        {
            DataTable tblNV = dsDoanhThu.LoadNhanVien();
            cboNhanVien.DataSource = tblNV;
            cboNhanVien.DisplayMember = "ho_ten";
            cboNhanVien.ValueMember = "ma_nv";
            cboNhanVien.SelectedIndex = -1; // Không chọn mặc định
        }

        private void UcDoanhThu_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;
            HienThiTatCa();
            LoadNhanVien();
        }

        // ✅ Hiển thị toàn bộ doanh thu
        private void HienThiTatCa()
        {
            DataTable tbl = dsDoanhThu.LoadDoanhThu();
            dataGridView1.DataSource = tbl;
            textBox1.Text = dsDoanhThu.TinhTongDoanhThu(tbl).ToString("N0") + " VNĐ";
        }

        // ✅ Hiển thị theo khoảng thời gian
        private void HienThiTheoThoiGian()
        {
            DateTime tu = dtpTuNgay.Value;
            DateTime den = dtpDenNgay.Value;
            string maNV = "";
            if (cboNhanVien.SelectedValue != null)
            {
                maNV = cboNhanVien.SelectedValue.ToString();
            }

            DataTable tbl = dsDoanhThu.LayDoanhThuTheoNgay(tu, den, maNV);
            dataGridView1.DataSource = tbl;
            textBox1.Text = dsDoanhThu.TinhTongDoanhThu(tbl).ToString("N0") + " VNĐ";
        }
    }
}

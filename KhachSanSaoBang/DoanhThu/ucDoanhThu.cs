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
            this.btnThongKe.Click += btnThongKe_Click;
        }

        private void UcDoanhThu_Load(object sender, EventArgs e)
        {
            // Không ẩn mà chỉ vô hiệu hóa DateTimePicker
            dtpTuNgay.Enabled = false;
            dtpDenNgay.Enabled = false;

            // Không cho người dùng nhập tổng doanh thu
            textBox1.ReadOnly = true;
            textBox1.BackColor = System.Drawing.Color.White;

            // Load danh sách nhân viên
            LoadNhanVien();

            // Gắn sự kiện
            ckLocTheoNgay.CheckedChanged += ckLocTheoNgay_CheckedChanged;
            cboNhanVien.SelectedIndexChanged += cboNhanVien_SelectedIndexChanged;
        }

        // Khi tick "Lọc theo ngày" → bật / tắt DateTimePicker
        private void ckLocTheoNgay_CheckedChanged(object sender, EventArgs e)
        {
            bool choPhep = ckLocTheoNgay.Checked;
            dtpTuNgay.Enabled = choPhep;
            dtpDenNgay.Enabled = choPhep;
        }

        private void LoadNhanVien()
        {
            DataTable tbl = dsDoanhThu.LoadNhanVien();
            cboNhanVien.DataSource = tbl;
            cboNhanVien.DisplayMember = "ten_nv"; // alias trong DBDoanhThu
            cboNhanVien.ValueMember = "ma_nv";
            cboNhanVien.SelectedIndex = -1;
        }

        // Khi chọn nhân viên (KHÔNG lọc theo ngày)
        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ckLocTheoNgay.Checked || cboNhanVien.SelectedValue == null)
                return;

            string maNV = cboNhanVien.SelectedValue.ToString();

            // Không lọc theo ngày => truyền false
            DataTable tbl = dsDoanhThu.LayDoanhThu(DateTime.Now, DateTime.Now, maNV, false);
            dataGridView1.DataSource = tbl;

            textBox1.Text = dsDoanhThu.TinhTongDoanhThu(tbl).ToString("N0") + " VNĐ";
        }

        // Khi nhấn "Thống kê" (lọc theo ngày)
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            bool locTheoNgay = ckLocTheoNgay.Checked;

            string maNV = "";
            if (cboNhanVien.SelectedValue != null)
            {
                maNV = cboNhanVien.SelectedValue.ToString();
            }

            DateTime tu = dtpTuNgay.Value;
            DateTime den = dtpDenNgay.Value;

            DataTable tbl = dsDoanhThu.LayDoanhThu(tu, den, maNV, locTheoNgay);
            dataGridView1.DataSource = tbl;

            textBox1.Text = dsDoanhThu.TinhTongDoanhThu(tbl).ToString("N0") + " VNĐ";
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang.DoanhThu
{
    public partial class QuanLyThongKe : Form
    {
        DBDoanhThu db = new DBDoanhThu();

        public QuanLyThongKe()
        {
            InitializeComponent();
            this.Load += QuanLyThongKe_Load;
            this.btnThongKe.Click += BtnThongKe_Click;
            this.btn_LamMoi.Click += Btn_LamMoi_Click;
            // Gán sự kiện check box nếu chưa có trong designer
            this.cbLocTheoNgay.CheckedChanged += ckLocTheoNgay_CheckedChanged;
        }

        private void QuanLyThongKe_Load(object sender, EventArgs e)
        {
            // Cấu hình định dạng ngày tháng
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";

            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";

            // Vô hiệu hóa chọn ngày lúc đầu (nếu chưa tick chọn)
            dtpTuNgay.Enabled = false;
            dtpDenNgay.Enabled = false;

            // TextBox tổng tiền chỉ để đọc
            textBox1.ReadOnly = true;
            textBox1.BackColor = Color.White;
            textBox1.Text = "0 VNĐ";

            textBox2.ReadOnly = true;
            textBox2.BackColor = Color.White;
            textBox2.Text = "";

            // Load dữ liệu nhân viên lên ComboBox
            LoadDataNhanVien();
        }

        private void LoadDataNhanVien()
        {
            try
            {
                DataTable dtNV = db.LoadNhanVien();
                cboNhanVien.DataSource = dtNV;

                // --- SỬA LỖI Ở ĐÂY ---
                // Tên cột phải trùng khớp với lệnh AS trong SQL (ten_nv, ma_nv)
                cboNhanVien.DisplayMember = "ten_nv";
                cboNhanVien.ValueMember = "ma_nv";

                cboNhanVien.SelectedIndex = -1; // Không chọn ai cả lúc đầu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load nhân viên: " + ex.Message);
            }
        }

        // Sự kiện khi tick vào "Lọc theo ngày"
        private void ckLocTheoNgay_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = cbLocTheoNgay.Checked;
            dtpTuNgay.Enabled = isChecked;
            dtpDenNgay.Enabled = isChecked;
        }

        // --- CODE CHỨC NĂNG THỐNG KÊ ---
        private void BtnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy tham số từ giao diện
                DateTime tuNgay = dtpTuNgay.Value.Date;
                // Lấy đến hết ngày (23:59:59)
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

                bool isLocNgay = cbLocTheoNgay.Checked;

                string maNV = "";
                // Kiểm tra null và kiểm tra SelectedValue có thực sự tồn tại không
                if (cboNhanVien.SelectedValue != null && cboNhanVien.SelectedIndex != -1)
                {
                    // Lấy mã để lọc
                    maNV = cboNhanVien.SelectedValue.ToString();

                    // Lấy tên hiển thị lên Textbox bên phải
                    textBox2.Text = cboNhanVien.Text;
                }
                else
                {
                    // Nếu không chọn ai
                    textBox2.Text = "Tất cả nhân viên";
                }

                // 2. Gọi lớp DB để lấy dữ liệu
                DataTable dtKetQua = db.LayDoanhThu(tuNgay, denNgay, maNV, isLocNgay);

                // 3. Đổ dữ liệu lên lưới
                dataGridView1.DataSource = dtKetQua;

                // 4. Tính và hiển thị tổng tiền
                decimal tongTien = db.TinhTongDoanhThu(dtKetQua);
                textBox1.Text = tongTien.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thống kê: " + ex.Message);
            }
        }

        private void Btn_LamMoi_Click(object sender, EventArgs e)
        {
            cboNhanVien.SelectedIndex = -1;
            cbLocTheoNgay.Checked = false;
            dataGridView1.DataSource = null;
            textBox1.Text = "0 VNĐ";
            textBox2.Text = "";
        }
    }
}
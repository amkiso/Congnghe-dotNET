using KhachSanSaoBang.KhachHang;
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
    public partial class ucKhachHang : UserControl
    {
        DanhSachKhachHang dsKH;
        DataTable dtKH;

        public ucKhachHang()
        {
            InitializeComponent();
            this.Load += ucKhachHang_Load;
            btnThem.Click += btnThem_Click;
            btnXoa.Click += btnXoa_Click;
            btnSua.Click += btnSua_Click;
            btnTimKiem.Click += btnTimKiem_Click;
        }

        private void ucKhachHang_Load(object sender, EventArgs e)
        {
            dsKH = new DanhSachKhachHang();
            dtKH = dsKH.LoadKhachHang();
            dataGridView1.DataSource = dtKH;

            for (int i = 1; i <= 31; i++) cbo_ngay.Items.Add(i);
            for (int i = 1; i <= 12; i++) cbo_thang.Items.Add(i);
            for (int i = 1960; i <= DateTime.Now.Year; i++) cbo_nam.Items.Add(i);

            cbo_ngay.SelectedIndex = 0;
            cbo_nam.SelectedIndex = 0;
            cbo_thang.SelectedIndex = cbo_thang.Items.Count - 1;
        }

        private DateTime LayNgaySinh()
        {
            int ngay = Convert.ToInt32(cbo_ngay.Text);
            int thang = Convert.ToInt32(cbo_nam.Text);
            int nam = Convert.ToInt32(cbo_thang.Text);
            return new DateTime(nam, thang, ngay);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string gioiTinh = rdbNam.Checked ? "Nam" : "Nữ";
                DateTime ngaySinh = LayNgaySinh();

                ThongTinKhachHang kh = new ThongTinKhachHang(
                    txtMaKH.Text,
                    txtHoTen.Text,
                    gioiTinh,
                    ngaySinh,
                    txtCCCD.Text,
                    txtEmail.Text,
                    txtDiaChi.Text,
                    txtSDT.Text
                );

                if (dsKH.Them(kh))
                {
                    dsKH.Luu();
                    dataGridView1.DataSource = dsKH.LoadKhachHang();
                    MessageBox.Show("Thêm khách hàng thành công!");
                    ClearForm();
                }
                else MessageBox.Show("Không thể thêm khách hàng!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message);
            }
        }

        // ✅ Nút XÓA
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaKH.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập mã khách hàng cần xóa!");
                    return;
                }

                ThongTinKhachHang kh = new ThongTinKhachHang();
                kh.MaKH = txtMaKH.Text;

                if (dsKH.Xoa(kh))
                {
                    dsKH.Luu();
                    dataGridView1.DataSource = dsKH.LoadKhachHang();
                    MessageBox.Show("Xóa thành công!");
                    ClearForm();
                }
                else MessageBox.Show("Không tìm thấy khách hàng để xóa!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string gioiTinh = rdbNam.Checked ? "Nam" : "Nữ";
                DateTime ngaySinh = LayNgaySinh();

                ThongTinKhachHang kh = new ThongTinKhachHang(
                    txtMaKH.Text,
                    txtHoTen.Text,
                    gioiTinh,
                    ngaySinh,
                    txtCCCD.Text,
                    txtEmail.Text,
                    txtDiaChi.Text,
                    txtSDT.Text
                );

                if (dsKH.Sua(kh))
                {
                    dsKH.Luu();
                    dataGridView1.DataSource = dsKH.LoadKhachHang();
                    MessageBox.Show("Cập nhật thông tin thành công!");
                }
                else MessageBox.Show("Không tìm thấy khách hàng để sửa!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa: " + ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                dataGridView1.DataSource = dsKH.TimKiem(tuKhoa);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtMaKH.Text = row.Cells["MaKH"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtCCCD.Text = row.Cells["CCCD"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                txtSDT.Text = row.Cells["SDT"].Value.ToString();

                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                if (gioiTinh == "Nam") rdbNam.Checked = true;
                else rdbNu.Checked = true;

                if (DateTime.TryParse(row.Cells["NgaySinh"].Value.ToString(), out DateTime ns))
                {
                    cbo_ngay.SelectedItem = ns.Day;
                    cbo_nam.SelectedItem = ns.Month;
                    cbo_thang.SelectedItem = ns.Year;
                }
            }
        }

        private void ClearForm()
        {
            txtMaKH.Clear();
            txtHoTen.Clear();
            txtCCCD.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtTimKiem.Clear();
            rdbNam.Checked = true;
            cbo_ngay.SelectedIndex = 0;
            cbo_nam.SelectedIndex = 0;
            cbo_thang.SelectedIndex = cbo_thang.Items.Count - 1;
        }
    }
}

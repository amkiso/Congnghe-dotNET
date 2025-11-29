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

namespace KhachSanSaoBang.NhanVien
{
    public partial class ucNhanVien : UserControl
    {
        Models.XulyNV xl_nv = new Models.XulyNV();

        public ucNhanVien()
        {
            InitializeComponent();
            this.Load += UcNhanVien_Load;
            this.btn_ThemNV.Click += Btn_ThemNV_Click;
            this.btn_SuaNV.Click += Btn_SuaNV_Click;
            this.btn_XoaNV.Click += Btn_XoaNV_Click;
            this.btn_VoHieuHoaTK.Click += Btn_VoHieuHoaTK_Click;
            this.btn_KhoiPhuc.Click += Btn_KhoiPhuc_Click;
            this.dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }



        private void UcNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dataGridView1.AutoGenerateColumns = false;
            // Lấy DataTable từ XulyNV và gán vào Grid
            dataGridView1.DataSource = xl_nv.LoadNhanVien();
        }

        //Tô màu xám cho nhân viên bị vô hiệu hóa
        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells["trang_thai"].Value != null)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "colSTT")
                {
                    // Gán giá trị là số thứ tự dòng (bắt đầu từ 0 nên phải +1)
                    e.Value = (e.RowIndex + 1).ToString();
                }

                // Lấy giá trị cột trang_thai (chú ý tên cột trong DB của bạn phải đúng)
                bool trangThai = false;
                var cellValue = dataGridView1.Rows[e.RowIndex].Cells["trang_thai"].Value;

                if (cellValue != DBNull.Value)
                {
                    trangThai = Convert.ToBoolean(cellValue);
                }

                if (trangThai == false)
                {
                    e.CellStyle.BackColor = Color.LightGray;
                    e.CellStyle.ForeColor = Color.DimGray;
                }
            }
        }

        private void Btn_ThemNV_Click(object sender, EventArgs e)
        {
            FormThemSuaNhanVien frm = new FormThemSuaNhanVien(0);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void Btn_SuaNV_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int maNV = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                FormThemSuaNhanVien frmSua = new FormThemSuaNhanVien(maNV);

                if (frmSua.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên để sửa!");
            }
        }

        private void Btn_XoaNV_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int maNV = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ma_nv"].Value);
                // Kiểm tra admin nếu cần (dựa vào tên cột tài khoản)

                if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (xl_nv.XoaNhanVien(maNV))
                    {
                        MessageBox.Show("Xóa thành công!");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
            }
        }

        private void Btn_VoHieuHoaTK_Click(object sender, EventArgs e)
        {
            XuLyTrangThai(false); // Gọi hàm chung với tham số false (vô hiệu hóa)
        }

        private void Btn_KhoiPhuc_Click(object sender, EventArgs e)
        {
            XuLyTrangThai(true); // Gọi hàm chung với tham số true (kích hoạt)
        }

        // Hàm xử lý chung cho Vô hiệu hóa và Kích hoạt
        private void XuLyTrangThai(bool trangThaiMoi)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int maNV = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                string hanhDong = trangThaiMoi ? "kích hoạt" : "vô hiệu hóa";

                if (MessageBox.Show($"Bạn có muốn {hanhDong} tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (xl_nv.CapNhatTrangThai(maNV, trangThaiMoi))
                    {
                        MessageBox.Show($"{hanhDong} thành công!");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Thao tác thất bại!");
                    }
                }
            }
        }
    }
}
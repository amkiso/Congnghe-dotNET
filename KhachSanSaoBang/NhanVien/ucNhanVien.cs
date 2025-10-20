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
            this.btn_ThemNV.Click += Btn_ThemNV_Click;
            this.Click += UcNhanVien_Click;
            this.btn_XoaNV.Click += Btn_XoaNV_Click;
            this.btn_SuaNV.Click += Btn_SuaNV_Click;
            this.Load += UcNhanVien_Load;
            this.btn_VoHieuHoaTK.Click += Btn_VoHieuHoaTK_Click;
            this.btn_KhoiPhuc.Click += Btn_KhoiPhuc_Click;
            this.dataGridView1.CellFormatting += DataGridView1_CellFormatting;
            
        }

        private void Btn_KhoiPhuc_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int maNV = (int)dataGridView1.SelectedRows[0].Cells["ma_nv"].Value;

                // Lấy trạng thái hiện tại để kiểm tra
                // Giả sử cột trạng thái có DataPropertyName là "trang_thai"
                bool trangThaiHienTai = (bool)dataGridView1.SelectedRows[0].Cells["trang_thai"].Value;

                // Chỉ cho phép kích hoạt nếu tài khoản đang bị vô hiệu
                if (trangThaiHienTai == true)
                {
                    MessageBox.Show("Tài khoản này đã ở trạng thái hoạt động!", "Thông báo");
                    return;
                }

                var confirmResult = MessageBox.Show("Bạn có muốn kích hoạt lại tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    // Gọi lại hàm cũ nhưng truyền vào giá trị 'true'
                    if (xl_nv.CapNhatTrangThai(maNV, true))
                    {
                        MessageBox.Show("Kích hoạt tài khoản thành công!");
                        LoadData(); // Tải lại để gridview bỏ tô màu xám
                    }
                    else
                    {
                        MessageBox.Show("Thao tác thất bại!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để kích hoạt.");
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Chỉ thực hiện khi có dữ liệu trong dòng
            if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].DataBoundItem != null)
            {
                // Lấy ra đối tượng nhân viên của dòng hiện tại
                var nv = dataGridView1.Rows[e.RowIndex].DataBoundItem as tblNhanVien;

                // Kiểm tra trạng thái
                if (nv != null && nv.trang_thai == false) // Nếu trạng thái là false (bị vô hiệu)
                {
                    // Đổi màu nền và màu chữ của cả dòng
                    e.CellStyle.BackColor = Color.LightGray;
                    e.CellStyle.ForeColor = Color.DimGray;
                }
            }
        }

      

        private void Btn_VoHieuHoaTK_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int maNV = (int)dataGridView1.SelectedRows[0].Cells["ma_nv"].Value;
                string taiKhoan = dataGridView1.SelectedRows[0].Cells["tai_khoan"].Value.ToString();

                // Không cho phép vô hiệu hóa admin
                if (taiKhoan.ToLower() == "admin")
                {
                    MessageBox.Show("Không thể vô hiệu hóa tài khoản Quản trị viên!", "Lỗi");
                    return;
                }

                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn vô hiệu hóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    // Gọi hàm xử lý với trạng thái mới là 'false' (vô hiệu)
                    if (xl_nv.CapNhatTrangThai(maNV, false))
                    {
                        MessageBox.Show("Vô hiệu hóa thành công!");
                        LoadData(); // Tải lại để thấy sự thay đổi
                    }
                    else
                    {
                        MessageBox.Show("Thao tác thất bại!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên.");
            }
        }

        private void UcNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Btn_SuaNV_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
               
                int maNV = (int)dataGridView1.SelectedRows[0].Cells["ma_nv"].Value;

               
                FormThemSuaNhanVien frmSua = new FormThemSuaNhanVien(maNV);

               
                if (frmSua.ShowDialog() == DialogResult.OK)
                {
                   
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa!");
            }
        }

        private void Btn_XoaNV_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string taiKhoan = dataGridView1.SelectedRows[0].Cells["tai_khoan"].Value.ToString();
                int maNV = (int)dataGridView1.SelectedRows[0].Cells["ma_nv"].Value;

                if (taiKhoan.ToLower() == "admin")
                {
                    MessageBox.Show("Bạn không thể xóa tài khoản Quản trị viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }

                DialogResult confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xác nhận Xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    
                    //bool ketQua = xl_nv.XoaNhanVien(maNV);

                    if (xl_nv.XoaNhanVien(maNV))
                    {
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                       
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UcNhanVien_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            dataGridView1.DataSource = xl_nv.LayDanhSachNhanVien();
        }


        private void Btn_ThemNV_Click(object sender, EventArgs e)
        {
            FormThemSuaNhanVien frm = new FormThemSuaNhanVien(0); 
            if (frm.ShowDialog() == DialogResult.OK)
            {
                
                LoadData();
            }
        }

        
    }
}

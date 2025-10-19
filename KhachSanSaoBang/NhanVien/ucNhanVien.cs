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
            
        }

        private void Btn_VoHieuHoaTK_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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

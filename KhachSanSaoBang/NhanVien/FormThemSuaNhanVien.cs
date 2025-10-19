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
    // File: FormThemSuaNhanVien.cs
    public partial class FormThemSuaNhanVien : Form

    {



        Models.XulyNV xl_nv = new Models.XulyNV();

        private int currentNhanVienId;

        private bool isEditMode = false;

        public FormThemSuaNhanVien(int maNV)

        {

            InitializeComponent();

            this.currentNhanVienId = maNV;

            this.Load += FormThemSuaNhanVien_Load;

            this.btn_Luu.Click += Btn_Luu_Click;

            this.btn_Huy.Click += Btn_Huy_Click;

            if (currentNhanVienId != 0)

            {

                isEditMode = true; // Chuyển sang chế độ Sửa

            }

        }



        private void Btn_Huy_Click(object sender, EventArgs e)

        {

            this.Close();

        }



        private void Btn_Luu_Click(object sender, EventArgs e)
        {
            
            if (!int.TryParse(txt_MSNV.Text, out int msnv) ||
                !int.TryParse(txt_Luong.Text, out int luong))
            {
                MessageBox.Show("Mã số và Lương phải là số!");
                return;
            }

            Models.tblNhanVien nv = new Models.tblNhanVien();
            nv.ho_ten = txt_HoTen.Text;
            nv.gioi_tinh = cbo_GioiTinh.Text;
            nv.que_quan = txt_QueQuan.Text;
            nv.luong = luong; 
            nv.dia_chi = txt_DiaChi.Text;
            nv.nam_bd = int.Parse(cbo_NamBD.Text);

            try
            {
                int d = int.Parse(cbo_NgaySinh.SelectedItem.ToString());
                int m = int.Parse(cbo_ThangSinh.SelectedItem.ToString());
                int y = int.Parse(cbo_NamSinh.SelectedItem.ToString());
                nv.ngay_sinh = new DateTime(y, m, d);
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn ngày sinh hợp lệ!");
                return;
            }

            

            bool ketQua = false;
            if (isEditMode)
            {
                
                nv.ma_nv = currentNhanVienId;
                ketQua = xl_nv.SuaNhanVien(nv);
            }
            else
            {
                
                nv.ma_nv = msnv;
                ketQua = xl_nv.ThemNhanVien(nv);
            }

           
            if (ketQua)
            {
               
                string thongBao = isEditMode ? "Sửa nhân viên thành công!" : "Thêm nhân viên thành công!";
                MessageBox.Show(thongBao);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                string thongBaoLoi = isEditMode ? "Sửa thất bại!" : "Thêm thất bại! Mã nhân viên có thể đã tồn tại.";
                MessageBox.Show(thongBaoLoi);
            }
        }



        private void FormThemSuaNhanVien_Load(object sender, EventArgs e)

        {

            

            for (int i = 1; i <= 31; i++) cbo_NgaySinh.Items.Add(i);

            for (int i = 1; i <= 12; i++) cbo_ThangSinh.Items.Add(i);

            for (int i = DateTime.Now.Year - 60; i <= DateTime.Now.Year; i++) cbo_NamSinh.Items.Add(i);

            for (int i = DateTime.Now.Year - 60; i <= DateTime.Now.Year; i++) cbo_NamBD.Items.Add(i);



            cbo_GioiTinh.Items.Add("Nam");

            cbo_GioiTinh.Items.Add("Nữ");



            if (currentNhanVienId == 0)

            {

                this.Text = "Thêm Nhân viên mới";

            }

            else

            {

              

                this.Text = "Sửa thông tin Nhân viên";

               

                var nv = xl_nv.LayThongTinNhanVien(currentNhanVienId);



                if (nv != null)

                {

                   

                    txt_MSNV.Text = nv.ma_nv.ToString();
                    txt_MSNV.ReadOnly = true;

                    txt_HoTen.Text = nv.ho_ten;
                    if (nv.ngay_sinh.HasValue)
                    {
                        cbo_NgaySinh.SelectedItem = nv.ngay_sinh.Value.Day;
                        cbo_ThangSinh.SelectedItem = nv.ngay_sinh.Value.Month;
                        cbo_NamSinh.SelectedItem = nv.ngay_sinh.Value.Year;
                    }

                    cbo_NgaySinh.SelectedItem = nv.ngay_sinh;

                    cbo_GioiTinh.Text = nv.gioi_tinh;

                    txt_QueQuan.Text = nv.que_quan;

                    cbo_NamBD.Text = nv.nam_bd.ToString();

                    txt_Luong.Text = nv.luong.ToString();

                    txt_DiaChi.Text = nv.dia_chi;



                   

                }

            }

        }

    }
}

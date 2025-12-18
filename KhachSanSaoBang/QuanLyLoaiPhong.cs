using KhachSanSaoBang.ChatBox;
using KhachSanSaoBang.Models;
using KhachSanSaoBang.Models.Data;
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
    public partial class QuanLyLoaiPhong : Form
    {
        Xuly xl = new Xuly();
        int acction = 0; // 0: None, 1: Add, 2: Edit

        public QuanLyLoaiPhong()
        {
            InitializeComponent();
            // Gán các sự kiện
            this.Load += QuanLyLoaiPhong_Load;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            // Gán sự kiện click cho các nút (giả định bạn đã có các nút này trên giao diện)
            btn_add.Click += Btn_add_Click;
            btn_change.Click += Btn_change_Click;
            btn_delete.Click += Btn_delete_Click;
            btn_save.Click += Btn_save_Click;
            btn_exit.Click += Btn_exit_Click;
            btn_datphong.Click += Btn_datphong_Click;
            btn_tracuukhachhang.Click += Btn_tracuukhachhang_Click;
            btn_dangkykhachhang.Click += Btn_dangkykhachhang_Click;
            btn_thongke.Click += DoiKhuVucLamViec;
            btn_dangkythanhvien.Click += DoiKhuVucLamViec;
            btn_danhsachhoadon.Click += DoiKhuVucLamViec;
            btn_chatbot.Click += Btn_chatbot_Click;
            btn_quanlyphong.Click += Btn_quanlyphong_Click;
            btn_quanlydv.Click += Btn_quanlydv_Click;
            btn_quanlytang.Click += Btn_quanlytang_Click;
            btn_Trangchu.Click += Btn_Trangchu_Click;
            
        }
        
        private void Btn_Trangchu_Click(object sender, EventArgs e)
        {
            TrangLamViec tlv = new TrangLamViec();
            this.Hide();
            tlv.ShowDialog();
            this.Close();
        }
        private void Btn_datphong_Click(object sender, EventArgs e)
        {
            int acction = 0;
            AcctionForm datp = new AcctionForm(acction);
            Session.Acction_status = false;
            datp.ShowDialog();
            LoadData();
            if (Session.Acction_status == false)
            {
                MessageBox.Show("Bạn đã hủy đặt phòng !", "Thông báo");
            }
        }
        private void Btn_quanlytang_Click(object sender, EventArgs e)
        {
            if (Session.Roleid != 1 && Session.Roleid != 2)
            {
                MessageBox.Show("Bạn không có quyền thức hiện thao tác này !", "Thông báo");
                return;
            }
            this.Hide();
            QuanLyTang tienIch = new QuanLyTang();
            tienIch.ShowDialog();
            this.Close();
        }


        private void Btn_quanlydv_Click(object sender, EventArgs e)
        {
            if (Session.Roleid != 1 && Session.Roleid != 2)
            {
                MessageBox.Show("Bạn không có quyền thức hiện thao tác này !", "Thông báo");
                return;
            }
            this.Hide();
            QuanLyDichVu qlp = new QuanLyDichVu
                ();
            qlp.ShowDialog();
            this.Close();
        }

        private void Btn_quanlyphong_Click(object sender, EventArgs e)
        {
            if (Session.Roleid != 1 && Session.Roleid != 2)
            {
                MessageBox.Show("Bạn không có quyền thức hiện thao tác này !", "Thông báo");
                return;
            }
            this.Hide();
            QuanLyPhong qlp = new QuanLyPhong
                ();
            qlp.ShowDialog();
            this.Close();

        }

        private void Btn_chatbot_Click(object sender, EventArgs e)
        {
            this.Hide();
            Chat tc = new Chat();
            tc.ShowDialog();
            this.Close();
        }

        private void DoiKhuVucLamViec(object sender, EventArgs e)
        {
            DialogResult rel = MessageBox.Show("Để thực hiện chức năng này cần phải đổi khu vực làm việc, Đồng ý ?", "Thông báo", MessageBoxButtons.YesNo);
            if (rel == DialogResult.Yes)
            {
                DoiTrang();
            }
            else return;
        }
        private void DoiTrang()
        {
            this.Hide();
            TrangChu tc = new TrangChu();
            tc.ShowDialog();
            this.Close();
        }
        private void Btn_tracuukhachhang_Click(object sender, EventArgs e)
        {
            AcctionForm tracuu = new AcctionForm(1);
            tracuu.ShowDialog();
        }

        private void Btn_dangkykhachhang_Click(object sender, EventArgs e)
        {
            int acction = 3;//đăng ký tài khoản khách hàng
            Session.Acction_status = false;
            AcctionForm dk = new AcctionForm(acction);
            dk.ShowDialog();
        }

        private void QuanLyLoaiPhong_Load(object sender, EventArgs e)
        {
            LoadData();
            LockText(true); // Khóa textbox khi mới load
            btn_save.Enabled = false;
        }

        // Hàm khóa/mở khóa các textbox
        private void LockText(bool lockState)
        {
            txt_maloai.Enabled = false; 
            txt_mota.Enabled = !lockState;
            txt_giathue.Enabled = !lockState;
            txt_tilephuthu.Enabled = !lockState;
            txt_anh.Enabled = !lockState;
        }

        // Hàm xóa trắng textbox để nhập mới
        private void ClearText()
        {
            txt_maloai.Text = "";
            txt_mota.Text = "";
            txt_giathue.Text = "";
            txt_tilephuthu.Text = "";
            txt_anh.Text = "";
        }

        private void LoadData()
        {
            try
            {
                var Loaiphong = xl.GetLoaiPhong();
                dataGridView1.DataSource = Loaiphong;

                // Xóa binding cũ để tránh lỗi trùng lặp
                txt_maloai.DataBindings.Clear();
                txt_mota.DataBindings.Clear();
                txt_giathue.DataBindings.Clear();
                txt_tilephuthu.DataBindings.Clear();
                txt_anh.DataBindings.Clear();

                // Tạo binding mới
                // Cấu trúc: DataBindings.Add("Thuộc tính của Control", DataSource, "Tên cột trong DB")
                txt_maloai.DataBindings.Add("Text", dataGridView1.DataSource, "loai_phong");
                txt_mota.DataBindings.Add("Text", dataGridView1.DataSource, "mo_ta");
                txt_giathue.DataBindings.Add("Text", dataGridView1.DataSource, "gia", true, DataSourceUpdateMode.Never, "", "N0"); // Format số
                txt_tilephuthu.DataBindings.Add("Text", dataGridView1.DataSource, "ti_le_phu_thu");
                txt_anh.DataBindings.Add("Text", dataGridView1.DataSource, "anh");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Khi chọn dòng thì cho phép thao tác các nút, nhưng khóa Textbox
            btn_add.Enabled = true;
            btn_change.Enabled = true;
            btn_delete.Enabled = true;
            btn_save.Enabled = false; // Chưa bấm thêm/sửa thì chưa cho lưu
            LockText(true);
            acction = 0;
        }

        private void Btn_add_Click(object sender, EventArgs e)
        {
            acction = 1; // Chế độ Thêm
            ClearText();
            LockText(false);
            ClearBindings();
            txt_maloai.Enabled = false;

            btn_save.Enabled = true;
            btn_change.Enabled = false;
            btn_delete.Enabled = false;
        }

        private void Btn_change_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            acction = 2; // Chế độ Sửa
            LockText(false);
            txt_maloai.Enabled = false; // Không cho sửa khóa chính
            ClearBindings();
            btn_save.Enabled = true;
            btn_add.Enabled = false;
            btn_delete.Enabled = false;
        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || string.IsNullOrEmpty(txt_maloai.Text)) return;

            int ma = int.Parse(txt_maloai.Text);

            // Kiểm tra ràng buộc dữ liệu (hàm có sẵn trong Xuly)
            if (xl.check_loai_phong(ma))
            {
                MessageBox.Show("Loại phòng này đang được sử dụng trong danh sách phòng, không thể xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa loại phòng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (xl.Delete_Loai_Phong(ma))
                {
                    MessageBox.Show("Đã xóa thành công!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }
        private void ClearBindings()
        {
            txt_maloai.DataBindings.Clear();
            txt_mota.DataBindings.Clear();
            txt_giathue.DataBindings.Clear();
            txt_tilephuthu.DataBindings.Clear();
            txt_anh.DataBindings.Clear();
        }
        private void Btn_save_Click(object sender, EventArgs e)
        {
            
            // 1. Chốt dữ liệu đang nhập trên giao diện (quan trọng với DataBinding)
            this.BindingContext[dataGridView1.DataSource].EndCurrentEdit();

            // 2. Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(txt_mota.Text))
            {
                MessageBox.Show("Vui lòng nhập mô tả!");
                txt_mota.Focus();
                return;
            }

            // 3. Tạo đối tượng và lấy dữ liệu
            tblLoaiPhong lp = new tblLoaiPhong();
            try
            {
                lp.mo_ta = txt_mota.Text.Trim();

                // Dùng decimal cho tiền tệ, double cho tỉ lệ (tùy theo DB của bạn)
                // Lưu ý: txt_giathue nếu đang format có dấu phẩy (100,000) thì phải xử lý
                string giaClean = txt_giathue.Text.Replace(",", "").Replace(".", "");
                lp.gia = string.IsNullOrEmpty(txt_giathue.Text) ? 0 : Double.Parse(txt_giathue.Text);

                lp.ti_le_phu_thu = string.IsNullOrEmpty(txt_tilephuthu.Text) ? 0 : int.Parse(txt_tilephuthu.Text);

                lp.anh = txt_anh.Text.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dữ liệu số không hợp lệ (Giá hoặc Tỉ lệ)! \nChi tiết: " + ex.Message);
                return;
            }

            bool kq = false;
            switch (acction)
            {
                case 1: // ADD
                    kq = xl.AddLoai(lp);
                    break;

                case 2: // EDIT
                        // Lấy ID chuẩn từ textbox mã loại (đang bị khóa/ẩn)
                    if (int.TryParse(txt_maloai.Text, out int maLoai))
                    {
                        lp.loai_phong = maLoai;
                        kq = xl.UpdateLoai(lp);
                    }
                    else
                    {
                        MessageBox.Show("Không xác định được mã loại phòng cần sửa!");
                        return;
                    }
                    break;
                   
            }

            if (kq)
            {
                MessageBox.Show("Lưu thành công!");
                LoadData(); // Load lại dữ liệu mới từ DB

                // Reset trạng thái nút
                btn_add.Enabled = true;
                btn_delete.Enabled = true;
                btn_change.Enabled = true;
                btn_save.Enabled = false;

                // Khóa lại các ô nhập liệu
                txt_mota.Enabled = false;
                txt_giathue.Enabled = false;
                txt_tilephuthu.Enabled = false;
                txt_anh.Enabled = false;

                acction = 0;
            }
            else
            {
                MessageBox.Show("Lưu thất bại! Vui lòng kiểm tra lại kết nối hoặc dữ liệu.");
            }
            LoadData();
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            DialogResult rel = MessageBox.Show("Bạn chắc muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo);
            if (rel == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
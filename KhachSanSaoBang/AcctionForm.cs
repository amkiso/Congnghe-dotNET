using KhachSanSaoBang.Models;
using KhachSanSaoBang.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang
{
    public partial class AcctionForm : Form
    {
        
        Xuly xl = new Xuly();
        BindingList<KhachThue> danhSachKhach;
        tblPhieuDatPhong pdp = new tblPhieuDatPhong();
        PhongTrong pt = new PhongTrong();
        KhachThue newRow;
        string makh;
        DateTime ngayvao, ngayra;
        bool isAdding = false;
        bool isEditing = false;
        PhieuNhanPhong phieu = new PhieuNhanPhong();
        public string jsonstring { get; private set; }

        int current_step = 0;
        int acction = 0;
        public AcctionForm(int Acction)
        {
            this.acction = Acction;
            InitializeComponent();
            txt_input.Click += Txt_input_Click;
            btn_tracuu.Click += Btn_tracuu_Click;
            tab_container.SelectedIndexChanged += Tab_container_SelectedIndexChanged;
            btn_back_step.Click += Btn_back_step_Click;
            this.Load += Form1_Load;
            btn_next_step.Click += Btn_next_step_Click;
            btn_them_acction2.Click += Btn_them_acction2_Click;
            btn_luu_acction2.Click += btn_luu_acction2_Click;
            btn_xoa_acction2.Click += btn_xoa_acction2_Click;
            btn_loc.Click += Btn_loc_Click;
            btn_dangky.Click += Btn_dangky_Click;
            btn_chonP.Click += Btn_chonP_Click;
            data_phongtrong.SelectionChanged += Data_phongtrong_SelectionChanged;
            btn_sua_acction2.Click += Btn_sua_acction2_Click;
            data_khach_di_cung.SelectionChanged += Data_khach_di_cung_SelectionChanged;
            btn_cancel.Click += Btn_cancel_Click;
            data_khach_di_cung.CellValidating += Data_khach_di_cung_CellValidating;
        }

        private void Btn_dangky_Click(object sender, EventArgs e)
        {
            tblKhachHang kh = new tblKhachHang();
            kh.ho_ten = txt_hoten_dangky.Text;
            kh.ma_kh = txt_taikhoandk.Text;
            kh.mat_khau = txt_matkhau_dangky.Text;
            if(txt_cccd_dangky.Text.Length > 10 && txt_sdt_dangky.Text.Length >10)
            {
                kh.cmt = txt_cccd_dangky.Text;
                kh.sdt = txt_sdt_dangky.Text;
            }
            else { MessageBox.Show("Số điện thoại và số căn cước không hợp lệ !","Thông báo"); return; }
            kh.mail = txt_email_dangky.Text;
            kh.ngsinh = dpt_ngsinh_dkKH.Value;
            if (xl.CreateNewKhachHang(kh)) { MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo"); Session.Acction_status = true;  tab_container.SelectedTab = step1; }
            else MessageBox.Show("Đăng ký tài khoản không thành công!", "Lỗi");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tab_container.ItemSize = new Size(0, 1);
            if (acction == 0)
            {
                this.Text = "Đặt phòng";
                tab_container.SelectedTab = step1;
            }
            if (acction == 2)
            {
                tab_container.SelectedTab = tabkhachdicung;
                btn_back_step.Enabled = false;
                this.Text = "Thêm khách thuê";
                danhSachKhach = new BindingList<KhachThue>();
                data_khach_di_cung.DataSource = danhSachKhach;

                data_khach_di_cung.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                data_khach_di_cung.MultiSelect = false;
                data_khach_di_cung.ReadOnly = true; // mặc định chỉ xem
                data_khach_di_cung.AllowUserToAddRows = false; // ẩn hàng thêm tự động

                btn_xoa_acction2.Enabled = false;
                btn_sua_acction2.Enabled = false;
                btn_luu_acction2.Enabled = false;
            }
            if (acction == 1)
            {
                this.Text = "Tra cứu thông tin khách hàng";
                tab_container.SelectedIndex = 0;
            }
            if (acction == 3)
            {
                this.Text = "Đăng ký tài khoản khách hàng";
                tab_container.SelectedTab = tab_khachhang_register;
            }
            
            step4_center_bot_container.CellPaint += (s, ev) => Dataloader.DrawTableFullCellBorder(s, ev, Color.Azure);
            step4_center_container.CellPaint += (s, ev) => Dataloader.DrawTableFullCellBorder(s, ev, Color.Azure);
            //step2_lower_container.CellPaint += (s, ev) => Dataloader.DrawTableFullCellBorder(s, ev, Color.Black);
            step2_lower_container.BackColor = step1_lower_container.BackColor = step3_bot_container.BackColor = form_dangky_KH.BackColor = step4_center_container.BackColor = Color.FromArgb(120, 0, 0, 0);
            step2_lower_container.Visible = step1_lower_container.Visible = step4_center_container.Visible = data_phongtrong.Visible = form_dangky_KH.Visible = step3_bot_container.Visible = true;
        }

        private void Btn_cancel_Click(object sender, EventArgs e)
        {
            if (Session.Acction_status) { this.Close(); }
            DialogResult rel = MessageBox.Show($"Thoát {this.Text} ?, mọi thông tin sẽ bị hủy bỏ ?", "Thông báo", MessageBoxButtons.YesNo);
            if (rel == DialogResult.Yes) { Session.Acction_status = false; this.Close(); }
        }

        private void Btn_chonP_Click(object sender, EventArgs e)
        {
            if (data_phongtrong.CurrentRow != null)
            {
                pt = data_phongtrong.CurrentRow.DataBoundItem as PhongTrong;

                if (pt != null)
                {
                    // dùng biến dangChon ở đây
                    DialogResult rel = MessageBox.Show($"Xác nhận chọn phòng {pt.Tenphong}?", "Thông báo", MessageBoxButtons.YesNo); 
                    if (rel == DialogResult.Yes) {
                        pdp.ma_phong=pt.Maphong;
                        pdp.ngay_dat = DateTime.Now;
                        pdp.ngay_ra = ngayra;
                        pdp.ngay_vao = ngayvao;
                        pdp.ma_tinh_trang = 1;
                        lbl_sophong_step4.Text = pt.Tenphong;
                        lbl_giathue_step4.Text = pt.Giathue.ToString("N0")+"VNĐ";
                        lbl_loaiphong_step4.Text = pt.Tenloai;
                        lbl_ngaydat_step4.Text = pdp.ngay_dat.ToString();
                        lbl_ngayra_step4.Text = ngayra.ToString();
                        lbl_ngayvao_step4.Text = ngayvao.ToString();
                        lbl_nhanvien.Text = Session.UserName;
                        lbl_sophieudat.Text = xl.GetNextMaPdp().ToString();
                        lbl_tienich_step4.Text = pt.Tentienich;
                        btn_inphieu.Click += Btn_inphieu_Click;
                        
                        btn_xacnhan_step4.Click += Btn_xacnhan_step4_Click;
                        tab_container.SelectedTab = step4;

                    }
                }
            }
            else { MessageBox.Show("Vui lòng chọn 1 phòng trong danh sách","Thông báo"); }
        }

        private void Txt_input_Click(object sender, EventArgs e)
        {
            txt_input.Clear();
        }

        private void Btn_xacnhan_step4_Click(object sender, EventArgs e)
        {
            if (xl.CreateNewPhieuDatPhong(pdp))
            {
                DialogResult rel = MessageBox.Show("Đặt phòng thành công !, Bạn có muốn in lại phiếu nhận không ?", "Thông báo",MessageBoxButtons.YesNo);
                if (rel == DialogResult.Yes) { inphieudatphong(); }
                this.Close();
                Session.Acction_status = true;
            }
            else { MessageBox.Show("Đặt phòng thất bại!", "Lỗi"); }
        }

        private void Btn_inphieu_Click(object sender, EventArgs e)
        {
            inphieudatphong();
            

        }
        private void inphieudatphong()
        {
            phieu.Tenkh = lbl_tenkh.Text;
            phieu.Mapdp = lbl_sophieudat.Text;
            phieu.Loaiphong = lbl_loaiphong_step4.Text;
            phieu.Giathue = lbl_giathue_step4.Text;
            phieu.Tienich = lbl_tienich_step4.Text;
            phieu.Ngayvao = ngayvao;
            phieu.Ngayra = ngayra;
            
            phieu.Ngaydat = DateTime.Now;
            phieu.Sdt = lbl_sdt_step4.Text;
            MauPhieuNhanDatPhong printer = new MauPhieuNhanDatPhong(phieu, "");
            printer.PrintPreview();
            printer.Dispose();
        }

        private void Data_phongtrong_SelectionChanged(object sender, EventArgs e)
        {
            btn_chonP.Enabled = true;
        }

        private void Btn_loc_Click(object sender, EventArgs e)
        {
            
            ngayra = dtp_ngayra.Value;
            ngayvao = dtp_ngayvao.Value;
            List<PhongTrong> phongtrongs   = xl.GetPhongTrongs(ngayvao,ngayra);
            data_phongtrong.AutoGenerateColumns = false;
            data_phongtrong.DataSource = phongtrongs;
        }

        
        private void Data_khach_di_cung_SelectionChanged(object sender, EventArgs e)
        {
            if (data_khach_di_cung.SelectedRows.Count > 0 && !isAdding)
            {
                btn_xoa_acction2.Enabled = true;
                btn_sua_acction2.Enabled = true;
            }
            else
            {
                btn_xoa_acction2.Enabled = false;
                btn_sua_acction2.Enabled = false;
            }
        }

        private void Btn_them_acction2_Click(object sender, EventArgs e)
        {
            if (isAdding) return;

            newRow = new KhachThue(); // tạo hàng trống
            danhSachKhach.Add(newRow);

            data_khach_di_cung.ClearSelection();
            int rowIndex = danhSachKhach.IndexOf(newRow);
            data_khach_di_cung.Rows[rowIndex].Selected = true;
            data_khach_di_cung.CurrentCell = data_khach_di_cung.Rows[rowIndex].Cells[0];

            data_khach_di_cung.ReadOnly = false; // cho phép nhập dữ liệu
            btn_luu_acction2.Enabled = true;

            isAdding = true;
        }

        private void Btn_sua_acction2_Click(object sender, EventArgs e)
        {
            if (data_khach_di_cung.SelectedRows.Count > 0)
            {
                data_khach_di_cung.ReadOnly = false;
                isEditing = true;
                btn_luu_acction2.Enabled = true;
            }
        }

        private void btn_xoa_acction2_Click(object sender, EventArgs e)
        {
            if (data_khach_di_cung.SelectedRows.Count > 0)
            {
                var row = data_khach_di_cung.SelectedRows[0].DataBoundItem as KhachThue;
                if (row != null)
                {
                    danhSachKhach.Remove(row);
                }
            }
        }

        private void btn_luu_acction2_Click(object sender, EventArgs e)
        {
            if ((!isAdding && !isEditing) || newRow == null)
            {
                MessageBox.Show("Không có hàng để lưu!");
                return;
            }

            try
            {
                // Commit dữ liệu đang edit
                data_khach_di_cung.EndEdit();
                data_khach_di_cung.CurrentCell = null;

                // Validate ngày sinh và tính tuổi
                DateTime ngsinh;
                DateTime.TryParseExact(newRow.Ngsinh, new[] { "dd/MM/yyyy", "yyyy/MM/dd" }, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out ngsinh);
                newRow.Tuoi = TinhTuoi(ngsinh);

                // Kiểm tra dữ liệu bắt buộc
                if (string.IsNullOrWhiteSpace(newRow.Tenkh))
                {
                    danhSachKhach.Remove(newRow);
                    MessageBox.Show("Tên khách hàng không được để trống!");
                }
            }
            catch
            {
                MessageBox.Show("Định dạng ngày sinh không hợp lệ!, vui lòng nhập theo định dạng dd/MM/yyyy hoặc yyyy/MM/dd", "Thông báo");
                return;
            }

            data_khach_di_cung.ReadOnly = true;
            btn_luu_acction2.Enabled = false;

            isAdding = false;
            isEditing = false;
            newRow = null;

            data_khach_di_cung.ClearSelection();
            data_khach_di_cung.Refresh();
        }

        private void Data_khach_di_cung_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Kiểm tra cột ngày sinh (giả sử tên cột là "Ngsinh")
            if (data_khach_di_cung.Columns[e.ColumnIndex].Name == "Ngsinh")
            {
                string input = e.FormattedValue.ToString();
                if (!DateTime.TryParseExact(input, new[] { "dd/MM/yyyy", "yyyy/MM/dd" },
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    e.Cancel = true;
                    MessageBox.Show("Ngày sinh không hợp lệ. Nhập theo dd/MM/yyyy hoặc yyyy/MM/dd");
                }
                else
                {
                    data_khach_di_cung.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = date;
                }
            }
        }

        private void Tab_container_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tab = tab_container.SelectedTab;
            current_step = tab_container.SelectedIndex;
            if (acction != 2)
            {
                if(current_step== tab_container.TabCount - 2)
                {
                    btn_next_step.Enabled = false;
                }   else { btn_next_step.Enabled=true; }  

            }
            if (tab_container.SelectedIndex == 0)
            {
                btn_back_step.Enabled = false;
            }
            else { btn_back_step.Enabled = true; }
            
            
        }

        private void Btn_next_step_Click(object sender, EventArgs e)
        {
            int maxstep;
            int thisstep = tab_container.SelectedIndex;
            void btn_acction(){
                tab_container.SelectedIndex = current_step + 1;
                if (current_step == maxstep) btn_next_step.Enabled = false;
                btn_back_step.Enabled = true;
            }
            
            if (acction == 0)
            {
                if (thisstep == 1) { pdp.ma_kh = makh; }
                maxstep = tab_container.TabCount - 3;
                btn_acction();
               

            }
            else if (acction==2){
                jsonstring = xl.ThongtinKH_To_Json(danhSachKhach.ToList());
                this.Close();
            }
            else if (acction == 1) { maxstep = 1;
                btn_acction();
                this.Close();
            }
                
            
        }

        private void Btn_back_step_Click(object sender, EventArgs e)
        {
            btn_next_step.Enabled = true;
            if (acction == 2)
            {
                DialogResult rel1 = MessageBox.Show("Bạn sẽ thoát khỏi cửa sổ này, đồng ý ?", "Thông báo");
                if (rel1 != DialogResult.Yes) { this.Close(); }
            }
            if (tab_container.SelectedTab == tab_khachhang_register && acction !=3)
            {
                tab_container.SelectedIndex = 0;

            } else if (acction == 3)
            {
                DialogResult rel = MessageBox.Show("Hủy đăng kí khách hàng ?", "Thông báo", MessageBoxButtons.YesNo);
                if (rel == DialogResult.Yes)
                {
                    Session.Acction_status = false;
                    this.Close();
                }
                else return;
                }
            else
            {
                tab_container.SelectedIndex = current_step - 1;
                current_step = tab_container.SelectedIndex;
                if (current_step == 0) btn_back_step.Enabled = false;
            }
        }

        private void Btn_tracuu_Click(object sender, EventArgs e)
        {
            if (xl.ChecKH(txt_input.Text))
            {
                ThongtinTracuu kh = xl.GetThongtinKH(txt_input.Text);
                if (kh != null)
                {
                    string listChoXN = kh.Phong_cho_xac_nhan == null ? "0" : string.Join(", ", kh.Phong_cho_xac_nhan);
                    string listchk = kh.Phong_dang_dat == null ? "0" : string.Join(", ", kh.Phong_dang_dat);
                    if (listchk == ""|| listchk==null) { listchk = "Trống"; }
                    if (listChoXN == null||listChoXN=="") { listChoXN = "Trống"; }
                    lbl_tenkh.Text = kh.Hoten;
                    lbl_cccd.Text = kh.Cccd;
                    lbl_sdt.Text = kh.Sdt;
                    lbl_cacphongcheckin.Text = listchk;
                    lbl_cacphongchoxacnhan.Text = listChoXN;
                    lbl_diemtichluy.Text = kh.Diem_tich_luy.ToString();
                    lbl_sophongchocheckin.Text = kh.So_phong_dang_dat.ToString();
                    lbl_sophongchoxacnhan.Text = kh.So_Phong_chua_Xac_nhan.ToString();
                    txt_input.Focus();
                    makh = kh.Makh;
                    lbl_tenkh_step4.Text = kh.Hoten;
                    lbl_cccd_step4.Text = kh.Cccd;
                    lbl_sdt_step4.Text = kh.Sdt;
                    
                    current_step = tab_container.SelectedIndex = 1;
                    btn_back_step.Enabled = true;
                }
                else
                {
                    DialogResult rel = MessageBox.Show("Không tìm thấy thông tin khách hàng! Chuyển đến tạo tài khoản và thuê phòng?", "Thông báo", MessageBoxButtons.YesNo);
                    if (rel == DialogResult.No) this.Close();
                }
            }
            else
            {
                DialogResult rel = MessageBox.Show("Không tìm thấy thông tin khách hàng! Chuyển đến tạo tài khoản và thuê phòng?", "Thông báo", MessageBoxButtons.YesNo);
                if (rel == DialogResult.No) {
                    MessageBox.Show("Khách hàng phải có tài khoản mới có thể đặt phòng !", "Thông báo");
                    Session.Acction_status = false;
                    this.Close(); }
                else
                {
                    tab_container.SelectedTab = tab_khachhang_register;
                    
                }
            }
        }
        private int TinhTuoi(DateTime ngsinh)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - ngsinh.Year;
            if (ngsinh.Date > today.AddYears(-age)) age--; // nếu chưa tới sinh nhật năm nay
            return age;
        }
    }
}

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
    public partial class Form1 : Form
    {
        int acction = 0;
        Xuly xl = new Xuly();
        BindingList<KhachThue> danhSachKhach;
        KhachThue newRow; // để lưu hàng thêm tạm thời
        bool isAdding = false;
        bool isEditing = false;
        public string jsonstring { get; private set; }

        int current_step = 0;

        public Form1(int Acction)
        {
            this.acction = Acction;
            InitializeComponent();

            btn_tracuu.Click += Btn_tracuu_Click;
            tab_container.SelectedIndexChanged += Tab_container_SelectedIndexChanged;
            btn_back_step.Click += Btn_back_step_Click;
            this.Load += Form1_Load;
            btn_next_step.Click += Btn_next_step_Click;
            btn_them_acction2.Click += Btn_them_acction2_Click;
            btn_luu_acction2.Click += btn_luu_acction2_Click;
            btn_xoa_acction2.Click += btn_xoa_acction2_Click;

            btn_sua_acction2.Click += Btn_sua_acction2_Click;
            data_khach_di_cung.SelectionChanged += Data_khach_di_cung_SelectionChanged;
            data_khach_di_cung.CellValidating += Data_khach_di_cung_CellValidating;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (acction == 0)
            {
                tab_container.SelectedIndex = 0;
            }
            if (acction == 2)
            {
                tab_container.SelectedIndex = 5;
                btn_back_step.Enabled = false;

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
            else if (acction == 1) {
                tab_container.SelectedIndex = 0;
            }
            //step2_lower_container.CellPaint += (s, ev) => Dataloader.DrawTableFullCellBorder(s, ev, Color.Black);
            step2_lower_container.BackColor = step1_lower_container.BackColor = Color.FromArgb(120, 0, 0, 0);
            step2_lower_container.Visible = step1_lower_container.Visible = true;
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
            void btn_acction(){
                tab_container.SelectedIndex = current_step + 1;
                if (current_step == maxstep) btn_next_step.Enabled = false;
                btn_back_step.Enabled = true;
            }
            
            if (acction == 0)
            {
                maxstep = tab_container.TabCount - 2;
               

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
            tab_container.SelectedIndex = current_step - 1;
            current_step = tab_container.SelectedIndex;
            if (current_step == 0) btn_back_step.Enabled = false;
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
                if (rel == DialogResult.No) this.Close();
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            txt_input.Text = string.Empty;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            MessageBox.Show($"Chiều rộng: {w}  Chiều cao: {h}", "Thông báo");
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

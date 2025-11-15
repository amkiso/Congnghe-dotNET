using KhachSanSaoBang.Models;
using KhachSanSaoBang.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace KhachSanSaoBang
{
    public partial class MainForm : Form
    {
        bool isMenuCollapsed = false;
        Xuly xl = new Xuly();
        private string lastSelectedValue = "";
        private int clickCount = 0;
        int counter = 0;
        private bool isLoading = false;
        int checktest = 0;
        bool isShowingMessage = false;

        private void ToggleSubMenu(Panel submenu)
        {
            if (!submenu.Visible)
            {
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }
        }
        private void Btn_Toggle_menu_Click(object sender, EventArgs e)
        {
            if (pnl_menu.Visible) { pnl_menu.Visible = false; }
            else pnl_menu.Visible = true;
            if (isMenuCollapsed)
            {
                // Mở rộng lại menu
                tableLayoutPanel1.ColumnStyles[0].Width = 15;
                tableLayoutPanel1.ColumnStyles[1].Width = 70;
            }
            else
            {
                // Thu nhỏ menu
                tableLayoutPanel1.ColumnStyles[0].Width = 1;
                tableLayoutPanel1.ColumnStyles[1].Width = 84;
            }
            
            isMenuCollapsed = !isMenuCollapsed;
        }
        private void btnKhachSan_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlKhachSanSub);
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlDichVuSub);
        }

        private void pnlKhachhang_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlKhachHangSub);
        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlDoanhThuSub);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlThongKeSub);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ToggleSubMenu(pnlNhanVienSub);
        }
        public MainForm()
        {
            InitializeComponent();
            this.Load += Mainform_Load;
            // Toggle menu chính
            list_dichvu.SelectedIndexChanged += list_dichvu_SelectedIndexChanged;
            this.btn_Toggle_menu.Click += Btn_Toggle_menu_Click;
            lbl_Nhanvien.Text = Session.UserName;
            lbl_chucvu.Text = Session.Role;
            // Các nút con
            btnKhachSan.Click += btnKhachSan_Click;
            btnDichVu.Click += btnDichVu_Click;
            pnlKhachhang.Click += pnlKhachhang_Click;
            btn_thoatca.Click += Btn_thoatca_Click;
            btnDoanhThu.Click += btnDoanhThu_Click;
            btnThongKe.Click += btnThongKe_Click;
            btn_nhanvien.Click += button1_Click;
            btn_TracuuKhachHang.Click += Btn_tracuu_Click;
            btn_datphong.Click += Btn_datphong_Click;
            this.timer1.Tick += Timer1_Tick; //timer
            //Sự kiện phòng Tầng 1
            btn_p101.Click += Sophong_click;
            btn_p102.Click += Sophong_click;
            btn_p103.Click += Sophong_click;
            btn_p104.Click += Sophong_click;
            btn_p105.Click += Sophong_click;
            btn_p106.Click += Sophong_click;
            //Sự kiện phòng Tầng 2
            btn_p201.Click += Sophong_click;
            btn_p202.Click += Sophong_click;
            btn_p203.Click += Sophong_click;
            btn_p204.Click += Sophong_click;
            btn_p205.Click += Sophong_click;
            btn_p206.Click += Sophong_click;
            //Sự kiện phòng Tầng 3
            btn_p301.Click += Sophong_click;
            btn_p302.Click += Sophong_click;
            btn_p303.Click += Sophong_click;
            btn_p304.Click += Sophong_click;
            btn_p305.Click += Sophong_click;
            btn_p306.Click += Sophong_click;
            //Sự kiện liên quan đến thao tác nghiệp vụ
            btn_goidv.Click += Btn_goidv_Click;
            btn_trahang.Click += Btn_trahang_Click;
            btn_dangkyKH.Click += Btn_dangkyKH_Click;
            btn_thanhtoan.Click += Btn_thanhtoan_Click;
            cbo_tinhtrang.SelectedIndexChanged += Cbo_tinhtrang_SelectedIndexChanged;
            list_dichvu.Click += List_dichvu_Click;
        }

        private void Btn_thoatca_Click(object sender, EventArgs e)
        {
            Session.Reset();
            this.Hide();
            Dangnhap dn = new Dangnhap();
            dn.ShowDialog();
            this.Close();
        }

        private void Btn_dangkyKH_Click(object sender, EventArgs e)
        {
            int acction = 3;//đăng ký tài khoản khách hàng
            Session.Acction_status = false;
            AcctionForm dk = new AcctionForm(acction);
            dk.ShowDialog();
        }

        private void Btn_datphong_Click(object sender, EventArgs e)
        {
            int acction = 0;
            AcctionForm datp = new AcctionForm(acction);
            Session.Acction_status = false;
            datp.ShowDialog();
            if(Session.Acction_status == false)
            {
                MessageBox.Show("Bạn đã hủy đặt phòng !", "Thông báo");
            }
        }

        private void Btn_tracuu_Click(object sender, EventArgs e)
        {
            AcctionForm tracuu = new AcctionForm(1);
            tracuu.ShowDialog();
        }
        private void List_dichvu_Click(object sender, EventArgs e)
        {
            if (list_dichvu.SelectedValue == null)
                return;

            string currentValue = list_dichvu.SelectedValue.ToString();

            if (currentValue == lastSelectedValue)
            {
                // Nếu chọn lại cùng item : tăng số lượng
                clickCount++;
            }
            else
            {
                // Nếu chọn item khác : reset về 1
                clickCount = 1;
                lastSelectedValue = currentValue;
            }

            txt_sl_goi.Text = clickCount.ToString();
        }
        private void Cbo_tinhtrang_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (isLoading) return; //bỏ qua sự kiện nếu đang load thông tin phòng khác
            else
            {
                if (Session.maphonghientai != 0)
                {
                    int new_values, old_values;
                    old_values = xl.GetCodeRoomStatus(Session.maphonghientai);
                    string trangthai_new = null, trangthai_old;
                    new_values = (int)cbo_tinhtrang.SelectedValue;
                    trangthai_old = xl.GetStringRoomStatus(old_values);
                    if (cbo_tinhtrang.SelectedItem is tblTinhTrangPhong ttr)
                    {
                        trangthai_new = ttr.mo_ta;

                    }

                    Thongtinchung tt = xl.GetThongtinphong(Session.maphonghientai);
                    DialogResult result = MessageBox.Show($"Bạn có chắc muốn thay đổi trạng thái của phòng {tt.Tenphong} \n từ '{trangthai_old}' thành '{trangthai_new}' ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        switch (xl.GetRoomCase(old_values, new_values))
                        {
                            case 1:
                                {
                                    int acction = 1;
                                    Session.Acction_status = false;
                                    //thay đổi từ 1-2: Mở cửa sổ nhập thông tin khách hàng thuê phòng, set ngày giờ vào, phiếu đặt phòng và tạo hóa đơn
                                    AcctionForm thuep = new AcctionForm(acction);
                                    thuep.ShowDialog();

                                    if (Session.Acction_status)
                                    {
                                        Mainform_Load(this, EventArgs.Empty);
                                        MessageBox.Show("Thay đổi trạng thái phòng thành công !", "THông báo");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Thay đổi trạng thái phòng thất bại, bạn đã hủy thuê phòng !", "THông báo");
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    /*thay đổi từ 2-3: Kiểm tra xem phòng có được thanh toán hay chưa, 
                                             nếu đã thay toán thì cho phép đổi trạng thái trực tiếp
                                             Nếu chưa thay toán thì thông báo "Phát hiện phòng này chưa thanh toán, 
                                             bạn có muốn thanh toán hóa đơn cho phòng này không ?(Yes/No)*/
                                    if (xl.CheckHoaDon(Session.maphonghientai))
                                    {
                                        if (xl.ChangeRoomStatus(Session.maphonghientai, new_values)) { MessageBox.Show("Thay đổi trạng thái phòng thành công !", "THông báo"); }
                                        else
                                        {
                                            MessageBox.Show("Thay đổi trạng thái phòng thất bại, có lỗi xảy ra khi đổi trạng thái phòng !", "THông báo");
                                        }
                                    }
                                    else
                                    {
                                        if (Session.maphonghientai != 0)
                                        {
                                            DialogResult rls = MessageBox.Show($"Xác nhận chuyển đến trang thanh toán phòng {tt.Tenphong} ?", "Thông báo", MessageBoxButtons.YesNo);
                                            if (rls == DialogResult.Yes)
                                            {
                                                ThanhToan pm = new ThanhToan(xl.GetThongtinThanhToan(Session.maphonghientai));
                                                pm.ShowDialog(this);

                                            }
                                            else return;
                                        }
                                        else { MessageBox.Show("Bạn chưa chọn phòng để thanh toán!", "Thông báo"); }
                                    }

                                    break;
                                }
                            case 3:
                                {
                                    //thay đổi từ 3-1: Thông báo hỏi xác nhận đã dọn phòng xong ?(Yes/No) Yes-> thay đổi trạng thái, No->return
                                    DialogResult rls = MessageBox.Show("Xác nhận đã dọn phòng xong ?", "Thông báo", MessageBoxButtons.YesNo);
                                    if (rls == DialogResult.Yes)
                                    {
                                        xl.ChangeRoomStatus(Session.maphonghientai, 1);//Chuyển thành trống
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    //thay đổi từ 1-6: Không thể thay đổi trạng thái từ 1->6 vì chức năng này thuộc về người dùng đặt phòng trên website
                                    MessageBox.Show("Trạng thái phòng không hợp lệ !", "Lỗi");
                                    break;
                                }
                            case 5:
                                {
                                    //thay đổi từ 6-7: Thông báo hỏi xác nhận phòng ? (Yes/No)
                                    DialogResult rel = MessageBox.Show("Xác nhận nhận phòng khách đặt ?", "Thông báo", MessageBoxButtons.YesNo);
                                    if (rel == DialogResult.Yes)
                                    {
                                        //Đổi trạng thái phòng, trạng thái phiếu đặt, tạo hóa đơn mới
                                        if (xl.XacNhanPhong(Session.maphonghientai))
                                        {
                                            FormStart();
                                            MessageBox.Show("Đã thay đổi trạng thái phòng thành công !", "Thông báo");
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }
                                    break;
                                }
                            case 6:
                                {
                                    //đổi từ 7-2: Hiện cửa sổ nhập thông tin khách nhận phòng,Ghi lại ngày vào (ngay_vao) trong database thành ngày giờ hiện tại, Cập nhật dữ liệu trên db
                                    Session.Acction_status = false;

                                    break;
                                }
                            case 0:
                                {
                                    //Các trạng thái 4,5 của phòng chỉ có thể thay đổi khi phòng trống(1)
                                    if (tt.Trangthai == 1)
                                    {
                                        xl.ChangeRoomStatus(Session.maphonghientai, new_values);
                                    }
                                    else MessageBox.Show("Phòng đang được sử dụng !", "Thông báo");
                                    break;
                                }
                            case -1:
                                {
                                    // Ngoài ra không chấp nhận thay đổi khác với luồng này
                                    MessageBox.Show($"Không thể thay đổi trạng thái {tt.Tenphong} \n từ '{trangthai_old}' thành '{trangthai_new}' vì không hợp lệ !");
                                    return;
                                }
                        }

                    }

                    else
                    {
                        isLoading = true;
                        cbo_tinhtrang.SelectedValue = old_values;
                        return;
                    }

                }
            }
        }
        private void Btn_thanhtoan_Click(object sender, EventArgs e)
        {
            //ẩn cửa sổ này và gọi cửa sổ thanh toán tại đây
            if (Session.maphonghientai != 0)
            {
                DialogResult rls = MessageBox.Show($"Xác nhận chuyển đến trang thanh toán phòng {Session.maphonghientai} ?", "Thông báo", MessageBoxButtons.YesNo);
                if (rls == DialogResult.Yes)
                {
                    ThanhToan pm = new ThanhToan(xl.GetThongtinThanhToan(Session.maphonghientai));
                    pm.ShowDialog(this);
                        
                }
                else return;
            }
            else { MessageBox.Show("Bạn chưa chọn phòng để thanh toán!", "Thông báo"); }

        }
        private void Sophong_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                int sophong = Convert.ToInt32(btn.Tag); // lấy giá trị từ Tag
                loadingroom(sophong);
            }
        }
        private void Btn_trahang_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn trả hàng ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (list_dichvu.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn một dịch vụ đã gọi để trả hàng.",
                                    "Chưa chọn dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Kiểm tra số lượng nhập vào (txt_sl_goi)
                if (!int.TryParse(txt_sl_goi.Text.Trim(), out int soLuongTra) || soLuongTra < 1)
                {
                    MessageBox.Show("Số lượng trả phải là một số nguyên lớn hơn 0.",
                                    "Số lượng không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_sl_goi.Focus();
                    txt_sl_goi.SelectAll();
                    return;
                }
                // 4. Xử lý logic nghiệp vụ
                int soLuongSeTra = soLuongTra; // Số lượng sẽ gọi hàm backend
                bool seTraHang = false;      // Cờ để xác định có thực hiện trả hàng không

                if (soLuongTra < (int.Parse(txt_sl_ton_dv.Text)))
                {
                    // Trả một phần
                    DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn trả lại {soLuongSeTra} ?",
                                                         "Xác nhận trả hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                        seTraHang = true;
                }
                else if (soLuongTra == (int.Parse(txt_sl_ton_dv.Text)))
                {
                    // Trả toàn bộ
                    DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn trả lại toàn bộ {soLuongSeTra} ?",
                                                         "Xác nhận trả toàn bộ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                        seTraHang = true;
                }
                else // (soLuongTra > soLuongDaGoi) -> Logic bạn yêu cầu
                {
                    DialogResult confirm = MessageBox.Show($"Số lượng trả ({soLuongTra}) lớn hơn số lượng đã gọi ({(int.Parse(txt_sl_ton_dv.Text))}).\nBạn có muốn trả lại toàn bộ {(int.Parse(txt_sl_ton_dv.Text))} không?",
                                                         "Xác nhận trả toàn bộ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirm == DialogResult.Yes)
                    {
                        soLuongSeTra = (int.Parse(txt_sl_ton_dv.Text)); // Chỉ trả đúng số lượng đã gọi
                        seTraHang = true;
                    }
                    // Nếu user chọn No, seTraHang vẫn là false và không làm gì cả
                }

                // 5. Thực hiện trả hàng nếu cờ seTraHang là true
                if (seTraHang)
                {
                    if (xl.Tradichvu(Session.maphonghientai, (int)list_dichvu.SelectedValue, soLuongSeTra))
                    {
                        MessageBox.Show("Trả hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật CACHE: Cộng trả lại số lượng
                        dsDichVu.TraLaiSoLuong((int)list_dichvu.SelectedValue, soLuongSeTra);

                        // Tải lại grid "dịch vụ đã sử dụng"
                        da_dv_su_dung.DataSource = xl.listdichvup(Session.maphonghientai);

                        // Cập nhật lại số tồn kho đang hiển thị (nếu đang chọn đúng dịch vụ đó)
                        if (list_dichvu.SelectedItem is tblDichVu dv)
                        {
                            txt_donvi_dv.Text = dv.don_vi.ToString();
                            txt_sl_ton_dv.Text = dv.ton_kho.ToString();
                            txt_gia_dv.Text = dv.gia.ToString();
                            txt_sl_goi.Focus();
                            txt_sl_goi.SelectAll();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Trả hàng thất bại. Đã có lỗi xảy ra !.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Đặt focus lại để dễ thao tác
                txt_sl_goi.Focus();
                txt_sl_goi.SelectAll();
            }
            else return;
        }
        private void Btn_goidv_Click(object sender, EventArgs e)
        {

            if (Session.maphonghientai == 0) { MessageBox.Show("Bạn chưa chọn phòng !", "Thông báo"); }
            else
            {
                //Kiểm tra số lượng nhập vào:
                if (int.TryParse(txt_sl_goi.Text.Trim(), out int so))
                {
                    if (so <= int.Parse(txt_sl_ton_dv.Text))
                    {
                        if (xl.GoiDichVu(Session.maphonghientai, (int)(list_dichvu.SelectedValue), so))
                        {
                            MessageBox.Show("Gọi dịch vụ thành công !", "Thông báo");
                            da_dv_su_dung.DataSource = xl.listdichvup(Session.maphonghientai);
                            dsDichVu.TruSoLuong((int)(list_dichvu.SelectedValue), so);
                            if (list_dichvu.SelectedItem is tblDichVu dv)
                            {
                                txt_donvi_dv.Text = dv.don_vi.ToString();
                                txt_sl_ton_dv.Text = dv.ton_kho.ToString();
                                txt_gia_dv.Text = dv.gia.ToString();
                                txt_sl_goi.Focus();
                                txt_sl_goi.SelectAll();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Gọi dịch vụ thất bại do phòng chưa có khách đặt hoặc do lỗi không xác định !", "Thông báo");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Số lượng gọi không được vượt quá số lượng tồn", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Giá trị nhập vào không phải là số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_sl_goi.Focus();
                    txt_sl_goi.SelectAll(); // Bôi đen toàn bộ nội dung
                }
            }

        }
       
        private void Timer1_Tick(object sender, EventArgs e)
        {
            lbl_timer.Text = DateTime.Now.ToString("dd/MM/yyyy \nHH:mm:ss");
            if (cb_lammoi5s.Checked)
            {
                counter++;

                // 5 giây (vì timer tick mỗi 0.5 giây)
                if (counter >= 50)
                {
                    counter = 0; // reset
                    FormStart();
                }
            }
            else
            {
                // Nếu tắt checkbox thì reset
                counter = 0;
            }

        }
        private void loadingroom(int ma_p)
        {
            isLoading = true;
            try
            {
                Thongtinchung ttp = new Thongtinchung();
                ttp = xl.GetThongtinphong(ma_p);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;
                da_dv_su_dung.DataSource = xl.listdichvup(ma_p);
                txt_TongTien.Text = xl.TienTamTinh(ma_p);
            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
            finally
            {
                isLoading = false; // Kết thúc nạp dữ liệu => cho phép thông báo
            }
        }
        private void Mainform_Load(object sender, EventArgs e)
        {
            FormStart();
        }
        private void FormStart()
        {
            dsDichVu.loaddata();
            timer1.Start();
            list_dichvu.DataSource = dsDichVu.dsDichVus;
            list_dichvu.ValueMember = "ma_dv";
            list_dichvu.DisplayMember = "ten_dv";
            List<tblTinhTrangPhong> ttp = xl.tinhtrangp();
            cbo_tinhtrang.DataSource = ttp;
            cbo_tinhtrang.ValueMember = "ma_tinh_trang";
            cbo_tinhtrang.DisplayMember = "mo_ta";
            int countsophong = xl.GetSoPhongChoXacNhan();
            lbl_sophongchoxacnhan.Text = countsophong.ToString();
            if (countsophong > 0)
            {
                lbl_sophongchoxacnhan.ForeColor = Color.Yellow;
            }
                Drawitem();
            
        }
        private void Drawitem()
        {
            if (!Dataloader.ToMauPhong(this)) MessageBox.Show("Lấy thông tin các phòng thất bại", "Thông báo");
            main_left_bot_container.Paint += (s, ev) => Dataloader.DrawTableCellBorder(s, ev, Color.DarkGoldenrod);
            main_right_top_container.Paint += (s, ev) => Dataloader.DrawTableCellBorder(s, ev, Color.DarkGoldenrod);
            main_right_bot_container.Paint += (s, ev) => Dataloader.DrawTableCellBorder(s, ev, Color.DarkGoldenrod);
        }
        private void list_dichvu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_dichvu.SelectedItem is tblDichVu dv)
            {
                txt_donvi_dv.Text = dv.don_vi.ToString();
                txt_sl_ton_dv.Text = dv.ton_kho.ToString();
                txt_gia_dv.Text = dv.gia.ToString();
                txt_sl_goi.Focus();
                txt_sl_goi.SelectAll();
            }
        }
        

    }
}


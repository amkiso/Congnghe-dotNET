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
    public partial class Form1 : Form
    {
        Xuly xl = new Xuly();
        int current_step = 0;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            btn_tracuu.Click += Btn_tracuu_Click;
            tab_container.SelectedIndexChanged += Tab_container_SelectedIndexChanged;
            btn_back_step.Click += Btn_back_step_Click;
            btn_next_step.Click += Btn_next_step_Click;
        }

        private void Tab_container_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show($"Index:{tab_container.SelectedIndex}");
            return;
        }

        private void Btn_next_step_Click(object sender, EventArgs e)
        {
            tab_container.SelectedIndex = current_step+=1;
            current_step = tab_container.SelectedIndex;
            if(current_step ==4) btn_next_step.Enabled = false;
            btn_back_step.Enabled = true;
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
                string listChoXN, listchk;
                ThongtinTracuu kh = xl.GetThongtinKH(txt_input.Text);
                if (kh != null)
                {
                    if (kh.Phong_cho_xac_nhan == null) { listChoXN = "0"; }
                    else
                    {
                        listChoXN = string.Join(", ", kh.Phong_cho_xac_nhan);
                    }
                    if (kh.Phong_cho_xac_nhan == null) { listchk = "0"; }
                    else
                    {
                        listchk = string.Join(", ", kh.Phong_dang_dat);
                    }
                    string Thongtin = $"Tên khách hàng:{kh.Hoten}\t " +
                        $"Số điện thoại:{kh.Sdt}\n Số CCCD:{kh.Cccd}\n" +
                        $"Số phòng đã đặt:{kh.So_phong_da_dat}\n" +
                        $" Số phòng chờ check-in:{kh.So_phong_dang_dat}    Điểm tích lũy:{kh.Diem_tich_luy}\n" +
                        $"Số phòng chưa xác nhận:{kh.So_Phong_chua_Xac_nhan}    Các phòng đợi check-in:{listchk}\n" +
                        $"Các phòng chưa xác nhận:{listChoXN}";
                    MessageBox.Show(Thongtin, "Thông tin");
                    txt_input.Focus();
                    current_step =  tab_container.SelectedIndex = 1;
                    btn_back_step.Enabled = true;

                }
                else
                {
                    DialogResult rel = MessageBox.Show("Không tìm thấy thông tin khách hàng !, chuyển đến tạo tài khoản và thuê phòng ?", "Thông báo", MessageBoxButtons.YesNo);
                    if (rel == DialogResult.Yes)
                    {
                    }
                    else { this.Close(); }
                }
            }
            else
            {
                DialogResult rel = MessageBox.Show("Không tìm thấy thông tin khách hàng !, chuyển đến tạo tài khoản và thuê phòng ?", "Thông báo", MessageBoxButtons.YesNo);
                if (rel == DialogResult.Yes)
                {
                }
                else { this.Close(); }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            txt_input.Text = string.Empty;
        }
    }
}

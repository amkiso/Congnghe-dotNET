using KhachSanSaoBang.ChatBox;
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

namespace KhachSanSaoBang
{
    public partial class QuanLyTienIch : Form
    {
        Xuly xl = new Xuly();
        int acction;
        public QuanLyTienIch()
        {
            InitializeComponent();
            btn_add.Click += Btn_add_Click;
            btn_change.Click += Btn_change_Click;
            btn_exit.Click += Btn_exit_Click;
            btn_save.Click += Btn_save_Click;
            btn_delete.Click += Btn_delete_Click;
            this.Load += QuanLyTienIch_Load;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            //Menu
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
            Loaddata();
            if (Session.Acction_status == false)
            {
                MessageBox.Show("Bạn đã hủy đặt phòng !", "Thông báo");
            }
        }
        private void Btn_quanlytang_Click(object sender, EventArgs e)
        {
            this.Hide();
            QuanLyTang tienIch = new QuanLyTang();
            tienIch.ShowDialog();
            this.Close();
        }

   
        private void Btn_quanlydv_Click(object sender, EventArgs e)
        {
            this.Hide();
            QuanLyDichVu qlp = new QuanLyDichVu
                ();
            qlp.ShowDialog();
            this.Close();
        }

        private void Btn_quanlyphong_Click(object sender, EventArgs e)
        {
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


        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btn_add.Enabled = true;
            btn_change.Enabled = true;
            btn_delete.Enabled = true;
        }

        private void Loaddata()
        {
            var tienich = xl.gettienich();
            dataGridView1.DataSource = tienich;
            txt_id.DataBindings.Clear();
            txt_name.DataBindings.Clear();

            txt_id.DataBindings.Add("Text", dataGridView1.DataSource, "ma_tienich");
            txt_name.DataBindings.Add("Text", dataGridView1.DataSource, "ten_tienich");

            txt_name.Enabled = false;



        }
        private void QuanLyTienIch_Load(object sender, EventArgs e)
        {
            Loaddata();
        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int ma = int.Parse(txt_id.Text);

            if (xl.TienIchDangDuocSuDung(ma))
            {
                MessageBox.Show("Tiện ích đang được sử dụng, không thể xóa");
                return;
            }

            if (MessageBox.Show("Xóa tiện ích này?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (xl.DeleteTienIch(ma))
                {
                    MessageBox.Show("Đã xóa");
                    Loaddata();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại");
                }
            }
        }


        private void Btn_save_Click(object sender, EventArgs e)
        {
            bool kq = false;

            switch (acction)
            {
                case 1: // ADD
                    kq = xl.AddTang(txt_name.Text.Trim());
                    break;

                case 2: // EDIT
                    kq = xl.UpdateTienIch(
                        int.Parse(txt_id.Text),
                        txt_name.Text.Trim()
                    );
                    break;
            }

            if (kq)
            {
                MessageBox.Show("Lưu thành công");
                Loaddata();
                btn_add.Enabled = true;
                btn_delete.Enabled = false;
                btn_save.Enabled = false;
                btn_change.Enabled = false;
            }
            else
            {
                MessageBox.Show("Lưu thất bại");
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            DialogResult rel = MessageBox.Show("Bạn chắc muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo);
            if (rel == DialogResult.Yes)
            {
                this.Hide();
                Dangnhap dn = new Dangnhap();
                dn.ShowDialog();
                this.Close();
            }
        }


        private void Btn_change_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            acction = 2;
            txt_name.Enabled = true;

            btn_save.Enabled = true;
        }


        private void Btn_add_Click(object sender, EventArgs e)
        {
            acction = 1;

            txt_id.Clear();
            txt_name.Clear();
            txt_name.Enabled = true;

            btn_save.Enabled = true;
        }

    }
}

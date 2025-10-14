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
    public partial class Mainform : Form
    {
        Xuly xl = new Xuly();
        public Mainform()
        {
            InitializeComponent();
            this.timer1.Tick += Timer1_Tick; //timer
            //Sự kiện phòng Tầng 1
            btn_p101.Click += Btn_p101_Click;
            btn_p102.Click += Btn_p102_Click;
            btn_p103.Click += Btn_p103_Click;
            btn_p104.Click += Btn_p104_Click;
            btn_p105.Click += Btn_p105_Click;
            btn_p106.Click += Btn_p106_Click;
            //Sự kiện phòng Tầng 2
            btn_p201.Click += Btn_p201_Click;
            btn_p202.Click += Btn_p202_Click;
            btn_p203.Click += Btn_p203_Click;
            btn_p204.Click += Btn_p204_Click;
            btn_p205.Click += Btn_p205_Click;
            btn_p206.Click += Btn_p206_Click;
            //Sự kiện phòng Tầng 3
            btn_p301.Click += Btn_p301_Click;
            btn_p302.Click += Btn_p302_Click;
            btn_p303.Click += Btn_p303_Click;
            btn_p304.Click += Btn_p304_Click;
            btn_p305.Click += Btn_p305_Click;
            btn_p306.Click += Btn_p306_Click;
        }

        

        

        private void Btn_p306_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(18);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }

        private void Btn_p305_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(17);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }

        private void Btn_p304_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(16);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }

        private void Btn_p303_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(15);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }

        private void Btn_p302_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(14);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }

        private void Btn_p301_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(13);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }

        private void Btn_p206_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(12);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }

        private void Btn_p205_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(11);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }

        private void Btn_p204_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(10);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }

        private void Btn_p203_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(9);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }

        private void Btn_p202_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(8);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }

        private void Btn_p201_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(7);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }

        private void Btn_p106_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(6);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }
        private void Btn_p105_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(5);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }
        private void Btn_p104_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(4);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }
        private void Btn_p103_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(3);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }
        private void Btn_p102_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(2);
                if (!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }

        private void Btn_p101_Click(object sender, EventArgs e)
        {
            try
            {
                Thongtinchung ttp = xl.GetThongtinphong(1);
                if(!Dataloader.loaddulieu(ttp, this)) { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); return; }
                cbo_tinhtrang.SelectedValue = ttp.Trangthai;

            }
            catch { MessageBox.Show("Lấy thông tin phòng thất bại", "Thông báo"); }
        }
        
        private void Timer1_Tick(object sender, EventArgs e)
        {
            lbl_timer.Text = DateTime.Now.ToString("dd/MM/yyyy \nHH:mm:ss");

        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            
            lbl_username.Text = Session.UserName;
            lbl_chucvu.Text = Session.Role;
            timer1.Start();
            List<tblTinhTrangPhong> ttp = xl.tinhtrangp();
            cbo_tinhtrang.DataSource = ttp;
            cbo_tinhtrang.ValueMember = "ma_tinh_trang";
            cbo_tinhtrang.DisplayMember = "mo_ta";
            if (!Dataloader.ToMauPhong(this, xl.tomau())) MessageBox.Show("Lấy thông tin các phòng thất bại","Thông báo");
        }
    }
}

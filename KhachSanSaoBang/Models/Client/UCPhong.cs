using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang.Models.Client
{
    public partial class UCPhong : UserControl
    {


        private Color normalColor;
        public event Action<string> OnPhongSelect;
        private Color hoverColor = Color.LightBlue;
        public UCPhong()
        {
            InitializeComponent();
            this.tableLayoutPanel1.Click += UCPhong_Click;
            this.lbl_tenphong.Click += UCPhong_Click;
            tableLayoutPanel1.MouseEnter += UCPhong_MouseHover;
            lbl_tenphong.MouseEnter += UCPhong_MouseHover;
            normalColor= this.BackColor;
            tableLayoutPanel1.MouseLeave += TableLayoutPanel1_MouseLeave;
            lbl_tenphong.MouseLeave += TableLayoutPanel1_MouseLeave;


        }

        

        private void TableLayoutPanel1_MouseLeave(object sender, EventArgs e)
        {
           this.BackColor = normalColor;
        }

        private void UCPhong_MouseHover(object sender, EventArgs e)
        {
            normalColor = this.BackColor;
            this.BackColor = hoverColor;
        }

        public string TenPhong
        {
            get => lbl_tenphong.Text;
            set => lbl_tenphong.Text = value;
        }

        private void UCPhong_Click(object sender, EventArgs e)
        {

            if (this.Tag != null && OnPhongSelect != null)
            {
                // Kích hoạt sự kiện, gửi mã phòng (Tag) ra cho Form cha
                OnPhongSelect.Invoke(this.Tag.ToString());
            }
        }

       

        private void Btn_Hello_Click(object sender, EventArgs e)
        {
            if (this.Tag != null)
            {
                MessageBox.Show("Thông tin phòng ID: " + this.Tag.ToString());
            }
        }
    }
}

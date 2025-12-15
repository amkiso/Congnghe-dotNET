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
    public partial class UCdichvu : UserControl
    {
        private Color normalColor; private Color hoverColor = Color.LightBlue;
        private Color image = Color.DarkGray;
        public event Action<string> DichVuSelect;
        public UCdichvu()
        {
            InitializeComponent();
            this.picImage.MouseEnter += UC_MouseHover;
            this.lblName.MouseEnter += UC_MouseHover;
            this.lblPrice.MouseEnter += UC_MouseHover;
            this.picImage.MouseLeave += UC_MouseLeave;
            this.lblName.MouseLeave += UC_MouseLeave;
            this.lblPrice.MouseLeave += UC_MouseLeave;
            this.tableLayoutPanel1.MouseEnter += UC_MouseHover;
            this.tableLayoutPanel1.MouseLeave += UC_MouseLeave;
            this.tableLayoutPanel1.Click += UC_click;
            this.picImage.Click += UC_click;
            this.lblName.Click += UC_click;
            this.lblPrice.Click += UC_click;
        }

        private void UC_click(object sender, EventArgs e)
        {
            if (this.Tag != null && DichVuSelect != null)
            {
                // Kích hoạt sự kiện, gửi mã phòng (Tag) ra cho Form cha
                DichVuSelect.Invoke(this.Tag.ToString());
            }
        }

        private void UC_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = normalColor;
            picImage.BackColor = image;
        }

        private void UC_MouseHover(object sender, EventArgs e)
        {
            normalColor = this.BackColor;
            this.BackColor = hoverColor;
            picImage.BackColor = Color.AliceBlue;
        }
        public Image ServiceImage
        {
            get => picImage.Image;
            set => picImage.Image = value;
        }

        // Gán tên dịch vụ
        public string ServiceName
        {
            get => lblName.Text;
            set => lblName.Text = value;
        }

        // Gán giá bán
        public string Price
        {
            get => lblPrice.Text;
            set => lblPrice.Text = value;
        }
    }
}

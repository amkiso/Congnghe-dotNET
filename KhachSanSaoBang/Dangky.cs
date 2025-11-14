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
using KhachSanSaoBang.Models.Data;

namespace KhachSanSaoBang
{
    public partial class Dangky : Form
    {
        public Dangky()
        {
            InitializeComponent();
            this.Load += Dangky_Load;
            this.lbl_back_to_login.Click += Lbl_back_to_login_Click;
        }

        private void Lbl_back_to_login_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dangnhap dn  = new Dangnhap();
            dn.ShowDialog();
            this.Close();
        }

        private void Dangky_Load(object sender, EventArgs e)
        {
            txt_matkhau.UseSystemPasswordChar = true;
            main_container.BackColor = Color.FromArgb(120, 0, 0, 0);
            main_container.Visible = true;
        }
    }
}

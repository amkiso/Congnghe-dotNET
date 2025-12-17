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

namespace KhachSanSaoBang.Models.Client
{
    public partial class UCMenu : UserControl
    {
        public UCMenu()
        {
            InitializeComponent();
            this.Load += UCMenu_Load;
            timer1.Tick += Timer1_Tick;
        }

       
            private void Timer1_Tick(object sender, EventArgs e)
        {
            lbl_timer.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            

        }
        

        private void UCMenu_Load(object sender, EventArgs e)
        {
            lbl_chucvu.Text = Session.Role ?? "None";
            lbl_tennb.Text = Session.UserName ?? "Chưa đăng nhập";
            timer1.Start();
        }
    }
}

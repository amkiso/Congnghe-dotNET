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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbl_trai.CellPaint += (s, ev) => DrawTableCellBorder(s, ev, Color.Azure);
            tbl_topleft.CellPaint += (s, ev) => DrawTableCellBorder(s, ev, Color.Azure);
            container_left.CellPaint += (s, ev) => DrawTableCellBorder(s, ev, Color.Azure);
            tbl_phai.CellPaint += (s, ev) => DrawTableCellBorder(s, ev, Color.Azure);
            tbl_phai.BackColor = Color.FromArgb(120, 0, 0, 0);
            tbl_phai.Visible = true;
            container_left.BackColor = Color.FromArgb(120, 0, 0, 0);
            container_left.Visible = true;
            lbl_tennv.Text = Session.UserName;
            data_phuthu.ClearSelection();
            data_tienphong.ClearSelection();
            da_dv_su_dung.ClearSelection();
        }
        private void DrawTableCellBorder(object sender, TableLayoutCellPaintEventArgs e, Color c)
        {
            var g = e.Graphics;
            var r = e.CellBounds;

            using (Pen p = new Pen(c))
            {
                g.DrawRectangle(p, r.X, r.Y, r.Width - 1, r.Height - 1);
            }
        }
    }
}

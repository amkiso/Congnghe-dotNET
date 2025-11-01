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
    public partial class ThanhToan : Form
    {
        public ThanhToan()
        {
            InitializeComponent();
            this.Load += ThanhToan_Load;
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            tbl_trai.CellPaint += DrawTableCellBorder;
            tbl_topleft.CellPaint += DrawTableCellBorder;
            tbl_con1.CellPaint += DrawTableCellBorder;
            tbl_con2.CellPaint += DrawTableCellBorder;
            container_left.CellPaint += DrawTableCellBorder;
        }

        private void DrawTableCellBorder(object sender, TableLayoutCellPaintEventArgs e)
        {
            var g = e.Graphics;
            var r = e.CellBounds;

            using (Pen p = new Pen(Color.Gray))
            {
                g.DrawRectangle(p, r.X, r.Y, r.Width - 1, r.Height - 1);
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang.Models.Client
{
    public class DoubleBufferingTablePanel : TableLayoutPanel
    {
        public DoubleBufferingTablePanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }
    }
}

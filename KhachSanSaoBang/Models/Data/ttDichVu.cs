using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.Models
{
    public class ttDichVu
    {

        public int ma_dv { get; set; }
        public string ten_dv { get; set; }
        public double gia { get; set; }
        public string don_vi { get; set; }
        public string anh { get; set; }
        public int ton_kho { get; set; }

        public override string ToString()
        {
            return ten_dv; // Dùng để hiển thị trên ListBox, ComboBox
        }
    }

}

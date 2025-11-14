using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.Models.Data
{
    public class KhachThue
    {
        string tenkh;
        string cccd;
        int tuoi;
        string ngsinh;
        public string Tenkh { get => tenkh; set => tenkh = value; }
        public string Cccd { get => cccd; set => cccd = value; }
        public int Tuoi { get => tuoi; set => tuoi = value; }
        public string Ngsinh { get => ngsinh; set => ngsinh = value; }
    }
}

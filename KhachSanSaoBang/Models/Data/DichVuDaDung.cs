using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.Models
{
    public class DichVuDaDung
    {
        string _tendv;
        string _donvi;
        int _soluong;
        float _giaban;

        public string Tendv { get => _tendv; set => _tendv = value; }
        public string Donvi { get => _donvi; set => _donvi = value; }
        public int Soluong { get => _soluong; set => _soluong = value; }
        public float Giaban { get => _giaban; set => _giaban = value; }
    }
}

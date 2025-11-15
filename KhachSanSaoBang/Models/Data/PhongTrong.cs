using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.Models.Data
{
    public class PhongTrong
    {
        int _maphong, _maloai, _matinhtrang, _matienich, _matang;
        string _tenphong, _tenloai, _tentinhtrang, _tentienich;
        float _giathue;


        public int Maphong { get => _maphong; set => _maphong = value; }
        public int Maloai { get => _maloai; set => _maloai = value; }
        public int Matinhtrang { get => _matinhtrang; set => _matinhtrang = value; }
        public int Matienich { get => _matienich; set => _matienich = value; }
        public int Matang { get => _matang; set => _matang = value; }
        public string Tenphong { get => _tenphong; set => _tenphong = value; }
        public string Tenloai { get => _tenloai; set => _tenloai = value; }
        public string Tentinhtrang { get => _tentinhtrang; set => _tentinhtrang = value; }
        public string Tentienich { get => _tentienich; set => _tentienich = value; }
        public float Giathue { get => _giathue; set => _giathue = value; }
    }
}

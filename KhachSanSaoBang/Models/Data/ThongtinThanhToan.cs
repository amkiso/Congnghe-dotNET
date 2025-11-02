using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.Models.Data
{
    public class ThongtinThanhToan
    {
        int _giaphong, _mahd,   _tilephuthu,  _tongtien;
        string _makh, _sophong, tenkh;
        DateTime ngayvao, ngayra, ngaydukienra;
        int khachdicung;
        float tienchietkhau;
        public List<DichVuDaDung> Dichvusudung { get; set; } = new List<DichVuDaDung>();
        public string Makh { get => _makh; set => _makh = value; }
        public string Sophong { get => _sophong; set => _sophong = value; }
        public string Tenkh { get => tenkh; set => tenkh = value; }
        public int Giaphong { get => _giaphong; set => _giaphong = value; }
        public int Mahd { get => _mahd; set => _mahd = value; }

        public int Songaythue
        {
            get
            {
                if (Ngayvao == default || Ngaydukienra == default)
                    return 0;
                var days = (Ngaydukienra.Date - Ngayvao.Date).Days;
                return Math.Max(0, days);
            }
        }

        public int Tiendichvu
        {
            get
            {
                if (Dichvusudung == null || Dichvusudung.Count == 0)
                    return 0;
                double total = Dichvusudung.Sum(d => (double)d.Giaban * d.Soluong);
                return (int)Math.Round(total);
            }
        }

        public int Thanhtien
        {
            get
            {
                return Giaphong * Songaythue + Tiendichvu;
            }
        }

        public int Songayphuthu {
            get
            {
                if (Ngayra == default || Ngaydukienra == default)
                    return 0;
                var days = (Ngayra.Date - Ngaydukienra.Date).Days;
                return Math.Max(0, days);
            } }
        public int Tilephuthu { get => _tilephuthu; set => _tilephuthu = value; }
        public int Thanhtienphuthu {
            get
            {
                return Giaphong * Songayphuthu * Tilephuthu;
            }
        }
        public int Tongtien { get => _tongtien; set => _tongtien = value; }
        public DateTime Ngayvao { get => ngayvao; set => ngayvao = value; }
        public DateTime Ngayra { get => ngayra; set => ngayra = value; }
        public DateTime Ngaydukienra { get => ngaydukienra; set => ngaydukienra = value; }
        public int sokhach { get => khachdicung; set => khachdicung = value; }
        public float Tienchietkhau { get => tienchietkhau; set => tienchietkhau = value; }
    }
}
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
        int _giaphong, _mahd,   _tilephuthu  ;
        string _makh, _sophong, tenkh,_tennv;
        DateTime ngayvao, ngayra, ngaydukienra;
        int khachdicung;
        float tienchietkhau, _tongtien;
        int _tilechietkhau;
        string makm;
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

        public float Songayphuthu {
            get
            {
                if (Ngayvao == default || Ngaydukienra == default)
                    return 0;

                TimeSpan thoiGianSuDung = Ngaydukienra - Ngayvao;

                float soNgay = (float)Math.Round(thoiGianSuDung.TotalDays,2);
                return Math.Max(0, soNgay);
            }
        }
        public int Tilephuthu { get => _tilephuthu; set => _tilephuthu = value; }
        public float Thanhtienphuthu {
            get
            {
                return (Songayphuthu * Giaphong * Tilephuthu) / 100;
            }
        }
        public float Tongtien { get => _tongtien; set => _tongtien = value; }
        public DateTime Ngayvao { get => ngayvao; set => ngayvao = value; }
        public DateTime Ngayra { get => ngayra; set => ngayra = value; }
        public DateTime Ngaydukienra { get => ngaydukienra; set => ngaydukienra = value; }
        public int sokhach { get => khachdicung; set => khachdicung = value; }
        public float Tienchietkhau { get => tienchietkhau; set => tienchietkhau = value; }
        public string Tennv { get => _tennv; set => _tennv = value; }
        public int Tilechietkhau { get => _tilechietkhau; set => _tilechietkhau = value; }
        public string Makm { get => makm; set => makm = value; }
    }
}
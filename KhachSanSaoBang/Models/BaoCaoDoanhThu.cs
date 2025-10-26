using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.Models
{
    public class BaoCaoDoanhThu
    {
        private string ngay;
        private double tongDoanhThu;
        public string Ngay { get => ngay; set => ngay = value; }
        public double TongDoanhThu { get => tongDoanhThu; set => tongDoanhThu = value; }
    }

    public class BaoCaoSoLuongKhach
    {
        private string ngay;
        private int soLuongKhach;
        public string Ngay { get => ngay; set => ngay = value; }
        public int SoLuongKhach { get => soLuongKhach; set => soLuongKhach = value; }
    }
}

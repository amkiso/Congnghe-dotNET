using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.KhachHang
{
    public class ThongTinKhachHang
    {
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string CCCD { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }

        public ThongTinKhachHang() { }

        public ThongTinKhachHang(string ma, string ten, string gt, DateTime ns, string cccd, string email, string diachi, string sdt)
        {
            MaKH = ma;
            HoTen = ten;
            GioiTinh = gt;
            NgaySinh = ns;
            CCCD = cccd;
            Email = email;
            DiaChi = diachi;
            SDT = sdt;
        }
    }
}

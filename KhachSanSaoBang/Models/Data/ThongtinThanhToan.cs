using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.Models.Data
{
    public class ThongtinThanhToan
    {
        int manv, tienphong,phuthu,tiendv,voucher;
        DateTime ngTraPhong;
        public int Manv { get => manv; set => manv = value; }
        public int Tienphong { get => tienphong; set => tienphong = value; }
        public int Phuthu { get => phuthu; set => phuthu = value; }
        public int Tiendv { get => tiendv; set => tiendv = value; }
        public int Voucher { get => voucher; set => voucher = value; }
        public DateTime NgTraPhong { get => ngTraPhong; set => ngTraPhong = value; }
    }
}

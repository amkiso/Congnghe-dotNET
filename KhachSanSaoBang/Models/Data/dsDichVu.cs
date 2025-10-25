using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhachSanSaoBang.Models.Data;
namespace KhachSanSaoBang.Models
{
    public static class dsDichVu
    {
        public static List<tblDichVu> dsDichVus { get; private set; } = new List<tblDichVu>();
        public static void loaddata()
        {
            Xuly xl = new Xuly();
            dsDichVus = xl.dsDichVu();
        }
        public static void Reload()
        {
            loaddata();
        }
        public static void TruSoLuong(int ma_dv, int soLuong)
        {
            var dv = dsDichVus.FirstOrDefault(x => x.ma_dv == ma_dv);
            if (dv != null)
            {
                dv.ton_kho -= soLuong;
                if (dv.ton_kho < 0) dv.ton_kho = 0; 
            }
        }
        public static void TraLaiSoLuong(int ma_dv, int soLuong)
        {
            var dv = dsDichVus.FirstOrDefault(x => x.ma_dv == ma_dv);
            if (dv != null)
            {
                dv.ton_kho += soLuong;
             
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.Models.Data
{
    public class ThongtinTracuu
    {
        string hoten, sdt, cccd;
        float diem_tich_luy;
        string makh;
        int phong_da_dat, so_phong_dang_dat, so_phong_chua_Xac_nhan,so_phong_da_dat;
        public ThongtinTracuu() { }
        public string Hoten { get => hoten; set => hoten = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string Cccd { get => cccd; set => cccd = value; }
        public float Diem_tich_luy { get => diem_tich_luy; set => diem_tich_luy = value; }
        public int Phong_da_dat { get => phong_da_dat; set => phong_da_dat = value; }
        public List<string> Phong_cho_xac_nhan { get; set; }
        public List<string> Phong_dang_dat { get; set; }
        public int So_phong_dang_dat { get => so_phong_dang_dat; set => so_phong_dang_dat = value; }
        public int So_Phong_chua_Xac_nhan { get => so_phong_chua_Xac_nhan; set => so_phong_chua_Xac_nhan = value; }
        public int So_phong_da_dat { get => so_phong_da_dat; set => so_phong_da_dat = value; }
        public string Makh { get => makh; set => makh = value; }
    }
}

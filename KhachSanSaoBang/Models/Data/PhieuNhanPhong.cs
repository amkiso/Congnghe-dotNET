using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.Models.Data
{
    public class PhieuNhanPhong
    {
        string _mapdp;
        string _tienich, _loaiphong, _sophong;
        string _giathue;
        DateTime ngayvao, ngayra, ngaydat;
        string tenkh, sdt;

        public string Mapdp { get => _mapdp; set => _mapdp = value; }
        public string Tienich { get => _tienich; set => _tienich = value; }
        public string Loaiphong { get => _loaiphong; set => _loaiphong = value; }
        public string Giathue { get => _giathue; set => _giathue = value; }
        public DateTime Ngayvao { get => ngayvao; set => ngayvao = value; }
        public DateTime Ngayra { get => ngayra; set => ngayra = value; }
        public DateTime Ngaydat { get => ngaydat; set => ngaydat = value; }
        public string Sophong { get => _sophong; set => _sophong = value; }
        public string Tenkh { get => tenkh; set => tenkh = value; }
        public string Sdt { get => sdt; set => sdt = value; }
    }
}

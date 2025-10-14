using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang.Models
{
    public class Thongtinchung
    {
        int maphong,trangthai;
        string tenphong, loaiphong, khachhang;
        int matang, sokhach, giathue;
        DateTime ngayvao, ngayra, thoigian_nhanphong;
        public string Tenphong { get => tenphong; set => tenphong = value; }
        public string Loaiphong { get => loaiphong; set => loaiphong = value; }
        public string Khachhang { get => khachhang; set => khachhang = value; }
        public int Matang { get => matang; set => matang = value; }
        public int Sokhach { get => sokhach; set => sokhach = value; }
        public int Giathue { get => giathue; set => giathue = value; }
        public int Trangthai { get => trangthai; set => trangthai = value; }
        public DateTime Ngayvao { get => ngayvao; set => ngayvao = value; }
        public DateTime Ngayra { get => ngayra; set => ngayra = value; }
        public DateTime Thoigian_nhanphong { get => thoigian_nhanphong; set => thoigian_nhanphong = value; }
        public int Maphong { get => maphong; set => maphong = value; }
    }
}

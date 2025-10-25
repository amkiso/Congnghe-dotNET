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
        string ngayvao, ngayra, thoigian_nhanphong;
        DateTime ngay_o_thuc_te;
        int phuthu;
        public string Tenphong { get => tenphong; set => tenphong = value; }
        public string Loaiphong { get => loaiphong; set => loaiphong = value; }
        public string Khachhang { get => khachhang; set => khachhang = value; }
        public int Matang { get => matang; set => matang = value; }
        public int Sokhach { get => sokhach; set => sokhach = value; }
        public int Giathue { get => giathue; set => giathue = value; }
        public int Trangthai { get => trangthai; set => trangthai = value; }
        public string Ngayvao { get => ngayvao; set => ngayvao = value; }
        public string Ngayra { get => ngayra; set => ngayra = value; }
        public string Thoigian_nhanphong { get => thoigian_nhanphong; set => thoigian_nhanphong = value; }
        public int Maphong { get => maphong; set => maphong = value; }
        public DateTime Ngay_o_thuc_te { get => ngay_o_thuc_te; set => ngay_o_thuc_te = value; }
        public int Phuthu { get => phuthu; set => phuthu = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.Models
{
    public class XuLyBaoCao
    {
        SQLDataDataContext db = new SQLDataDataContext();
        //ngày lập là ngày trả phòng ?
        public object LayDoanhThuTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            return db.tblHoaDons 
                     .Where(hd => hd.ngay_tra_phong >= tuNgay && hd.ngay_tra_phong <= denNgay)
                     .GroupBy(hd => hd.ngay_tra_phong.Value.Date)
                     .Select(g => new { Ngay = g.Key, TongTien = g.Sum(hd => hd.tong_tien ?? 0) })
                     .OrderBy(r => r.Ngay)
                     .ToList();
        }

        public object LayDoanhThuTheoThang(DateTime tuNgay, DateTime denNgay)
        {
            return db.tblHoaDons
                     .Where(hd => hd.ngay_tra_phong >= tuNgay && hd.ngay_tra_phong <= denNgay)
                     .GroupBy(hd => new { hd.ngay_tra_phong.Value.Year, hd.ngay_tra_phong.Value.Month })
                     .Select(g => new { Thang = g.Key.Month + "/" + g.Key.Year, TongTien = g.Sum(hd => hd.tong_tien ?? 0) })
                     .OrderBy(r => r.Thang)
                     .ToList();
        }

        public object LayDoanhThuTheoNam(DateTime tuNgay, DateTime denNgay)
        {
            return db.tblHoaDons
                     .Where(hd => hd.ngay_tra_phong.HasValue && hd.ngay_tra_phong.Value.Date >= tuNgay && hd.ngay_tra_phong.Value.Date <= denNgay)
                     .GroupBy(hd => hd.ngay_tra_phong.Value.Year)
                     .Select(g => new { Nam = g.Key, TongDoanhThu = g.Sum(hd => hd.tong_tien ?? 0) })
                     .OrderBy(r => r.Nam)
                     .ToList();
        }

        public object LaySoLuongKhachTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            return db.tblPhieuDatPhongs
                     .Where(p => p.ngay_vao.HasValue && p.ngay_vao.Value.Date >= tuNgay && p.ngay_vao.Value.Date <= denNgay)
                     .GroupBy(p => p.ngay_vao.Value.Date)
                     .Select(g => new { Ngay = g.Key, SoLuongKhach = g.Sum(p => p.so_luong_khach ?? 0) })
                     .OrderBy(r => r.Ngay)
                     .ToList();
        }

        public object LaySoLuongKhachTheoThang(DateTime tuNgay, DateTime denNgay)
        {
            return db.tblPhieuDatPhongs
                     .Where(p => p.ngay_vao.HasValue && p.ngay_vao.Value.Date >= tuNgay && p.ngay_vao.Value.Date <= denNgay)
                     .GroupBy(p => new { Thang = p.ngay_vao.Value.Month, Nam = p.ngay_vao.Value.Year })
                     .Select(g => new { ThangNam = g.Key.Thang + "/" + g.Key.Nam, SoLuongKhach = g.Sum(p => p.so_luong_khach ?? 0) })
                     .ToList();
        }

        public object LaySoLuongKhachTheoNam(DateTime tuNgay, DateTime denNgay)
        {
            return db.tblPhieuDatPhongs
                     .Where(p => p.ngay_vao.HasValue && p.ngay_vao.Value.Date >= tuNgay && p.ngay_vao.Value.Date <= denNgay)
                     .GroupBy(p => p.ngay_vao.Value.Year)
                     .Select(g => new { Nam = g.Key, SoLuongKhach = g.Sum(p => p.so_luong_khach ?? 0) })
                     .OrderBy(r => r.Nam)
                     .ToList();
        }
    }

}

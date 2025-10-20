﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.Models
{
    public class XuLyHoaDon
    {
        SQLDataDataContext db = new SQLDataDataContext();
        // 1. Lấy danh sách nhân viên (cho Index.cshtml)
        public object LayDanhSachHoaDon()
        {
            // Dùng 'select new' để chỉ lấy những cột bạn thực sự cần
            var danhSach = from hd in db.tblHoaDons
                           select new
                           {
                               MaHoaDon = hd.ma_hd,
                               MaNhanVien = hd.ma_nv,
                               NgayTraPhong = hd.ngay_tra_phong,
                               MaTinhTrang = hd.ma_tinh_trang,
                               TienPhong = hd.tien_phong,
                               TienDV = hd.tien_dich_vu,
                               PhuThu = hd.phu_thu,
                               TongTien = hd.tong_tien,
                               TenKhachHang = hd.tblPhieuDatPhong.ma_kh,
                               TinhTrangHD = hd.tblTinhTrangHoaDon.mo_ta// Ví dụ lấy tên từ bảng liên quan khác
                                                                         // Thêm bất kỳ cột nào khác từ tblHoaDon bạn muốn hiển thị
                                                                         // KHÔNG lấy hd.tblKhuyenmai hoặc hd.ma_km
                           };

            return danhSach.ToList();
        }

        public tblHoaDon LayThongTinHoaDon(int maNV)
        {
            return db.tblHoaDons.FirstOrDefault(nv => nv.ma_nv == maNV);
        }

       
    }

}

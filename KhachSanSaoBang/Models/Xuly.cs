using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang.Models
{
    public class Xuly
    {
        SQLDataDataContext db = new SQLDataDataContext();
        public Xuly() { }
        //Xử lý đăng nhập và lưu session
        public bool Dangnhap(tblNhanVien nv)
        {
            var user = db.tblNhanViens.FirstOrDefault(u =>
                    u.tai_khoan == nv.tai_khoan &&
                    u.mat_khau == nv.mat_khau);

            if (user == null) { return false; }
            else
            {
                var obj = (from u in db.tblNhanViens
                          join cv in db.tblChucVus on u.ma_chuc_vu equals cv.ma_chuc_vu
                          where u.tai_khoan == nv.tai_khoan && u.mat_khau == nv.mat_khau
                          select new { u.ho_ten, u.ma_nv, cv.chuc_vu }).FirstOrDefault();
                Session.Role = obj.chuc_vu;
                Session.UserId = obj.ma_nv;
                Session.UserName = obj.ho_ten;
                return true;
            }
        }
        //Lấy toàn bộ thông tin tình trạng phòng
        public List<tblTinhTrangPhong> tinhtrangp()
        {
            return (from u in db.tblTinhTrangPhongs select u).ToList();
        }
        public List<tblPhong> tomau()
        {
            return (from u in db.tblPhongs select u).ToList();
        }
        //Lấy thông tin theo phòng
        public Thongtinchung GetThongtinphong(int ma) { 
        Thongtinchung tt = new Thongtinchung();
           var phong = (from u in db.tblPhongs
                        join lp in db.tblLoaiPhongs
                        on u.loai_phong equals lp.loai_phong
                        where u.ma_phong == ma select new
                        {
                            u.ma_phong,u.ma_tinh_trang,lp.mo_ta, u.so_phong,u.ma_tang,lp.gia
                        }).FirstOrDefault();
            tt.Trangthai = (int) phong.ma_tinh_trang;
            tt.Loaiphong = phong.mo_ta;
            tt.Maphong = phong.ma_phong;
            tt.Tenphong = phong.so_phong;
            tt.Matang = (int)phong.ma_tang;
            tt.Giathue = (int)phong.gia;
            Session.maphonghientai = tt.Maphong;
            return tt;
        
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.Models
{
    // File: Models/Xuly.cs
    public class XulyNV
    {
        SQLDataDataContext db = new SQLDataDataContext();

        // 1. Lấy danh sách nhân viên (cho Index.cshtml)
        public List<tblNhanVien> LayDanhSachNhanVien()
        {
            return db.tblNhanViens.ToList();
        }

        // 2. Lấy thông tin chi tiết một nhân viên (cho Edit.cshtml, Details.cshtml)
        public tblNhanVien LayThongTinNhanVien(int maNV)
        {
            return db.tblNhanViens.FirstOrDefault(nv => nv.ma_nv == maNV);
        }

        // 3. Thêm nhân viên mới (cho Create.cshtml)
        public bool ThemNhanVien(tblNhanVien nv)
        {
            try
            {
                db.tblNhanViens.InsertOnSubmit(nv);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm NV: " + ex.Message);
                return false;
            }
        }

        // 4. Sửa thông tin nhân viên (cho Edit.cshtml)
        public bool SuaNhanVien(tblNhanVien nv_moi)
        {
            try
            {
                // Lấy nhân viên cũ từ DB
                tblNhanVien nv_cu = db.tblNhanViens.FirstOrDefault(n => n.ma_nv == nv_moi.ma_nv);
                if (nv_cu == null) return false;

                // Cập nhật thông tin
                nv_cu.ma_nv = nv_moi.ma_nv;
                nv_cu.ho_ten = nv_moi.ho_ten;
                nv_cu.ngay_sinh = nv_moi.ngay_sinh;
                nv_cu.gioi_tinh = nv_moi.gioi_tinh;
                nv_cu.que_quan = nv_moi.que_quan;
                nv_cu.nam_bd = nv_moi.nam_bd;
                nv_cu.luong = nv_moi.luong;
                nv_cu.dia_chi = nv_moi.dia_chi;
                

                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        // 5. Xóa nhân viên (cho Delete.cshtml)
        public bool XoaNhanVien(int maNV)
        {
            try
            {
                tblNhanVien nv = db.tblNhanViens.FirstOrDefault(n => n.ma_nv == maNV);
                if (nv == null) return false;

                db.tblNhanViens.DeleteOnSubmit(nv);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        // 6. Vô hiệu hóa / Kích hoạt tài khoản (Chức năng mới)
        // Giả sử bảng tblNhanVien có một cột 'trang_thai' (kiểu bit hoặc int)
        public bool VoHieuHoaNhanVien(int maNV, bool kichHoat)
        {
            try
            {
                tblNhanVien nv = db.tblNhanViens.FirstOrDefault(n => n.ma_nv == maNV);
                if (nv == null) return false;

                 nv.trang_thai = kichHoat; // Cập nhật trạng thái
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        //trạng thái nhân viên 
        public bool CapNhatTrangThai(int maNV, bool trangThaiMoi)
        {
            try
            {
                tblNhanVien nv = db.tblNhanViens.FirstOrDefault(n => n.ma_nv == maNV);
                if (nv == null) return false;

                // Cập nhật trạng thái
                nv.trang_thai = trangThaiMoi;

                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }


    }
}

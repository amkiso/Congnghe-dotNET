using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.ThongKe.BaoCao
{
    public class DBBaoCao
    {
        private string _cnn = "Data Source=LAPTOP-MOJM17CB\\SQLEXPRESS02;Initial Catalog=dataQLKS;Integrated Security=True";
        private SqlConnection sqlcnn;

        public DBBaoCao()
        {
            sqlcnn = new SqlConnection(_cnn);
        }

        // ✅ 1. Biểu đồ doanh thu theo tháng
        public DataTable LayDoanhThuTheoThang()
        {
            string sql = @"
                SELECT MONTH(ngay_tra_phong) AS Thang, 
                       SUM(tong_tien) AS TongDoanhThu
                FROM tblHoaDon
                GROUP BY MONTH(ngay_tra_phong)
                ORDER BY Thang";

            SqlDataAdapter da = new SqlDataAdapter(sql, sqlcnn);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

        // ✅ 2. Biểu đồ doanh thu theo nhân viên
        public DataTable LayDoanhThuTheoNhanVien()
        {
            string sql = @"
                SELECT NV.ho_ten AS TenNhanVien, 
                       SUM(HD.tong_tien) AS TongDoanhThu
                FROM tblHoaDon HD
                INNER JOIN tblNhanVien NV ON HD.ma_nv = NV.ma_nv
                GROUP BY NV.ho_ten
                ORDER BY TongDoanhThu DESC";

            SqlDataAdapter da = new SqlDataAdapter(sql, sqlcnn);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

        // ✅ 3. Biểu đồ dịch vụ được sử dụng nhiều nhất
        public DataTable LayDichVuPhoBien()
        {
            string sql = @"
                SELECT TOP 5 DV.ten_dv AS TenDichVu, 
                             SUM(CT.so_luong) AS SoLanDung
                FROM tblDichVuDaDat CT
                INNER JOIN tblDichVu DV ON CT.ma_dv = DV.ma_dv
                GROUP BY DV.ten_dv
                ORDER BY SoLanDung DESC";

            SqlDataAdapter da = new SqlDataAdapter(sql, sqlcnn);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

        // ✅ 4. Số lượt đặt phòng theo loại phòng
        public DataTable LaySoLuotDatPhongTheoLoaiPhong()
        {
            string sql = @"
        SELECT LP.mo_ta AS LoaiPhong, COUNT(*) AS SoLuotDat
        FROM tblPhieuDatPhong PDP
        INNER JOIN tblPhong P ON PDP.ma_phong = P.ma_phong
        INNER JOIN tblLoaiPhong LP ON P.loai_phong = LP.loai_phong
        GROUP BY LP.mo_ta
        ORDER BY SoLuotDat DESC";

            SqlDataAdapter da = new SqlDataAdapter(sql, sqlcnn);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

    }
}

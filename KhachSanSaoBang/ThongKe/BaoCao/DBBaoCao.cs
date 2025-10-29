using System;
using System.Data;
using System.Data.SqlClient;

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

        // ✅ Lấy danh sách năm có dữ liệu trong tblHoaDon
        public DataTable LayDanhSachNam()
        {
            string sql = "SELECT DISTINCT YEAR(ngay_tra_phong) AS Nam FROM tblHoaDon ORDER BY Nam DESC";
            SqlDataAdapter da = new SqlDataAdapter(sql, sqlcnn);
            DataTable tbl = new DataTable();
            da.Fill(tbl);

            // Thêm dòng "Tất cả" để chọn toàn bộ năm
            DataRow row = tbl.NewRow();
            row["Nam"] = 0;
            tbl.Rows.InsertAt(row, 0);

            return tbl;
        }

        // ✅ 1. Biểu đồ Doanh thu theo Tháng (lọc theo năm)
        public DataTable LayDoanhThuTheoThang(int nam)
        {
            string sql = @"
                SELECT MONTH(ngay_tra_phong) AS Thang, 
                       SUM(tong_tien) AS TongDoanhThu
                FROM tblHoaDon
                WHERE (@nam = 0 OR YEAR(ngay_tra_phong) = @nam)
                GROUP BY MONTH(ngay_tra_phong)
                ORDER BY Thang";

            using (SqlCommand cmd = new SqlCommand(sql, sqlcnn))
            {
                cmd.Parameters.AddWithValue("@nam", nam);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);
                return tbl;
            }
        }

        // ✅ 1b. Biểu đồ Doanh thu theo Năm (dùng khi không chọn năm)
        public DataTable LayDoanhThuTheoNam()
        {
            string sql = @"
                SELECT YEAR(ngay_tra_phong) AS Nam, 
                       SUM(tong_tien) AS TongDoanhThu
                FROM tblHoaDon
                GROUP BY YEAR(ngay_tra_phong)
                ORDER BY Nam";

            SqlDataAdapter da = new SqlDataAdapter(sql, sqlcnn);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

        // ✅ 2. Biểu đồ Doanh thu theo Nhân viên (lọc theo năm)
        public DataTable LayDoanhThuTheoNhanVienTheoNam(int nam)
        {
            string sql = @"
                SELECT NV.ho_ten AS TenNhanVien, 
                       SUM(HD.tong_tien) AS TongDoanhThu
                FROM tblHoaDon HD
                INNER JOIN tblNhanVien NV ON HD.ma_nv = NV.ma_nv
                WHERE (@nam = 0 OR YEAR(HD.ngay_tra_phong) = @nam)
                GROUP BY NV.ho_ten
                ORDER BY TongDoanhThu DESC";

            using (SqlCommand cmd = new SqlCommand(sql, sqlcnn))
            {
                cmd.Parameters.AddWithValue("@nam", nam);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);
                return tbl;
            }
        }

        // ✅ 3. Biểu đồ Dịch vụ phổ biến (lọc theo năm + tháng)
        public DataTable LayDichVuPhoBienTheoNamThang(int nam, int thang)
        {
            string sql = @"
                SELECT TOP 5 DV.ten_dv AS TenDichVu, 
                             SUM(CT.so_luong) AS SoLanDung
                FROM tblDichVuDaDat CT
                INNER JOIN tblDichVu DV ON CT.ma_dv = DV.ma_dv
                INNER JOIN tblHoaDon HD ON CT.ma_hd = HD.ma_hd
                WHERE (@nam = 0 OR YEAR(HD.ngay_tra_phong) = @nam)
                  AND (@thang = 0 OR MONTH(HD.ngay_tra_phong) = @thang)
                GROUP BY DV.ten_dv
                ORDER BY SoLanDung DESC";

            using (SqlCommand cmd = new SqlCommand(sql, sqlcnn))
            {
                cmd.Parameters.AddWithValue("@nam", nam);
                cmd.Parameters.AddWithValue("@thang", thang);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);
                return tbl;
            }
        }

        // ✅ 4. Biểu đồ Số lượt đặt phòng theo loại phòng (lọc theo năm)
        public DataTable LaySoLuotDatPhongTheoLoaiPhongTheoNam(int nam)
        {
            string sql = @"
                SELECT LP.mo_ta AS LoaiPhong, COUNT(*) AS SoLuotDat
                FROM tblPhieuDatPhong PDP
                INNER JOIN tblPhong P ON PDP.ma_phong = P.ma_phong
                INNER JOIN tblLoaiPhong LP ON P.loai_phong = LP.loai_phong
                WHERE (@nam = 0 OR YEAR(PDP.ngay_dat) = @nam)
                GROUP BY LP.mo_ta
                ORDER BY SoLuotDat DESC";

            using (SqlCommand cmd = new SqlCommand(sql, sqlcnn))
            {
                cmd.Parameters.AddWithValue("@nam", nam);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);
                return tbl;
            }
        }
    }
}

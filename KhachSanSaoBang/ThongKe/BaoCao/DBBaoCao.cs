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

        // 🔹 Doanh thu theo ngày
        public DataTable LayDoanhThuTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT CAST(ngay_tra_phong AS DATE) AS Ngay, SUM(tong_tien) AS TongDoanhThu
                FROM tblHoaDon
                WHERE ngay_tra_phong BETWEEN @tuNgay AND @denNgay
                GROUP BY CAST(ngay_tra_phong AS DATE)
                ORDER BY Ngay";
            SqlCommand cmd = new SqlCommand(sql, sqlcnn);
            cmd.Parameters.AddWithValue("@tuNgay", tuNgay);
            cmd.Parameters.AddWithValue("@denNgay", denNgay);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

        // 🔹 Doanh thu theo tháng
        public DataTable LayDoanhThuTheoThang(int nam)
        {
            string sql = @"
                SELECT MONTH(ngay_tra_phong) AS Thang, SUM(tong_tien) AS TongDoanhThu
                FROM tblHoaDon
                WHERE YEAR(ngay_tra_phong) = @nam
                GROUP BY MONTH(ngay_tra_phong)
                ORDER BY Thang";
            SqlCommand cmd = new SqlCommand(sql, sqlcnn);
            cmd.Parameters.AddWithValue("@nam", nam);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

        // 🔹 Doanh thu 3 năm gần nhất
        public DataTable LayDoanhThu3NamGanNhat()
        {
            string sql = @"
                SELECT YEAR(ngay_tra_phong) AS Nam, SUM(tong_tien) AS TongDoanhThu
                FROM tblHoaDon
                WHERE YEAR(ngay_tra_phong) >= YEAR(GETDATE()) - 2
                GROUP BY YEAR(ngay_tra_phong)
                ORDER BY Nam";
            SqlDataAdapter da = new SqlDataAdapter(sql, sqlcnn);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

        // 🔹 Doanh thu nhân viên theo khoảng
        public DataTable LayDoanhThuNhanVienTheoKhoang(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT NV.ho_ten AS TenNV, SUM(HD.tong_tien) AS TongDoanhThu
                FROM tblHoaDon HD
                JOIN tblNhanVien NV ON HD.ma_nv = NV.ma_nv
                WHERE HD.ngay_tra_phong BETWEEN @tuNgay AND @denNgay
                GROUP BY NV.ho_ten
                ORDER BY TongDoanhThu DESC";
            SqlCommand cmd = new SqlCommand(sql, sqlcnn);
            cmd.Parameters.AddWithValue("@tuNgay", tuNgay);
            cmd.Parameters.AddWithValue("@denNgay", denNgay);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

        // 🔹 Doanh thu nhân viên theo năm
        public DataTable LayDoanhThuNhanVienTheoNam(int nam)
        {
            string sql = @"
                SELECT NV.ho_ten AS TenNV, SUM(HD.tong_tien) AS TongDoanhThu
                FROM tblHoaDon HD
                JOIN tblNhanVien NV ON HD.ma_nv = NV.ma_nv
                WHERE YEAR(ngay_tra_phong) = @nam
                GROUP BY NV.ho_ten
                ORDER BY TongDoanhThu DESC";
            SqlCommand cmd = new SqlCommand(sql, sqlcnn);
            cmd.Parameters.AddWithValue("@nam", nam);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

        // 🔹 Dịch vụ phổ biến
        public DataTable LayDichVuPhoBienTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT TOP 5 DV.ten_dv AS TenDV, SUM(CT.so_luong) AS SoLanDung
                FROM tblDichVuDaDat CT
                JOIN tblDichVu DV ON CT.ma_dv = DV.ma_dv
                JOIN tblHoaDon HD ON CT.ma_hd = HD.ma_hd
                WHERE HD.ngay_tra_phong BETWEEN @tuNgay AND @denNgay
                GROUP BY DV.ten_dv
                ORDER BY SoLanDung DESC";
            SqlCommand cmd = new SqlCommand(sql, sqlcnn);
            cmd.Parameters.AddWithValue("@tuNgay", tuNgay);
            cmd.Parameters.AddWithValue("@denNgay", denNgay);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

        // 🔹 Doanh thu theo loại phòng (theo khoảng ngày)
        public DataTable LayDoanhThuTheoLoaiPhong(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
        SELECT LP.mo_ta AS LoaiPhong, SUM(HD.tong_tien) AS TongDoanhThu
        FROM tblHoaDon HD
        JOIN tblPhieuDatPhong PDP ON HD.ma_pdp = PDP.ma_pdp
        JOIN tblPhong P ON PDP.ma_phong = P.ma_phong
        JOIN tblLoaiPhong LP ON P.loai_phong = LP.loai_phong
        WHERE HD.ngay_tra_phong BETWEEN @tuNgay AND @denNgay
        GROUP BY LP.mo_ta
        ORDER BY TongDoanhThu DESC";
            SqlCommand cmd = new SqlCommand(sql, sqlcnn);
            cmd.Parameters.AddWithValue("@tuNgay", tuNgay);
            cmd.Parameters.AddWithValue("@denNgay", denNgay);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

        // 🔹 Phòng – lượt đặt theo loại
        public DataTable LaySoLuotDatPhongTheoLoaiPhong(int nam)
        {
            string sql = @"
                SELECT LP.mo_ta AS LoaiPhong, COUNT(*) AS SoLuotDat
                FROM tblPhieuDatPhong PDP
                JOIN tblPhong P ON PDP.ma_phong = P.ma_phong
                JOIN tblLoaiPhong LP ON P.loai_phong = LP.loai_phong
                WHERE YEAR(PDP.ngay_dat) = @nam
                GROUP BY LP.mo_ta
                ORDER BY SoLuotDat DESC";
            SqlCommand cmd = new SqlCommand(sql, sqlcnn);
            cmd.Parameters.AddWithValue("@nam", nam);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }
    }
}

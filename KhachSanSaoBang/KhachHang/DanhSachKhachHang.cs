using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.KhachHang
{
    public class DanhSachKhachHang
    {
        private string cnn = "Data Source=hoangvux.database.windows.net;Initial Catalog=dataQLKS;User ID=ApplicationClient;Password=Abcd@123;Encrypt=True;";
        private SqlConnection cn;

        public DanhSachKhachHang()
        {
            cn = new SqlConnection(cnn);
        }

        // ============================================================
        // LOAD THÀNH VIÊN
        // ============================================================
        public DataTable LoadThanhVien()
        {
            string sql = @"
                SELECT ma_kh, ho_ten, sdt, diem,
                CASE 
	                WHEN diem < 1000 THEN N'Đồng'
	                WHEN diem < 5000 THEN N'Bạc'
	                ELSE N'Vàng'
                END AS Hang,
                CASE 
	                WHEN diem < 1000 THEN N'Không'
	                WHEN diem < 5000 THEN N'Giảm 5%'
	                ELSE N'Giảm 10%'
                END AS Voucher
                FROM tblKhachHang
                WHERE is_member = 1";

            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // ============================================================
        // TÌM KHÁCH THEO SDT
        // ============================================================
        public DataRow TimKhachTheoSDT(string sdt)
        {
            string sql = @"
                SELECT TOP 1 k.*
                FROM tblKhachHang k
                JOIN tblPhieuDatPhong p ON k.ma_kh = p.ma_kh
                WHERE k.sdt = @sdt";

            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            da.SelectCommand.Parameters.AddWithValue("@sdt", sdt);

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
                return null;

            return dt.Rows[0];
        }

        // =====================================================
        // KIỂM TRA KHÁCH ĐÃ LÀ THÀNH VIÊN CHƯA
        // =====================================================
        public bool IsMember(string maKH)
        {
            string sql = "SELECT is_member FROM tblKhachHang WHERE ma_kh = @ma";

            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@ma", maKH);

            cn.Open();
            object result = cmd.ExecuteScalar();
            cn.Close();

            if (result == null) return false;

            return Convert.ToBoolean(result);
        }

        // ============================================================
        // ĐĂNG KÝ THÀNH VIÊN
        // ============================================================
        public bool DangKyThanhVien(string maKH)
        {
            string sql = "UPDATE tblKhachHang SET is_member = 1 WHERE ma_kh = @ma_kh";

            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@ma_kh", maKH);

            cn.Open();
            int rows = cmd.ExecuteNonQuery();
            cn.Close();

            return rows > 0;
        }
        public DataRow GetKhachByMa(string ma)
        {
            string sql = "SELECT * FROM tblKhachHang WHERE ma_kh = @ma";

            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            da.SelectCommand.Parameters.AddWithValue("@ma", ma);

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0) return null;

            return dt.Rows[0];
        }

    }
}

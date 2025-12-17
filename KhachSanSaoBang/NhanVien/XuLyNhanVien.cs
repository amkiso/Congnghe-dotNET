using KhachSanSaoBang.Models.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace KhachSanSaoBang.NhanVien
{
    public class XuLyNhanVien
    {
        private readonly string cnn =
            "Data Source=hoangvux.database.windows.net;Initial Catalog=dataQLKS;User ID=ApplicationClient;Password=Abcd@123;Encrypt=True;";

        // ============================================================
        // LOAD DANH SÁCH NHÂN VIÊN
        // ============================================================
        public DataTable LoadNhanVien()
        {
            using (SqlConnection cn = new SqlConnection(cnn))
            {
                string sql = "SELECT * FROM tblNhanVien";
                SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // ============================================================
        // LẤY 1 NHÂN VIÊN
        // ============================================================
        public DataRow LayThongTinNhanVien(int maNV)
        {
            using (SqlConnection cn = new SqlConnection(cnn))
            {
                string sql = "SELECT * FROM tblNhanVien WHERE ma_nv = @ma";
                SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                da.SelectCommand.Parameters.Add("@ma", SqlDbType.Int).Value = maNV;

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
        }

        // ============================================================
        // THÊM NHÂN VIÊN
        // ============================================================
        public bool ThemNhanVien(tblNhanVien nv)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cnn))
                {
                    string sql = @"
                        INSERT INTO tblNhanVien
                        (ho_ten, gioi_tinh, ngay_sinh, dia_chi, sdt, que_quan, nam_bd, luong, trang_thai)
                        VALUES
                        (@ten, @gt, @ns, @dc, @sdt, @qq, @nam, @luong, 1)";

                    SqlCommand cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.Add("@ten", SqlDbType.NVarChar).Value = nv.ho_ten;
                    cmd.Parameters.Add("@gt", SqlDbType.NVarChar).Value = nv.gioi_tinh;
                    cmd.Parameters.Add("@ns", SqlDbType.Date).Value = (object)nv.ngay_sinh ?? DBNull.Value;
                    cmd.Parameters.Add("@dc", SqlDbType.NVarChar).Value = nv.dia_chi;
                    cmd.Parameters.Add("@sdt", SqlDbType.VarChar).Value = nv.sdt;
                    cmd.Parameters.Add("@qq", SqlDbType.NVarChar).Value = nv.que_quan;
                    cmd.Parameters.Add("@nam", SqlDbType.Int).Value = (object)nv.nam_bd ?? DBNull.Value;
                    cmd.Parameters.Add("@luong", SqlDbType.Int).Value = nv.luong;

                    cn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        // ============================================================
        // SỬA NHÂN VIÊN
        // ============================================================
        public bool SuaNhanVien(tblNhanVien nv)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cnn))
                {
                    string sql = @"
                        UPDATE tblNhanVien SET
                            ho_ten = @ten,
                            gioi_tinh = @gt,
                            ngay_sinh = @ns,
                            dia_chi = @dc,
                            sdt = @sdt,
                            que_quan = @qq,
                            nam_bd = @nam,
                            luong = @luong
                        WHERE ma_nv = @ma";

                    SqlCommand cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.Add("@ma", SqlDbType.Int).Value = nv.ma_nv;
                    cmd.Parameters.Add("@ten", SqlDbType.NVarChar).Value = nv.ho_ten;
                    cmd.Parameters.Add("@gt", SqlDbType.NVarChar).Value = nv.gioi_tinh;
                    cmd.Parameters.Add("@ns", SqlDbType.Date).Value = (object)nv.ngay_sinh ?? DBNull.Value;
                    cmd.Parameters.Add("@dc", SqlDbType.NVarChar).Value = nv.dia_chi;
                    cmd.Parameters.Add("@sdt", SqlDbType.VarChar).Value = nv.sdt;
                    cmd.Parameters.Add("@qq", SqlDbType.NVarChar).Value = nv.que_quan;
                    cmd.Parameters.Add("@nam", SqlDbType.Int).Value = (object)nv.nam_bd ?? DBNull.Value;
                    cmd.Parameters.Add("@luong", SqlDbType.Int).Value = nv.luong;

                    cn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        // ============================================================
        // XÓA NHÂN VIÊN
        // ============================================================
        public bool XoaNhanVien(int maNV)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cnn))
                {
                    string sql = "DELETE FROM tblNhanVien WHERE ma_nv = @ma";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.Add("@ma", SqlDbType.Int).Value = maNV;

                    cn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        // ============================================================
        // CẬP NHẬT TRẠNG THÁI
        // ============================================================
        public bool CapNhatTrangThai(int maNV, bool trangThai)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cnn))
                {
                    string sql = "UPDATE tblNhanVien SET trang_thai = @tt WHERE ma_nv = @ma";
                    SqlCommand cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.Add("@ma", SqlDbType.Int).Value = maNV;
                    cmd.Parameters.Add("@tt", SqlDbType.Bit).Value = trangThai;

                    cn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

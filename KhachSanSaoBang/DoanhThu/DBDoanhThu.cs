using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.DoanhThu
{
    public class DBDoanhThu
    {
        // Chuỗi kết nối của bạn
        string _cnn = KhachSanSaoBang.Properties.Settings.Default.dataQLKSConnectionString1;

        SqlConnection sqlcnn;
        SqlDataAdapter da;

        public DBDoanhThu()
        {
            sqlcnn = new SqlConnection(_cnn);
        }

        // 🔸 1. Lấy danh sách hóa đơn (lọc theo ngày và nhân viên)
        public DataTable LayDoanhThu(DateTime tuNgay, DateTime denNgay, string maNV, bool locTheoNgay)
        {
            // Mẹo: Dùng 1=1 để dễ nối chuỗi AND
            string sql = "SELECT * FROM tblHoaDon WHERE 1=1";

            // Nếu lọc theo ngày
            if (locTheoNgay)
            {
                sql += " AND ngay_tra_phong BETWEEN @tuNgay AND @denNgay";
            }

            // Nếu có lọc theo nhân viên
            if (!string.IsNullOrEmpty(maNV))
            {
                sql += " AND ma_nv = @maNV";
            }

            DataTable tbl = new DataTable();

            using (SqlConnection conn = new SqlConnection(_cnn))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Gán giá trị tham số
                    if (locTheoNgay)
                    {
                        cmd.Parameters.AddWithValue("@tuNgay", tuNgay);
                        cmd.Parameters.AddWithValue("@denNgay", denNgay);
                    }

                    if (!string.IsNullOrEmpty(maNV))
                    {
                        cmd.Parameters.AddWithValue("@maNV", maNV);
                    }

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tbl);
                }
            }
            return tbl;
        }

        // 🔸 2. Tính tổng doanh thu
        public decimal TinhTongDoanhThu(DataTable tbl)
        {
            decimal tong = 0;
            if (tbl != null && tbl.Rows.Count > 0)
            {
                foreach (DataRow r in tbl.Rows)
                {
                    // Đảm bảo cột tong_tien tồn tại và không null
                    if (tbl.Columns.Contains("tong_tien") && r["tong_tien"] != DBNull.Value)
                    {
                        tong += Convert.ToDecimal(r["tong_tien"]);
                    }
                }
            }
            return tong;
        }

        // 🔸 3. Load danh sách nhân viên
        public DataTable LoadNhanVien()
        {
            // Lưu ý: alias 'ten_nv' ở đây phải khớp với DisplayMember bên Form
            string query = "SELECT ma_nv, ho_ten AS ten_nv FROM tblNhanVien";

            DataTable tbl = new DataTable();
            using (SqlConnection conn = new SqlConnection(_cnn))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(tbl);
            }
            return tbl;
        }
    }
}
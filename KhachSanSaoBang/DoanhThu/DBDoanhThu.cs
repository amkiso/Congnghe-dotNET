using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace KhachSanSaoBang.DoanhThu
{
    public class DBDoanhThu
    {
        string _cnn = "Data Source=hoangvux.database.windows.net;Initial Catalog=dataQLKS;Persist Security Info=True;User ID=ApplicationClient;Password=Abcd@123;Encrypt=True;TrustServerCertificate=True;";
        SqlConnection sqlcnn;
        DataSet dataQLKS;
        SqlDataAdapter da;

        public DBDoanhThu()
        {
            sqlcnn = new SqlConnection(_cnn);
            dataQLKS = new DataSet();
        }

        // 🔸 1. Lấy danh sách hóa đơn (lọc theo ngày và nhân viên)
        public DataTable LayDoanhThu(DateTime tuNgay, DateTime denNgay, string maNV, bool locTheoNgay)
        {
            string sql = "SELECT * FROM tblHoaDon";
            bool coDieuKien = false;

            // Nếu lọc theo ngày
            if (locTheoNgay)
            {
                sql += " WHERE ngay_tra_phong BETWEEN @tuNgay AND @denNgay";
                coDieuKien = true;
            }

            // Nếu có lọc theo nhân viên
            if (!string.IsNullOrEmpty(maNV))
            {
                if (coDieuKien)
                    sql += " AND ma_nv = @maNV";
                else
                    sql += " WHERE ma_nv = @maNV";
            }

            SqlCommand cmd = new SqlCommand(sql, sqlcnn);

            // Gán giá trị tham số
            if (locTheoNgay)
            {
                cmd.Parameters.AddWithValue("@tuNgay", tuNgay);
                cmd.Parameters.AddWithValue("@denNgay", denNgay);
            }

            if (!string.IsNullOrEmpty(maNV))
                cmd.Parameters.AddWithValue("@maNV", maNV);

            da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

        // 🔸 2. Tính tổng doanh thu
        public decimal TinhTongDoanhThu(DataTable tbl)
        {
            decimal tong = 0;
            foreach (DataRow r in tbl.Rows)
            {
                if (r["tong_tien"] != DBNull.Value)
                    tong += Convert.ToDecimal(r["tong_tien"]);
            }
            return tong;
        }

        // 🔸 3. Load danh sách nhân viên
        public DataTable LoadNhanVien()
        {
            string query = "SELECT ma_nv, ho_ten AS ten_nv FROM tblNhanVien";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcnn);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

    }
}

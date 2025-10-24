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
        string _cnn = "Data Source=LAPTOP-MOJM17CB\\SQLEXPRESS02;Initial Catalog=dataQLKS;Integrated Security=True";
        SqlConnection sqlcnn;
        DataSet dataQLKS;
        SqlDataAdapter daDoanhThu;

        public DBDoanhThu()
        {
            sqlcnn = new SqlConnection(_cnn);
            dataQLKS = new DataSet();
            LoadDoanhThu();
        }

        // ✅ Nạp dữ liệu doanh thu
        public DataTable LoadDoanhThu()
        {
            string query = "SELECT * FROM tblHoaDon";
            daDoanhThu = new SqlDataAdapter(query, sqlcnn);
            dataQLKS.Tables.Clear();
            daDoanhThu.Fill(dataQLKS, "tblHoaDon");

            DataColumn[] key = new DataColumn[1];
            key[0] = dataQLKS.Tables["tblHoaDon"].Columns["ma_hd"];
            dataQLKS.Tables["tblHoaDon"].PrimaryKey = key;

            return dataQLKS.Tables["tblHoaDon"];
        }

        // ✅ Lấy doanh thu theo thời gian (và có thể theo nhân viên)
        public DataTable LayDoanhThuTheoNgay(DateTime tuNgay, DateTime denNgay, string maNV = "")
        {
            string query = "SELECT * FROM tblHoaDon WHERE ngay_tra_phong BETWEEN @tuNgay AND @denNgay";
            if (!string.IsNullOrEmpty(maNV))
                query += " AND ma_nv = @maNV";

            SqlCommand cmd = new SqlCommand(query, sqlcnn);
            cmd.Parameters.AddWithValue("@tuNgay", tuNgay);
            cmd.Parameters.AddWithValue("@denNgay", denNgay);
            cmd.Parameters.AddWithValue("@maNV", maNV);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

        // ✅ Tính tổng doanh thu từ DataTable
        public decimal TinhTongDoanhThu(DataTable tbl)
        {
            decimal tong = 0;
            foreach (DataRow row in tbl.Rows)
            {
                if (row["tong_tien"] != DBNull.Value)
                    tong += Convert.ToDecimal(row["tong_tien"]);
            }
            return tong;
        }

        public DataTable LoadNhanVien()
        {
            string query = "SELECT ma_nv, ho_ten FROM tblNhanVien";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcnn);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

        public DataTable LayDoanhThuTheoNhanVien(string maNV)
        {
            string query = "SELECT * FROM tblHoaDon WHERE ma_nv = @maNV";
            SqlCommand cmd = new SqlCommand(query, sqlcnn);
            cmd.Parameters.AddWithValue("@maNV", maNV);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

        public DataTable LayDoanhThuTheoLoaiPhong(string maLoaiPhong)
        {
            string query = @"SELECT * FROM tblHoaDon 
                     WHERE ma_pdp IN (
                         SELECT ma_pdp 
                         FROM tblPhieuDatPhong 
                         WHERE ma_loai_phong = @maLoaiPhong)";
            SqlCommand cmd = new SqlCommand(query, sqlcnn);
            cmd.Parameters.AddWithValue("@maLoaiPhong", maLoaiPhong);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            return tbl;
        }

    }
}

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
        string _cnn = "Data Source=LAPTOP-MOJM17CB\\SQLEXPRESS02;Initial Catalog=dataQLKS;Integrated Security=True";
        SqlConnection sqlcnn;
        DataSet dataQLKS;
        SqlDataAdapter daKhachHang;

        public DanhSachKhachHang()
        {
            sqlcnn = new SqlConnection(_cnn);
            dataQLKS = new DataSet();
            LoadKhachHang();
        }

        public DataTable LoadKhachHang()
        {
            string query = "SELECT * FROM tblKhachHang";
            daKhachHang = new SqlDataAdapter(query, sqlcnn);
            dataQLKS.Tables.Clear(); 
            daKhachHang.Fill(dataQLKS, "tblKhachHang");

            DataColumn[] key = new DataColumn[1];
            key[0] = dataQLKS.Tables["tblKhachHang"].Columns["ma_kh"];
            dataQLKS.Tables["tblKhachHang"].PrimaryKey = key;

            return dataQLKS.Tables["tblKhachHang"];
        }

        public bool Them(ThongTinKhachHang kh)
        {
            try
            {
                DataRow row = dataQLKS.Tables["tblKhachHang"].NewRow();
                row["ma_kh"] = kh.MaKH;
                row["HoTen"] = kh.HoTen;
                row["GioiTinh"] = kh.GioiTinh;
                row["NgaySinh"] = kh.NgaySinh;
                row["CCCD"] = kh.CCCD;
                row["Email"] = kh.Email;
                row["DiaChi"] = kh.DiaChi;
                row["SDT"] = kh.SDT;

                dataQLKS.Tables["tblKhachHang"].Rows.Add(row);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi thêm: " + ex.Message);
                return false;
            }
        }

        // ✅ Xóa khách hàng
        public bool Xoa(ThongTinKhachHang kh)
        {
            DataRow row = dataQLKS.Tables["tblKhachHang"].Rows.Find(kh.MaKH);
            if (row != null)
            {
                row.Delete();
                return true;
            }
            return false;
        }

        // ✅ Sửa khách hàng
        public bool Sua(ThongTinKhachHang kh)
        {
            DataRow row = dataQLKS.Tables["tblKhachHang"].Rows.Find(kh.MaKH);
            if (row != null)
            {
                row["HoTen"] = kh.HoTen;
                row["GioiTinh"] = kh.GioiTinh;
                row["NgaySinh"] = kh.NgaySinh;
                row["CCCD"] = kh.CCCD;
                row["Email"] = kh.Email;
                row["DiaChi"] = kh.DiaChi;
                row["SDT"] = kh.SDT;
                return true;
            }
            return false;
        }

        public bool Luu()
        {
            try
            {
                SqlCommandBuilder build = new SqlCommandBuilder(daKhachHang);
                daKhachHang.Update(dataQLKS, "tblKhachHang");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lưu: " + ex.Message);
                return false;
            }
        }

        public DataTable TimKiem(string tuKhoa)
        {
            DataTable tbl = dataQLKS.Tables["tblKhachHang"];
            DataView view = new DataView(tbl);
            view.RowFilter = $"ma_kh LIKE '%{tuKhoa}%' OR SDT LIKE '%{tuKhoa}%'";
            return view.ToTable();
        }
    }
}

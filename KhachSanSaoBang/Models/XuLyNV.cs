using KhachSanSaoBang.Models.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang.Models
{
    public class XulyNV
    {
        // Thay chuỗi kết nối này bằng chuỗi kết nối của bạn
        string _cnn = "Data Source=DESKTOP-65Q6L55;Initial Catalog=dataQLKS;Integrated Security=True;";
        SqlConnection sqlcnn;
        DataSet dataQLKS;
        SqlDataAdapter daNhanVien;

        public XulyNV()
        {
            sqlcnn = new SqlConnection(_cnn);
            dataQLKS = new DataSet();
            LoadNhanVien(); // Tải dữ liệu ngay khi khởi tạo
        }

        // Tải dữ liệu từ DB lên DataSet
        public DataTable LoadNhanVien()
        {
            try
            {
                // Lấy tất cả cột, kể cả cột trang_thai
                string query = "SELECT * FROM tblNhanVien";
                daNhanVien = new SqlDataAdapter(query, sqlcnn);

                // Cấu hình để adapter có thể tự động sinh lệnh Insert/Update/Delete
                SqlCommandBuilder builder = new SqlCommandBuilder(daNhanVien);

                if (dataQLKS.Tables.Contains("tblNhanVien"))
                {
                    dataQLKS.Tables["tblNhanVien"].Clear();
                }

                daNhanVien.Fill(dataQLKS, "tblNhanVien");

                // Thiết lập khóa chính để tìm kiếm (Find) hoạt động được
                DataColumn[] key = new DataColumn[1];
                key[0] = dataQLKS.Tables["tblNhanVien"].Columns["ma_nv"];
                dataQLKS.Tables["tblNhanVien"].PrimaryKey = key;

                return dataQLKS.Tables["tblNhanVien"];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi load: " + ex.Message);
                return null;
            }
        }

        // Thêm nhân viên mới
        public bool ThemNhanVien(tblNhanVien nv)
        {
            try
            {
                if (!dataQLKS.Tables.Contains("tblNhanVien"))
                {
                    LoadNhanVien();
                }

                // Kiểm tra kỹ lại lần nữa, nếu vẫn null thì báo lỗi và dừng
                if (dataQLKS.Tables["tblNhanVien"] == null)
                {
                    MessageBox.Show("Lỗi: Không thể kết nối cơ sở dữ liệu để lấy cấu trúc bảng.");
                    return false;
                }
                // Tạo một dòng mới dựa trên cấu trúc bảng trong DataSet
                DataRow row = dataQLKS.Tables["tblNhanVien"].NewRow();

                // Gán dữ liệu từ đối tượng nv vào dòng mới
                row["ma_nv"] = nv.ma_nv;
                row["ho_ten"] = nv.ho_ten;
                row["gioi_tinh"] = nv.gioi_tinh;
                row["ngay_sinh"] = nv.ngay_sinh; // Có thể null
                row["dia_chi"] = nv.dia_chi;
                row["sdt"] = nv.sdt; // Nếu có
                row["que_quan"] = nv.que_quan;
                row["nam_bd"] = nv.nam_bd;
                row["luong"] = nv.luong;
                row["trang_thai"] = true; // Mặc định là hoạt động

                // Thêm dòng vào bảng trong bộ nhớ
                dataQLKS.Tables["tblNhanVien"].Rows.Add(row);

                // Lưu xuống database
                return Luu();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi thêm: " + ex.Message);
                return false;
            }
        }

        // Sửa nhân viên
        public bool SuaNhanVien(tblNhanVien nv)
        {
            try
            {
                // Tìm dòng cần sửa theo khóa chính (ma_nv)
                DataRow row = dataQLKS.Tables["tblNhanVien"].Rows.Find(nv.ma_nv);

                if (row != null)
                {
                    row["ho_ten"] = nv.ho_ten;
                    row["gioi_tinh"] = nv.gioi_tinh;
                    row["ngay_sinh"] = nv.ngay_sinh;
                    row["dia_chi"] = nv.dia_chi;
                    row["que_quan"] = nv.que_quan;
                    row["nam_bd"] = nv.nam_bd;
                    row["luong"] = nv.luong;
                    // Không sửa ma_nv và trang_thai ở đây

                    return Luu(); // Lưu xuống DB
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi sửa: " + ex.Message);
                return false;
            }
        }

        // Xóa nhân viên (Xóa hẳn khỏi CSDL)
        public bool XoaNhanVien(int maNV)
        {
            try
            {
                DataRow row = dataQLKS.Tables["tblNhanVien"].Rows.Find(maNV);
                if (row != null)
                {
                    row.Delete(); // Đánh dấu dòng này là đã xóa
                    return Luu(); // Thực thi xóa dưới DB
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi xóa: " + ex.Message);
                return false;
            }
        }

        // Cập nhật trạng thái (Vô hiệu hóa / Kích hoạt)
        public bool CapNhatTrangThai(int maNV, bool trangThaiMoi)
        {
            try
            {
                DataRow row = dataQLKS.Tables["tblNhanVien"].Rows.Find(maNV);
                if (row != null)
                {
                    row["trang_thai"] = trangThaiMoi;
                    return Luu();
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi cập nhật trạng thái: " + ex.Message);
                return false;
            }
        }

        // Hàm chung để đẩy dữ liệu từ DataSet xuống Database
        public bool Luu()
        {
            try
            {
                // Tự động sinh lệnh và cập nhật
                SqlCommandBuilder build = new SqlCommandBuilder(daNhanVien);
                daNhanVien.Update(dataQLKS, "tblNhanVien");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lưu DB: " + ex.Message);
                return false;
            }
        }

        // Lấy thông tin chi tiết (Dùng cho form Sửa)
        public tblNhanVien LayThongTinNhanVien(int maNV)
        {
            DataRow row = dataQLKS.Tables["tblNhanVien"].Rows.Find(maNV);
            if (row != null)
            {
                // Chuyển DataRow thành object tblNhanVien để dễ dùng trên form
                tblNhanVien nv = new tblNhanVien();
                nv.ma_nv = (int)row["ma_nv"];
                nv.ho_ten = row["ho_ten"].ToString();
                nv.gioi_tinh = row["gioi_tinh"].ToString();
                if (row["ngay_sinh"] != DBNull.Value) nv.ngay_sinh = (DateTime)row["ngay_sinh"];
                nv.dia_chi = row["dia_chi"].ToString();
                nv.sdt = row["sdt"].ToString();
                nv.que_quan = row["que_quan"].ToString();
                if (row["nam_bd"] != DBNull.Value) nv.nam_bd = (int)row["nam_bd"];
                if (row["luong"] != DBNull.Value) nv.luong = Convert.ToInt32(row["luong"]); // Ép kiểu an toàn hơn
                //if (row["trang_thai"] != DBNull.Value) nv.trang_thai = (bool)row["trang_thai"];

                return nv;
            }
            return null;
        }
    }
}
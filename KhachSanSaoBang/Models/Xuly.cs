using KhachSanSaoBang.Models.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Syncfusion.Windows.Forms.Tools.TextBoxExt;
namespace KhachSanSaoBang.Models
{
    public class Xuly
    {
        SQLDataDataContext db = new SQLDataDataContext();
        public Xuly()
        {

        }
        //Xử lý đăng nhập và lưu session
        public bool Dangnhap(tblNhanVien nv)
        {
            var user = db.tblNhanViens.FirstOrDefault(u =>
                    u.tai_khoan == nv.tai_khoan &&
                    u.mat_khau == nv.mat_khau);

            if (user == null) { return false; }
            else
            {
                var obj = (from u in db.tblNhanViens
                           join cv in db.tblChucVus on u.ma_chuc_vu equals cv.ma_chuc_vu
                           where u.tai_khoan == nv.tai_khoan && u.mat_khau == nv.mat_khau
                           select new { u.ho_ten, u.ma_nv, cv.chuc_vu }).FirstOrDefault();
                Session.Role = obj.chuc_vu;
                Session.UserId = obj.ma_nv;
                Session.UserName = obj.ho_ten;
                return true;
            }
        }
        //Lấy list toàn bộ thông tin dịch vụ hiện có
        public List<tblDichVu> dsDichVu()
        {
            return (from u in db.tblDichVus select u).ToList();
        }
        //Lấy list toàn bộ thông tin tình trạng phòng
        public List<tblTinhTrangPhong> tinhtrangp()
        {
            return (from u in db.tblTinhTrangPhongs select u).ToList();
        }
        //Lấy thông tin dịch vụ khách đã sử dụng của phòng hiện tại
        public List<DichVuDaDung> listdichvup(int ma)
        {
            List<DichVuDaDung> list = new List<DichVuDaDung>();
            var query = from dvd in db.tblDichVuDaDats
                        join hd in db.tblHoaDons on dvd.ma_hd equals hd.ma_hd
                        join pdp in db.tblPhieuDatPhongs on hd.ma_pdp equals pdp.ma_pdp
                        join dv in db.tblDichVus on dvd.ma_dv equals dv.ma_dv
                        where pdp.ma_phong == ma && hd.ma_tinh_trang == 1
                        select new
                        {
                            dv.ten_dv,
                            dv.gia,
                            dv.don_vi,
                            dvd.so_luong
                        };
            foreach (var item in query)
            {
                list.Add(new DichVuDaDung
                {
                    Tendv = item.ten_dv,
                    Donvi = item.don_vi,
                    Soluong = (int)item.so_luong,
                    Giaban = (float)item.gia
                });
            }
            return list;
        }
        //tính toán số lượng khách dự vào tính hợp lệ của chuỗi json từ db
        public int Soluongkhach(string ttkhachtheo)
        {
            try
            {
                // Phân tích chuỗi JSON thành JArray
                JArray arr = JArray.Parse(ttkhachtheo);
                int count = 1;

                foreach (var item in arr)
                {
                    string hoten = (string)item["hoten"];
                    string tuoi = (string)item["tuoi"];

                    // Kiểm tra điều kiện hợp lệ: hoten và tuoi không rỗng hoặc null
                    if (!string.IsNullOrWhiteSpace(hoten) && !string.IsNullOrWhiteSpace(tuoi))
                    {
                        count++;
                    }
                }

                return count;
            }
            catch (Exception ex)
            {

                return 1;
            }
        }
        //Lấy thông tin phòng cho giao diện
        public List<tblPhong> tomau()
        {
            return (from u in db.tblPhongs select u).ToList();
        }
        /// <summary>
        /// truyền vào giá trị mới/cũ của trạng thái phòng sau đó trả về số nguyên cho case
        /// từ 1-2: return 1 ; 7-2: return 6
        /// 2-3: return 2 ; 6-7: return 5
        /// 3-1:return 3 ; 1-6: return 4
        /// bảo trì phòng: 1-4 hoặc 1-5: return 0;
        /// Ngoại lệ: return -1;
        /// </summary>
        /// <param name="old"></param>
        /// <param name="pnew"></param>
        /// <returns></returns>
        public int GetRoomCase(int old, int pnew)
        {
            /*Sơ đồ hoạt động: 
                        nếu thay đổi từ 1-2: Mở cửa sổ nhập thông tin khách hàng thuê phòng, set ngày giờ vào, phiếu đặt phòng và tạo hóa đơn
                        Nếu thay đổi từ 2-3: Kiểm tra xem phòng có được thanh toán hay chưa, 
                                             nếu đã thay toán thì cho phép đổi trạng thái trực tiếp
                                             Nếu chưa thay toán thì thông báo "Phát hiện phòng này chưa thanh toán, bạn có muốn thanh toán hóa đơn cho phòng này không ?(Yes/No)
                                             Yes: mở thêm cửa sổ thanh toán, tiến hành nhập thông tin thanh toán và thanh toán hóa đơn
                                             No: return và không cho phép thay đổi
                        Nếu thay đổi từ 3-1: Thông báo hỏi xác nhận đã dọn phòng xong ?(Yes/No) Yes-> thay đổi trạng thái, No->return
                        Nếu thay đổi từ 1-6: Không thể thay đổi trạng thái từ 1->6 vì chức năng này thuộc về người dùng đặt phòng trên website
                        Nếu thay đổi từ 6-7: Thông báo hỏi xác nhận phòng ? (Yes/No)
                        Thay đổi từ 7-2: Hiện cửa sổ nhập thông tin khách nhận phòng,Ghi lại ngày vào (ngay_vao) trong database thành ngày giờ hiện tại, Cập nhật dữ liệu trên db
                        Các trạng thái 4,5 của phòng chỉ có thể thay đổi khi phòng trống(1)
                        Ngoài ra không chấp nhận thay đổi khác với luồng này*/
            if (old == 1 && pnew == 2)
            {
                return 1;
            }
            else if (old == 2 && pnew == 3)
            {
                return 2;
            }
            else if (old == 3 && pnew == 1)
            {
                return 3;
            }
            else if (old == 1 && pnew == 6)
            {
                return 4;
            }
            else if (old == 6 && pnew == 7)
            {
                return 5;
            }
            else if (old == 7 && pnew == 2)
            {
                return 6;
            }
            else if (old == 1 && (pnew == 4 || pnew == 5))
            {
                return 0;
            }
            else return -1;

        }
        //Lấy thông tin tất cả các phòng
        public List<Thongtinchung> GetAllThongtinphong()
        {
            List<Thongtinchung> list = new List<Thongtinchung>();

            // Lấy toàn bộ danh sách phòng
            var dsPhong = from u in db.tblPhongs
                          join lp in db.tblLoaiPhongs on u.loai_phong equals lp.loai_phong
                          select new
                          {
                              u.ma_phong,
                              u.ma_tinh_trang,
                              lp.mo_ta,
                              u.so_phong,
                              u.ma_tang,
                              lp.gia,
                              lp.ti_le_phu_thu
                          };

            // Lặp qua từng phòng và lấy thông tin chi tiết
            foreach (var phong in dsPhong)
            {
                Thongtinchung tt = new Thongtinchung();
                tt.Trangthai = (int)phong.ma_tinh_trang;
                tt.Loaiphong = phong.mo_ta;
                tt.Maphong = phong.ma_phong;
                tt.Tenphong = phong.so_phong;
                tt.Matang = (int)phong.ma_tang;
                tt.Giathue = (int)phong.gia;
                tt.Phuthu = (int)phong.ti_le_phu_thu;

                // Nếu phòng đang có người (không phải trống)
                if (phong.ma_tinh_trang != 1)
                {
                    var phongp = (from u in db.tblPhieuDatPhongs
                                  join k in db.tblKhachHangs on u.ma_kh equals k.ma_kh
                                  where u.ma_phong == phong.ma_phong && (u.ma_tinh_trang == 1 || u.ma_tinh_trang == 2)
                                  select new
                                  {
                                      u.ngay_dat,
                                      u.ngay_ra,
                                      u.ngay_vao,
                                      u.ma_tinh_trang,
                                      k.ho_ten,
                                      u.thong_tin_khach_thue
                                  }).FirstOrDefault();

                    if (phongp != null)
                    {
                        tt.Thoigian_nhanphong = phongp.ngay_vao.ToString();
                        tt.Ngayra = phongp.ngay_ra.ToString();
                        tt.Ngayvao = phongp.ngay_dat.ToString();
                        tt.Khachhang = phongp.ho_ten;
                        tt.Sokhach = Soluongkhach(phongp.thong_tin_khach_thue);
                    }
                    else
                    {
                        tt.Thoigian_nhanphong = "-";
                        tt.Ngayra = "-";
                        tt.Ngayvao = "-";
                        tt.Khachhang = "-";
                        tt.Sokhach = 0;
                    }
                }
                else
                {
                    tt.Thoigian_nhanphong = "-";
                    tt.Ngayra = "-";
                    tt.Ngayvao = "-";
                    tt.Khachhang = "-";
                    tt.Sokhach = 0;
                }

                // Thêm vào danh sách
                list.Add(tt);
            }

            return list;
        }

        //Lấy thông tin theo phòng
        public Thongtinchung GetThongtinphong(int ma)
        {
            Thongtinchung tt = new Thongtinchung();
            var phong = (from u in db.tblPhongs
                         join lp in db.tblLoaiPhongs
                         on u.loai_phong equals lp.loai_phong
                         where u.ma_phong == ma
                         select new
                         {
                             u.ma_phong,
                             u.ma_tinh_trang,
                             lp.mo_ta,
                             u.so_phong,
                             u.ma_tang,
                             lp.gia,
                             lp.ti_le_phu_thu
                         }).FirstOrDefault();
            tt.Trangthai = (int)phong.ma_tinh_trang;
            tt.Loaiphong = phong.mo_ta;
            tt.Maphong = phong.ma_phong;
            tt.Tenphong = phong.so_phong;
            tt.Matang = (int)phong.ma_tang;
            tt.Giathue = (int)phong.gia;
            tt.Phuthu = (int)phong.ti_le_phu_thu;
            if (phong.ma_tinh_trang != 1)
            {
                var phongp = (from u in db.tblPhieuDatPhongs
                              join k in db.tblKhachHangs
                              on u.ma_kh equals k.ma_kh
                              where u.ma_phong == ma && (u.ma_tinh_trang == 1 || u.ma_tinh_trang == 2)
                              select new { u.ngay_dat, u.ngay_ra, u.ngay_vao, u.ma_tinh_trang, k.ho_ten, u.thong_tin_khach_thue, u.ma_pdp }).FirstOrDefault();
                if (phongp != null)
                {
                    tt.Thoigian_nhanphong = phongp.ngay_vao.ToString();
                    tt.Ngayra = phongp.ngay_ra.ToString();
                    tt.Ngayvao = phongp.ngay_dat.ToString();
                    tt.Khachhang = phongp.ho_ten;
                    tt.Sokhach = Soluongkhach(phongp.thong_tin_khach_thue);
                    tt.Maphieudp = phongp.ma_pdp;
                }
                else
                {

                    tt.Thoigian_nhanphong = "-";
                    tt.Ngayra = "-";
                    tt.Ngayvao = "-";
                    tt.Khachhang = "-";
                    tt.Sokhach = 0;
                }
            }
            else
            {
                tt.Thoigian_nhanphong = "-";
                tt.Ngayra = "-";
                tt.Ngayvao = "-";
                tt.Khachhang = "-";
                tt.Sokhach = 0;
            }

            Session.maphonghientai = tt.Maphong;
            return tt;

        }
        //Trả hàng
        public bool Tradichvu(int ma_p, int madv, int soluong)
        {
            //Dịch vụ trả phải tương ứng với số phòng và hóa đơn chưa thanh toán tương ứng
            try
            {
                // 1. Lấy mã hóa đơn
                int current_ma_hd = (from u in db.tblHoaDons
                                     join p in db.tblPhieuDatPhongs on u.ma_pdp equals p.ma_pdp
                                     where p.ma_phong == ma_p && u.ma_tinh_trang == 1 // 1 = chưa thanh toán
                                     select u.ma_hd).FirstOrDefault();

                if (current_ma_hd == 0)
                {
                    return false;
                }

                // 2. Tìm dịch vụ đã gọi trong hóa đơn đó
                var existingService = db.tblDichVuDaDats.FirstOrDefault(d => d.ma_hd == current_ma_hd && d.ma_dv == madv);

                if (existingService == null)
                {
                    return false;
                }

                if (soluong < existingService.so_luong)
                {
                    // Trả một phần -> Cập nhật (giảm) số lượng
                    existingService.so_luong -= soluong;
                }
                else
                {
                    // Trả hết (hoặc lỡ nhập số > số đã gọi -> Xóa
                    db.tblDichVuDaDats.DeleteOnSubmit(existingService);
                }
                // 4. Lưu thay đổi
                // TRIGGER 'trg_Update_TonKho_DichVu' trong CSDL sẽ tự động kích hoạt
                // và cộng trả lại 'soluongTra' vào tblDichVu.ton_kho
                db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //Gọi dịch vụ
        public bool GoiDichVu(int ma_p, int madv, int soluong)
        {
            try
            {   //Lấy mã hóa đơn của phòng
                int current_ma_hd = (from u in db.tblHoaDons
                                     join p in db.tblPhieuDatPhongs on u.ma_pdp equals p.ma_pdp
                                     where p.ma_phong == ma_p && u.ma_tinh_trang == 1
                                     select u.ma_hd).FirstOrDefault();

                if (current_ma_hd == 0)
                {
                    return false;
                }
                //Nếu dịch vụ đã có gọi từ trước thì tăng thêm số lượng
                var existingService = db.tblDichVuDaDats.FirstOrDefault(d => d.ma_hd == current_ma_hd && d.ma_dv == madv);

                if (existingService != null)
                {
                    existingService.so_luong += soluong;
                }
                else // chưa có thì thêm mới vào bảng dichvudatat
                {
                    var dv = new tblDichVuDaDat();
                    dv.ma_hd = current_ma_hd;
                    dv.ma_dv = madv;
                    dv.so_luong = soluong;

                    db.tblDichVuDaDats.InsertOnSubmit(dv);
                }
                //lưu
                db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //Tính tiền tạm tính
        public string TienTamTinh(int ma)
        {

            //Tiền tạm tính được tính bằng: số tiền dịch vụ đã dùng + ( tiền phòng * số ngày đặt ) + ( %phụ thu tiền phòng * Số ngày ở thêm )
            Thongtinchung tt = GetThongtinphong(ma);
            if (tt.Trangthai == 1) return "0 VNĐ";
            else
            {
                //Nếu trạng thái là đang sử dụng (2) thì mới tính tiền
                if (tt.Trangthai == 2)
                {
                    List<DichVuDaDung> dv = listdichvup(ma);
                    float tiendv = dv.Sum(t => t.Giaban * t.Soluong);
                    //tính số ngày ở
                    float tongngayo = (float)(DateTime.Now - DateTime.Parse(tt.Ngayvao)).TotalDays; //dùng now để cập nhật số tiền ở đến hiện tại ( tạm tính )
                    //Tính số ngay đặt và ngày ở thêm
                    int sngaydat = (int)(DateTime.Parse(tt.Ngayra) - DateTime.Parse(tt.Ngayvao)).TotalDays;
                    float thoigianthem = tongngayo - sngaydat;
                    //Tiến hành tính tiền
                    //Tiền tạm tính được tính bằng: số tiền dịch vụ đã dùng + ( tiền phòng * số ngày đặt ) + ( %phụ thu tiền phòng * Số ngày ở thêm )
                    return (tiendv + (tt.Giathue * sngaydat) + ((tt.Giathue / 100) * tt.Phuthu * thoigianthem)).ToString() + " VNĐ";
                }

            }
            return "0 VNĐ";
        }
        //Lấy mã trạng thái phòng
        public int GetCodeRoomStatus(int ma_p)
        {
            return (int)db.tblPhongs.Where(t => t.ma_phong == ma_p).FirstOrDefault().ma_tinh_trang;
        }
        //Lấy tên trạng thái phòng
        public string GetStringRoomStatus(int ma_tinh_trang)
        {
            return db.tblTinhTrangPhongs.FirstOrDefault(t => t.ma_tinh_trang == ma_tinh_trang).mo_ta;
        }
        //Kiểm tra tình trạng phòng đã được thanh toán hay chưa ?
        public bool CheckHoaDon(int ma_p)
        {
            try
            {
                return true;
            }
            catch (Exception ex) { return false; }
        }
        //Đổi trạng thái phòng
        public bool ChangeRoomStatus(int maphong, int matrangthai)
        {
            try
            {
                {
                    // Tìm phòng theo mã phòng
                    var phong = db.tblPhongs.SingleOrDefault(p => p.ma_phong == maphong);

                    if (phong == null)
                        return false; // Không tìm thấy phòng

                    // Cập nhật trạng thái
                    phong.ma_tinh_trang = matrangthai;

                    // Lưu thay đổi xuống database
                    db.SubmitChanges();
                }

                return true; // Thành công
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần, ví dụ:
                // MessageBox.Show("Lỗi khi cập nhật trạng thái phòng: " + ex.Message);
                return false;
            }
        }

        //Tra cứu thông tin dựa vào số điện thoại hoặc CCCD của khách hàng
        public ThongtinTracuu GetThongtinKH(string _input)
        {
            ThongtinTracuu data = new ThongtinTracuu();
            string makh = GetMaKH(_input);
            if (makh == null) { return null; }
            else
            {
                try
                {
                    //Lấy thông tin khách hàng
                    var datakh = (from u in db.tblKhachHangs where u.ma_kh == makh select new { u.ma_kh, u.ho_ten, u.mail, u.cmt, u.sdt, u.diem }).FirstOrDefault();
                    //Lấy thông tin đặt phòng
                    data.So_Phong_chua_Xac_nhan = GetSoPhongChoXacNhan(makh);//chờ xác nhận(trạng thái phiếu = 1)
                    data.So_phong_dang_dat = GetSoPhongDangDat(makh);//đã đặt và chờ ckecin(trạng thái phiếu = 2)
                    data.Phong_cho_xac_nhan = GetlistPhongTrangThai(makh, 1);//chờ xác nhận(trạng thái phiếu = 1)
                    data.Phong_dang_dat = GetlistPhongTrangThai(makh, 2);//đã đặt và chờ ckecin(trạng thái phiếu = 2)
                    data.Cccd = datakh.cmt;
                    data.So_phong_da_dat = GetSoPhongDaDat(makh);
                    data.Hoten = datakh.ho_ten;
                    data.Diem_tich_luy = (int)datakh.diem;
                    data.Sdt = datakh.sdt;
                    data.Phong_da_dat = GetSoPhongDaDat(makh);//đã đặt(trạng thái phiếu = 4)
                    data.Makh = makh;
                    return data;
                }
                catch
                {
                    return null;
                }
            }
        }
        //GET Mã khách hàng
        public string GetMaKH(string _input)
        {
            if (!ChecKH(_input)) { return null; }
            else
            {
                var kh = db.tblKhachHangs.FirstOrDefault(t => t.cmt == _input || t.sdt == _input);
                return kh.ma_kh;
            }
        }
        //Kiểm tra khách hàng có tài khoản hay chưa dựa vào sdt hoặc cccd
        public bool ChecKH(string _input)
        {
            try
            {
                var kh = db.tblKhachHangs.FirstOrDefault(t => t.cmt == _input || t.sdt == _input);
                if (kh == null) return false;
                else return true;

            }
            catch (Exception e) { return false; }

        }
        //Kiểm tra khách hàng có trùng thông tin hay không
        public int CheckTrungKhachHang(tblKhachHang kh)
        {

            //kiểm tra trùng tên tài khoản
            if (db.tblKhachHangs.Where(t => t.ma_kh == kh.ma_kh) != null)
            {
                return 1;
            }
            //kiểm tra trùng cccd
            else if (db.tblKhachHangs.Where(t => t.cmt == kh.cmt) != null)
            {
                return 2;
            }
            //kiểm tra trùng sdt
            else if (db.tblKhachHangs.Where(t => t.sdt == kh.sdt) != null)
            {
                return 3;
            }
            //kiểm tra trùng email
            else if (db.tblKhachHangs.Where(t => t.mail == kh.mail) != null)
            {
                return 4;
            }
            else return 0;
        }
        //Đăng ký tài khoản cho khách hàng mới
        public bool DangkyKH(tblKhachHang khach)
        {
            try

            {
                db.tblKhachHangs.InsertOnSubmit(khach);
                return true;
            }
            catch (Exception e) { return false; }
        }
        //Lấy danh sách tên phòng theo trạng thái
        public List<string> GetlistPhongTrangThai(string _input, int trangthai)
        {
            try
            {
                /*Lấy danh sách tên của các phòng mà khách hàng nay đã đặt
                Cách làm: Lấy danh sách mã phòng của khách hàng tương ứng trong phiếu đặt phòng có trạng thái  tương ứng
                Sau đó có được mã phòng để tra cứu tên phòng và lưu vào list*/

                List<int> sophong = getlistmaphongtrangthai(_input, trangthai);
                List<string> tenp = new List<string>();
                foreach (int i in sophong)
                {
                    var phong = db.tblPhongs.FirstOrDefault(t => t.ma_phong == i);
                    string room_name = phong.so_phong;
                    tenp.Add(room_name);
                }

                return tenp;
            }
            catch (Exception e) { return null; }
        }
        //Lấy danh sách mã phòng theo trạng thái
        public List<int> getlistmaphongtrangthai(string _input, int trangthai)
        {
            try
            {
                List<int> sophong = new List<int>();
                var phongkh = db.tblPhieuDatPhongs.Where(t => t.ma_kh == _input && t.ma_tinh_trang == trangthai).ToList();
                foreach (var item in phongkh)
                {
                    int sop = (int)item.ma_phong;
                    sophong.Add(sop);
                }

                return sophong;
            }
            catch (Exception e) { return null; }
        }

        //Lấy số phòng dựa theo trạng thái muốn tra cứu và khách hàng tương ứng
        public int GetSoPhongKH_TrangThai(string _input, int trangthai)
        {
            return db.tblPhieuDatPhongs.Where(t => t.ma_kh == _input && t.ma_tinh_trang == trangthai).Count();
        }
        //Lấy số phòng đang chờ check-in của khách hàng(mã)
        public int GetSoPhongDangDat(string _input)
        {
            //Số phòng đang đặt và chờ checkin = count* số phiếu đặt phòng của khách hàng tương ứng && trạng thái = 2(Đã xong-> có nghĩa là đã được nhân viên xác nhận)
            return db.tblPhieuDatPhongs.Where(t => t.ma_kh == _input && t.ma_tinh_trang == 2).Count();
        }
        //Lấy số phòng đang chờ xác nhận của khách hàng(mã)
        public int GetSoPhongChoXacNhan(string _input)
        {
            return db.tblPhieuDatPhongs.Where(t => t.ma_kh == _input && t.ma_tinh_trang == 1).Count();
        }
        //Lấy số phòng đã đặt tại khách sạn của khách hàng(mã)
        public int GetSoPhongDaDat(string _input)
        {
            //Số phòng đã đặt  = count* số phiếu đặt phòng của khách hàng tương ứng && trạng thái = 4(Đã thanh toán)
            return db.tblPhieuDatPhongs.Where(t => t.ma_kh == _input && t.ma_tinh_trang == 4).Count();
        }
        //chuyển thông tin căn cước công dân khách hàng sang định dạng json
        public string ThongtinKH_To_Json(List<KhachThue> kht)
        {
            if (kht == null || kht.Count == 0)
            {
                return "[]"; // Trả về mảng JSON rỗng nếu danh sách trống
            }
            else
            {
                var thongTinList = kht.Select(kh => new
                {
                    hoten = kh.Tenkh,
                    socmt = kh.Cccd,
                    tuoi = kh.Tuoi,
                    ngsinh = kh.Ngsinh
                }).ToList();
                return JsonConvert.SerializeObject(thongTinList);
            }
        }
        //Tạo hóa đơn khi xác nhận cho phiếu đặt phòng
        public bool CreateTempHoaDon(int mapdp)
        {
            try
            {
                tblHoaDon hd = new tblHoaDon();
                hd.ma_pdp = mapdp;
                hd.ma_tinh_trang = 1; //Chưa thanh toán
                db.tblHoaDons.InsertOnSubmit(hd);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }
        public int AutoRoomBook(tblPhieuDatPhong pdp)
        {
            try
            {
                if (!CreateNewPhieuDatPhong(pdp))
                {
                    return 1;
                }
                else
                {
                    //Lấy mã phiếu đặt phòng mói tạo có trạng thái  = 1
                    int mapdp = (from u in db.tblPhieuDatPhongs
                                 where u.ma_tinh_trang == 1 && u.ma_phong == pdp.ma_phong && u.ma_kh == pdp.ma_kh
                                 select u.ma_pdp).FirstOrDefault();
                    //sau đó tạo hóa đơn
                    CreateTempHoaDon(mapdp);
                    //đặt trạng thái phiếu đặt phòng về thành 2(đã xong->đã được xác nhận và đang sử dụng phòng)
                    var phieu = db.tblPhieuDatPhongs.SingleOrDefault(p => p.ma_pdp == mapdp);
                    if (phieu != null)
                    {
                        phieu.ma_tinh_trang = 2;
                    }
                    else return 2;//Đặt trạng thái thất bại vì không tìm thấy phiếu đặt phòng tương ứng
                                  //Đổi trạng thái phòng thành 2( đang sử dụng)

                    if (!ChangeRoomStatus((int)pdp.ma_phong, 2))
                    {
                        return 3;//không thể cập nhật trạng thái phòng
                    }
                    db.SubmitChanges();
                }
                return 0;//Không có lỗi
            }
            catch (Exception e) { return -1; }//Lỗi không xác định
        }
        public bool CreateNewPhieuDatPhong(tblPhieuDatPhong p)
        {
            try
            {
                {
                    // Thêm đối tượng mới vào bảng
                    db.tblPhieuDatPhongs.InsertOnSubmit(p);
                    ChangeRoomStatus((int)p.ma_phong, 6);
                    // Lưu thay đổi xuống database
                    db.SubmitChanges();
                }
                return true;
            }
            catch (Exception e) { return false; }
        }
        //Lấy thông tin thanh toán
        public ThongtinThanhToan GetThongtinThanhToan(int ma_p)
        {
            try
            {
                ThongtinThanhToan data = new ThongtinThanhToan();
                //Lấy thông tin phòng hiện tại
                Thongtinchung ttp = GetThongtinphong(ma_p);
                data.Dichvusudung = listdichvup(ma_p);
                data.Tenkh = db.tblKhachHangs.FirstOrDefault(t => t.ma_kh == ttp.Khachhang).ho_ten;
                data.Makh = ttp.Khachhang;
                data.Sophong = ttp.Tenphong;
                data.Giaphong = ttp.Giathue;
                data.Ngayvao = DateTime.Parse(ttp.Ngayvao);
                data.Ngaydukienra = DateTime.Parse(ttp.Ngayra);
                data.Ngayra = DateTime.Now;
                data.Mahd = ttp.Maphieudp;
                data.Tilephuthu = ttp.Phuthu;
                data.sokhach = ttp.Sokhach;
                data.Tienkhachdua = 0;
                return data;
            }
            catch { return null; }

        }
        //Đổi trạng thái phiếu đặt phòng
        public bool ChangeRoomForm(int mapdp, int trangthai)
        {
            var pdp = db.tblPhieuDatPhongs.FirstOrDefault(t => t.ma_pdp == mapdp);
            if (pdp != null)
            {
                pdp.ma_tinh_trang = trangthai;
                db.SubmitChanges();
                return true;
            }
            else return false;
        }
        //Thanh toán
        
        public bool ThanhToanHoaDon(ThongtinThanhToan hd, int mathanhtoan)
        {
            try
            {
                var hoadon = db.tblHoaDons.FirstOrDefault(t => t.ma_hd == hd.Mahd);
                if (hoadon != null)
                {
                    hoadon.ngay_tra_phong = DateTime.Now;
                    hoadon.tien_dich_vu = hd.Tiendichvu;
                    hoadon.tien_phong = hd.Giaphong;
                    hoadon.phu_thu = hd.Thanhtienphuthu;
                    hoadon.tong_tien = hd.Tongtien;
                    hoadon.ma_thanh_toan = mathanhtoan;
                    hoadon.ma_khuyenmai = hd.Makm;
                    hoadon.ma_nv = Session.UserId;
                    hoadon.ma_tinh_trang = 2; //Đã thanh toán
                    ChangeRoomForm((int)hoadon.ma_pdp, 4);//Đã xong
                    db.SubmitChanges();
                    ChangeRoomStatus(Session.maphonghientai, 3);
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }
        //Kiểm tra mã khuyến mãi
        public bool CheckMaKM(string makm)
        {
            var km = db.tblKhuyenmais.FirstOrDefault(t => t.ma_khuyenmai == makm && t.ngayketthuc >= DateTime.Now && t.ngaybatdau <= DateTime.Now);
            if (km != null) return true;
            else return false;
        }
        //Lấy số tiền chiết khấu của khuyến mãi
        public float GetTienChietKhau(string makm, float tongtien)
        {
            var km = db.tblKhuyenmais.FirstOrDefault(t => t.ma_khuyenmai == makm);
            if (km != null)
            {
                if (km.loai_km == true) //Giảm theo %
                {
                    return (tongtien / 100) * (float)km.giatri;
                }
                else //Giảm theo tiền
                {
                    return (float)km.giatri;
                }
            }
            else return 0;
        }
        public int GetTileChietKhau(string makm)
        {
            var km = db.tblKhuyenmais.FirstOrDefault(t => t.ma_khuyenmai == makm);
            if (km != null)
            {
                if (km.loai_km == true) //Giảm theo %
                {
                    return (int)km.giatri;
                }
                else //Giảm theo tiền
                {
                    return 0;
                }
            }
            
            else return 0;
        }
        //Đếm số phòng đang chờ xác nhận
        public int GetSoPhongChoXacNhan()
        {
            return (from p in db.tblPhongs where p.ma_tinh_trang==6 select p.ma_phong).Count();
        }
        public bool XacNhanPhong(int map)
        {
            try
            {
                //Xác nhận phòng
                var list = db.tblPhieuDatPhongs
                 .Where(x => x.ma_tinh_trang == 1 && x.ma_phong == map)
                 .FirstOrDefault();
                    list.ma_tinh_trang = 2;
                ChangeRoomStatus(map, 7);
                CreateTempHoaDon(list.ma_pdp);
                db.SubmitChanges();
                //Tạo hóa đơn
                return true;

            }
            catch { return false; }
        }
    }  }

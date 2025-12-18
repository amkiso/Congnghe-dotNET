using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhachSanSaoBang.Models;

namespace KhachSanSaoBang.Models
{
    
    /// <summary>
    /// Mô phỏng Session lưu trữ thông tin người dùng hiện tại như trên website
    /// </summary>
    public static class Session
    {
        public static bool isdialog { get; set; }
        public static int Madv { get; set; }
        public static int UserId { get; set; }
        public static string UserName { get; set; }
        public static string Role { get; set; }
        public static int Roleid { get; set; }
        public static string UserEmail { get; set; }
        public static int maphonghientai { get; set; }
        public static int trangthaiphong { get; set; }
        public static string tenphonghientai { get; set; }
        /// <summary>
        /// Cờ Trạng thái của hành động thực hiện tiếp theo khi hoạt động ở cửa sổ khác
        /// Cờ mang giá trị false nếu hành động bị hủy hoặc thất bại và ngược lại
        /// </summary>
        public static bool Acction_status { get; set; }
        public static void Reset()
        {
            UserId = 0;
            UserName = null;
            Role = null;
            UserEmail = null;
            maphonghientai = 0;
            Acction_status = false;
        }
    }
}

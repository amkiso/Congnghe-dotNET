
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrangChu());

            //Dangnhap frm = new Dangnhap();

            //// 2. Dùng ShowDialog() để chạy form đăng nhập và chờ kết quả
            ////    Chương trình sẽ tạm dừng ở đây cho đến khi form login đóng lại.
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    // 3. NẾU kết quả trả về là OK (đăng nhập thành công)
            //    //    THÌ mới chạy form chính là TrangChu.
            //    Application.Run(new TrangChu());
            //}

            //// Nếu kết quả không phải OK (người dùng đóng form, đăng nhập thất bại...)
            //// thì hàm Main() sẽ kết thúc và ứng dụng tự đóng, không làm gì cả.
        }
    }
}

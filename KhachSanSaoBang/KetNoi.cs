using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang
{
    public partial class KetNoi : Form
    {
        public KetNoi()
        {
            InitializeComponent();
            btn_ketnoi.Click += Btn_ketnoi_Click;
            btn_Kiemtra.Click += Btn_Kiemtra_Click;
        }

        private void Btn_Kiemtra_Click(object sender, EventArgs e)
        {
            string connString = GetConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open(); // Thử mở kết nối
                    MessageBox.Show("Kết nối thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối thất bại.\nLỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_ketnoi_Click(object sender, EventArgs e)
        {
            string connString = GetConnectionString();

            // Kiểm tra kết nối trước khi lưu cho chắc chắn
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                }
                Properties.Settings.Default.dataQLKSConnectionString1 = connString;
                Properties.Settings.Default.Save(); 

                MessageBox.Show("Lưu cấu hình thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dangnhap dn = new Dangnhap();
                this.Hide();
                dn.ShowDialog();
                this.Close();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lưu vì kết nối không hợp lệ.\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetConnectionString()
        {
            // Sử dụng Builder để nối chuỗi an toàn
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = txt_tensever.Text.Trim();   // Tên Server
            builder.InitialCatalog = txt_tendb.Text.Trim(); // Tên Database
            builder.UserID = txt_tk.Text.Trim();     // Tài khoản
            builder.Password = txt_mk.Text.Trim();   // Mật khẩu
            builder.PersistSecurityInfo = true; // Cho phép lưu thông tin bảo mật

            return builder.ConnectionString;
        }
        private void QlThietBi_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            try
            {
                // Lấy chuỗi kết nối hiện tại trong Settings
                string currentConn = Properties.Settings.Default.dataQLKSConnectionString1;

                if (!string.IsNullOrEmpty(currentConn))
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(currentConn);
                    txt_tensever.Text = builder.DataSource;
                    txt_tendb.Text = builder.InitialCatalog;
                    txt_tk.Text = builder.UserID;
                    txt_mk.Focus();
                }
            }
            catch { /* Bỏ qua nếu chuỗi cũ bị lỗi */ }
        }

    }
    }


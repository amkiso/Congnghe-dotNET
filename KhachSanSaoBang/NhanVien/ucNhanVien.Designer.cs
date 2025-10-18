namespace KhachSanSaoBang.NhanVien
{
    partial class ucNhanVien
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_ThemNV = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.r = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbo_MaPB = new System.Windows.Forms.ComboBox();
            this.cbo_NamBD = new System.Windows.Forms.ComboBox();
            this.txt_Luong = new System.Windows.Forms.TextBox();
            this.txt_QueQuan = new System.Windows.Forms.TextBox();
            this.lbl_MaPhong = new System.Windows.Forms.Label();
            this.lbl_Luong = new System.Windows.Forms.Label();
            this.lbl_NamBD = new System.Windows.Forms.Label();
            this.lbl_QueQuan = new System.Windows.Forms.Label();
            this.cbo_NgaySinh = new System.Windows.Forms.ComboBox();
            this.cbo_GioiTinh = new System.Windows.Forms.ComboBox();
            this.lbl_GioiTinh = new System.Windows.Forms.Label();
            this.lbl_NgaySinh = new System.Windows.Forms.Label();
            this.txt_HoTen = new System.Windows.Forms.TextBox();
            this.lbl_HoTen = new System.Windows.Forms.Label();
            this.txt_MSNV = new System.Windows.Forms.TextBox();
            this.lbl_MSNV = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ThemNV
            // 
            this.btn_ThemNV.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_ThemNV.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_ThemNV.ForeColor = System.Drawing.Color.White;
            this.btn_ThemNV.Image = global::KhachSanSaoBang.Properties.Resources.user_add;
            this.btn_ThemNV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ThemNV.Location = new System.Drawing.Point(107, 91);
            this.btn_ThemNV.Margin = new System.Windows.Forms.Padding(5);
            this.btn_ThemNV.Name = "btn_ThemNV";
            this.btn_ThemNV.Size = new System.Drawing.Size(240, 71);
            this.btn_ThemNV.TabIndex = 0;
            this.btn_ThemNV.Text = "Thêm nhân viên";
            this.btn_ThemNV.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(58, 594);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(1578, 390);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh Sách Nhân Viên";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Location = new System.Drawing.Point(84, 641);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1502, 280);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // STT
            // 
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 6;
            this.STT.Name = "STT";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Mã Nhân Viên";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Tên Nhân Viên";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Số CCCD";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Số ĐT";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = global::KhachSanSaoBang.Properties.Resources.user_delete;
            this.button1.Location = new System.Drawing.Point(474, 91);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(240, 71);
            this.button1.TabIndex = 2;
            this.button1.Text = "Xóa nhân viên";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = global::KhachSanSaoBang.Properties.Resources.file_edit;
            this.button2.Location = new System.Drawing.Point(830, 91);
            this.button2.Margin = new System.Windows.Forms.Padding(5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(240, 71);
            this.button2.TabIndex = 3;
            this.button2.Text = "Chỉnh sửa nhân viên";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = global::KhachSanSaoBang.Properties.Resources.gui_cancel;
            this.button3.Location = new System.Drawing.Point(1171, 91);
            this.button3.Margin = new System.Windows.Forms.Padding(5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(240, 71);
            this.button3.TabIndex = 4;
            this.button3.Text = "Vô hiệu hóa tài khoản";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Silver;
            this.groupBox2.Controls.Add(this.cbo_MaPB);
            this.groupBox2.Controls.Add(this.cbo_NamBD);
            this.groupBox2.Controls.Add(this.txt_Luong);
            this.groupBox2.Controls.Add(this.txt_QueQuan);
            this.groupBox2.Controls.Add(this.lbl_MaPhong);
            this.groupBox2.Controls.Add(this.lbl_Luong);
            this.groupBox2.Controls.Add(this.lbl_NamBD);
            this.groupBox2.Controls.Add(this.lbl_QueQuan);
            this.groupBox2.Controls.Add(this.cbo_NgaySinh);
            this.groupBox2.Controls.Add(this.cbo_GioiTinh);
            this.groupBox2.Controls.Add(this.lbl_GioiTinh);
            this.groupBox2.Controls.Add(this.lbl_NgaySinh);
            this.groupBox2.Controls.Add(this.txt_HoTen);
            this.groupBox2.Controls.Add(this.lbl_HoTen);
            this.groupBox2.Controls.Add(this.txt_MSNV);
            this.groupBox2.Controls.Add(this.lbl_MSNV);
            this.groupBox2.Location = new System.Drawing.Point(107, 232);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1479, 288);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông Tin Nhân Viên";
            // 
            // cbo_MaPB
            // 
            this.cbo_MaPB.FormattingEnabled = true;
            this.cbo_MaPB.Location = new System.Drawing.Point(868, 219);
            this.cbo_MaPB.Name = "cbo_MaPB";
            this.cbo_MaPB.Size = new System.Drawing.Size(282, 30);
            this.cbo_MaPB.TabIndex = 15;
            // 
            // cbo_NamBD
            // 
            this.cbo_NamBD.FormattingEnabled = true;
            this.cbo_NamBD.Location = new System.Drawing.Point(868, 104);
            this.cbo_NamBD.Name = "cbo_NamBD";
            this.cbo_NamBD.Size = new System.Drawing.Size(282, 30);
            this.cbo_NamBD.TabIndex = 14;
            // 
            // txt_Luong
            // 
            this.txt_Luong.BackColor = System.Drawing.Color.White;
            this.txt_Luong.Location = new System.Drawing.Point(868, 159);
            this.txt_Luong.Multiline = true;
            this.txt_Luong.Name = "txt_Luong";
            this.txt_Luong.Size = new System.Drawing.Size(282, 37);
            this.txt_Luong.TabIndex = 13;
            // 
            // txt_QueQuan
            // 
            this.txt_QueQuan.BackColor = System.Drawing.Color.White;
            this.txt_QueQuan.Location = new System.Drawing.Point(868, 42);
            this.txt_QueQuan.Multiline = true;
            this.txt_QueQuan.Name = "txt_QueQuan";
            this.txt_QueQuan.Size = new System.Drawing.Size(282, 37);
            this.txt_QueQuan.TabIndex = 12;
            // 
            // lbl_MaPhong
            // 
            this.lbl_MaPhong.AutoSize = true;
            this.lbl_MaPhong.Location = new System.Drawing.Point(638, 219);
            this.lbl_MaPhong.Name = "lbl_MaPhong";
            this.lbl_MaPhong.Size = new System.Drawing.Size(135, 24);
            this.lbl_MaPhong.TabIndex = 11;
            this.lbl_MaPhong.Text = "Mã Phòng Ban";
            // 
            // lbl_Luong
            // 
            this.lbl_Luong.AutoSize = true;
            this.lbl_Luong.Location = new System.Drawing.Point(638, 159);
            this.lbl_Luong.Name = "lbl_Luong";
            this.lbl_Luong.Size = new System.Drawing.Size(64, 24);
            this.lbl_Luong.TabIndex = 10;
            this.lbl_Luong.Text = "Lương";
            // 
            // lbl_NamBD
            // 
            this.lbl_NamBD.AutoSize = true;
            this.lbl_NamBD.Location = new System.Drawing.Point(638, 110);
            this.lbl_NamBD.Name = "lbl_NamBD";
            this.lbl_NamBD.Size = new System.Drawing.Size(204, 24);
            this.lbl_NamBD.TabIndex = 9;
            this.lbl_NamBD.Text = "Năm Bắt Đầu Làm Việc";
            // 
            // lbl_QueQuan
            // 
            this.lbl_QueQuan.AutoSize = true;
            this.lbl_QueQuan.Location = new System.Drawing.Point(638, 52);
            this.lbl_QueQuan.Name = "lbl_QueQuan";
            this.lbl_QueQuan.Size = new System.Drawing.Size(99, 24);
            this.lbl_QueQuan.TabIndex = 8;
            this.lbl_QueQuan.Text = "Quê Quán";
            // 
            // cbo_NgaySinh
            // 
            this.cbo_NgaySinh.FormattingEnabled = true;
            this.cbo_NgaySinh.Location = new System.Drawing.Point(197, 159);
            this.cbo_NgaySinh.Name = "cbo_NgaySinh";
            this.cbo_NgaySinh.Size = new System.Drawing.Size(282, 30);
            this.cbo_NgaySinh.TabIndex = 7;
            // 
            // cbo_GioiTinh
            // 
            this.cbo_GioiTinh.FormattingEnabled = true;
            this.cbo_GioiTinh.Location = new System.Drawing.Point(198, 219);
            this.cbo_GioiTinh.Name = "cbo_GioiTinh";
            this.cbo_GioiTinh.Size = new System.Drawing.Size(281, 30);
            this.cbo_GioiTinh.TabIndex = 6;
            // 
            // lbl_GioiTinh
            // 
            this.lbl_GioiTinh.AutoSize = true;
            this.lbl_GioiTinh.Location = new System.Drawing.Point(38, 225);
            this.lbl_GioiTinh.Name = "lbl_GioiTinh";
            this.lbl_GioiTinh.Size = new System.Drawing.Size(86, 24);
            this.lbl_GioiTinh.TabIndex = 5;
            this.lbl_GioiTinh.Text = "Giới Tính";
            // 
            // lbl_NgaySinh
            // 
            this.lbl_NgaySinh.AutoSize = true;
            this.lbl_NgaySinh.Location = new System.Drawing.Point(38, 165);
            this.lbl_NgaySinh.Name = "lbl_NgaySinh";
            this.lbl_NgaySinh.Size = new System.Drawing.Size(97, 24);
            this.lbl_NgaySinh.TabIndex = 4;
            this.lbl_NgaySinh.Text = "Ngày Sinh";
            // 
            // txt_HoTen
            // 
            this.txt_HoTen.BackColor = System.Drawing.Color.White;
            this.txt_HoTen.Location = new System.Drawing.Point(197, 98);
            this.txt_HoTen.Multiline = true;
            this.txt_HoTen.Name = "txt_HoTen";
            this.txt_HoTen.Size = new System.Drawing.Size(282, 36);
            this.txt_HoTen.TabIndex = 3;
            // 
            // lbl_HoTen
            // 
            this.lbl_HoTen.AutoSize = true;
            this.lbl_HoTen.Location = new System.Drawing.Point(38, 110);
            this.lbl_HoTen.Name = "lbl_HoTen";
            this.lbl_HoTen.Size = new System.Drawing.Size(74, 24);
            this.lbl_HoTen.TabIndex = 2;
            this.lbl_HoTen.Text = "Họ Tên";
            // 
            // txt_MSNV
            // 
            this.txt_MSNV.BackColor = System.Drawing.Color.White;
            this.txt_MSNV.Location = new System.Drawing.Point(197, 39);
            this.txt_MSNV.Multiline = true;
            this.txt_MSNV.Name = "txt_MSNV";
            this.txt_MSNV.Size = new System.Drawing.Size(282, 37);
            this.txt_MSNV.TabIndex = 1;
            // 
            // lbl_MSNV
            // 
            this.lbl_MSNV.AutoSize = true;
            this.lbl_MSNV.Location = new System.Drawing.Point(38, 55);
            this.lbl_MSNV.Name = "lbl_MSNV";
            this.lbl_MSNV.Size = new System.Drawing.Size(131, 24);
            this.lbl_MSNV.TabIndex = 0;
            this.lbl_MSNV.Text = "Mã Nhân Viên";
            // 
            // ucNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_ThemNV);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ucNhanVien";
            this.Size = new System.Drawing.Size(2398, 1004);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ThemNV;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.ComponentModel.BackgroundWorker r;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_MSNV;
        private System.Windows.Forms.Label lbl_MSNV;
        private System.Windows.Forms.TextBox txt_HoTen;
        private System.Windows.Forms.Label lbl_HoTen;
        private System.Windows.Forms.ComboBox cbo_GioiTinh;
        private System.Windows.Forms.Label lbl_GioiTinh;
        private System.Windows.Forms.Label lbl_NgaySinh;
        private System.Windows.Forms.Label lbl_QueQuan;
        private System.Windows.Forms.ComboBox cbo_MaPB;
        private System.Windows.Forms.ComboBox cbo_NamBD;
        private System.Windows.Forms.TextBox txt_Luong;
        private System.Windows.Forms.TextBox txt_QueQuan;
        private System.Windows.Forms.Label lbl_MaPhong;
        private System.Windows.Forms.Label lbl_Luong;
        private System.Windows.Forms.Label lbl_NamBD;
        private System.Windows.Forms.ComboBox cbo_NgaySinh;
    }
}

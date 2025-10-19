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
            this.components = new System.ComponentModel.Container();
            this.btn_ThemNV = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ma_nv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hotenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngaysinhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diachiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sdtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tai_khoan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matkhauDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machucvuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gioitinhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quequanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nambdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.luongDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblNhanVienBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataQLKSDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataQLKSDataSet = new KhachSanSaoBang.dataQLKSDataSet();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.btn_XoaNV = new System.Windows.Forms.Button();
            this.btn_SuaNV = new System.Windows.Forms.Button();
            this.btn_VoHieuHoaTK = new System.Windows.Forms.Button();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.r = new System.ComponentModel.BackgroundWorker();
            this.tblNhanVienTableAdapter = new KhachSanSaoBang.dataQLKSDataSetTableAdapters.tblNhanVienTableAdapter();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblNhanVienBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataQLKSDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataQLKSDataSet)).BeginInit();
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
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(5, 218);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(1359, 390);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh Sách Nhân Viên";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ma_nv,
            this.hotenDataGridViewTextBoxColumn,
            this.ngaysinhDataGridViewTextBoxColumn,
            this.diachiDataGridViewTextBoxColumn,
            this.sdtDataGridViewTextBoxColumn,
            this.tai_khoan,
            this.matkhauDataGridViewTextBoxColumn,
            this.machucvuDataGridViewTextBoxColumn,
            this.gioitinhDataGridViewTextBoxColumn,
            this.quequanDataGridViewTextBoxColumn,
            this.nambdDataGridViewTextBoxColumn,
            this.luongDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.tblNhanVienBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(10, 53);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1349, 285);
            this.dataGridView1.TabIndex = 0;
            // 
            // ma_nv
            // 
            this.ma_nv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ma_nv.DataPropertyName = "ma_nv";
            this.ma_nv.HeaderText = "Mã NV";
            this.ma_nv.MinimumWidth = 6;
            this.ma_nv.Name = "ma_nv";
            this.ma_nv.ReadOnly = true;
            this.ma_nv.Width = 114;
            // 
            // hotenDataGridViewTextBoxColumn
            // 
            this.hotenDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.hotenDataGridViewTextBoxColumn.DataPropertyName = "ho_ten";
            this.hotenDataGridViewTextBoxColumn.HeaderText = "Họ Tên";
            this.hotenDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.hotenDataGridViewTextBoxColumn.Name = "hotenDataGridViewTextBoxColumn";
            this.hotenDataGridViewTextBoxColumn.ReadOnly = true;
            this.hotenDataGridViewTextBoxColumn.Width = 122;
            // 
            // ngaysinhDataGridViewTextBoxColumn
            // 
            this.ngaysinhDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ngaysinhDataGridViewTextBoxColumn.DataPropertyName = "ngay_sinh";
            this.ngaysinhDataGridViewTextBoxColumn.HeaderText = "Ngày Sinh";
            this.ngaysinhDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.ngaysinhDataGridViewTextBoxColumn.Name = "ngaysinhDataGridViewTextBoxColumn";
            this.ngaysinhDataGridViewTextBoxColumn.ReadOnly = true;
            this.ngaysinhDataGridViewTextBoxColumn.Width = 152;
            // 
            // diachiDataGridViewTextBoxColumn
            // 
            this.diachiDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.diachiDataGridViewTextBoxColumn.DataPropertyName = "dia_chi";
            this.diachiDataGridViewTextBoxColumn.HeaderText = "Địa Chỉ";
            this.diachiDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.diachiDataGridViewTextBoxColumn.Name = "diachiDataGridViewTextBoxColumn";
            this.diachiDataGridViewTextBoxColumn.ReadOnly = true;
            this.diachiDataGridViewTextBoxColumn.Width = 120;
            // 
            // sdtDataGridViewTextBoxColumn
            // 
            this.sdtDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sdtDataGridViewTextBoxColumn.DataPropertyName = "sdt";
            this.sdtDataGridViewTextBoxColumn.HeaderText = "sdt";
            this.sdtDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.sdtDataGridViewTextBoxColumn.Name = "sdtDataGridViewTextBoxColumn";
            this.sdtDataGridViewTextBoxColumn.ReadOnly = true;
            this.sdtDataGridViewTextBoxColumn.Width = 74;
            // 
            // tai_khoan
            // 
            this.tai_khoan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.tai_khoan.DataPropertyName = "tai_khoan";
            this.tai_khoan.HeaderText = "tai_khoan";
            this.tai_khoan.MinimumWidth = 6;
            this.tai_khoan.Name = "tai_khoan";
            this.tai_khoan.ReadOnly = true;
            this.tai_khoan.Visible = false;
            this.tai_khoan.Width = 125;
            // 
            // matkhauDataGridViewTextBoxColumn
            // 
            this.matkhauDataGridViewTextBoxColumn.DataPropertyName = "mat_khau";
            this.matkhauDataGridViewTextBoxColumn.HeaderText = "mat_khau";
            this.matkhauDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.matkhauDataGridViewTextBoxColumn.Name = "matkhauDataGridViewTextBoxColumn";
            this.matkhauDataGridViewTextBoxColumn.Visible = false;
            // 
            // machucvuDataGridViewTextBoxColumn
            // 
            this.machucvuDataGridViewTextBoxColumn.DataPropertyName = "ma_chuc_vu";
            this.machucvuDataGridViewTextBoxColumn.HeaderText = "ma_chuc_vu";
            this.machucvuDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.machucvuDataGridViewTextBoxColumn.Name = "machucvuDataGridViewTextBoxColumn";
            this.machucvuDataGridViewTextBoxColumn.Visible = false;
            // 
            // gioitinhDataGridViewTextBoxColumn
            // 
            this.gioitinhDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.gioitinhDataGridViewTextBoxColumn.DataPropertyName = "gioi_tinh";
            this.gioitinhDataGridViewTextBoxColumn.HeaderText = "Giới Tính";
            this.gioitinhDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.gioitinhDataGridViewTextBoxColumn.Name = "gioitinhDataGridViewTextBoxColumn";
            this.gioitinhDataGridViewTextBoxColumn.ReadOnly = true;
            this.gioitinhDataGridViewTextBoxColumn.Width = 140;
            // 
            // quequanDataGridViewTextBoxColumn
            // 
            this.quequanDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.quequanDataGridViewTextBoxColumn.DataPropertyName = "que_quan";
            this.quequanDataGridViewTextBoxColumn.HeaderText = "Quê Quán";
            this.quequanDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.quequanDataGridViewTextBoxColumn.Name = "quequanDataGridViewTextBoxColumn";
            this.quequanDataGridViewTextBoxColumn.ReadOnly = true;
            this.quequanDataGridViewTextBoxColumn.Width = 152;
            // 
            // nambdDataGridViewTextBoxColumn
            // 
            this.nambdDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.nambdDataGridViewTextBoxColumn.DataPropertyName = "nam_bd";
            this.nambdDataGridViewTextBoxColumn.HeaderText = "Năm Vào Làm";
            this.nambdDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nambdDataGridViewTextBoxColumn.Name = "nambdDataGridViewTextBoxColumn";
            this.nambdDataGridViewTextBoxColumn.ReadOnly = true;
            this.nambdDataGridViewTextBoxColumn.Width = 193;
            // 
            // luongDataGridViewTextBoxColumn
            // 
            this.luongDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.luongDataGridViewTextBoxColumn.DataPropertyName = "luong";
            this.luongDataGridViewTextBoxColumn.HeaderText = "Lương";
            this.luongDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.luongDataGridViewTextBoxColumn.Name = "luongDataGridViewTextBoxColumn";
            this.luongDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tblNhanVienBindingSource
            // 
            this.tblNhanVienBindingSource.DataMember = "tblNhanVien";
            this.tblNhanVienBindingSource.DataSource = this.dataQLKSDataSetBindingSource;
            // 
            // dataQLKSDataSetBindingSource
            // 
            this.dataQLKSDataSetBindingSource.DataSource = this.dataQLKSDataSet;
            this.dataQLKSDataSetBindingSource.Position = 0;
            // 
            // dataQLKSDataSet
            // 
            this.dataQLKSDataSet.DataSetName = "dataQLKSDataSet";
            this.dataQLKSDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btn_XoaNV
            // 
            this.btn_XoaNV.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_XoaNV.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_XoaNV.ForeColor = System.Drawing.Color.White;
            this.btn_XoaNV.Image = global::KhachSanSaoBang.Properties.Resources.user_delete;
            this.btn_XoaNV.Location = new System.Drawing.Point(474, 91);
            this.btn_XoaNV.Margin = new System.Windows.Forms.Padding(5);
            this.btn_XoaNV.Name = "btn_XoaNV";
            this.btn_XoaNV.Size = new System.Drawing.Size(240, 71);
            this.btn_XoaNV.TabIndex = 2;
            this.btn_XoaNV.Text = "Xóa nhân viên";
            this.btn_XoaNV.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_XoaNV.UseVisualStyleBackColor = false;
            // 
            // btn_SuaNV
            // 
            this.btn_SuaNV.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_SuaNV.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_SuaNV.ForeColor = System.Drawing.Color.White;
            this.btn_SuaNV.Image = global::KhachSanSaoBang.Properties.Resources.file_edit;
            this.btn_SuaNV.Location = new System.Drawing.Point(830, 91);
            this.btn_SuaNV.Margin = new System.Windows.Forms.Padding(5);
            this.btn_SuaNV.Name = "btn_SuaNV";
            this.btn_SuaNV.Size = new System.Drawing.Size(240, 71);
            this.btn_SuaNV.TabIndex = 3;
            this.btn_SuaNV.Text = "Chỉnh sửa nhân viên";
            this.btn_SuaNV.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_SuaNV.UseVisualStyleBackColor = false;
            // 
            // btn_VoHieuHoaTK
            // 
            this.btn_VoHieuHoaTK.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_VoHieuHoaTK.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_VoHieuHoaTK.ForeColor = System.Drawing.Color.White;
            this.btn_VoHieuHoaTK.Image = global::KhachSanSaoBang.Properties.Resources.gui_cancel;
            this.btn_VoHieuHoaTK.Location = new System.Drawing.Point(1171, 91);
            this.btn_VoHieuHoaTK.Margin = new System.Windows.Forms.Padding(5);
            this.btn_VoHieuHoaTK.Name = "btn_VoHieuHoaTK";
            this.btn_VoHieuHoaTK.Size = new System.Drawing.Size(240, 71);
            this.btn_VoHieuHoaTK.TabIndex = 4;
            this.btn_VoHieuHoaTK.Text = "Vô hiệu hóa tài khoản";
            this.btn_VoHieuHoaTK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_VoHieuHoaTK.UseVisualStyleBackColor = false;
            // 
            // tblNhanVienTableAdapter
            // 
            this.tblNhanVienTableAdapter.ClearBeforeFill = true;
            // 
            // ucNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_VoHieuHoaTK);
            this.Controls.Add(this.btn_SuaNV);
            this.Controls.Add(this.btn_XoaNV);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_ThemNV);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ucNhanVien";
            this.Size = new System.Drawing.Size(2398, 1004);
            this.Load += new System.EventHandler(this.UcNhanVien_Click);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblNhanVienBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataQLKSDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataQLKSDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ThemNV;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Button btn_XoaNV;
        private System.Windows.Forms.Button btn_SuaNV;
        private System.Windows.Forms.Button btn_VoHieuHoaTK;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.ComponentModel.BackgroundWorker r;
        private dataQLKSDataSet dataQLKSDataSet;
        private dataQLKSDataSetTableAdapters.tblNhanVienTableAdapter tblNhanVienTableAdapter;
        private System.Windows.Forms.BindingSource tblNhanVienBindingSource;
        private System.Windows.Forms.BindingSource dataQLKSDataSetBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn ma_nv;
        private System.Windows.Forms.DataGridViewTextBoxColumn hotenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngaysinhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diachiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sdtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tai_khoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn matkhauDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn machucvuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gioitinhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quequanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nambdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn luongDataGridViewTextBoxColumn;
    }
}

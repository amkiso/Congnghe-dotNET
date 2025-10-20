namespace KhachSanSaoBang.ThongKe
{
    partial class ucHoaDon
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
            this.tblChucVuTableAdapter1 = new KhachSanSaoBang.dataQLKSDataSetTableAdapters.tblChucVuTableAdapter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_DSHHD = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dataQLKSDataSet = new KhachSanSaoBang.dataQLKSDataSet();
            this.tblHoaDonBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblHoaDonTableAdapter = new KhachSanSaoBang.dataQLKSDataSetTableAdapters.tblHoaDonTableAdapter();
            this.tblHoaDonBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btn_print = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DSHHD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataQLKSDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblHoaDonBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblHoaDonBindingSource1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblChucVuTableAdapter1
            // 
            this.tblChucVuTableAdapter1.ClearBeforeFill = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_DSHHD);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox1.Location = new System.Drawing.Point(90, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1488, 398);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh Sách Hóa Đơn";
            // 
            // dgv_DSHHD
            // 
            this.dgv_DSHHD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DSHHD.Location = new System.Drawing.Point(0, 47);
            this.dgv_DSHHD.MultiSelect = false;
            this.dgv_DSHHD.Name = "dgv_DSHHD";
            this.dgv_DSHHD.RowHeadersWidth = 51;
            this.dgv_DSHHD.RowTemplate.Height = 24;
            this.dgv_DSHHD.Size = new System.Drawing.Size(1418, 351);
            this.dgv_DSHHD.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(519, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "Khách Sạn Sao Băng";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(938, 64);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(331, 22);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // dataQLKSDataSet
            // 
            this.dataQLKSDataSet.DataSetName = "dataQLKSDataSet";
            this.dataQLKSDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblHoaDonBindingSource
            // 
            this.tblHoaDonBindingSource.DataMember = "tblHoaDon";
            this.tblHoaDonBindingSource.DataSource = this.dataQLKSDataSet;
            // 
            // tblHoaDonTableAdapter
            // 
            this.tblHoaDonTableAdapter.ClearBeforeFill = true;
            // 
            // tblHoaDonBindingSource1
            // 
            this.tblHoaDonBindingSource1.DataMember = "tblHoaDon";
            this.tblHoaDonBindingSource1.DataSource = this.dataQLKSDataSet;
            // 
            // btn_print
            // 
            this.btn_print.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_print.Location = new System.Drawing.Point(693, 518);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(124, 47);
            this.btn_print.TabIndex = 6;
            this.btn_print.Text = "in ";
            this.btn_print.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(0, 49);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(729, 345);
            this.reportViewer1.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.reportViewer1);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox2.Location = new System.Drawing.Point(90, 610);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(731, 400);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "In Hóa Đơn";
            // 
            // ucHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_print);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "ucHoaDon";
            this.Size = new System.Drawing.Size(1667, 1039);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DSHHD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataQLKSDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblHoaDonBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblHoaDonBindingSource1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private dataQLKSDataSetTableAdapters.tblChucVuTableAdapter tblChucVuTableAdapter1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_DSHHD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.BindingSource tblHoaDonBindingSource;
        private dataQLKSDataSet dataQLKSDataSet;
        private dataQLKSDataSetTableAdapters.tblHoaDonTableAdapter tblHoaDonTableAdapter;
        private System.Windows.Forms.BindingSource tblHoaDonBindingSource1;
        private System.Windows.Forms.Button btn_print;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

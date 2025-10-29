namespace KhachSanSaoBang.ThongKe.BaoCao
{
    partial class ucBaoCao
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chartDoanhThuThang = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chartDoanhThuNhanVien = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartDichVuPhoBien = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartDatPhongLoaiPhong = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.cboNam = new System.Windows.Forms.ComboBox();
            this.cboThang = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThuThang)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThuNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDichVuPhoBien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDatPhongLoaiPhong)).BeginInit();
            this.SuspendLayout();
            // 
            // chartDoanhThuThang
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDoanhThuThang.ChartAreas.Add(chartArea1);
            this.chartDoanhThuThang.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.ForeColor = System.Drawing.Color.DarkGoldenrod;
            legend1.Name = "Legend1";
            this.chartDoanhThuThang.Legends.Add(legend1);
            this.chartDoanhThuThang.Location = new System.Drawing.Point(3, 3);
            this.chartDoanhThuThang.Name = "chartDoanhThuThang";
            series1.BorderColor = System.Drawing.Color.Peru;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.DarkGoldenrod;
            series1.IsValueShownAsLabel = true;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartDoanhThuThang.Series.Add(series1);
            this.chartDoanhThuThang.Size = new System.Drawing.Size(618, 341);
            this.chartDoanhThuThang.TabIndex = 0;
            this.chartDoanhThuThang.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            title1.ForeColor = System.Drawing.Color.DarkGoldenrod;
            title1.Name = "Title1";
            title1.Text = "Biểu đồ Doanh thu theo Tháng";
            this.chartDoanhThuThang.Titles.Add(title1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.chartDoanhThuThang, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chartDoanhThuNhanVien, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chartDichVuPhoBien, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.chartDatPhongLoaiPhong, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(193, 175);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1249, 694);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // chartDoanhThuNhanVien
            // 
            chartArea2.Name = "ChartArea1";
            this.chartDoanhThuNhanVien.ChartAreas.Add(chartArea2);
            this.chartDoanhThuNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.ForeColor = System.Drawing.Color.DarkGoldenrod;
            legend2.Name = "Legend1";
            this.chartDoanhThuNhanVien.Legends.Add(legend2);
            this.chartDoanhThuNhanVien.Location = new System.Drawing.Point(627, 3);
            this.chartDoanhThuNhanVien.Name = "chartDoanhThuNhanVien";
            series2.BorderColor = System.Drawing.Color.Peru;
            series2.ChartArea = "ChartArea1";
            series2.Color = System.Drawing.Color.DarkGoldenrod;
            series2.IsValueShownAsLabel = true;
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartDoanhThuNhanVien.Series.Add(series2);
            this.chartDoanhThuNhanVien.Size = new System.Drawing.Size(619, 341);
            this.chartDoanhThuNhanVien.TabIndex = 1;
            this.chartDoanhThuNhanVien.Text = "chart1";
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            title2.ForeColor = System.Drawing.Color.DarkGoldenrod;
            title2.Name = "Title1";
            title2.Text = "Biểu đồ Doanh thu theo Nhân viên";
            this.chartDoanhThuNhanVien.Titles.Add(title2);
            // 
            // chartDichVuPhoBien
            // 
            chartArea3.Name = "ChartArea1";
            this.chartDichVuPhoBien.ChartAreas.Add(chartArea3);
            this.chartDichVuPhoBien.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.chartDichVuPhoBien.Legends.Add(legend3);
            this.chartDichVuPhoBien.Location = new System.Drawing.Point(3, 350);
            this.chartDichVuPhoBien.Name = "chartDichVuPhoBien";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartDichVuPhoBien.Series.Add(series3);
            this.chartDichVuPhoBien.Size = new System.Drawing.Size(618, 341);
            this.chartDichVuPhoBien.TabIndex = 2;
            this.chartDichVuPhoBien.Text = "chart2";
            // 
            // chartDatPhongLoaiPhong
            // 
            chartArea4.Name = "ChartArea1";
            this.chartDatPhongLoaiPhong.ChartAreas.Add(chartArea4);
            this.chartDatPhongLoaiPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend4.ForeColor = System.Drawing.Color.DarkGoldenrod;
            legend4.Name = "Legend1";
            this.chartDatPhongLoaiPhong.Legends.Add(legend4);
            this.chartDatPhongLoaiPhong.Location = new System.Drawing.Point(627, 350);
            this.chartDatPhongLoaiPhong.Name = "chartDatPhongLoaiPhong";
            series4.BorderColor = System.Drawing.Color.Peru;
            series4.ChartArea = "ChartArea1";
            series4.Color = System.Drawing.Color.DarkGoldenrod;
            series4.IsValueShownAsLabel = true;
            series4.IsVisibleInLegend = false;
            series4.IsXValueIndexed = true;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartDatPhongLoaiPhong.Series.Add(series4);
            this.chartDatPhongLoaiPhong.Size = new System.Drawing.Size(619, 341);
            this.chartDatPhongLoaiPhong.TabIndex = 3;
            this.chartDatPhongLoaiPhong.Text = "chart1";
            title3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            title3.ForeColor = System.Drawing.Color.DarkGoldenrod;
            title3.Name = "Title1";
            title3.Text = "Biểu đồ Số lượt đặt phòng theo Loại phòng";
            this.chartDatPhongLoaiPhong.Titles.Add(title3);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(196, 78);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(196, 54);
            this.btnLamMoi.TabIndex = 8;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // cboNam
            // 
            this.cboNam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboNam.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.cboNam.FormattingEnabled = true;
            this.cboNam.Location = new System.Drawing.Point(848, 92);
            this.cboNam.Name = "cboNam";
            this.cboNam.Size = new System.Drawing.Size(177, 34);
            this.cboNam.TabIndex = 9;
            // 
            // cboThang
            // 
            this.cboThang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboThang.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.cboThang.FormattingEnabled = true;
            this.cboThang.Location = new System.Drawing.Point(536, 89);
            this.cboThang.Name = "cboThang";
            this.cboThang.Size = new System.Drawing.Size(177, 34);
            this.cboThang.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label1.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label1.Location = new System.Drawing.Point(439, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 26);
            this.label1.TabIndex = 11;
            this.label1.Text = "Tháng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label2.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label2.Location = new System.Drawing.Point(759, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 26);
            this.label2.TabIndex = 12;
            this.label2.Text = "Năm";
            // 
            // ucBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboThang);
            this.Controls.Add(this.cboNam);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ucBaoCao";
            this.Size = new System.Drawing.Size(1500, 907);
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThuThang)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThuNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDichVuPhoBien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDatPhongLoaiPhong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThuThang;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDichVuPhoBien;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThuNhanVien;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDatPhongLoaiPhong;
        private System.Windows.Forms.ComboBox cboNam;
        private System.Windows.Forms.ComboBox cboThang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

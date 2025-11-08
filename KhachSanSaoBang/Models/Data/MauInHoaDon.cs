using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang.Models.Data
{
    public class MauInHoaDon
    {

        private ThongtinThanhToan invoiceData;
        private PrintDocument printDoc;
        private Image logoImage;
        private float tienKhachDua;
        private float tienThua;

        // Khổ giấy K80 (80mm = 3.15 inches)
        private const int PAPER_WIDTH = 302; // 80mm ≈ 302 pixels at 96 DPI
        private const int MARGIN = 10;
        private const int CONTENT_WIDTH = PAPER_WIDTH - (MARGIN * 2);

        private int calculatedHeight = 0; // Chiều cao tính toán được

        public MauInHoaDon(ThongtinThanhToan data, string logoPath, int tienKhachDua = 0)
        {
            this.invoiceData = data;
            this.tienKhachDua = tienKhachDua;
            this.tienThua = tienKhachDua - data.Tongtien;

            // Load logo
            try
            {
                if (!string.IsNullOrEmpty(logoPath) && System.IO.File.Exists(logoPath))
                {
                    logoImage = Image.FromFile(logoPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể load logo: {ex.Message}", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            InitializePrintDocument();
        }

        private void InitializePrintDocument()
        {
            printDoc = new PrintDocument();

            // Lần đầu tính toán chiều cao với PaperSize tạm
            PaperSize tempPaperSize = new PaperSize("K80", 302, 5000);
            printDoc.DefaultPageSettings.PaperSize = tempPaperSize;
            printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

            // Đăng ký event để tính chiều cao
            printDoc.PrintPage += PrintDoc_CalculateHeight;

            // Tính toán chiều cao bằng cách in thử vào memory
            using (Bitmap bmp = new Bitmap(PAPER_WIDTH, 5000))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                PrintPageEventArgs args = new PrintPageEventArgs(
                    g,
                    new Rectangle(0, 0, PAPER_WIDTH, 5000),
                    new Rectangle(0, 0, PAPER_WIDTH, 5000),
                    printDoc.DefaultPageSettings
                );
                PrintDoc_CalculateHeight(printDoc, args);
            }

            // Hủy event tính toán và đăng ký event in thật
            printDoc.PrintPage -= PrintDoc_CalculateHeight;

            // Tạo PaperSize với chiều cao chính xác (thêm margin cho an toàn)
            int finalHeight = calculatedHeight + (MARGIN * 2) + 20; // Thêm 20px buffer
            PaperSize finalPaperSize = new PaperSize("K80", PAPER_WIDTH, finalHeight);
            printDoc.DefaultPageSettings.PaperSize = finalPaperSize;

            // Đăng ký event in thật
            printDoc.PrintPage += PrintDoc_PrintPage;
        }

        private void PrintDoc_CalculateHeight(object sender, PrintPageEventArgs e)
        {
            calculatedHeight = (int)RenderInvoice(e.Graphics, true);
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            RenderInvoice(e.Graphics, false);
        }

        // Hàm render chính - có thể dùng để tính toán hoặc in thật
        private float RenderInvoice(Graphics g, bool calculateOnly)
        {
            Font fontHeader = new Font("Arial", 11, FontStyle.Bold);
            Font fontTitle = new Font("Arial", 12, FontStyle.Bold);
            Font fontNormal = new Font("Arial", 8, FontStyle.Regular);
            Font fontBold = new Font("Arial", 8, FontStyle.Bold);
            Font fontSmall = new Font("Arial", 7, FontStyle.Regular);

            Brush brush = Brushes.Black;
            float yPos = MARGIN;

            // === HEADER: Logo và Tên Khách Sạn ===
            if (logoImage != null)
            {
                int logoSize = 60;
                int logoX = (PAPER_WIDTH - logoSize) / 2;
                if (!calculateOnly)
                    g.DrawImage(logoImage, logoX, yPos, logoSize, logoSize);
                yPos += logoSize + 5;
            }

            // Tên khách sạn
            string hotelName = "KHÁCH SẠN SAO BĂNG";
            SizeF hotelNameSize = g.MeasureString(hotelName, fontHeader);
            float hotelNameX = (PAPER_WIDTH - hotelNameSize.Width) / 2;
            if (!calculateOnly)
                g.DrawString(hotelName, fontHeader, brush, hotelNameX, yPos);
            yPos += hotelNameSize.Height + 2;

            // Đường kẻ
            if (!calculateOnly)
                g.DrawLine(Pens.Black, MARGIN, yPos, PAPER_WIDTH - MARGIN, yPos);
            yPos += 8;

            // === TIÊU ĐỀ HÓA ĐƠN ===
            string title = null;
            if(tienKhachDua > 0)
            {
                title = "HÓA ĐƠN THANH TOÁN";
            }
            else
            {
                title = "HÓA ĐƠN TẠM TÍNH";
            }
            SizeF titleSize = g.MeasureString(title, fontTitle);
            float titleX = (PAPER_WIDTH - titleSize.Width) / 2;
            if (!calculateOnly)
                g.DrawString(title, fontTitle, brush, titleX, yPos);
            yPos += titleSize.Height + 10;

            // === THÔNG TIN CHUNG ===
            yPos = DrawTextWithLabel(g, "Mã HĐ:", invoiceData.Mahd.ToString(), fontNormal, fontBold, brush, MARGIN, yPos, calculateOnly);
            yPos = DrawText(g, $"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm}", fontNormal, brush, MARGIN, yPos, calculateOnly);
            yPos = DrawText(g, $"Khách hàng: {invoiceData.Tenkh}", fontNormal, brush, MARGIN, yPos, calculateOnly);
            yPos = DrawText(g, $"Mã KH: {invoiceData.Makh}", fontNormal, brush, MARGIN, yPos, calculateOnly);
            yPos = DrawTextWithLabel(g, "Số phòng:", invoiceData.Sophong, fontNormal, fontBold, brush, MARGIN, yPos, calculateOnly);
            yPos = DrawText(g, $"Số khách: {invoiceData.sokhach}", fontNormal, brush, MARGIN, yPos, calculateOnly);

            yPos += 5;
            if (!calculateOnly)
                g.DrawLine(Pens.Black, MARGIN, yPos, PAPER_WIDTH - MARGIN, yPos);
            yPos += 8;

            // === THỜI GIAN LƯU TRÚ ===
            yPos = DrawText(g, "THÔNG TIN LƯU TRÚ", fontBold, brush, MARGIN, yPos, calculateOnly);
            yPos = DrawText(g, $"Ngày vào: {invoiceData.Ngayvao:dd/MM/yyyy HH:mm}", fontNormal, brush, MARGIN, yPos, calculateOnly);
            yPos = DrawText(g, $"Ngày ra DK: {invoiceData.Ngaydukienra:dd/MM/yyyy HH:mm}", fontNormal, brush, MARGIN, yPos, calculateOnly);
            yPos = DrawText(g, $"Ngày ra TT: {invoiceData.Ngayra:dd/MM/yyyy HH:mm}", fontNormal, brush, MARGIN, yPos, calculateOnly);
            yPos = DrawText(g, $"Số ngày thuê: {invoiceData.Songaythue} ngày", fontNormal, brush, MARGIN, yPos, calculateOnly);

            if (invoiceData.Songayphuthu > 0)
            {
                yPos = DrawText(g, $"Số ngày phụ thu: {invoiceData.Songayphuthu} ngày", fontNormal, brush, MARGIN, yPos, calculateOnly);
            }

            yPos += 5;
            if (!calculateOnly)
                g.DrawLine(Pens.Black, MARGIN, yPos, PAPER_WIDTH - MARGIN, yPos);
            yPos += 8;

            // === CHI TIẾT PHÒNG ===
            yPos = DrawText(g, "CHI TIẾT THANH TOÁN", fontBold, brush, MARGIN, yPos, calculateOnly);
            yPos += 3;

            // Tiền phòng
            string roomLine = $"Phòng ({invoiceData.Songaythue} ngày x {FormatMoney(invoiceData.Giaphong)})";
            int roomTotal = invoiceData.Giaphong * invoiceData.Songaythue;
            yPos = DrawLineItem(g, roomLine, roomTotal, fontNormal, brush, MARGIN, yPos, calculateOnly);

            // Phụ thu (nếu có)
            if (invoiceData.Songayphuthu > 0)
            {
                string phuThuLine = $"Phụ thu ({invoiceData.Songayphuthu} ngày x {invoiceData.Tilephuthu}%)";
                yPos = DrawLineItem(g, phuThuLine, invoiceData.Thanhtienphuthu, fontNormal, brush, MARGIN, yPos, calculateOnly);
            }

            // === DỊCH VỤ ===
            if (invoiceData.Dichvusudung != null && invoiceData.Dichvusudung.Count > 0)
            {
                yPos += 5;
                yPos = DrawText(g, "DỊCH VỤ SỬ DỤNG", fontBold, brush, MARGIN, yPos, calculateOnly);
                yPos += 3;

                foreach (var dv in invoiceData.Dichvusudung)
                {
                    string dvLine = $"{dv.Tendv} ({dv.Soluong} {dv.Donvi} x {FormatMoney((int)dv.Giaban)})";
                    int dvTotal = (int)(dv.Soluong * dv.Giaban);
                    yPos = DrawLineItem(g, dvLine, dvTotal, fontNormal, brush, MARGIN, yPos, calculateOnly);
                }
            }

            yPos += 5;
            if (!calculateOnly)
                g.DrawLine(Pens.Black, MARGIN, yPos, PAPER_WIDTH - MARGIN, yPos);
            yPos += 8;

            // === TỔNG CỘNG ===
            yPos = DrawLineItem(g, "Thành tiền:", invoiceData.Thanhtien, fontBold, brush, MARGIN, yPos, calculateOnly);

            if (invoiceData.Tienchietkhau > 0)
            {
                if(invoiceData.Tilechietkhau > 0)
                {

                    yPos = DrawLineItem(g, $"Khuyễn mãi ({invoiceData.Tilechietkhau}%):", "-"+(int)invoiceData.Tienchietkhau, fontNormal, brush, MARGIN, yPos, calculateOnly);
                }
                else
                {
                    yPos = DrawLineItem(g, "Khuyễn mãi:", "-"+(int)invoiceData.Tienchietkhau, fontNormal, brush, MARGIN, yPos, calculateOnly);
                }
            }

            yPos += 3;
            yPos = DrawLineItem(g, "TỔNG TIỀN:", invoiceData.Tongtien, fontTitle, brush, MARGIN, yPos, calculateOnly);

            // === TIỀN KHÁCH ĐƯA VÀ TIỀN THỪA ===
            if (tienKhachDua > 0)
            {
                yPos += 8;
                if (!calculateOnly)
                    g.DrawLine(Pens.Black, MARGIN, yPos, PAPER_WIDTH - MARGIN, yPos);
                yPos += 8;

                yPos = DrawLineItem(g, "Tiền khách đưa:", tienKhachDua, fontBold, brush, MARGIN, yPos, calculateOnly);
                yPos = DrawLineItem(g, "Tiền thừa:", tienThua, fontBold, brush, MARGIN, yPos, calculateOnly);
            }

            yPos += 15;
            if (!calculateOnly)
                g.DrawLine(Pens.Black, MARGIN, yPos, PAPER_WIDTH - MARGIN, yPos);
            yPos += 10;

            // === FOOTER ===
            string footer1 = "Cảm ơn quý khách!";
            string footer2 = "Hẹn gặp lại!";

            SizeF footer1Size = g.MeasureString(footer1, fontBold);
            float footer1X = (PAPER_WIDTH - footer1Size.Width) / 2;
            if (!calculateOnly)
                g.DrawString(footer1, fontBold, brush, footer1X, yPos);
            yPos += footer1Size.Height + 2;

            SizeF footer2Size = g.MeasureString(footer2, fontNormal);
            float footer2X = (PAPER_WIDTH - footer2Size.Width) / 2;
            if (!calculateOnly)
                g.DrawString(footer2, fontNormal, brush, footer2X, yPos);
            yPos += footer2Size.Height;

            // Cleanup
            fontHeader.Dispose();
            fontTitle.Dispose();
            fontNormal.Dispose();
            fontBold.Dispose();
            fontSmall.Dispose();

            return yPos;
        }

        private float DrawText(Graphics g, string text, Font font, Brush brush, float x, float y, bool calculateOnly)
        {
            if (!calculateOnly)
                g.DrawString(text, font, brush, x, y);
            SizeF size = g.MeasureString(text, font);
            return y + size.Height + 2;
        }

        private float DrawTextWithLabel(Graphics g, string label, string value, Font labelFont, Font valueFont, Brush brush, float x, float y, bool calculateOnly)
        {
            if (!calculateOnly)
            {
                // Vẽ label với font thường
                g.DrawString(label, labelFont, brush, x, y);
                SizeF labelSize = g.MeasureString(label, labelFont);

                // Vẽ value với font đậm, cách 1 khoảng nhỏ
                g.DrawString(" " + value, valueFont, brush, x + labelSize.Width, y);
            }

            // Tính tổng chiều cao
            SizeF labelSize2 = g.MeasureString(label, labelFont);
            SizeF valueSize = g.MeasureString(" " + value, valueFont);
            float totalHeight = Math.Max(labelSize2.Height, valueSize.Height);

            return y + totalHeight + 2;
        }

        private float DrawLineItem(Graphics g, string text, float amount, Font font, Brush brush, float x, float y, bool calculateOnly)
        {
            string amountStr = FormatMoney(amount);
            SizeF amountSize = g.MeasureString(amountStr, font);
            SizeF textSize = g.MeasureString(text, font);

            if (!calculateOnly)
            {
                // Vẽ text bên trái
                g.DrawString(text, font, brush, x, y);

                // Vẽ số tiền bên phải
                float amountX = PAPER_WIDTH - MARGIN - amountSize.Width;
                g.DrawString(amountStr, font, brush, amountX, y);
            }

            return y + Math.Max(textSize.Height, amountSize.Height) + 2;
        }
        private float DrawLineItem(Graphics g, string text, string amount, Font font, Brush brush, float x, float y, bool calculateOnly)
        {
            string amountStr = decimal.Parse(amount).ToString("N0", new System.Globalization.CultureInfo("vi-VN")) + "đ";
            SizeF amountSize = g.MeasureString(amountStr, font);
            SizeF textSize = g.MeasureString(text, font);

            if (!calculateOnly)
            {
                // Vẽ text bên trái
                g.DrawString(text, font, brush, x, y);

                // Vẽ số tiền bên phải
                float amountX = PAPER_WIDTH - MARGIN - amountSize.Width;
                g.DrawString(amountStr, font, brush, amountX, y);
            }

            return y + Math.Max(textSize.Height, amountSize.Height) + 2;
        }

        private string FormatMoney(float amount)
        {
            return amount.ToString("N0", new System.Globalization.CultureInfo("vi-VN")) + "đ";
        }

        // Phương thức in
        public void Print()
        {
            try
            {
                printDoc.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phương thức xem trước
        public void PrintPreview()
        {
            try
            {
                PrintPreviewDialog previewDialog = new PrintPreviewDialog
                {
                    Document = printDoc,
                    Width = 400,
                    Height = 600,
                    StartPosition = FormStartPosition.CenterScreen
                };
                previewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem trước: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phương thức hiển thị hộp thoại in
        public void ShowPrintDialog()
        {
            try
            {
                PrintDialog printDialog = new PrintDialog
                {
                    Document = printDoc
                };

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Dispose
        public void Dispose()
        {
            if (logoImage != null)
            {
                logoImage.Dispose();
            }
            if (printDoc != null)
            {
                printDoc.Dispose();
            }
        }
    }

}




using KhachSanSaoBang.Models.Data;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace KhachSanSaoBang
{
    public class MauPhieuNhanDatPhong
    {
        private PhieuNhanPhong bookingData;
        private PrintDocument printDoc;
        private Image logoImage;
        
        

        // Khổ giấy K80 (80mm = 3.15 inches)
        private const int PAPER_WIDTH = 302; // 80mm ≈ 302 pixels at 96 DPI
        private const int MARGIN = 10;
        private const int CONTENT_WIDTH = PAPER_WIDTH - (MARGIN * 2);

        private int calculatedHeight = 0; // Chiều cao tính toán được

        public MauPhieuNhanDatPhong(PhieuNhanPhong data, string logoPath )
        {
            this.bookingData = data;
            
            

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
            calculatedHeight = (int)RenderBookingSlip(e.Graphics, true);
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            RenderBookingSlip(e.Graphics, false);
        }

        // Hàm render chính - có thể dùng để tính toán hoặc in thật
        private float RenderBookingSlip(Graphics g, bool calculateOnly)
        {
            Font fontHeader = new Font("Arial", 11, FontStyle.Bold);
            Font fontTitle = new Font("Arial", 12, FontStyle.Bold);
            Font fontNormal = new Font("Arial", 8, FontStyle.Regular);
            Font fontBold = new Font("Arial", 8, FontStyle.Bold);
            Font fontSmall = new Font("Arial", 7, FontStyle.Regular);
            Font fontLarge = new Font("Arial", 10, FontStyle.Bold);

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

            // === TIÊU ĐỀ PHIẾU ===
            string title = "PHIẾU ĐẶT PHÒNG";
            SizeF titleSize = g.MeasureString(title, fontTitle);
            float titleX = (PAPER_WIDTH - titleSize.Width) / 2;
            if (!calculateOnly)
                g.DrawString(title, fontTitle, brush, titleX, yPos);
            yPos += titleSize.Height + 10;

            // === THÔNG TIN CHUNG ===
            yPos = DrawTextWithLabel(g, "Mã phiếu:", bookingData.Mapdp.ToString(), fontNormal, fontBold, brush, MARGIN, yPos, calculateOnly);
            yPos = DrawText(g, $"Ngày đặt: {bookingData.Ngaydat:dd/MM/yyyy HH:mm}", fontNormal, brush, MARGIN, yPos, calculateOnly);

            if (!string.IsNullOrEmpty(bookingData.Tenkh))
            {
                yPos = DrawText(g, $"Khách hàng: {bookingData.Tenkh}", fontNormal, brush, MARGIN, yPos, calculateOnly);
            }

            if (!string.IsNullOrEmpty(bookingData.Sdt))
            {
                yPos = DrawText(g, $"Số ĐT: {bookingData.Sdt}", fontNormal, brush, MARGIN, yPos, calculateOnly);
            }

            yPos += 5;
            if (!calculateOnly)
                g.DrawLine(Pens.Black, MARGIN, yPos, PAPER_WIDTH - MARGIN, yPos);
            yPos += 8;

            // === THÔNG TIN PHÒNG ===
            yPos = DrawText(g, "THÔNG TIN PHÒNG", fontBold, brush, MARGIN, yPos, calculateOnly);
            yPos += 3;

            if (!string.IsNullOrEmpty(bookingData.Sophong))
            {
                yPos = DrawTextWithLabel(g, "Số phòng:", bookingData.Sophong, fontNormal, fontLarge, brush, MARGIN, yPos, calculateOnly);
            }

            yPos = DrawTextWithLabel(g, "Loại phòng:", bookingData.Loaiphong, fontNormal, fontBold, brush, MARGIN, yPos, calculateOnly);

            yPos += 5;
            if (!calculateOnly)
                g.DrawLine(Pens.Black, MARGIN, yPos, PAPER_WIDTH - MARGIN, yPos);
            yPos += 8;

            // === THỜI GIAN LƯU TRÚ ===
            yPos = DrawText(g, "THỜI GIAN LƯU TRÚ", fontBold, brush, MARGIN, yPos, calculateOnly);
            yPos += 3;

            yPos = DrawText(g, $"Nhận phòng: {bookingData.Ngayvao:dd/MM/yyyy HH:mm}", fontNormal, brush, MARGIN, yPos, calculateOnly);
            yPos = DrawText(g, $"Trả phòng: {bookingData.Ngayra:dd/MM/yyyy HH:mm}", fontNormal, brush, MARGIN, yPos, calculateOnly);

            // Tính số ngày
            int soNgay = (bookingData.Ngayra.Date - bookingData.Ngayvao.Date).Days;
            if (soNgay > 0)
            {
                yPos = DrawTextWithLabel(g, "Số ngày:", $"{soNgay} ngày", fontNormal, fontBold, brush, MARGIN, yPos, calculateOnly);
            }

            yPos += 5;
            if (!calculateOnly)
                g.DrawLine(Pens.Black, MARGIN, yPos, PAPER_WIDTH - MARGIN, yPos);
            yPos += 8;

            // === TIỆN ÍCH PHÒNG ===
            if (!string.IsNullOrEmpty(bookingData.Tienich))
            {
                yPos = DrawText(g, "TIỆN ÍCH PHÒNG", fontBold, brush, MARGIN, yPos, calculateOnly);
                yPos += 3;

                // Tách tiện ích thành các dòng (giả sử phân tách bằng dấu phẩy hoặc chấm phẩy)
                string[] tienIchList = bookingData.Tienich.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string tienIch in tienIchList)
                {
                    yPos = DrawText(g, $"• {tienIch.Trim()}", fontSmall, brush, MARGIN + 5, yPos, calculateOnly);
                }

                yPos += 5;
                if (!calculateOnly)
                    g.DrawLine(Pens.Black, MARGIN, yPos, PAPER_WIDTH - MARGIN, yPos);
                yPos += 8;
            }

            // === GIÁ THUÊ ===
            yPos = DrawText(g, "GIÁ THUÊ", fontBold, brush, MARGIN, yPos, calculateOnly);
            yPos += 3;

            string giaThueStr = bookingData.Giathue;
            SizeF giaSize = g.MeasureString(giaThueStr, fontLarge);
            float giaX = (PAPER_WIDTH - giaSize.Width) / 2;
            if (!calculateOnly)
                g.DrawString(giaThueStr, fontLarge, brush, giaX, yPos);
            yPos += giaSize.Height + 3;

            string perNight = "/ ngày";
            SizeF perNightSize = g.MeasureString(perNight, fontSmall);
            float perNightX = (PAPER_WIDTH - perNightSize.Width) / 2;
            if (!calculateOnly)
                g.DrawString(perNight, fontSmall, brush, perNightX, yPos);
            yPos += perNightSize.Height + 10;

            if (!calculateOnly)
                g.DrawLine(Pens.Black, MARGIN, yPos, PAPER_WIDTH - MARGIN, yPos);
            yPos += 10;

            // === LƯU Ý ===
            yPos = DrawText(g, "LƯU Ý", fontBold, brush, MARGIN, yPos, calculateOnly);
            yPos += 2;
            yPos = DrawText(g, "• Vui lòng xuất trình CMND/CCCD", fontSmall, brush, MARGIN, yPos, calculateOnly);
            yPos = DrawText(g, "• Nhận phòng 24/24", fontSmall, brush, MARGIN, yPos, calculateOnly);
            yPos = DrawText(g, "• Trả phòng trễ không quá 5 ngày", fontSmall, brush, MARGIN, yPos, calculateOnly);
            yPos = DrawText(g, "• Liên hệ lễ tân khi cần hỗ trợ", fontSmall, brush, MARGIN, yPos, calculateOnly);

            yPos += 15;
            if (!calculateOnly)
                g.DrawLine(Pens.Black, MARGIN, yPos, PAPER_WIDTH - MARGIN, yPos);
            yPos += 10;

            // === FOOTER ===
            string footer1 = "Cảm ơn quý khách!";
            string footer2 = "Chúc quý khách có kỳ nghỉ vui vẻ!";

            SizeF footer1Size = g.MeasureString(footer1, fontBold);
            float footer1X = (PAPER_WIDTH - footer1Size.Width) / 2;
            if (!calculateOnly)
                g.DrawString(footer1, fontBold, brush, footer1X, yPos);
            yPos += footer1Size.Height + 2;

            SizeF footer2Size = g.MeasureString(footer2, fontSmall);
            float footer2X = (PAPER_WIDTH - footer2Size.Width) / 2;
            if (!calculateOnly)
                g.DrawString(footer2, fontSmall, brush, footer2X, yPos);
            yPos += footer2Size.Height;

            // Cleanup
            fontHeader.Dispose();
            fontTitle.Dispose();
            fontNormal.Dispose();
            fontBold.Dispose();
            fontSmall.Dispose();
            fontLarge.Dispose();

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
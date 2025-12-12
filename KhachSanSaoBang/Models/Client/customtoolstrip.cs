using System;
using System.Drawing;
using System.Windows.Forms;

namespace KhachSanSaoBang
{
    // Kế thừa từ ToolStrip có sẵn
    public class ModernToolStrip : ToolStrip
    {
        private Size _customImageSize = new Size(32, 32); // Mặc định size 32x32

        public ModernToolStrip()
        {
            // Cài đặt render để giao diện phẳng (bỏ gradient mặc định)
            this.Renderer = new ToolStripProfessionalRenderer(new ModernColorTable());

            // Cài đặt mặc định cho ToolStrip
            this.GripStyle = ToolStripGripStyle.Hidden; // Ẩn dấu chấm bi ở đầu
            this.Padding = new Padding(5);
            this.AutoSize = true;

            // Cập nhật size ảnh
            UpdateImageScaling();
        }

        // Tạo thuộc tính để chỉnh Size Icon trực tiếp trên giao diện Properties
        public Size IconSize
        {
            get { return _customImageSize; }
            set
            {
                _customImageSize = value;
                UpdateImageScaling();
                this.Invalidate(); // Vẽ lại
            }
        }

        private void UpdateImageScaling()
        {
            // Đây là thuộc tính quan trọng nhất để chỉnh cỡ ảnh
            this.ImageScalingSize = _customImageSize;
        }

        // Override sự kiện khi thêm item mới để tự động chỉnh Text/Image Relation
        protected override void OnItemAdded(ToolStripItemEventArgs e)
        {
            base.OnItemAdded(e);

            // Tự động cấu hình cho các button được thêm vào
            if (e.Item is ToolStripButton btn)
            {
                btn.AutoSize = true; // Tự co dãn theo nội dung
                btn.TextImageRelation = TextImageRelation.ImageAboveText; // Ảnh trên, chữ dưới (hoặc chỉnh thành ImageBeforeText)
                btn.TextAlign = ContentAlignment.BottomCenter;
                btn.ImageAlign = ContentAlignment.TopCenter;
                btn.Padding = new Padding(5); // Tạo khoảng cách thoáng hơn
            }
        }
    }

    // Class phụ để chỉnh màu sắc (Giao diện phẳng, bỏ màu xanh lam cũ)
    public class ModernColorTable : ProfessionalColorTable
    {
        // Màu nền chính của Toolstrip
        public override Color ToolStripGradientBegin => Color.WhiteSmoke;
        public override Color ToolStripGradientMiddle => Color.WhiteSmoke;
        public override Color ToolStripGradientEnd => Color.WhiteSmoke;

        // Màu viền
        public override Color ToolStripBorder => Color.LightGray;

        // Màu khi di chuột vào (Hover)
        public override Color ButtonSelectedHighlight => Color.LightBlue;
        public override Color ButtonSelectedBorder => Color.CornflowerBlue;

        // Màu khi nhấn nút (Pressed)
        public override Color ButtonPressedHighlight => Color.DeepSkyBlue;
        public override Color ButtonPressedBorder => Color.Blue;
    }
}
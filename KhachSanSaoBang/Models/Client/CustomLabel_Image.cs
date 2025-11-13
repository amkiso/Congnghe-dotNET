using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang
{
    public class CustomLabel_Image : Control
    {

        // Properties
        private Image _image = null;
        [Category("Appearance")]
        public Image Image
        {
            get => _image;
            set { _image = value; Invalidate(); }
        }

        private bool _autoSizeImage = true;
        [Category("Appearance")]
        [DefaultValue(true)]
        public bool AutoSizeImage
        {
            get => _autoSizeImage;
            set { _autoSizeImage = value; Invalidate(); }
        }

        private Size _imageSize = new Size(20, 20);
        [Category("Appearance")]
        public Size ImageSize
        {
            get => _imageSize;
            set { _imageSize = value; Invalidate(); }
        }

        private float _imageScaleFactor = 0.6f;
        [Category("Appearance")]
        [DefaultValue(0.6f)]
        [Description("Tỷ lệ icon so với chiều cao label khi AutoSizeImage = true (0.3 - 0.9)")]
        public float ImageScaleFactor
        {
            get => _imageScaleFactor;
            set
            {
                _imageScaleFactor = Math.Max(0.2f, Math.Min(0.9f, value));
                Invalidate();
            }
        }

        private ContentAlignment _imageAlign = ContentAlignment.MiddleLeft;
        [Category("Appearance")]
        public ContentAlignment ImageAlign
        {
            get => _imageAlign;
            set { _imageAlign = value; Invalidate(); }
        }

        private ContentAlignment _textAlign = ContentAlignment.MiddleLeft;
        [Category("Appearance")]
        public ContentAlignment TextAlign
        {
            get => _textAlign;
            set { _textAlign = value; Invalidate(); }
        }

        private int _imagePadding = 4;
        [Category("Layout")]
        public int ImagePadding
        {
            get => _imagePadding;
            set { _imagePadding = Math.Max(0, value); Invalidate(); }
        }

        private int _textPadding = 4;
        [Category("Layout")]
        public int TextPadding
        {
            get => _textPadding;
            set { _textPadding = Math.Max(0, value); Invalidate(); }
        }

        private int _spacing = 6;
        [Category("Layout")]
        [DefaultValue(6)]
        [Description("Khoảng cách giữa icon và text")]
        public int Spacing
        {
            get => _spacing;
            set { _spacing = Math.Max(0, value); Invalidate(); }
        }

        private bool _autoEllipsis = true;
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Tự động thêm ... khi text quá dài")]
        public bool AutoEllipsis
        {
            get => _autoEllipsis;
            set { _autoEllipsis = value; Invalidate(); }
        }

        private bool _wordWrap = false;
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Cho phép text xuống dòng")]
        public bool WordWrap
        {
            get => _wordWrap;
            set { _wordWrap = value; Invalidate(); }
        }

        private Color _borderColor = Color.Transparent;
        [Category("Appearance")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        private int _borderWidth = 0;
        [Category("Appearance")]
        [DefaultValue(0)]
        public int BorderWidth
        {
            get => _borderWidth;
            set { _borderWidth = Math.Max(0, value); Invalidate(); }
        }

        private int _cornerRadius = 0;
        [Category("Appearance")]
        [DefaultValue(0)]
        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = Math.Max(0, value); Invalidate(); }
        }

        // Constructor
        public CustomLabel_Image()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.SupportsTransparentBackColor, true);

            base.BackColor = Color.Transparent;
            this.ForeColor = SystemColors.ControlText;
            this.Size = new Size(120, 24);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        }

        // Helper: get rounded rectangle path
        private GraphicsPath GetRoundRect(RectangleF bounds, int radius)
        {
            var path = new GraphicsPath();
            if (radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            float d = radius * 2;
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // Calculate automatic image size based on label size
        private Size GetCalculatedImageSize(Rectangle clientRect)
        {
            if (!_autoSizeImage || _image == null)
                return _imageSize;

            // Calculate size based on label height with scale factor
            int targetSize = (int)(clientRect.Height * _imageScaleFactor);
            targetSize = Math.Max(8, Math.Min(targetSize, clientRect.Height - _imagePadding * 2));

            // Maintain aspect ratio
            float aspectRatio = (float)_image.Width / _image.Height;
            int width = targetSize;
            int height = targetSize;

            if (aspectRatio > 1) // Wider than tall
            {
                height = (int)(width / aspectRatio);
            }
            else if (aspectRatio < 1) // Taller than wide
            {
                width = (int)(height * aspectRatio);
            }

            // Ensure it doesn't exceed label bounds
            if (width > clientRect.Width - _imagePadding * 2)
            {
                width = clientRect.Width - _imagePadding * 2;
                height = (int)(width / aspectRatio);
            }

            return new Size(Math.Max(8, width), Math.Max(8, height));
        }

        // Calculate image and text layout
        private void CalculateLayout(Rectangle clientRect, out Rectangle imgRect, out RectangleF textRect)
        {
            imgRect = Rectangle.Empty;
            textRect = clientRect;

            if (_image == null)
            {
                // No image, text takes full area with padding
                textRect.Inflate(-_textPadding, -_textPadding);
                return;
            }

            // Get image size (auto or manual)
            Size imgSz = GetCalculatedImageSize(clientRect);

            bool hasText = !string.IsNullOrEmpty(this.Text);

            // Calculate image position based on ImageAlign
            Point imgPos = Point.Empty;

            switch (_imageAlign)
            {
                case ContentAlignment.TopLeft:
                    imgPos = new Point(_imagePadding, _imagePadding);
                    break;
                case ContentAlignment.TopCenter:
                    imgPos = new Point((clientRect.Width - imgSz.Width) / 2, _imagePadding);
                    break;
                case ContentAlignment.TopRight:
                    imgPos = new Point(clientRect.Width - imgSz.Width - _imagePadding, _imagePadding);
                    break;
                case ContentAlignment.MiddleLeft:
                    imgPos = new Point(_imagePadding, (clientRect.Height - imgSz.Height) / 2);
                    break;
                case ContentAlignment.MiddleCenter:
                    imgPos = new Point((clientRect.Width - imgSz.Width) / 2, (clientRect.Height - imgSz.Height) / 2);
                    break;
                case ContentAlignment.MiddleRight:
                    imgPos = new Point(clientRect.Width - imgSz.Width - _imagePadding, (clientRect.Height - imgSz.Height) / 2);
                    break;
                case ContentAlignment.BottomLeft:
                    imgPos = new Point(_imagePadding, clientRect.Height - imgSz.Height - _imagePadding);
                    break;
                case ContentAlignment.BottomCenter:
                    imgPos = new Point((clientRect.Width - imgSz.Width) / 2, clientRect.Height - imgSz.Height - _imagePadding);
                    break;
                case ContentAlignment.BottomRight:
                    imgPos = new Point(clientRect.Width - imgSz.Width - _imagePadding, clientRect.Height - imgSz.Height - _imagePadding);
                    break;
            }

            imgRect = new Rectangle(imgPos, imgSz);

            // Calculate text area
            if (hasText)
            {
                // If image is on left or right, adjust text area accordingly
                if (_imageAlign == ContentAlignment.MiddleLeft ||
                    _imageAlign == ContentAlignment.TopLeft ||
                    _imageAlign == ContentAlignment.BottomLeft)
                {
                    // Image on left, text on right
                    float textLeft = imgRect.Right + _spacing;
                    textRect = new RectangleF(
                        textLeft,
                        _textPadding,
                        Math.Max(0, clientRect.Width - textLeft - _textPadding),
                        clientRect.Height - _textPadding * 2
                    );
                }
                else if (_imageAlign == ContentAlignment.MiddleRight ||
                         _imageAlign == ContentAlignment.TopRight ||
                         _imageAlign == ContentAlignment.BottomRight)
                {
                    // Image on right, text on left
                    textRect = new RectangleF(
                        _textPadding,
                        _textPadding,
                        Math.Max(0, imgRect.Left - _spacing - _textPadding),
                        clientRect.Height - _textPadding * 2
                    );
                }
                else if (_imageAlign == ContentAlignment.TopCenter ||
                         _imageAlign == ContentAlignment.TopLeft ||
                         _imageAlign == ContentAlignment.TopRight)
                {
                    // Image on top, text on bottom
                    float textTop = imgRect.Bottom + _spacing;
                    textRect = new RectangleF(
                        _textPadding,
                        textTop,
                        clientRect.Width - _textPadding * 2,
                        Math.Max(0, clientRect.Height - textTop - _textPadding)
                    );
                }
                else if (_imageAlign == ContentAlignment.BottomCenter ||
                         _imageAlign == ContentAlignment.BottomLeft ||
                         _imageAlign == ContentAlignment.BottomRight)
                {
                    // Image on bottom, text on top
                    textRect = new RectangleF(
                        _textPadding,
                        _textPadding,
                        clientRect.Width - _textPadding * 2,
                        Math.Max(0, imgRect.Top - _spacing - _textPadding)
                    );
                }
                else
                {
                    // Image is centered, text uses full width
                    textRect = new RectangleF(
                        _textPadding,
                        _textPadding,
                        clientRect.Width - _textPadding * 2,
                        clientRect.Height - _textPadding * 2
                    );
                }
            }
        }

        // Painting
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle r = ClientRectangle;

            // Background
            if (BackColor != Color.Transparent)
            {
                using (var brush = new SolidBrush(BackColor))
                {
                    if (_cornerRadius > 0)
                    {
                        using (var path = GetRoundRect(r, _cornerRadius))
                        {
                            g.FillPath(brush, path);
                        }
                    }
                    else
                    {
                        g.FillRectangle(brush, r);
                    }
                }
            }

            // Border
            if (_borderWidth > 0 && _borderColor != Color.Transparent)
            {
                using (var pen = new Pen(_borderColor, _borderWidth))
                {
                    pen.Alignment = PenAlignment.Inset;
                    if (_cornerRadius > 0)
                    {
                        using (var path = GetRoundRect(r, _cornerRadius))
                        {
                            g.DrawPath(pen, path);
                        }
                    }
                    else
                    {
                        g.DrawRectangle(pen, r.X, r.Y, r.Width - 1, r.Height - 1);
                    }
                }
            }

            // Calculate layout
            Rectangle imgRect;
            RectangleF textRect;
            CalculateLayout(r, out imgRect, out textRect);

            // Draw image with high quality
            if (_image != null && !imgRect.IsEmpty)
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(_image, imgRect);
            }

            // Draw text
            if (!string.IsNullOrEmpty(this.Text))
            {
                using (var sf = new StringFormat())
                {
                    switch (_textAlign)
                    {
                        case ContentAlignment.TopLeft:
                            sf.Alignment = StringAlignment.Near;
                            sf.LineAlignment = StringAlignment.Near;
                            break;
                        case ContentAlignment.TopCenter:
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Near;
                            break;
                        case ContentAlignment.TopRight:
                            sf.Alignment = StringAlignment.Far;
                            sf.LineAlignment = StringAlignment.Near;
                            break;
                        case ContentAlignment.MiddleLeft:
                            sf.Alignment = StringAlignment.Near;
                            sf.LineAlignment = StringAlignment.Center;
                            break;
                        case ContentAlignment.MiddleCenter:
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Center;
                            break;
                        case ContentAlignment.MiddleRight:
                            sf.Alignment = StringAlignment.Far;
                            sf.LineAlignment = StringAlignment.Center;
                            break;
                        case ContentAlignment.BottomLeft:
                            sf.Alignment = StringAlignment.Near;
                            sf.LineAlignment = StringAlignment.Far;
                            break;
                        case ContentAlignment.BottomCenter:
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Far;
                            break;
                        case ContentAlignment.BottomRight:
                            sf.Alignment = StringAlignment.Far;
                            sf.LineAlignment = StringAlignment.Far;
                            break;
                    }

                    if (_autoEllipsis)
                        sf.Trimming = StringTrimming.EllipsisCharacter;

                    if (!_wordWrap)
                        sf.FormatFlags = StringFormatFlags.NoWrap;

                    using (var textBrush = new SolidBrush(this.ForeColor))
                    {
                        g.DrawString(this.Text, this.Font, textBrush, textRect, sf);
                    }
                }
            }

        }
    }
}



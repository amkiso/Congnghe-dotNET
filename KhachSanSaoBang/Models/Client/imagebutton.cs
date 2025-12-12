using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace KhachSanSaoBang
{
    // Enum định nghĩa kiểu hiển thị
    public enum ButtonDisplayStyle
    {
        ImageAndText,
        ImageOnly,
        TextOnly
    }

    public class ImageButton : Control
    {
        // --- Properties ---

        private Image _image = null;
        [Category("Appearance")]
        public Image Image
        {
            get => _image;
            set { _image = value; Invalidate(); }
        }

        private ButtonDisplayStyle _displayStyle = ButtonDisplayStyle.ImageAndText;
        [Category("Appearance")]
        [DefaultValue(ButtonDisplayStyle.ImageAndText)]
        [Description("Chọn chế độ hiển thị: Cả hai, Chỉ ảnh, hoặc Chỉ chữ.")]
        public ButtonDisplayStyle DisplayStyle
        {
            get => _displayStyle;
            set { _displayStyle = value; Invalidate(); }
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

        private float _imageScaleFactor = 0.5f;
        [Category("Appearance")]
        [DefaultValue(0.5f)]
        [Description("Tỷ lệ icon so với chiều cao button khi AutoSizeImage = true (0.3 - 0.8)")]
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

        private ContentAlignment _textAlign = ContentAlignment.MiddleCenter;
        [Category("Appearance")]
        public ContentAlignment TextAlign
        {
            get => _textAlign;
            set { _textAlign = value; Invalidate(); }
        }

        private int _cornerRadius = 8;
        [Category("Appearance")]
        [DefaultValue(8)]
        [Description("Độ bo góc. Đặt = 0 để tắt bo tròn (vuông góc).")]
        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = Math.Max(0, value); Invalidate(); }
        }

        // --- Colors ---

        private Color _normalBackColor = Color.White;
        [Category("Appearance")]
        public Color NormalBackColor
        {
            get => _normalBackColor;
            set { _normalBackColor = value; Invalidate(); }
        }

        private Color _hoverBackColor = Color.FromArgb(240, 240, 240);
        [Category("Appearance")]
        public Color HoverBackColor
        {
            get => _hoverBackColor;
            set { _hoverBackColor = value; Invalidate(); }
        }

        private Color _pressedBackColor = Color.FromArgb(220, 220, 220);
        [Category("Appearance")]
        public Color PressedBackColor
        {
            get => _pressedBackColor;
            set { _pressedBackColor = value; Invalidate(); }
        }

        private Color _disabledBackColor = Color.FromArgb(245, 245, 245);
        [Category("Appearance")]
        public Color DisabledBackColor
        {
            get => _disabledBackColor;
            set { _disabledBackColor = value; Invalidate(); }
        }

        private Color _borderColor = Color.FromArgb(200, 200, 200);
        [Category("Appearance")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        private int _borderWidth = 1;
        [Category("Appearance")]
        public int BorderWidth
        {
            get => _borderWidth;
            set { _borderWidth = Math.Max(0, value); Invalidate(); }
        }

        // --- Layout Padding ---

        private int _imagePadding = 8;
        [Category("Layout")]
        public int ImagePadding
        {
            get => _imagePadding;
            set { _imagePadding = Math.Max(0, value); Invalidate(); }
        }

        private int _textPadding = 8;
        [Category("Layout")]
        public int TextPadding
        {
            get => _textPadding;
            set { _textPadding = Math.Max(0, value); Invalidate(); }
        }

        // --- Text AutoSize ---

        private bool _autoSizeText = false;
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool AutoSizeText
        {
            get => _autoSizeText;
            set { _autoSizeText = value; Invalidate(); }
        }

        private float _minFontSize = 6f;
        [Category("Appearance")]
        [DefaultValue(6f)]
        public float MinFontSize
        {
            get => _minFontSize;
            set { _minFontSize = Math.Max(4f, value); Invalidate(); }
        }

        private float _maxFontSize = 48f;
        [Category("Appearance")]
        [DefaultValue(48f)]
        public float MaxFontSize
        {
            get => _maxFontSize;
            set { _maxFontSize = Math.Max(_minFontSize, value); Invalidate(); }
        }

        // --- Effects ---

        private bool _enableShadow = false;
        [Category("Appearance")]
        public bool EnableShadow
        {
            get => _enableShadow;
            set { _enableShadow = value; Invalidate(); }
        }

        // --- Gradient ---

        private bool _useGradient = false;
        [Category("Appearance")]
        public bool UseGradient
        {
            get => _useGradient;
            set { _useGradient = value; Invalidate(); }
        }

        private Color _gradientColor1 = Color.White;
        [Category("Appearance")]
        public Color GradientColor1
        {
            get => _gradientColor1;
            set { _gradientColor1 = value; Invalidate(); }
        }

        private Color _gradientColor2 = Color.LightGray;
        [Category("Appearance")]
        public Color GradientColor2
        {
            get => _gradientColor2;
            set { _gradientColor2 = value; Invalidate(); }
        }

        private LinearGradientMode _gradientMode = LinearGradientMode.Vertical;
        [Category("Appearance")]
        public LinearGradientMode GradientMode
        {
            get => _gradientMode;
            set { _gradientMode = value; Invalidate(); }
        }

        // Override BackColor
        [Browsable(true)]
        [Category("Appearance")]
        public override Color BackColor
        {
            get => _normalBackColor;
            set
            {
                _normalBackColor = value;
                base.BackColor = value;
                Invalidate();
            }
        }

        // States
        private bool _hovered = false;
        private bool _pressed = false;

        // Constructor
        public ImageButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.SupportsTransparentBackColor, true);

            base.BackColor = Color.White;
            this.ForeColor = Color.FromArgb(60, 60, 60);
            this.Size = new Size(120, 40);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.Cursor = Cursors.Hand;
        }

        // Mouse Events
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _hovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hovered = false;
            _pressed = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            if (mevent.Button == MouseButtons.Left)
            {
                _pressed = true;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left && _pressed)
            {
                _pressed = false;
                Invalidate();
            }
            base.OnMouseUp(mevent);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        // Helper: Get Rounded Rectangle
        private GraphicsPath GetRoundRect(RectangleF bounds, int radius)
        {
            var path = new GraphicsPath();
            // Nếu radius <= 0 thì vẽ hình chữ nhật vuông
            if (radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // Xử lý trường hợp radius quá lớn so với kích thước nút
            float d = Math.Min(radius * 2, Math.Min(bounds.Width, bounds.Height));

            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private Size GetCalculatedImageSize(Rectangle clientRect)
        {
            if (!_autoSizeImage || _image == null)
                return _imageSize;

            int targetSize = (int)(clientRect.Height * _imageScaleFactor);
            targetSize = Math.Max(8, Math.Min(targetSize, clientRect.Height - _imagePadding * 2));

            float aspectRatio = (float)_image.Width / _image.Height;
            int width = targetSize;
            int height = targetSize;

            if (aspectRatio > 1) height = (int)(width / aspectRatio);
            else if (aspectRatio < 1) width = (int)(height * aspectRatio);

            if (width > clientRect.Width - _imagePadding * 2)
            {
                width = clientRect.Width - _imagePadding * 2;
                height = (int)(width / aspectRatio);
            }

            return new Size(Math.Max(8, width), Math.Max(8, height));
        }

        // --- Cập nhật CalculateLayout để hỗ trợ DisplayStyle ---
        private void CalculateLayout(Rectangle clientRect, out Rectangle imgRect, out RectangleF textRect)
        {
            imgRect = Rectangle.Empty;
            textRect = clientRect;

            // Kiểm tra xem có hiển thị ảnh không
            bool showImage = (_displayStyle != ButtonDisplayStyle.TextOnly) && (_image != null);
            // Kiểm tra xem có hiển thị chữ không
            bool showText = (_displayStyle != ButtonDisplayStyle.ImageOnly) && !string.IsNullOrEmpty(this.Text);

            // Trường hợp 1: Không có ảnh (hoặc mode TextOnly)
            if (!showImage)
            {
                textRect = new RectangleF(
                    _textPadding,
                    2,
                    clientRect.Width - _textPadding * 2,
                    clientRect.Height - 4
                );
                return;
            }

            // Tính kích thước ảnh
            Size imgSz = GetCalculatedImageSize(clientRect);

            // Trường hợp 2: Chỉ có ảnh (Mode ImageOnly hoặc text rỗng)
            if (!showText)
            {
                // Căn chỉnh ảnh theo ImageAlign nhưng trong toàn bộ không gian (bỏ qua text)
                Point singleImgPos = Point.Empty;
                switch (_imageAlign)
                {
                    case ContentAlignment.TopLeft: singleImgPos = new Point(_imagePadding, _imagePadding); break;
                    case ContentAlignment.TopCenter: singleImgPos = new Point((clientRect.Width - imgSz.Width) / 2, _imagePadding); break;
                    case ContentAlignment.TopRight: singleImgPos = new Point(clientRect.Width - imgSz.Width - _imagePadding, _imagePadding); break;
                    case ContentAlignment.MiddleLeft: singleImgPos = new Point(_imagePadding, (clientRect.Height - imgSz.Height) / 2); break;
                    case ContentAlignment.MiddleCenter: singleImgPos = new Point((clientRect.Width - imgSz.Width) / 2, (clientRect.Height - imgSz.Height) / 2); break;
                    case ContentAlignment.MiddleRight: singleImgPos = new Point(clientRect.Width - imgSz.Width - _imagePadding, (clientRect.Height - imgSz.Height) / 2); break;
                    case ContentAlignment.BottomLeft: singleImgPos = new Point(_imagePadding, clientRect.Height - imgSz.Height - _imagePadding); break;
                    case ContentAlignment.BottomCenter: singleImgPos = new Point((clientRect.Width - imgSz.Width) / 2, clientRect.Height - imgSz.Height - _imagePadding); break;
                    case ContentAlignment.BottomRight: singleImgPos = new Point(clientRect.Width - imgSz.Width - _imagePadding, clientRect.Height - imgSz.Height - _imagePadding); break;
                }
                imgRect = new Rectangle(singleImgPos, imgSz);
                textRect = RectangleF.Empty; // Không vẽ text
                return;
            }

            // Trường hợp 3: Có cả ảnh và chữ (ImageAndText) -> Logic cũ
            Point imgPos = Point.Empty;

            switch (_imageAlign)
            {
                case ContentAlignment.TopLeft: imgPos = new Point(_imagePadding, _imagePadding); break;
                case ContentAlignment.TopCenter: imgPos = new Point((clientRect.Width - imgSz.Width) / 2, _imagePadding); break;
                case ContentAlignment.TopRight: imgPos = new Point(clientRect.Width - imgSz.Width - _imagePadding, _imagePadding); break;
                case ContentAlignment.MiddleLeft: imgPos = new Point(_imagePadding, (clientRect.Height - imgSz.Height) / 2); break;
                case ContentAlignment.MiddleCenter: imgPos = new Point((clientRect.Width - imgSz.Width) / 2, (clientRect.Height - imgSz.Height) / 2); break;
                case ContentAlignment.MiddleRight: imgPos = new Point(clientRect.Width - imgSz.Width - _imagePadding, (clientRect.Height - imgSz.Height) / 2); break;
                case ContentAlignment.BottomLeft: imgPos = new Point(_imagePadding, clientRect.Height - imgSz.Height - _imagePadding); break;
                case ContentAlignment.BottomCenter: imgPos = new Point((clientRect.Width - imgSz.Width) / 2, clientRect.Height - imgSz.Height - _imagePadding); break;
                case ContentAlignment.BottomRight: imgPos = new Point(clientRect.Width - imgSz.Width - _imagePadding, clientRect.Height - imgSz.Height - _imagePadding); break;
            }

            imgRect = new Rectangle(imgPos, imgSz);

            // Tính toán vùng text dựa trên vị trí ảnh
            if (_imageAlign == ContentAlignment.MiddleLeft || _imageAlign == ContentAlignment.TopLeft || _imageAlign == ContentAlignment.BottomLeft)
            {
                float textLeft = imgRect.Right + _textPadding;
                textRect = new RectangleF(textLeft, 2, clientRect.Width - textLeft - _textPadding, clientRect.Height - 4);
            }
            else if (_imageAlign == ContentAlignment.MiddleRight || _imageAlign == ContentAlignment.TopRight || _imageAlign == ContentAlignment.BottomRight)
            {
                textRect = new RectangleF(_textPadding, 2, imgRect.Left - _textPadding * 2, clientRect.Height - 4);
            }
            else
            {
                textRect = new RectangleF(_textPadding, 2, clientRect.Width - _textPadding * 2, clientRect.Height - 4);
            }
        }

        private Color GetCurrentBackColor()
        {
            if (!this.Enabled) return _disabledBackColor;
            if (_pressed) return _pressedBackColor;
            if (_hovered) return _hoverBackColor;
            return _normalBackColor;
        }

        private Font GetOptimalFont(Graphics g, string text, RectangleF bounds, Font originalFont)
        {
            if (!_autoSizeText || string.IsNullOrEmpty(text) || bounds.Width <= 0 || bounds.Height <= 0)
                return originalFont;

            float availableWidth = bounds.Width - 4;
            float availableHeight = bounds.Height - 4;

            if (availableWidth <= 0 || availableHeight <= 0) return originalFont;

            float fontSize = Math.Min(_maxFontSize, originalFont.Size);
            Font testFont = new Font(originalFont.FontFamily, fontSize, originalFont.Style);
            SizeF textSize = g.MeasureString(text, testFont, (int)availableWidth);

            while (fontSize > _minFontSize && (textSize.Width > availableWidth || textSize.Height > availableHeight))
            {
                fontSize -= 0.5f;
                testFont.Dispose();
                testFont = new Font(originalFont.FontFamily, fontSize, originalFont.Style);
                textSize = g.MeasureString(text, testFont, (int)availableWidth);
            }

            while (fontSize < _maxFontSize)
            {
                Font nextFont = new Font(originalFont.FontFamily, fontSize + 0.5f, originalFont.Style);
                SizeF nextSize = g.MeasureString(text, nextFont, (int)availableWidth);

                if (nextSize.Width <= availableWidth && nextSize.Height <= availableHeight)
                {
                    testFont.Dispose();
                    testFont = nextFont;
                    fontSize += 0.5f;
                }
                else
                {
                    nextFont.Dispose();
                    break;
                }
            }

            return testFont;
        }

        // Painting
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle r = ClientRectangle;

            // Shadow
            if (_enableShadow && !_pressed && this.Enabled)
            {
                using (var shadowPath = GetRoundRect(new RectangleF(r.X + 2, r.Y + 2, r.Width, r.Height), CornerRadius))
                using (var shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
                {
                    g.FillPath(shadowBrush, shadowPath);
                }
            }

            RectangleF drawRect = r;
            if (_pressed && this.Enabled) drawRect.Offset(1, 1);

            using (var path = GetRoundRect(drawRect, CornerRadius))
            {
                if (_useGradient)
                {
                    Color color1 = _gradientColor1;
                    Color color2 = _gradientColor2;

                    if (!this.Enabled) { color1 = _disabledBackColor; color2 = _disabledBackColor; }
                    else if (_pressed) { color1 = ControlPaint.Dark(_gradientColor1, 0.1f); color2 = ControlPaint.Dark(_gradientColor2, 0.1f); }
                    else if (_hovered) { color1 = ControlPaint.Light(_gradientColor1, 0.1f); color2 = ControlPaint.Light(_gradientColor2, 0.1f); }

                    RectangleF gradientRect = drawRect;
                    if (gradientRect.Width > 0 && gradientRect.Height > 0)
                    {
                        using (var gradientBrush = new LinearGradientBrush(gradientRect, color1, color2, _gradientMode))
                        {
                            g.FillPath(gradientBrush, path);
                        }
                    }
                }
                else
                {
                    Color back = GetCurrentBackColor();
                    using (var brush = new SolidBrush(back))
                    {
                        g.FillPath(brush, path);
                    }
                }

                if (_borderWidth > 0)
                {
                    using (var pen = new Pen(_borderColor, _borderWidth))
                    {
                        pen.Alignment = PenAlignment.Inset;
                        g.DrawPath(pen, path);
                    }
                }
            }

            // Tính toán Layout (đã bao gồm logic DisplayStyle)
            Rectangle imgRect;
            RectangleF textRect;
            CalculateLayout(new Rectangle((int)drawRect.X, (int)drawRect.Y, (int)drawRect.Width, (int)drawRect.Height), out imgRect, out textRect);

            // Vẽ ảnh (Nếu DisplayStyle != TextOnly và có ảnh)
            if (_image != null && !imgRect.IsEmpty && imgRect.Width > 0 && imgRect.Height > 0 && _displayStyle != ButtonDisplayStyle.TextOnly)
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                if (!this.Enabled)
                {
                    try
                    {
                        using (var imageAttr = new System.Drawing.Imaging.ImageAttributes())
                        {
                            float[][] matrixItems = {
                                new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                                new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                                new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                                new float[] {0, 0, 0, 0.5f, 0},
                                new float[] {0.2f, 0.2f, 0.2f, 0, 1}
                            };
                            var colorMatrix = new System.Drawing.Imaging.ColorMatrix(matrixItems);
                            imageAttr.SetColorMatrix(colorMatrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);
                            g.DrawImage(_image, imgRect, 0, 0, _image.Width, _image.Height, GraphicsUnit.Pixel, imageAttr);
                        }
                    }
                    catch
                    {
                        g.DrawImage(_image, imgRect);
                    }
                }
                else
                {
                    g.DrawImage(_image, imgRect);
                }
            }

            // Vẽ chữ (Nếu DisplayStyle != ImageOnly)
            if (!string.IsNullOrEmpty(this.Text) && !textRect.IsEmpty && _displayStyle != ButtonDisplayStyle.ImageOnly)
            {
                Font drawFont = _autoSizeText ? GetOptimalFont(g, this.Text, textRect, this.Font) : this.Font;

                using (var sf = new StringFormat())
                {
                    // Map ContentAlignment sang StringAlignment
                    switch (_textAlign)
                    {
                        case ContentAlignment.TopLeft: sf.Alignment = StringAlignment.Near; sf.LineAlignment = StringAlignment.Near; break;
                        case ContentAlignment.TopCenter: sf.Alignment = StringAlignment.Center; sf.LineAlignment = StringAlignment.Near; break;
                        case ContentAlignment.TopRight: sf.Alignment = StringAlignment.Far; sf.LineAlignment = StringAlignment.Near; break;
                        case ContentAlignment.MiddleLeft: sf.Alignment = StringAlignment.Near; sf.LineAlignment = StringAlignment.Center; break;
                        case ContentAlignment.MiddleCenter: sf.Alignment = StringAlignment.Center; sf.LineAlignment = StringAlignment.Center; break;
                        case ContentAlignment.MiddleRight: sf.Alignment = StringAlignment.Far; sf.LineAlignment = StringAlignment.Center; break;
                        case ContentAlignment.BottomLeft: sf.Alignment = StringAlignment.Near; sf.LineAlignment = StringAlignment.Far; break;
                        case ContentAlignment.BottomCenter: sf.Alignment = StringAlignment.Center; sf.LineAlignment = StringAlignment.Far; break;
                        case ContentAlignment.BottomRight: sf.Alignment = StringAlignment.Far; sf.LineAlignment = StringAlignment.Far; break;
                    }

                    sf.Trimming = StringTrimming.EllipsisCharacter;
                    sf.FormatFlags = StringFormatFlags.NoWrap;

                    Color textColor = this.Enabled ? this.ForeColor : ControlPaint.Dark(this.ForeColor, 0.5f);
                    using (var textBrush = new SolidBrush(textColor))
                    {
                        g.DrawString(this.Text, drawFont, textBrush, textRect, sf);
                    }
                }
                if (_autoSizeText && drawFont != this.Font) drawFont.Dispose();
            }

            if (this.Focused && this.Enabled)
            {
                Rectangle fr = Rectangle.Inflate(r, -4, -4);
                ControlPaint.DrawFocusRectangle(g, fr);
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Space || keyData == Keys.Enter) return true;
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                _pressed = true;
                Invalidate();
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if ((e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter) && _pressed)
            {
                _pressed = false;
                Invalidate();
                OnClick(EventArgs.Empty);
            }
        }
    }
}
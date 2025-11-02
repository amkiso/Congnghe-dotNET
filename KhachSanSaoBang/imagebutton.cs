using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class ImageButton : Control
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
    public int CornerRadius
    {
        get => _cornerRadius;
        set { _cornerRadius = Math.Max(0, value); Invalidate(); }
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

    private bool _enableShadow = false;
    [Category("Appearance")]
    public bool EnableShadow
    {
        get => _enableShadow;
        set { _enableShadow = value; Invalidate(); }
    }

    private Color _defaultBackColor = Color.White;
    [Browsable(false)]
    public override Color BackColor
    {
        get => base.BackColor;
        set { base.BackColor = value; _defaultBackColor = value; Invalidate(); }
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

    // Mouse events
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
        bool wasPressed = _pressed;
        base.OnMouseUp(mevent);
        if (mevent.Button == MouseButtons.Left)
        {
            _pressed = false;
            Invalidate();
            if (wasPressed && ClientRectangle.Contains(mevent.Location))
                OnClick(EventArgs.Empty);
        }
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

    // Calculate automatic image size based on button size
    private Size GetCalculatedImageSize(Rectangle clientRect)
    {
        if (!_autoSizeImage || _image == null)
            return _imageSize;

        // Calculate size based on button height with scale factor
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

        // Ensure it doesn't exceed button bounds
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
                float textLeft = imgRect.Right + _textPadding;
                textRect = new RectangleF(
                    textLeft,
                    _textPadding,
                    clientRect.Width - textLeft - _textPadding,
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
                    imgRect.Left - _textPadding * 2,
                    clientRect.Height - _textPadding * 2
                );
            }
            else
            {
                // Image is centered or top/bottom, text uses full width
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

        // Shadow effect
        if (_enableShadow && !_pressed)
        {
            using (var shadowPath = GetRoundRect(new RectangleF(r.X + 2, r.Y + 2, r.Width, r.Height), CornerRadius))
            using (var shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
            {
                g.FillPath(shadowBrush, shadowPath);
            }
        }

        // Background
        Color back = _defaultBackColor;
        if (_pressed)
            back = _pressedBackColor;
        else if (_hovered)
            back = _hoverBackColor;

        // Apply slight offset when pressed for depth effect
        RectangleF drawRect = r;
        if (_pressed)
        {
            drawRect.Offset(1, 1);
        }

        using (var brush = new SolidBrush(back))
        using (var path = GetRoundRect(drawRect, CornerRadius))
        {
            g.FillPath(brush, path);
        }

        // Border
        if (_borderWidth > 0)
        {
            using (var pen = new Pen(_borderColor, _borderWidth))
            {
                pen.Alignment = PenAlignment.Inset;
                using (var path = GetRoundRect(drawRect, CornerRadius))
                {
                    g.DrawPath(pen, path);
                }
            }
        }

        // Calculate layout
        Rectangle imgRect;
        RectangleF textRect;
        CalculateLayout(new Rectangle((int)drawRect.X, (int)drawRect.Y, (int)drawRect.Width, (int)drawRect.Height),
                       out imgRect, out textRect);

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

                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.NoWrap;

                using (var textBrush = new SolidBrush(this.ForeColor))
                {
                    g.DrawString(this.Text, this.Font, textBrush, textRect, sf);
                }
            }
        }

        // Focus rectangle
        if (this.Focused)
        {
            Rectangle fr = Rectangle.Inflate(r, -4, -4);
            ControlPaint.DrawFocusRectangle(g, fr);
        }
    }

    // Keyboard support
    protected override bool IsInputKey(Keys keyData)
    {
        if (keyData == Keys.Space || keyData == Keys.Enter)
            return true;
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
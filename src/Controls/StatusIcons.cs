using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Chemo.Controls
{
    internal enum TreatmentStatus
    {
        NotStarted,
        Ok,
        Info,
        Restart,
        Error,
    }

    /// <summary>
    /// Status icons drawn from the Windows 11 icon font, in the style of the Windows Settings app:
    /// a colored circle with a mark cut out of it.
    /// </summary>
    internal static class StatusIcons
    {
        private const string FontName = "Segoe Fluent Icons";

        // Menu icons, in Segoe Fluent Icons
        public const string CopyGlyph = "";
        public const string OpenInNewWindowGlyph = "";
        private const string Circle = "";
        private const string Checkmark = "";
        private const string InfoMark = "";
        private const string RestartMark = "";
        private const string ErrorMark = "";

        /// <summary>
        /// Creates an image list holding an icon for each status, keyed by the status name.
        /// </summary>
        /// <param name="size">The icon size in device pixels.</param>
        /// <param name="dark">Whether the icons are shown on a dark background.</param>
        public static ImageList Create(int size, bool dark)
        {
            // Colors match the WinUI system fill colors for success, attention, caution, and critical.
            Color markColor = dark ? Color.FromArgb(228, 0, 0, 0) : Color.White;
            Color ok = dark ? Color.FromArgb(0x6C, 0xCB, 0x5F) : Color.FromArgb(0x0F, 0x7B, 0x0F);
            Color info = dark ? Color.FromArgb(0x60, 0xCD, 0xFF) : Color.FromArgb(0x00, 0x5F, 0xB7);
            Color restart = dark ? Color.FromArgb(0xFC, 0xE1, 0x00) : Color.FromArgb(0x9D, 0x5D, 0x00);
            Color error = dark ? Color.FromArgb(0xFF, 0x99, 0xA4) : Color.FromArgb(0xC4, 0x2B, 0x1C);

            ImageList imageList = new()
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(size, size)
            };

            // Nothing is shown until a treatment has a result.
            imageList.Images.Add(nameof(TreatmentStatus.NotStarted), new Bitmap(size, size, PixelFormat.Format32bppArgb));
            imageList.Images.Add(nameof(TreatmentStatus.Ok), Draw(size, ok, Checkmark, markColor));
            imageList.Images.Add(nameof(TreatmentStatus.Info), Draw(size, info, InfoMark, markColor));
            imageList.Images.Add(nameof(TreatmentStatus.Restart), Draw(size, restart, RestartMark, markColor));
            imageList.Images.Add(nameof(TreatmentStatus.Error), Draw(size, error, ErrorMark, markColor));

            return imageList;
        }

        private static Bitmap Draw(int size, Color circleColor, string mark, Color markColor)
        {
            Bitmap bitmap = new(size, size, PixelFormat.Format32bppArgb);

            using (FontFamily family = new(FontName))
            using (GraphicsPath circle = Glyph(family, Circle, size))
            using (GraphicsPath markPath = Glyph(family, mark, size))
            using (Matrix center = new())
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (SolidBrush circleBrush = new(circleColor))
            using (SolidBrush markBrush = new(markColor))
            {
                // The mark glyphs are drawn relative to the circle, so both move by the offset that centers the circle.
                RectangleF bounds = circle.GetBounds();
                center.Translate(((size - bounds.Width) / 2) - bounds.X, ((size - bounds.Height) / 2) - bounds.Y);
                circle.Transform(center);
                markPath.Transform(center);

                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillPath(circleBrush, circle);
                graphics.FillPath(markBrush, markPath);
            }

            return bitmap;
        }

        /// <summary>
        /// Draws a single glyph from the icon font, centered, such as an icon for a menu item.
        /// </summary>
        /// <param name="glyph">The glyph's character in Segoe Fluent Icons.</param>
        /// <param name="size">The icon size in device pixels.</param>
        /// <param name="color">The glyph's color.</param>
        public static Bitmap DrawGlyph(string glyph, int size, Color color)
        {
            Bitmap bitmap = new(size, size, PixelFormat.Format32bppArgb);

            using (FontFamily family = new(FontName))
            using (GraphicsPath path = Glyph(family, glyph, size))
            using (Matrix center = new())
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (SolidBrush brush = new(color))
            {
                RectangleF bounds = path.GetBounds();
                center.Translate(((size - bounds.Width) / 2) - bounds.X, ((size - bounds.Height) / 2) - bounds.Y);
                path.Transform(center);

                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillPath(brush, path);
            }

            return bitmap;
        }

        private static GraphicsPath Glyph(FontFamily family, string glyph, int size)
        {
            GraphicsPath path = new();
            path.AddString(glyph, family, (int)FontStyle.Regular, size, PointF.Empty, StringFormat.GenericTypographic);
            return path;
        }
    }
}

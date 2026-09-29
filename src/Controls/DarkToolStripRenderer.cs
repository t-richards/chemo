namespace Chemo.Controls
{
    /// <summary>
    /// Draws the menu bar, its menus and the status bar in <see cref="DarkTheme"/> colors. Windows Forms draws these
    /// itself, so unlike the other controls they can't use Windows' dark look.
    /// </summary>
    internal sealed class DarkToolStripRenderer : ToolStripProfessionalRenderer
    {
        public DarkToolStripRenderer()
            : base(new DarkColors())
        {
            RoundedEdges = false;
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = e.Item.Enabled ? DarkTheme.Text : DarkTheme.DisabledText;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = DarkTheme.Text;
            base.OnRenderArrow(e);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            // The standard check mark is a black image, so draw the icon font's check mark in the text color instead.
            using Bitmap checkMark = FluentIcons.DrawGlyph(FluentIcons.CheckMarkGlyph, e.ImageRectangle.Width, DarkTheme.Text);
            e.Graphics.DrawImage(checkMark, e.ImageRectangle.Location);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            // The status bar's top edge is drawn in a light system color whatever the color table says, so leave it out.
            if (e.ToolStrip is not StatusStrip)
            {
                base.OnRenderToolStripBorder(e);
            }
        }

        private sealed class DarkColors : ProfessionalColorTable
        {
            public DarkColors()
            {
                UseSystemColors = false;
            }

            public override Color MenuStripGradientBegin => DarkTheme.Background;

            public override Color MenuStripGradientEnd => DarkTheme.Background;

            public override Color StatusStripGradientBegin => DarkTheme.Background;

            public override Color StatusStripGradientEnd => DarkTheme.Background;

            public override Color ToolStripBorder => DarkTheme.Background;

            public override Color ToolStripDropDownBackground => DarkTheme.MenuBackground;

            public override Color ImageMarginGradientBegin => DarkTheme.MenuBackground;

            public override Color ImageMarginGradientMiddle => DarkTheme.MenuBackground;

            public override Color ImageMarginGradientEnd => DarkTheme.MenuBackground;

            public override Color MenuBorder => DarkTheme.MenuBorder;

            public override Color MenuItemBorder => DarkTheme.MenuHover;

            public override Color MenuItemSelected => DarkTheme.MenuHover;

            public override Color MenuItemSelectedGradientBegin => DarkTheme.MenuHover;

            public override Color MenuItemSelectedGradientEnd => DarkTheme.MenuHover;

            public override Color MenuItemPressedGradientBegin => DarkTheme.MenuBackground;

            public override Color MenuItemPressedGradientMiddle => DarkTheme.MenuBackground;

            public override Color MenuItemPressedGradientEnd => DarkTheme.MenuBackground;

            public override Color SeparatorDark => DarkTheme.MenuBorder;

            public override Color SeparatorLight => DarkTheme.MenuBackground;

            // The dots of the status bar's resize grip.
            public override Color GripDark => Color.FromArgb(0x5A, 0x5A, 0x5A);

            public override Color GripLight => DarkTheme.Background;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InputDisplay;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InputDisplay.Entities
{
    class RectangularButton: FaceButton
    {
        public RectangularButton(Point coords, Size size, double corner): base(coords)
        {
            this.BaseSize = size;
            this.Size = size;
            this.CornerPercentage = corner;
            this.CornerRadius = (int)(corner * Math.Min(this.Size.Width, this.Size.Height));
        }

        public override void Draw(ref Graphics g, Color colour)
        {
            if (this.Highlighted)
            {
                Color highlightColour = Color.FromArgb(200, 255 - Config.BackgroundColour.R, 255 - Config.BackgroundColour.G, 255 - Config.BackgroundColour.B);
                Pen outlinePen = new Pen(highlightColour, (Config.LineWidth + 2 * Config.Outline) + 8);
                g.DrawRoundedRectangle(outlinePen, new Rectangle(new Point(this.Coords.X, this.Coords.Y), this.Size), this.CornerRadius);
            }
            if (this.Pressed)
            {
                SolidBrush brush = new SolidBrush(colour);
                g.FillRoundedRectangle(brush, new Rectangle(new Point(this.Coords.X, this.Coords.Y), this.Size), this.CornerRadius);
            }

            if (Config.UseOutline)
            {
                Pen outlinePen = new Pen(Config.OutlineColour, Config.LineWidth + 2 * Config.Outline);
                g.DrawRoundedRectangle(outlinePen, new Rectangle(new Point(this.Coords.X, this.Coords.Y), this.Size), this.CornerRadius);
            }

            if (Config.LineWidth != 0)
            {
                Pen pen = new Pen(colour, Config.LineWidth);
                g.DrawRoundedRectangle(pen, new Rectangle(new Point(this.Coords.X, this.Coords.Y), this.Size), this.CornerRadius);
            }
        }

        public override bool CheckMouse(Point cursor)
        {
            return CustomShapes.RoundedRect(new Rectangle(new Point(this.Coords.X, this.Coords.Y), this.Size), this.CornerRadius).IsVisible(cursor);
        }

        public override void Scale(double scale)
        {
            this.Size.Width = (int) (scale * this.BaseSize.Width);
            this.Size.Height = (int) (scale * this.BaseSize.Height);
            this.CornerRadius = (int)(this.CornerPercentage * Math.Min(this.Size.Width, this.Size.Height));
        }

        private Size BaseSize;
        private Size Size;
        private double CornerPercentage;
        private int CornerRadius;
    }
}

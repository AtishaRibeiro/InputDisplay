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
    class Bar: FaceButton
    {
        public Bar(int x, int y): base(x, y)
        {
            this.Size = new Size(90, 23);
            this.CornerRadius = (int)(0.5 * Math.Min(this.Size.Width, this.Size.Height));
        }

        public override void Draw(ref Graphics g, Color colour)
        {
            if (this.Highlighted)
            {
                Color highlightColour = Color.FromArgb(200, 255 - Config.BackgroundColour.R, 255 - Config.BackgroundColour.G, 255 - Config.BackgroundColour.B);
                Pen outlinePen = new Pen(highlightColour, (Config.LineWidth + 2 * Config.Outline) + 8);
                g.DrawRoundedRectangle(outlinePen, new Rectangle(new Point(this.Coords.x, this.Coords.y), this.Size), this.CornerRadius);
            }
            if (this.Pressed)
            {
                SolidBrush brush = new SolidBrush(colour);
                g.FillRoundedRectangle(brush, new Rectangle(new Point(this.Coords.x, this.Coords.y), this.Size), this.CornerRadius);
            }

            if (Config.UseOutline)
            {
                Pen outlinePen = new Pen(Config.OutlineColour, Config.LineWidth + 2 * Config.Outline);
                g.DrawRoundedRectangle(outlinePen, new Rectangle(new Point(this.Coords.x, this.Coords.y), this.Size), this.CornerRadius);
            }

            Pen pen = new Pen(colour, Config.LineWidth);
            g.DrawRoundedRectangle(pen, new Rectangle(new Point(this.Coords.x, this.Coords.y), this.Size), this.CornerRadius);
        }

        public override bool CheckMouse(Point cursor)
        {
            return CustomShapes.RoundedRect(new Rectangle(new Point(this.Coords.x, this.Coords.y), this.Size), this.CornerRadius).IsVisible(cursor);
        }

        public override void Scale(double scale)
        {
            this.Size.Width = (int) (scale * 90);
            this.Size.Height = (int) (scale * 23);
            this.CornerRadius = (int)(0.5 * Math.Min(this.Size.Width, this.Size.Height));
        }

        private Size Size;
        private int CornerRadius;
    }
}

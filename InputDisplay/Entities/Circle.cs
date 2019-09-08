using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InputDisplay.Entities
{
    class Circle: FaceButton
    {
        public Circle(Point coords, int radius): base(coords)
        {
            this.BaseRadius = radius;
            this.Radius = radius;
        }

        public override void Draw(ref Graphics g, Color colour)
        {
            if (this.Highlighted)
            {
                Color highlightColour = Color.FromArgb(200, 255 - Config.BackgroundColour.R, 255 - Config.BackgroundColour.G, 255 - Config.BackgroundColour.B);
                Pen highlightPen = new Pen(highlightColour, (Config.LineWidth + 2 * Config.Outline) + 8);
                g.DrawEllipse(highlightPen, this.Coords.X - this.Radius, this.Coords.Y - this.Radius, this.Radius * 2, this.Radius * 2);
            }
            if (this.Pressed)
            {
                SolidBrush brush = new SolidBrush(colour);
                g.FillEllipse(brush, this.Coords.X - this.Radius, this.Coords.Y - this.Radius, this.Radius * 2, this.Radius * 2);
            }
            if (Config.UseOutline)
            {
                Pen outlinePen = new Pen(Config.OutlineColour, Config.LineWidth + 2 * Config.Outline);
                g.DrawEllipse(outlinePen, this.Coords.X - this.Radius, this.Coords.Y - this.Radius, this.Radius * 2, this.Radius * 2);
            }

            if (Config.LineWidth != 0)
            {
                Pen pen = new Pen(colour, Config.LineWidth);
                g.DrawEllipse(pen, this.Coords.X - this.Radius, this.Coords.Y - this.Radius, this.Radius * 2, this.Radius * 2);
            }
            
        }

        public override bool CheckMouse(Point cursor)
        {
            return (Math.Pow((cursor.X - this.Coords.X), 2) + Math.Pow((cursor.Y - this.Coords.Y), 2) < Math.Pow(this.Radius, 2));
        }

        public override void Scale(double scale)
        {
            this.Radius = (int)(scale * this.BaseRadius);
        }

        private int BaseRadius;
        private int Radius;
    }
}

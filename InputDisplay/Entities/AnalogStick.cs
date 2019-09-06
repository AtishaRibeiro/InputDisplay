using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InputDisplay.Entities
{
    class AnalogStick: BaseEntity
    {
        public AnalogStick(Point coords)
        {
            this.Coords = coords;
            this.StickCoords = coords;
            this.Radius = 43;
            this.StickRadius = (int) (43 * 0.65);
        }

        public void Update(double horizontal, double vertical)
        {
            this.StickCoords.X = (int)(this.Coords.X + (horizontal - 0.5) * this.StickRadius * 1.8);
            this.StickCoords.Y = (int)(this.Coords.Y - ((float)vertical - 0.5) * this.StickRadius * 1.8);
        }

        public override void Draw(ref Graphics g, Color colour)
        {
            if (this.Highlighted)
            {
                Color highlightColour = Color.FromArgb(200, 255 - Config.BackgroundColour.R, 255 - Config.BackgroundColour.G, 255 - Config.BackgroundColour.B);
                Pen outlinePen = new Pen(highlightColour, (Config.LineWidth + 2 * Config.Outline) + 8);
                g.DrawOctagon(outlinePen, new Point(this.Coords.X, this.Coords.Y), this.Radius);
            }
            if (Config.UseOutline)
            {
                Pen outlinePen = new Pen(Config.OutlineColour, Config.LineWidth + 2 * Config.Outline);
                g.DrawOctagon(outlinePen, new Point(this.Coords.X, this.Coords.Y), this.Radius);
            }

            Pen pen = new Pen(colour, Config.LineWidth);
            g.DrawOctagon(pen, new Point(this.Coords.X, this.Coords.Y), this.Radius);

            SolidBrush analogFill = new SolidBrush(Config.BackgroundColour);
            g.FillEllipse(analogFill, this.StickCoords.X - this.StickRadius, this.StickCoords.Y - this.StickRadius, this.StickRadius * 2, this.StickRadius * 2);

            if (Config.UseOutline)
            {
                Pen outlinePen = new Pen(Config.OutlineColour, Config.LineWidth + 2 * Config.Outline);
                g.DrawEllipse(outlinePen, this.StickCoords.X - this.StickRadius, this.StickCoords.Y - this.StickRadius, this.StickRadius * 2, this.StickRadius * 2);
            }

            g.DrawEllipse(pen, this.StickCoords.X - this.StickRadius, this.StickCoords.Y - this.StickRadius, this.StickRadius * 2, this.StickRadius * 2);
        }

        public override bool CheckMouse(Point cursor)
        {
            return CustomShapes.CreateOctagon(new Point(this.Coords.X, this.Coords.Y), this.Radius).IsVisible(cursor);
        }

        public override void Translate(Point vector)
        {
            this.Coords.X += vector.X;
            this.Coords.Y += vector.Y;
            this.StickCoords.X += vector.X;
            this.StickCoords.Y += vector.Y;
        }

        public override void Scale(double scale)
        {
            this.Radius = (int)(43 * scale);
            this.StickRadius = (int)(43 * 0.65 * scale);
        }

        private Point StickCoords;
        private int Radius;
        private int StickRadius;
    }
}

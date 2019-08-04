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
        public AnalogStick(int x, int y)
        {
            this.Coords = (x, y);
            this.StickCoords = (x, y);
            this.Radius = 43;
            this.StickRadius = (int) (43 * 0.65);
        }

        public void Update(double horizontal, double vertical)
        {
            this.StickCoords = ((int) (this.Coords.x + (horizontal - 0.5) * this.StickRadius * 1.6), (int) (this.Coords.y - ((float)vertical - 0.5) * this.StickRadius * 1.6));
        }

        public override void Draw(ref Graphics g, Color colour)
        {
            if (this.Highlighted)
            {
                Color highlightColour = Color.FromArgb(200, 255 - Config.BackgroundColour.R, 255 - Config.BackgroundColour.G, 255 - Config.BackgroundColour.B);
                Pen outlinePen = new Pen(highlightColour, (Config.LineWidth + 2 * Config.Outline) + 8);
                g.DrawOctagon(outlinePen, new Point(this.Coords.x, this.Coords.y), this.Radius);
            }
            if (Config.UseOutline)
            {
                Pen outlinePen = new Pen(Config.OutlineColour, Config.LineWidth + 2 * Config.Outline);
                g.DrawOctagon(outlinePen, new Point(this.Coords.x, this.Coords.y), this.Radius);
            }

            Pen pen = new Pen(colour, Config.LineWidth);
            g.DrawOctagon(pen, new Point(this.Coords.x, this.Coords.y), this.Radius);

            SolidBrush analogFill = new SolidBrush(Config.BackgroundColour);
            g.FillEllipse(analogFill, this.StickCoords.x - this.StickRadius, this.StickCoords.y - this.StickRadius, this.StickRadius * 2, this.StickRadius * 2);

            if (Config.UseOutline)
            {
                Pen outlinePen = new Pen(Config.OutlineColour, Config.LineWidth + 2 * Config.Outline);
                g.DrawEllipse(outlinePen, this.StickCoords.x - this.StickRadius, this.StickCoords.y - this.StickRadius, this.StickRadius * 2, this.StickRadius * 2);
            }

            g.DrawEllipse(pen, this.StickCoords.x - this.StickRadius, this.StickCoords.y - this.StickRadius, this.StickRadius * 2, this.StickRadius * 2);
        }

        public override bool CheckMouse(Point cursor)
        {
            return CustomShapes.CreateOctagon(new Point(this.Coords.x, this.Coords.y), this.Radius).IsVisible(cursor);
        }

        public override void Translate((int x, int y) coords)
        {
            this.Coords = (this.Coords.x + coords.x, this.Coords.y + coords.y);
            this.StickCoords = (this.StickCoords.x + coords.x, this.StickCoords.y + coords.y);
        }

        public override void Scale(double scale)
        {
            this.Radius = (int)(43 * scale);
            this.StickRadius = (int)(43 * 0.65 * scale);
        }

        private (int x, int y) StickCoords;
        private int Radius;
        private int StickRadius;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InputDisplay
{
    class AnalogStick
    {
        public AnalogStick(int x, int y, int radius)
        {
            this.Coords = (x, y);
            this.StickCoords = (x, y);
            this.Radius = radius;
            this.StickRadius = (int) (radius * 0.6);
        }

        public void Update(double horizontal, double vertical)
        {
            this.StickCoords = ((int) (this.Coords.x + (horizontal - 0.5) * this.StickRadius * 1.6), (int) (this.Coords.y - ((float)vertical - 0.5) * this.StickRadius * 1.6));
        }

        public void Draw(ref Graphics g, Color colour)
        {
            Pen pen = new Pen(colour, Config.LineWidth);
            //g.DrawPolygon(pen, this.OctagonPoints);
            g.DrawOctagon(pen, new Point(this.Coords.x, this.Coords.y), this.Radius);
            //SolidBrush blackBrush = new SolidBrush(Color.Gray);
            //g.FillPolygon(blackBrush, this.OctagonPoints);
            SolidBrush brush = new SolidBrush(Config.BackgroundColour);
            g.FillEllipse(brush, this.StickCoords.x - this.StickRadius, this.StickCoords.y - this.StickRadius, this.StickRadius * 2, this.StickRadius * 2);
            g.DrawEllipse(pen, this.StickCoords.x - this.StickRadius, this.StickCoords.y - this.StickRadius, this.StickRadius * 2, this.StickRadius * 2);
        }

        public bool CheckMouse(Point cursor)
        {
            return CustomShapes.CreateOctagon(new Point(this.Coords.x, this.Coords.y), this.Radius).IsVisible(cursor);
        }

        public void Translate((int x, int y) coords)
        {
            this.Coords = (this.Coords.x + coords.x, this.Coords.y + coords.y);
            this.StickCoords = (this.StickCoords.x + coords.x, this.StickCoords.y + coords.y);
        }

        private (int x, int y) Coords;
        private (int x, int y) StickCoords;
        private int Radius;
        private int StickRadius;
    }
}

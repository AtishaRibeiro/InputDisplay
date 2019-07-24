using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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

            float c = (float) Math.Sqrt(2) / 2;
            List<(float x, float y)> coords = new List<(float x, float y)> { (1, 0), (c, c), (0, 1), (-c, c), (-1, 0), (-c, -c), (0, -1), (c, -c) };
            List<PointF> octagonPointsList = new List<PointF>();
            for (int i = 0; i < coords.Count; ++i)
            {
                octagonPointsList.Add(new PointF(radius * coords[i].x + x, radius * coords[i].y + y));
            }
            this.OctagonPoints = octagonPointsList.ToArray();
        }

        public void Update(double horizontal, double vertical)
        {
            this.StickCoords = ((int) (this.Coords.x + (horizontal - 0.5) * this.StickRadius * 1.6), (int) (this.Coords.y - ((float)vertical - 0.5) * this.StickRadius * 1.6));
        }

        public void Draw(ref Graphics g)
        {
            Pen pen = new Pen(Config.ButtonColour, Config.LineWidth);
            g.DrawPolygon(pen, this.OctagonPoints);
            //SolidBrush blackBrush = new SolidBrush(Color.Gray);
            //g.FillPolygon(blackBrush, this.OctagonPoints);
            SolidBrush brush = new SolidBrush(Config.BackgroundColour);
            g.FillEllipse(brush, this.StickCoords.x - this.StickRadius, this.StickCoords.y - this.StickRadius, this.StickRadius * 2, this.StickRadius * 2);
            g.DrawEllipse(pen, this.StickCoords.x - this.StickRadius, this.StickCoords.y - this.StickRadius, this.StickRadius * 2, this.StickRadius * 2);
        }

        private (int x, int y) Coords;
        private (int x, int y) StickCoords;
        private int Radius;
        private int StickRadius;
        private PointF[] OctagonPoints;
    }
}

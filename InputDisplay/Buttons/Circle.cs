using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace InputDisplay.Buttons
{
    class Circle: FaceButton
    {
        public Circle(int x, int y): base(x, y)
        {
            this.Radius = 20;
        }

        public override void Draw(ref Graphics g, Color colour)
        {
            Pen pen = new Pen(colour, Config.LineWidth);
            SolidBrush brush = new SolidBrush(colour);

            g.DrawEllipse(pen, this.Coords.x - this.Radius, this.Coords.y - this.Radius, this.Radius * 2, this.Radius * 2);

            if (this.Pressed)
            {
                g.FillEllipse(brush, this.Coords.x - this.Radius, this.Coords.y - this.Radius, this.Radius * 2, this.Radius * 2);
            }

        }

        public override bool CheckMouse(Point cursor)
        {
            return (Math.Pow((cursor.X - this.Coords.x), 2) + Math.Pow((cursor.Y - this.Coords.y), 2) < Math.Pow(this.Radius, 2));
        }

        public override void Scale(double scale)
        {
            this.Radius = (int)(scale * 20.0);
        }

        private int Radius;
    }
}

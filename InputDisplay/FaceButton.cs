using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InputDisplay
{
    class FaceButton
    {

        public FaceButton(int x, int y, String type)
        {
            this.Coords = (x, y);
            this.Type = type;
        }

        public void Update(bool pressed)
        {
            this.Pressed = pressed;
        }

        public void Draw(ref Graphics g, Color colour)
        {
            Pen pen = new Pen(colour, Config.LineWidth);
            SolidBrush brush = new SolidBrush(colour);

            if (this.Type == "Circle")
            {
                g.DrawEllipse(pen, this.Coords.x - 20, this.Coords.y - 20, 40, 40);

                if (this.Pressed)
                {
                    g.FillEllipse(brush, this.Coords.x - 20, this.Coords.y - 20, 40, 40);
                }
            } else if(this.Type == "Bar")
            {
                g.DrawRoundedRectangle(pen, new Rectangle(new Point(this.Coords.x, this.Coords.y), new Size(80, 20)), 10);

                if (this.Pressed)
                {
                    g.FillRoundedRectangle(brush, new Rectangle(new Point(this.Coords.x, this.Coords.y), new Size(80, 20)), 10);
                }
            }
            
        }

        public bool CheckMouse(Point cursor)
        {
            if(this.Type == "Circle")
            {
                return (Math.Pow((cursor.X - this.Coords.x), 2) + Math.Pow((cursor.Y - this.Coords.y), 2) < Math.Pow(20, 2));
            }

            if (this.Type == "Bar")
            {
                return CustomShapes.RoundedRect(new Rectangle(new Point(this.Coords.x, this.Coords.y), new Size(80, 20)), 10).IsVisible(cursor);
            }
            return false;
        }

        public void Translate((int x, int y) coords)
        {
            this.Coords = (this.Coords.x + coords.x, this.Coords.y + coords.y);
        }

        private (int x, int y) Coords;
        private String Type;
        private bool Pressed = false;
    }
}

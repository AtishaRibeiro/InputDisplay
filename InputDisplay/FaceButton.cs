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

        public void Draw(ref Graphics g)
        {
            Pen pen = new Pen(Config.ButtonColour, 3);
            SolidBrush brush = new SolidBrush(Config.ButtonColour);

            if (this.Type == "Circle")
            {
                g.DrawEllipse(pen, this.Coords.x - 40, this.Coords.y - 40, 40, 40);

                if (this.Pressed)
                {
                    g.FillEllipse(brush, this.Coords.x - 40, this.Coords.y - 40, 40, 40);
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

        private (int x, int y) Coords;
        private String Type;
        private bool Pressed = false;
    }
}

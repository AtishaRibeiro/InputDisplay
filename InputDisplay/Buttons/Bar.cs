using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InputDisplay;
using System.Drawing;

namespace InputDisplay.Buttons
{
    class Bar: FaceButton
    {
        public Bar(int x, int y): base(x, y)
        {
            this.Size = new Size(80, 20);
        }

        public override void Draw(ref Graphics g, Color colour)
        {
            Pen pen = new Pen(colour, Config.LineWidth);
            SolidBrush brush = new SolidBrush(colour);

            g.DrawRoundedRectangle(pen, new Rectangle(new Point(this.Coords.x, this.Coords.y), this.Size), 10);

            if (this.Pressed)
            {
                g.FillRoundedRectangle(brush, new Rectangle(new Point(this.Coords.x, this.Coords.y), this.Size), 10);
            }

        }

        public override bool CheckMouse(Point cursor)
        {
            return CustomShapes.RoundedRect(new Rectangle(new Point(this.Coords.x, this.Coords.y), this.Size), 10).IsVisible(cursor);
        }

        public override void Scale(double scale)
        {
            this.Size.Width = (int) (scale * 80);
            this.Size.Height = (int) (scale * 20);
        }

        private Size Size;
    }
}

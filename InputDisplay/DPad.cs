using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace InputDisplay
{
    class DPad
    {
        public DPad(int x, int y, int size)
        {
            this.Coords = (x, y);
            this.Size = size;
        }

        public void Update(int direction)
        {
            this.CurrentDirection = direction;
        }

        public void Draw(ref Graphics g, Color colour)
        {
            if (this.CurrentDirection != 0)
            {
                SolidBrush brush = new SolidBrush(colour);
                g.FillPointDirection(brush, new Point(Coords.x + this.Size / 2, Coords.y + this.Size / 2), 20, this.Size, 1, this.CurrentDirection);
            }

            Pen pen = new Pen(colour, Config.LineWidth);
            g.DrawPlus(pen, new Rectangle(new Point(Coords.x, Coords.y), new Size(this.Size, this.Size)), 20, 1);
        }
        public bool CheckMouse(Point cursor)
        {
            return CustomShapes.Plus(new Rectangle(new Point(Coords.x, Coords.y), new Size(this.Size, this.Size)), 20, 1).IsVisible(cursor);
        }

        public void Translate((int x, int y) coords)
        {
            this.Coords = (this.Coords.x + coords.x, this.Coords.y + coords.y);
        }

        private (int x, int y) Coords;
        private int Size;
        private int CurrentDirection = 0;
    }
}

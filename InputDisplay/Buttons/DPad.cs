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
            this.ThicknessFraction = 2.0 / 7.0;
        }

        public void Update(int direction)
        {
            this.CurrentDirection = direction;
        }

        public void Draw(ref Graphics g, Color colour)
        {
            int thickness = (int)(this.ThicknessFraction * this.Size);
            if (this.CurrentDirection != 0)
            {
                SolidBrush brush = new SolidBrush(colour);
                g.FillPointDirection(brush, new Point(Coords.x + this.Size / 2, Coords.y + this.Size / 2), thickness, this.Size, 1, this.CurrentDirection);
            }

            Pen pen = new Pen(colour, Config.LineWidth);
            g.DrawPlus(pen, new Rectangle(new Point(Coords.x, Coords.y), new Size(this.Size, this.Size)), thickness, 1);
        }
        public bool CheckMouse(Point cursor)
        {
            int thickness = (int)(this.ThicknessFraction * this.Size);
            return CustomShapes.Plus(new Rectangle(new Point(Coords.x, Coords.y), new Size(this.Size, this.Size)), thickness, 1).IsVisible(cursor);
        }

        public void Translate((int x, int y) coords)
        {
            this.Coords = (this.Coords.x + coords.x, this.Coords.y + coords.y);
        }

        private (int x, int y) Coords;
        private int Size;
        private int CurrentDirection = 0;
        private double ThicknessFraction;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace InputDisplay.Entities
{
    class DPad: BaseEntity
    {
        public DPad(int x, int y)
        {
            this.Coords = (x, y);
            this.Size = 80;
            this.ThicknessFraction = 2.0 / 7.0;
            this.CornerRadius = (int)(0.1 * this.ThicknessFraction * (double)this.Size);
        }

        public void Update(int direction)
        {
            this.CurrentDirection = direction;
            if (direction != 0) {
                this.LastTrick = direction;
            }
            else
            {
                this.CurrentDirection = this.LastTrick;
                this.LastTrick = 0;
            }
        }

        public override void Draw(ref Graphics g, Color colour)
        {
            int thickness = (int)(this.ThicknessFraction * this.Size);
            if (this.CurrentDirection != 0)
            {
                SolidBrush brush = new SolidBrush(colour);
                g.FillPointDirection(brush, new Point(Coords.x + this.Size / 2, Coords.y + this.Size / 2), thickness, this.Size, this.CornerRadius, this.CurrentDirection);
            }

            if (this.Highlighted)
            {
                Color highlightColour = Color.FromArgb(200, 255 - Config.BackgroundColour.R, 255 - Config.BackgroundColour.G, 255 - Config.BackgroundColour.B);
                Pen outlinePen = new Pen(highlightColour, (Config.LineWidth + 2 * Config.Outline) + 8);
                outlinePen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                g.DrawPlus(outlinePen, new Rectangle(new Point(Coords.x, Coords.y), new Size(this.Size, this.Size)), thickness, this.CornerRadius);
            }

            if (Config.UseOutline)
            {
                Pen outlinePen = new Pen(Config.OutlineColour, Config.LineWidth + 2 * Config.Outline);
                g.DrawPlus(outlinePen, new Rectangle(new Point(Coords.x, Coords.y), new Size(this.Size, this.Size)), thickness, this.CornerRadius);
            }

            Pen pen = new Pen(colour, Config.LineWidth);
            g.DrawPlus(pen, new Rectangle(new Point(Coords.x, Coords.y), new Size(this.Size, this.Size)), thickness, this.CornerRadius);
        }

        public override bool CheckMouse(Point cursor)
        {
            int thickness = (int)(this.ThicknessFraction * this.Size);
            return CustomShapes.Plus(new Rectangle(new Point(Coords.x, Coords.y), new Size(this.Size, this.Size)), thickness, this.CornerRadius).IsVisible(cursor);
        }

        public override void Translate((int x, int y) coords)
        {
            this.Coords = (this.Coords.x + coords.x, this.Coords.y + coords.y);
        }

        public override void Scale(double scale)
        {
            this.Size = (int)(80 * scale);
            this.CornerRadius = (int)(0.1 * this.ThicknessFraction * this.Size);
            this.CornerRadius = this.CornerRadius == 0 ? 1 : this.CornerRadius;
        }

        private int Size;
        private int CurrentDirection = 0;
        private double ThicknessFraction;
        private int CornerRadius;
        private int LastTrick = 0;
    }
}

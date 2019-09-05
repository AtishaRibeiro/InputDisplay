using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InputDisplay.Entities
{
    class WiiMote: BaseEntity
    {
        public WiiMote(int x, int y)
        {
            this.Coords = (x, y);
            this.Size = new Size(70, 170);
            this.CornerRadius = (int)(0.1 * (double)this.Size.Width);

            this.Accelerator = new Circle(x + this.Size.Width / 2, y + this.Size.Height / 4, 20);
            this.Drift = new RectangularButton((x + (int)(0.22 * (double)this.Size.Width)), y + (int)(0.50 * (double)this.Size.Height), new Size(40, 60), 0.1);
            this.Trick = new MotionTrick(x, y);

            // 0 when nothing is selected, 1: accelerator, 2: drift, 3: trick, 4: wiimote
            this.Selected = 0;
        }

        public void Update(bool acc, bool drift, int trick)
        {
            this.Accelerator.Update(acc);
            this.Drift.Update(drift);
            this.Trick.Update(trick);
        }

        // not used
        public override void Draw(ref Graphics g, Color colour) {}

        public void Draw(ref Graphics g, Color border, Color acc, Color drift, Color trick)
        {
            if (Config.UseOutline)
            {
                Pen outlinePen = new Pen(Config.OutlineColour, Config.LineWidth + 2 * Config.Outline);
                g.DrawRoundedRectangle(outlinePen, new Rectangle(new Point(this.Coords.x, this.Coords.y), this.Size), this.CornerRadius);
            }

            Pen motePen = new Pen(border, Config.LineWidth);
            g.DrawRoundedRectangle(motePen, new Rectangle(new Point(this.Coords.x, this.Coords.y), this.Size), this.CornerRadius);

            this.Accelerator.Draw(ref g, Config.N_AcceleratorColour);
            this.Drift.Draw(ref g, drift);
            this.Trick.Draw(ref g, trick);
        }

        public override bool CheckMouse(Point cursor)
        {
            this.Selected = 0;

            if (CustomShapes.RoundedRect(new Rectangle(new Point(this.Coords.x, this.Coords.y), this.Size), this.CornerRadius).IsVisible(cursor))
            {
                this.Selected = 4;
            }
            if (this.Accelerator.CheckMouse(cursor))
            {
                this.Selected = 1;
            }
            if (this.Drift.CheckMouse(cursor))
            {
                this.Selected = 2;
            }
            if (this.Trick.CheckMouse(cursor))
            {
                this.Selected = 3;
            }
            return this.Selected != 0;
        }

        public override void Translate((int x, int y) coords)
        {
            this.Coords = (this.Coords.x + coords.x, this.Coords.y + coords.y);
            this.Accelerator.Translate(coords);
            this.Drift.Translate(coords);
            this.Trick.Translate(coords);
        }

        public override void Scale(double scale)
        {
            //wiimote
            this.Size.Width = (int)(scale * 70);
            this.Size.Height = (int)(scale * 170);
            this.CornerRadius = (int)(0.1 * (double)this.Size.Width);

            int x = this.Coords.x;
            int y = this.Coords.y;
            //accelerator
            this.Accelerator.Scale(scale);
            this.Accelerator.Coords = (x + this.Size.Width / 2, y + this.Size.Height / 4);
            //drift
            this.Drift.Scale(scale);
            this.Drift.Coords = ((x + (int)(0.22 * (double)this.Size.Width)), y + (int)(0.50 * (double)(this.Size.Height)));
            //trick
            this.Trick.Scale(scale);
        }

        public void DisplayAllTricks(bool display)
        {
            this.Trick.DisplayAllTricks(display);
        }

        private Size Size;
        private int CornerRadius;
        private int Selected;
        private Circle Accelerator;
        private RectangularButton Drift;
        private MotionTrick Trick;

        Rectangle Mote;

        int TrickSpacing;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InputDisplay.Entities
{
    class WiiMote : BaseEntity
    {
        public WiiMote(Point coords)
        {
            base.Coords = coords;
            // Keep size here as well because the other elements need it as a reference
            // I know this isn't cood coding practice but I just want to finish the project
            this.Size = new Size(70, 170);
            // the wiimote shape is just a rectangular button that never gets pressed
            this.Mote = new RectangularButton(coords, this.Size, 0.1);
            this.Accelerator = new Circle(new Point(coords.X + this.Size.Width / 2, coords.Y + this.Size.Height / 4), 20);
            this.Drift = new RectangularButton(new Point(coords.X + (int)(0.22 * (double)this.Size.Width), coords.Y + (int)(0.50 * (double)this.Size.Height)), new Size(40, 60), 0.1);
            this.Trick = new MotionTrick(coords);

            // 0 when nothing is selected, 1: wiimote, 2: accelerator, 3: drift, 4: trick
            this.Selected = 0;
        }

        public void Update(bool acc, bool drift, int trick)
        {
            this.Accelerator.Update(acc);
            this.Drift.Update(drift);
            this.Trick.Update(trick);
        }

        // not used
        public override void Draw(ref Graphics g, Color colour) { }

        public void Draw(ref Graphics g, Color border, Color acc, Color drift, Color trick)
        {
            this.Mote.Draw(ref g, border);
            this.Accelerator.Draw(ref g, acc);
            this.Drift.Draw(ref g, drift);
            this.Trick.Draw(ref g, trick);
        }

        public override bool CheckMouse(Point cursor)
        {
            this.Selected = 0;

            if (this.Mote.CheckMouse(cursor))
            {
                this.Selected = 1;
            }
            if (this.Accelerator.CheckMouse(cursor))
            {
                this.Selected = 2;
            }
            if (this.Drift.CheckMouse(cursor))
            {
                this.Selected = 3;
            }
            if (this.Trick.CheckMouse(cursor))
            {
                this.Selected = 4;
            }
            return this.Selected != 0;
        }

        public override void Translate(Point vector)
        {
            base.Coords.X += vector.X;
            base.Coords.Y += vector.Y;
            this.Mote.Translate(vector);
            this.Accelerator.Translate(vector);
            this.Drift.Translate(vector);
            this.Trick.Translate(vector);
        }

        public override void Scale(double scale)
        {
            //wiimote
            this.Size.Width = (int)(70 * scale);
            this.Size.Height = (int)(170 * scale);
            this.Mote.Scale(scale);

            int x = this.Mote.Coords.X;
            int y = this.Mote.Coords.Y;
            //accelerator
            this.Accelerator.Scale(scale);
            this.Accelerator.Coords.X = x + this.Size.Width / 2;
            this.Accelerator.Coords.Y = y + this.Size.Height / 4;
            //drift
            this.Drift.Scale(scale);
            this.Drift.Coords.X = x + (int)(0.22 * (double)this.Size.Width);
            this.Drift.Coords.Y = y + (int)(0.50 * (double)(this.Size.Height));
            //trick
            this.Trick.Scale(scale);
        }

        public void DisplayAllTricks(bool display)
        {
            this.Trick.DisplayAllTricks(display);
        }

        public (string, Color, double) GetSelectedInfo()
        {
            switch (this.Selected)
            {
                case 1:
                    return ("Wii Mote", Config.N_WiiMoteColour, Config.N_WiiMoteScale);
                case 2:
                    return ("Accelerator", Config.N_AcceleratorColour, Config.N_WiiMoteScale);
                case 3:
                    return ("Drift", Config.N_DriftColour, Config.N_WiiMoteScale);
                case 4:
                    return ("Tricks", Config.N_TrickColour, Config.N_WiiMoteScale);
                default:
                    return (null, Color.Transparent, 0);
            }
        }

        public void ChangeColour(Color colour)
        {
            switch (this.Selected)
            {
                case 1:
                    Config.N_WiiMoteColour = colour;
                    break;
                case 2:
                    Config.N_AcceleratorColour = colour;
                    break;
                case 3:
                    Config.N_DriftColour = colour;
                    break;
                case 4:
                    Config.N_TrickColour = colour;
                    break;
                default:
                    break;
            }
        }

        public override bool Highlighted
        {
            get => base.Highlighted;
            set {
                base.Highlighted = value;
                //If highlight needs to be turned off
                if (!value)
                {
                    this.Selected = 0;
                }
                this.Mote.Highlighted = this.Selected == 1;
                this.Accelerator.Highlighted = this.Selected == 2;
                this.Drift.Highlighted = this.Selected == 3;
                this.Trick.Highlighted = this.Selected == 4;

            }
        }

        public new Point Coords
        {
            get => base.Coords;
            set
            {
                base.Coords = value;
                this.Mote.Coords = value;
                this.Trick.Coords = value;
                this.Accelerator.Coords = new Point(value.X + this.Size.Width / 2, value.Y + this.Size.Height / 4);
                this.Drift.Coords = new Point(value.X + (int)(0.22 * (double)this.Size.Width), value.Y + (int)(0.50 * (double)this.Size.Height));
            }
        }
        private Size Size;
        private int Selected;
        private RectangularButton Mote;
        private Circle Accelerator;
        private RectangularButton Drift;
        private MotionTrick Trick;
    }
}

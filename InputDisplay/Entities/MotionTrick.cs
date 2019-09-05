using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InputDisplay.Entities
{
    class MotionTrick: BaseEntity
    {
        public MotionTrick(int x, int y)
        {
            this.Coords = (x, y);
            this.Scale(1);
        }

        public void Update(int direction)
        {
            this.CurrentDirection = direction;
        }

        public override void Draw(ref Graphics g, Color colour)
        {
            SolidBrush trickBrush = new SolidBrush(colour);

            if (this.CurrentDirection == 1 || this.DisplayAll)
            {
                g.FillRoundedRectangle(trickBrush, this.TrickUp, 7);
            }
            if (this.CurrentDirection == 2 || this.DisplayAll)
            {
                g.FillRoundedRectangle(trickBrush, this.TrickDown, 7);
            }
            if (this.CurrentDirection == 3 || this.DisplayAll)
            {
                g.FillRoundedRectangle(trickBrush, this.TrickLeft, 7);
            }
            if (this.CurrentDirection == 4 || this.DisplayAll)
            {
                g.FillRoundedRectangle(trickBrush, this.TrickRight, 7);
            }
        }

        public override bool CheckMouse(Point cursor)
        {
            return false;
        }

        public override void Translate((int x, int y) coords)
        {
            this.Coords = (this.Coords.x + coords.x, this.Coords.y + coords.y);
            this.TrickUp.X += coords.x;
            this.TrickUp.Y += coords.y;
            this.TrickDown.X += coords.x;
            this.TrickDown.Y += coords.y;
            this.TrickLeft.X += coords.x;
            this.TrickLeft.Y += coords.y;
            this.TrickRight.X += coords.x;
            this.TrickRight.Y += coords.y;
        }

        public override void Scale(double scale)
        {
            this.MoteSize.Width = (int)(scale * 70);
            this.MoteSize.Height = (int)(scale * 170);
            int x = this.Coords.x;
            int y = this.Coords.y;
            this.Spacing = (int)(10.0 * scale);
            int trickWidth = (int)(15.0 * scale);
            this.TrickUp = new Rectangle(new Point(x - this.Spacing, y - (this.Spacing + trickWidth)), new Size(this.MoteSize.Width + 2 * this.Spacing, trickWidth));
            this.TrickRight = new Rectangle(new Point(x + this.MoteSize.Width + this.Spacing, y - this.Spacing), new Size(trickWidth, this.MoteSize.Height + this.Spacing + trickWidth));
            this.TrickDown = new Rectangle(new Point(x - this.Spacing, y + this.MoteSize.Height + this.Spacing), new Size(this.MoteSize.Width + 2 * this.Spacing, trickWidth));
            this.TrickLeft = new Rectangle(new Point(x - this.Spacing - trickWidth, y - this.Spacing), new Size(trickWidth, this.MoteSize.Height + this.Spacing + trickWidth));
        }

        public void DisplayAllTricks(bool display)
        {
            this.DisplayAll = display;
        }

        private Size MoteSize;

        Rectangle TrickUp;
        Rectangle TrickRight;
        Rectangle TrickDown;
        Rectangle TrickLeft;

        private int CurrentDirection = 0;
        bool DisplayAll = false;

        int Spacing; // amount of space between trick indicator and wiimote
    }
}

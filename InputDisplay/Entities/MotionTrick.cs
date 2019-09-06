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
        public MotionTrick(Point coords)
        {
            this.Coords = coords;
            this.Scale(1);
        }

        public void Update(int direction)
        {
            this.CurrentDirection = direction;
        }

        public override void Draw(ref Graphics g, Color colour)
        {
            if (DisplayAll && this.Highlighted)
            {
                Color highlightColour = Color.FromArgb(200, 255 - Config.BackgroundColour.R, 255 - Config.BackgroundColour.G, 255 - Config.BackgroundColour.B);
                Pen outlinePen = new Pen(highlightColour, (Config.LineWidth + 2 * Config.Outline) + 4);
                g.DrawRoundedRectangle(outlinePen, this.TrickUp, this.CornerRadius);
                g.DrawRoundedRectangle(outlinePen, this.TrickDown, this.CornerRadius);
                g.DrawRoundedRectangle(outlinePen, this.TrickLeft, this.CornerRadius);
                g.DrawRoundedRectangle(outlinePen, this.TrickRight, this.CornerRadius);
            }

            SolidBrush trickBrush = new SolidBrush(colour);

            if (this.CurrentDirection == 1 || this.DisplayAll)
            {
                g.FillRoundedRectangle(trickBrush, this.TrickUp, this.CornerRadius);
            }
            if (this.CurrentDirection == 2 || this.DisplayAll)
            {
                g.FillRoundedRectangle(trickBrush, this.TrickDown, this.CornerRadius);
            }
            if (this.CurrentDirection == 3 || this.DisplayAll)
            {
                g.FillRoundedRectangle(trickBrush, this.TrickLeft, this.CornerRadius);
            }
            if (this.CurrentDirection == 4 || this.DisplayAll)
            {
                g.FillRoundedRectangle(trickBrush, this.TrickRight, this.CornerRadius);
            }
        }

        public override bool CheckMouse(Point cursor)
        {
            if (CustomShapes.RoundedRect(this.TrickUp, this.CornerRadius).IsVisible(cursor))
            {
                return true;
            } else if (CustomShapes.RoundedRect(this.TrickDown, this.CornerRadius).IsVisible(cursor))
            {
                return true;
            } else if (CustomShapes.RoundedRect(this.TrickLeft, this.CornerRadius).IsVisible(cursor))
            {
                return true;
            } else if (CustomShapes.RoundedRect(this.TrickRight, this.CornerRadius).IsVisible(cursor))
            {
                return true;
            }
            return false;
        }

        public override void Translate(Point vector)
        {
            this.Coords.X += vector.X;
            this.Coords.Y += vector.Y;
            this.TrickUp.X += vector.X;
            this.TrickUp.Y += vector.Y;
            this.TrickDown.X += vector.X;
            this.TrickDown.Y += vector.Y;
            this.TrickLeft.X += vector.X;
            this.TrickLeft.Y += vector.Y;
            this.TrickRight.X += vector.X;
            this.TrickRight.Y += vector.Y;
        }

        public override void Scale(double scale)
        {
            this.CornerRadius = (int)(scale * 7.0);
            this.MoteSize.Width = (int)(scale * 70);
            this.MoteSize.Height = (int)(scale * 170);
            int x = this.Coords.X;
            int y = this.Coords.Y;
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
        int CornerRadius = 7;
    }
}

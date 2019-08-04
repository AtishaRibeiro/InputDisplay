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
            /*this.AccCoords = new Point(x + this.Size.Width / 2, this.Coords.y + 40);
            this.AccRadius = 20;
            this.DriftSize = new Size(40, 60);
            this.DriftCoords = new Point((x + this.Size.Width / 2) - (this.DriftSize.Width / 2), y + 90);
            this.CornerRadius = (int) ((1.0 / 10.0) * (double) this.Size.Width);
            this.DriftCornerRadius = (int)((1.0 / 10.0) * 30.0);

            this.TrickSpacing = 10;
            this.TrickUp = new Rectangle(new Point(x - 10, y - 25), new Size(this.Size.Width + 20, 15));
            this.TrickRight = new Rectangle(new Point(x + this.Size.Width + 10, y - 10), new Size(15, this.Size.Height + 25));
            this.TrickDown = new Rectangle(new Point(x - 10, y + this.Size.Height + 10), new Size(this.Size.Width + 20, 15));
            this.TrickLeft = new Rectangle(new Point(x - 25, y - 10), new Size(15, this.Size.Height + 25));*/
            this.Scale(1);
        }

        public void Update(bool acc, bool drift, int trick)
        {
            this.AccPressed = acc;
            this.DriftPressed = drift;
            this.CurrentDirection = trick;
        }

        // not used
        public override void Draw(ref Graphics g, Color colour) {}

        public void Draw(ref Graphics g, Color border, Color acc, Color drift, Color trick)
        {
            Pen motePen = new Pen(border, Config.LineWidth);
            g.DrawRoundedRectangle(motePen, new Rectangle(new Point(this.Coords.x, this.Coords.y), this.Size), this.CornerRadius);

            Pen accPen = new Pen(acc, Config.LineWidth);
            g.DrawEllipse(accPen, this.AccCoords.X - this.AccRadius, this.AccCoords.Y - this.AccRadius, this.AccRadius * 2, this.AccRadius * 2);

            Pen driftPen = new Pen(drift, Config.LineWidth);
            g.DrawRoundedRectangle(driftPen, this.Drift, this.DriftCornerRadius);


            SolidBrush trickBrush = new SolidBrush(Color.FromArgb(255, trick.R, trick.G, trick.B));

            switch (this.CurrentDirection)
            {
                case 1:
                    g.FillRoundedRectangle(trickBrush, this.TrickUp, 7);
                    break;
                case 2:
                    g.FillRoundedRectangle(trickBrush, this.TrickDown, 7);
                    break;
                case 3:
                    g.FillRoundedRectangle(trickBrush, this.TrickLeft, 7);
                    break;
                case 4:
                    g.FillRoundedRectangle(trickBrush, this.TrickRight, 7);
                    break;
                default:
                    break;
            }
        }

        public override bool CheckMouse(Point cursor)
        {
            return CustomShapes.RoundedRect(new Rectangle(new Point(this.Coords.x, this.Coords.y), this.Size), this.CornerRadius).IsVisible(cursor);
        }

        public override void Translate((int x, int y) coords)
        {
            this.Coords = (this.Coords.x + coords.x, this.Coords.y + coords.y);
            this.AccCoords.X += coords.x;
            this.AccCoords.Y += coords.y;
            this.Drift.X += coords.x;
            this.Drift.Y += coords.y;
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
            //wiimote
            this.Size.Width = (int)(scale * 70);
            this.Size.Height = (int)(scale * 170);
            this.CornerRadius = (int)(0.1 * (double)this.Size.Width);

            int x = this.Coords.x;
            int y = this.Coords.y;
            //accelerator
            this.AccRadius = (int)(20.0 * scale);
            this.AccCoords = new Point(x + this.Size.Width / 2, this.Coords.y + 40);
            //drift
            Size driftSize = new Size((int)(40.0 * scale), (int)(60.0 * scale));
            this.DriftCornerRadius = (int)(0.1 * (double)driftSize.Width);
            this.Drift = new Rectangle(new Point((x + this.Size.Width / 2) - (driftSize.Width / 2), y + (int)(0.47 * (double)(this.Size.Height))), driftSize);
            //tricks
            this.TrickSpacing = (int)(10.0 * scale);
            int trickWidth = (int)(15.0 * scale);
            this.TrickUp = new Rectangle(new Point(x - this.TrickSpacing, y - (this.TrickSpacing + trickWidth)), new Size(this.Size.Width + 2 * this.TrickSpacing, trickWidth));
            this.TrickRight = new Rectangle(new Point(x + this.Size.Width + 10, y - 10), new Size(trickWidth, this.Size.Height + this.TrickSpacing + trickWidth));
            this.TrickDown = new Rectangle(new Point(x - this.TrickSpacing, y + this.Size.Height + this.TrickSpacing), new Size(this.Size.Width + 2 * this.TrickSpacing, trickWidth));
            this.TrickLeft = new Rectangle(new Point(x - this.TrickSpacing + trickWidth, y - TrickSpacing), new Size(trickWidth, this.Size.Height + this.TrickSpacing + trickWidth));
        }

        Point AccCoords;
        int AccRadius;
        Size Size;
        int CornerRadius;
        int DriftCornerRadius;

        Rectangle Mote;
        Rectangle Drift;
        Rectangle TrickUp;
        Rectangle TrickRight;
        Rectangle TrickDown;
        Rectangle TrickLeft;

        int TrickSpacing;

        bool AccPressed = false;
        bool DriftPressed = false;
        int CurrentDirection = 0;
    }
}

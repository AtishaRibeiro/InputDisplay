using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using InputDisplay.Core;

namespace InputDisplay.Entities
{
    class AnalogStick: BaseEntity
    {
        public AnalogStick(Point coords, Animator animator)
        {
            this.Coords = coords;
            this.StickCoords = coords;
            this.Radius = 43;
            this.StickRadius = (int) (43 * 0.65);
            this.Animator = animator;
        }

        public void Update(double horizontal, double vertical)
        {
            this.StickCoords.X = (int)(this.Coords.X + (horizontal - 0.5) * this.StickRadius * 1.8);
            this.StickCoords.Y = (int)(this.Coords.Y - ((float)vertical - 0.5) * this.StickRadius * 1.8);
        }

        public override void Draw(ref Graphics g, Color colour)
        {
            // Draw the highlight
            if (this.Highlighted)
            {
                Color highlightColour = Color.FromArgb(200, 255 - Config.BackgroundColour.R, 255 - Config.BackgroundColour.G, 255 - Config.BackgroundColour.B);
                Pen outlinePen = new Pen(highlightColour, (Config.LineWidth + 2 * Config.Outline) + 8);
                g.DrawOctagon(outlinePen, new Point(this.Coords.X, this.Coords.Y), this.Radius);
            }
            // Draw the outline of the octagon
            if (Config.UseOutline)
            {
                Pen outlinePen = new Pen(Config.OutlineColour, Config.LineWidth + 2 * Config.Outline);
                g.DrawOctagon(outlinePen, new Point(this.Coords.X, this.Coords.Y), this.Radius);
            }
            // Draw the ridges of the analog stick (octagon)
            Pen pen = new Pen(colour, Config.LineWidth);
            if (Config.LineWidth != 0)
            {
                g.DrawOctagon(pen, new Point(this.Coords.X, this.Coords.Y), this.Radius);
            }
            // If the background is transparent (for recording) then the custom circle function has to be used
            SolidBrush analogFill = new SolidBrush(Config.BackgroundColour);
            if (Config.BackgroundColour == Color.Transparent)
            {
                this.Animator.OverwriteTransparentCircle(this.StickCoords.X, this.StickCoords.Y, this.StickRadius);
            } else
            {
                g.FillEllipse(analogFill, this.StickCoords.X - this.StickRadius, this.StickCoords.Y - this.StickRadius, this.StickRadius * 2, this.StickRadius * 2);
            }
            // Draw outline of the stick
            if (Config.UseOutline)
            {
                Pen outlinePen = new Pen(Config.OutlineColour, Config.LineWidth + 2 * Config.Outline);
                g.DrawEllipse(outlinePen, this.StickCoords.X - this.StickRadius, this.StickCoords.Y - this.StickRadius, this.StickRadius * 2, this.StickRadius * 2);
            }
            // If the linewidth is 0, don't draw anyhting
            if (Config.LineWidth != 0)
            {
                g.DrawEllipse(pen, this.StickCoords.X - this.StickRadius, this.StickCoords.Y - this.StickRadius, this.StickRadius * 2, this.StickRadius * 2);
            }
        }

        public override bool CheckMouse(Point cursor)
        {
            return CustomShapes.CreateOctagon(new Point(this.Coords.X, this.Coords.Y), this.Radius).IsVisible(cursor);
        }

        public override void Translate(Point vector)
        {
            base.Coords.X += vector.X;
            base.Coords.Y += vector.Y;
            this.StickCoords.X += vector.X;
            this.StickCoords.Y += vector.Y;
        }

        public override void Scale(double scale)
        {
            this.Radius = (int)(43 * scale);
            this.StickRadius = (int)(43 * 0.65 * scale);
        }

        public new Point Coords
        {
            get => base.Coords;
            set
            {
                base.Coords = value;
                this.StickCoords = value;
            }
        }
        private Point StickCoords;
        private int Radius;
        private int StickRadius;
        private Animator Animator;
    }
}

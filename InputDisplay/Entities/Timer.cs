using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;

namespace InputDisplay.Entities
{
    class Timer : BaseEntity
    {

        public Timer(Point coords)
        {
            this.Coords = coords;
            this.Size = new Size(100, 30);
        } 

        public override void Draw(ref Graphics g, Color colour) { }

        public void Draw(ref Graphics g, double currentFrame)
        {
            Font drawFont = new Font("Arial", 16);

            SolidBrush blackBrush = new SolidBrush(Color.Black);
            g.FillRectangle(blackBrush, new Rectangle(new Point(this.Coords.X, this.Coords.Y), this.Size));

            int actualFrame = (int)Math.Floor(currentFrame) - 240;
            double seconds = actualFrame * (1.0 / 60.0);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            g.DrawString(seconds.ToString("0.000", CultureInfo.CreateSpecificCulture("en-CA")), drawFont, whiteBrush, this.Coords.X, this.Coords.Y + 5, new StringFormat());
        }

        public override bool CheckMouse(Point cursor)
        {
            return cursor.X >= this.Coords.X && cursor.X <= this.Coords.Y + this.Size.Width && cursor.Y >= this.Coords.Y && cursor.Y <= this.Coords.Y + this.Size.Height;
        }

        public override void Translate(Point vector)
        {
            this.Coords.X += vector.X;
            this.Coords.Y += vector.Y;
        }

        public override void Scale(double scale)
        {
            //doesn't do anything
        }

        private Size Size;
    }
}

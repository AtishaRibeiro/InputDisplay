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

        public Timer()
        {
            this.Coords = (100, 195);
            this.Size = new Size(100, 30);
        } 

        public override void Draw(ref Graphics g, Color colour) { }

        public void Draw(ref Graphics g, double currentFrame)
        {
            Font drawFont = new Font("Arial", 16);

            SolidBrush blackBrush = new SolidBrush(Color.Black);
            g.FillRectangle(blackBrush, new Rectangle(new Point(this.Coords.x, this.Coords.y), this.Size));

            int actualFrame = (int)Math.Floor(currentFrame) - 240;
            double seconds = actualFrame * (1.0 / 60.0);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            g.DrawString(seconds.ToString("0.000", CultureInfo.CreateSpecificCulture("en-CA")), drawFont, whiteBrush, 100, 200, new StringFormat());
        }

        public override bool CheckMouse(Point cursor)
        {
            return cursor.X >= this.Coords.x && cursor.X <= this.Coords.x + this.Size.Width && cursor.Y >= this.Coords.y && cursor.Y <= this.Coords.y + this.Size.Height;
        }

        public override void Translate((int x, int y) coords)
        {

        }

        public override void Scale(double scale)
        {

        }

        private Size Size;
    }
}

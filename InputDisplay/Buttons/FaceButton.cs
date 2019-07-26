using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InputDisplay.Buttons
{
    abstract class FaceButton
    {

        public FaceButton(int x, int y)
        {
            this.Coords = (x, y);
        }

        public void Update(bool pressed)
        {
            this.Pressed = pressed;
        }

        public abstract void Draw(ref Graphics g, Color colour);

        public abstract bool CheckMouse(Point cursor);

        public void Translate((int x, int y) coords)
        {
            this.Coords = (this.Coords.x + coords.x, this.Coords.y + coords.y);
        }

        public abstract void Scale(double scale);

        protected (int x, int y) Coords;
        protected bool Pressed = false;
    }
}

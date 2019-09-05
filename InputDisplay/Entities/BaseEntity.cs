using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InputDisplay.Entities
{
    abstract class BaseEntity
    {

        public abstract void Draw(ref Graphics g, Color colour);

        public abstract bool CheckMouse(Point cursor);

        public abstract void Translate((int x, int y) coords);

        public abstract void Scale(double scale);

        public (int x, int y) Coords { get; set; }
        public bool Highlighted { get; set; } = false;
    }
}

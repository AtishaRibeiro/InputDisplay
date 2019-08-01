using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InputDisplay.Entities
{
    abstract class FaceButton: BaseEntity
    {

        public FaceButton(int x, int y)
        {
            this.Coords = (x, y);
        }

        public virtual void Update(bool pressed)
        {
            this.Pressed = pressed;
        }

        public override void Translate((int x, int y) coords)
        {
            this.Coords = (this.Coords.x + coords.x, this.Coords.y + coords.y);
        }

        protected bool Pressed = false;
    }
}

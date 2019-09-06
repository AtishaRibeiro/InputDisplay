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

        public FaceButton(Point coords)
        {
            this.Coords = coords;
        }

        public virtual void Update(bool pressed)
        {
            this.Pressed = pressed;
        }

        public override void Translate(Point vector)
        {
            this.Coords.X += vector.X;
            this.Coords.Y += vector.Y;
        }

        protected bool Pressed = false;
    }
}

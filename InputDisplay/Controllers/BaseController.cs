using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace InputDisplay.Controllers
{
    abstract class BaseController
    {

        public abstract void Clear();

        public abstract void Update(bool acc, bool drift, bool item, (double x, double y) pos, int trick);

        public abstract void Draw(ref Graphics g);

        public abstract (string, Color, double) EvaluateCursor(Point cursor);

        public abstract void Highlight();

        public abstract void MoveShapes(int xChange, int yChange);

        public abstract void Scale(double scale);

        public abstract void ChangeColour(Color colour);
    }
}

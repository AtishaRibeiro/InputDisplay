using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using InputDisplay.Entities;

namespace InputDisplay.Controllers
{
    class Classic: BaseController
    {
        public Classic()
        {
            this.AnalogStick = new AnalogStick(150, 100);
            this.Accelerator = new Circle(230, 100);
            this.Drift = new Bar(170, 20);
            this.Item = new Bar(20, 20);
            this.DPad = new DPad(20, 65);
        }

        public override void Clear()
        {
            this.Accelerator.Update(false);
            this.Drift.Update(false);
            this.Item.Update(false);
            this.AnalogStick.Update(0.5, 0.5);
            this.DPad.Update(0);
        }

        public override void Update(bool acc, bool drift, bool item, (double x, double y) pos, int trick)
        {
            this.Accelerator.Update(acc);
            this.Drift.Update(drift);
            this.Item.Update(item);
            this.AnalogStick.Update(pos.x, pos.y);
            this.DPad.Update(trick);
        }

        public override void Draw(ref Graphics g)
        {
            if (Config.C_CustomColours)
            {
                this.AnalogStick.Draw(ref g, Config.C_DirectionalColour);
                this.DPad.Draw(ref g, Config.C_DPadColour);
                this.Drift.Draw(ref g, Config.C_DriftColour);
                this.Item.Draw(ref g, Config.C_ItemColour);
                this.Accelerator.Draw(ref g, Config.C_AcceleratorColour);
            }
            else
            {
                Color colour = Config.ButtonColour;
                this.AnalogStick.Draw(ref g, colour);
                this.DPad.Draw(ref g, colour);
                this.Drift.Draw(ref g, colour);
                this.Item.Draw(ref g, colour);
                this.Accelerator.Draw(ref g, colour);
            }
        }

        public override (string, Color, double) EvaluateCursor(Point cursor)
        {
            this.MoveAcc = false;
            this.MoveDrift = false;
            this.MoveItem = false;
            this.MoveDPad = false;
            this.MoveAnalog = false;
            if (this.Accelerator.CheckMouse(cursor))
            {
                this.MoveAcc = true;
                return ("Accelerate", Config.C_AcceleratorColour, Config.C_AcceleratorScale);
            } else if (this.Drift.CheckMouse(cursor))
            {
                this.MoveDrift = true;
                return ("Drift", Config.C_DriftColour, Config.C_DriftScale);
            } else if (this.Item.CheckMouse(cursor))
            {
                this.MoveItem = true;
                return ("Item", Config.C_ItemColour, Config.C_ItemScale);
            } else if (this.DPad.CheckMouse(cursor))
            {
                this.MoveDPad = true;
                return ("D-Pad", Config.C_DPadColour, Config.C_DPadScale);
            } else if (this.AnalogStick.CheckMouse(cursor))
            {
                this.MoveAnalog = true;
                return ("Analog Stick", Config.C_DirectionalColour, Config.C_DirectionalScale);
            }
            return (null, Color.Transparent, 0);
        }

        public override void Highlight()
        {
            this.Accelerator.Highlighted = this.MoveAcc;
            this.Drift.Highlighted = this.MoveDrift;
            this.Item.Highlighted = this.MoveItem;
            this.DPad.Highlighted = this.MoveDPad;
            this.AnalogStick.Highlighted = this.MoveAnalog;
        }

        public override void MoveShapes(int xChange, int yChange)
        {
            if (this.MoveAcc) { this.Accelerator.Translate((xChange, yChange)); }
            if (this.MoveDrift) { this.Drift.Translate((xChange, yChange)); }
            if (this.MoveItem) { this.Item.Translate((xChange, yChange)); }
            if (this.MoveAnalog) { this.AnalogStick.Translate((xChange, yChange)); }
            if (this.MoveDPad) { this.DPad.Translate((xChange, yChange)); }
        }

        public override void Scale(double scale)
        {
            if (this.MoveAcc) {
                Config.C_AcceleratorScale = scale;
                this.Accelerator.Scale(scale);
            }
            if (this.MoveDrift) {
                Config.C_DriftScale = scale;
                this.Drift.Scale(scale);
            }
            if (this.MoveItem) {
                Config.C_ItemScale = scale;
                this.Item.Scale(scale);
            }
            if (this.MoveAnalog) {
                Config.C_DirectionalScale = scale;
                this.AnalogStick.Scale(scale);
            }
            if (this.MoveDPad) {
                Config.C_DPadScale = scale;
                this.DPad.Scale(scale);
            }
        }

        public override void ChangeColour(Color colour)
        {
            if (this.MoveAcc) { Config.C_AcceleratorColour = colour; }
            if (this.MoveDrift) { Config.C_DriftColour = colour; }
            if (this.MoveItem) { Config.C_ItemColour = colour; }
            if (this.MoveAnalog) { Config.C_DirectionalColour = colour; }
            if (this.MoveDPad) { Config.C_DPadColour = colour; }
        }

        private AnalogStick AnalogStick;
        private Circle Accelerator;
        private Bar Drift;
        private Bar Item;
        private DPad DPad;

        private bool MoveAcc = false;
        private bool MoveDrift = false;
        private bool MoveItem = false;
        private bool MoveAnalog = false;
        private bool MoveDPad = false;
    }
}

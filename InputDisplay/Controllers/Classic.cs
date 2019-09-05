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
            this.AnalogStick = new AnalogStick(221, 150);
            this.Accelerator = new Circle(310, 150, 23);
            this.Drift = new RectangularButton(250, 60, new Size(90, 23), 0.5);
            this.Item = new RectangularButton(80, 60, new Size(90, 23), 0.5);
            this.DPad = new DPad(76, 110);
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
            if (Config.CustomColours)
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
            this.SelectAcc = false;
            this.SelectDrift = false;
            this.SelectItem = false;
            this.SelectDPad = false;
            this.SelectAnalog = false;
            if (this.Accelerator.CheckMouse(cursor))
            {
                this.SelectAcc = true;
                return ("Accelerate", Config.C_AcceleratorColour, Config.C_AcceleratorScale);
            } else if (this.Drift.CheckMouse(cursor))
            {
                this.SelectDrift = true;
                return ("Drift", Config.C_DriftColour, Config.C_DriftScale);
            } else if (this.Item.CheckMouse(cursor))
            {
                this.SelectItem = true;
                return ("Item", Config.C_ItemColour, Config.C_ItemScale);
            } else if (this.DPad.CheckMouse(cursor))
            {
                this.SelectDPad = true;
                return ("D-Pad", Config.C_DPadColour, Config.C_DPadScale);
            } else if (this.AnalogStick.CheckMouse(cursor))
            {
                this.SelectAnalog = true;
                return ("Analog Stick", Config.C_DirectionalColour, Config.C_DirectionalScale);
            }
            return (null, Color.Transparent, 0);
        }

        public override void Highlight()
        {
            this.Accelerator.Highlighted = this.SelectAcc;
            this.Drift.Highlighted = this.SelectDrift;
            this.Item.Highlighted = this.SelectItem;
            this.DPad.Highlighted = this.SelectDPad;
            this.AnalogStick.Highlighted = this.SelectAnalog;
        }

        public override void MoveShapes(int xChange, int yChange)
        {
            if (this.SelectAcc) { this.Accelerator.Translate((xChange, yChange)); }
            if (this.SelectDrift) { this.Drift.Translate((xChange, yChange)); }
            if (this.SelectItem) { this.Item.Translate((xChange, yChange)); }
            if (this.SelectAnalog) { this.AnalogStick.Translate((xChange, yChange)); }
            if (this.SelectDPad) { this.DPad.Translate((xChange, yChange)); }
        }

        public override void Scale(double scale)
        {
            if (this.SelectAcc) {
                Config.C_AcceleratorScale = scale;
                this.Accelerator.Scale(scale);
            }
            if (this.SelectDrift) {
                Config.C_DriftScale = scale;
                this.Drift.Scale(scale);
            }
            if (this.SelectItem) {
                Config.C_ItemScale = scale;
                this.Item.Scale(scale);
            }
            if (this.SelectAnalog) {
                Config.C_DirectionalScale = scale;
                this.AnalogStick.Scale(scale);
            }
            if (this.SelectDPad) {
                Config.C_DPadScale = scale;
                this.DPad.Scale(scale);
            }
        }

        public override void ChangeColour(Color colour)
        {
            if (this.SelectAcc) { Config.C_AcceleratorColour = colour; }
            if (this.SelectDrift) { Config.C_DriftColour = colour; }
            if (this.SelectItem) { Config.C_ItemColour = colour; }
            if (this.SelectAnalog) { Config.C_DirectionalColour = colour; }
            if (this.SelectDPad) { Config.C_DPadColour = colour; }
        }

        public override void SetEditMode(bool edit)
        {
            // doesn't do anything
        }

        private AnalogStick AnalogStick;
        private Circle Accelerator;
        private RectangularButton Drift;
        private RectangularButton Item;
        private DPad DPad;

        private bool SelectAcc = false;
        private bool SelectDrift = false;
        private bool SelectItem = false;
        private bool SelectAnalog = false;
        private bool SelectDPad = false;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using InputDisplay.Entities;
using InputDisplay.Core;

namespace InputDisplay.Controllers
{
    class Classic: BaseController
    {
        public Classic(Animator animator)
        {
            this.AnalogStick = new AnalogStick(Config.C_DirectionalPos, animator);
            this.Accelerator = new Circle(Config.C_AcceleratorPos, 23);
            this.Drift = new RectangularButton(Config.C_DriftPos, new Size(90, 23), 0.5);
            this.Item = new RectangularButton(Config.C_ItemPos, new Size(90, 23), 0.5);
            this.DPad = new DPad(Config.C_DPadPos);

            this.Accelerator.Scale(Config.C_AcceleratorScale);
            this.Drift.Scale(Config.C_DriftScale);
            this.Item.Scale(Config.C_ItemScale);
            this.AnalogStick.Scale(Config.C_DirectionalScale);
            this.DPad.Scale(Config.C_DPadScale);
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

        public override void ResetSizePosition()
        {
            Config.C_AcceleratorPos = new Point(320, 150);
            Config.C_DirectionalPos = new Point(230, 150);
            Config.C_DriftPos = new Point(260, 60);
            Config.C_ItemPos = new Point(90, 60);
            Config.C_DPadPos = new Point(85, 110);
            this.AnalogStick.Coords = Config.C_DirectionalPos;
            this.Accelerator.Coords = Config.C_AcceleratorPos;
            this.Drift.Coords = Config.C_DriftPos;
            this.Item.Coords = Config.C_ItemPos;
            this.DPad.Coords = Config.C_DPadPos;

            Config.C_AcceleratorScale = 1;
            Config.C_DirectionalScale = 1;
            Config.C_DriftScale = 1;
            Config.C_ItemScale = 1;
            Config.C_DPadScale = 1;
            this.AnalogStick.Scale(1);
            this.Accelerator.Scale(Config.C_AcceleratorScale);
            this.Drift.Scale(Config.C_DriftScale);
            this.Item.Scale(Config.C_ItemScale);
            this.AnalogStick.Scale(Config.C_DirectionalScale);
            this.DPad.Scale(Config.C_DPadScale);
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

        public override void MoveShapes(Point changeVector)
        {
            if (this.SelectAcc) {
                this.Accelerator.Translate(changeVector);
                Config.C_AcceleratorPos = this.Accelerator.Coords;
            }
            if (this.SelectDrift) {
                this.Drift.Translate(changeVector);
                Config.C_DriftPos = this.Drift.Coords;
            }
            if (this.SelectItem) {
                this.Item.Translate(changeVector);
                Config.C_ItemPos = this.Item.Coords;
            }
            if (this.SelectAnalog) {
                this.AnalogStick.Translate(changeVector);
                Config.C_DirectionalPos = this.AnalogStick.Coords;
            }
            if (this.SelectDPad) {
                this.DPad.Translate(changeVector);
                Config.C_DPadPos = this.DPad.Coords;
            }
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
            if (!edit)
            {
                this.SelectAcc = false;
                this.SelectDrift = false;
                this.SelectItem = false;
                this.SelectAnalog = false;
                this.SelectDPad = false;
                this.Highlight();
            }
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

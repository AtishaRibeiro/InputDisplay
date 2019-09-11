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
    class Nunchuck : BaseController
    {

        public Nunchuck(Animator animator)
        {
            this.AnalogStick = new AnalogStick(Config.N_DirectionalPos, animator);
            this.Item = new RectangularButton(Config.N_ItemPos, new Size(90, 23), 0.5);
            this.Mote = new WiiMote(Config.N_WiiMotePos);

            this.Mote.Scale(Config.N_WiiMoteScale);
            this.Item.Scale(Config.N_ItemScale);
            this.AnalogStick.Scale(Config.N_DirectionalScale);
        }

        public override void Clear()
        {
            this.Item.Update(false);
            this.AnalogStick.Update(0.5, 0.5);
            this.Mote.Update(false, false, 0);
        }

        public override void Update(bool acc, bool drift, bool item, (double x, double y) pos, int trick)
        {
            this.Item.Update(item);
            this.AnalogStick.Update(pos.x, pos.y);
            this.Mote.Update(acc, drift, trick);
        }

        public override void ResetSizePosition()
        {
            Config.N_DirectionalPos = new Point(145, 160);
            Config.N_ItemPos = new Point(100, 70);
            Config.N_WiiMotePos = new Point(240, 50);
            this.AnalogStick.Coords = Config.N_DirectionalPos;
            this.Item.Coords = Config.N_ItemPos;
            this.Mote.Coords = Config.N_WiiMotePos;

            Config.N_DirectionalScale = 1;
            Config.N_ItemScale = 1;
            Config.N_WiiMoteScale = 1;
            this.Item.Scale(Config.N_ItemScale);
            this.AnalogStick.Scale(Config.N_DirectionalScale);
            this.Mote.Scale(Config.N_WiiMoteScale);
        }

        public override void Draw(ref Graphics g)
        {
            if (Config.CustomColours)
            {
                this.AnalogStick.Draw(ref g, Config.N_DirectionalColour);
                this.Item.Draw(ref g, Config.N_ItemColour);
                this.Mote.Draw(ref g, Config.N_WiiMoteColour, Config.N_AcceleratorColour, Config.N_DriftColour, Config.N_TrickColour);
            }
            else
            {
                Color colour = Config.ButtonColour;
                this.AnalogStick.Draw(ref g, colour);
                this.Item.Draw(ref g, colour);
                this.Mote.Draw(ref g, colour, colour, colour, colour);
            }
        }

        public override (string, Color, double) EvaluateCursor(Point cursor)
        {
            this.SelectItem = false;
            this.SelectAnalog = false;
            this.SelectMote = false;
            if (this.Item.CheckMouse(cursor))
            {
                this.SelectItem = true;
                return ("Item", Config.N_ItemColour, Config.N_ItemScale);
            }
            else if (this.AnalogStick.CheckMouse(cursor))
            {
                this.SelectAnalog = true;
                return ("Analog Stick", Config.N_DirectionalColour, Config.N_DirectionalScale);
            } else if (this.Mote.CheckMouse(cursor))
            {
                this.SelectMote = true;
                return this.Mote.GetSelectedInfo();
            }
            return (null, Color.Transparent, 0);
        }

        public override void Highlight()
        {
            this.Item.Highlighted = this.SelectItem;
            this.AnalogStick.Highlighted = this.SelectAnalog;
            this.Mote.Highlighted = this.SelectMote;
        }

        public override void MoveShapes(Point changeVector)
        {
            if (this.SelectItem) {
                this.Item.Translate(changeVector);
                Config.N_ItemPos = this.Item.Coords;
            }
            if (this.SelectAnalog) {
                this.AnalogStick.Translate(changeVector);
                Config.N_DirectionalPos = this.AnalogStick.Coords;
            }
            if (this.SelectMote) {
                this.Mote.Translate(changeVector);
                Config.N_WiiMotePos = this.Mote.Coords;
                Console.WriteLine(Config.N_WiiMotePos);
            }
        }

        public override void Scale(double scale)
        {
            if (this.SelectItem)
            {
                Config.N_ItemScale = scale;
                this.Item.Scale(scale);
            }
            if (this.SelectAnalog)
            {
                Config.N_DirectionalScale = scale;
                this.AnalogStick.Scale(scale);
            }
            if (this.SelectMote)
            {
                Config.N_WiiMoteScale = scale;
                this.Mote.Scale(scale);
            }
        }

        public override void ChangeColour(Color colour)
        {
            if (this.SelectItem) { Config.N_ItemColour = colour; }
            if (this.SelectAnalog) { Config.N_DirectionalColour = colour; }
            if (this.SelectMote) { this.Mote.ChangeColour(colour); }
        }

        public override void SetEditMode(bool edit)
        {
            // True if the customise tab is selected, used to show the trick inputs on the wiimote so they can be selected
            this.Mote.DisplayAllTricks(edit);
            if (!edit)
            {
                this.SelectItem = false;
                this.SelectAnalog = false;
                this.SelectMote = false;
                this.Highlight();
            }
        }

        private AnalogStick AnalogStick;
        private RectangularButton Item;
        private WiiMote Mote;

        private bool SelectItem = false;
        private bool SelectAnalog = false;
        private bool SelectMote = false;
    }

}

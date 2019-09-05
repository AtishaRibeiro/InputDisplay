using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using InputDisplay.Entities;

namespace InputDisplay.Controllers
{
    class Nunchuck : BaseController
    {

        public Nunchuck()
        {
            this.AnalogStick = new AnalogStick(145, 160);
            this.Item = new RectangularButton(100, 70, new Size(90, 23), 0.5);
            this.Mote = new WiiMote(250, 50);
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
                return ("Item", Config.C_ItemColour, Config.C_ItemScale);
            }
            else if (this.AnalogStick.CheckMouse(cursor))
            {
                this.SelectAnalog = true;
                return ("Analog Stick", Config.C_DirectionalColour, Config.C_DirectionalScale);
            } else if (this.Mote.CheckMouse(cursor))
            {
                this.SelectMote = true;
                return ("Wii Mote", Config.N_AcceleratorColour, Config.N_WiiMoteScale);
            }
            return (null, Color.Transparent, 0);
        }

        public override void Highlight()
        {
            this.Item.Highlighted = this.SelectItem;
            this.AnalogStick.Highlighted = this.SelectAnalog;
        }

        public override void MoveShapes(int xChange, int yChange)
        {
            if (this.SelectItem) { this.Item.Translate((xChange, yChange)); }
            if (this.SelectAnalog) { this.AnalogStick.Translate((xChange, yChange)); }
            if (this.SelectMote) { this.Mote.Translate((xChange, yChange)); }
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
            if (this.SelectItem) { Config.C_ItemColour = colour; }
            if (this.SelectAnalog) { Config.C_DirectionalColour = colour; }
        }

        public override void SetEditMode(bool edit)
        {
            // True if the customise tab is selected, used to show the trick inputs on the wiimote so they can be selected
            this.Mote.DisplayAllTricks(edit);
        }

        private AnalogStick AnalogStick;
        private RectangularButton Item;
        private WiiMote Mote;

        private bool SelectItem = false;
        private bool SelectAnalog = false;
        private bool SelectMote = false;
    }

}

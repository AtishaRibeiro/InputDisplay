using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;

namespace InputDisplay
{
    class Animator
    {

        public Animator(int fps) {
            this.Fps = fps;
            this.AnalogStick = new AnalogStick(150, 100, 40);
            this.Accelerator = new FaceButton(250, 120, "Circle");
            this.Drift = new FaceButton(170, 20, "Bar");
            this.Item = new FaceButton(20, 20, "Bar");
            this.Dpad = new DPad(20, 65, 70);
        }

        public void ReadFile(String FileName)
        {
            this.GhostReader.ReadFile(FileName);
        }

        public void Clear()
        {
            this.FaceIndex = 0;
            this.AnalogIndex = 0;
            this.TrickIndex = 0;
            this.CurrentFrame = 0;

            this.Accelerator.Update(false);
            this.Drift.Update(false);
            this.Item.Update(false);
            this.AnalogStick.Update(0.5, 0.5);
            this.Dpad.Update(0);
        }

        public void Update()
        {
            int roundedFrame = (int) Math.Floor(this.CurrentFrame);

            if (roundedFrame >= this.GhostReader.Face_inputs[this.FaceIndex].endFrame)
            {
                this.FaceIndex += 1;
            }

            if (roundedFrame >= this.GhostReader.Analog_inputs[this.AnalogIndex].endFrame)
            {
                this.AnalogIndex += 1;
            }

            if (roundedFrame >= this.GhostReader.Trick_inputs[this.TrickIndex].endFrame)
            {
                this.TrickIndex += 1;
            }

            (bool accelerator, bool drift, bool item) actions = this.GhostReader.Face_inputs[this.FaceIndex].values;
            this.Accelerator.Update(actions.accelerator);
            this.Drift.Update(actions.drift);
            this.Item.Update(actions.item);

            (double x, double y) coords = this.GhostReader.Analog_inputs[this.AnalogIndex].values;
            this.AnalogStick.Update(coords.x, coords.y);

            int trick = this.GhostReader.Trick_inputs[this.TrickIndex].values;
            this.Dpad.Update(trick);

            this.CurrentFrame += 60.0 / this.Fps;
        }

        public void Draw(ref Graphics g)
        {
            Font drawFont = new Font("Arial", 16);

            if (Config.DisplayTimer)
            {
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                g.FillRectangle(blackBrush, new Rectangle(new Point(100, 195), new Size(70, 30)));

                int actualFrame = 0;
                if (this.CurrentFrame < 240)
                {
                    actualFrame = 240 - (int)Math.Floor(this.CurrentFrame);
                }
                else
                {
                    actualFrame = (int)this.CurrentFrame - 240;
                }
                double seconds = actualFrame * (1.0 / 60.0);
                SolidBrush whiteBrush = new SolidBrush(Color.White);
                g.DrawString(seconds.ToString("0.000", CultureInfo.CreateSpecificCulture("en-CA")), drawFont, whiteBrush, 100, 200, new StringFormat());
            }
            

            this.Accelerator.Draw(ref g);
            this.AnalogStick.Draw(ref g);
            this.Drift.Draw(ref g);
            this.Item.Draw(ref g);
            this.Dpad.Draw(ref g);
        }

        GhostReader GhostReader = new GhostReader();

        private int FaceIndex = 0;
        private int AnalogIndex = 0;
        private int TrickIndex = 0;

        private AnalogStick AnalogStick;
        private FaceButton Accelerator;
        private FaceButton Drift;
        private FaceButton Item;
        private DPad Dpad;

        private int Fps;
        private double CurrentFrame = 0;
    }
}

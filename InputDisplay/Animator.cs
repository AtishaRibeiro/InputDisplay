using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;
using InputDisplay.Buttons;

namespace InputDisplay
{
    class Animator
    {

        public Animator(int fps) {
            this.Fps = fps;
            this.AnalogStick = new AnalogStick(150, 100);
            this.Accelerator = new Circle(230, 100);
            this.Drift = new Bar(170, 20);
            this.Item = new Bar(20, 20);
            this.Dpad = new DPad(20, 65);
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

        public bool Update()
        {
            int roundedFrame = (int) Math.Floor(this.CurrentFrame);

            //end of inputs reached
            if (roundedFrame >= this.GhostReader.TotalFrames)
            {
                return false;
            }

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

            this.CurrentFrame += (double) Config.PlaybackSpeed / this.Fps;
            return true;
        }

        public void Draw(ref Graphics g)
        {
            Font drawFont = new Font("Arial", 16);

            if (Config.DisplayTimer)
            {
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                g.FillRectangle(blackBrush, new Rectangle(new Point(100, 195), new Size(100, 30)));

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

            if (Config.CustomColours)
            {
                this.Accelerator.Draw(ref g, Config.AcceleratorColour);
                this.AnalogStick.Draw(ref g, Config.DirectionalColour);
                this.Drift.Draw(ref g, Config.DriftColour);
                this.Item.Draw(ref g, Config.ItemColour);
                this.Dpad.Draw(ref g, Config.DPadColour);
            }
            else
            {
                Color colour = Config.ButtonColour;
                this.Accelerator.Draw(ref g, colour);
                this.AnalogStick.Draw(ref g, colour);
                this.Drift.Draw(ref g, colour);
                this.Item.Draw(ref g, colour);
                this.Dpad.Draw(ref g, colour);
            }
        }

        public (string, string) GetGhostInfo()
        {
            return (this.GhostReader.CompletionTime, this.GhostReader.MiiName);
        }

        public bool EvaluateCursor(Point cursor)
        {
            this.MousePos = cursor;

            this.MoveAcc = this.Accelerator.CheckMouse(cursor);
            this.MoveDrift = this.Drift.CheckMouse(cursor);
            this.MoveItem = this.Item.CheckMouse(cursor);
            this.MoveAnalog = this.AnalogStick.CheckMouse(cursor);
            this.MoveDPad = this.Dpad.CheckMouse(cursor);
            return (this.MoveAcc || this.MoveDrift || this.MoveItem || this.MoveAnalog || this.MoveDPad);
        }

        public void MoveShapes(Point cursor)
        {
            int xChange = cursor.X - this.MousePos.X;
            int yChange = cursor.Y - this.MousePos.Y;

            if (this.MoveAcc) { this.Accelerator.Translate((xChange, yChange)); }
            if (this.MoveDrift) { this.Drift.Translate((xChange, yChange)); }
            if (this.MoveItem) { this.Item.Translate((xChange, yChange)); }
            if (this.MoveAnalog) { this.AnalogStick.Translate((xChange, yChange)); }
            if (this.MoveDPad) { this.Dpad.Translate((xChange, yChange)); }

            this.MousePos = cursor;
        }

        private GhostReader GhostReader = new GhostReader();

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

        private Point MousePos;
        private bool MoveAcc = false;
        private bool MoveDrift = false;
        private bool MoveItem = false;
        private bool MoveAnalog = false;
        private bool MoveDPad = false;

        public void ScaleAccelerator(double scale)
        {
            this.Accelerator.Scale(scale);
        }

        public void ScaleDrift(double scale)
        {
            this.Drift.Scale(scale);
        }

        public void ScaleItem(double scale)
        {
            this.Item.Scale(scale);
        }

        public void ScaleDir(double scale)
        {
            this.AnalogStick.Scale(scale);
        }

        public void ScaleDPad(double scale)
        {
            this.Dpad.Scale(scale);
        }
    }
}

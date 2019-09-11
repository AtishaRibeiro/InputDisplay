using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using InputDisplay.Entities;
using InputDisplay.Controllers;

namespace InputDisplay.Core
{
    public class Animator
    {

        public Animator(double fps, int width, int height) {
            this.Fps = fps;
            this.Controller = new Classic(this);
            this.Timer = new Timer(new Point(322, 245));
            this.bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
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

            this.Controller.Clear();
        }

        public void ResetSizePosition()
        {
            this.Controller.ResetSizePosition();
        }

        public void SwitchController(string controller)
        {
            switch (controller)
            {
                case "Classic/GCN":
                    this.Controller = new Classic(this);
                    break;
                case "Nunchuck":
                    this.Controller = new Nunchuck(this);
                    break;
            }
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
            (double x, double y) coords = this.GhostReader.Analog_inputs[this.AnalogIndex].values;
            int trick = this.GhostReader.Trick_inputs[this.TrickIndex].values;

            this.Controller.Update(actions.accelerator, actions.drift, actions.item, coords, trick);

            this.CurrentFrame += (double) Config.PlaybackSpeed / this.Fps;
            return true;
        }

        public void DrawFrame()
        {
            Graphics g = Graphics.FromImage(this.bmp);
            g.Clear(Config.BackgroundColour);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.Draw(ref g);
        }

        public void Draw(ref Graphics g)
        {
            if (Config.DisplayTimer) { this.Timer.Draw(ref g, this.CurrentFrame); }

            this.Controller.Draw(ref g);
        }

        public (string, string, string, int) GetGhostInfo()
        {
            return (this.GhostReader.CompletionTime, this.GhostReader.MiiName, this.GhostReader.Controller_type, this.GhostReader.TotalFrames);
        }

        public (string, Color, double) EvaluateCursor(Point cursor)
        {
            this.MousePos = cursor;
            this.TimerMove = this.Timer.CheckMouse(cursor);
            // controller has to be checked before return in case the timer is pressed, so the highlight can go away
            (String, Color, double) controlRet = this.Controller.EvaluateCursor(cursor);
            if (this.TimerMove) { return ("Timer", Color.Transparent, 0); }
            else { return controlRet; }
        }

        public void Highlight()
        {
            this.Controller.Highlight();
        }

        public void MoveShapes(Point cursor)
        {
            int xChange = cursor.X - this.MousePos.X;
            int yChange = cursor.Y - this.MousePos.Y;
            Point changeVector = new Point(xChange, yChange);
            if (this.TimerMove) { this.Timer.Translate(changeVector); }
            else { this.Controller.MoveShapes(changeVector); }
            this.MousePos = cursor;
        }

        public void Scale(double scale)
        {
            this.Controller.Scale(scale);
        }

        public void ChangeColour(Color colour)
        {
            this.Controller.ChangeColour(colour);
        }

        public void SetEditMode(bool edit)
        {
            this.Controller.SetEditMode(edit);
        }

        public Bitmap bmp { get; set; }

        private GhostReader GhostReader = new GhostReader();
        private BaseController Controller;
        private Timer Timer;

        private int FaceIndex = 0;
        private int AnalogIndex = 0;
        private int TrickIndex = 0;
        public double Fps;
        private double CurrentFrame = 0;

        private Point MousePos;
        private bool TimerMove = false;

        //
        // The functions below are for the AnalogStick class
        //
        public void OverwriteTransparentLine(int x0, int y0, int x1, int y1)
        {
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;
            for (; ; )
            {
                this.bmp.SetPixel(x0, y0, Color.Transparent);
                if (x0 == x1 && y0 == y1) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }
        }

        public void OverwriteTransparentCircle( int x_centre, int y_centre, int r)
        {
            // Force every pixel within a circle to have full transparancy

            int x = r, y = 0;

            // The 2 points between which a line will be drawn
            Point p0, p1;
            Graphics g = Graphics.FromImage(this.bmp);
            Pen pen = new Pen(Color.White, 1);

            p0 = new Point(x + x_centre, y + y_centre);
            p1 = p0;

            // When radius is zero only a single point will be drawn
            if (r > 0)
            {
                p1 = new Point(x + x_centre, -y + y_centre);
                this.OverwriteTransparentLine(p0.X, p0.Y, p1.X, p1.Y);
                p0 = new Point(y + x_centre, x + y_centre);
                p1 = new Point(-y + x_centre, x + y_centre);
            }

            this.OverwriteTransparentLine(p0.X, p0.Y, p1.X, p1.Y);

            // Initialising the value of P 
            int P = 1 - r;
            while (x > y)
            {
                y++;

                // Mid-point is inside or on the perimeter 
                if (P <= 0)
                    P = P + 2 * y + 1;
                // Mid-point is outside the perimeter 
                else
                {
                    x--;
                    P = P + 2 * y - 2 * x + 1;
                }

                // All the perimeter points have already been drawn 
                if (x < y)
                    break;

                // Drawing the generated point and its reflection 
                // in the other octants after translation 
                p0 = new Point(x + x_centre, y + y_centre);
                p1 = new Point(-x + x_centre, y + y_centre);
                this.OverwriteTransparentLine(p0.X, p0.Y, p1.X, p1.Y);
                p0 = new Point(x + x_centre, -y + y_centre);
                p1 = new Point(-x + x_centre, -y + y_centre);
                this.OverwriteTransparentLine(p0.X, p0.Y, p1.X, p1.Y);

                // If the generated point is on the line x = y then  
                // the perimeter points have already been drawn
                if (x != y)
                {
                    p0 = new Point(y + x_centre, x + y_centre);
                    p1 = new Point(-y + x_centre, x + y_centre);
                    this.OverwriteTransparentLine(p0.X, p0.Y, p1.X, p1.Y);
                    p0 = new Point(y + x_centre, -x + y_centre);
                    p1 = new Point(-y + x_centre, -x + y_centre);
                    this.OverwriteTransparentLine(p0.X, p0.Y, p1.X, p1.Y);
                }
            }

            // Draw the last line (which doesn't get drawn by the algorithm and I don't know why)
            // Use width 2 because it doesn't work otherwise, this is confusing
            p0 = new Point(x_centre - r, y_centre);
            p1 = new Point(x_centre + r, y_centre);
            this.OverwriteTransparentLine(p0.X, p0.Y, p1.X, p1.Y);
            p0 = new Point(x_centre - r, y_centre + 1);
            p1 = new Point(x_centre + r, y_centre + 1);
            this.OverwriteTransparentLine(p0.X, p0.Y, p1.X, p1.Y);
        }
    }
}

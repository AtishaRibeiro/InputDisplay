using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.Diagnostics;
using System.IO;
using System.ComponentModel;

namespace InputDisplay
{
    public partial class Form1: Form
    {
        private int xChange = 0;
        private int yChange = 0;

        //
        // Variable Setup
        //

        private void VariableSetup()
        {
            this.pictureBox1.BackColor = Config.BackgroundColour;
            this.numericUpDown1.Value = new decimal(new int[] {Config.LineWidth, 0, 0, 0});
            this.numericUpDown4.Value = new decimal(new int[] {Config.Outline, 0, 0, 0 });
            this.button6.BackColor = Config.OutlineColour;
            this.button1.BackColor = Config.BackgroundColour;
            this.button2.Enabled = !Config.CustomColours;
            this.ButtonColour.Enabled = Config.CustomColours;
            this.button2.BackColor = Config.ButtonColour;
            this.LayoutBox.SelectedIndex = 0;
            this.checkBox1.Checked = Config.CustomColours;
            this.recordButton.Enabled = false;
            this.progressBar1.Visible = false;
            this.ProgressBarText.Visible = false;
          
            //events
            this.button1.Click += new EventHandler(Button1_Click);
            this.button2.Click += new EventHandler(Button2_Click);
            this.button6.Click += new EventHandler(Button6_Click);
            this.button7.Click += new EventHandler(Button7_Click);
            this.ButtonColour.Click += new EventHandler(ButtonColour_Click);
            this.ButtonSlide.ValueChanged += new EventHandler(ButtonSlide_ValueChanged);
            this.LayoutBox.SelectedValueChanged += new EventHandler(LayoutBox_SelectedValueChanged);
            this.tabControl1.SelectedIndexChanged += new EventHandler(TabControl1_SelectedIndexChanged);
            this.checkBox1.CheckedChanged += new EventHandler(Checkbox1_CheckedChange);
            this.recordButton.Click += new EventHandler(RecordButton_Click);
            this.linkLabel1.Click += new EventHandler(LinkLabel1_Click);
        }

        //
        // General Tab
        //

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Config.LineWidth = (int)this.numericUpDown1.Value;
            this.pictureBox1.Invalidate();
        }

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Config.PlaybackSpeed = (int)this.numericUpDown2.Value;
        }

        private void NumericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            Config.Outline = (int)this.numericUpDown4.Value;
            Config.UseOutline = Config.Outline != 0;
            this.pictureBox1.Invalidate();
        }

        private void LayoutBox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.Animator.SwitchController(this.LayoutBox.Text);
            this.pictureBox1.Invalidate();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // true if the newly selected tab is the customise tab
            if (this.tabControl1.SelectedIndex == 1)
            {
                this.Animator.SetEditMode(true);
            } else
            {
                this.Animator.SetEditMode(false);
                this.CurrentButton.Text = null;
                this.ButtonSlide.Value = 10;
                this.ButtonScale.Text = null;
                this.ButtonColour.BackColor = Color.Transparent;
                this.groupBox3.Enabled = false;
            }

            // true if the newly selected tab is the record tab
            if (this.tabControl1.SelectedIndex == 2)
            {
                this.timer.Stop();
                // emulate pressing the stop button
                this.Button4_Click(null, null);
                this.button3.Enabled = false;
                this.button4.Enabled = false;
                this.pictureBox1.Visible = false;
                this.progressBar1.Value = 0;
                this.progressBar1.Visible = true;
            }
            else
            {
                this.timer.Stop();
                this.timer = new AccurateTimer(this, new Action(TimerCallback), 16);
                this.pictureBox1.Visible = true;
                this.progressBar1.Visible = false;
                this.ProgressBarText.Visible = false;
                if (this.GhostLoaded)
                {
                    this.button3.Enabled = true;
                    this.button4.Enabled = true;
                }
            }

            this.pictureBox1.Invalidate();
        }

        //
        // Colours
        //

        private void Button1_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.button1.BackColor = this.colorDialog1.Color;
                this.pictureBox1.BackColor = this.button1.BackColor;
                Config.BackgroundColour = this.button1.BackColor;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.button2.BackColor = this.colorDialog1.Color;
                Config.ButtonColour = this.button2.BackColor;
                this.pictureBox1.Invalidate();
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.button6.BackColor = this.colorDialog1.Color;
                Config.OutlineColour = this.button6.BackColor;
                this.pictureBox1.Invalidate();
            }
        }

        //
        // Customise Tab
        //

        private bool Dragging = false;
        private Point MousePos;

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.tabControl1.SelectedTab == this.CustomiseTab)
            {
                Point pos = this.pictureBox1.PointToClient(Cursor.Position);
                Cursor.Current = Cursors.Hand;
                this.Dragging = this.Animator.EvaluateCursor(pos).Item1 != null;
                this.Animator.Highlight();
                this.pictureBox1.Invalidate();
            }
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.tabControl1.SelectedTab == this.CustomiseTab)
            {
                this.Dragging = false;
                Cursor.Current = Cursors.Default;

                Point pos = this.pictureBox1.PointToClient(Cursor.Position);
                (string name, Color colour, double scale) = this.Animator.EvaluateCursor(pos);
                this.CurrentButton.Text = name;
                this.ButtonColour.BackColor = colour;
                if (scale == 0)
                {
                    this.ButtonSlide.Value = 10;
                    this.ButtonScale.Text = null;
                }
                else
                {
                    this.ButtonSlide.Value = (int)(10 * scale);
                    this.ButtonScale.Text = Convert.ToString(scale);
                }
                this.groupBox3.Enabled = name != null && name != "Timer";
                //this.Animator.EvaluateCursor(this.pictureBox1.PointToClient(Cursor.Position));
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Dragging)
            {
                bool shift = (ModifierKeys & Keys.Shift) != 0;
                int xChange = Cursor.Position.X - this.MousePos.X;
                int yChange = Cursor.Position.Y - this.MousePos.Y;

                // grid based movement implementation
                if (shift)
                {
                    this.xChange += xChange;
                    int xmo5 = xChange / 5;
                    if (xmo5 != 0) {
                        this.xChange = xChange % 5;
                        xChange -= this.xChange;
                    } else
                    {
                        xChange = 0;
                    }
                    this.yChange += yChange;
                    int ymo5 = yChange / 5;
                    if (ymo5 != 0)
                    {
                        this.yChange = yChange % 5;
                        yChange -= this.yChange;
                    }
                    else
                    {
                        yChange = 0;
                    }
                }
                this.MousePos = new Point(this.MousePos.X + xChange, this.MousePos.Y + yChange);
                this.Animator.MoveShapes(this.pictureBox1.PointToClient(this.MousePos));
                this.pictureBox1.Invalidate();
                //Cursor.Position = this.MousePos;
            }
        }

        private void Checkbox1_CheckedChange(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                Config.CustomColours = true;
                this.button2.Enabled = false;
                this.ButtonColour.Enabled = true;
            } else
            {
                Config.CustomColours = false;
                this.button2.Enabled = true;
                this.ButtonColour.Enabled = false;
            }
            this.pictureBox1.Invalidate();
        } 

        private void ButtonColour_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ButtonColour.BackColor = this.colorDialog1.Color;
                this.Animator.ChangeColour(this.colorDialog1.Color);
                this.pictureBox1.Invalidate();
            }
        }

        private void ButtonSlide_ValueChanged(object sender, EventArgs e)
        {
            double scale = this.ButtonSlide.Value * 0.1;
            this.Animator.Scale(scale);
            this.pictureBox1.Invalidate();
            this.ButtonScale.Text = Convert.ToString(scale);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            this.Animator.ResetSizePosition();
        }

        //
        // About Tab
        //

        private void LinkLabel1_Click(object sender, EventArgs e)
        {
            this.linkLabel1.LinkVisited = true;
            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UCkbRo8h8Fy7lbgBBpKdurCw");
        }

        //
        // Record Tab
        //

        private void RecordButton_Click(object sender, EventArgs e)
        {
            this.ProgressBarText.Text = "Processing...\nThis can take a few minutes";
            this.ProgressBarText.Visible = true;
            // disable controls
            foreach (Control c in this.Controls)
            {
                c.Enabled = false;
            }

            string path = "temp";
            DirectoryInfo di;
            if (!Directory.Exists(path))
            {
                di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            } else
            {
                di = new DirectoryInfo("temp");
                // empty the temp directory
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
            }

            // prepare the animator
            this.Animator.Clear();
            this.Animate = true;
            Color savedColour = Config.BackgroundColour;
            if (this.checkBox2.Checked)
            {
                Config.BackgroundColour = Color.Transparent;
            }
            
            // fill the progress bar just a little bit to let the user know that the process is working
            this.progressBar1.Value = 25;

            // start rendering
            this.Render();

            // re-enable control
            foreach (Control c in this.Controls)
            {
                c.Enabled = true;
            }

            Config.BackgroundColour = savedColour;
            this.ProgressBarText.Text = "Done";
        }

        private void Render()
        {
            // prepare the animator and write all the frames to the temp directory
            int frameNr = 0;
            //int totalFrames = this.Animator.GetGhostInfo().Item4;
            while (this.AdvanceAnimator())
            {
                bmp.Save(String.Format("temp\\{0}.png", frameNr), System.Drawing.Imaging.ImageFormat.Png);
                /*if (frameNr % 100 == 0)
                {
                    this.progressBar1.Value = 25 + (frameNr / totalFrames) * 50;
                }*/
                ++frameNr;
            }
            this.progressBar1.Value = 50;
            this.Animate = false;

            // compile all the frames into a video using png encoding to preserve transparent background
            Process proc = new Process();
            proc.StartInfo.FileName = "ffmpeg\\ffmpeg";
            proc.StartInfo.Arguments = String.Format("-framerate 62.5 -i temp\\%d.png -vcodec png {0}.avi", DateTime.Now.ToString("yy-MM-dd-hh-mm-ss"));
            //proc.StartInfo.Arguments = "-framerate 62.5 -i temp\\%d.png -vcodec png output.avi";
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            if (!proc.Start())
            {
                Console.WriteLine("Error starting");
                return;
            }

            // extract info from the console to know how far we are in the compilation process
            StreamReader reader = proc.StandardError;
            string line;
            char[] delimChar = { ' ' };
            while ((line = reader.ReadLine()) != null)
            {
                string[] words = line.Split(delimChar);
                if (words[0] == "frame=")
                {
                    int i = 1;
                    while (words[i] == "") { ++i; }
                    int currentFrame = Int32.Parse(words[i]);
                    //backgroundWorker1.ReportProgress(75 + (currentFrame / totalFrames) * 25);
                    //this.progressBar1.Value = 75 + (currentFrame / totalFrames) * 25;
                }
            }

            this.progressBar1.Value = 100;
            proc.Close();
            return;
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // The progress percentage is a property of e
            this.progressBar1.Value = e.ProgressPercentage;

            // Disable all controls, this has to be done here because every progressbar update seems to reset the controls
            foreach (Control c in this.Controls)
            {
                c.Enabled = false;
            }
            this.ProgressBarText.Visible = true;
        }
    }
}

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

namespace InputDisplay.Forms
{
    public partial class MainForm: Form
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
            this.ButtonColour.Enabled = Config.CustomColours;
            this.button2.BackColor = Config.ButtonColour;
            this.LayoutBox.SelectedIndex = 0;
            this.checkBox1.Checked = Config.CustomColours;
            this.FileFormatBox.SelectedIndex = 0;
            this.recordButton.Enabled = false;
          
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
            // The original playback speed is 59.94(60/1.001) fps
            // do this weird calculation to properly allow up to 3x speed
            Config.PlaybackSpeed = 60 * ((double)this.numericUpDown2.Value / 1.001 / 100);
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
            } else
            {
                this.timer.Stop();
                this.timer = new AccurateTimer(this, new Action(TimerCallback), 16);
                this.pictureBox1.Visible = true;
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
            }
        }

        private void Checkbox1_CheckedChange(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                Config.CustomColours = true;
                this.ButtonColour.Enabled = true;
            } else
            {
                Config.CustomColours = false;
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
        // Record Tab
        //

        private void RecordButton_Click(object sender, EventArgs e)
        {
            RecordForm recordForm = new RecordForm(this, this.Animator, this.checkBox2.Checked, this.FileFormatBox.Text.ToLower());
            recordForm.Show();
            recordForm.Start();
        }

        //
        // About Tab
        //

        private void LinkLabel1_Click(object sender, EventArgs e)
        {
            this.linkLabel1.LinkVisited = true;
            // Navigate to a URL.
            Process.Start("https://www.youtube.com/channel/UCkbRo8h8Fy7lbgBBpKdurCw");
        }

        
    }
}

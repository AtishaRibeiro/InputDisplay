using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

namespace InputDisplay
{
    public partial class Form1: Form
    {
        //
        // Variable Setup
        //

        private void VariableSetup()
        {
            this.pictureBox1.BackColor = Config.BackgroundColour;
            this.numericUpDown1.Value = new decimal(new int[] {Config.LineWidth, 0, 0, 0});
            this.numericUpDown4.Value = new decimal(new int[] {Config.Outline, 0, 0, 0 });
            this.checkBox1.Checked = Config.DisplayTimer;
            this.button6.BackColor = Config.OutlineColour;
            this.button1.BackColor = Config.BackgroundColour;
            this.button2.BackColor = Config.ButtonColour;

            //events
            this.button1.Click += new EventHandler(Button1_Click);
            this.button2.Click += new EventHandler(Button2_Click);
            this.button6.Click += new EventHandler(Button6_Click);
            this.ButtonColour.Click += new EventHandler(ButtonColour_Click);
            this.ButtonSlide.ValueChanged += new EventHandler(ButtonSlide_ValueChanged);
            this.LayoutBox.SelectedValueChanged += new EventHandler(LayoutBox_SelectedValueChanged);
        }

        //
        // General Tab
        //

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            Config.DisplayTimer = this.checkBox1.Checked;
            this.pictureBox1.Invalidate();
        }

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
            }
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
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
            } else
            {
                this.ButtonSlide.Value = (int)(10 * scale);
                this.ButtonScale.Text = Convert.ToString(scale);
            }
            this.groupBox3.Enabled = name != null && name != "Timer";

            this.Animator.Highlight();
            this.pictureBox1.Invalidate();
            //this.Animator.EvaluateCursor(this.pictureBox1.PointToClient(Cursor.Position));
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Dragging)
            {
                bool shift = (ModifierKeys & Keys.Shift) != 0;
                int xChange = Cursor.Position.X - this.MousePos.X;
                int yChange = Cursor.Position.Y - this.MousePos.Y;
                if (shift)
                {
                    if (xChange != 0) { xChange = xChange > 0 ? 1 : -1; }
                    if (yChange != 0) { yChange = yChange > 0 ? 1 : -1; }
                }
                this.MousePos = new Point(this.MousePos.X + xChange, this.MousePos.Y + yChange);
                this.Animator.MoveShapes(this.pictureBox1.PointToClient(this.MousePos));
                this.pictureBox1.Invalidate();
                Cursor.Position = this.MousePos;
            }
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
    }
}

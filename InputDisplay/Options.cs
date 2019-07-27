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

        //
        // Colour Tab
        //

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked)
            {
                this.groupBox3.Enabled = true;
                Config.CustomColours = true;
                this.button2.Enabled = false;
            }
            else
            {
                this.groupBox3.Enabled = false;
                Config.CustomColours = false;
                this.button2.Enabled = true;
            }
            this.pictureBox1.Invalidate();
        }

        private void AccColButton_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.AccColButton.BackColor = this.colorDialog1.Color;
                Config.AcceleratorColour = this.colorDialog1.Color;
            }
            this.pictureBox1.Invalidate();
        }

        private void DriftColButton_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.DriftColButton.BackColor = this.colorDialog1.Color;
                Config.DriftColour = this.colorDialog1.Color;
            }
            this.pictureBox1.Invalidate();
        }

        private void ItemColButton_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ItemColButton.BackColor = this.colorDialog1.Color;
                Config.ItemColour = this.colorDialog1.Color;
            }
            this.pictureBox1.Invalidate();
        }

        private void DirColButton_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.DirColButton.BackColor = this.colorDialog1.Color;
                Config.DirectionalColour = this.colorDialog1.Color;
            }
            this.pictureBox1.Invalidate();
        }

        private void DPadColButton_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.DPadColButton.BackColor = this.colorDialog1.Color;
                Config.DPadColour = this.colorDialog1.Color;
            }
            this.pictureBox1.Invalidate();
        }

        //
        // Transform Tab
        //

        private bool Dragging = false;
        private Point MousePos;

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.tabControl1.SelectedTab == this.TransformTab)
            {
                Point pos = this.pictureBox1.PointToClient(Cursor.Position);
                Cursor.Current = Cursors.Hand;
                this.Dragging = this.Animator.EvaluateCursor(pos);
            }
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Dragging = false;
            Cursor.Current = Cursors.Default;
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

        private void AccSlide_ValueChanged(object sender, EventArgs e)
        {
            double scale = this.AccSlide.Value * 0.1;
            this.Animator.ScaleAccelerator(scale);
            this.pictureBox1.Invalidate();
            this.AccScale.Text = Convert.ToString(scale);
        }

        private void DriftSlide_ValueChanged(object sender, EventArgs e)
        {
            double scale = this.DriftSlide.Value * 0.1;
            this.Animator.ScaleDrift(scale);
            this.pictureBox1.Invalidate();
            this.DriftScale.Text = Convert.ToString(scale);
        }

        private void ItemSlide_ValueChanged(object sender, EventArgs e)
        {
            double scale = this.ItemSlide.Value * 0.1;
            this.Animator.ScaleItem(scale);
            this.pictureBox1.Invalidate();
            this.ItemScale.Text = Convert.ToString(scale);
        }

        private void DirSlide_ValueChanged(object sender, EventArgs e)
        {
            double scale = this.DirSlide.Value * 0.1;
            this.Animator.ScaleDir(scale);
            this.pictureBox1.Invalidate();
            this.DirScale.Text = Convert.ToString(scale);
        }

        private void DPadSlide_ValueChanged(object sender, EventArgs e)
        {
            double scale = this.DPadSlide.Value * 0.1;
            this.Animator.ScaleDPad(scale);
            this.pictureBox1.Invalidate();
            this.DPadScale.Text = Convert.ToString(scale);
        }
    }
}

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
            Point pos = this.pictureBox1.PointToClient(Cursor.Position);
            Cursor.Current = Cursors.Hand;
            this.Dragging = this.Animator.EvaluateCursor(pos);
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

        private void AccScale_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double scale;
                if (double.TryParse(this.AccScale.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out scale))
                {
                    this.Animator.ScaleAccelerator(scale);
                    Config.AcceleratorScale = scale;
                    this.pictureBox1.Invalidate();
                } else
                {
                    this.AccScale.Text = Convert.ToString(Config.AcceleratorScale);
                }
                
            }
        }

        private void AccScale_Leave(object sender, EventArgs e)
        {
            double scale = Convert.ToDouble(this.AccScale.Text, CultureInfo.InvariantCulture);
            this.Animator.ScaleAccelerator(scale);
            this.pictureBox1.Invalidate();
        }
    }
}

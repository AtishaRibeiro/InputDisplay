using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputDisplay
{
    public partial class AdvancedWindow : Form
    {
        Form1 f1;

        public AdvancedWindow(Form1 parent)
        {
            InitializeComponent();
            f1 = parent;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.groupBox2.Enabled = true;
                Config.CustomColours = true;
                this.f1.SwitchButtonColourButton();
            } else
            {
                this.groupBox2.Enabled = false;
                Config.CustomColours = false;
                this.f1.SwitchButtonColourButton();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.f1.Enabled = true;
            this.f1.Activate();
            this.f1.Redraw();
        }

        private void AdvancedWindow_Shown(object sender, EventArgs e)
        {
            this.f1.Enabled = false;
            this.groupBox2.Enabled = Config.CustomColours;
        }

        private void AdvancedWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Button1_Click(null, null);
            }
        }

        private void AccColButton_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.AccColButton.BackColor = this.colorDialog1.Color;
                Config.AcceleratorColour = this.colorDialog1.Color;
            }
            this.f1.Redraw();
        }

        private void DriftColButton_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.DriftColButton.BackColor = this.colorDialog1.Color;
                Config.DriftColour = this.colorDialog1.Color;
            }
            this.f1.Redraw();
        }

        private void ItemColButton_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ItemColButton.BackColor = this.colorDialog1.Color;
                Config.ItemColour = this.colorDialog1.Color;
            }
            this.f1.Redraw();
        }

        private void DirColButton_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.DirColButton.BackColor = this.colorDialog1.Color;
                Config.DirectionalColour = this.colorDialog1.Color;
            }
            this.f1.Redraw();
        }

        private void DPadColButton_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.DPadColButton.BackColor = this.colorDialog1.Color;
                Config.DPadColour = this.colorDialog1.Color;
            }
            this.f1.Redraw();
        }

        private void Tabcontrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Tabcontrol.SelectedTab == this.tabPage2)
            {
                
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputDisplay.Forms
{
    public partial class NoConfigForm : Form
    {
        public NoConfigForm()
        {
            InitializeComponent();

            this.FormClosed += new FormClosedEventHandler(NoConfigForm_OnClosed);
            this.button1.Click += new EventHandler(Button1_Click);
        }

        private void NoConfigForm_OnClosed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}

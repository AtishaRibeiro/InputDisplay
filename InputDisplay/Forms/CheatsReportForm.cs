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
    public partial class CheatsReportForm : Form
    {

        private List<String> RapidFireMessages { get; }
        private List<String> IllegalInputMessages { get; }

        public CheatsReportForm(List<String> rpMessages, List<String> iiMessages)
        {
            InitializeComponent();
            this.RapidFireMessages = rpMessages;
            this.IllegalInputMessages = iiMessages;
        }

        private void CheatsReportForm_Load(object sender, EventArgs e)
        {
            if (this.RapidFireMessages == null && this.IllegalInputMessages == null)
            {
                this.reportTxt.AppendText("No cheats settings were selected.\r\n");
                return;
            }

            if (this.RapidFireMessages != null)
            {
                this.reportTxt.AppendText("RAPID FIRE: \r\n");
                foreach (String message in this.RapidFireMessages)
                    this.reportTxt.AppendText(message + "\r\n");
                this.reportTxt.AppendText("\r\n");
            }

            if (this.IllegalInputMessages != null)
            {
                this.reportTxt.AppendText("ILLEGAL INPUTS: \r\n");
                foreach (String message in this.IllegalInputMessages)
                    this.reportTxt.AppendText(message + "\r\n");
                this.reportTxt.AppendText("\r\n");
            }
        }

        private void DismissButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

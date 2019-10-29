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

        private List<String> Messages { get; }

        public CheatsReportForm(List<String> messages)
        {
            InitializeComponent();
            this.Messages = messages;
        }

        private void CheatsReportForm_Load(object sender, EventArgs e)
        {
            foreach (String message in this.Messages) 
                this.reportTxt.AppendText(message + "\r\n");
        }

        private void DismissButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

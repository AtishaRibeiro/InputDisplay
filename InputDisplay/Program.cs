using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using InputDisplay.Forms;

namespace InputDisplay
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Config.CheckConfigFileIsPresent())
            {
                Application.Run(new MainForm());
            } else
            {
                Application.Run(new NoConfigForm());
            }
            
        }

    }
}

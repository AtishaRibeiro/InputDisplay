using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace InputDisplay
{
    static class Config
    {
        static public Color BackgroundColour {
            get
            {
                return Properties.Settings.Default.BackgroundColour;
            }
            set
            {
                Properties.Settings.Default.BackgroundColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color ButtonColour
        {
            get
            {
                return Properties.Settings.Default.ButtonColour;
            }
            set
            {
                Properties.Settings.Default.ButtonColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public bool CustomColours {
            get
            {
                return Properties.Settings.Default.CustomColours;
            }
            set
            {
                Properties.Settings.Default.CustomColours = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color AcceleratorColour {
            get
            {
                return Properties.Settings.Default.AcceleratorColour;
            }
            set
            {
                Properties.Settings.Default.AcceleratorColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color DriftColour {
            get
            {
                return Properties.Settings.Default.DriftColour;
            }
            set
            {
                Properties.Settings.Default.DriftColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color ItemColour {
            get
            {
                return Properties.Settings.Default.ItemColour;
            }
            set
            {
                Properties.Settings.Default.ItemColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color DirectionalColour {
            get
            {
                return Properties.Settings.Default.DirectionalColour;
            }
            set
            {
                Properties.Settings.Default.DirectionalColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color DPadColour {
            get
            {
                return Properties.Settings.Default.DPadColour;
            }
            set
            {
                Properties.Settings.Default.DPadColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public bool DisplayTimer {
            get
            {
                return Properties.Settings.Default.DisplayTimer;
            }
            set
            {
                Properties.Settings.Default.DisplayTimer = value;
                Properties.Settings.Default.Save();
            }
        }
        static public int LineWidth {
            get
            {
                return Properties.Settings.Default.LineWidth;
            }
            set
            {
                Properties.Settings.Default.LineWidth = value;
                Properties.Settings.Default.Save();
            }
        }
        static public int PlaybackSpeed { get; set; } = 60;
    }
}

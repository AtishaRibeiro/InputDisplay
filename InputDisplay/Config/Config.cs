using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace InputDisplay
{
    static partial class Config
    {
        static public void Save()
        {
            Properties.Settings.Default.Save();
        }

        static public string GhostFolder
        {
            get
            {
                return Properties.Settings.Default.GhostFolder;
            }
            set
            {
                Properties.Settings.Default.GhostFolder = value;
                Properties.Settings.Default.Save();
            }
        }

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
        static public Color OutlineColour
        {
            get
            {
                return Properties.Settings.Default.OutlineColour;
            }
            set
            {
                Properties.Settings.Default.OutlineColour = value;
                Properties.Settings.Default.Save();
            }
        }

        static public bool CustomColours
        {
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
        

        static public bool UseOutline
        {
            get
            {
                return Properties.Settings.Default.UseOutline;
            }
            set
            {
                Properties.Settings.Default.UseOutline = value;
                Properties.Settings.Default.Save();
            }
        }

        static public int Outline
        {
            get
            {
                return Properties.Settings.Default.Outline;
            }
            set
            {
                Properties.Settings.Default.Outline = value;
                Properties.Settings.Default.Save();
            }
        }

        static public int PlaybackSpeed { get; set; } = 60;
    }
}

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
        static public bool C_CustomColours {
            get
            {
                return Properties.Settings.Default.C_CustomColours;
            }
            set
            {
                Properties.Settings.Default.C_CustomColours = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color C_AcceleratorColour {
            get
            {
                return Properties.Settings.Default.C_AcceleratorColour;
            }
            set
            {
                Properties.Settings.Default.C_AcceleratorColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color C_DriftColour {
            get
            {
                return Properties.Settings.Default.C_DriftColour;
            }
            set
            {
                Properties.Settings.Default.C_DriftColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color C_ItemColour {
            get
            {
                return Properties.Settings.Default.C_ItemColour;
            }
            set
            {
                Properties.Settings.Default.C_ItemColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color C_DirectionalColour {
            get
            {
                return Properties.Settings.Default.C_DirectionalColour;
            }
            set
            {
                Properties.Settings.Default.C_DirectionalColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color C_DPadColour {
            get
            {
                return Properties.Settings.Default.C_DPadColour;
            }
            set
            {
                Properties.Settings.Default.C_DPadColour = value;
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
        static public double C_AcceleratorScale
        {
            get
            {
                return Properties.Settings.Default.C_AcceleratorScale;
            }
            set
            {
                Properties.Settings.Default.C_AcceleratorScale = value;
                Properties.Settings.Default.Save();
            }
        }

        static public double C_DriftScale
        {
            get
            {
                return Properties.Settings.Default.C_DriftScale;
            }
            set
            {
                Properties.Settings.Default.C_DriftScale = value;
                Properties.Settings.Default.Save();
            }
        }

        static public double C_ItemScale
        {
            get
            {
                return Properties.Settings.Default.C_ItemScale;
            }
            set
            {
                Properties.Settings.Default.C_ItemScale = value;
                Properties.Settings.Default.Save();
            }
        }

        static public double C_DirectionalScale
        {
            get
            {
                return Properties.Settings.Default.C_DirectionalScale;
            }
            set
            {
                Properties.Settings.Default.C_DirectionalScale = value;
                Properties.Settings.Default.Save();
            }
        }

        static public double C_DPadScale
        {
            get
            {
                return Properties.Settings.Default.C_DPadScale;
            }
            set
            {
                Properties.Settings.Default.C_DPadScale = value;
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

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
        static public Color N_WiiMoteColour
        {
            get
            {
                return Properties.Settings.Default.N_WiiMoteColour;
            }
            set
            {
                Properties.Settings.Default.N_WiiMoteColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color N_AcceleratorColour
        {
            get
            {
                return Properties.Settings.Default.N_AcceleratorColour;
            }
            set
            {
                Properties.Settings.Default.N_AcceleratorColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color N_DriftColour
        {
            get
            {
                return Properties.Settings.Default.N_DriftColour;
            }
            set
            {
                Properties.Settings.Default.N_DriftColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color N_ItemColour
        {
            get
            {
                return Properties.Settings.Default.N_ItemColour;
            }
            set
            {
                Properties.Settings.Default.N_ItemColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color N_DirectionalColour
        {
            get
            {
                return Properties.Settings.Default.N_DirectionalColour;
            }
            set
            {
                Properties.Settings.Default.N_DirectionalColour = value;
                Properties.Settings.Default.Save();
            }
        }
        static public Color N_TrickColour
        {
            get
            {
                return Properties.Settings.Default.N_TrickColour;
            }
            set
            {
                Properties.Settings.Default.N_TrickColour = value;
                Properties.Settings.Default.Save();
            }
        }

        static public double N_WiiMoteScale
        {
            get
            {
                return Properties.Settings.Default.N_WiiMoteScale;
            }
            set
            {
                Properties.Settings.Default.N_WiiMoteScale = value;
                Properties.Settings.Default.Save();
            }
        }

        static public double N_ItemScale
        {
            get
            {
                return Properties.Settings.Default.N_ItemScale;
            }
            set
            {
                Properties.Settings.Default.N_ItemScale = value;
                Properties.Settings.Default.Save();
            }
        }

        static public double N_DirectionalScale
        {
            get
            {
                return Properties.Settings.Default.N_DirectionalScale;
            }
            set
            {
                Properties.Settings.Default.N_DirectionalScale = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}

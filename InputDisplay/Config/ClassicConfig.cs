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
        static public Color C_AcceleratorColour
        {
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
        static public Color C_DriftColour
        {
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
        static public Color C_ItemColour
        {
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
        static public Color C_DirectionalColour
        {
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
        static public Color C_DPadColour
        {
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

        static public Point C_AcceleratorPos
        {
            get
            {
                return Properties.Settings.Default.C_AcceleratorPos;
            }
            set
            {
                Properties.Settings.Default.C_AcceleratorPos = value;
            }
        }

        static public Point C_DriftPos
        {
            get
            {
                return Properties.Settings.Default.C_DriftPos;
            }
            set
            {
                Properties.Settings.Default.C_DriftPos = value;
            }
        }

        static public Point C_DirectionalPos
        {
            get
            {
                return Properties.Settings.Default.C_DirectionalPos;
            }
            set
            {
                Properties.Settings.Default.C_DirectionalPos = value;
            }
        }

        static public Point C_ItemPos
        {
            get
            {
                return Properties.Settings.Default.C_ItemPos;
            }
            set
            {
                Properties.Settings.Default.C_ItemPos = value;
            }
        }

        static public Point C_DPadPos
        {
            get
            {
                return Properties.Settings.Default.C_DPadPos;
            }
            set
            {
                Properties.Settings.Default.C_DPadPos = value;
            }
        }
    }
}

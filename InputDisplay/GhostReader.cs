using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InputDisplay
{
    class GhostReader
    {
        public GhostReader()
        {
            // (Accelerator, Drift, Item)
            this.Face_inputs = new List<(int, (bool, bool, bool))>();
            // (Horizontal, Vertical)
            this.Analog_inputs = new List<(int, (double, double))>();
            // 0 -> none, 1 -> up, 2 -> down, 3 -> left, 4 -> right 
            this.Trick_inputs = new List<(int, int)>();
    }

        public void ReadFile(string filename)
        {
            // clear lists first
            this.Face_inputs.Clear();
            this.Analog_inputs.Clear();
            this.Trick_inputs.Clear();

            byte[] data = File.ReadAllBytes(filename);
            this.Controller_type = data[11] & 15;
            //extract the input data and put it in its own array
            byte[] input_data = new byte[data.Length - 136];
            Array.Copy(data, 136, input_data, 0, data.Length - 136);
            
            // if input data is compressed
            if ((data[12] & 8) != 0)
            {
                Yaz1dec decoder = new Yaz1dec();
                input_data = decoder.DecodeAll(input_data).ToArray();
            }

            // evaluate inputs

            //read header information
            int face_button_inputs = (input_data[0] << 8) | input_data[1];
            int directional_inputs = (input_data[2] << 8) | input_data[3];
            int trick_inputs = (input_data[4] << 8) | input_data[5];
            int current_byte = 8;

            int endFrame = 0;
            for (int i = 0; i < face_button_inputs; ++i)
            {
                int inputs = input_data[current_byte];
                int duration = input_data[current_byte + 1];
                bool accelerator = (inputs & 1) != 0;
                bool drift = (inputs & 2) != 0;
                bool item = (inputs & 4) != 0;

                endFrame += duration;
                this.Face_inputs.Add((endFrame, (accelerator, drift, item)));

                current_byte += 2;
            }

            endFrame = 0;
            for (int i = 0; i < directional_inputs; ++i)
            {
                int inputs = input_data[current_byte];
                int duration = input_data[current_byte + 1];
                int vertical = inputs & 15;
                int horizontal = (inputs >> 4) & 15;

                endFrame += duration;
                this.Analog_inputs.Add((endFrame, (horizontal / 14.0, vertical / 14.0)));

                current_byte += 2;
            }

            endFrame = 0;
            for (int i = 0; i < trick_inputs; ++i)
            {
                int inputs = input_data[current_byte];
                int duration = input_data[current_byte + 1];
                int trick = Convert.ToInt32(Math.Sqrt((inputs & 112) / 16));

                endFrame += duration;
                this.Trick_inputs.Add((endFrame, trick));

                current_byte += 2;
            }
        }

        public int Controller_type { get; set; }
        public List<(int endFrame, (bool, bool, bool) values)> Face_inputs { get; }
        public List<(int endFrame, (double, double) values)> Analog_inputs { get; }
        public List<(int endFrame, int values)> Trick_inputs { get; }
    }
}

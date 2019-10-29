using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InputDisplay.Core
{
    class GhostReader
    {
        public string CompletionTime { get; set; }
        public string Controller_type { get; set; }
        public String MiiName { get; set; }
        public int TotalFrames { get; set; }
        public List<(int endFrame, (bool, bool, bool) values)> Face_inputs { get; }
        public List<(int endFrame, (double, double) values)> Analog_inputs { get; }
        public List<(int endFrame, int values)> Trick_inputs { get; }
        public CheatDetector CheatDetetor { get; set; }


        public GhostReader()
        {
            // (Accelerator, Drift, Item)
            this.Face_inputs = new List<(int, (bool, bool, bool))>();
            // (Horizontal, Vertical)
            this.Analog_inputs = new List<(int, (double, double))>();
            // 0 -> none, 1 -> up, 2 -> down, 3 -> left, 4 -> right 
            this.Trick_inputs = new List<(int, int)>();

            this.CheatDetetor = new CheatDetector();
        }

        public void ReadFile(string filename)
        {
            // clear lists first
            this.Face_inputs.Clear();
            this.Analog_inputs.Clear();
            this.Trick_inputs.Clear();

            //
            // evaluate header
            //

            byte[] data = File.ReadAllBytes(filename);
            // read completion time
            int minutes = (data[4] & 0xFE) >> 1;
            int seconds = ((data[4] & 0x01) << 6) | ((data[5] & 0xFC) >> 2);
            int milliseconds = ((data[5] & 0x03) << 8) | data[6];
            // this can probably be formatted in an easier way but I can't figure it out
            this.CompletionTime = String.Format("{0}:{1}.{2}", minutes.ToString("D2"), seconds.ToString("D2"), milliseconds.ToString("D3"));
            // read controller type
            int ctrl_type = data[11] & 0x0F;
            switch (ctrl_type)
            {
                case 0:
                    this.Controller_type = "Wii Wheel";
                    break;
                case 1:
                    this.Controller_type = "Nunchuck";
                    break;
                case 2:
                    this.Controller_type = "Classic";
                    break;
                default:
                    this.Controller_type = "Gamecube";
                    break;
            }

            // read mii name
            byte[] name_bytes = new byte[20];
            Array.Copy(data, 62, name_bytes, 0, 20);
            this.MiiName = Encoding.BigEndianUnicode.GetString(name_bytes);

            //extract the input data and put it in its own array
            byte[] input_data = new byte[data.Length - 136];
            Array.Copy(data, 136, input_data, 0, data.Length - 136);

            // if input data is compressed
            if ((data[12] & 0x08) != 0)
            {
                Console.WriteLine("Compressed file");
                Yaz1dec decoder = new Yaz1dec();
                input_data = decoder.DecodeAll(input_data).ToArray();
            }

            //
            // evaluate inputs
            //

            //read header information
            int face_button_inputs = (input_data[0] << 0x08) | input_data[1];
            int directional_inputs = (input_data[2] << 0x08) | input_data[3];
            int trick_inputs = (input_data[4] << 0x08) | input_data[5];
            int current_byte = 8;

            int endFrame = 0;
            for (int i = 0; i < face_button_inputs; ++i)
            {
                int inputs = input_data[current_byte];
                int duration = input_data[current_byte + 1];
                bool accelerator = (inputs & 0x01) != 0;
                bool drift = (inputs & 0x02) != 0;
                bool item = (inputs & 0x04) != 0;

                endFrame += duration;
                this.Face_inputs.Add((endFrame, (accelerator, drift, item)));

                current_byte += 2;
            }

            this.TotalFrames = endFrame;

            endFrame = 0;
            for (int i = 0; i < directional_inputs; ++i)
            {
                int inputs = input_data[current_byte];
                int duration = input_data[current_byte + 1];
                int vertical = inputs & 0x0F;
                int horizontal = (inputs >> 4) & 0x0F;

                endFrame += duration;
                this.Analog_inputs.Add((endFrame, (horizontal / 14.0, vertical / 14.0)));

                current_byte += 2;
            }

            endFrame = 0;
            for (int i = 0; i < trick_inputs; ++i)
            {
                int inputs = input_data[current_byte];
                int duration = input_data[current_byte + 1];
                int trick = Convert.ToInt32((inputs & 0x70) / 16);
                int fullBytePresses = inputs & 0x0F;

                // fullBytePresses specifies how many times 255 frames was spent idling before the current action
                for (int j = 0; j < fullBytePresses; ++j)
                {
                    endFrame += 256;
                    this.Trick_inputs.Add((endFrame, 0));
                }

                endFrame += duration;
                this.Trick_inputs.Add((endFrame, trick));

                current_byte += 2;
            }

        }

        public List<String> DetectRapidFire(int gapSize)
        {
            return this.CheatDetetor.DetectRapidFire(this.Trick_inputs, gapSize);
        }

        public List<String> DetectIllegalInputs()
        {
            int controllerType = 0;

            switch (this.Controller_type)
            {
                case "Wii Wheel":
                    controllerType = 0;
                    break;
                case "Nunchuck":
                    controllerType = 1;
                    break;
                case "Classic":
                    controllerType = 2;
                    break;
                default:
                    controllerType = 3;
                    break;
            }

            return this.CheatDetetor.DetectIllegalInputs(this.Analog_inputs, controllerType);
        }

    }
}

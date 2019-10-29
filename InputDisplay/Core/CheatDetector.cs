using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputDisplay.Core
{
    class CheatDetector
    {

        private List<(double, double)> IllegalInputsGcnClassic { get; set; }
        private List<(double, double)> IllegalInputsNunchuck { get; set; }


        public CheatDetector()
        {
            this.populateIllegalInputs();
        }


        private void populateIllegalInputs()
        {
            // This could be replaced by a formula of some sort
            this.IllegalInputsGcnClassic = new List<(double, double)>
            {
                (0,0), (0,1), (0,2), (0,11), (0,12), (0,13), (0,14),
                (1,0), (1,1), (1,12), (1,13), (1,14),
                (2,0), (2,13), (2,14),
                (3,14),
                (4,14),
                (9,14),
                (10,14),
                (11,0), (11,13), (11,14),
                (12,0), (12,1), (12,12), (12,13), (12,14),
                (13,0), (13,1), (13,2), (13,11), (13,12), (13,13), (13,14),
                (14,0), (14,1), (14,2), (14,3), (14,4), (14,9), (14,10),
                (14,11), (14,12), (14,13), (14,14)

           };

            this.IllegalInputsNunchuck = new List<(double, double)>
            {
                (0,0), (0,1), (0,2), (0,12), (0,13), (0,14),
                (1,0), (1,1), (1,13), (1,14),
                (2,0), (2,14),
                (12,0), (12,14),
                (13,0), (13,1), (13,13), (13,14),
                (14,0), (14,1), (14,2),
                (14,12), (14,13), (14,14)

           };

        }

        public List<String> DetectRapidFire(List<(int endFrame, int values)> trickInputs, int gapSize)
        {

            List<String> messages = new List<string>();

            int prevEndingFrame = 0;
            int prevWheelieFrame = 0;
            bool streak = false;
            bool streakOver = false;
            int streakWheelieFrame = 0;
            int wheelieCount = 0;
            int frameCount = 0;
            gapSize++;

            // checking RapidFire (specifically wheelie input)
            foreach ((int endFrame, int values) input in trickInputs)
            {
                if (input.values == 0x1) // TODO: check any kind of input for Rapid Fire.
                {

                    if ((input.endFrame - prevWheelieFrame) <= gapSize && prevWheelieFrame != 0)
                    {
                        frameCount += (input.endFrame - prevWheelieFrame);
                        streak = true;
                        wheelieCount++;

                        if (streakWheelieFrame == 0)
                            streakWheelieFrame = prevEndingFrame;
                    }

                    prevWheelieFrame = input.endFrame;

                }
                else
                {
                    if (streak && (input.endFrame - prevEndingFrame) > gapSize)
                        streakOver = true;
                }


                if (streak && streakOver)
                {
                    wheelieCount++;
                    frameCount++;

                    double seconds = ((double)(streakWheelieFrame - 240) / 60);
                    TimeSpan ts = TimeSpan.FromSeconds(seconds);
                    messages.Add(String.Format("[Frame {0} ({1})] - {2} wheelies in {3} frames ({4:0.000} seconds)", streakWheelieFrame, (seconds >= 0 ? "" : "-") + ts.ToString("mm':'ss':'fff"), wheelieCount, frameCount, ((double)frameCount / 60)));
                    
                    streak = false;
                    streakOver = false;
                    wheelieCount = 0;
                    frameCount = 0;
                    streakWheelieFrame = 0;
                }

                // writetext.WriteLine(String.Format("{0} - {1}", input.values, input.endFrame - prevEndingFrame));

                prevEndingFrame = input.endFrame;

            }

            if (messages.Count == 0)
                messages.Add("No Rapid Fire inputs found.");

            return messages;

        }

        public List<String> DetectIllegalInputs(List<(int endFrame, (double, double) values)> analogInputs, int controllerType)
        {

            List<String> messages = new List<string>();

            if (controllerType == 0)
            {
                messages.Add("The controller type is a Wii Wheel wich cannot get illegal inputs.");
                return messages;
            }

            int prevEndingFrame = analogInputs[0].endFrame;
            foreach ((int endFrame, (double, double) values) input in analogInputs)
            {

                bool illegal = controllerType == 1
                    ? this.IllegalInputsGcnClassic.Contains(input.values)
                    : this.IllegalInputsNunchuck.Contains(input.values);

                if (illegal)
                {
                    double seconds = ((double)(prevEndingFrame - 240) / 60);
                    TimeSpan ts = TimeSpan.FromSeconds(seconds);

                    messages.Add(String.Format("[Frame {0} ({1})] - illegal input found ({2}, {3})!", prevEndingFrame, (seconds >= 0 ? "" : "-") + ts.ToString("mm':'ss':'fff"), input.values.Item1, input.values.Item2));
                }

                prevEndingFrame = input.endFrame;

            }

            if (messages.Count == 0)
                messages.Add("No illegal inputs found.");

            return messages;

        }
    }
}

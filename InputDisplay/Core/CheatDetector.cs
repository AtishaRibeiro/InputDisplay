using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputDisplay.Core
{
    public class CheatDetector
    {

        private List<(double, double)> IllegalInputsGcnClassic { get; set; }
        private List<(double, double)> IllegalInputsNunchuck { get; set; }
        private readonly int FrameCount_CountDown = 240;


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

                    double seconds = ((double)(streakWheelieFrame - this.FrameCount_CountDown) / 60);
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
                    double seconds = ((double)(prevEndingFrame - this.FrameCount_CountDown) / 60);
                    TimeSpan ts = TimeSpan.FromSeconds(seconds);

                    messages.Add(String.Format("[Frame {0} ({1})] - illegal input found ({2}, {3})!", prevEndingFrame, (seconds >= 0 ? "" : "-") + ts.ToString("mm':'ss':'fff"), input.values.Item1, input.values.Item2));
                }

                prevEndingFrame = input.endFrame;

            }

            if (messages.Count == 0)
                messages.Add("No illegal inputs found.");

            return messages;

        }


        private struct InputInfo { 
            public int Analog;
            public (double, double) AnalogInput;

            public int Face;
            public (bool, bool, bool) FaceInput;

            public int Trick;
            public int TrickInput;

        };
        public List<String> CompareGhost(GhostReader main, GhostReader second)
        {

            List<String> messages = new List<String>();
            messages.Add(String.Format("Comparing {0}: {1} ({2})   to   {3}: {4} ({5})", main.MiiName, main.CompletionTime, main.Controller_type, second.MiiName, second.CompletionTime, second.Controller_type));

            int streakCount = 0;
            int actualInputCount = 0;

            InputInfo mainCurrent = new InputInfo();
            InputInfo secondCurrent = new InputInfo();

            // main is being compared to second, so the loop will follow the main's frame size.
            for (int i = 0; i < main.TotalFrames; i++)
            {
                if (main.Analog_inputs.ElementAt(mainCurrent.Analog).endFrame <= i)
                {
                    if (mainCurrent.Analog + 1 < main.Analog_inputs.Count)
                    {
                        mainCurrent.Analog++;
                        mainCurrent.AnalogInput = main.Analog_inputs.ElementAt(mainCurrent.Analog).values;
                    }
                }
                if (main.Face_inputs.ElementAt(mainCurrent.Face).endFrame <= i)
                {
                    if (mainCurrent.Face + 1 < main.Face_inputs.Count) 
                    {
                        mainCurrent.Face++;
                        mainCurrent.FaceInput = main.Face_inputs.ElementAt(mainCurrent.Face).values;
                    }
                }
                if (main.Trick_inputs.ElementAt(mainCurrent.Trick).endFrame <= i)
                {
                    if (mainCurrent.Trick + 1 < main.Trick_inputs.Count)
                    {
                        mainCurrent.Trick++;
                        mainCurrent.TrickInput = main.Trick_inputs.ElementAt(mainCurrent.Trick).values;
                    }
                }

                if (second.Analog_inputs.ElementAt(secondCurrent.Analog).endFrame <= i)
                {
                    if (secondCurrent.Analog + 1 < second.Analog_inputs.Count)
                    {
                        secondCurrent.Analog++;
                        secondCurrent.AnalogInput = second.Analog_inputs.ElementAt(secondCurrent.Analog).values;
                    }
                }
                if (second.Face_inputs.ElementAt(secondCurrent.Face).endFrame <= i)
                {
                    if (secondCurrent.Face + 1 < second.Face_inputs.Count)
                    {
                        secondCurrent.Face++;
                        secondCurrent.FaceInput = second.Face_inputs.ElementAt(secondCurrent.Face).values;
                    }
                }
                if (second.Trick_inputs.ElementAt(secondCurrent.Trick).endFrame <= i)
                {
                    if (secondCurrent.Trick + 1 < second.Trick_inputs.Count)
                    {
                        secondCurrent.Trick++;
                        secondCurrent.TrickInput = second.Trick_inputs.ElementAt(secondCurrent.Trick).values;
                    }
                }

                // Check if both ghosts do the same inputs and on the same frame
                if (mainCurrent.AnalogInput == secondCurrent.AnalogInput && mainCurrent.Analog == secondCurrent.Analog
                    && mainCurrent.FaceInput == secondCurrent.FaceInput && mainCurrent.Face == secondCurrent.Face
                    && mainCurrent.TrickInput == secondCurrent.TrickInput && mainCurrent.Trick == secondCurrent.Trick)
                {
                    if (mainCurrent.AnalogInput != (7, 7) && mainCurrent.FaceInput != (true, false, false) && mainCurrent.TrickInput != 0x0)
                        actualInputCount++;

                    streakCount++;
                }
            }

            if ((streakCount > this.FrameCount_CountDown + 60) && actualInputCount > 2) 
            {
                double seconds = ((double)(streakCount - this.FrameCount_CountDown) / 60);
                TimeSpan ts = TimeSpan.FromSeconds(seconds);
                TimeSpan ts2 = TimeSpan.FromSeconds(((double)streakCount / 60));
                messages.Add(String.Format("TAS Code detected for {0} frames!!!", streakCount));
                messages.Add(String.Format("Duration: {0}. TAS Code released at: {1}", ts2.ToString("mm':'ss':'fff"), (seconds >= 0 ? "" : "-") + ts.ToString("mm':'ss':'fff")));
            }
            else
            {
                messages.Add("No TAS Code inputs found.");
            }

            return messages;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using InputDisplay.Core;

namespace InputDisplay.Forms
{
    public partial class RecordForm : Form
    {
        MainForm MainF;
        Animator Animator;
        bool AlphaChannel;
        bool RecordFormActive = false;
        string FileFormat;
        string FileName;
        Color SavedColour;
        bool ManualClose = true;

        public RecordForm(MainForm mainForm, Animator animator, bool alhpaChannel, string fileFormat)
        {
            this.MainF = mainForm;
            this.Animator = animator;
            this.AlphaChannel = alhpaChannel;
            this.FileFormat = fileFormat;
            InitializeComponent();

            
            this.FormClosing += new FormClosingEventHandler(RecordForm_FormClosing);
            this.LeftButton.Click += new EventHandler(LeftButton_Click);
            this.RightButton.Click += new EventHandler(RightButton_Click);
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(Render);
            this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker1_RunWorkerCompleted);

            // Prepare the form
            this.MainF.Enabled = false;
            this.Focus();
            this.label1.Visible = true;
            this.label2.Visible = false;
            this.progressBar1.Visible = false;

            Animator.Fps = (60 / 1.001); // Set the framerate of the animator to 59.94fps, this matches the framerate of the game

            if (Config.PlaybackSpeed == (60/1.001))
            {
                this.PrepareRecording();
            } 
            else
            {
                this.label1.Text = String.Format("The current playback speed is set to {0} (regular speed is 100)\nAre you sure you want to continue?", Config.PlaybackSpeed);
            }
        }

        public void Start()
        {
            if (this.RecordFormActive)
            {
                this.backgroundWorker1.RunWorkerAsync();
            }
        }

        private void PrepareRecording()
        {
            this.label1.Visible = false;
            this.label2.Visible = true;
            this.progressBar1.Visible = true;
            this.LeftButton.Text = "Cancel";
            this.RightButton.Text = "Continue";
            this.RightButton.Enabled = false;
            this.RecordFormActive = true;
        }

        private void Exit()
        {
            this.ManualClose = false;
            this.backgroundWorker1.CancelAsync();
            this.Close();
            this.MainF.Enabled = true;
            this.MainF.Focus();
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            this.Exit();
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            if (this.RecordFormActive)
            {
                this.Exit();
            } else
            {
                this.PrepareRecording();
                this.Start();
            }
        }

        private void RecordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.RightButton.Enabled)
            {
                this.backgroundWorker1.CancelAsync();
                this.MainF.Enabled = true;
                this.MainF.Focus();
            } else if (this.ManualClose)
            {
                e.Cancel = true;
            }
            
        }

        private void Render(object sender, DoWorkEventArgs e)
        {
            // Create the temp folder if it doesn't exist yet
            string path = "temp";
            DirectoryInfo di;
            if (!Directory.Exists(path))
            {
                di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            else
            {
                di = new DirectoryInfo("temp");
                // Empty the temp directory
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
            }
            
            // Set the background colour to transparent if necessary
            this.SavedColour = Config.BackgroundColour;
            string codec = "mjpeg -q:v 1";
            if (this.AlphaChannel)
            {
                Config.BackgroundColour = Color.Transparent;
                codec = "png";
            }

            this.backgroundWorker1.ReportProgress(25);

            // Prepare the animator and write all the frames to the temp directory
            this.Animator.Clear();
            int frameNr = 0;
            int totalFrames = (int)(Math.Ceiling((((60/1.001) / Config.PlaybackSpeed) * this.Animator.Fps)) * this.Animator.GetGhostInfo().Item4);
            while (this.Animator.Update())
            {
                // Check for cancel request
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                this.Animator.DrawFrame();
                this.Animator.bmp.Save(String.Format("temp\\{0}.png", frameNr), System.Drawing.Imaging.ImageFormat.Png);
                this.backgroundWorker1.ReportProgress(25 + (frameNr / totalFrames) * 50);
                ++frameNr;
            }
            this.backgroundWorker1.ReportProgress(75);

            // Compile all the frames into a video using png encoding to preserve transparent background
            Process proc = new Process();
            proc.StartInfo.FileName = "ffmpeg\\ffmpeg";
            this.FileName = String.Format("{0}.{1}", DateTime.Now.ToString("yy-MM-dd-hh-mm-ss"), this.FileFormat);
            proc.StartInfo.Arguments = String.Format("-r \"60000/1001\" -i temp\\%d.png -vcodec {0} {1}", codec, this.FileName);
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            if (!proc.Start())
            {
                Console.WriteLine("Error starting");
                e.Cancel = true;
                this.label2.Text = "An error occured.";
                return;
            }

            // Extract info from the console to know how far we are in the compilation process
            StreamReader reader = proc.StandardError;
            string line;
            char[] delimChar = { ' ' };
            while ((line = reader.ReadLine()) != null)
            {
                // Checks for cancel request
                if (backgroundWorker1.CancellationPending)
                {
                    proc.Kill();
                    e.Cancel = true;
                    return;
                }
                string[] words = line.Split(delimChar);
                if (words[0] == "frame=")
                {
                    int i = 1;
                    while (words[i] == "") { ++i; }
                    int currentFrame = Int32.Parse(words[i]);
                    this.backgroundWorker1.ReportProgress(75 + (currentFrame / totalFrames) * 25);
                }
            }
            this.backgroundWorker1.ReportProgress(100);
            proc.Close();
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.LeftButton.Enabled = false;
            this.RightButton.Enabled = true;
            this.label2.Text = "Done.";

            // If cancelled delete the unfinished video file if present
            if (e.Cancelled && this.FileName != null)
            {
                // Keep trying to delete the file until it is successful
                while (!this.TryDeleteFile()) { }
            }

            Config.BackgroundColour = this.SavedColour;
            this.MainF.SimulateStopClick();
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }

        private bool TryDeleteFile()
        {
            try
            {
                FileInfo file = new FileInfo(this.FileName);
                if (file.Exists)
                {
                    file.Delete();
                }
                return true;
            } catch (IOException e)
            {
                return false;
            }
        }
    }
}

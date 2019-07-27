using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics; //testing

namespace InputDisplay
{
    public partial class Form1 : Form
    {
        private Animator Animator;
        private bool GhostLoaded = false;
        private bool Animate = false;

        private Stopwatch stopWatch = new Stopwatch();
        private AccurateTimer timer;

        public Form1()
        {
            InitializeComponent();
            this.Animator = new Animator(50);
        }

        private void InitializeComponent()
        {
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ControlTab = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ColourTab = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DirColButton = new System.Windows.Forms.Button();
            this.DPadColButton = new System.Windows.Forms.Button();
            this.ItemColButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.DriftColButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.AccColButton = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.TransformTab = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.DPadSlide = new System.Windows.Forms.TrackBar();
            this.panel5 = new System.Windows.Forms.Panel();
            this.DirSlide = new System.Windows.Forms.TrackBar();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ItemSlide = new System.Windows.Forms.TrackBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DriftSlide = new System.Windows.Forms.TrackBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.AccSlide = new System.Windows.Forms.TrackBar();
            this.DPadScale = new System.Windows.Forms.TextBox();
            this.DirScale = new System.Windows.Forms.TextBox();
            this.ItemScale = new System.Windows.Forms.TextBox();
            this.DriftScale = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.AccScale = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.StyleTab = new System.Windows.Forms.TabPage();
            this.SchemeTab = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.ControlTab.SuspendLayout();
            this.ColourTab.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.TransformTab.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DPadSlide)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DirSlide)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemSlide)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DriftSlide)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AccSlide)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(185, 10);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(44, 20);
            this.numericUpDown2.TabIndex = 11;
            this.numericUpDown2.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.NumericUpDown2_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Line Width";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(185, 34);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(44, 20);
            this.numericUpDown1.TabIndex = 8;
            this.numericUpDown1.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.NumericUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Playback Speed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Button Colour";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Background Colour";
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LightGreen;
            this.button3.Enabled = false;
            this.button3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(81, 321);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(175, 48);
            this.button3.TabIndex = 3;
            this.button3.Text = "Play";
            this.button3.UseMnemonic = false;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button4.Enabled = false;
            this.button4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(12, 321);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(65, 48);
            this.button4.TabIndex = 4;
            this.button4.Text = "Stop";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Location = new System.Drawing.Point(12, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 47);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(9, 13);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(224, 28);
            this.button5.TabIndex = 0;
            this.button5.Text = "Open Ghost File";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Location = new System.Drawing.Point(262, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(455, 314);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = global::InputDisplay.Properties.Settings.Default.BackgroundColour;
            this.pictureBox1.Location = new System.Drawing.Point(7, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(442, 295);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(122, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Time";
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "MiiName";
            this.label7.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(192, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "No File Selected";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "File";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(262, 322);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(455, 47);
            this.panel1.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ControlTab);
            this.tabControl1.Controls.Add(this.ColourTab);
            this.tabControl1.Controls.Add(this.TransformTab);
            this.tabControl1.Controls.Add(this.StyleTab);
            this.tabControl1.Controls.Add(this.SchemeTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 55);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(248, 260);
            this.tabControl1.TabIndex = 8;
            // 
            // ControlTab
            // 
            this.ControlTab.Controls.Add(this.numericUpDown2);
            this.ControlTab.Controls.Add(this.checkBox1);
            this.ControlTab.Controls.Add(this.numericUpDown1);
            this.ControlTab.Controls.Add(this.label4);
            this.ControlTab.Controls.Add(this.label3);
            this.ControlTab.Location = new System.Drawing.Point(4, 22);
            this.ControlTab.Name = "ControlTab";
            this.ControlTab.Padding = new System.Windows.Forms.Padding(3);
            this.ControlTab.Size = new System.Drawing.Size(240, 234);
            this.ControlTab.TabIndex = 0;
            this.ControlTab.Text = "General";
            this.ControlTab.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = global::InputDisplay.Properties.Settings.Default.DisplayTimer;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(6, 86);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(89, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Display Timer";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // ColourTab
            // 
            this.ColourTab.Controls.Add(this.groupBox3);
            this.ColourTab.Controls.Add(this.checkBox2);
            this.ColourTab.Controls.Add(this.label1);
            this.ColourTab.Controls.Add(this.label2);
            this.ColourTab.Controls.Add(this.button1);
            this.ColourTab.Controls.Add(this.button2);
            this.ColourTab.Location = new System.Drawing.Point(4, 22);
            this.ColourTab.Name = "ColourTab";
            this.ColourTab.Padding = new System.Windows.Forms.Padding(3);
            this.ColourTab.Size = new System.Drawing.Size(240, 234);
            this.ColourTab.TabIndex = 1;
            this.ColourTab.Text = "Colour";
            this.ColourTab.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DirColButton);
            this.groupBox3.Controls.Add(this.DPadColButton);
            this.groupBox3.Controls.Add(this.ItemColButton);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.DriftColButton);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.AccColButton);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Enabled = global::InputDisplay.Properties.Settings.Default.CustomColours;
            this.groupBox3.Location = new System.Drawing.Point(6, 80);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(228, 155);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // DirColButton
            // 
            this.DirColButton.BackColor = global::InputDisplay.Properties.Settings.Default.DirectionalColour;
            this.DirColButton.Location = new System.Drawing.Point(166, 88);
            this.DirColButton.Name = "DirColButton";
            this.DirColButton.Size = new System.Drawing.Size(57, 20);
            this.DirColButton.TabIndex = 2;
            this.DirColButton.UseVisualStyleBackColor = false;
            this.DirColButton.Click += new System.EventHandler(this.DirColButton_Click);
            // 
            // DPadColButton
            // 
            this.DPadColButton.BackColor = global::InputDisplay.Properties.Settings.Default.DPadColour;
            this.DPadColButton.Location = new System.Drawing.Point(166, 114);
            this.DPadColButton.Name = "DPadColButton";
            this.DPadColButton.Size = new System.Drawing.Size(57, 20);
            this.DPadColButton.TabIndex = 2;
            this.DPadColButton.UseVisualStyleBackColor = false;
            this.DPadColButton.Click += new System.EventHandler(this.DPadColButton_Click);
            // 
            // ItemColButton
            // 
            this.ItemColButton.BackColor = global::InputDisplay.Properties.Settings.Default.ItemColour;
            this.ItemColButton.Location = new System.Drawing.Point(166, 62);
            this.ItemColButton.Name = "ItemColButton";
            this.ItemColButton.Size = new System.Drawing.Size(57, 20);
            this.ItemColButton.TabIndex = 2;
            this.ItemColButton.UseVisualStyleBackColor = false;
            this.ItemColButton.Click += new System.EventHandler(this.ItemColButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "D-Pad";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 62);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Item";
            // 
            // DriftColButton
            // 
            this.DriftColButton.BackColor = global::InputDisplay.Properties.Settings.Default.DriftColour;
            this.DriftColButton.Location = new System.Drawing.Point(166, 38);
            this.DriftColButton.Name = "DriftColButton";
            this.DriftColButton.Size = new System.Drawing.Size(57, 20);
            this.DriftColButton.TabIndex = 2;
            this.DriftColButton.UseVisualStyleBackColor = false;
            this.DriftColButton.Click += new System.EventHandler(this.DriftColButton_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Drift";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Directional";
            // 
            // AccColButton
            // 
            this.AccColButton.BackColor = global::InputDisplay.Properties.Settings.Default.AcceleratorColour;
            this.AccColButton.Location = new System.Drawing.Point(166, 12);
            this.AccColButton.Name = "AccColButton";
            this.AccColButton.Size = new System.Drawing.Size(57, 20);
            this.AccColButton.TabIndex = 2;
            this.AccColButton.UseVisualStyleBackColor = false;
            this.AccColButton.Click += new System.EventHandler(this.AccColButton_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Accelerator";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = global::InputDisplay.Properties.Settings.Default.CustomColours;
            this.checkBox2.Location = new System.Drawing.Point(5, 66);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(99, 17);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "Custom Colours";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.CheckBox2_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = global::InputDisplay.Properties.Settings.Default.BackgroundColour;
            this.button1.Location = new System.Drawing.Point(172, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 20);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = global::InputDisplay.Properties.Settings.Default.ButtonColour;
            this.button2.Location = new System.Drawing.Point(172, 35);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(57, 20);
            this.button2.TabIndex = 3;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // TransformTab
            // 
            this.TransformTab.Controls.Add(this.groupBox5);
            this.TransformTab.Controls.Add(this.groupBox4);
            this.TransformTab.Location = new System.Drawing.Point(4, 22);
            this.TransformTab.Name = "TransformTab";
            this.TransformTab.Size = new System.Drawing.Size(240, 234);
            this.TransformTab.TabIndex = 2;
            this.TransformTab.Text = "Transform";
            this.TransformTab.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.panel6);
            this.groupBox5.Controls.Add(this.panel5);
            this.groupBox5.Controls.Add(this.panel4);
            this.groupBox5.Controls.Add(this.panel3);
            this.groupBox5.Controls.Add(this.panel2);
            this.groupBox5.Controls.Add(this.DPadScale);
            this.groupBox5.Controls.Add(this.DirScale);
            this.groupBox5.Controls.Add(this.ItemScale);
            this.groupBox5.Controls.Add(this.DriftScale);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.AccScale);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Location = new System.Drawing.Point(3, 66);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(234, 165);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Scale";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.DPadSlide);
            this.panel6.Location = new System.Drawing.Point(111, 123);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(117, 23);
            this.panel6.TabIndex = 15;
            // 
            // DPadSlide
            // 
            this.DPadSlide.Location = new System.Drawing.Point(11, -9);
            this.DPadSlide.Maximum = 20;
            this.DPadSlide.Minimum = 1;
            this.DPadSlide.Name = "DPadSlide";
            this.DPadSlide.Size = new System.Drawing.Size(104, 45);
            this.DPadSlide.TabIndex = 0;
            this.DPadSlide.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.DPadSlide.Value = 10;
            this.DPadSlide.ValueChanged += new System.EventHandler(this.DPadSlide_ValueChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.DirSlide);
            this.panel5.Location = new System.Drawing.Point(111, 97);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(117, 23);
            this.panel5.TabIndex = 15;
            // 
            // DirSlide
            // 
            this.DirSlide.Location = new System.Drawing.Point(11, -9);
            this.DirSlide.Maximum = 20;
            this.DirSlide.Minimum = 1;
            this.DirSlide.Name = "DirSlide";
            this.DirSlide.Size = new System.Drawing.Size(104, 45);
            this.DirSlide.TabIndex = 0;
            this.DirSlide.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.DirSlide.Value = 10;
            this.DirSlide.ValueChanged += new System.EventHandler(this.DirSlide_ValueChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ItemSlide);
            this.panel4.Location = new System.Drawing.Point(111, 71);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(117, 23);
            this.panel4.TabIndex = 15;
            // 
            // ItemSlide
            // 
            this.ItemSlide.Location = new System.Drawing.Point(11, -9);
            this.ItemSlide.Maximum = 20;
            this.ItemSlide.Minimum = 1;
            this.ItemSlide.Name = "ItemSlide";
            this.ItemSlide.Size = new System.Drawing.Size(104, 45);
            this.ItemSlide.TabIndex = 0;
            this.ItemSlide.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.ItemSlide.Value = 10;
            this.ItemSlide.ValueChanged += new System.EventHandler(this.ItemSlide_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.DriftSlide);
            this.panel3.Location = new System.Drawing.Point(111, 45);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(117, 23);
            this.panel3.TabIndex = 15;
            // 
            // DriftSlide
            // 
            this.DriftSlide.Location = new System.Drawing.Point(11, -9);
            this.DriftSlide.Maximum = 20;
            this.DriftSlide.Minimum = 1;
            this.DriftSlide.Name = "DriftSlide";
            this.DriftSlide.Size = new System.Drawing.Size(104, 45);
            this.DriftSlide.TabIndex = 0;
            this.DriftSlide.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.DriftSlide.Value = 10;
            this.DriftSlide.ValueChanged += new System.EventHandler(this.DriftSlide_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.AccSlide);
            this.panel2.Location = new System.Drawing.Point(111, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(117, 23);
            this.panel2.TabIndex = 14;
            // 
            // AccSlide
            // 
            this.AccSlide.Location = new System.Drawing.Point(11, -9);
            this.AccSlide.Maximum = 20;
            this.AccSlide.Minimum = 1;
            this.AccSlide.Name = "AccSlide";
            this.AccSlide.Size = new System.Drawing.Size(104, 45);
            this.AccSlide.TabIndex = 0;
            this.AccSlide.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.AccSlide.Value = 10;
            this.AccSlide.ValueChanged += new System.EventHandler(this.AccSlide_ValueChanged);
            // 
            // DPadScale
            // 
            this.DPadScale.Enabled = false;
            this.DPadScale.Location = new System.Drawing.Point(74, 126);
            this.DPadScale.MaxLength = 4;
            this.DPadScale.Name = "DPadScale";
            this.DPadScale.Size = new System.Drawing.Size(29, 20);
            this.DPadScale.TabIndex = 13;
            this.DPadScale.Text = "1";
            this.DPadScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DirScale
            // 
            this.DirScale.Enabled = false;
            this.DirScale.Location = new System.Drawing.Point(74, 100);
            this.DirScale.MaxLength = 4;
            this.DirScale.Name = "DirScale";
            this.DirScale.Size = new System.Drawing.Size(29, 20);
            this.DirScale.TabIndex = 12;
            this.DirScale.Text = "1";
            this.DirScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ItemScale
            // 
            this.ItemScale.Enabled = false;
            this.ItemScale.Location = new System.Drawing.Point(74, 74);
            this.ItemScale.MaxLength = 4;
            this.ItemScale.Name = "ItemScale";
            this.ItemScale.Size = new System.Drawing.Size(29, 20);
            this.ItemScale.TabIndex = 11;
            this.ItemScale.Text = "1";
            this.ItemScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DriftScale
            // 
            this.DriftScale.Enabled = false;
            this.DriftScale.Location = new System.Drawing.Point(74, 48);
            this.DriftScale.MaxLength = 4;
            this.DriftScale.Name = "DriftScale";
            this.DriftScale.Size = new System.Drawing.Size(29, 20);
            this.DriftScale.TabIndex = 10;
            this.DriftScale.Text = "1";
            this.DriftScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 130);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(37, 13);
            this.label16.TabIndex = 6;
            this.label16.Text = "D-Pad";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 77);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(27, 13);
            this.label17.TabIndex = 7;
            this.label17.Text = "Item";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 51);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(26, 13);
            this.label18.TabIndex = 8;
            this.label18.Text = "Drift";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 104);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(57, 13);
            this.label19.TabIndex = 9;
            this.label19.Text = "Directional";
            // 
            // AccScale
            // 
            this.AccScale.Enabled = false;
            this.AccScale.Location = new System.Drawing.Point(74, 22);
            this.AccScale.MaxLength = 4;
            this.AccScale.Name = "AccScale";
            this.AccScale.Size = new System.Drawing.Size(29, 20);
            this.AccScale.TabIndex = 5;
            this.AccScale.Text = "1";
            this.AccScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Accelerator";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(234, 57);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Position";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(181, 26);
            this.label14.TabIndex = 0;
            this.label14.Text = "Drag and drop buttons to move them\nHold shift for more precise movement";
            // 
            // StyleTab
            // 
            this.StyleTab.Location = new System.Drawing.Point(4, 22);
            this.StyleTab.Name = "StyleTab";
            this.StyleTab.Size = new System.Drawing.Size(240, 234);
            this.StyleTab.TabIndex = 4;
            this.StyleTab.Text = "Style";
            this.StyleTab.UseVisualStyleBackColor = true;
            // 
            // SchemeTab
            // 
            this.SchemeTab.Location = new System.Drawing.Point(4, 22);
            this.SchemeTab.Name = "SchemeTab";
            this.SchemeTab.Size = new System.Drawing.Size(240, 234);
            this.SchemeTab.TabIndex = 3;
            this.SchemeTab.Text = "Scheme";
            this.SchemeTab.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(729, 381);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.MinimumSize = new System.Drawing.Size(745, 420);
            this.Name = "Form1";
            this.Text = "Input Display";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ControlTab.ResumeLayout(false);
            this.ControlTab.PerformLayout();
            this.ColourTab.ResumeLayout(false);
            this.ColourTab.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.TransformTab.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DPadSlide)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DirSlide)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemSlide)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DriftSlide)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AccSlide)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.timer = new AccurateTimer(this, new Action(TimerCallback), 20);
            if (this.checkBox1.Checked)
            {
                this.checkBox1.CheckState = CheckState.Checked;
            }
            else
            {
                this.checkBox1.CheckState = CheckState.Unchecked;
            }
            if (Config.CustomColours)
            {
                this.button2.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer.Stop();
        }

        public void SwitchButtonColourButton()
        {
            this.button2.Enabled = !this.button2.Enabled;
        }

        private void TimerCallback()
        {
            this.stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            //Console.WriteLine(stopWatch.Elapsed);

            //this.stopWatch.Restart();
            if (this.Animate)
            {
                if (this.Animator.Update())
                {
                    this.pictureBox1.Invalidate();
                } else
                {
                    this.Animator.Clear();
                    this.Button3_Click(null, null);
                }
                
            }
            //this.stopWatch.Stop();
            //Console.WriteLine(stopWatch.Elapsed);


            this.stopWatch.Restart();

            return;
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "rkg files (*.rkg)|*.rkg",
                RestoreDirectory = true,
                Title = ("Open File")
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.Animator.ReadFile(ofd.FileName);
                this.GhostLoaded = true;
                this.button3.Enabled = true;
                this.button4.Enabled = true;
                this.label6.Visible = false;

                (string time, string name) info = this.Animator.GetGhostInfo();
                this.label5.Text = Path.GetFileName(ofd.FileName);
                this.label7.Text = "Mii: " + info.name;
                this.label8.Text = "Time: " + info.time;
                this.label5.Visible = true;
                this.label7.Visible = true;
                this.label8.Visible = true;
            }
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            this.Animator.Draw(ref g);
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.button1.BackColor = this.colorDialog1.Color;
                this.pictureBox1.BackColor = this.button1.BackColor;
                Config.BackgroundColour = this.button1.BackColor;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.button2.BackColor = this.colorDialog1.Color;
                Config.ButtonColour = this.button2.BackColor;
                this.pictureBox1.Invalidate();
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (this.GhostLoaded)
            {
                if (this.Animate)
                {
                    this.Animate = false;
                    this.button3.Text = "Play";
                    this.button3.BackColor = Color.LightGreen;

                } else
                {
                    this.Animate = true;
                    this.button3.Text = "Pause";
                    this.button3.BackColor = Color.LightGoldenrodYellow;
                }
            }

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (this.GhostLoaded)
            {
                this.Animate = false;
                this.button3.Text = "Play";
                this.button3.BackColor = Color.LightGreen;
                this.Animator.Clear();
                this.pictureBox1.Invalidate();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            int widthChange = control.Size.Width - 745;
            this.groupBox2.Width = 460 + widthChange;
            this.pictureBox1.Width = 448 + widthChange;
            this.panel1.Width = 460 + widthChange;

            int heightChange = control.Size.Height - 420;
            this.groupBox2.Height = 314 + heightChange;
            this.pictureBox1.Height = 295 + heightChange;
            this.button3.Location = new Point(81, 321 + heightChange);
            this.button4.Location = new Point(12, 321 + heightChange);
            this.panel1.Location = new Point(257, 322 + heightChange);
        }

    }
}

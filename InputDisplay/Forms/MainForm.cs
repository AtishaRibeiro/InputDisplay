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
using InputDisplay.Core;

namespace InputDisplay.Forms
{
    public partial class MainForm : Form
    {
        private Animator Animator;
        private GhostReader ComparisonGhost;
        private CheatDetector CheatDetector;
        private bool GhostLoaded = false;
        private bool Animate = false;
        private AccurateTimer timer;

        public MainForm()
        {
            InitializeComponent();
            VariableSetup();
            // Set the framerate of the animator to 62.5fps, this may seem weird but a frame lasts exactly 16ms this way
            this.Animator = new Animator(62.5, this.pictureBox1.ClientSize.Width, this.pictureBox1.ClientSize.Height);
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
            this.frameGapCombo.SelectedIndex = 0;
            this.ComparisonGhost = new GhostReader();
            this.CheatDetector = new CheatDetector();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            this.ControllerType = new System.Windows.Forms.Label();
            this.CustomiseTab = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ButtonSlide = new System.Windows.Forms.TrackBar();
            this.ButtonScale = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.ButtonColour = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.CurrentButton = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.GeneralTab = new System.Windows.Forms.TabPage();
            this.LayoutBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.RecordTab = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.FileFormatBox = new System.Windows.Forms.ComboBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.recordButton = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.CheatTab = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.cheatsSeperatorLbl = new System.Windows.Forms.Label();
            this.compareGhostBtn = new System.Windows.Forms.Button();
            this.compareGhostLbl = new System.Windows.Forms.Label();
            this.liveReplayCheck = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.frameGapCombo = new System.Windows.Forms.ComboBox();
            this.rapidFireCheck = new System.Windows.Forms.CheckBox();
            this.illegalInputCheck = new System.Windows.Forms.CheckBox();
            this.cheatCheckBtn = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.AboutTab = new System.Windows.Forms.TabPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label12 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.CustomiseTab.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonSlide)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.GeneralTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.RecordTab.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.CheatTab.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.AboutTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
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
            this.pictureBox1.BackColor = System.Drawing.Color.White;
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
            this.panel1.Controls.Add(this.ControllerType);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(262, 322);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(455, 47);
            this.panel1.TabIndex = 7;
            // 
            // ControllerType
            // 
            this.ControllerType.AutoSize = true;
            this.ControllerType.Location = new System.Drawing.Point(226, 29);
            this.ControllerType.Name = "ControllerType";
            this.ControllerType.Size = new System.Drawing.Size(51, 13);
            this.ControllerType.TabIndex = 4;
            this.ControllerType.Text = "Controller";
            this.ControllerType.Visible = false;
            // 
            // CustomiseTab
            // 
            this.CustomiseTab.Controls.Add(this.groupBox5);
            this.CustomiseTab.Controls.Add(this.groupBox4);
            this.CustomiseTab.Location = new System.Drawing.Point(4, 22);
            this.CustomiseTab.Name = "CustomiseTab";
            this.CustomiseTab.Size = new System.Drawing.Size(240, 234);
            this.CustomiseTab.TabIndex = 2;
            this.CustomiseTab.Text = "Customise";
            this.CustomiseTab.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button7);
            this.groupBox5.Controls.Add(this.checkBox1);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Controls.Add(this.CurrentButton);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Location = new System.Drawing.Point(0, 60);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(237, 172);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(55, 143);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(129, 23);
            this.button7.TabIndex = 29;
            this.button7.Text = "Reset Scale && Position";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(125, 43);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(99, 17);
            this.checkBox1.TabIndex = 28;
            this.checkBox1.Text = "Custom Colours";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Controls.Add(this.ButtonScale);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.ButtonColour);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(5, 63);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(226, 74);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ButtonSlide);
            this.panel2.Location = new System.Drawing.Point(109, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(117, 23);
            this.panel2.TabIndex = 14;
            // 
            // ButtonSlide
            // 
            this.ButtonSlide.Location = new System.Drawing.Point(11, -9);
            this.ButtonSlide.Maximum = 20;
            this.ButtonSlide.Minimum = 1;
            this.ButtonSlide.Name = "ButtonSlide";
            this.ButtonSlide.Size = new System.Drawing.Size(104, 45);
            this.ButtonSlide.TabIndex = 0;
            this.ButtonSlide.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.ButtonSlide.Value = 10;
            // 
            // ButtonScale
            // 
            this.ButtonScale.Enabled = false;
            this.ButtonScale.Location = new System.Drawing.Point(67, 45);
            this.ButtonScale.MaxLength = 4;
            this.ButtonScale.Name = "ButtonScale";
            this.ButtonScale.Size = new System.Drawing.Size(29, 20);
            this.ButtonScale.TabIndex = 5;
            this.ButtonScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Scale";
            // 
            // ButtonColour
            // 
            this.ButtonColour.BackColor = System.Drawing.Color.Transparent;
            this.ButtonColour.Location = new System.Drawing.Point(163, 16);
            this.ButtonColour.Name = "ButtonColour";
            this.ButtonColour.Size = new System.Drawing.Size(57, 20);
            this.ButtonColour.TabIndex = 24;
            this.ButtonColour.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Colour";
            // 
            // CurrentButton
            // 
            this.CurrentButton.Enabled = false;
            this.CurrentButton.Location = new System.Drawing.Point(6, 41);
            this.CurrentButton.Name = "CurrentButton";
            this.CurrentButton.ReadOnly = true;
            this.CurrentButton.Size = new System.Drawing.Size(100, 20);
            this.CurrentButton.TabIndex = 26;
            this.CurrentButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(213, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Click on an element to change its properties";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Location = new System.Drawing.Point(0, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(237, 57);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Position";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(187, 26);
            this.label14.TabIndex = 0;
            this.label14.Text = "Drag and drop elements to move them\r\nHold shift for grid-based movement";
            // 
            // GeneralTab
            // 
            this.GeneralTab.Controls.Add(this.LayoutBox);
            this.GeneralTab.Controls.Add(this.label11);
            this.GeneralTab.Controls.Add(this.button6);
            this.GeneralTab.Controls.Add(this.label21);
            this.GeneralTab.Controls.Add(this.label1);
            this.GeneralTab.Controls.Add(this.label2);
            this.GeneralTab.Controls.Add(this.button1);
            this.GeneralTab.Controls.Add(this.button2);
            this.GeneralTab.Controls.Add(this.numericUpDown4);
            this.GeneralTab.Controls.Add(this.label22);
            this.GeneralTab.Controls.Add(this.numericUpDown2);
            this.GeneralTab.Controls.Add(this.numericUpDown1);
            this.GeneralTab.Controls.Add(this.label4);
            this.GeneralTab.Controls.Add(this.label3);
            this.GeneralTab.Location = new System.Drawing.Point(4, 22);
            this.GeneralTab.Name = "GeneralTab";
            this.GeneralTab.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralTab.Size = new System.Drawing.Size(240, 234);
            this.GeneralTab.TabIndex = 0;
            this.GeneralTab.Text = "General";
            this.GeneralTab.UseVisualStyleBackColor = true;
            // 
            // LayoutBox
            // 
            this.LayoutBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LayoutBox.FormattingEnabled = true;
            this.LayoutBox.Items.AddRange(new object[] {
            "Classic/GCN",
            "Nunchuck"});
            this.LayoutBox.Location = new System.Drawing.Point(143, 157);
            this.LayoutBox.Name = "LayoutBox";
            this.LayoutBox.Size = new System.Drawing.Size(91, 21);
            this.LayoutBox.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 160);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Layout";
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(177, 131);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(57, 20);
            this.button6.TabIndex = 22;
            this.button6.UseVisualStyleBackColor = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 131);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(73, 13);
            this.label21.TabIndex = 21;
            this.label21.Text = "Outline Colour";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Background Colour";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Button Colour";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(177, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 20);
            this.button1.TabIndex = 18;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(177, 107);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(57, 20);
            this.button2.TabIndex = 20;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.numericUpDown4.Location = new System.Drawing.Point(185, 58);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(44, 20);
            this.numericUpDown4.TabIndex = 15;
            this.numericUpDown4.ValueChanged += new System.EventHandler(this.NumericUpDown4_ValueChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 60);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(71, 13);
            this.label22.TabIndex = 14;
            this.label22.Text = "Outline Width";
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
            100,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.NumericUpDown2_ValueChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(185, 34);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            99,
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Line Width";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Playback Speed";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.GeneralTab);
            this.tabControl1.Controls.Add(this.CustomiseTab);
            this.tabControl1.Controls.Add(this.RecordTab);
            this.tabControl1.Controls.Add(this.CheatTab);
            this.tabControl1.Controls.Add(this.AboutTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 55);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(248, 260);
            this.tabControl1.TabIndex = 8;
            // 
            // RecordTab
            // 
            this.RecordTab.Controls.Add(this.groupBox7);
            this.RecordTab.Controls.Add(this.groupBox6);
            this.RecordTab.Location = new System.Drawing.Point(4, 22);
            this.RecordTab.Name = "RecordTab";
            this.RecordTab.Size = new System.Drawing.Size(240, 234);
            this.RecordTab.TabIndex = 3;
            this.RecordTab.Text = "Record";
            this.RecordTab.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.FileFormatBox);
            this.groupBox7.Controls.Add(this.checkBox2);
            this.groupBox7.Controls.Add(this.recordButton);
            this.groupBox7.Location = new System.Drawing.Point(3, 49);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(234, 182);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 45);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(69, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "Video Format";
            // 
            // FileFormatBox
            // 
            this.FileFormatBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FileFormatBox.FormattingEnabled = true;
            this.FileFormatBox.Items.AddRange(new object[] {
            "AVI",
            "MOV",
            "MP4"});
            this.FileFormatBox.Location = new System.Drawing.Point(99, 42);
            this.FileFormatBox.Name = "FileFormatBox";
            this.FileFormatBox.Size = new System.Drawing.Size(51, 21);
            this.FileFormatBox.TabIndex = 2;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(6, 19);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox2.Size = new System.Drawing.Size(144, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Transparent Background";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // recordButton
            // 
            this.recordButton.Location = new System.Drawing.Point(6, 137);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(222, 39);
            this.recordButton.TabIndex = 0;
            this.recordButton.Text = "Record";
            this.recordButton.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(234, 40);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(50, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(132, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Create a video of the input";
            // 
            // CheatTab
            // 
            this.CheatTab.Controls.Add(this.groupBox8);
            this.CheatTab.Controls.Add(this.groupBox9);
            this.CheatTab.Location = new System.Drawing.Point(4, 22);
            this.CheatTab.Name = "CheatTab";
            this.CheatTab.Padding = new System.Windows.Forms.Padding(3);
            this.CheatTab.Size = new System.Drawing.Size(240, 234);
            this.CheatTab.TabIndex = 5;
            this.CheatTab.Text = "Cheats";
            this.CheatTab.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.cheatsSeperatorLbl);
            this.groupBox8.Controls.Add(this.compareGhostBtn);
            this.groupBox8.Controls.Add(this.compareGhostLbl);
            this.groupBox8.Controls.Add(this.liveReplayCheck);
            this.groupBox8.Controls.Add(this.label17);
            this.groupBox8.Controls.Add(this.frameGapCombo);
            this.groupBox8.Controls.Add(this.rapidFireCheck);
            this.groupBox8.Controls.Add(this.illegalInputCheck);
            this.groupBox8.Controls.Add(this.cheatCheckBtn);
            this.groupBox8.Location = new System.Drawing.Point(5, 49);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(228, 179);
            this.groupBox8.TabIndex = 6;
            this.groupBox8.TabStop = false;
            // 
            // cheatsSeperatorLbl
            // 
            this.cheatsSeperatorLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cheatsSeperatorLbl.Location = new System.Drawing.Point(6, 74);
            this.cheatsSeperatorLbl.Name = "cheatsSeperatorLbl";
            this.cheatsSeperatorLbl.Size = new System.Drawing.Size(216, 2);
            this.cheatsSeperatorLbl.TabIndex = 31;
            // 
            // compareGhostBtn
            // 
            this.compareGhostBtn.Enabled = false;
            this.compareGhostBtn.Location = new System.Drawing.Point(147, 87);
            this.compareGhostBtn.Name = "compareGhostBtn";
            this.compareGhostBtn.Size = new System.Drawing.Size(75, 23);
            this.compareGhostBtn.TabIndex = 30;
            this.compareGhostBtn.Text = "Select .rkg";
            this.compareGhostBtn.UseVisualStyleBackColor = true;
            this.compareGhostBtn.Click += new System.EventHandler(this.compareGhostBtn_Click);
            // 
            // compareGhostLbl
            // 
            this.compareGhostLbl.AutoSize = true;
            this.compareGhostLbl.Location = new System.Drawing.Point(6, 113);
            this.compareGhostLbl.Name = "compareGhostLbl";
            this.compareGhostLbl.Size = new System.Drawing.Size(99, 13);
            this.compareGhostLbl.TabIndex = 29;
            this.compareGhostLbl.Text = "(No ghost selected)";
            // 
            // liveReplayCheck
            // 
            this.liveReplayCheck.AutoSize = true;
            this.liveReplayCheck.Location = new System.Drawing.Point(5, 93);
            this.liveReplayCheck.Name = "liveReplayCheck";
            this.liveReplayCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.liveReplayCheck.Size = new System.Drawing.Size(82, 17);
            this.liveReplayCheck.TabIndex = 28;
            this.liveReplayCheck.Text = "Live Replay";
            this.liveReplayCheck.UseVisualStyleBackColor = true;
            this.liveReplayCheck.CheckedChanged += new System.EventHandler(this.LiveReplayCheck_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(98, 43);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(78, 13);
            this.label17.TabIndex = 27;
            this.label17.Text = "Frame gap size";
            // 
            // frameGapCombo
            // 
            this.frameGapCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.frameGapCombo.FormattingEnabled = true;
            this.frameGapCombo.Items.AddRange(new object[] {
            "1",
            "2"});
            this.frameGapCombo.Location = new System.Drawing.Point(182, 40);
            this.frameGapCombo.Name = "frameGapCombo";
            this.frameGapCombo.Size = new System.Drawing.Size(40, 21);
            this.frameGapCombo.TabIndex = 26;
            // 
            // rapidFireCheck
            // 
            this.rapidFireCheck.AutoSize = true;
            this.rapidFireCheck.Checked = true;
            this.rapidFireCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rapidFireCheck.Location = new System.Drawing.Point(6, 42);
            this.rapidFireCheck.Name = "rapidFireCheck";
            this.rapidFireCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rapidFireCheck.Size = new System.Drawing.Size(74, 17);
            this.rapidFireCheck.TabIndex = 2;
            this.rapidFireCheck.Text = "Rapid Fire";
            this.rapidFireCheck.UseVisualStyleBackColor = true;
            // 
            // illegalInputCheck
            // 
            this.illegalInputCheck.AutoSize = true;
            this.illegalInputCheck.Checked = true;
            this.illegalInputCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.illegalInputCheck.Location = new System.Drawing.Point(6, 19);
            this.illegalInputCheck.Name = "illegalInputCheck";
            this.illegalInputCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.illegalInputCheck.Size = new System.Drawing.Size(80, 17);
            this.illegalInputCheck.TabIndex = 1;
            this.illegalInputCheck.Text = "Illegal Input";
            this.illegalInputCheck.UseVisualStyleBackColor = true;
            // 
            // cheatCheckBtn
            // 
            this.cheatCheckBtn.Enabled = false;
            this.cheatCheckBtn.Location = new System.Drawing.Point(6, 133);
            this.cheatCheckBtn.Name = "cheatCheckBtn";
            this.cheatCheckBtn.Size = new System.Drawing.Size(216, 40);
            this.cheatCheckBtn.TabIndex = 0;
            this.cheatCheckBtn.Text = "Check";
            this.cheatCheckBtn.UseVisualStyleBackColor = true;
            this.cheatCheckBtn.Click += new System.EventHandler(this.cheatCheckBtn_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label18);
            this.groupBox9.Location = new System.Drawing.Point(6, 0);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(228, 43);
            this.groupBox9.TabIndex = 5;
            this.groupBox9.TabStop = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(50, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(122, 13);
            this.label18.TabIndex = 2;
            this.label18.Text = "Check ghosts for cheats";
            // 
            // AboutTab
            // 
            this.AboutTab.Controls.Add(this.linkLabel1);
            this.AboutTab.Controls.Add(this.label12);
            this.AboutTab.Location = new System.Drawing.Point(4, 22);
            this.AboutTab.Name = "AboutTab";
            this.AboutTab.Size = new System.Drawing.Size(240, 234);
            this.AboutTab.TabIndex = 4;
            this.AboutTab.Text = "About";
            this.AboutTab.ToolTipText = "WhatisLoaf\'s youtube channel";
            this.AboutTab.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(3, 128);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(47, 13);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Youtube";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(135, 104);
            this.label12.TabIndex = 0;
            this.label12.Text = "Ghost Input Display\r\nVersion 1.2\r\n\r\nMade by WhatisLoaf, Olifré\r\n\r\nDiscord: \r\nWhat" +
    "IsLoaf#9370\r\nOlifré#0215";
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(729, 381);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(745, 420);
            this.Name = "MainForm";
            this.Text = "Ghost Input Display";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.CustomiseTab.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonSlide)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.GeneralTab.ResumeLayout(false);
            this.GeneralTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.RecordTab.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.CheatTab.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.AboutTab.ResumeLayout(false);
            this.AboutTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.timer = new AccurateTimer(this, new Action(TimerCallback), 16);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer.Stop();

            Config.Save();

            string path = "temp";
            // delete the temp folder
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo("temp");
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                Directory.Delete(path);
            }
        }

        private void TimerCallback()
        {
            this.AdvanceAnimator();
            this.pictureBox1.Invalidate();
            return;
        }

        private bool AdvanceAnimator()
        {
            if (this.Animate)
            {
                if (this.Animator.Update())
                {
                    this.Animator.DrawFrame();
                    return true;
                }
                else
                {
                    this.Animator.Clear();
                    this.Button3_Click(null, null);
                    return false;
                }
            }
            else
            {
                this.Animator.DrawFrame();
                return false;
            }

        }

        private void ReadFile(string fileName)
        {
            Config.GhostFolder = Path.GetDirectoryName(fileName);
            this.Animator.ReadFile(fileName);
            this.GhostLoaded = true;
            // if the record tab is selected
            if (this.tabControl1.SelectedIndex != 2)
            {
                this.button3.Enabled = true;
                this.button4.Enabled = true;
            }
            this.label6.Visible = false;
            this.recordButton.Enabled = true;
            this.cheatCheckBtn.Enabled = true;

            (string time, string name, string controller, int frames) = this.Animator.GetGhostInfo();
            this.label5.Text = Path.GetFileName(fileName);
            this.label7.Text = "Mii: " + name;
            this.label8.Text = "Time: " + time;
            this.ControllerType.Text = "Controller: " + controller;
            this.label5.Visible = true;
            this.label7.Visible = true;
            this.label8.Visible = true;
            this.ControllerType.Visible = true;

            switch (controller)
            {
                case "Classic":
                case "Gamecube":
                    this.LayoutBox.SelectedIndex = 0;
                    break;
                case "Nunchuck":
                    this.LayoutBox.SelectedIndex = 1;
                    break;
                default:
                    break;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = Config.GhostFolder,
                Filter = "rkg files (*.rkg)|*.rkg",
                RestoreDirectory = true,
                Title = ("Open File")
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.ReadFile(ofd.FileName);
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string fileName = files[0];
            // Check if it is a file
            FileAttributes attr = File.GetAttributes(@fileName);
            if (!attr.HasFlag(FileAttributes.Directory))
            {
                if (Path.GetExtension(fileName) == ".rkg")
                {
                    this.ReadFile(fileName);
                }
            }
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(this.Animator.bmp, new Point(0, 0));
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

                }
                else
                {
                    this.Animate = true;
                    this.button3.Text = "Pause";
                    this.button3.BackColor = Color.LightGoldenrodYellow;
                }
            }

        }

        public void SimulateStopClick()
        {
            this.Button4_Click(null, null);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (this.GhostLoaded)
            {
                this.Animate = false;
                this.button3.Text = "Play";
                this.button3.BackColor = Color.LightGreen;
                this.Animator.Clear();
                Graphics g = Graphics.FromImage(this.Animator.bmp);
                g.Clear(Config.BackgroundColour);
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
            this.panel1.Location = new Point(262, 322 + heightChange);

            if (this.pictureBox1.ClientSize.Width > 0 && this.pictureBox1.ClientSize.Height > 0)
            {
                Console.WriteLine(this.pictureBox1.ClientSize.Width);
                Console.WriteLine(this.pictureBox1.ClientSize.Height);
                this.Animator.bmp = new Bitmap(this.pictureBox1.ClientSize.Width, this.pictureBox1.ClientSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
        }

        private void cheatCheckBtn_Click(object sender, EventArgs e)
        {
            if (this.Animator == null)
                return;

            if (this.liveReplayCheck.Checked && !this.ComparisonGhost.HasGhost)
                return;

            GhostReader main = this.Animator.GetGhost();
            List<String> RapidFireMessages = null;
            List<String> IllegalInputMessages = null;
            List<String> LiveReplayMessages = null;

            int gapSize = int.Parse(this.frameGapCombo.SelectedItem.ToString());

            if (this.rapidFireCheck.Checked)
                RapidFireMessages = CheatDetector.DetectRapidFire(main.Trick_inputs, gapSize);
            if (this.illegalInputCheck.Checked)
                IllegalInputMessages = CheatDetector.DetectIllegalInputs(main.Analog_inputs, main.GetControllerType());
            if (this.liveReplayCheck.Checked)
                LiveReplayMessages = CheatDetector.CompareGhost(main, this.ComparisonGhost);

            List<String> messages = this.formFormalReport(RapidFireMessages, IllegalInputMessages, LiveReplayMessages);

            CheatsReportForm crf = new CheatsReportForm(messages);
            crf.Show();
        }

        private List<String> formFormalReport(List<String> RapidFireMessages, List<String> IllegalInputMessages, List<String> LiveReplayMessages)
        {

            List<String> messages = new List<String>();

            if (RapidFireMessages == null && IllegalInputMessages == null && LiveReplayMessages == null)
            {
                messages.Add("No cheats settings were selected.\r\n");
                return messages;
            }

            if (LiveReplayMessages != null)
            {
                messages.Add("LIVE REPLAY: \r\n");
                foreach (String message in LiveReplayMessages)
                    messages.Add(message + "\r\n");
                messages.Add("\r\n");
            }

            if (RapidFireMessages != null)
            {
                messages.Add("RAPID FIRE: \r\n");
                foreach (String message in RapidFireMessages)
                    messages.Add(message + "\r\n");
                messages.Add("\r\n");
            }

            if (IllegalInputMessages != null)
            {
                messages.Add("ILLEGAL INPUTS: \r\n");
                foreach (String message in IllegalInputMessages)
                    messages.Add(message + "\r\n");
                messages.Add("\r\n");
            }

            return messages;

        }

        private void LiveReplayCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.compareGhostBtn.Enabled = this.liveReplayCheck.Checked;
        }

        private void compareGhostBtn_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = Config.GhostFolder,
                Filter = "rkg files (*.rkg)|*.rkg",
                RestoreDirectory = true,
                Title = ("Open File")
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.ComparisonGhost.ReadFile(ofd.FileName);
            }
            else
            {
                return;
            }

            // set lables
            this.compareGhostLbl.Text = this.ComparisonGhost.GetFormalGhostTimeInfo();

        }
    }
}

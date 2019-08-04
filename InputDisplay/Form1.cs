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
            VariableSetup();
            this.Animator = new Animator(62.5);
        }

        private void InitializeComponent()
        {
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
            this.SchemeTab = new System.Windows.Forms.TabPage();
            this.CustomiseTab = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
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
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(262, 322);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(455, 47);
            this.panel1.TabIndex = 7;
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
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Controls.Add(this.CurrentButton);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Location = new System.Drawing.Point(0, 66);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(237, 165);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Controls.Add(this.ButtonScale);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.ButtonColour);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(5, 75);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(226, 84);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ButtonSlide);
            this.panel2.Location = new System.Drawing.Point(109, 52);
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
            this.ButtonScale.Location = new System.Drawing.Point(67, 55);
            this.ButtonScale.MaxLength = 4;
            this.ButtonScale.Name = "ButtonScale";
            this.ButtonScale.Size = new System.Drawing.Size(29, 20);
            this.ButtonScale.TabIndex = 5;
            this.ButtonScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 58);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Scale";
            // 
            // ButtonColour
            // 
            this.ButtonColour.BackColor = System.Drawing.Color.Transparent;
            this.ButtonColour.Location = new System.Drawing.Point(165, 20);
            this.ButtonColour.Name = "ButtonColour";
            this.ButtonColour.Size = new System.Drawing.Size(57, 20);
            this.ButtonColour.TabIndex = 24;
            this.ButtonColour.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Colour";
            // 
            // CurrentButton
            // 
            this.CurrentButton.Enabled = false;
            this.CurrentButton.Location = new System.Drawing.Point(70, 49);
            this.CurrentButton.Name = "CurrentButton";
            this.CurrentButton.ReadOnly = true;
            this.CurrentButton.Size = new System.Drawing.Size(100, 20);
            this.CurrentButton.TabIndex = 26;
            this.CurrentButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(232, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Click on a button to change its colour and scale";
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
            this.label14.Size = new System.Drawing.Size(181, 26);
            this.label14.TabIndex = 0;
            this.label14.Text = "Drag and drop buttons to move them\nHold shift for more precise movement";
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
            this.GeneralTab.Controls.Add(this.checkBox1);
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
            "Nunchuck",
            "Wii Wheel"});
            this.LayoutBox.Location = new System.Drawing.Point(143, 157);
            this.LayoutBox.Name = "LayoutBox";
            this.LayoutBox.Size = new System.Drawing.Size(91, 21);
            this.LayoutBox.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 157);
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
            60,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.NumericUpDown2_ValueChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(6, 211);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(89, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Display Timer";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
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
            this.tabControl1.Controls.Add(this.SchemeTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 55);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(248, 260);
            this.tabControl1.TabIndex = 8;
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
            this.ResumeLayout(false);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.timer = new AccurateTimer(this, new Action(TimerCallback), 16);
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

        public void Redraw()
        {
            this.pictureBox1.Invalidate();
        }

        public void AnimatorScale(string button, double scale)
        {
            //this.Animator.Scale(button, scale);
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

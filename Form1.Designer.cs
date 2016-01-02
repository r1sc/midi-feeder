namespace MidiFeeder
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.radioTypeAxis = new System.Windows.Forms.RadioButton();
            this.radioTypeButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.progessMidiData = new System.Windows.Forms.ProgressBar();
            this.lblMidiControlNumber = new System.Windows.Forms.Label();
            this.lblMidiDevice = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.radioTypeAxis);
            this.groupBox3.Controls.Add(this.radioTypeButton);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.progessMidiData);
            this.groupBox3.Controls.Add(this.lblMidiControlNumber);
            this.groupBox3.Controls.Add(this.lblMidiDevice);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(323, 250);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(6, 209);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(308, 30);
            this.button1.TabIndex = 19;
            this.button1.Text = "Remap";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // radioTypeAxis
            // 
            this.radioTypeAxis.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioTypeAxis.Location = new System.Drawing.Point(176, 145);
            this.radioTypeAxis.Name = "radioTypeAxis";
            this.radioTypeAxis.Size = new System.Drawing.Size(138, 58);
            this.radioTypeAxis.TabIndex = 18;
            this.radioTypeAxis.Text = "Axis";
            this.radioTypeAxis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioTypeAxis.UseVisualStyleBackColor = true;
            this.radioTypeAxis.CheckedChanged += new System.EventHandler(this.radioTypeAxis_CheckedChanged);
            // 
            // radioTypeButton
            // 
            this.radioTypeButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioTypeButton.Location = new System.Drawing.Point(6, 145);
            this.radioTypeButton.Name = "radioTypeButton";
            this.radioTypeButton.Size = new System.Drawing.Size(138, 58);
            this.radioTypeButton.TabIndex = 17;
            this.radioTypeButton.Text = "Button";
            this.radioTypeButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioTypeButton.UseVisualStyleBackColor = true;
            this.radioTypeButton.CheckedChanged += new System.EventHandler(this.radioTypeButton_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Activate a MIDI controller to map it to a joystick function";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // progessMidiData
            // 
            this.progessMidiData.Location = new System.Drawing.Point(6, 63);
            this.progessMidiData.Maximum = 128;
            this.progessMidiData.Name = "progessMidiData";
            this.progessMidiData.Size = new System.Drawing.Size(308, 19);
            this.progessMidiData.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progessMidiData.TabIndex = 11;
            // 
            // lblMidiControlNumber
            // 
            this.lblMidiControlNumber.AutoSize = true;
            this.lblMidiControlNumber.Location = new System.Drawing.Point(3, 38);
            this.lblMidiControlNumber.Name = "lblMidiControlNumber";
            this.lblMidiControlNumber.Size = new System.Drawing.Size(92, 13);
            this.lblMidiControlNumber.TabIndex = 10;
            this.lblMidiControlNumber.Text = "Control #: <none>";
            // 
            // lblMidiDevice
            // 
            this.lblMidiDevice.AutoSize = true;
            this.lblMidiDevice.Location = new System.Drawing.Point(3, 16);
            this.lblMidiDevice.Name = "lblMidiDevice";
            this.lblMidiDevice.Size = new System.Drawing.Size(109, 13);
            this.lblMidiDevice.TabIndex = 9;
            this.lblMidiDevice.Text = "MIDI Device: <none>";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "vJoy MIDI Mapper";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "vJoy MIDI Mapper";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 264);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "vJoy MIDI Mapper";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblMidiControlNumber;
        private System.Windows.Forms.Label lblMidiDevice;
        private System.Windows.Forms.ProgressBar progessMidiData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioTypeAxis;
        private System.Windows.Forms.RadioButton radioTypeButton;

    }
}


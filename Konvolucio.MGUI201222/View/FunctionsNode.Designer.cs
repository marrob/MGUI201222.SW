namespace Konvolucio.MGUI201222.View
{
    partial class FunctionsNode
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FunctionsNode));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.numericUpDownDisplay = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownButton = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.knvIoInputs = new Konvolucio.MGUI201222.Controls.KnvIo16Control();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.knvIoOutputs = new Konvolucio.MGUI201222.Controls.KnvIo8Control();
            this.checkBoxPSP = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplay = new System.Windows.Forms.CheckBox();
            this.textBoxTemp1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTemp2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxTemp3 = new System.Windows.Forms.TextBox();
            this.textBoxTemp4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonStartShutdownCmd = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonPwrLedFlashOff = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownPwrLedFlashPeriod = new System.Windows.Forms.NumericUpDown();
            this.checkBoxPowerLedDimming = new System.Windows.Forms.CheckBox();
            this.buttonPwrLedFlash = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownButton)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPwrLedFlashPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // numericUpDownDisplay
            // 
            this.numericUpDownDisplay.Location = new System.Drawing.Point(382, 325);
            this.numericUpDownDisplay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownDisplay.Name = "numericUpDownDisplay";
            this.numericUpDownDisplay.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownDisplay.TabIndex = 1;
            this.numericUpDownDisplay.ValueChanged += new System.EventHandler(this.numericUpDownDisplay_ValueChanged);
            // 
            // numericUpDownButton
            // 
            this.numericUpDownButton.Location = new System.Drawing.Point(382, 296);
            this.numericUpDownButton.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownButton.Name = "numericUpDownButton";
            this.numericUpDownButton.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownButton.TabIndex = 2;
            this.numericUpDownButton.ValueChanged += new System.EventHandler(this.numericUpDownButton_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(298, 329);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Display Light %";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 300);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Power Button Light %";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox3);
            this.groupBox1.Controls.Add(this.richTextBox2);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.knvIoInputs);
            this.groupBox1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(281, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(585, 236);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bementek";
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox3.Location = new System.Drawing.Point(-272, 73);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(260, 157);
            this.richTextBox3.TabIndex = 12;
            this.richTextBox3.Text = "yvdfgsdfg\n ";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(-278, 79);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(247, 134);
            this.richTextBox2.TabIndex = 12;
            this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(6, 73);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(573, 157);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // knvIoInputs
            // 
            this.knvIoInputs.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.knvIoInputs.Location = new System.Drawing.Point(28, 25);
            this.knvIoInputs.Name = "knvIoInputs";
            this.knvIoInputs.NotAvaliable = false;
            this.knvIoInputs.ReadOnly = false;
            this.knvIoInputs.Size = new System.Drawing.Size(440, 42);
            this.knvIoInputs.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.knvIoOutputs);
            this.groupBox2.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 236);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kimenetek";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(168, 32);
            this.label7.TabIndex = 6;
            this.label7.Text = "DO3: PC-MAIN_ON - relés\r\nDO2: AC_SW";
            // 
            // knvIoOutputs
            // 
            this.knvIoOutputs.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.knvIoOutputs.Location = new System.Drawing.Point(21, 25);
            this.knvIoOutputs.Name = "knvIoOutputs";
            this.knvIoOutputs.NotAvaliable = false;
            this.knvIoOutputs.ReadOnly = false;
            this.knvIoOutputs.Size = new System.Drawing.Size(197, 42);
            this.knvIoOutputs.TabIndex = 5;
            // 
            // checkBoxPSP
            // 
            this.checkBoxPSP.AutoSize = true;
            this.checkBoxPSP.Location = new System.Drawing.Point(271, 245);
            this.checkBoxPSP.Name = "checkBoxPSP";
            this.checkBoxPSP.Size = new System.Drawing.Size(165, 17);
            this.checkBoxPSP.TabIndex = 11;
            this.checkBoxPSP.Text = "Power Button\'s Power Supply";
            this.checkBoxPSP.UseVisualStyleBackColor = true;
            this.checkBoxPSP.CheckedChanged += new System.EventHandler(this.checkBoxPSP_CheckedChanged);
            // 
            // checkBoxDisplay
            // 
            this.checkBoxDisplay.AutoSize = true;
            this.checkBoxDisplay.Location = new System.Drawing.Point(271, 268);
            this.checkBoxDisplay.Name = "checkBoxDisplay";
            this.checkBoxDisplay.Size = new System.Drawing.Size(60, 17);
            this.checkBoxDisplay.TabIndex = 12;
            this.checkBoxDisplay.Text = "Display";
            this.checkBoxDisplay.UseVisualStyleBackColor = true;
            this.checkBoxDisplay.CheckedChanged += new System.EventHandler(this.checkBoxDisplay_CheckedChanged);
            // 
            // textBoxTemp1
            // 
            this.textBoxTemp1.Location = new System.Drawing.Point(170, 242);
            this.textBoxTemp1.Name = "textBoxTemp1";
            this.textBoxTemp1.ReadOnly = true;
            this.textBoxTemp1.Size = new System.Drawing.Size(51, 20);
            this.textBoxTemp1.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Temperature 1 - [TEMP1_AI]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Temperature 2 - [TEMP2_AI]";
            // 
            // textBoxTemp2
            // 
            this.textBoxTemp2.Location = new System.Drawing.Point(170, 268);
            this.textBoxTemp2.Name = "textBoxTemp2";
            this.textBoxTemp2.ReadOnly = true;
            this.textBoxTemp2.Size = new System.Drawing.Size(51, 20);
            this.textBoxTemp2.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 300);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Temperature 3 - [TEMP3_AI]";
            // 
            // textBoxTemp3
            // 
            this.textBoxTemp3.Location = new System.Drawing.Point(170, 296);
            this.textBoxTemp3.Name = "textBoxTemp3";
            this.textBoxTemp3.ReadOnly = true;
            this.textBoxTemp3.Size = new System.Drawing.Size(51, 20);
            this.textBoxTemp3.TabIndex = 17;
            // 
            // textBoxTemp4
            // 
            this.textBoxTemp4.Location = new System.Drawing.Point(170, 322);
            this.textBoxTemp4.Name = "textBoxTemp4";
            this.textBoxTemp4.ReadOnly = true;
            this.textBoxTemp4.Size = new System.Drawing.Size(51, 20);
            this.textBoxTemp4.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 325);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Temperature 4 - [TEMP4_AI]";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonStartShutdownCmd
            // 
            this.buttonStartShutdownCmd.Location = new System.Drawing.Point(12, 350);
            this.buttonStartShutdownCmd.Name = "buttonStartShutdownCmd";
            this.buttonStartShutdownCmd.Size = new System.Drawing.Size(209, 23);
            this.buttonStartShutdownCmd.TabIndex = 21;
            this.buttonStartShutdownCmd.Text = "Start Shutdown Cmd";
            this.buttonStartShutdownCmd.UseVisualStyleBackColor = true;
            this.buttonStartShutdownCmd.Click += new System.EventHandler(this.buttonStartShutdownCmd_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(90, 316);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(8, 8);
            this.button2.TabIndex = 22;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonPwrLedFlashOff);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.numericUpDownPwrLedFlashPeriod);
            this.groupBox3.Controls.Add(this.checkBoxPowerLedDimming);
            this.groupBox3.Controls.Add(this.buttonPwrLedFlash);
            this.groupBox3.Location = new System.Drawing.Point(12, 379);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(439, 47);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Power LED";
            // 
            // buttonPwrLedFlashOff
            // 
            this.buttonPwrLedFlashOff.Location = new System.Drawing.Point(332, 13);
            this.buttonPwrLedFlashOff.Name = "buttonPwrLedFlashOff";
            this.buttonPwrLedFlashOff.Size = new System.Drawing.Size(98, 23);
            this.buttonPwrLedFlashOff.TabIndex = 29;
            this.buttonPwrLedFlashOff.Text = "Power LED Off";
            this.buttonPwrLedFlashOff.UseVisualStyleBackColor = true;
            this.buttonPwrLedFlashOff.Click += new System.EventHandler(this.buttonPwrLedFlashOff_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(174, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "ms";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(65, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Period:";
            // 
            // numericUpDownPwrLedFlashPeriod
            // 
            this.numericUpDownPwrLedFlashPeriod.Location = new System.Drawing.Point(106, 16);
            this.numericUpDownPwrLedFlashPeriod.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownPwrLedFlashPeriod.Name = "numericUpDownPwrLedFlashPeriod";
            this.numericUpDownPwrLedFlashPeriod.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownPwrLedFlashPeriod.TabIndex = 26;
            this.numericUpDownPwrLedFlashPeriod.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // checkBoxPowerLedDimming
            // 
            this.checkBoxPowerLedDimming.AutoSize = true;
            this.checkBoxPowerLedDimming.Location = new System.Drawing.Point(12, 17);
            this.checkBoxPowerLedDimming.Name = "checkBoxPowerLedDimming";
            this.checkBoxPowerLedDimming.Size = new System.Drawing.Size(47, 17);
            this.checkBoxPowerLedDimming.TabIndex = 25;
            this.checkBoxPowerLedDimming.Text = "Dim.";
            this.checkBoxPowerLedDimming.UseVisualStyleBackColor = true;
            // 
            // buttonPwrLedFlash
            // 
            this.buttonPwrLedFlash.Location = new System.Drawing.Point(200, 13);
            this.buttonPwrLedFlash.Name = "buttonPwrLedFlash";
            this.buttonPwrLedFlash.Size = new System.Drawing.Size(126, 23);
            this.buttonPwrLedFlash.TabIndex = 24;
            this.buttonPwrLedFlash.Text = "Power LED Flash Cmd";
            this.buttonPwrLedFlash.UseVisualStyleBackColor = true;
            this.buttonPwrLedFlash.Click += new System.EventHandler(this.buttonPwrLedFlash_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(0, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(0, 0);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 25;
            // 
            // FunctionsNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonStartShutdownCmd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxTemp4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxTemp3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxTemp2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxTemp1);
            this.Controls.Add(this.checkBoxDisplay);
            this.Controls.Add(this.checkBoxPSP);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownButton);
            this.Controls.Add(this.numericUpDownDisplay);
            this.Name = "FunctionsNode";
            this.Size = new System.Drawing.Size(882, 620);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownButton)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPwrLedFlashPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.NumericUpDown numericUpDownDisplay;
        private System.Windows.Forms.NumericUpDown numericUpDownButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Controls.KnvIo8Control knvIoOutputs;
        private Controls.KnvIo16Control knvIoInputs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox checkBoxPSP;
        private System.Windows.Forms.CheckBox checkBoxDisplay;
        private System.Windows.Forms.TextBox textBoxTemp1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTemp2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxTemp3;
        private System.Windows.Forms.TextBox textBoxTemp4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonStartShutdownCmd;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonPwrLedFlashOff;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownPwrLedFlashPeriod;
        private System.Windows.Forms.CheckBox checkBoxPowerLedDimming;
        private System.Windows.Forms.Button buttonPwrLedFlash;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}

namespace Konvolucio.MGUI201222.View
{
    partial class DacNode
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLrck = new System.Windows.Forms.TextBox();
            this.textBoxBclk = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxUpdateTime = new System.Windows.Forms.TextBox();
            this.textBoxInputs = new System.Windows.Forms.TextBox();
            this.comboBoxModes = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxXMOSMute = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxSRCPowerDown = new System.Windows.Forms.CheckBox();
            this.checkBoxSRCMute = new System.Windows.Forms.CheckBox();
            this.checkBoxSRCBypass = new System.Windows.Forms.CheckBox();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.trackBarVol2 = new System.Windows.Forms.TrackBar();
            this.trackBarVol1 = new System.Windows.Forms.TrackBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVol2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVol1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "LRCK:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "BCLK:";
            // 
            // textBoxLrck
            // 
            this.textBoxLrck.Location = new System.Drawing.Point(59, 19);
            this.textBoxLrck.Name = "textBoxLrck";
            this.textBoxLrck.ReadOnly = true;
            this.textBoxLrck.Size = new System.Drawing.Size(100, 20);
            this.textBoxLrck.TabIndex = 3;
            // 
            // textBoxBclk
            // 
            this.textBoxBclk.Location = new System.Drawing.Point(59, 42);
            this.textBoxBclk.Name = "textBoxBclk";
            this.textBoxBclk.ReadOnly = true;
            this.textBoxBclk.Size = new System.Drawing.Size(100, 20);
            this.textBoxBclk.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 315);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ez a panel ilyen gyakran frissül:";
            // 
            // textBoxUpdateTime
            // 
            this.textBoxUpdateTime.Location = new System.Drawing.Point(210, 308);
            this.textBoxUpdateTime.Name = "textBoxUpdateTime";
            this.textBoxUpdateTime.ReadOnly = true;
            this.textBoxUpdateTime.Size = new System.Drawing.Size(100, 20);
            this.textBoxUpdateTime.TabIndex = 6;
            // 
            // textBoxInputs
            // 
            this.textBoxInputs.Location = new System.Drawing.Point(8, 15);
            this.textBoxInputs.Name = "textBoxInputs";
            this.textBoxInputs.ReadOnly = true;
            this.textBoxInputs.Size = new System.Drawing.Size(186, 20);
            this.textBoxInputs.TabIndex = 7;
            // 
            // comboBoxModes
            // 
            this.comboBoxModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxModes.FormattingEnabled = true;
            this.comboBoxModes.Location = new System.Drawing.Point(17, 17);
            this.comboBoxModes.Name = "comboBoxModes";
            this.comboBoxModes.Size = new System.Drawing.Size(572, 33);
            this.comboBoxModes.TabIndex = 9;
            this.comboBoxModes.SelectionChangeCommitted += new System.EventHandler(this.comboBoxModes_SelectionChangeCommitted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxBclk);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxLrck);
            this.groupBox1.Location = new System.Drawing.Point(12, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(165, 89);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Frequnecy Measurment";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxXMOSMute);
            this.groupBox2.Controls.Add(this.textBoxInputs);
            this.groupBox2.Location = new System.Drawing.Point(183, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 89);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "XMOS";
            // 
            // checkBoxXMOSMute
            // 
            this.checkBoxXMOSMute.AutoCheck = false;
            this.checkBoxXMOSMute.AutoSize = true;
            this.checkBoxXMOSMute.Location = new System.Drawing.Point(8, 45);
            this.checkBoxXMOSMute.Name = "checkBoxXMOSMute";
            this.checkBoxXMOSMute.Size = new System.Drawing.Size(50, 17);
            this.checkBoxXMOSMute.TabIndex = 9;
            this.checkBoxXMOSMute.Text = "Mute";
            this.checkBoxXMOSMute.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxSRCPowerDown);
            this.groupBox3.Controls.Add(this.checkBoxSRCMute);
            this.groupBox3.Controls.Add(this.checkBoxSRCBypass);
            this.groupBox3.Location = new System.Drawing.Point(389, 57);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 89);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SRC4193";
            // 
            // checkBoxSRCPowerDown
            // 
            this.checkBoxSRCPowerDown.AutoSize = true;
            this.checkBoxSRCPowerDown.Location = new System.Drawing.Point(6, 22);
            this.checkBoxSRCPowerDown.Name = "checkBoxSRCPowerDown";
            this.checkBoxSRCPowerDown.Size = new System.Drawing.Size(94, 17);
            this.checkBoxSRCPowerDown.TabIndex = 3;
            this.checkBoxSRCPowerDown.Text = "Power Down#";
            this.checkBoxSRCPowerDown.UseVisualStyleBackColor = true;
            this.checkBoxSRCPowerDown.CheckedChanged += new System.EventHandler(this.checkBoxSRCPowerDown_CheckedChanged);
            // 
            // checkBoxSRCMute
            // 
            this.checkBoxSRCMute.AutoSize = true;
            this.checkBoxSRCMute.Location = new System.Drawing.Point(7, 45);
            this.checkBoxSRCMute.Name = "checkBoxSRCMute";
            this.checkBoxSRCMute.Size = new System.Drawing.Size(50, 17);
            this.checkBoxSRCMute.TabIndex = 2;
            this.checkBoxSRCMute.Text = "Mute";
            this.checkBoxSRCMute.UseVisualStyleBackColor = true;
            this.checkBoxSRCMute.CheckedChanged += new System.EventHandler(this.checkBoxMute_CheckedChanged);
            // 
            // checkBoxSRCBypass
            // 
            this.checkBoxSRCBypass.AutoSize = true;
            this.checkBoxSRCBypass.Location = new System.Drawing.Point(7, 66);
            this.checkBoxSRCBypass.Name = "checkBoxSRCBypass";
            this.checkBoxSRCBypass.Size = new System.Drawing.Size(60, 17);
            this.checkBoxSRCBypass.TabIndex = 1;
            this.checkBoxSRCBypass.Text = "Bypass";
            this.checkBoxSRCBypass.UseVisualStyleBackColor = true;
            this.checkBoxSRCBypass.CheckedChanged += new System.EventHandler(this.checkBoxSRCBypass_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.trackBarVol1);
            this.groupBox4.Controls.Add(this.trackBarVol2);
            this.groupBox4.Location = new System.Drawing.Point(12, 152);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(577, 126);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "BD34301";
            // 
            // trackBarVol2
            // 
            this.trackBarVol2.Location = new System.Drawing.Point(350, 75);
            this.trackBarVol2.Maximum = 256;
            this.trackBarVol2.Name = "trackBarVol2";
            this.trackBarVol2.Size = new System.Drawing.Size(221, 45);
            this.trackBarVol2.TabIndex = 0;
            this.trackBarVol2.Scroll += new System.EventHandler(this.trackBarVol2_Scroll);
            // 
            // trackBarVol1
            // 
            this.trackBarVol1.Location = new System.Drawing.Point(350, 19);
            this.trackBarVol1.Maximum = 256;
            this.trackBarVol1.Name = "trackBarVol1";
            this.trackBarVol1.Size = new System.Drawing.Size(221, 45);
            this.trackBarVol1.TabIndex = 1;
            this.trackBarVol1.Scroll += new System.EventHandler(this.trackBarVol1_Scroll);
            // 
            // DacNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxModes);
            this.Controls.Add(this.textBoxUpdateTime);
            this.Controls.Add(this.label3);
            this.Name = "DacNode";
            this.Size = new System.Drawing.Size(797, 366);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVol2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVol1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLrck;
        private System.Windows.Forms.TextBox textBoxBclk;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxUpdateTime;
        private System.Windows.Forms.TextBox textBoxInputs;
        private System.Windows.Forms.ComboBox comboBoxModes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxSRCBypass;
        private System.Windows.Forms.CheckBox checkBoxSRCMute;
        private System.Windows.Forms.CheckBox checkBoxSRCPowerDown;
        private System.Windows.Forms.CheckBox checkBoxXMOSMute;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TrackBar trackBarVol1;
        private System.Windows.Forms.TrackBar trackBarVol2;
    }
}

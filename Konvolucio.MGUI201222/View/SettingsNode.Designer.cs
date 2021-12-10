namespace Konvolucio.MCEL181123.Calib.View
{
    partial class SettingsNode
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
            this.checkBoxOpenAfterStart = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxUpTimeCounterPeriodicUpdateCheck = new System.Windows.Forms.CheckBox();
            this.numericPeriodicUpdate = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericPeriodicUpdate)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxOpenAfterStart
            // 
            this.checkBoxOpenAfterStart.AutoSize = true;
            this.checkBoxOpenAfterStart.Location = new System.Drawing.Point(5, 28);
            this.checkBoxOpenAfterStart.Name = "checkBoxOpenAfterStart";
            this.checkBoxOpenAfterStart.Size = new System.Drawing.Size(250, 17);
            this.checkBoxOpenAfterStart.TabIndex = 3;
            this.checkBoxOpenAfterStart.Text = "Alkalmazás inditása után automatikus protnyitás";
            this.checkBoxOpenAfterStart.UseVisualStyleBackColor = true;
            this.checkBoxOpenAfterStart.CheckedChanged += new System.EventHandler(this.checkBoxOpenAfterStart_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 104);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 21);
            this.button1.TabIndex = 8;
            this.button1.Text = "Teszt";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // checkBoxUpTimeCounterPeriodicUpdateCheck
            // 
            this.checkBoxUpTimeCounterPeriodicUpdateCheck.AutoSize = true;
            this.checkBoxUpTimeCounterPeriodicUpdateCheck.Location = new System.Drawing.Point(5, 51);
            this.checkBoxUpTimeCounterPeriodicUpdateCheck.Name = "checkBoxUpTimeCounterPeriodicUpdateCheck";
            this.checkBoxUpTimeCounterPeriodicUpdateCheck.Size = new System.Drawing.Size(199, 17);
            this.checkBoxUpTimeCounterPeriodicUpdateCheck.TabIndex = 9;
            this.checkBoxUpTimeCounterPeriodicUpdateCheck.Text = "UpTime Counter periodikus frissítése";
            this.checkBoxUpTimeCounterPeriodicUpdateCheck.UseVisualStyleBackColor = true;
            this.checkBoxUpTimeCounterPeriodicUpdateCheck.CheckedChanged += new System.EventHandler(this.checkBoxUpTimeCounterPeriodicUpdateCheck_CheckedChanged);
            // 
            // numericPeriodicUpdate
            // 
            this.numericPeriodicUpdate.Location = new System.Drawing.Point(167, 74);
            this.numericPeriodicUpdate.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericPeriodicUpdate.Name = "numericPeriodicUpdate";
            this.numericPeriodicUpdate.Size = new System.Drawing.Size(74, 20);
            this.numericPeriodicUpdate.TabIndex = 10;
            this.numericPeriodicUpdate.ValueChanged += new System.EventHandler(this.numericPeriodicUpdate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Felület firssítési gyakorisága";
            // 
            // SettingsNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericPeriodicUpdate);
            this.Controls.Add(this.checkBoxUpTimeCounterPeriodicUpdateCheck);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxOpenAfterStart);
            this.Name = "SettingsNode";
            this.Size = new System.Drawing.Size(705, 300);
            ((System.ComponentModel.ISupportInitialize)(this.numericPeriodicUpdate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBoxOpenAfterStart;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxUpTimeCounterPeriodicUpdateCheck;
        private System.Windows.Forms.NumericUpDown numericPeriodicUpdate;
        private System.Windows.Forms.Label label1;
    }
}

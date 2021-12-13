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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.numericUpDownDisplay = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownButton = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownButton)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // numericUpDownDisplay
            // 
            this.numericUpDownDisplay.Location = new System.Drawing.Point(3, 123);
            this.numericUpDownDisplay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownDisplay.Name = "numericUpDownDisplay";
            this.numericUpDownDisplay.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownDisplay.TabIndex = 1;
            this.numericUpDownDisplay.ValueChanged += new System.EventHandler(this.numericUpDownDisplay_ValueChanged);
            // 
            // numericUpDownButton
            // 
            this.numericUpDownButton.Location = new System.Drawing.Point(3, 162);
            this.numericUpDownButton.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownButton.Name = "numericUpDownButton";
            this.numericUpDownButton.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownButton.TabIndex = 2;
            this.numericUpDownButton.ValueChanged += new System.EventHandler(this.numericUpDownButton_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Display Light %";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Power Button Light %";
            // 
            // FunctionsNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownButton);
            this.Controls.Add(this.numericUpDownDisplay);
            this.Name = "FunctionsNode";
            this.Size = new System.Drawing.Size(882, 430);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.NumericUpDown numericUpDownDisplay;
        private System.Windows.Forms.NumericUpDown numericUpDownButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

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
            this.knvIoInputs = new Konvolucio.MGUI201222.Controls.KnvIo16Control();
            this.knvIoOutputs = new Konvolucio.MGUI201222.Controls.KnvIo8Control();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownButton)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // numericUpDownDisplay
            // 
            this.numericUpDownDisplay.Location = new System.Drawing.Point(3, 18);
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
            this.numericUpDownButton.Location = new System.Drawing.Point(147, 18);
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
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Display Light %";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Power Button Light %";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.knvIoInputs);
            this.groupBox1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(284, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 204);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bementek";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.knvIoOutputs);
            this.groupBox2.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(6, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 204);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kimenetek";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(6, 73);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(475, 125);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = "DI8:BTN_ON_DI - Bekapcsolás";
            // 
            // FunctionsNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownButton);
            this.Controls.Add(this.numericUpDownDisplay);
            this.Name = "FunctionsNode";
            this.Size = new System.Drawing.Size(882, 430);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownButton)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
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
    }
}

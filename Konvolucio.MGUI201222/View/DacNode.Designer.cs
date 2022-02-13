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
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxModes = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
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
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "LRCK:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "BCLK:";
            // 
            // textBoxLrck
            // 
            this.textBoxLrck.Location = new System.Drawing.Point(52, 29);
            this.textBoxLrck.Name = "textBoxLrck";
            this.textBoxLrck.ReadOnly = true;
            this.textBoxLrck.Size = new System.Drawing.Size(100, 20);
            this.textBoxLrck.TabIndex = 3;
            // 
            // textBoxBclk
            // 
            this.textBoxBclk.Location = new System.Drawing.Point(52, 55);
            this.textBoxBclk.Name = "textBoxBclk";
            this.textBoxBclk.ReadOnly = true;
            this.textBoxBclk.Size = new System.Drawing.Size(100, 20);
            this.textBoxBclk.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 311);
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
            this.textBoxInputs.Location = new System.Drawing.Point(64, 191);
            this.textBoxInputs.Name = "textBoxInputs";
            this.textBoxInputs.ReadOnly = true;
            this.textBoxInputs.Size = new System.Drawing.Size(100, 20);
            this.textBoxInputs.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Inputs:";
            // 
            // comboBoxModes
            // 
            this.comboBoxModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxModes.FormattingEnabled = true;
            this.comboBoxModes.Location = new System.Drawing.Point(17, 17);
            this.comboBoxModes.Name = "comboBoxModes";
            this.comboBoxModes.Size = new System.Drawing.Size(551, 33);
            this.comboBoxModes.TabIndex = 9;
            this.comboBoxModes.SelectionChangeCommitted += new System.EventHandler(this.comboBoxModes_SelectionChangeCommitted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxBclk);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxLrck);
            this.groupBox1.Location = new System.Drawing.Point(12, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(165, 89);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Frequnecy Measurment";
            // 
            // DacNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxModes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxInputs);
            this.Controls.Add(this.textBoxUpdateTime);
            this.Controls.Add(this.label3);
            this.Name = "DacNode";
            this.Size = new System.Drawing.Size(797, 366);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxModes;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

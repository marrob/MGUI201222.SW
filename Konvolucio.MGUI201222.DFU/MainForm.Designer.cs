namespace Konvolucio.MGUI201222.DFU
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textIntFilePath = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textExtFilePath = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.knvRichTextBox1 = new Konvolucio.MGUI201222.DFU.Controls.KnvRichTextBox();
            this.labelProgressStatus = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 263);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(746, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(0, 17);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.FlatAppearance.BorderSize = 0;
            this.buttonBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.buttonBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBrowse.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonBrowse.Location = new System.Drawing.Point(656, 27);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(82, 23);
            this.buttonBrowse.TabIndex = 8;
            this.buttonBrowse.Text = "Int Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.ButtonIntBrowse_Click);
            // 
            // textIntFilePath
            // 
            this.textIntFilePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textIntFilePath.Location = new System.Drawing.Point(12, 37);
            this.textIntFilePath.Name = "textIntFilePath";
            this.textIntFilePath.ReadOnly = true;
            this.textIntFilePath.Size = new System.Drawing.Size(638, 13);
            this.textIntFilePath.TabIndex = 16;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(3, 232);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(738, 23);
            this.progressBar.TabIndex = 18;
            // 
            // textExtFilePath
            // 
            this.textExtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textExtFilePath.Location = new System.Drawing.Point(12, 61);
            this.textExtFilePath.Name = "textExtFilePath";
            this.textExtFilePath.ReadOnly = true;
            this.textExtFilePath.Size = new System.Drawing.Size(638, 13);
            this.textExtFilePath.TabIndex = 16;
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button3.Location = new System.Drawing.Point(656, 56);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Ext Browse";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.ButtonExtBrowse_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(746, 24);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // knvRichTextBox1
            // 
            this.knvRichTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.knvRichTextBox1.BackgroundText = "TRACEING";
            this.knvRichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.knvRichTextBox1.Location = new System.Drawing.Point(3, 80);
            this.knvRichTextBox1.Name = "knvRichTextBox1";
            this.knvRichTextBox1.Size = new System.Drawing.Size(741, 146);
            this.knvRichTextBox1.TabIndex = 0;
            this.knvRichTextBox1.Text = "";
            // 
            // labelProgressStatus
            // 
            this.labelProgressStatus.AutoSize = true;
            this.labelProgressStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgressStatus.Location = new System.Drawing.Point(6, 235);
            this.labelProgressStatus.Name = "labelProgressStatus";
            this.labelProgressStatus.Size = new System.Drawing.Size(23, 17);
            this.labelProgressStatus.TabIndex = 21;
            this.labelProgressStatus.Text = "---";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 285);
            this.Controls.Add(this.labelProgressStatus);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.knvRichTextBox1);
            this.Controls.Add(this.textIntFilePath);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.textExtFilePath);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripSplitButton1;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textIntFilePath;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox textExtFilePath;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private Controls.KnvRichTextBox knvRichTextBox1;
        private System.Windows.Forms.Label labelProgressStatus;
    }
}


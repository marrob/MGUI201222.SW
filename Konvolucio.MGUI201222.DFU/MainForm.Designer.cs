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
            this.textIntFilePath = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textExtFilePath = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.labelProgressStatus = new System.Windows.Forms.Label();
            this.knvRichTextBox1 = new Konvolucio.MGUI201222.DFU.Controls.KnvRichTextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 295);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(748, 22);
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
            // textIntFilePath
            // 
            this.textIntFilePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textIntFilePath.Location = new System.Drawing.Point(3, 37);
            this.textIntFilePath.Name = "textIntFilePath";
            this.textIntFilePath.Size = new System.Drawing.Size(669, 13);
            this.textIntFilePath.TabIndex = 16;
            this.textIntFilePath.TextChanged += new System.EventHandler(this.textIntFilePath_TextChanged);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(0, 252);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(741, 40);
            this.progressBar.TabIndex = 18;
            // 
            // textExtFilePath
            // 
            this.textExtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textExtFilePath.Location = new System.Drawing.Point(3, 61);
            this.textExtFilePath.Name = "textExtFilePath";
            this.textExtFilePath.Size = new System.Drawing.Size(669, 13);
            this.textExtFilePath.TabIndex = 16;
            this.textExtFilePath.TextChanged += new System.EventHandler(this.textExtFilePath_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(748, 24);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // labelProgressStatus
            // 
            this.labelProgressStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgressStatus.Location = new System.Drawing.Point(8, 262);
            this.labelProgressStatus.Name = "labelProgressStatus";
            this.labelProgressStatus.Size = new System.Drawing.Size(728, 20);
            this.labelProgressStatus.TabIndex = 21;
            this.labelProgressStatus.Text = "---";
            this.labelProgressStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // knvRichTextBox1
            // 
            this.knvRichTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.knvRichTextBox1.BackgroundText = "DEVICE FIRMWARE UPGRADE";
            this.knvRichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.knvRichTextBox1.Location = new System.Drawing.Point(3, 85);
            this.knvRichTextBox1.Name = "knvRichTextBox1";
            this.knvRichTextBox1.Size = new System.Drawing.Size(741, 161);
            this.knvRichTextBox1.TabIndex = 0;
            this.knvRichTextBox1.Text = "";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.FlatAppearance.BorderSize = 0;
            this.buttonBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.buttonBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBrowse.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonBrowse.Image = global::Konvolucio.MGUI201222.DFU.Properties.Resources.folder_explorer16x16;
            this.buttonBrowse.Location = new System.Drawing.Point(678, 32);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(66, 23);
            this.buttonBrowse.TabIndex = 8;
            this.buttonBrowse.Text = "Int";
            this.buttonBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.ButtonIntBrowse_Click);
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button3.Image = global::Konvolucio.MGUI201222.DFU.Properties.Resources.folder_explorer16x16;
            this.button3.Location = new System.Drawing.Point(678, 56);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(66, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Ext";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.ButtonExtBrowse_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 317);
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


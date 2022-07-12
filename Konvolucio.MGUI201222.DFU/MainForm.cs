
namespace Konvolucio.MGUI201222.DFU
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Properties;
    using Controls;


    public interface IMainForm
    {
        event EventHandler Shown;
        event FormClosedEventHandler FormClosed;
        event FormClosingEventHandler FormClosing;

        string Text { get; set; }
        ToolStripItem[] MenuBar { set; }

        string ExtFlashFilePath { get; set; }
        string IntFlashFilePath { get; set; }

        KnvRichTextBox RichTextBoxTrace { get; }

        int ProgressValue { get; set; }
        string ProgressStatus { get; set; }

        ToolStripItem[] StatusBar { set; }

        void ResultReset();
        void ResultFailed();
        void ResultPassed();
    }

    public partial class MainForm : Form, IMainForm
    {
        public ToolStripItem[] MenuBar
        {
            set { menuStrip1.Items.AddRange(value); }
        }

        public string ExtFlashFilePath {
            get { return textExtFilePath.Text; }
            set { textExtFilePath.Text = value; }
        }

        public string IntFlashFilePath {
            get { return textIntFilePath.Text; }
            set { textIntFilePath.Text = value; }
        }

        public KnvRichTextBox RichTextBoxTrace
        {
            get { return knvRichTextBox1; }
        }

        public int ProgressValue
        {
            get { return progressBar.Value; }
            set { progressBar.Value = value; }
        }

        public string ProgressStatus
        {
            get { return labelProgressStatus.Text; }
            set { labelProgressStatus.Text = value; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        public ToolStripItem[] StatusBar
        {
            set { statusStrip1.Items.AddRange(value); }
        }

        public void ResultReset()
        {
            labelProgressStatus.BackColor = System.Drawing.SystemColors.Control;
        }
        public void ResultFailed()
        {
            labelProgressStatus.BackColor = Color.Red;
        }
        public void ResultPassed()
        {
            labelProgressStatus.BackColor = Color.LimeGreen;
        }

        private void ButtonExtBrowse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (string.IsNullOrEmpty(Settings.Default.ExtFirmwareFilePath))
                ofd.InitialDirectory = Settings.Default.ExtFirmwareFilePath;
            else
                ofd.InitialDirectory = "";
            ofd.Filter = AppConstants.FileFilter;
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.ExtFirmwareFilePath = ofd.FileName;
                textExtFilePath.Text = ofd.FileName;
            }
        }

        private void ButtonIntBrowse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (string.IsNullOrEmpty(Settings.Default.IntFirmwareFilePath))
                ofd.InitialDirectory = Settings.Default.IntFirmwareFilePath;
            else
                ofd.InitialDirectory = "";
            ofd.Filter = AppConstants.FileFilter;
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.IntFirmwareFilePath = ofd.FileName;
                textIntFilePath.Text = ofd.FileName;
            }
        }
    }
}

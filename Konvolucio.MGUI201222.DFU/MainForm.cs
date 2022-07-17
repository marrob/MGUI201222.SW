
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

        void ProgressUpdate(string text, Color color, int progress);

        ToolStripItem[] StatusBar { set; }

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

        public void ProgressUpdate(string text, Color color, int progress)
        {
            labelProgressStatus.BackColor = color;
            labelProgressStatus.Text = text;
            progressBar.Value = progress;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        public ToolStripItem[] StatusBar
        {
            set { statusStrip1.Items.AddRange(value); }
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

        private void textExtFilePath_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ExtFirmwareFilePath = textExtFilePath.Text;
        }

        private void textIntFilePath_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.IntFirmwareFilePath = textIntFilePath.Text;
        }
    }
}

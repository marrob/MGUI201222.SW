
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
        event EventHandler BtnConnectClick;
        event EventHandler<int> DeviceRestart;
        event EventHandler Disposed;

        event EventHandler WriteEventHandler;
        event EventHandler FileBrowseEventHandler;
        event EventHandler ShowConfiguration;

        ToolStripItem[] MenuBar { set; }

        KnvRichTextBox RichTextBoxTrace { get; }

        string Text { get; set; }
        string FileName { get; set; }








        string Version { get; set; }
    }

    public partial class MainForm : Form, IMainForm
    {
        public event EventHandler BtnConnectClick;

        public event EventHandler<int> DeviceRestart;
        public event EventHandler ShowConfiguration;

        public string Version 
        {
            get { return toolStripStatusLabelVersion.Text; }
            set { toolStripStatusLabelVersion.Text = value; }
        }

        public ConnectionStatusTypes ConnectionStatus;

        public event EventHandler WriteEventHandler;
        public event EventHandler FileBrowseEventHandler
        {
            add { buttonBrowse.Click += value; }
            remove { buttonBrowse.Click -= value; }
        }

        public KnvRichTextBox RichTextBoxTrace
        {
            get { return knvRichTextBox1; }
        }

        public ToolStripItem[] MenuBar
        {
            set { menuStrip1.Items.AddRange(value); }
        }
        public int PoregressValue;


        public string FileName
        {
            get { return textFileName.Text; }
            set { textFileName.Text = value; }
        }
        
        bool wrtieEnabled;
        public bool WriteEnabled;


        public MainForm() 
        {
           InitializeComponent();   
        }

        private void TextFileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void ButtonConfig_Click(object sender, EventArgs e)
        {
            ShowConfiguration?.Invoke(sender, EventArgs.Empty);
        }

        private void ButtonLogs_Click(object sender, EventArgs e)
        {

        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {

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
            }
        }

    }
}

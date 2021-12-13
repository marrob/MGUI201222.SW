
namespace Konvolucio.MGUI201222
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using View;
    using Common;
    using Properties;
    using Controls;

    public interface IMainForm : IWindowLayoutRestoring 
    {

        event EventHandler Shown;
        event FormClosedEventHandler FormClosed;
        event FormClosingEventHandler FormClosing;
        event EventHandler Disposed;

        string Text { get; set; }
        ToolStripItem[] MenuBar { set; }
        ToolStripItem[] StatusBar { set; }
        bool AlwaysOnTop { get; set; }
        KnvRichTextBox RichTextBoxTrace { get; }

        //event KeyEventHandler KeyUp;
        //event HelpEventHandler HelpRequested; /*????*/

        //void CursorWait();
        //void CursorDefault();

        
    }



    public partial class MainForm : Form, IMainForm
    {
        public ToolStripItem[] MenuBar
        {
            set { menuStrip1.Items.AddRange(value); }
        }

        public ToolStripItem[] StatusBar
        {
            set { statusStrip1.Items.AddRange(value); }
        }

        public bool AlwaysOnTop
        {
            get { return this.TopMost; }
            set { this.TopMost = value; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        public KnvRichTextBox RichTextBoxTrace 
        {
            get
            {
                return knvRichTextBox1; 
            }
        }

        public void LayoutSave()
        {
            Settings.Default.MainFormLocation = Location;
            Settings.Default.MainFormWindowState = WindowState;
            Settings.Default.MainFormSize = Size;
            Settings.Default.MainTree_SplitterDistance = splitContainer1.SplitterDistance;


        }

        public void LayoutRestore()
        {
            //Location = Settings.Default.MainFormLocation;
            //WindowState = Settings.Default.MainFormWindowState;
            //Size = Settings.Default.MainFormSize;
            //splitContainer1.SplitterDistance = Settings.Default.MainTree_SplitterDistance;


        }

        private void KnvRichTextBox1_TextChanged(object sender, EventArgs e)
        {
            knvRichTextBox1.SelectionStart = knvRichTextBox1.Text.Length;
            knvRichTextBox1.ScrollToCaret();
        }

        private void MainView1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

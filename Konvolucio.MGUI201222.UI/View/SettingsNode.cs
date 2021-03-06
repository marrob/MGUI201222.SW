

namespace Konvolucio.MGUI201222.View
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Properties;
    using IO;
    using Events;

    public partial class SettingsNode : UserControl, IUIPanelProperties
    {
        public SettingsNode()
        {
            InitializeComponent();
            this.Name = "SettingsNode";
            
        }

        public void UserEnter()
        {
            checkBoxOpenAfterStart.Checked = Settings.Default.OpenAfterStartUp;
            checkBoxUpTimeCounterPeriodicUpdateCheck.Checked = Settings.Default.UpTimeCounterPeriodicUpdate;
            numericPeriodicUpdate.Value = Settings.Default.GuiRefreshRateMs;
            checkBoxTracing.Checked = Settings.Default.TracingEnabled;
        }
    
        private void button1_Click_1(object sender, EventArgs e)
        {
            GuiIo.Instance.Test();
        }

        private void checkBoxUpTimeCounterPeriodicUpdateCheck_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.UpTimeCounterPeriodicUpdate = checkBoxUpTimeCounterPeriodicUpdateCheck.Checked;
        }

        public void UserLeave()
        {

        }

        private void checkBoxOpenAfterStart_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.OpenAfterStartUp = checkBoxOpenAfterStart.Checked;
        }

        private void numericPeriodicUpdate_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.GuiRefreshRateMs = (int)numericPeriodicUpdate.Value;
        }

        private void checkBoxTracing_CheckedChanged(object sender, EventArgs e)
        {
            if (Settings.Default.TracingEnabled != checkBoxTracing.Checked)
            {
                Settings.Default.TracingEnabled = checkBoxTracing.Checked;
                EventAggregator.Instance.Publish<TracingChanged>(new TracingChanged(checkBoxTracing.Checked));
            }
        }
    }
}

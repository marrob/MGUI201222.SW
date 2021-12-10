

namespace Konvolucio.MCEL181123.Calib.View
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
        }
    
        private void button1_Click_1(object sender, EventArgs e)
        {
            DevIoSrv.Instance.Test();
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
    }
}

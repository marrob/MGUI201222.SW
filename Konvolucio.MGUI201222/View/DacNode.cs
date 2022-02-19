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
    using Controls;
    using Events;
    using System.Diagnostics;
    using Konvolucio.DACcomm;


    public partial class DacNode : UserControl, IUIPanelProperties
    {

        public DacNode()
        {
            InitializeComponent();
            comboBoxModes.DataSource = Enum.GetValues(typeof(DacIoSrv.Modes));

            this.Name = "DacNode";

            EventAggregator.Instance.Subscribe((Action<ConnectionChangedAppEvent>)(e =>
            {
                Enabled = e.IsOpen;
            }));

        }



        public void UserEnter()
        { 
            timer1.Start();
            comboBoxModes.SelectedItem = DacIoSrv.Instance.Mode;
            checkBoxSRCBypass.Checked = DacIoSrv.Instance.SRCBypass;
            checkBoxSRCMute.Checked = DacIoSrv.Instance.SRCMute;
            checkBoxSRCPowerDown.Checked = DacIoSrv.Instance.SRCPowerDown;
            trackBarVol1.Value = DacIoSrv.Instance.Volume1;
            trackBarVol2.Value = DacIoSrv.Instance.Volume2;
        }

        public void UserLeave()
        {

            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var sw = new Stopwatch();
            sw.Restart();
            UpdateData();
            sw.Stop();
            textBoxUpdateTime.Text = $"{sw.ElapsedMilliseconds} ms";
        }

        void UpdateData()
        {
            if (DacIoSrv.Instance.IsOpen)
            {
                textBoxLrck.Text = $"{DacIoSrv.Instance.FreqLRCK() } Hz";
                textBoxBclk.Text = $"{DacIoSrv.Instance.FreqBCLK() } Hz";
                textBoxInputs.Text = $"{DacIoSrv.Instance.XmosStatus}";
                checkBoxXMOSMute.Checked = DacIoSrv.Instance.XmosMute;
                checkBoxSRCBypass.Checked = DacIoSrv.Instance.SRCBypass;
            }
        }

        private void comboBoxModes_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DacIoSrv.Instance.Mode = (DacIoSrv.Modes)comboBoxModes.SelectedItem;
        }

        private void checkBoxSRCBypass_CheckedChanged(object sender, EventArgs e)
        {
            DacIoSrv.Instance.SRCBypass = checkBoxSRCBypass.Checked;
        }

        private void checkBoxMute_CheckedChanged(object sender, EventArgs e)
        {
            DacIoSrv.Instance.SRCMute = checkBoxSRCMute.Checked;
        }

        private void checkBoxSRCPowerDown_CheckedChanged(object sender, EventArgs e)
        {
            DacIoSrv.Instance.SRCPowerDown = checkBoxSRCPowerDown.Checked;
        }

        private void trackBarVol1_Scroll(object sender, EventArgs e)
        {
            DacIoSrv.Instance.Volume1 = trackBarVol1.Value;
        }

        private void trackBarVol2_Scroll(object sender, EventArgs e)
        {
            DacIoSrv.Instance.Volume2 = trackBarVol2.Value;
        }
    }
}

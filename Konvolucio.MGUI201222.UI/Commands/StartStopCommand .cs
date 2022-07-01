
namespace Konvolucio.MGUI201222.Commands
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    using Properties;
    using Events;
    using IO;
    using DACcomm;

    class StartStopCommand : ToolStripMenuItem
    {
        public StartStopCommand()
        {
            Text = "A kapcsolódáshoz nyomd meg!";
            Image = Resources.Play_48x48;
            ShortcutKeys = Keys.F5;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            Enabled = true;
            EventAggregator.Instance.Subscribe((Action<ConnectionChangedAppEvent>)(e =>
            {
                if (e.IsOpen)
                {
                    Image = Resources.Stop_48x48;
                    Text = "Kapcsolat bontása";
                }
                else
                {
                    Image = Resources.Play_48x48;
                    Text = "Kapcsolódás";
                }
            }));
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Debug.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");

            if (Settings.Default.LastDeviceName == AppConstants.DeviceNames[AppConstants.DEV_GUI])
            {
                if (GuiIo.Instance.IsOpen)
                    GuiIo.Instance.Close();
                else
                 GuiIo.Instance.Open(Settings.Default.SeriaPortName);
            }
            else if (Settings.Default.LastDeviceName == AppConstants.DeviceNames[AppConstants.DEV_DAC])
            {
                if (DacIoSrv.Instance.IsOpen)
                    DacIoSrv.Instance.Close();
                else
                    DacIoSrv.Instance.Open(Settings.Default.SeriaPortName);
            }
        }
    }
}

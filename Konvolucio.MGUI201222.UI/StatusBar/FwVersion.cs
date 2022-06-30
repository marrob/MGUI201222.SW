namespace Konvolucio.MGUI201222.StatusBar
{
    using System;
    using System.Windows.Forms;
    using Properties;
    using Events;
    using IO;
    using DACcomm;

    class FwVersion : ToolStripStatusLabel
    { 
        public FwVersion()
        {
            BorderSides = ToolStripStatusLabelBorderSides.Left;
            BorderStyle = Border3DStyle.Etched;
            Size = new System.Drawing.Size(58, 19);
            Text = "FW: " + AppConstants.ValueNotAvailable2;
            EventAggregator.Instance.Subscribe((Action<ConnectionChangedAppEvent>)(e =>
            {
                if (e.IsOpen)
                {
                    if (Settings.Default.LastDeviceName == AppConstants.DeviceNames[AppConstants.DEV_GUI])
                        Text = "FW:" + GuiIoSrv.Instance.GetVersion();
                    else if(Settings.Default.LastDeviceName == AppConstants.DeviceNames[AppConstants.DEV_DAC])
                        Text = "FW:" + DacIoSrv.Instance.GetVersion();
                }
                else
                    Text = "FW: " + AppConstants.ValueNotAvailable2;
            }));
        }
    }
}

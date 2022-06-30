namespace Konvolucio.MGUI201222.StatusBar
{
    using System;
    using System.Windows.Forms;
    using Properties;
    using Events;
    using IO;
    using DACcomm;

    class WhoIsStatusBar : ToolStripStatusLabel
    { 
        public WhoIsStatusBar()
        {
            BorderSides = ToolStripStatusLabelBorderSides.Left;
            BorderStyle = Border3DStyle.Etched;
            Size = new System.Drawing.Size(58, 19);
            Text = "WhoIs: " + AppConstants.ValueNotAvailable2;

            EventAggregator.Instance.Subscribe((Action<ConnectionChangedAppEvent>)(e =>
            {
                if (Settings.Default.LastDeviceName == AppConstants.DeviceNames[AppConstants.DEV_GUI])
                {
                    if (GuiIoSrv.Instance.IsOpen)
                        Text = "WhoIs:" + GuiIoSrv.Instance.WhoIs();
                }
                else if (Settings.Default.LastDeviceName == AppConstants.DeviceNames[AppConstants.DEV_DAC])
                {
                    if (DacIoSrv.Instance.IsOpen)
                        Text = "WhoIs:" + DacIoSrv.Instance.WhoIs();
                }
            }));
        }
    }
}

namespace Konvolucio.MGUI201222.StatusBar
{
    using System;
    using System.Windows.Forms;
    using Properties;
    using Events;
    using IO;
    using DACcomm;

    class UniqueId: ToolStripStatusLabel
    { 
        public UniqueId()
        {
            BorderSides = ToolStripStatusLabelBorderSides.Left;
            BorderStyle = Border3DStyle.Etched;
            Size = new System.Drawing.Size(58, 19);
            Text = "UID: " + AppConstants.ValueNotAvailable2;

            EventAggregator.Instance.Subscribe((Action<ConnectionChangedAppEvent>)(e =>
            {
                if (Settings.Default.LastDeviceName == AppConstants.DeviceNames[AppConstants.DEV_GUI])
                {
                    if (GuiIo.Instance.IsOpen)
                        Text = "UID:" + GuiIo.Instance.UniqeId();
                }
                else if (Settings.Default.LastDeviceName == AppConstants.DeviceNames[AppConstants.DEV_DAC])
                {
                    if (DacIoSrv.Instance.IsOpen)
                        Text = "UID:" + DacIoSrv.Instance.UniqeId();
                }
            }));
        }
    }
}



namespace Konvolucio.MGUI201222.StatusBar
{
    using System;
    using System.Windows.Forms;
    using Properties;
    using IO;
    using DACcomm;

    class UpTimeCounterStatusBar : ToolStripStatusLabel
    { 
        public UpTimeCounterStatusBar()
        {
            BorderSides = ToolStripStatusLabelBorderSides.Left;
            BorderStyle = Border3DStyle.Etched;
            Size = new System.Drawing.Size(58, 19);
            Text = "UpTime Counter: " + AppConstants.ValueNotAvailable2;
 

            TimerService.Instance.Tick += (s, e) =>
            {
                if (Settings.Default.UpTimeCounterPeriodicUpdate)
                {
                    if (Settings.Default.LastDeviceName == AppConstants.DeviceNames[AppConstants.DEV_GUI])
                    {
                        if (GuiIo.Instance.IsOpen)
                            Text = "UpTime Counter: " + GuiIo.Instance.GetUpTime();
                    }
                    else if (Settings.Default.LastDeviceName == AppConstants.DeviceNames[AppConstants.DEV_DAC])
                    {
                        if (DacIoSrv.Instance.IsOpen)
                            Text = "UpTime Counter: " + DacIoSrv.Instance.GetUpTime();
                    }
                }
            };
        }
    }
}

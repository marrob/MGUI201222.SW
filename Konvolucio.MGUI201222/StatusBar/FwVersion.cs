namespace Konvolucio.MGUI201222.StatusBar
{
    using System;
    using System.Windows.Forms;
    using Properties;
    using Events;
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
                    Text = "FW:" + DevIoSrv.Instance.GetVersion();
                else
                    Text = "FW: " + AppConstants.ValueNotAvailable2;
            }));
        }
    }
}

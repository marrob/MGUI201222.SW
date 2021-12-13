namespace Konvolucio.MGUI201222.StatusBar
{
    using System;
    using System.Windows.Forms;
    using Properties;
    using Events;
    class CurrentRangeStatusBar: ToolStripStatusLabel
    { 
        public CurrentRangeStatusBar()
        {
            BorderSides = ToolStripStatusLabelBorderSides.Left;
            BorderStyle = Border3DStyle.Etched;
            Size = new System.Drawing.Size(58, 19);
            Text = "UID: " + AppConstants.ValueNotAvailable2;

            EventAggregator.Instance.Subscribe((Action<ConnectionChangedAppEvent>)(e =>
            {
                if (e.IsOpen)
                    Text = "UID:" + DevIoSrv.Instance.GetUniqeId();
                else
                    Text = "UID: " + AppConstants.ValueNotAvailable2;
            }));
        }
    }
}

namespace Konvolucio.MGUI201222.DFU.StatusBar
{
    using System;
    using System.Windows.Forms;
    using Properties;
    using Events;
    using IO;

    class WhoIsStatusBar : ToolStripStatusLabel
    { 
        public WhoIsStatusBar()
        {
            BorderSides = ToolStripStatusLabelBorderSides.Left;
            BorderStyle = Border3DStyle.Etched;
            Size = new System.Drawing.Size(58, 19);
            Text =  AppConstants.ValueNotAvailable2;

            EventAggregator.Instance.Subscribe((Action<ConnectionChangedAppEvent>)(e =>
            {
                if (e.IsOpen)
                    Text = MemoryInterface.Instance.WhoIs();

            }));
        }
    }
}

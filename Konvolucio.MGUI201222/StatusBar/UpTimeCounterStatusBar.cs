

namespace Konvolucio.MGUI201222.StatusBar
{
    using System;
    using System.Windows.Forms;
    using Konvolucio.MGUIcomm;
    using Properties;

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
                if(GuiIoSrv.Instance.IsOpen && Settings.Default.UpTimeCounterPeriodicUpdate)
                    Text = "UpTime Counter: " + GuiIoSrv.Instance.GetUpTime();
            };
        }
    }
}

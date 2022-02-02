namespace Konvolucio.MGUI201222.StatusBar
{
    using System;
    using System.Windows.Forms;
    using Properties;
    using Events;
    using Konvolucio.MGUIcomm;

    class VoltageStatusBar : ToolStripStatusLabel
    { 
        public VoltageStatusBar()
        {
            BorderSides = ToolStripStatusLabelBorderSides.Left;
            BorderStyle = Border3DStyle.Etched;
            Size = new System.Drawing.Size(58, 19);
            Text = "Voltages: " + AppConstants.ValueNotAvailable2;
            EventAggregator.Instance.Subscribe((Action<ConfigsChangedAppEvent>)(e =>
            {
                Text = "Voltages: " + GuiIoSrv.Instance.GetLastVoltage + "V";
            }));
        }
    }
}

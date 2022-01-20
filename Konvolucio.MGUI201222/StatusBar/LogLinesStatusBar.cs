namespace Konvolucio.MGUI201222.StatusBar
{
    using Konvolucio.MGUIComm;
    using System;
    using System.Windows.Forms;

    class LogLinesStatusBar : ToolStripStatusLabel
    { 
        public LogLinesStatusBar()
        {
            BorderSides = ToolStripStatusLabelBorderSides.Left;
            BorderStyle = Border3DStyle.Etched;
            Size = new System.Drawing.Size(58, 19);
            Text = AppConstants.ValueNotAvailable2;

            TimerService.Instance.Tick += (s, e) =>
            {
               Text = "Log Lines: " + DevIoSrv.Instance.TraceLines.ToString();
            };
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            AppLog.Instance.FileOpenProces();
        }
    }
}

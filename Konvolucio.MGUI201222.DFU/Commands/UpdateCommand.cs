
namespace Konvolucio.MGUI201222.DFU.Commands
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    using Properties;
    using Events;
    using IO;


    class UpdateCommand : ToolStripMenuItem
    {
        readonly App _app;
        bool _isRunning = false;

        public UpdateCommand(App app)
        {
            _app = app;
            Text = "Update";
           // Image = Resources.Play_48x48;
            ShortcutKeys = Keys.F5;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            Enabled = false;

            EventAggregator.Instance.Subscribe((Action<UpdatingStateChangedAppEvent>)(e =>
            {
                _isRunning = e.IsRunning;

                if (e.IsRunning)
                {
                    Text = "Abort";
                }
                else
                {
                    Text = "Update";
                }
            }));

            EventAggregator.Instance.Subscribe((Action<ConnectionChangedAppEvent>)(e =>
            {
                Enabled = e.IsOpen;
            }));

        }

        protected override void OnClick(EventArgs e)
        {
            if (_isRunning)
            {
                _app.FwAbort();
            }
            else
            {
                _app.FwUpdate(Settings.Default.IntFirmwareFilePath, Settings.Default.ExtFirmwareFilePath);
            }
            base.OnClick(e);
        }
    }
}

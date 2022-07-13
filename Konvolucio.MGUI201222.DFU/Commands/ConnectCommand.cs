
namespace Konvolucio.MGUI201222.DFU.Commands
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    using Properties;
    using Events;
    using IO;


    class ConnectCommand : ToolStripMenuItem
    {
        readonly App _app;
        public ConnectCommand(App app)
        {
            _app = app;
            Text = "Connect";
           // Image = Resources.Play_48x48;
            ShortcutKeys = Keys.F5;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            Enabled = true;
            EventAggregator.Instance.Subscribe((Action<ConnectionChangedAppEvent>)(e =>
            {
                if (e.IsOpen)
                {
                   //Image = Resources.Stop_48x48;
                    Text = "Disconnect";
                }
                else
                {
                    //Image = Resources.Play_48x48;
                    Text = "Connect";
                }
            }));
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (!MemoryInterface.Instance.IsOpen)
                _app.OpenPort();
            else
                MemoryInterface.Instance.Close();
        }
    }
}

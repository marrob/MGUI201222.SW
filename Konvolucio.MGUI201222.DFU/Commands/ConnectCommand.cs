
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
        public ConnectCommand()
        {
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
                MemoryInterface.Instance.Open(Settings.Default.SeriaPortName);
            else
                MemoryInterface.Instance.Close();
        }
    }
}

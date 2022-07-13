
namespace Konvolucio.MGUI201222.DFU.Commands
{
    using System;
    using System.Windows.Forms;
    using Properties;
    using Events;
    using IO;

    class ExitDfutCommand : ToolStripMenuItem
    {
        public ExitDfutCommand()
        {
            Text = "Exit DFU";
            //Image = Resources.Play_48x48;
            ShortcutKeys = Keys.F5;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            Enabled = false;

            EventAggregator.Instance.Subscribe((Action<ConnectionChangedAppEvent>)(e =>
            {
                Enabled = e.IsOpen;
            }));

        }

        protected override void OnClick(EventArgs e)
        {
            MemoryInterface.Instance.ExitDfuMode();
            base.OnClick(e);
        }
    }
}

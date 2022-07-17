
namespace Konvolucio.MGUI201222.DFU.Commands
{
    using System;
    using System.Windows.Forms;
    using Properties;

    class ExitDfuModeAfterUpdateCommand : ToolStripButton
    {
        public ExitDfuModeAfterUpdateCommand()
        {
            //    Image = Resources.Delete32x32;
            DisplayStyle = ToolStripItemDisplayStyle.Text;
            //    Size = new System.Drawing.Size(50, 50);
            Text = "Exit DFU mode ater update";

            Checked = Settings.Default.ExitDfuModeAfterUpdate;

            Settings.Default.PropertyChanged += (o, e) =>
            {
                Checked = Settings.Default.ExitDfuModeAfterUpdate;
            };

        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Settings.Default.ExitDfuModeAfterUpdate = !Settings.Default.ExitDfuModeAfterUpdate;
            Checked = Settings.Default.ExitDfuModeAfterUpdate;
        }
    }
}

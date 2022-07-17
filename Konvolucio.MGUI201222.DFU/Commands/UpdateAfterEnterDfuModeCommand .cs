
namespace Konvolucio.MGUI201222.DFU.Commands
{
    using System;
    using System.Windows.Forms;
    using Properties;

    class UpdateAfterEnterDfuModeCommand : ToolStripButton
    {
        public UpdateAfterEnterDfuModeCommand()
        {
            //    Image = Resources.Delete32x32;
            DisplayStyle = ToolStripItemDisplayStyle.Text;
            //    Size = new System.Drawing.Size(50, 50);
            Text = "Update After Enter DFU Mode";

            Checked = Settings.Default.UpdateAfterEnterDfuMode;

            Settings.Default.PropertyChanged += (o, e) =>
            {
                Checked = Settings.Default.UpdateAfterEnterDfuMode;
            };

        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Settings.Default.UpdateAfterEnterDfuMode = !Settings.Default.UpdateAfterEnterDfuMode;
            Checked = Settings.Default.UpdateAfterEnterDfuMode;
        }
    }
}

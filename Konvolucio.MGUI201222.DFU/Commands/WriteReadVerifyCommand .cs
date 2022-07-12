
namespace Konvolucio.MGUI201222.DFU.Commands
{
    using System;
    using System.Windows.Forms;
    using Properties;

    class WriteReadVerifyCommand : ToolStripButton
    {
        public WriteReadVerifyCommand()
        {
            //    Image = Resources.Delete32x32;
            DisplayStyle = ToolStripItemDisplayStyle.Text;
            //    Size = new System.Drawing.Size(50, 50);
            Text = "Write Read Verify";

            Checked = Settings.Default.WriteReadVerify;

            Settings.Default.PropertyChanged += (o, e) =>
            {
                Checked = Settings.Default.WriteReadVerify;
            };

        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Settings.Default.WriteReadVerify = !Settings.Default.WriteReadVerify;
            Checked = Settings.Default.WriteReadVerify;
        }
    }
}

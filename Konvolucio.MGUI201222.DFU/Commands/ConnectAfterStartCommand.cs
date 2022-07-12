
namespace Konvolucio.MGUI201222.DFU.Commands
{
    using System;
    using System.Windows.Forms;
    using Properties;

    class ConnectAfterStartCommand : ToolStripButton
    {
        public ConnectAfterStartCommand()
        {
            //    Image = Resources.Delete32x32;
            DisplayStyle = ToolStripItemDisplayStyle.Text;
            //    Size = new System.Drawing.Size(50, 50);
            Text = "Connect After Start";

            Checked = Settings.Default.OpenAfterStartUp;



            Settings.Default.PropertyChanged += (o, e) =>
            {
                Checked = Settings.Default.OpenAfterStartUp;
            };


        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);


            Settings.Default.OpenAfterStartUp = !Settings.Default.OpenAfterStartUp;
            Checked = Settings.Default.OpenAfterStartUp;

        }
    }
}

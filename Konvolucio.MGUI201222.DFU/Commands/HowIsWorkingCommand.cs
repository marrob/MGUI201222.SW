
namespace Konvolucio.MGUI201222.DFU.Commands
{
    using Properties;
    using System;
    using System.Windows.Forms;


    class HowIsWorkingCommand : ToolStripMenuItem
    {
        public HowIsWorkingCommand()
        {
            Image = Resources.dictionary48;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            Text = "How is Working?";
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            var hiw = new HowIsWorkingForm();
            hiw.ShowDialog();
        }
    }
}

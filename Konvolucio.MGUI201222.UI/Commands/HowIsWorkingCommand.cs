
namespace Konvolucio.MGUI201222.Commands
{
    using System;
    using System.Windows.Forms;
    using System.Diagnostics;
    using Properties;

    class HowIsWorkingCommand : ToolStripMenuItem
    {
        readonly IMainForm _mainForm;

        public HowIsWorkingCommand()
        {
            Image = Resources.dictionary48;
            Text = "How Is Working";
            ShortcutKeys = Keys.F1;
            Enabled = true;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            ToolTipText = @"none";
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            base.OnClick(e);
            Debug.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            // var hiw = new MJBL180509.View.HowIsWorkingForm();
            //hiw.ShowDialog();
        }
    }
}

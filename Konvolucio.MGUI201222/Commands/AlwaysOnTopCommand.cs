
namespace Konvolucio.MCEL181123.Calib.Commands
{
    using System;
    using System.Windows.Forms;

    class AlwaysOnTopCommand : ToolStripMenuItem
    {
        readonly IMainForm _mainForm;

        public AlwaysOnTopCommand(IMainForm mainForm)
        {
            //    Image = Resources.Delete32x32;
            DisplayStyle = ToolStripItemDisplayStyle.Text;
            //    Size = new System.Drawing.Size(50, 50);
            Text = "Always On Top";

            _mainForm = mainForm;
            //   _diagnosticsView = diagnosticsView;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _mainForm.AlwaysOnTop = !_mainForm.AlwaysOnTop;
            Checked = _mainForm.AlwaysOnTop;
        }
    }
}

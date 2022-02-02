
namespace Konvolucio.MGUI201222.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms; /*ToolStripMenuItem*/
    using Controls;
    using Konvolucio.MGUIcomm;
    using Properties;


    class ClearReachTextBoxCommand : ToolStripMenuItem
    {
        KnvRichTextBox _KnvRtb;

        public ClearReachTextBoxCommand(KnvRichTextBox knvRtb)
        {
            _KnvRtb = knvRtb;
            ShortcutKeys = Keys.None;
            Image = Resources.Delete48x48 ;
            Text = "Törlés";

        }

        protected override void OnClick(EventArgs e)
        {
            _KnvRtb.Clear();
            GuiIoSrv.Instance.TraceClear();
        }
    }
}

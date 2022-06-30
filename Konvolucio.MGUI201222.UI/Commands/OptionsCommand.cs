
namespace Konvolucio.MGUI201222.Commands
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    using Properties;
    using Events;

    class OptionsCommand : ToolStripMenuItem
    {
        readonly IApp _app;

        public OptionsCommand(IApp obj)
        {
            _app = obj;
            Image = Resources.Settings_48x48;
            Text = "Options...";
            ShortcutKeys = Keys.None;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            Enabled = true; 

        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            //_app.CanConfig();
            Debug.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            //_mainForm.AlwaysOnTop = !_mainForm.AlwaysOnTop;
            //Checked = _mainForm.AlwaysOnTop;
        }
    }
}

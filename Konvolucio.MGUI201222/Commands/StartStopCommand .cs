
namespace Konvolucio.MGUI201222.Commands
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    using Properties;
    using Events;
    using Konvolucio.MGUIComm;

    class StartStopCommand : ToolStripMenuItem
    {
        readonly IApp _app;

        public StartStopCommand(IApp obj)
        {
            _app = obj;
            Text = "A kapcsolódáshoz nyomd meg!";
            Image = Resources.Play_48x48;
            ShortcutKeys = Keys.F5;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            Enabled = true;

            EventAggregator.Instance.Subscribe((Action<ConnectionChangedAppEvent>)(e =>
            {
                if (e.IsOpen)
                {
                    Image = Resources.Stop_48x48;
                    Text = "Kapcsolat bontása";
                }
                else
                {
                    Image = Resources.Play_48x48;
                    Text = "Kapcsolódás";
                }
            }));

        }




        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            //_app.CanConfig();
            Debug.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");

            if (DevIoSrv.Instance.IsOpen)
                DevIoSrv.Instance.Close();
            else
                DevIoSrv.Instance.Open(Settings.Default.SeriaPortName);

        }
    }
}

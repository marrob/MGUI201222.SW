
namespace Konvolucio.MCEL181123.Calib.Commands
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    using Properties;
    using Events;

    class ComPortSelectCommand : ToolStripComboBox
    {
        readonly IApp _app;

        public ComPortSelectCommand(IApp obj)
        {
            _app = obj;
           // Image = Resources.Settings_48x48;
            Text = "Válaszd ki a megfelelő COM portot a listából...";
           // ShortcutKeys = Keys.None;
            //DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            Enabled = true;

            DropDownStyle = ComboBoxStyle.DropDownList;


            DropDown += (o, e) =>
            {
                Items.Clear();
                Items.AddRange(DevIoSrv.GetPortNames());
            };

            DropDownClosed += (o, e) =>{
                Control p;
                p = ((ToolStripComboBox)o).Control;
                p.Parent.Focus();

                Settings.Default.SeriaPortName = Text;

            };

            EventAggregator.Instance.Subscribe((Action<ShowAppEvent>)(e =>
            {               
                Items.Clear();
                Items.AddRange(DevIoSrv.GetPortNames());

                if (!string.IsNullOrWhiteSpace(Settings.Default.SeriaPortName))
                {
                    Text = Settings.Default.SeriaPortName;
                }
            }));


            EventAggregator.Instance.Subscribe((Action<ConnectionChangedAppEvent>)(e =>
            {
                Enabled = !e.IsOpen;       
            }));
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

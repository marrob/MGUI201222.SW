
namespace Konvolucio.MGUI201222.DFU.Commands
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    using Properties;
    using Events;
    using IO;

    class ComPortSelectCommand : ToolStripComboBox
    {
        readonly App _app;

        public ComPortSelectCommand(App obj)
        {
            _app = obj;
            Text = "Válaszd ki a megfelelő COM portot a listából...";
            Enabled = true;
            BackColor = System.Drawing.SystemColors.Control;
            DropDownStyle = ComboBoxStyle.DropDownList;


            DropDown += (o, e) =>
            {
                Items.Clear();
                Items.AddRange(GuiIo.GetPortNames());
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
                Items.AddRange(GuiIo.GetPortNames());

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

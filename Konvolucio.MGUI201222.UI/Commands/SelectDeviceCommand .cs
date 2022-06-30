
namespace Konvolucio.MGUI201222.Commands
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    using Properties;
    using Events;

    class SelectDeviceCommand : ToolStripComboBox
    {
        public SelectDeviceCommand()
        {
            Text = "Válaszd ki a megfelelő eszköz típust a listából...";
            Enabled = true;
            DropDownStyle = ComboBoxStyle.DropDownList;

            DropDownClosed += (o, e) =>
            {
                Control p;
                p = ((ToolStripComboBox)o).Control;
                p.Parent.Focus();
                if (Settings.Default.LastDeviceName != Text)
                {
                    Settings.Default.LastDeviceName = Text; 
                    EventAggregator.Instance.Publish<DeviceNameChanged>(new DeviceNameChanged(Text));
                }
            };

            EventAggregator.Instance.Subscribe((Action<ShowAppEvent>)(e =>
            {
                Items.Clear();
                Items.AddRange(AppConstants.DeviceNames);
                if (!string.IsNullOrWhiteSpace(Settings.Default.LastDeviceName))
                {
                    Text = Settings.Default.LastDeviceName;
                }
            }));

            EventAggregator.Instance.Subscribe((Action<ConnectionChangedAppEvent>)(e =>
            {
                Enabled = !e.IsOpen;       
            }));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konvolucio.MGUI201222.View
{
    public partial class MainView : UserControl
    {
        private readonly HelpNode _helpNode;
        private readonly SettingsNode _settingsPanel;
        private readonly PcReferenceNode _functionNode;

        private readonly UserControl[] _ctrlPanels;

        public MainView()
        {
            InitializeComponent();

            _ctrlPanels = new UserControl[]
            {
                _helpNode = new HelpNode() { Dock = DockStyle.Fill },
                _settingsPanel = new SettingsNode() { Dock = DockStyle.Fill },
             //   _configsNode = new ConfigsNode() { Dock = DockStyle.Fill },
                _functionNode = new PcReferenceNode() { Dock = DockStyle.Fill },
               // _currentMeasNode = new CurrentMeasNode() { Dock = DockStyle.Fill },
               // _voltageMeasNode = new VoltageMeasNode() { Dock = DockStyle.Fill },
            };
        }

        private void TreeViewNavigator_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selectedPanel = _ctrlPanels.FirstOrDefault(n => n.Name == e.Node.Name);
            if (selectedPanel != null)
            {
                var ctrl = splitContainer1.Panel2.Controls;

                if (ctrl.Count != 0 && ctrl[0] is IUIPanelProperties)
                    (ctrl[0] as IUIPanelProperties).UserLeave();

                ctrl.Clear();
                ctrl.Add(selectedPanel);

                if (selectedPanel is IUIPanelProperties)       
                    (selectedPanel as IUIPanelProperties).UserEnter();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Konvolucio.MCEL181123.Controls;

namespace Konvolucio.MCEL181123.View
{
    public partial class CurrentMeasNode : UserControl
    {
        public CurrentMeasNode()
        {
            InitializeComponent();
            this.Name = "CalibrationNode";
        }

        private void configItemControl_Send(object sender, EventArgs e)
        {
            var config = (sender as ConfigItemControl);

            try
            {
                DevIoSrv.Instance.SetVolt(0, double.Parse(config.Volts));
                DevIoSrv.Instance.SetAmpers(0, double.Parse(config.Ampers));
                if (config.Remote)
                    DevIoSrv.Instance.SetSenseRemote(0);
                if (config.Local)
                    DevIoSrv.Instance.SetSenseLocal(0);

                DevIoSrv.Instance.OutputOn(0);
            }
            catch (Exception ex)
            {
                DevIoSrv.Instance.Trace(ex.Message);
            }
        }
    }
}

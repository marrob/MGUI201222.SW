namespace Konvolucio.MGUI201222.View
{
    using System;
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Controls;
    using Konvolucio.MGUIcomm;
    using Properties;

    public partial class VoltageMeasNode : UserControl, IUIPanelProperties
    {

        bool UserInFocus = false;

        public VoltageMeasNode()
        {
            InitializeComponent();
            this.Name = "VoltageMeasNode";

            TimerService.Instance.Tick += (s, e) =>
            {
                if (UserInFocus && GuiIoSrv.Instance.IsOpen)
                {

                }
            };

        }

        private void configItemControl_Send(object sender, EventArgs e)
        {
            
        }



         private void buttonStartStop_Click(object sender, EventArgs e)
        {

        }

        public void UserLeave()
        {
            UserInFocus = false;
        }

        public void UserEnter()
        {
            UserInFocus = true;
        }
    }
}

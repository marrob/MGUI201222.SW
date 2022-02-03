namespace Konvolucio.MGUI201222.View
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Controls;
    using Events;
    using System.Diagnostics;
    using Konvolucio.DACcomm;

    public partial class DacNode : UserControl, IUIPanelProperties
    {
        public DacNode()
        {
            InitializeComponent();
            this.Name = "DacNode";
        }

        public void UserEnter()
        { 
            timer1.Start();

        }

        public void UserLeave()
        {

            timer1.Stop();
        }

    }
}

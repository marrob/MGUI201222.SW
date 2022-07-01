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
    using IO;

    public partial class KarunaNode : UserControl, IUIPanelProperties
    {
        public KarunaNode()
        {
            InitializeComponent();
            this.Name = "KarunaNode";
        }

        public void UserEnter()
        { 
            
            UpdateData();

            timer1.Start();

        }

        public void UserLeave()
        {
            timer1.Stop();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            var sw = new Stopwatch();
            sw.Restart();

            UpdateData();

            sw.Stop();
        }



        void UpdateData()
        {

        }

        private void buttonStartShutdownCmd_Click(object sender, EventArgs e)
        {
            if(GuiIo.Instance.IsOpen)
                GuiIo.Instance.StartShutdownSequence();
        }

        private void buttonPwrLedFlash_Click(object sender, EventArgs e)
        {

        }

        private void buttonPwrLedFlashOff_Click(object sender, EventArgs e)
        {
            GuiIo.Instance.LedCustomFlashStop();
        }
    }
}

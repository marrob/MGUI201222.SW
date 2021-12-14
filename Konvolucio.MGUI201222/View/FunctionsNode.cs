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

    public partial class FunctionsNode : UserControl, IUIPanelProperties
    {
        public FunctionsNode()
        {
            InitializeComponent();
            this.Name = "FunctionsNode";
        }

        public void UserEnter()
        {
           numericUpDownDisplay.Value = DevIoSrv.Instance.GetDisplayLight();
           numericUpDownButton.Value = DevIoSrv.Instance.GetPowerButtonLight();

            knvIoOutputs.ChangedValue += KnvIo8Control1_ChangedValue;
            if (!DevIoSrv.Instance.IsOpen)
                knvIoOutputs.NotAvaliable = true;
            else
            {
                knvIoOutputs.NotAvaliable = false;

                for (int i = 1; i <= 8; i++)
                    knvIoOutputs.SetContent(i, DevIoSrv.Instance.GetOutput(i));

                for (int i = 1; i <= 16; i++)
                    knvIoInputs.SetContent(i, DevIoSrv.Instance.GetInput(i));
            }
        }   

        public void UserLeave()
        {
            knvIoOutputs.ChangedValue -= KnvIo8Control1_ChangedValue;
        }

        private void KnvIo8Control1_ChangedValue(object sender, KnvIoEventArg e)
        {
            if (e.state == false)
                DevIoSrv.Instance.SetOnOutput(e.Index);
            else
                DevIoSrv.Instance.SetOffOutput(e.Index);

            knvIoOutputs.SetContent(e.Index, DevIoSrv.Instance.GetOutput(e.Index));
        }


       


        private void numericUpDownButton_ValueChanged(object sender, EventArgs e)
        {
            DevIoSrv.Instance.SetPowerButtonLight((int)numericUpDownButton.Value);
        }

        private void numericUpDownDisplay_ValueChanged(object sender, EventArgs e)
        {
            DevIoSrv.Instance.SetDisplayLight((int)numericUpDownDisplay.Value);
        }
    }
}

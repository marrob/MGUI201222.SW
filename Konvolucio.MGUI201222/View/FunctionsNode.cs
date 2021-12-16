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
            this.SuspendLayout();
           
            numericUpDownDisplay.Value = DevIoSrv.Instance.GetDisplayLight();
            numericUpDownButton.Value = DevIoSrv.Instance.GetPowerButtonLight();
            checkBoxPSP.Checked = DevIoSrv.Instance.GetDisplay();
            checkBoxPSP.Checked = DevIoSrv.Instance.GetPowerSupply();

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

            textBoxTemp1.Text = DevIoSrv.Instance.GetTemperature(1).ToString();
            textBoxTemp2.Text = DevIoSrv.Instance.GetTemperature(2).ToString();
            textBoxTemp3.Text = DevIoSrv.Instance.GetTemperature(3).ToString();
            textBoxTemp4.Text = DevIoSrv.Instance.GetTemperature(4).ToString();
           
            this.ResumeLayout();

            timer1.Start();

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

        private void checkBoxPSP_CheckedChanged(object sender, EventArgs e)
        {
            DevIoSrv.Instance.SetPowerSupply((sender as CheckBox).Checked);
        }

        private void checkBoxDisplay_CheckedChanged(object sender, EventArgs e)
        {
            DevIoSrv.Instance.SetDisplay((sender as CheckBox).Checked);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DevIoSrv.Instance.IsOpen)
            {
                
/*
                textBoxTemp1.Text = DevIoSrv.Instance.GetTemperature(1).ToString();
                textBoxTemp2.Text = DevIoSrv.Instance.GetTemperature(2).ToString();
                textBoxTemp3.Text = DevIoSrv.Instance.GetTemperature(3).ToString();
                textBoxTemp4.Text = DevIoSrv.Instance.GetTemperature(4).ToString();
 *//*
                for (int i = 1; i <= 16; i++)
                    knvIoInputs.SetContent(i, DevIoSrv.Instance.GetInput(i));
              */ 

            }
            else 
            {
            
            }


        }
    }
}

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
    using Konvolucio.MGUIcomm;

    public partial class PcReferenceNode : UserControl, IUIPanelProperties
    {
        public PcReferenceNode()
        {
            InitializeComponent();
            this.Name = "PcReference";
        }

        public void UserEnter()
        { 
            knvIoOutputs.ChangedValue += KnvIo8Control1_ChangedValue;
            numericUpDownDisplay.ValueChanged += new EventHandler(this.numericUpDownDisplay_ValueChanged);
            numericUpDownButton.ValueChanged += new EventHandler(this.numericUpDownButton_ValueChanged);


            numericUpDownDisplay.Value = GuiIoSrv.Instance.DisplayLight();
            numericUpDownButton.Value = GuiIoSrv.Instance.LedLight();
            
            UpdateData();

            timer1.Start();

        }

        public void UserLeave()
        {
            knvIoOutputs.ChangedValue -= KnvIo8Control1_ChangedValue;
            numericUpDownDisplay.ValueChanged -= new EventHandler(this.numericUpDownDisplay_ValueChanged);
            numericUpDownButton.ValueChanged -= new EventHandler(this.numericUpDownButton_ValueChanged);
            timer1.Stop();
        }

        private void KnvIo8Control1_ChangedValue(object sender, KnvIoEventArg e)
        {
            byte currentState = knvIoOutputs.GetValue();
            byte newState;
            if (e.state == false)
           
                newState = (byte)(1 << e.Index - 1 | currentState);
            
            else
           
                newState = (byte)( ~(1 << e.Index - 1) & currentState);
            
            GuiIoSrv.Instance.SetOutputs(newState);
            

        }

        private void numericUpDownButton_ValueChanged(object sender, EventArgs e)
        {
            GuiIoSrv.Instance.LedLight((int)numericUpDownButton.Value);
        }

        private void numericUpDownDisplay_ValueChanged(object sender, EventArgs e)
        {
            GuiIoSrv.Instance.DisplayLight((int)numericUpDownDisplay.Value);
        }

        private void checkBoxPSP_CheckedChanged(object sender, EventArgs e)
        {
            GuiIoSrv.Instance.SetPowerSupply((sender as CheckBox).Checked);
        }

        private void checkBoxDisplay_CheckedChanged(object sender, EventArgs e)
        {
            GuiIoSrv.Instance.SetDisplay((sender as CheckBox).Checked);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            var sw = new Stopwatch();
            sw.Restart();

            UpdateData();

            sw.Stop();
            textBoxUpdateTime.Text = $"{sw.ElapsedMilliseconds} ms";
        }



        void UpdateData()
        {
            if (GuiIoSrv.Instance.IsOpen)
            { 
                checkBoxDisplay.Checked = GuiIoSrv.Instance.GetDisplay();


                if (!GuiIoSrv.Instance.IsOpen)
                    knvIoOutputs.NotAvaliable = true;
                else
                {
                    knvIoOutputs.NotAvaliable = false;
                    knvIoOutputs.SetContent(GuiIoSrv.Instance.GetOutputs());
                    knvIoInputs.SetContent(GuiIoSrv.Instance.GetInputs());
                }

                double[] temps = GuiIoSrv.Instance.GetTemperatures();
                if (temps.Length > 3)
                {
                    textBoxTemp1.Text = temps[0].ToString();
                }
            }
        }

        private void buttonStartShutdownCmd_Click(object sender, EventArgs e)
        {
            if(GuiIoSrv.Instance.IsOpen)
                GuiIoSrv.Instance.StartShutdownSequence();
        }

        private void buttonPwrLedFlash_Click(object sender, EventArgs e)
        {
            if (!GuiIoSrv.Instance.IsOpen)
                return;

            GuiIoSrv.Instance.LedDimming(checkBoxPowerLedDimming.Checked);
            GuiIoSrv.Instance.LedPeriodTime((int)numericUpDownPwrLedFlashPeriod.Value);
            GuiIoSrv.Instance.LedCustomFlashStart();

        }

        private void buttonPwrLedFlashOff_Click(object sender, EventArgs e)
        {
            GuiIoSrv.Instance.LedCustomFlashStop();
        }
    }
}

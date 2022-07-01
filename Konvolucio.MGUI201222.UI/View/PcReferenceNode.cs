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


            numericUpDownDisplay.Value = GuiIo.Instance.DisplayLight();
            numericUpDownButton.Value = GuiIo.Instance.LedLight();
            
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
            
            GuiIo.Instance.SetOutputs(newState);
            

        }

        private void numericUpDownButton_ValueChanged(object sender, EventArgs e)
        {
            GuiIo.Instance.LedLight((int)numericUpDownButton.Value);
        }

        private void numericUpDownDisplay_ValueChanged(object sender, EventArgs e)
        {
            GuiIo.Instance.DisplayLight((int)numericUpDownDisplay.Value);
        }

        private void checkBoxPSP_CheckedChanged(object sender, EventArgs e)
        {
            GuiIo.Instance.SetPowerSupply((sender as CheckBox).Checked);
        }

        private void checkBoxDisplay_CheckedChanged(object sender, EventArgs e)
        {
            GuiIo.Instance.SetDisplay((sender as CheckBox).Checked);
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
            if (GuiIo.Instance.IsOpen)
            { 
                checkBoxDisplay.Checked = GuiIo.Instance.GetDisplay();


                if (!GuiIo.Instance.IsOpen)
                    knvIoOutputs.NotAvaliable = true;
                else
                {
                    knvIoOutputs.NotAvaliable = false;
                    knvIoOutputs.SetContent(GuiIo.Instance.GetOutputs());
                    knvIoInputs.SetContent(GuiIo.Instance.GetInputs());
                }

                double[] temps = GuiIo.Instance.GetTemperatures();
                if (temps.Length > 3)
                {
                    textBoxTemp1.Text = temps[0].ToString();
                }
            }
        }

        private void buttonStartShutdownCmd_Click(object sender, EventArgs e)
        {
            if(GuiIo.Instance.IsOpen)
                GuiIo.Instance.StartShutdownSequence();
        }

        private void buttonPwrLedFlash_Click(object sender, EventArgs e)
        {
            if (!GuiIo.Instance.IsOpen)
                return;

            GuiIo.Instance.LedDimming(checkBoxPowerLedDimming.Checked);
            GuiIo.Instance.LedPeriodTime((int)numericUpDownPwrLedFlashPeriod.Value);
            GuiIo.Instance.LedCustomFlashStart();

        }

        private void buttonPwrLedFlashOff_Click(object sender, EventArgs e)
        {
            GuiIo.Instance.LedCustomFlashStop();
        }
    }
}

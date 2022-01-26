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
    using Konvolucio.MGUIComm;

    public partial class FunctionsNode : UserControl, IUIPanelProperties
    {
        public FunctionsNode()
        {
            InitializeComponent();
            this.Name = "FunctionsNode";
        }

        public void UserEnter()
        { 
            knvIoOutputs.ChangedValue += KnvIo8Control1_ChangedValue;
            numericUpDownDisplay.ValueChanged += new EventHandler(this.numericUpDownDisplay_ValueChanged);
            numericUpDownButton.ValueChanged += new EventHandler(this.numericUpDownButton_ValueChanged);


            numericUpDownDisplay.Value = DevIoSrv.Instance.DisplayLight();
            numericUpDownButton.Value = DevIoSrv.Instance.LedLight();
            
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
            
            DevIoSrv.Instance.SetOutputs(newState);
            

        }

        private void numericUpDownButton_ValueChanged(object sender, EventArgs e)
        {
            DevIoSrv.Instance.LedLight((int)numericUpDownButton.Value);
        }

        private void numericUpDownDisplay_ValueChanged(object sender, EventArgs e)
        {
            DevIoSrv.Instance.DisplayLight((int)numericUpDownDisplay.Value);
        }

        private void checkBoxPSP_CheckedChanged(object sender, EventArgs e)
        {
            DevIoSrv.Instance.SetPowerSupply((sender as CheckBox).Checked);
        }

        private void checkBoxDisplay_CheckedChanged(object sender, EventArgs e)
        {
            DevIoSrv.Instance.SetDisplay((sender as CheckBox).Checked);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateData();
        }



        void UpdateData()
        {
            if (DevIoSrv.Instance.IsOpen)
            {
                var sw = new Stopwatch();
                sw.Restart();

                checkBoxDisplay.Checked = DevIoSrv.Instance.GetDisplay();
                checkBoxPSP.Checked = DevIoSrv.Instance.GetPowerSupply();
               

                if (!DevIoSrv.Instance.IsOpen)
                    knvIoOutputs.NotAvaliable = true;
                else
                {
                    knvIoOutputs.NotAvaliable = false;
                    knvIoOutputs.SetContent(DevIoSrv.Instance.GetOutputs());
                    knvIoInputs.SetContent(DevIoSrv.Instance.GetInputs());
                }

                double[] temps = DevIoSrv.Instance.GetTemperatures();
                if (temps.Length > 3)
                {
                    textBoxTemp1.Text = temps[0].ToString();
                    textBoxTemp2.Text = temps[1].ToString();
                    textBoxTemp3.Text = temps[2].ToString();
                    textBoxTemp4.Text = temps[3].ToString();
                }
                sw.Stop();
                Debug.WriteLine($"Update Data: { sw.ElapsedMilliseconds } ms ");
            }
        }

        private void buttonStartShutdownCmd_Click(object sender, EventArgs e)
        {
            if(DevIoSrv.Instance.IsOpen)
                DevIoSrv.Instance.StartShutdownSequence();
        }

        private void buttonPwrLedFlash_Click(object sender, EventArgs e)
        {
            if (!DevIoSrv.Instance.IsOpen)
                return;

            DevIoSrv.Instance.LedDimming(checkBoxPowerLedDimming.Checked);
            DevIoSrv.Instance.LedPeriodTime((int)numericUpDownPwrLedFlashPeriod.Value);
            DevIoSrv.Instance.LedCustomFlashStart();

        }

        private void buttonPwrLedFlashOff_Click(object sender, EventArgs e)
        {
            DevIoSrv.Instance.LedCustomFlashStop();
        }
    }
}

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

            UpdateData();

            timer1.Start();

        }

        public void UserLeave()
        {
            knvIoOutputs.ChangedValue -= KnvIo8Control1_ChangedValue;
            timer1.Stop();
        }

        private void KnvIo8Control1_ChangedValue(object sender, KnvIoEventArg e)
        {
            if (e.state == false)
                DevIoSrv.Instance.SetOnOutput(e.Index);
            else
                DevIoSrv.Instance.SetOffOutput(e.Index);

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


        private void timer1_Tick(object sender, EventArgs e)
        {

            UpdateData();
 /*       
            string in1 = "";
            string in2 = "";
            string in3 = "";
            string in4 = "";
            UInt16 inputs = 0;
            byte outputs = 0;

            new Action(() =>
            {
                if (!DevIoSrv.Instance.IsOpen)
                { 
                }
                //knvIoOutputs.NotAvaliable = true;
                else
                {
                    // knvIoOutputs.NotAvaliable = false;

                    outputs = DevIoSrv.Instance.GetOutputs();
                    inputs = DevIoSrv.Instance.GetInputs();
                }
                in1 = DevIoSrv.Instance.GetTemperature(1).ToString();
                in2 = DevIoSrv.Instance.GetTemperature(2).ToString();
                in3 = DevIoSrv.Instance.GetTemperature(3).ToString();
                in4 = DevIoSrv.Instance.GetTemperature(4).ToString();
            }).BeginInvoke((iftAR) => 
            {
               
                knvIoOutputs.SetContent(outputs);
                knvIoInputs.SetContent(inputs);
            
                textBoxTemp1.Text = in1;
                textBoxTemp2.Text = in2;
                textBoxTemp3.Text = in3;
                textBoxTemp4.Text = in4;
            
            }, null);
*/

        }



        void UpdateData()
        {
            if (DevIoSrv.Instance.IsOpen)
            {
                var sw = new Stopwatch();
                sw.Restart();

                checkBoxPSP.Checked = DevIoSrv.Instance.GetDisplay();
                checkBoxPSP.Checked = DevIoSrv.Instance.GetPowerSupply();

                if (!DevIoSrv.Instance.IsOpen)
                    knvIoOutputs.NotAvaliable = true;
                else
                {
                    knvIoOutputs.NotAvaliable = false;
                    knvIoOutputs.SetContent(DevIoSrv.Instance.GetOutputs());
                    knvIoInputs.SetContent(DevIoSrv.Instance.GetInputs());
                }

                textBoxTemp1.Text = DevIoSrv.Instance.GetTemperature(1).ToString();
                textBoxTemp2.Text = DevIoSrv.Instance.GetTemperature(2).ToString();
                textBoxTemp3.Text = DevIoSrv.Instance.GetTemperature(3).ToString();
                textBoxTemp4.Text = DevIoSrv.Instance.GetTemperature(4).ToString();

                sw.Stop();
                Debug.WriteLine($"Update Data: { sw.ElapsedMilliseconds } ms ");
            }
        }

    }
}

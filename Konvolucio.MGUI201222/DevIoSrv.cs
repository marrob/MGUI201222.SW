

namespace Konvolucio.MCEL181123.Calib
{ 
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO.Ports;
    using System.Globalization;

    public class DevIoSrv
    {


        public event EventHandler ConnectionChanged;

        public static DevIoSrv Instance { get { return _instance; } }

        private static readonly DevIoSrv _instance = new DevIoSrv();

        public Queue<string> TraceQueue = new Queue<string>();
        public int TraceLines { get; private set; }

        public SerialPort _sp;
        public bool IsOpen
        {
            get
            {
                if (_sp == null)
                    return false;
                else
                    return _sp.IsOpen;
            }
        }


        double _lastCurrent;
        public double GetLastCurrentLimit
        {
            get { return _lastCurrent; }
            set { _lastCurrent = value; }
        }

        double _lastVoltage;
        public double GetLastVoltage
        {
            get { return _lastVoltage; }
            set { _lastVoltage = value; }
        }


        public static string[] GetPortNames()
        {
            return (SerialPort.GetPortNames());
        }

        public DevIoSrv()
        {

        }

        public void Open(string port)
        {
            try
            {
                _sp = new SerialPort(port);
                _sp.ReadTimeout = 1000;
                _sp.BaudRate = 460800;
                _sp.NewLine = "\n";
                _sp.Open();
                Trace("Serial Port: " + port + " is Open.");
                Test();
                OnConnectionChanged();
            }
            catch (Exception ex)
            {
                Trace("IO ERROR Serial Port is: " + port + " Open fail... beacuse:" + ex.Message);
                OnConnectionChanged();
            }
        }

        public void Test()
        {
            if (_sp == null || !_sp.IsOpen)
            {
                Trace("IO ERROR: port is closed.");
            }

            try
            {
                var resp = WriteRead("*OPC?");
                if (resp == null || resp != "*OPC")
                    Trace("Test Failed");
            }
            catch (Exception ex)
            {
                Trace("IO-ERROR:" + ex.Message);
            }
        }

        string WriteRead(string str)
        {
            if (_sp == null || !_sp.IsOpen)
            {
                Trace("IO ERROR Serial Port is closed. " + str);
                OnConnectionChanged();
                return null;
            }
            try
            {
                Trace("Tx: " + str);
                _sp.WriteLine(str);
                // IoLog.Instance.WirteLine(str);
            }
            catch (Exception ex)
            {
                Trace("Tx ERROR Serial Port is:" + ex.Message);
                OnConnectionChanged();
            }

            try
            {

                str = _sp.ReadLine();
                // IoLog.Instance.WirteLine(str);
                Trace("Rx: " + str);
            }
            catch (Exception ex)
            {
                Trace("Rx ERROR Serial Port is:" + ex.Message);
                OnConnectionChanged();
            }
            return str;
        }

        public double MeasVolt(byte node)
        {
            double retval = double.NaN;
            var resp = WriteRead("#" + node.ToString("X2") + " " + "MEAS:VOLT?");
            if (resp == null)
                return double.NaN;
            else if (double.TryParse(resp, NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out retval))
                return retval;
            else
                return double.NaN;
        }

        public double MeasCurr(byte node)
        {
            double retval = double.NaN;
            var resp = WriteRead("#" + node.ToString("X2") + " " + "MEAS:CURR?");
            if (resp == null)
                return double.NaN;
            else if (double.TryParse(resp, NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out retval))
                return retval;
            else
                return double.NaN;
        }

        public string GetCurrRange(byte node)
        {
            if (_sp == null || !_sp.IsOpen)
                return "ERROR port is Closed";

            try
            {
                var resp = int.Parse(WriteRead("#" + node.ToString("X2") + " " + "CURR:RNG?"));
                if (resp == 0)
                {
                    return "100mA";
                }
                if (resp == 1)
                {
                    return "50uA";
                }
                else
                {
                    return "Unknown";
                }
            }
            catch (Exception ex)
            {
                Trace("IO-ERROR:" + ex.Message);
            }
            return "na";
        }


        public void SetVolt(byte node, double volt)
        {
            _lastVoltage = volt;
            WriteRead("#" + node.ToString("X2") + " " + "SET:VOLT" + "  " + volt.ToString());
        }

        public void OutputOn(byte node)
        {
            WriteRead("#" + node.ToString("X2") + " " + "SET:OE 1");
        }

        public void OutputOff(byte node)
        {
            WriteRead("#" + node.ToString("X2") + " " + "SET:OE 0");
        }

        public void SetCurrentLimit(byte node, double current)
        {
            _lastCurrent = current;
            WriteRead("#" + node.ToString("X2") + " " + "SET:CURR" + "  " + current.ToString());
        }


        public void SetTriggerVolt(byte node)
        {
            WriteRead("#" + node.ToString("X2") + " " + "TRIG:VOLT");
        }

        public void SetTriggerCurrent(byte node)
        {
            WriteRead("#" + node.ToString("X2") + " " + "TRIG:CURR");
        }

        public int ReadUpTime(byte node)
        {
            int retval = 0;
            var resp = WriteRead("READ:UPTIME?");
            if (resp == null)
                return 0;
            else if (int.TryParse(resp, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out retval))
                return retval;
            else
                return 0;
        }

        public void Close()
        {
            TraceQueue.Enqueue(DateTime.Now.ToString(AppConstants.GenericTimestampFormat) + " " + "Serial Port is: " + "Close");
            _sp.Close();
            OnConnectionChanged();
        }

        public void Trace(string msg)
        {
            TraceLines++;
            TraceQueue.Enqueue(DateTime.Now.ToString(AppConstants.GenericTimestampFormat) + " " + msg);
        }

        public void TraceClear()
        {
            TraceQueue.Clear();
            TraceLines = 0;
        }
        protected virtual void OnConnectionChanged()
        {
            EventHandler handler = ConnectionChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}

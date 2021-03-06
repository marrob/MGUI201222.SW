namespace Konvolucio.DACcomm
{ 
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO.Ports;
    using System.Globalization;

    public class DacIoSrv
    {

       public enum Modes
        {
            MODE_NONE = 0,
            MODE_USB_PCM_WITH_SRC,
            MODE_USB_PCM_WITHOUT_SRC,

            MODE_USB_DSD,

            MODE_HDMI_PCM_WITH_SRC,
            MODE_HDMI_PCM_WITHOUT_SRC,
            MODE_HDMI_DSD,

            MODE_BNC_SPDIF_WITH_SRC,
            MODE_BNC_SPDIF_WITHOUT_SRC,

            MODE_RCA_SPDIF_WITH_SRC,
            MODE_RCA_SPDIF_WITHOUT_SRC,

            MODE_XLR_SPDIF_WITH_SRC,
            MODE_XLR_SPDIF_WITHOUT_SRC

        }

        public enum Xtatus
        {
            UNKNOWN = 0xFF,
            PCM44_1KHZ = 0x1F,
            PCM48_0KHZ = 0x1E,
            PCM88_2KHZ = 0x1D,
            PCM96_0KHZ = 0x1C,
            PCM176_4KHZ = 0x1B,
            PCM192_KHZ	= 0x1A,
            PCM352_8KHZ	= 0x19,
            PCM384_KHZ	= 0x18,
            DSD64_DOP = 0x0A,
            DSD128_DOP = 0x08,
            DSD64_NATVIE = 0x02,
            DSD128_NATIVE = 0x00,
            DSD256_NATIVE = 0x04
        }


        public event EventHandler ConnectionChanged;
        public event EventHandler ErrorHappened;

        const string GenericTimestampFormat = "yyyy.MM.dd HH:mm:ss";

        public bool TracingEnable { get; set; } = false;

        public static DacIoSrv Instance { get { return _instance; } }

        private static readonly DacIoSrv _instance = new DacIoSrv();

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

        int _consecutiveRxErrorCounter;

        public static string[] GetPortNames()
        {
            return (SerialPort.GetPortNames());
        }

        public DacIoSrv()
        {

        }

       static readonly object _syncObject = new object();

        /// <summary>
        /// Srosport Nyitása
        /// </summary>
        /// <param name="port">Port:COMx</param>
        public void Open(string port)
        {
            try
            {
                _sp = new SerialPort(port)
                {
                    ReadTimeout = 1000,
                    BaudRate = 115200,
                    NewLine = "\n"
                };
                _consecutiveRxErrorCounter = 0;
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
            lock (_syncObject)
                
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
                }
                catch (Exception ex)
                {
                    Trace("Tx ERROR Serial Port is:" + ex.Message);
                    OnErrorHappened();
                }

                try
                {

                    str = _sp.ReadLine().Trim(new char[]{'\0', '\r', '\n' });
                    Trace("Rx: " + str);
                    _consecutiveRxErrorCounter = 0;
                }
                catch (Exception ex)
                {
                    Trace("Rx ERROR Serial Port is:" + ex.Message);
                    _consecutiveRxErrorCounter++;
                    OnErrorHappened();
                }
                if (_consecutiveRxErrorCounter >= 3)
                {
                    Trace("Három hibás egymást követő válasz! megszakítom a kapcsolatot...");
                    Close();
                }
            }
            return str;
        }
        /// <summary>
        /// Firmware Verziószáma
        /// </summary>
        /// <returns>pl. 1.0.0.0</returns>
        public string GetVersion()
        {
            var resp = WriteRead("*VER?");
            if (resp == null)
                return "n/a";
            else
               return resp;
        }
        /// <summary>
        /// Processzor egyedi azonsítója, hosza nem változik
        /// </summary>
        /// <returns>pl:20001E354D501320383252</returns>
        public string UniqeId()
        {
            var resp = WriteRead("*UID?");
            if (resp == null)
                return "n/a";
            else
                return resp;
        }
        /// <summary>
        /// Bekapcsolás óta eltelt idő másodpercben
        /// </summary>
        /// <returns>másodperc</returns>
        public int GetUpTime()
        {
            int retval = 0;
            var resp = WriteRead("UPTIME?");
            if (resp == null)
                return 0;
            else if (int.TryParse(resp, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out retval))
                return retval;
            else
                return 0;
        }

        /// <summary>
        /// A panel varáció neve pl: MGUI201222V00-PCREF
        /// </summary>
        /// <returns></returns>
        public string WhoIs()
        {
            var resp = WriteRead("*WHOIS?");
            if (resp == null)
                return "n/a";
            else
                return resp;
        }

        public int FreqLRCK()
        {
            var resp = WriteRead("FRQ:LRCK?");
            if (resp == null)
                return -1;
            else if (int.TryParse(resp, NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out int retval))
                return retval;
            else
                return -1;
        }

        public int FreqBCLK()
        {
            var resp = WriteRead("FRQ:BCLK?");
            if (resp == null)
                return -1;
            else if (int.TryParse(resp, NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out int retval))
                return retval;
            else
                return -1;
        }


        public Modes Mode
        {
            get
            {
                var resp = WriteRead("MODE?");
                if (resp == null)
                    return Modes.MODE_NONE;
                else if (int.TryParse(resp, NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out int retval))
                    return (Modes)retval;
                else
                    return Modes.MODE_NONE;
            }
            set 
            {
                if (WriteRead($"MODE { (int)value}") != "RDY")
                    Trace("IO-ERROR: Invalid Response.");
            }
        }

        public Xtatus XmosStatus
        {
            get 
            {
                var resp = WriteRead("XMOS:STATUS?");
                if (resp == null)
                    return Xtatus.UNKNOWN;
                else if (int.TryParse(resp, NumberStyles.HexNumber, CultureInfo.GetCultureInfo("en-US"), out int retval))
                    return (Xtatus)retval;
                else
                    return Xtatus.UNKNOWN;
            }
        }

        public bool XmosMute
        {
            get
            {
                var resp = WriteRead("XMOS:MUTE?");
                if (resp == null)
                    return false;
                else if (int.TryParse(resp, NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out int retval))
                    return retval == 1;
                else
                    return false;
            }
        }

        public bool SRCBypass
        {
            get
            {
                var resp = WriteRead("SRC:BYPAS?");
                if (resp == null)
                    return false;
                else if (int.TryParse(resp, NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out int retval))
                    return retval==1;
                else
                    return false;
            }
            set 
            {
             if (WriteRead($"SRC:BYPAS {(value == true?1:0)}") != "RDY")
                Trace("IO-ERROR: Invalid Response.");
            }
        }

        public bool SRCMute 
        {
            get 
            {
                var resp = WriteRead("SRC:MUTE?");
                if (resp == null)
                    return false;
                else if (int.TryParse(resp, NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out int retval))
                    return retval == 1;
                else
                    return false;
            }
            set 
            {
                if (WriteRead($"SRC:MUTE {(value == true ? 1 : 0)}") != "RDY")
                Trace("IO-ERROR: Invalid Response.");
            }
        }

        public bool SRCPowerDown
        {
            get
            {
                var resp = WriteRead("SRC:PDN?");
                if (resp == null)
                    return false;
                else if (int.TryParse(resp, NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out int retval))
                    return retval == 1;
                else
                    return false;
            }
            set
            {
                if (WriteRead($"SRC:PDN {(value == true ? 1 : 0)}") != "RDY")
                    Trace("IO-ERROR: Invalid Response.");
            }
        }

        public int Volume1
        {
            get
            {
                var resp = WriteRead("DAC:VOL1?");
                if (resp == null)
                    return 0;
                else if (int.TryParse(resp, NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out int retval))
                    return retval;
                else
                    return 0;
            }
            set
            {
                if (WriteRead($"DAC:VOL1 {value}") != "RDY")
                    Trace("IO-ERROR: Invalid Response.");
            }
        }

        public int Volume2
        {
            get
            {
                var resp = WriteRead("DAC:VOL2?");
                if (resp == null)
                    return 0;
                else if (int.TryParse(resp, NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out int retval))
                    return retval;
                else
                    return 0;
            }
            set
            {
                if (WriteRead($"DAC:VOL2 {value}") != "RDY")
                    Trace("IO-ERROR: Invalid Response.");
            }
        }

        public void Close()
        {
            TraceQueue.Enqueue(DateTime.Now.ToString(GenericTimestampFormat) + " " + "Serial Port is: " + "Close");
            _sp.Close();
            OnConnectionChanged();
        }

        void Trace(string msg)
        {
            if (!TracingEnable)
                return;
            TraceLines++;
            TraceQueue.Enqueue(DateTime.Now.ToString(GenericTimestampFormat) + " " + msg);
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
        protected virtual void OnErrorHappened()
        {
            EventHandler handler = ErrorHappened;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}

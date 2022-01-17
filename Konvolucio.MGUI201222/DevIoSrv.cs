namespace Konvolucio.MGUI201222
{ 
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

       static object SyncObject = new object();

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
                    BaudRate = 460800,
                    NewLine = "\n"
                };
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
            lock (SyncObject)
                
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
                    OnConnectionChanged();
                }

                try
                {

                    str = _sp.ReadLine();
                    Trace("Rx: " + str);
                }
                catch (Exception ex)
                {
                    Trace("Rx ERROR Serial Port is:" + ex.Message);
                    OnConnectionChanged();
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
        public string GetUniqeId()
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
        public string GetWhoIs()
        {
            var resp = WriteRead("*WHOIS?");
            if (resp == null)
                return "n/a";
            else
                return resp;
        }

        /// <summary>
        /// Beállított háttérfényerő százalékban
        /// </summary>
        /// <returns>0..100</returns>
        public int GetDisplayLight()
        {
            var retval = -1;
            var resp = WriteRead("DIS:LIG?");
            if (resp == null)
                return -1;
            else if (int.TryParse(resp, NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out retval))
                return retval;
            else
                return -1;
        }

        public void SetDisplayLight(int percent)
        {
            if (WriteRead("DIS:LIG" + "  " + percent.ToString()) != "RDY")
                Trace("IO-ERROR: Invalid Response.");
        }

        /// <summary>
        /// Power nyomógomb fényerje százalékban.
        /// </summary>
        /// <returns></returns>
        public int GetPowerButtonLight()
        {
            var retval = -1;
            var resp = WriteRead("PBT:LIG?");
            if (resp == null)
                return -1;
            else if (int.TryParse(resp, NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out retval))
                return retval;
            else
                return -1;
        }

        public void SetPowerButtonLight(int percent)
        {
            if(WriteRead("PBT:LIG" + "  " + percent.ToString())!= "RDY")
                Trace("IO-ERROR: Invalid Response.");
        }

        /// <summary>
        /// Egy digitális kimenet bekapcsolása
        /// </summary>
        /// <param name="channel">1..8-ig</param>
        [ObsoleteAttribute("Helyette SetOutputs()")]
        public void SetOnOutput(int channel)
        {
            if (WriteRead("DIG:OUT:ON:BIT" + "  " + channel.ToString()) != "RDY")
                Trace("IO-ERROR: Invalid Response.");

        }

        /// <summary>
        /// Egy digitális kimenet kikapcsolása
        /// </summary>
        /// <param name="channel"></param>
        [ObsoleteAttribute("Helyette SetOutputs()")]
        public void SetOffOutput(int channel)
        {
            if (WriteRead("DIG:OUT:OFF:BIT" + "  " + channel.ToString()) != "RDY")
                Trace("IO-ERROR: Invalid Response.");

        }

        /// <summary>
        /// A digitális kimenetek frissítése, a paraméter alapján.
        /// </summary>
        /// <param name="states"> 0x00.. 0xFF, 0x01 = DO1 </param>
        public void SetOutputs(byte states)
        {
            if (WriteRead("DIG:OUT:SET:U8" + "  " + states.ToString("X2")) != "RDY")
                Trace("IO-ERROR: Invalid Response.");
        }

        /// <summary>
        /// Egy digitális kimenet olvasása
        /// </summary>
        /// <param name="channel">1..16-ig</param>
        /// <returns>True = aktív, Open Drain kimenetek </returns>
        
        [ObsoleteAttribute("Helyette GetOutputs()")]
        public bool GetOutput(int channel)
        {
            var resp = WriteRead($"DIG:OUT:BIT? { channel }");
            if (resp == "0")
                return false;
            else if (resp == "1")
                return true;
            else
                Trace("IO-ERROR: Invalid Response.");
            return false;   
        }




        /// <summary>
        /// Digitális kimenetek lekérdezése
        /// </summary>
        /// <returns> 0x00..0xFF-ig, pl: 0x01 = DO1, 0x80 = DO8 </returns>
        public byte GetOutputs()
        {
            byte retval = 0;
            var resp = WriteRead("DIG:OUT:U8?");
            if (resp == null)
                return 0;
            else if (byte.TryParse(resp, NumberStyles.HexNumber, CultureInfo.GetCultureInfo("en-US"), out retval))
                return retval;
            else
                return 0;
        }

        /// <summary>
        /// Egy digitális bemenet olvasása
        /// Alaphelyzetben minden bemenet magas.
        /// </summary>
        /// <param name="channel">1..16-ig</param>
        /// <returns>True = aktív, alaphelyzetben: True</returns>
        [ObsoleteAttribute("Helyette GetInputs()")]
        public bool GetInput(int channel)
        {
            var resp = WriteRead("DIG:INP:BIT?" + " " + channel.ToString());
            if (resp == "0")
                return false;
            else if (resp == "1")
                return true;
            else
                Trace("IO-ERROR: Invalid Response.");
            return false;
        }

        /// <summary>
        /// Bementek olvasása egy lépésben. Hatékonyabb, ha többet kell egyszerre olvasni
        /// DI1 = 0x0001
        /// DI16 = 0x8000
        /// Alaphelyzetben minden bemenet magas.
        /// </summary>
        /// <returns>0x0000..0xFFFF, alaphelyzetben 0xFFFF, 0x0001 = DI1 </returns>
        public UInt16 GetInputs() 
        {
            UInt16 retval = 0;
            var resp = WriteRead("DIG:INP:U16?");
            if (resp == null)
                return 0;
            else if (UInt16.TryParse(resp, NumberStyles.HexNumber, CultureInfo.GetCultureInfo("en-US"), out retval))
                return retval;
            else
                return 0;
        }

        public void SetDisplay(bool onoff)
        {
            if (WriteRead("DIS:" + (onoff?"ON":"OFF")) != "RDY")
                Trace("IO-ERROR: Invalid Response.");
        }

        public bool GetDisplay()
        {
            var resp = WriteRead("DIS?");
            if (resp == "0")
                return false;
            else if (resp == "1")
                return true;
            else
                Trace("IO-ERROR: Invalid Response.");
            return false;
        }

        public void SetPowerSupply(bool onoff)
        {
            if (WriteRead("PSP:" + (onoff ? "ON" : "OFF")) != "RDY")
                Trace("IO-ERROR: Invalid Response.");
        }

        public bool GetPowerSupply()
        {
            var resp = WriteRead("PSP?");
            if (resp == "0")
                return false;
            else if (resp == "1")
                return true;
            else
                Trace("IO-ERROR: Invalid Response.");
            return false;
        }

        /// <summary>
        /// Hőmérséklet szenzorok értékeinek lekérdezése
        /// </summary>
        /// <param name="channel">1..4-ig</param>
        /// <returns>0..2.5V</returns>
        [ObsoleteAttribute("Helyette GetTemperatures()")]
        public double GetTemperature(int channel)
        {
            double retval = double.NaN;
            var resp = WriteRead("TEM:DBL?" + " " + channel.ToString());
            if (resp == null)
                return double.NaN;
            else if (double.TryParse(resp, NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out retval))
                return retval;
            else
                return double.NaN;
        }
        /// <summary>
        /// Hőmérséklet szenzorok értékeinek lekérdezése egyszerre
        /// </summary>
        /// <returns>0.index = Temp1, 0..2.5V </returns>
        public double[] GetTemperatures()
        {
            var resp = WriteRead("TEM:ARR?");
            string[] valueArr = resp.Split();
            double[] resultArr = new double[valueArr.Length];
            try
            {
                for (int i = 0; i < valueArr.Length; i++)
                    resultArr[i] = double.Parse(valueArr[i].Trim(';'), NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"));
            }
            catch (Exception ex)
            {
                Trace($"IO-ERROR: {ex.Message}");
            }
            return resultArr;
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

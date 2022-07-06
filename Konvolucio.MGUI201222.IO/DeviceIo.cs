﻿
namespace Konvolucio.MGUI201222.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO.Ports;
    using System.Globalization;
    using System.ComponentModel;
    public class DeviceIo
    {
        const string GenericTimestampFormat = "yyyy.MM.dd HH:mm:ss";

        public event EventHandler ConnectionChanged;
        public event EventHandler ErrorHappened;
        public bool TracingEnable { get; set; } = false;
        public static GuiIo Instance { get { return _instance; } }

        private static readonly GuiIo _instance = new GuiIo();

        public Queue<string> TraceQueue = new Queue<string>();
        public int TraceLines { get; private set; }
        
        int _consecutiveRxErrorCounter;
        
        SerialPort _sp;
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

        public static string[] GetPortNames()
        {
            return (SerialPort.GetPortNames());
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
                    BaudRate = 460800,
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


        internal string WriteRead(string str)
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
                    str = _sp.ReadLine().Trim(new char[] { '\0', '\r', '\n' }); ;
                    Trace("Rx: " + str);
                    _consecutiveRxErrorCounter = 0;
                }
                catch (Exception ex) //TODO: Nem jol van kezelve a TIMOUT
                {
                    Trace("Rx ERROR Serial Port is:" + ex.Message);
                    _consecutiveRxErrorCounter++;
                    OnErrorHappened();
                }

                if (_consecutiveRxErrorCounter >= 3)
                {
                    Trace("Három hibás egymást követő válasz! megszakítom a kapcsolatot...");
                    Close();

                    //TODO: Innen ha kilép, akkor a kiküldött értékkel tér vissza...
                }
            }
            return str;
        }
        public void Close()
        {
            TraceQueue.Enqueue(DateTime.Now.ToString(GenericTimestampFormat) + " " + "Serial Port is: " + "Close");
            _sp.Close();
            OnConnectionChanged();
        }

        internal void Trace(string msg)
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

    }
}

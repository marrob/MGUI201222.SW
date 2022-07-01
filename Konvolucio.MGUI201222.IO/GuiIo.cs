namespace Konvolucio.MGUI201222.IO
{ 
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO.Ports;
    using System.Globalization;
    using System.ComponentModel;

    public class GuiIo:DeviceIo
    {
        public event RunWorkerCompletedEventHandler Completed;
        public event ProgressChangedEventHandler ProgressChanged;

        public GuiIo()
        {

        }


        /// <summary>
        /// Beállított háttérfényerő százalékban
        /// </summary>
        /// <returns>0..100</returns>
        public int DisplayLight()
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

        public void DisplayLight(int percent)
        {
            if (WriteRead("DIS:LIG" + "  " + percent.ToString()) != "RDY")
                Trace("IO-ERROR: Invalid Response.");
        }

        /// <summary>
        /// Power nyomógomb fényerje százalékban.
        /// </summary>
        /// <returns></returns>
        public int LedLight()
        {
            var resp = WriteRead("LED:LIG?");
            if (resp == null)
                return -1;
            else if (int.TryParse(resp, NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out int retval))
                return retval;
            else
                return -1;
        }

        public void LedLight(int percent)
        {
            if (WriteRead($"LED:LIG {percent}") != "RDY")
                Trace("IO-ERROR: Invalid Response.");
        }

        public void LedPeriodTime(int periodms)
        {
            if (WriteRead($"LED:PER {periodms}") != "RDY")
                Trace("IO-ERROR: Invalid Response.");
        }
        public void LedDimming(bool onoff)
        {
            if (WriteRead("LED:DIM " + (onoff ? "1" : "0")) != "RDY")
                Trace("IO-ERROR: Invalid Response.");
        }

        public void LedCustomFlashStart()
        {
            if (WriteRead("SEQ:LED:EXE 4") != "RDY")
                Trace("IO-ERROR: Invalid Response.");
        }

        public void LedCustomFlashStop()
        {
            if (WriteRead("SEQ:LED:EXE 0") != "RDY")
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
            var resp = WriteRead($"DIG:OUT:BIT? {channel}");
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
            if (WriteRead("DIS:" + (onoff ? "ON" : "OFF")) != "RDY")
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
            var resp = WriteRead($"TEM:DBL? + {channel} ");
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

        /// <summary>
        /// Elindítja a leállítás folyamatát...
        /// Figyelem! Feltételezi hogy a PC _BE VAN_ kapcsolva. 
        /// </summary>
        public void StartShutdownSequence()
        {
            if (WriteRead("SEQ:PON:EXE 5") != "RDY")
                Trace("IO-ERROR: Invalid Response.");
        }

        public UInt32 GetLogLastAddress()
        {
            UInt32 retval = 0;
            var resp = WriteRead("LOG:LAST?");
            if (resp == null)
                return 0;
            else if (UInt32.TryParse(resp, NumberStyles.HexNumber, CultureInfo.GetCultureInfo("en-US"), out retval))
                return retval;
            else
                return 0;
        }

        public string GetLogLine(UInt32 line)
        {
            var resp = WriteRead($"LOG:LINE? {line:X4} ");
            return resp;
        }


        public void DownloadLog(string path)
        {
            int last = (int)GetLogLastAddress();
            for (uint i = 0; i < last; i++)
            {
                string line = GetLogLine(i) + "\r\n";
                if (System.IO.File.Exists(path))
                    System.IO.File.AppendAllText(path, line);
                else
                    System.IO.File.WriteAllText(path, line);
                OnProgressChanged(this, new ProgressChangedEventArgs((int)(i / last) * 100, null));
            }
            OnClompleted(this, new RunWorkerCompletedEventArgs(null, null, false));
        }


        protected void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
                ProgressChanged(sender, e);
        }

        protected void OnClompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Completed != null)
                Completed(sender, e);
        }



    }
}

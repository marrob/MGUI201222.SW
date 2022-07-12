namespace Konvolucio.MGUI201222.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Globalization;
    using System.ComponentModel;
    using System.Threading;

    public class MemoryInterface : SerialIo, IDisposable
    {
        public static MemoryInterface Instance { get { return _instance; } }
        private static readonly MemoryInterface _instance = new MemoryInterface();

        public const int FRAME_SIZE = 0x100;

        //STM32F207
        public const int APP_FLASH_SIZE = 0x100000 - 0x40000; //-> 768KB
        public const int BTLDR_FLASH_LAST_SECTOR = 5;

        public const int EXT_FLASH_BASE_ADDR = 0x00000000;
        public const int EXT_FLASH_SIZE = 0x02000000;        //0x02000000 -> 32MB

        bool _disposed = false;

        public int FrameSize { get { return FRAME_SIZE; } }
        public int AppFirstSector { get { return 6; } }
        public int AppLastSector { get { return 11; } }
        public int ExtFlashBlockSize { get { return 0x10000; } }

        #region Internal



        public MemoryInterface()
        {

        }

        public MemoryInterface( string COMx)
        {
            Open(COMx);
        }

        public void IntFlashLock()
        { 
            string response = WriteReadWoTracing("FL I");
            if (response != "OK")
            {
                string msg = $"{response} ";
                Trace(msg);
                throw new ApplicationException(msg);
            }
        }

        public void IntFlashUnlock()
        {
            string response = WriteReadWoTracing("FU I");
            if (response != "OK")
            {
                string msg = $"{response}"; 
                Trace(msg);
                throw new ApplicationException(msg);
            }  
        }

        /// <summary>
        /// Flash Sector Erase
        /// Please check the current used chip
        /// </summary>
        /// <param name="num">STM32F207: FLASH_SECTOR_0..FLASH_SECTOR_11</param>
        /// <returns></returns>
        public void IntFlashErase(int sector)
        {
            var response = WriteReadWoTracing($"FE I {sector:X8}");
            if (response != "OK")
            {
                var msg = $"{response}";
                Trace(msg);
                throw new ApplicationException(msg);
            }
        }

        public Byte[] IntFlashRead(int address, int size)
        {
            string result = WriteReadWoTracing($"FR I {address:X8} {size:X3}");

            if (result.Contains("ERROR"))
                new ApplicationException(result);

            var array = result.Split(' '); //addr size data crc
            int rx_bsize;
            int rx_addr;
            int rx_crc;

            int.TryParse(array[0], NumberStyles.HexNumber, CultureInfo.GetCultureInfo("en-US"), out rx_addr);
            int.TryParse(array[1], NumberStyles.HexNumber, CultureInfo.GetCultureInfo("en-US"), out rx_bsize);
            Byte[] rx_data = Tools.StringToByteArray(array[2]);
            int.TryParse(array[3], NumberStyles.HexNumber, CultureInfo.GetCultureInfo("en-US"), out rx_crc);

            if (rx_data.Length != rx_bsize)
                new ApplicationException("Size Error:");
            if (rx_addr != address)
                new ApplicationException("Address Error");
            UInt16 calc_crc = Tools.CalcCrc16Ansi(0, rx_data);
            if (calc_crc != rx_crc)
                new ApplicationException("CRC Error");

            return rx_data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address">Internal size from 0x0000 0000 up to APP_FLASH_SIZE </param>
        /// <param name="data">Cannot longer than the frame/block size</param>
        /// <exception cref="ApplicationException"></exception>
        public void IntFlashWrite(int address, Byte[] data)
        {
            string response = WriteReadWoTracing($"FW I {address:X8} {data.Length:X3} {Tools.ByteArrayToString(data)} {Tools.CalcCrc16Ansi(0, data):X4}");
            if (response != "OK")
            {
                string msg = $"{response}";
                Trace(msg);
                throw new ApplicationException(msg);
            }
        }
        #endregion
        #region External
        public Byte[] ExtFlashRead(int address, int size)
        {
            /*
             * FR E 00000000 00 //cmd addr size
             */
            string result = WriteReadWoTracing($"FR E {address:X8} {size:X3}");

            if (result.Contains('!'))
                new ApplicationException(result);

            var array = result.Split(' '); //addr size data crc
            UInt16 rx_bsize;
            int rx_addr;
            UInt16 rx_crc;

            int.TryParse(array[0], NumberStyles.HexNumber, CultureInfo.GetCultureInfo("en-US"), out rx_addr);
            UInt16.TryParse(array[1], NumberStyles.HexNumber, CultureInfo.GetCultureInfo("en-US"), out rx_bsize);
            Byte[] rx_data = Tools.StringToByteArray(array[2]);
            UInt16.TryParse(array[3], NumberStyles.HexNumber, CultureInfo.GetCultureInfo("en-US"), out rx_crc);

            if (rx_data.Length != rx_bsize)
                new ApplicationException("Size Error:");
            if (rx_addr != address)
                new ApplicationException("Address Error");
            UInt16 calc_crc = Tools.CalcCrc16Ansi(0, rx_data);
            if (calc_crc != rx_crc)
                new ApplicationException("CRC Error");

            return rx_data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address">Internal size from 0x0000 0000 up to APP_FLASH_SIZE </param>
        /// <param name="data">Cannot longer than the frame/block size</param>
        /// <exception cref="ApplicationException"></exception>
        public void ExtFlashWrite(int address, Byte[] data)
        {
            string response = WriteReadWoTracing($"FW E {address:X8} {data.Length:X3} {Tools.ByteArrayToString(data)} {Tools.CalcCrc16Ansi(0, data):X4}");
            if (response != "OK")
            {
                string msg = $"{response}";
                Trace(msg);
                throw new ApplicationException(msg);
            }
        }

        /// <summary>
        /// Block Size = 64kB = 0x10000 byte
        /// </summary>
        /// <param name="address">Absoulte address</param>
        /// <exception cref="ApplicationException"></exception>
        public void ExtFlashBlockErase(int address)
        {
            var response = WriteReadWoTracing($"FE E {address:X8}");
            if (response != "OK")
            {
                var msg = $"{response}";
                Trace(msg);
                throw new ApplicationException(msg);
            }
        }

        public bool ExtrnalFlashIsBusy()
        {
            var response = WriteReadWoTracing($"FB E");
            if (response == "FREE")
                return false;
            else if (response == "BUSY")
                return true;
            else
            {
                var msg = $"Bootloader: Invalid Response: {response}";
                Trace(msg);
                throw new ApplicationException(msg);
            }
        }
        #endregion
        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            _disposed = true;
        }
        #endregion

    }
}

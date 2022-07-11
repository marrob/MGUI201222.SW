

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

    public class Memory : Io, IDisposable
    {

        public const UInt16 FRAME_SIZE = 0x100;

        //STM32F207
        public const UInt32 APP_FLASH_START_ADDR = 0x00040000;
        public const UInt32 APP_FLASH_SIZE = 0x100000 - 0x40000; //-> 768KB


        public const int BTLDR_FLASH_LAST_SECTOR = 5;
        public const UInt32 BTLDR_BASE_ADDR = 0x00000000;
        public const UInt32 BTLDR_SIZE = 0x40000;               //0x40000-> 256KB

        public const UInt32 EXT_FLASH_BASE_ADDR = 0x00000000;
        public const UInt32 EXT_FLASH_SIZE = 0x02000000;        //0x02000000 -> 32MB
        public const UInt32 EXT_FLASH_BLOCK_SIZE = 0x10000;


        bool _disposed = false;


        public int FrameSize { get { return FRAME_SIZE; } }

        #region Internal



        public Memory()
        {

        }

        public Memory( string COMx)
        {
            Open(COMx);
        }

        public void IntFlashLock()
        { 
            string response = WriteRead("FL I");
            if (response != "OK")
            {
                string msg = $"{response} ";
                Trace(msg);
                throw new ApplicationException(msg);
            }
        }

        public void IntFlashUnlock()
        {
            string response = WriteRead("FU I");
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
            var response = WriteRead($"FE I {sector:X8}");
            if (response != "OK")
            {
                var msg = $"{response}";
                Trace(msg);
                throw new ApplicationException(msg);
            }
        }

        public Byte[] IntFlashRead(UInt32 address, int size)
        {
            string result = WriteRead($"FR I {address:X8} {size:X3}");

            if (result.Contains("ERROR"))
                new ApplicationException(result);

            var array = result.Split(' '); //addr size data crc
            UInt16 rx_bsize;
            UInt32 rx_addr;
            UInt16 rx_crc;

            UInt32.TryParse(array[0], NumberStyles.HexNumber, CultureInfo.GetCultureInfo("en-US"), out rx_addr);
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
        public void IntFlashWrite(UInt32 address, Byte[] data)
        {
            string response = WriteRead($"FP I {address:X8} {data.Length:X3} {Tools.ByteArrayToString(data)} {Tools.CalcCrc16Ansi(0, data):X4}");
            if (response != "OK")
            {
                string msg = $"{response}";
                Trace(msg);
                throw new ApplicationException(msg);
            }
        }
        #endregion


        #region External
        public Byte[] ExtFlashRead(UInt32 address, int size)
        {
            /*
             * FR E 00000000 00 //cmd addr size
             */
            string result = WriteRead($"FR E {address:X8} {size:X3}");

            if (result.Contains('!'))
                new ApplicationException(result);

            var array = result.Split(' '); //addr size data crc
            UInt16 rx_bsize;
            UInt32 rx_addr;
            UInt16 rx_crc;

            UInt32.TryParse(array[0], NumberStyles.HexNumber, CultureInfo.GetCultureInfo("en-US"), out rx_addr);
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
        public void ExtFlashWrite(UInt32 address, Byte[] data)
        {
            string response = WriteRead($"FP E {address:X8} {data.Length:X3} {Tools.ByteArrayToString(data)} {Tools.CalcCrc16Ansi(0, data):X4}");
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
        public void ExtFlashBlockErase(UInt32 address)
        {
            var response = WriteRead($"FE E {address:X8}");
            if (response != "OK")
            {
                var msg = $"{response}";
                Trace(msg);
                throw new ApplicationException(msg);
            }
        }

        public bool ExtrnalFlashIsBusy()
        {
            var response = WriteRead($"FB E");
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

        


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            // Free any unmanaged objects here. 
            //
            _disposed = true;
        }

    }
}

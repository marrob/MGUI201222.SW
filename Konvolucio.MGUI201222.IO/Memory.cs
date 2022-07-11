

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

    public class Memory: DeviceIo, IDisposable
    {

        public const UInt16 PAGE_SIZE = 0x100;

        //STM32F207
        public const UInt32 APP_FLASH_START_ADDR = 0x08040000;
        public const UInt32 APP_FLASH_SIZE = 0x100000 - 0x40000; //-> 768KB


        public const int    BTLDR_FLASH_LAST_SECTOR = 5;
        public const UInt32 BTLDR_BASE_ADDR = 0x08000000;
        public const UInt32 BTLDR_SIZE = 0x40000;               //0x40000-> 256KB

        public const UInt32 EXT_FLASH_BASE_ADDR = 0x10000000;
        public const UInt32 EXT_FLASH_SIZE = 0x02000000;        //0x02000000 -> 32MB
        public const UInt32 EXT_FLASH_BLOCK_SIZE = 0x10000;
     
        
        bool _disposed = false;

        public void InternalFlashLock()
        { 
            string response = WriteRead("FL I");
            if (response != "OK")
            {
                string msg = $"{response} ";
                Trace(msg);
                throw new ApplicationException(msg);
            }
        }

        public void InternalFlashUnlock()
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
        /// Internal Flash: 0x0800 0000 ... 
        /// External Flash: 0x1000 0000 ...
        /// </summary>
        /// <param name="address"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void FlashProgram(UInt32 address, Byte[] data)
        {
            UInt32 secotrs = (UInt32)data.Length / PAGE_SIZE;
            byte[] dest = new byte[PAGE_SIZE];
            int sourceOffset = 0;
            for (int i = 0; i < secotrs; i++)
            {
                Buffer.BlockCopy(data, sourceOffset, dest, 0, PAGE_SIZE);
                var response = FlashSectorProgram(address, dest);
                if (response != "OK")
                {
                    string msg = $"{response}";
                    Trace(msg);
                    throw new ApplicationException(msg);
                }
                sourceOffset += PAGE_SIZE;
                address += PAGE_SIZE;
            }

            UInt32 singles = (UInt32)data.Length % PAGE_SIZE;
            dest = new byte[singles];
            if (singles != 0)
            {
                Buffer.BlockCopy(data, sourceOffset, dest, 0, (int)singles);
                var response = FlashSectorProgram(address, dest);
                if (response != "OK")
                {
                    string msg = $"{response}";
                    Trace(msg);
                    throw new ApplicationException(msg);
                }
            }
        }

        private string FlashSectorProgram(UInt32 address, Byte[] data)
        {         
            /*
             * PG 00000000 00 000000000 0000 //cmd addr size data crc         
             */
            string response = WriteRead($"FP {address:X8} {data.Length:X3} {Tools.ByteArrayToString(data)} {Tools.CalcCrc16Ansi(0, data):X4}");
            if (response != "OK")
                Trace("IO-ERROR: Invalid Response." + response);
            return response;
        }

        public Byte[] FlashRead(UInt32 address, int size)
        {
            byte[] retval = new byte[size];
            UInt32 secotrs = (UInt32)size / PAGE_SIZE;
            int destOffset = 0;
            for (int i = 0; i < secotrs; i++)
            {
                byte[] temp = FlashSectorRead(address, PAGE_SIZE);
                Buffer.BlockCopy(temp, 0, retval, destOffset, PAGE_SIZE);
                address += PAGE_SIZE;
                destOffset += PAGE_SIZE;
            }
            UInt16 singles = (UInt16)(size % PAGE_SIZE);
            byte[] bytes = FlashSectorRead(address, singles);
            Buffer.BlockCopy(bytes, 0, retval, destOffset, singles);
            return retval;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private Byte[] FlashSectorRead(UInt32 address, UInt16 size)
        { 
            /*
             * FR 00000000 00 //cmd addr size
             */
            string result = WriteRead($"FR {address:X8} {size:X3}");

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
        /// Flash Sector Erase
        /// Please check the current used chip
        /// </summary>
        /// <param name="num">STM32F207: FLASH_SECTOR_0..FLASH_SECTOR_11</param>
        /// <returns></returns>
        public void InternalFlashErase(int sector)
        {
            var response = WriteRead($"FE I {sector:X8}");
            if (response != "OK")
            {
                var msg = $"{response}";
                Trace(msg);
                throw new ApplicationException(msg);
            }
        }

        /// <summary>
        /// Block Size = 64kB = 0x10000 byte
        /// </summary>
        /// <param name="address">Absoulte address</param>
        /// <exception cref="ApplicationException"></exception>
        public void ExternalFlashBlockErase(UInt32 address)
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

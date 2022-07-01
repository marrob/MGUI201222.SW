

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

    public class Bootloader: DeviceIo, IDisposable
    {
        public const UInt16 FLASH_SECTOR_SIZE = 0x100;
        
        public event RunWorkerCompletedEventHandler Completed
        {
            remove { _bkWorker.RunWorkerCompleted -= value; }
            add { _bkWorker.RunWorkerCompleted += value; }
        }
        public event ProgressChangedEventHandler ProgressChange
        {
            add{ _bkWorker.ProgressChanged += value; }
            remove { _bkWorker.ProgressChanged -= value; }
        }

        readonly BackgroundWorker _bkWorker;
        readonly AutoResetEvent _waitForDoneEvent;
        readonly AutoResetEvent _waitForDelayEvent;
        bool _disposed = false;

        public string FlashLock()
        { 
            string response = WriteRead("FL");
            if (response != "OK")    
                Trace("IO-ERROR: Invalid Response." + response);

            return response;
        }

        public string FlashUnlock()
        {
            string response = WriteRead("FU");
            if (response != "OK")
                Trace("IO-ERROR: Invalid Response." + response);
            return response;
        }

        public string FlashProgram(UInt32 address, Byte[] data)
        {
            /*
             * PG 00000000 00 000000000 0000 //cmd addr size data crc         
             */
            string response = WriteRead($"FP {address:X8} {data.Length:X3} {Tools.ByteArrayToString(data)} {Tools.CalcCrc16Ansi(0, data):X4}");
            if (response != "OK")
                Trace("IO-ERROR: Invalid Response." + response);

            return response;
        }

        public Byte[] FlashRead(UInt32 address, UInt32 size)
        {

            byte[] retval = new byte[size];

            UInt32 secotrs = size / FLASH_SECTOR_SIZE;
            int destOffset = 0;
            for (int i = 0; i < secotrs; i++)
            {
                byte[] temp = FlashSectorRead(address, FLASH_SECTOR_SIZE);
                Buffer.BlockCopy(temp, 0, retval, destOffset, FLASH_SECTOR_SIZE);
                address += FLASH_SECTOR_SIZE;
                destOffset += FLASH_SECTOR_SIZE;
            }

            UInt16 singles = (UInt16)(size % FLASH_SECTOR_SIZE);
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
        /// </summary>
        /// <param name="start">STM32F207: FLASH_SECTOR_0..FLASH_SECTOR_11</param>
        /// <param name="num">STM32F207: FLASH_SECTOR_0..FLASH_SECTOR_11</param>
        /// <returns></returns>
        public UInt32 FlashSectorErase(int start, int num)
        {
            UInt32 retval = 0;
            var resp = WriteRead($"FE {start:X2} {num:X2}");
            if (resp == null)
                return 0;
            else if (UInt32.TryParse(resp, NumberStyles.AllowHexSpecifier, CultureInfo.GetCultureInfo("en-US"), out retval))
                return retval;
            else
                return 0;
        }

        public void Abort()
        {
            if (_bkWorker.IsBusy)
            {
                _waitForDelayEvent.Set();
                _bkWorker.CancelAsync();
                _waitForDoneEvent.WaitOne();
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

            if (disposing)
            {
                // Free any other managed objects here. 
                if (_bkWorker.IsBusy)
                {
                    _bkWorker.CancelAsync();
                    _waitForDoneEvent.WaitOne();
                    Console.WriteLine("Auth Dispose.");
                }
            }

            // Free any unmanaged objects here. 
            //
            _disposed = true;
        }

    }
}

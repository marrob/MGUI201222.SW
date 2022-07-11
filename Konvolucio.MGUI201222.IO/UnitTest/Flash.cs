
namespace Konvolucio.MGUI201222.IO.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using NUnit.Framework;

    [TestFixture]
    public class Flash
    {
        const string FileNameTimestampFormat = "yyMMdd_HHmmss";
        const string COMX = "COM9";
        const string DEVICE_NAME = "BOOTLOADER";
        const string DEVICE_VERSION = "220629_1834";

        const int BTLDR_FLASH_LAST_SECTOR = 5;

        const UInt32 BTLDR_FLASH_END_ADDR = 0x0803FFFF;
        const UInt32 BTLDR_SIZE = 0x40000;

        const UInt32 APP_FLASH_START_ADDR = 0x08040000;
        const UInt32 APP_FLASH_END_ADDR = 0x080FFFFF;

        const UInt32 EXT_FLASH_BASE_ADDR = 0x10000000;
        const UInt32 EXT_FLASH_END_ADDR = 0x01FFFFFF;
        const UInt32 EXT_FLASH_SIZE = 0x02000000;
        const UInt32 EXT_FLASH_64K_BLOCK_SIZE = 0x10000;

        Bootloader _btldr;

        [SetUp]
        public void TestSetup()
        {
            _btldr = new Bootloader();
            _btldr.Open(COMX);

            Assert.AreEqual(DEVICE_NAME, _btldr.WhoIs());
            Assert.AreEqual(DEVICE_VERSION, _btldr.GetVersion());

        }

        [TearDown]
        public void TestCleanUp()
        {
            _btldr.Close();
        }

        #region Internal Flash Low Level Tests
        [Test]
        public void LowlLevelFlashProgram()
        {
            _btldr.InternalFlashUnlock();
            //cmd, size, bytes, crc
            Assert.AreEqual("OK", _btldr.WriteRead($"FP {APP_FLASH_START_ADDR:X8} 003 000102 060C"));
            _btldr.InternalFlashLock();
        }

        [Test]
        public void FlashProgramInvlaidCRC()
        {
            //cmd, size, bytes, crc
            Assert.AreEqual("ERROR: RECEIVED DATA HAS INVALID CRC!",    _btldr.WriteRead($"FP {APP_FLASH_START_ADDR:X8} 003 000102 00FF"));
        }

        [Test]
        public void FlashProgramInvlaidSize()
        {
            //cmd, size, bytes, crc
            Assert.AreEqual("ERROR: RECEIVED DATA SIZE IS INVALID!", _btldr.WriteRead($"FP {APP_FLASH_START_ADDR:X8} 002 000102 060C"));
        }
        #endregion
        #region Internal Flash Exceptions
        [Test]
        public void YouTryToProgramInvalidAddress()
        {
            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _btldr.FlashProgram(0, new byte[] {0x00, 0x01 });
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: YOU TRY TO WRITE INVALID ADDRESS!"));
        }

        [Test]
        public void YouTryToEraseBootloaderSector()
        {
            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _btldr.InternalFlashErase(BTLDR_FLASH_LAST_SECTOR);
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: YOU TRY TO ERASE A BOOTLOADER SECTOR!"));
        }

        [Test]
        public void YouTryToWriteBootloaderArea()
        {
            var toWrite = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x57, 0x6F, 0x72, 0x6C, 0x64 };
            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _btldr.FlashProgram(BTLDR_FLASH_END_ADDR - (BTLDR_SIZE - 1), toWrite);
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: YOU TRY TO WRITE BOOTLOADER AREA!"));
        }

        [Test]
        public void YouTryToWriteOutOfAppFlashArea()
        {
            UInt32 address = 0x09000000;
            var toWrite = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x57, 0x6F, 0x72, 0x6C, 0x64 };
            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _btldr.FlashProgram(address, toWrite);
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: YOU TRY TO WRITE OUT OF APP FLASH AREA!"));
        }
        #endregion
        #region Internal Flash Test

        [Test, Order(1)]
        public void InternalWrtieReadBytes()
        {
            _btldr.InternalFlashUnlock();
            _btldr.InternalFlashErase(6);
            _btldr.InternalFlashLock();

            _btldr.InternalFlashUnlock();
            var toWrite = new byte[]{ 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x57, 0x6F, 0x72, 0x6C, 0x64 };
            _btldr.FlashProgram(APP_FLASH_START_ADDR, toWrite);
            _btldr.InternalFlashLock();

            byte[] toRead = _btldr.FlashRead(APP_FLASH_START_ADDR, 11);
            Console.WriteLine(ASCIIEncoding.ASCII.GetString(toRead));
            Assert.AreEqual(toWrite, toRead);
        }   

        [TestCase((UInt32)0x08040000, 10,  new int[] { 6 })]
        [TestCase((UInt32)0x08040000, 256, new int[] { 6 })]
        [TestCase((UInt32)0x08040000, 4096,new int[] { 6 })]
        [TestCase((UInt32)0x08040001, 10,  new int[] { 6 })]
        [TestCase((UInt32)0x08040002, 256, new int[] { 6 })]
        [TestCase((UInt32)0x08040003, 4096, new int[] { 6 })]
        public void IntWriteReadBytes(UInt32 address, int size, int[] sectors)
        {
            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);

            _btldr.InternalFlashUnlock();
            for(int i=0; i<sectors.Length; i++)
                _btldr.InternalFlashErase(sectors[i]);
            _btldr.FlashProgram(address, toWrite);
            _btldr.InternalFlashLock();
            byte[] toRead = _btldr.FlashRead(address, size);
            Assert.AreEqual(toWrite, toRead);
        }

        #endregion
        #region External Flash Exceptions
        [Test]
        public void ExtFlashTryToEreaseNotExtFlashArea()
        {
            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _btldr.ExternalFlashBlockErase(0);
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: TRY TO ERASE NOT EXT FLASH AREA!"));
        }



        [TestCase((UInt32)0x00000001, 256)]
        public void ExtFlashNotAligned(UInt32 address, int size)
        {
            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);

            long timestamp = DateTime.Now.Ticks;
            _btldr.ExternalFlashBlockErase(EXT_FLASH_BASE_ADDR);
            do
            {
                if ((DateTime.Now.Ticks - timestamp) > 1000 * 10000)
                    throw new TimeoutException();
            } while (_btldr.ExtrnalFlashIsBusy());

            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _btldr.FlashProgram(EXT_FLASH_BASE_ADDR + address, toWrite);
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: NOT ALIGNED!"));
        }

        [Test]
        public void ExtFlashTryToEraseOutOfExtFlashArea()
        {
            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _btldr.ExternalFlashBlockErase(EXT_FLASH_BASE_ADDR + EXT_FLASH_SIZE);
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: YOU TRY TO ERASE OUT OF EXT FLASH AREA!"));
        }

        [Test]
        public void ExtFlashTryToWriteOutOfExtFlashArea()
        {
            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _btldr.FlashProgram(EXT_FLASH_BASE_ADDR + EXT_FLASH_SIZE, new byte[] { 0 });
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: YOU TRY TO WRITE OUT OF EXT FLASH AREA!"));
        }
        #endregion
        #region External Flash Tests

        [Test]
        public void ExtFlashNoBusy()
        {
            Assert.AreEqual(false, _btldr.ExtrnalFlashIsBusy());
        }

        [Test]
        public void ExtFlashBusy()
        {
            _btldr.ExternalFlashBlockErase(EXT_FLASH_BASE_ADDR);
            Assert.AreEqual(true, _btldr.ExtrnalFlashIsBusy());

            long timestamp = DateTime.Now.Ticks;
            do
            {
                if ((DateTime.Now.Ticks - timestamp) > 1000 * 10000)
                    throw new TimeoutException();
            } while (_btldr.ExtrnalFlashIsBusy());
        }

        [Test]
        public void ExtWriteReadBytes()
        {
            _btldr.ExternalFlashBlockErase(EXT_FLASH_BASE_ADDR);
            Stopwatch sw = new Stopwatch();
            sw.Start();

            long timestamp = DateTime.Now.Ticks;
            do
            {
                if ((DateTime.Now.Ticks - timestamp) > 1000 * 10000)
                    throw new TimeoutException();
            } while (_btldr.ExtrnalFlashIsBusy());
            sw.Stop();

            Console.WriteLine($"Sector Erase Elapsed Time: {sw.ElapsedMilliseconds/1000}sec");

            var toWrite = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x57, 0x6F, 0x72, 0x6C, 0x64 };
            _btldr.FlashProgram(EXT_FLASH_BASE_ADDR, toWrite);

            byte[] toRead = _btldr.FlashRead(EXT_FLASH_BASE_ADDR, 11);
            Console.WriteLine(ASCIIEncoding.ASCII.GetString(toRead));

            Assert.AreEqual(toWrite, toRead);
        }

       [Test]
        public void ExtFlashWriteReadLastByte()
        {
            UInt32 address = EXT_FLASH_BASE_ADDR + (EXT_FLASH_SIZE - 1) - 1;
            int size = 1;

            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);

            UInt32 last64kBlockAddress =  EXT_FLASH_SIZE - EXT_FLASH_64K_BLOCK_SIZE;
            _btldr.ExternalFlashBlockErase(EXT_FLASH_BASE_ADDR + last64kBlockAddress);
            long timestamp = DateTime.Now.Ticks;
            do
            {
                if ((DateTime.Now.Ticks - timestamp) > 1000 * 10000)
                    throw new TimeoutException();
            } while (_btldr.ExtrnalFlashIsBusy());

            _btldr.FlashProgram(address, toWrite);
            byte[] toRead = _btldr.FlashRead(address, size);
            Assert.AreEqual(toWrite, toRead);
        }


        [TestCase((UInt32)0x00000000, 10)]
        [TestCase((UInt32)0x00000000, 256)]
        [TestCase((UInt32)0x00000000, 4096 )]
        [TestCase((UInt32)0x00000000, 65536)]
        [TestCase((UInt32)0x00000001, 255)]
        public void ExtFlashWriteRead(UInt32 address, int size)
        {
            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);

            long timestamp = DateTime.Now.Ticks;
            _btldr.ExternalFlashBlockErase(EXT_FLASH_BASE_ADDR);
            do
            {
                if ((DateTime.Now.Ticks - timestamp) > 1000 * 10000)
                    throw new TimeoutException();
            } while (_btldr.ExtrnalFlashIsBusy());

            _btldr.FlashProgram(EXT_FLASH_BASE_ADDR + address, toWrite);
            byte[] toRead = _btldr.FlashRead(EXT_FLASH_BASE_ADDR + address, size);
            Assert.AreEqual(toWrite, toRead);
        }

        #endregion
    }
}

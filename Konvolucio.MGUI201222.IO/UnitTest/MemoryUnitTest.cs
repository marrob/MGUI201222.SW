
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
    public class MemoryUnitTest
    {
        const string FileNameTimestampFormat = "yyMMdd_HHmmss";
        const string COMX = "COM9";
        const string DEVICE_NAME = "BOOTLOADER";
        const string DEVICE_VERSION = "220629_1834";

        Memory _mem;

        [SetUp]
        public void TestSetup()
        {
            _mem = new Memory();
            _mem.Open(COMX);

            Assert.AreEqual(DEVICE_NAME, _mem.WhoIs());
            Assert.AreEqual(DEVICE_VERSION, _mem.GetVersion());
        }

        [TearDown]
        public void TestCleanUp()
        {
            _mem.Close();
        }

        #region Internal Flash Low Level Tests
        [Test]
        public void IntSectorsErase()
        {
            _mem.IntFlashUnlock();
            for (int i = _mem.AppFirstSector; i <= _mem.AppLastSector; i++)
                _mem.IntFlashErase(i);
            _mem.IntFlashLock();
        }

        [Test]
        public void LowlLevelIntFlashWrite()
        {
            UInt32 address = 0;
            _mem.IntFlashUnlock();
            Assert.AreEqual("OK", _mem.WriteRead($"FW I {address:X8} 003 000102 060C"));
            _mem.IntFlashLock();
        }

        [Test]
        public void IntFlashWriteInvlaidCRC()
        {
            UInt32 address = 0;
            Assert.AreEqual("ERROR: RECEIVED DATA HAS INVALID CRC!", _mem.WriteRead($"FW I {address:X8} 003 000102 00FF"));
        }

        [Test]
        public void FlashProgramInvlaidSize()
        {
            UInt32 address = 0;
            Assert.AreEqual("ERROR: RECEIVED DATA SIZE IS INVALID!", _mem.WriteRead($"FW I {address:X8} 002 000102 060C"));
        }
        #endregion
        #region Internal Flash Exceptions

        [Test]
        public void YouTryToEraseOutOfAppFlashSector()
        {
            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _mem.IntFlashErase(_mem.AppLastSector + 1);
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: YOU TRY TO ERASE OUT OF APP FLASH SECTOR!"));
        }

        [Test]
        public void YouTryToEraseBootloaderSector()
        {
            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _mem.IntFlashErase(Memory.BTLDR_FLASH_LAST_SECTOR);
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: YOU TRY TO ERASE A BOOTLOADER SECTOR!"));
        }


        [Test]
        public void YouTryToWriteOutOfAppFlashArea()
        {
            UInt32 address = Memory.APP_FLASH_SIZE ;
            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _mem.IntFlashWrite(address, new byte[] { 0, 1 });
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: YOU TRY TO WRITE OUT OF APP FLASH AREA!"));
        }
        #endregion
        #region Internal Flash Test

        [Test, Order(1)]
        public void IntFlashWrtieRead()
        {
            _mem.IntFlashUnlock();
            _mem.IntFlashErase(6);
            _mem.IntFlashLock();

            _mem.IntFlashUnlock();
            var toWrite = new byte[]{ 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x57, 0x6F, 0x72, 0x6C, 0x64 };
            _mem.IntFlashWrite(0, toWrite);
            _mem.IntFlashLock();

            byte[] toRead = _mem.IntFlashRead(0, 11);
            Console.WriteLine(ASCIIEncoding.ASCII.GetString(toRead));
            Assert.AreEqual(toWrite, toRead);
        }   

        [TestCase((UInt32)0x00000000, 10,  new int[] { 6 })]
        [TestCase((UInt32)0x00000000, 256, new int[] { 6 })]
        public void IntWriteReadBytes(UInt32 address, int size, int[] sectors)
        {
            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);

            _mem.IntFlashUnlock();
            for(int i=0; i<sectors.Length; i++)
                _mem.IntFlashErase(sectors[i]);
            _mem.IntFlashWrite(address, toWrite);
            _mem.IntFlashLock();
            byte[] toRead = _mem.IntFlashRead(address, size);
            Assert.AreEqual(toWrite, toRead);
        }

        #endregion
        #region External Flash Exceptions

        [TestCase((UInt32)0x00000001, 256)]
        public void ExtFlashNotAligned(UInt32 address, int size)
        {
            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);

            long timestamp = DateTime.Now.Ticks;
            _mem.ExtFlashBlockErase(Memory.EXT_FLASH_BASE_ADDR);
            do
            {
                if ((DateTime.Now.Ticks - timestamp) > 1000 * 10000)
                    throw new TimeoutException();
            } while (_mem.ExtrnalFlashIsBusy());

            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _mem.ExtFlashWrite(Memory.EXT_FLASH_BASE_ADDR + address, toWrite);
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: NOT ALIGNED!"));
        }

        [Test]
        public void ExtFlashTryToEraseOutOfExtFlashArea()
        {
            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _mem.ExtFlashBlockErase(Memory.EXT_FLASH_BASE_ADDR + Memory.EXT_FLASH_SIZE);
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: YOU TRY TO ERASE OUT OF EXT FLASH AREA!"));
        }

        [Test]
        public void ExtFlashTryToWriteOutOfExtFlashArea()
        {
            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _mem.ExtFlashWrite(Memory.EXT_FLASH_BASE_ADDR + Memory.EXT_FLASH_SIZE, new byte[] { 0 });
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: YOU TRY TO WRITE OUT OF EXT FLASH AREA!"));
        }
        #endregion
        #region External Flash Tests

        [Test]
        public void ExtFlashNoBusy()
        {
            Assert.AreEqual(false, _mem.ExtrnalFlashIsBusy());
        }

        [Test]
        public void ExtFlashBusy()
        {
            _mem.ExtFlashBlockErase(Memory.EXT_FLASH_BASE_ADDR);
            Assert.AreEqual(true, _mem.ExtrnalFlashIsBusy());

            long timestamp = DateTime.Now.Ticks;
            do
            {
                if ((DateTime.Now.Ticks - timestamp) > 1000 * 10000)
                    throw new TimeoutException();
            } while (_mem.ExtrnalFlashIsBusy());
        }

        [Test]
        public void ExtWriteReadBytes()
        {
            _mem.ExtFlashBlockErase(Memory.EXT_FLASH_BASE_ADDR);
            Stopwatch sw = new Stopwatch();
            sw.Start();

            long timestamp = DateTime.Now.Ticks;
            do
            {
                if ((DateTime.Now.Ticks - timestamp) > 1000 * 10000)
                    throw new TimeoutException();
            } while (_mem.ExtrnalFlashIsBusy());
            sw.Stop();

            Console.WriteLine($"Sector Erase Elapsed Time: {sw.ElapsedMilliseconds/1000}sec");

            var toWrite = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x57, 0x6F, 0x72, 0x6C, 0x64 };
            _mem.ExtFlashWrite(Memory.EXT_FLASH_BASE_ADDR, toWrite);

            byte[] toRead = _mem.ExtFlashRead(Memory.EXT_FLASH_BASE_ADDR, 11);
            Console.WriteLine(ASCIIEncoding.ASCII.GetString(toRead));

            Assert.AreEqual(toWrite, toRead);
        }

       [Test]
        public void ExtFlashWriteReadLastByte()
        {
            UInt32 address = Memory.EXT_FLASH_BASE_ADDR + (Memory.EXT_FLASH_SIZE - 1) - 1;
            int size = 1;

            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);

            UInt32 last64kBlockAddress = Memory.EXT_FLASH_SIZE - Memory.EXT_FLASH_BLOCK_SIZE;
            _mem.ExtFlashBlockErase(Memory.EXT_FLASH_BASE_ADDR + last64kBlockAddress);
            long timestamp = DateTime.Now.Ticks;
            do
            {
                if ((DateTime.Now.Ticks - timestamp) > 1000 * 10000)
                    throw new TimeoutException();
            } while (_mem.ExtrnalFlashIsBusy());

            _mem.ExtFlashWrite(address, toWrite);
            byte[] toRead = _mem.ExtFlashRead(address, size);
            Assert.AreEqual(toWrite, toRead);
        }


        [TestCase((UInt32)0x00000000, 10)]
        [TestCase((UInt32)0x00000000, 256)]
        [TestCase((UInt32)0x00000001, 255)]
        public void ExtFlashWriteRead(UInt32 address, int size)
        {
            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);

            long timestamp = DateTime.Now.Ticks;
            _mem.ExtFlashBlockErase(Memory.EXT_FLASH_BASE_ADDR);
            do
            {
                if ((DateTime.Now.Ticks - timestamp) > 1000 * 10000)
                    throw new TimeoutException();
            } while (_mem.ExtrnalFlashIsBusy());

            _mem.ExtFlashWrite(Memory.EXT_FLASH_BASE_ADDR + address, toWrite);
            byte[] toRead = _mem.ExtFlashRead(Memory.EXT_FLASH_BASE_ADDR + address, size);
            Assert.AreEqual(toWrite, toRead);
        }

        /// <summary>
        /// Elapsed time: 2.5min
        /// </summary>
        /// <exception cref="TimeoutException"></exception>
        [Test]
        public void ExtFlashErase()
        {
            UInt32 blocks = Memory.EXT_FLASH_SIZE / Memory.EXT_FLASH_BLOCK_SIZE;
            UInt32 addr = Memory.EXT_FLASH_BASE_ADDR;

            for (int b = 0; b < blocks; b++)
            { 
                long timestamp = DateTime.Now.Ticks;
                _mem.ExtFlashBlockErase(addr);
                do
                {
                    if ((DateTime.Now.Ticks - timestamp) > 1000 * 10000)
                        throw new TimeoutException();
                } while (_mem.ExtrnalFlashIsBusy());

                addr+= Memory.EXT_FLASH_BLOCK_SIZE;
            }    
        }
        #endregion
    }
}

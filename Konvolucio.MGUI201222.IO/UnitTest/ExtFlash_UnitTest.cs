
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
    public class ExtFlash_UnitTest
    {

        MemoryInterface _mem;

        [SetUp]
        public void TestSetup()
        {
            _mem = new MemoryInterface();
            _mem.Open(DutConstatns.PORT);
            Assert.AreEqual(DutConstatns.DEVICE_NAME, _mem.WhoIs());
        }
        [TearDown]
        public void TestCleanUp()
        {
            _mem.Close();
        }
        #region External Flash Exceptions

        [TestCase((int)0x00000001, 256)]
        public void ExtFlashNotAligned(int address, int size)
        {
            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);

            long timestamp = DateTime.Now.Ticks;
            _mem.ExtFlashBlockErase(MemoryInterface.EXT_FLASH_BASE_ADDR);

            if ((DateTime.Now.Ticks - timestamp) > 1000 * 10000)
                throw new TimeoutException();
            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _mem.ExtFlashWrite(MemoryInterface.EXT_FLASH_BASE_ADDR + address, toWrite);
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: NOT ALIGNED!"));
        }

        [Test]
        public void ExtFlashTryToEraseOutOfExtFlashArea()
        {
            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _mem.ExtFlashBlockErase(MemoryInterface.EXT_FLASH_BASE_ADDR + MemoryInterface.EXT_FLASH_SIZE);
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: YOU TRY TO ERASE OUT OF EXT FLASH AREA!"));
        }

        [Test]
        public void ExtFlashTryToWriteOutOfExtFlashArea()
        {
            Exception ex = Assert.Throws<ApplicationException>(delegate
            {
                _mem.ExtFlashWrite(MemoryInterface.EXT_FLASH_BASE_ADDR + MemoryInterface.EXT_FLASH_SIZE, new byte[] { 0 });
            });
            Assert.That(ex.Message, Is.EqualTo("ERROR: YOU TRY TO WRITE OUT OF EXT FLASH AREA!"));
        }
        #endregion
        #region External Flash Tests

        [Test]
        public void ExtWriteReadBytes()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            _mem.ExtFlashBlockErase(MemoryInterface.EXT_FLASH_BASE_ADDR);
            Console.WriteLine($"Sector Erase Elapsed Time: {sw.ElapsedMilliseconds/1000}sec");
            sw.Stop();
            var toWrite = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x57, 0x6F, 0x72, 0x6C, 0x64 };
            _mem.ExtFlashWrite(MemoryInterface.EXT_FLASH_BASE_ADDR, toWrite);

            byte[] toRead = _mem.ExtFlashRead(MemoryInterface.EXT_FLASH_BASE_ADDR, 11);
            Console.WriteLine(ASCIIEncoding.ASCII.GetString(toRead));

            Assert.AreEqual(toWrite, toRead);
        }

       [Test]
        public void ExtFlashWriteReadLastByte()
        {
            int address = MemoryInterface.EXT_FLASH_BASE_ADDR + (MemoryInterface.EXT_FLASH_SIZE - 1) - 1;
            int size = 1;

            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);

            int last64kBlockAddress = MemoryInterface.EXT_FLASH_SIZE - _mem.ExtFlashBlockSize;
            _mem.ExtFlashBlockErase(MemoryInterface.EXT_FLASH_BASE_ADDR + last64kBlockAddress);

            _mem.ExtFlashWrite(address, toWrite);
            byte[] toRead = _mem.ExtFlashRead(address, size);
            Assert.AreEqual(toWrite, toRead);
        }


        [TestCase((int)0x00000000, 10)]
        [TestCase((int)0x00000000, 256)]
        [TestCase((int)0x00000001, 255)]
        public void ExtFlashWriteRead(int address, int size)
        {
            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);

            long timestamp = DateTime.Now.Ticks;
            _mem.ExtFlashBlockErase(MemoryInterface.EXT_FLASH_BASE_ADDR);

            _mem.ExtFlashWrite(MemoryInterface.EXT_FLASH_BASE_ADDR + address, toWrite);
            byte[] toRead = _mem.ExtFlashRead(MemoryInterface.EXT_FLASH_BASE_ADDR + address, size);
            Assert.AreEqual(toWrite, toRead);
        }

        /// <summary>
        /// Elapsed time: 2.5min
        /// </summary>
        /// <exception cref="TimeoutException"></exception>
        [Test]
        public void ExtFlashErase()
        {
            int blocks = MemoryInterface.EXT_FLASH_SIZE / _mem.ExtFlashBlockSize;
            int address = MemoryInterface.EXT_FLASH_BASE_ADDR;

            for (int b = 0; b < blocks; b++)
            { 
                long timestamp = DateTime.Now.Ticks;
                _mem.ExtFlashBlockErase(address);
                address+= _mem.ExtFlashBlockSize;
            }    
        }
        #endregion
    }
}

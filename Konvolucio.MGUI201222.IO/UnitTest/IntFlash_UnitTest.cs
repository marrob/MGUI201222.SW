
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
    public class IntFlash_UnitTest
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

        #region Internal Flash Low Level Tests
        [Test]
        public void IntFlashErase()
        {
            _mem.IntFlashUnlock();
            long timestamp = DateTime.Now.Ticks;
            string status = "";
            do
            {
                if ((DateTime.Now.Ticks - timestamp) > 7000 * 10000)
                    throw new TimeoutException();
                Console.WriteLine(status);
            } while (!_mem.IntFlashEraseCompleted(out status));
            _mem.IntFlashLock();
        }

        [Test]
        public void LowlLevelIntFlashWrite()
        {
            int address = 0;
            _mem.IntFlashUnlock();
            Assert.AreEqual("OK", _mem.WriteRead($"FW I {address:X8} 003 000102 060C"));
            _mem.IntFlashLock();
        }

        [Test]
        public void IntFlashWriteInvlaidCRC()
        {
            int address = 0;
            Assert.AreEqual("ERROR: RECEIVED DATA HAS INVALID CRC!", _mem.WriteRead($"FW I {address:X8} 003 000102 00FF"));
        }

        [Test]
        public void FlashProgramInvlaidSize()
        {
            int address = 0;
            Assert.AreEqual("ERROR: RECEIVED DATA SIZE IS INVALID!", _mem.WriteRead($"FW I {address:X8} 002 000102 060C"));
        }
        #endregion
        #region Internal Flash Exceptions





        [Test]
        public void YouTryToWriteOutOfAppFlashArea()
        {
            int address = MemoryInterface.APP_FLASH_SIZE ;
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
            /*** Erase Internal Flash ***/
            _mem.IntFlashUnlock();
            long timestamp = DateTime.Now.Ticks;
            string status = "";
            do
            {
                if ((DateTime.Now.Ticks - timestamp) > 7000 * 10000)
                    throw new TimeoutException();
                Console.WriteLine(status);
            } while (!_mem.IntFlashEraseCompleted(out status));
            _mem.IntFlashLock();

            _mem.IntFlashUnlock();
            var toWrite = new byte[]{ 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x57, 0x6F, 0x72, 0x6C, 0x64 };
            _mem.IntFlashWrite(0, toWrite);
            _mem.IntFlashLock();

            byte[] toRead = _mem.IntFlashRead(0, 11);
            Console.WriteLine(ASCIIEncoding.ASCII.GetString(toRead));
            Assert.AreEqual(toWrite, toRead);
        }   

        [TestCase((int)0x00000000, 10,  new int[] { 6 })]
        [TestCase((int)0x00000000, 256, new int[] { 6 })]
        public void IntWriteReadBytes(int address, int size, int[] sectors)
        {
            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);

            /*** Erase Internal Flash ***/
            _mem.IntFlashUnlock();
            long timestamp = DateTime.Now.Ticks;
            string status = "";
            do
            {
                if ((DateTime.Now.Ticks - timestamp) > 7000 * 10000)
                    throw new TimeoutException();
                Console.WriteLine(status);
            } while (!_mem.IntFlashEraseCompleted(out status));


            _mem.IntFlashWrite(address, toWrite);
            _mem.IntFlashLock();
            byte[] toRead = _mem.IntFlashRead(address, size);
            Assert.AreEqual(toWrite, toRead);
        }

        #endregion
    }
}

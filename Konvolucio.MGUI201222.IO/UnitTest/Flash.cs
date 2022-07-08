
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
        const int EXT_FLASH_START_ADDR = 0x10000000;

        const UInt32 BTLDR_FLASH_END_ADDR = 0x0803FFFF;
        const UInt32 BTLDR_SIZE = 0x3FFFF;

        const UInt32 APP_FLASH_START_ADDR = 0x08040000;
        const UInt32 APP_FLASH_END_ADDR = 0x080FFFFF;

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

        #region Test Of Exceptions
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
                _btldr.FlashProgram(BTLDR_FLASH_END_ADDR - BTLDR_SIZE, toWrite);
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
        public void IntWriteReadBytes(UInt32 address, int bytes, int[] sectors)
        {
            byte[] toWrite = new byte[bytes];
            new Random().NextBytes(toWrite);

            _btldr.InternalFlashUnlock();
            for(int i=0; i<sectors.Length; i++)
                _btldr.InternalFlashErase(sectors[i]);
            _btldr.FlashProgram(address, toWrite);
            _btldr.InternalFlashLock();
            byte[] toRead = _btldr.FlashRead(address, bytes);
            Assert.AreEqual(toWrite, toRead);
        }

        #endregion


        #region External Flash Tests

        [Test]
        public void ExtFlashNoBusy()
        {
            Assert.AreEqual(false, _btldr.ExtrnalFlashIsBusy());
        }

        [Test]
        public void ExtWriteReadBytes()
        {
            _btldr.ExternalFlashUnlock();
            _btldr.ExternalFlashErase();


            Stopwatch sw = new Stopwatch();
            sw.Start();

            do
            {

            } while (_btldr.ExtrnalFlashIsBusy());
            _btldr.ExternalFlashLock();
            sw.Stop();

            Console.WriteLine($"Chip Erase Elapsed Time: {sw.ElapsedMilliseconds/1000}sec");

            _btldr.ExternalFlashUnlock();
            var toWrite = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x57, 0x6F, 0x72, 0x6C, 0x64 };
            _btldr.FlashProgram(EXT_FLASH_START_ADDR, toWrite);
            _btldr.ExternalFlashLock();

            byte[] toRead = _btldr.FlashRead(EXT_FLASH_START_ADDR, 11);
            Console.WriteLine(ASCIIEncoding.ASCII.GetString(toRead));

            Assert.AreEqual(toWrite, toRead);
        }

        #endregion
    }
}

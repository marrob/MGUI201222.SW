
namespace Konvolucio.MGUI201222.IO.UnitTest
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
using NUnit.Framework;

    [TestFixture]
    public class UploadDownloadUnitTest
    {
        const string FileNameTimestampFormat = "yyMMdd_HHmmss";
        const string COMX = "COM9";
        const string DEVICE_NAME = "BOOTLOADER";
        const string DEVICE_VERSION = "220629_1834";

        Memory _mem;    

        [SetUp]
        public void TestSetup()
        {
            _mem = new Memory(COMX);
            Assert.AreEqual(DEVICE_NAME, _mem.WhoIs());
            Assert.AreEqual(DEVICE_VERSION, _mem.GetVersion());
        }

        [TearDown]
        public void TestCleanUp()
        {
            _mem.Close();
        }


        [Test]
        public void IntFlashDownload()
        {
            var wait = new AutoResetEvent(false);

            int size = 1024;
            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);

            var ifd =  new IntFlashDownload(_mem);
            ifd.Begin(0x00000000, size);

            ifd.Completed += (s, e) =>
            {
                byte[] data = (byte[])e.Result;
                Assert.AreNotEqual(0, data[0]);
                wait.Set();
            };

            ifd.ProgressChange += (s, e) => Console.WriteLine(e.UserState);

            Assert.IsTrue(wait.WaitOne(TimeSpan.FromSeconds(5))); 
        }

        [Test]
        public void IntFlashUpload()
        {
            var wait = new AutoResetEvent(false);
            int addr = (int)Memory.APP_FLASH_START_ADDR;

            int size = 8;
            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);
            byte[] toRead = null;
            object result = null;
            _mem.IntFlashUnlock();
            _mem.IntFlashErase(6);

            _mem.IntFlashUnlock();
            var ifu = new IntFlashUpload(_mem);
            ifu.Begin(addr, toWrite);
            ifu.ProgressChange += (s, e) => Console.WriteLine(e.UserState);
            ifu.Completed += (s, e) =>
            {
                result = e.Result;
                wait.Set();
            };

            if (!wait.WaitOne(TimeSpan.FromSeconds(1000)))
                Assert.Fail("Timeout");

            Assert.IsFalse(result is Exception);


            var ifd = new IntFlashDownload(_mem);
            ifd.Begin(addr, size);
            ifd.ProgressChange += (s, e) => Console.WriteLine(e.UserState);
            ifd.Completed += (s, e) =>
            {
                toRead = (byte[])e.Result;
                wait.Set();
            };

            if (!wait.WaitOne(TimeSpan.FromSeconds(1000)))
                Assert.Fail("Timeout");
            Assert.AreEqual(toWrite, toRead);

        }

    }


}

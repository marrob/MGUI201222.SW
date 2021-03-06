
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
    public class UpDown_UnitTest
    {
        MemoryInterface _mem;    

        [SetUp]
        public void TestSetup()
        {
            _mem = new MemoryInterface(DutConstatns.PORT);
            Assert.AreEqual(DutConstatns.DEVICE_NAME, _mem.WhoIs());
        }

        [TearDown]
        public void TestCleanUp()
        {
            _mem.Close();
        }

        #region Internal Flash
        [Test]
        public void IntFlashUpload()
        {
            var wait = new AutoResetEvent(false);

            int addr = 0;
            int size = 8;
            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);
            byte[] toRead = null;
            object result = null;

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

            if (result is Exception)
                Assert.Fail((result as Exception).Message);

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

        [TestCase(0x00000000, 1, Description =        "1B    |6.8s  |3.5s")] 
        [TestCase(0x00000001, 1, Description =        "1B    |6.7s  |3.3s")]
        [TestCase(0x000000FF, 1, Description =        "1B    |6.7s  |3.4s")]
        [TestCase(0x00000000, 1024, Description =     "1kB   |6.9s  |3.5s")]
        [TestCase(0x00000000, 65536, Description =    "64kB  |12.9s |14.7s")]
        [TestCase(0x00000000, 262144, Description =   "256kB |30.8s |49.3")]
        [TestCase(0x00000000, 786432-1, Description = "768kB |1.3m  |2.3m")]
        public void IntFlashUpDown(int address, int size)
        {
            var wait = new AutoResetEvent(false);

            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);
            byte[] toRead = null;
            object result = null;

            var ifu = new IntFlashUpload(_mem);
            ifu.Begin(address, toWrite);
            ifu.ProgressChange += (s, e) => Console.WriteLine(e.UserState);
            ifu.Completed += (s, e) =>
            {
                result = e.Result;
                wait.Set();
            };

            if (!wait.WaitOne(TimeSpan.FromSeconds(1000)))
                Assert.Fail("Timeout");

            if (result is Exception)
                Assert.Fail((result as Exception).Message);

            var ifd = new IntFlashDownload(_mem);
            ifd.Begin(address, size);
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

        #endregion

        #region ExternalFlash
        [Test]
        public void ExtFlashUpload()
        {
            var wait = new AutoResetEvent(false);
            int address = 0;
            int size = 8;
            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);
            byte[] toRead = null;
            object result = null;

            var efu = new ExtFlashUpload(_mem);
            efu.Begin(address, toWrite);
            efu.ProgressChange += (s, e) => Console.WriteLine(e.UserState);
            efu.Completed += (s, e) =>
            {
                result = e.Result;
                wait.Set();
            };

            if (!wait.WaitOne(TimeSpan.FromSeconds(1000)))
                Assert.Fail("Timeout");

            if (result is Exception)
                Assert.Fail((result as Exception).Message);

            var efd = new ExtFlashDownload(_mem);
            efd.Begin(address, size);
            efd.ProgressChange += (s, e) => Console.WriteLine(e.UserState);
            efd.Completed += (s, e) =>
            {
                toRead = (byte[])e.Result;
                wait.Set();
            };

            if (!wait.WaitOne(TimeSpan.FromSeconds(1000)))
                Assert.Fail("Timeout");
            Assert.AreEqual(toWrite, toRead);
        }
        //                                                  |F2      |220715-F7  
        [TestCase(0x00000000, 1, Description =      "1B     |464ms   |635ms ")]
        [TestCase(0x00000000, 1024, Description =   "1kB    |435ms   |694ms")]
        [TestCase(0x00000000, 65536, Description =  "64kB   |5.5s    |11.s " )]
        [TestCase(0x00000000, 262144, Description = "256kB  |21s     |47s")]
        [TestCase(0x00000000, 524288, Description = "512kBs |43s     |1.6m")]
        public void ExtFlashUpDown(int address, int size)
        {
            var wait = new AutoResetEvent(false);
            byte[] toWrite = new byte[size];
            new Random().NextBytes(toWrite);
            byte[] toRead = null;
            object result = null;

            var efu = new ExtFlashUpload(_mem);
            efu.Begin(address, toWrite);
            efu.ProgressChange += (s, e) => Console.WriteLine(e.UserState);
            efu.Completed += (s, e) =>
            {
                result = e.Result;
                wait.Set();
            };

            if (!wait.WaitOne(TimeSpan.FromSeconds(1000)))
                Assert.Fail("Timeout");

            if (result is Exception)
                Assert.Fail((result as Exception).Message);

            var efd = new ExtFlashDownload(_mem);
            efd.Begin(address, size);
            efd.ProgressChange += (s, e) => Console.WriteLine(e.UserState);
            efd.Completed += (s, e) =>
            {
                toRead = (byte[])e.Result;
                wait.Set();
            };

            if (!wait.WaitOne(TimeSpan.FromSeconds(1000)))
                Assert.Fail("Timeout");
            Assert.AreEqual(toWrite, toRead);
        }
        #endregion
    }


}

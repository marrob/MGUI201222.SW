
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

        [Test]
        public void InternalWrtieReadBytes()
        {
            var srv = new Bootloader();
            srv.Open(COMX);

            Assert.AreEqual("BOOTLOADER", srv.WhoIs());
            Assert.AreEqual("220629_1834", srv.GetVersion());

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Assert.AreEqual("OK",srv.FlashUnlock());
            Assert.AreEqual("OK", srv.FlashSectorErase(5));
            Assert.AreEqual("OK", srv.FlashSectorErase(6));
            Assert.AreEqual("OK", srv.FlashLock());

            Assert.AreEqual("OK", srv.FlashUnlock());
            var toWrite = new byte[]{ 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x57, 0x6F, 0x72, 0x6C, 0x64 };
            Assert.AreEqual("OK", srv.FlashProgram(0x08010000, toWrite));
            Assert.AreEqual("OK", srv.FlashLock());


            byte[] toRead = srv.FlashRead(0x08010000, 11);
            sw.Stop();

            Assert.AreEqual(toWrite, toRead);

            Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds / 1000.0} sec");

            srv.Close();
        }


    }
}

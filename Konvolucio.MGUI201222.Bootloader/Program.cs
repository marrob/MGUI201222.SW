

namespace Konvolucio.MGUI201222.Bootloader
{
  
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using IO;
    using System.Diagnostics;
        
    internal class Program
    {
        static void Main(string[] args)
        {
            new App();
        }
    }


    class App
    {
        Bootloader srv = new Bootloader();
        const string FileNameTimestampFormat = "yyMMdd_HHmmss";
        const string COMX = "COM9";

        public App()
        {
            string path = $"D:\\test {DateTime.Now.ToString(FileNameTimestampFormat)}.txt";

              //GenerateRandomBytesFile(path, 983040);
             ReadFileProgramWriteFile();

           // WrtieRead();

        }

        void ReadFileProgramWriteFile()
        {
            string path = $"D:\\test {DateTime.Now.ToString(FileNameTimestampFormat)}.txt";

            srv.Open(COMX);
            Console.WriteLine($"Name:{srv.WhoIs()}");
            Console.WriteLine($"Uptime:{srv.GetUpTime()}");
            Console.WriteLine($"UID:{srv.UniqeId()}");
            Console.WriteLine($"VER:{srv.GetVersion()}");

            //byte[] source = Tools.OpenFile($"D:\\VECTOR-BYTES-RANDOM-257B.bin");
            //byte[] source = Tools.OpenFile($"D:\\VECTOR-BYTES-RANDOM-16B.bin");
            byte[] source = Tools.OpenFile($"D:\\VECTOR-BYTES-RANDOM-983040B.bin");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Console.WriteLine($"Unlock:{srv.FlashUnlock()}");
            Console.WriteLine($"Erase:{srv.FlashSectorErase(4, 1):X08}");
            Console.WriteLine($"Lock: {srv.FlashLock()}");

            Console.WriteLine($"Unlock: {srv.FlashUnlock()}");
            Console.WriteLine($"Program:{srv.FlashProgram(0x08010000, source)}");
            Console.WriteLine($"Lock: {srv.FlashLock()}");

            byte[] dest = srv.FlashRead(0x08010000, (UInt32)source.Length);
            Tools.CreateFile(path, dest);

            sw.Stop();
            Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds / 1000.0} sec");

            srv.Close();
            Console.ReadLine();
        }

        void WrtieRead()
        {
            string path = $"D:\\test {DateTime.Now.ToString(FileNameTimestampFormat)}.txt";

            srv.Open(COMX);
            Console.WriteLine($"Name:{srv.WhoIs()}");
            Console.WriteLine($"Uptime:{srv.GetUpTime()}");
            Console.WriteLine($"UID:{srv.UniqeId()}");
            Console.WriteLine($"VER:{srv.GetVersion()}");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Console.WriteLine($"Unlock:{srv.FlashUnlock()}");
            Console.WriteLine($"Erase:{srv.FlashSectorErase(4, 2):X08}");
            Console.WriteLine($"Lock: {srv.FlashLock()}");

            Console.WriteLine($"Unlock: {srv.FlashUnlock()}");
            Console.WriteLine($"Program:{srv.FlashProgram(0x08010000, new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38 })}");
            Console.WriteLine($"Lock: {srv.FlashLock()}");


            byte[] data = srv.FlashRead(0x08010000, 10);
            Tools.CreateFile(path, data);

            sw.Stop();
            Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds / 1000.0} sec");

            srv.Close();
            Console.ReadLine();
        }

        void GenerateRandomBytesFile(string path, int size)
        {
            byte[] bytes = new byte[size];
            new Random().NextBytes(bytes);
            Tools.CreateFile(path, bytes);
        }
    }
}

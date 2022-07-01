

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


        public App()
        {
            string path = $"D:\\test {DateTime.Now.ToString(FileNameTimestampFormat)}.txt";

            srv.Open("COM9");
            Console.WriteLine($"Name:{srv.WhoIs()}");
            Console.WriteLine($"Uptime:{srv.GetUpTime()}");
            Console.WriteLine($"UID:{srv.UniqeId()}");
            Console.WriteLine($"VER:{srv.GetVersion()}");



            //srv.FlashProgram(0x0FFFFFFF, new byte[] { 0x01, 0x02, 0x03 });

            Stopwatch sw = new Stopwatch();
            sw.Start();

            //byte[] data =  srv.FlashRead(0x08000000, 393215 + 1);   //0x5FFFF     383KB  
            //byte[] data = srv.FlashRead(0x08000000, 0xFFFFF + 1);   //0xFFFFF     1MB  

            //#define FLASH_SECTOR_TOTAL  12U

            Console.WriteLine($"Unlock:{srv.FlashUnlock()}");
            Console.WriteLine($"Erase:{srv.FlashSectorErase(4, 2):X08}");
            Console.WriteLine($"Lock: {srv.FlashLock()}");

            Console.WriteLine($"Unlock: {srv.FlashUnlock()}");
            Console.WriteLine($"Program:{srv.FlashProgram(0x08010000, new byte[] { 0x31, 0x32, 0x33, 0x33, 0x31, 0x32, 0x33, 0x33 })}" );

            Console.WriteLine($"Lock: {srv.FlashLock()}");
            byte[] data = srv.FlashRead(0x08010000, 10);
            Tools.CreateFile(path, data);

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds / 1000.0);
                
           //Console.WriteLine($"Erase:{ srv.FlashSectorErase(2, 1):X8}");

            srv.Close();
            Console.ReadLine();

        }
    }

}

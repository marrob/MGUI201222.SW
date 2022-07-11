
namespace Konvolucio.MGUI201222.DFU.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Tools
    {
        /// <summary>
        /// Bináris beolvasása bájt tömbe
        /// </summary>
        /// <param name="fullPath">Teljes elérési utvonal.</param>
        /// <param name="databytes">Adatbájtok</param>
        public static byte[] OpenFile(string fullPath)
        {
            byte[] databytes;
            using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
            {
                databytes = new byte[fs.Length];
                fs.Read(databytes, 0, databytes.Length);
            }

            return databytes;
        }

        public static void OpenFolder(string path)
        {
            Process.Start("explorer.exe", path);
        }


        public static void RunNotepadOrNpp(string path)
        {
            var myProcess = new Process();
            myProcess.StartInfo.Arguments = "\"" + path + "\"";
            myProcess.StartInfo.FileName = "Notepad++";
            try
            {
                myProcess.Start();
            }
            catch (Exception)
            {
                myProcess.StartInfo.FileName = "Notepad";
                myProcess.Start();
            }
        }
    }
}

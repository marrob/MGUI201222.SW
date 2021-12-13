namespace Konvolucio.MGUI201222
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Diagnostics;


    public class AppLog
    {
        public static AppLog Instance { get; } = new AppLog();
        public string FilePath = Application.StartupPath;
        public bool Enabled;
        public int Lines;

        public double? GetFileSizeKB
        {
            get
            {
                if (File.Exists(FilePath))
                {
                    FileInfo fi = new FileInfo(FilePath);
                    return fi.Length / 1024;
                }
                else
                    return null;
            }
        }

        public AppLog()
        {
            Enabled = true;
            FilePath = "AppLog.txt";
        }

        public int? GetRecodsCount
        {
            get
            {
                return Lines;
            }
        }

        public void FileOpenProces()
        {

            var myProcess = new Process();
            myProcess.StartInfo.Arguments = "\"" + FilePath + "\"";
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

        public void WirteLine(string line)
        {
            if (Enabled)
            {
                Lines++;
                line = DateTime.Now.ToString(AppConstants.GenericTimestampFormat, System.Globalization.CultureInfo.InvariantCulture) + ";" + line.Trim() + AppConstants.NewLine;
                var fileWrite = new StreamWriter(FilePath, true, Encoding.ASCII);
                fileWrite.NewLine = AppConstants.NewLine;
                fileWrite.Write(line);
                fileWrite.Flush();
                fileWrite.Close();
            }
        }
    }
}

namespace Konvolucio.MGUI201222.DFU
{
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class Log
    {
        public static Log Instance { get; } = new Log();
        public string FilePath = Application.StartupPath;
        public bool Enabled;

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

        public Log()
        {
            Enabled = true;
            FilePath = $"Log_{DateTime.Now:AppConstants.FileNameTimestampFormat}.txt ";
        }

        public void WirteLine(string line)
        {
            if (Enabled)
            {
                var fileWrite = new StreamWriter(FilePath, true, Encoding.ASCII);
                fileWrite.NewLine = AppConstants.NewLine;
                fileWrite.Write(line + AppConstants.NewLine);
                fileWrite.Flush();
                fileWrite.Close();
            }
        }
    }
}

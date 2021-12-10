
namespace Konvolucio.MCEL181123.Calib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public class IoLog
    {
        public static IoLog Instance { get; } = new IoLog();
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

        public IoLog()
        {
            Enabled = true;
            FilePath = "IoLog.txt";
            WirteLine("**** Appliction Start ****");
        }

        public void WirteLine(string line)
        {
            if (Enabled)
            {
                line = DateTime.Now.ToString(AppConstants.GenericTimestampFormat, System.Globalization.CultureInfo.InvariantCulture) + ";" + line + AppConstants.NewLine;
                var fileWrite = new StreamWriter(FilePath, true, Encoding.ASCII);
                fileWrite.NewLine = AppConstants.NewLine;
                fileWrite.Write(line);
                fileWrite.Flush();
                fileWrite.Close();
            }
        }
    }
}

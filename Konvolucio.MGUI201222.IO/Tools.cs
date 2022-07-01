

namespace Konvolucio.MGUI201222.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;

    public static class Tools
    {
        /// <summary>
        /// Az bájt tömb érték konvertálása string.
        /// </summary>
        /// <param name="byteArray">byte array</param>
        /// <param name="offset">az ofszettől kezdődően kezdődik a konvertálás</param>
        /// <returns>string pl.: (00FFAA) </returns>
        public static string ByteArrayToString(byte[] byteArray)
        {
            string retval = string.Empty;
            for (int i = 0; i < +byteArray.Length; i++)
                retval += string.Format("{0:X2}", byteArray[i]);
            return (retval);

            //String.Join(String.Empty, Array.ConvertAll(bytes, x => x.ToString("X2")));
            //String.Concat(Array.ConvertAll(bytes, x => x.ToString("X2")));
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static UInt16 CalcCrc16Ansi(UInt16 initValue, byte[] data)
        {
            UInt16 remainder = initValue;
            UInt16 polynomial = 0x8005;
            for (long i = 0; i < data.LongLength; ++i)
            {
                remainder ^= (UInt16)(data[i] << 8);
                for (byte bit = 8; bit > 0; --bit)
                {
                    if ((remainder & 0x8000) != 0)
                        remainder = (UInt16)((remainder << 1) ^ polynomial);
                    else
                        remainder = (UInt16)(remainder << 1);
                }
            }
            return (remainder);
        }

        /// <summary>
        /// Bináris beolvasása bájt tömbe
        /// </summary>
        /// <param name="fullPath">Teljes elérési utvonal.</param>
        /// <param name="databytes">Adatbájtok</param>
        public static byte[] OpenFile(string path)
        {
            byte[] databytes;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                databytes = new byte[fs.Length];
                fs.Read(databytes, 0, databytes.Length);
            }
            return databytes;
        }

        public static void CreateFile (string path, byte[] data)
        {
            using (FileStream fs = new FileStream(path, FileMode.CreateNew))
                fs.Write(data, 0, data.Length);
        }
    }
}

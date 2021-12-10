namespace Konvolucio.MCEL181123.Calib.Common
{
    using System;
    using System.Linq.Expressions;
    using System.Runtime.InteropServices;

    public static class Tools
    {
        /// <summary>
        /// Az bájt tömb érték konvertálása string.
        /// </summary>
        /// <param name="byteArray">byte array</param>
        /// <param name="offset">az ofszettől kezdődően kezdődik a konvertálás</param>
        /// <returns>string pl.: (00 FF AA) </returns>
        public static string ByteArrayLogString(byte[] byteArray)
        {
            string retval = string.Empty;
       
            for (int i = 0 ; i< + byteArray.Length; i++)
              retval += string.Format("{0:X2} ", byteArray[i]);

                if (byteArray.Length > 1)
                    retval = retval.Remove(retval.Length - 1, 1);
            return (retval);
        }

        /// <summary>
        /// Tulajdonság nevének megszerzése, még a tulajdonságban
        /// Típikus haszánlata:
        /// Minőségi szoftver estén használd!!!!
        /// <code> 
        /// set 
        /// { 
        ///     if (_year != value) 
        ///     {
        ///        _year = value; 
        ///        OnPropertyChanged(GetPropertyName(() => Year)); 
        ///     } 
        /// }</code>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="propertyId"></param>
        /// <returns></returns>
        public static String GetPropertyName<TValue>(Expression<Func<TValue>> propertyId)
        {
            return ((MemberExpression)propertyId.Body).Member.Name;
        }
    }
}

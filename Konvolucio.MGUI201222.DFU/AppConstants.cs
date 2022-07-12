namespace Konvolucio.MGUI201222.DFU
{
    internal class AppConstants
    {
        public const string ValueNotAvailable2 = "n/a";
        public const string InvalidFlieNameChar = "A file name can't contain any of flowing characters:";
        public const string SoftwareCustomer = "Konvolúció Bt.";
        public const string SoftwareTitle = "MGUI201222 - DIAG TOOL";
        public const string GenericTimestampFormat = "yyyy.MM.dd HH:mm:ss";
        public const string FileNameTimestampFormat = "yyMMdd_HHmmss";
        public const string FileFilter = "*.csv,*.mes,*.typ|*.csv;*.mes;*.typ|*.csv|*.csv|*.mes|*.mes|*.typ|*.mes";
        public const string NewLine = "\r\n";
        public const string CsvFileSeparator = ",";

        public static readonly string[] DeviceNames =  
            {
                "KARUNA",  
                "PCREF", 
                "DAC" 
            };
        public const int DEV_GUI = 0;
        public const int DEV_DAC = 1;
    }
}

namespace Konvolucio.MCEL181123.Calib.Events
{
    class ConnectionChangedAppEvent : IApplicationEvent
    {
        
        public bool IsOpen { get; set; }
        
        public ConnectionChangedAppEvent(bool isOpen)
        {
            IsOpen = isOpen;
        }
    }
}

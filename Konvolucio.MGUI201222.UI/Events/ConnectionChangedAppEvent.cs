namespace Konvolucio.MGUI201222.Events
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

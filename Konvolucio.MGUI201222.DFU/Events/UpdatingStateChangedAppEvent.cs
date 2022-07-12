namespace Konvolucio.MGUI201222.DFU.Events
{
    class UpdatingStateChangedAppEvent : IApplicationEvent
    {
        public bool IsRunning { get; set; }
        
        public UpdatingStateChangedAppEvent(bool isRunning)
        {
            IsRunning = isRunning;
        }
    }
}

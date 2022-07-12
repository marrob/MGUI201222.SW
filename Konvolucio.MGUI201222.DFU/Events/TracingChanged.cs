namespace Konvolucio.MGUI201222.DFU.Events
{
    class TracingChanged : IApplicationEvent
    {
        public bool Enabled { get; private set; } 
        public TracingChanged(bool enabled)
        {
            Enabled = enabled;
        }
    }
}

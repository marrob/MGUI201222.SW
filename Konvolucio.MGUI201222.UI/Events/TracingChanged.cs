namespace Konvolucio.MGUI201222.Events
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

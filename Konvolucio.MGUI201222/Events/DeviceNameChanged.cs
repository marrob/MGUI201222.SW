namespace Konvolucio.MGUI201222.Events
{
    class DeviceNameChanged : IApplicationEvent
    {
        public string DeviceName { get; private set; } 
        public DeviceNameChanged(string name)
        {
            DeviceName = name;
        }
    }
}

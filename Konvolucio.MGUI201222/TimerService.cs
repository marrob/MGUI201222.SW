namespace Konvolucio.MGUI201222
{
    using System;
    using System.Windows.Forms; /*Timer*/


    public interface ITimerService :IDisposable
    {
        event EventHandler Tick;
        
        int Interval { get; set; }
        void Start();
        void Stop();
    }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TimerService: ITimerService
    {

        /// <summary>
        /// Interval időközönként meghívja a Tick-et
        /// </summary>
        public event EventHandler Tick;

        public int Interval
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        public static ITimerService Instance { get { return _instance; } }

        private static readonly TimerService _instance = new TimerService();

        private readonly Timer _timer;

        public TimerService()
        {
            _timer = new Timer();
            _timer.Tick += Update;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        void Update(object sender, EventArgs e)
        {
             Tick?.Invoke(this,EventArgs.Empty);
        }

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }
        }
    }
}

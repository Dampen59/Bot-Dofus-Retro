using System;
using System.Threading;

namespace Bot_Dofus_1._29._1.Utilidades
{
    class TimerWrapper : IDisposable
    {
        private Timer timer;
        public bool enabled { get; private set; }
        public int interval { get; set; }

        public TimerWrapper(int _interval, TimerCallback callback)
        {
            interval = _interval;
            timer = new Timer(callback, null, Timeout.Infinite, Timeout.Infinite);
        }

        public void Start(bool inmediatamente = false)
        {
            if (enabled)
                return;

            enabled = true;
            timer.Change(inmediatamente ? 0 : interval, interval);
        }

        public void Stop()
        {
            if (!enabled)
                return;

            enabled = false;
            timer?.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public void Dispose()
        {
            timer?.Dispose();
            timer = null;
        }
    }
}

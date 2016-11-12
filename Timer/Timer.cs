using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Timer
{
    public class Timer : Stopwatch
    {
        public new Stop Start()
        {
            base.Start();
            return new Stop(this);
        }
        public Stop Continue()
        {
            return Start();
        }
    }

    public class Stop : IDisposable
    {
        private Timer timer;
        public Stop(Timer timer)
        {
            this.timer = timer;
        }
        public void Dispose()
        {
            timer.Stop();
        }
    }
}

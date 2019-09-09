using System;

namespace Tracer.exception
{
    public class TimerException :Exception
    {
        public TimerException(string message) : base(message)
        { }
    }
}
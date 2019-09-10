using System;

namespace Tracer.exception
{
    public class StopException:Exception
    {
        public StopException(string message) : base(message)
        { }
    }
}
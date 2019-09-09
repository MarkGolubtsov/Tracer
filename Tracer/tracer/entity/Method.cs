using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using Tracer.exception;

namespace Tracer.tracer.entity
{
    public class Method
    {
        public Method(string name,string clazz)
        {
            this.clazz = clazz;
            this.name = name;
            methods = new ConcurrentStack<Method>();
            stopwatch = new Stopwatch();
        }

        public long time
        {
            get {
                if (stopwatch.IsRunning)
                {
                    throw new TimerException("Timer not stopped!");
                }
                return stopwatch.ElapsedMilliseconds; }
        }

        private string clazz { get; set; }
        private string name { get; set; }
        private Stopwatch stopwatch { get; set; }
        private ConcurrentStack<Method> methods { get; set; }

        public void addMethod(Method method)
        {
            methods.Push(method);
        }
        public void Stop()
        {
            stopwatch.Stop();
        }
    }
}
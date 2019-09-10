﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Tracer.exception;

namespace Tracer.tracer.entity
{
    public class Method
    {
        public Method(string name,string methodClass)
        {
            this.methodClass = methodClass;
            stopwatch=new Stopwatch();
            this.name = name;
            methods = new ConcurrentStack<Method>();
        }
        public long time { get; set; }

        public void  balanceTime(long delete)
        {
            time = time - delete;
        }

        public void startTimer()
        {
            stopwatch.Start();
        }
        private string methodClass { get; set; }
        private string name { get; set; }
        private Stopwatch stopwatch { get;}
        private ConcurrentStack<Method> methods { get; set; }

        public void addMethod(Method method)
        {
            methods.Push(method);
        }
        public void stopTimer()
        {
            stopwatch.Stop();
            time = stopwatch.ElapsedMilliseconds;
           
        }
    }
}
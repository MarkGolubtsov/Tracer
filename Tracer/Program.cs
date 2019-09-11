

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Tracer.tracer;
using Tracer.tracer.entity;
using Tracer.tracer.output;
using Tracer.tracer.serilize.impl;

namespace Tracer
{
    class Program
    {
        
        static void Main(string[] args)
        {
            test();
        }

        static void test()
        {
            tracer.impl.Tracer tracer = new tracer.impl.Tracer();
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
            ResultTrace resultTrace = tracer.GetResult();
            resultTrace.OutPut(new ConsoleOutPut(),  new JsonSerializeImpl());
        }
    }
}
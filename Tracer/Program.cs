

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Tracer.tracer.entity;

namespace Tracer
{
    class Program
    {
        
        static void Main(string[] args)
        {
          tracer.impl.Tracer tracer = new tracer.impl.Tracer();
          test test1 = new test(tracer);
          test1.main();
          
          List<ThreadTracer> res=test1.Tracer.test();
          Console.WriteLine(res[1].time);
        }
    }
}
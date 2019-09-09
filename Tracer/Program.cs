using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Tracer
{
    class Program
    {


        static void Main(string[] args)
        {
           
            Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine(stopwatch.ElapsedTicks);
            stopwatch.Restart();
            Console.WriteLine(stopwatch.ElapsedTicks);
            Console.WriteLine(stopwatch.ElapsedTicks);
        }
    }
}
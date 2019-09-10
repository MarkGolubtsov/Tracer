using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Tracer
{
    class Program
    {


        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();
            
            Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Thread.Sleep(100 );
        Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
using System;
using Tracer.tracer.output;

namespace Tracer.tracer
{
    public class ConsoleOutPut :IOutPutTracerResult
    {
        public  ConsoleOutPut(){}
        public void output(string result)
        {
            Console.WriteLine(result);
        }
    }
}
using System;

namespace Tracer.tracer.output
{
    public class ConsoleOutPut :OutPutTracerResult
    {
        public  ConsoleOutPut(){}
        public void output(string result)
        {
            Console.WriteLine(result);
        }
    }
}
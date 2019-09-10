using System;
using System.Threading;

namespace Tracer
{
    public class test
    {
        public tracer.impl.Tracer Tracer;
        public test(tracer.impl.Tracer tracer)
        {
            this.Tracer = tracer;
        }

        public void main()
        {
            test1();
            test2();
            Thread myThread = new Thread(new ParameterizedThreadStart(testThread));
            myThread.Start(Tracer);
            myThread.Join();
        }
        
        public void testThread(Object tracer)
        {
            tracer.impl.Tracer tracer1= (tracer.impl.Tracer) tracer;
            tracer1.StartTrace();
            Thread.Sleep(111);
            tracer1.StopTrace();
        }
        public void  test1()
        {
            Tracer.StartTrace();
            Thread.Sleep(100);
            Tracer.StopTrace();
        }

        public void test2()
        {
            Tracer.StartTrace();
            Thread.Sleep(50);
            Tracer.StopTrace();
        }
    }
}
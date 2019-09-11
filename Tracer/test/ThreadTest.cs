using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using Tracer.tracer.entity;
using Tracer.tracer.serilize.impl;

namespace Tracer.tracer.test
{
    [TestFixture]
    public class ThreadTest
    {
        [Test]
        public void main()
        {
            impl.Tracer tracer = new impl.Tracer();
            tracer.StartTrace();
            Thread.Sleep(10);
            tracer.StopTrace();
            Thread thread = new Thread(new ParameterizedThreadStart(Do1));
            thread.Start(tracer);
            thread.Join();
            List<ThreadTracer> TracerResult = tracer.GetResult().GetThreadTracers();
            tracer.GetResult().OutPut(new ConsoleOutPut(), new JsonSerializeImpl());
            Assert.AreEqual(2,TracerResult.Count);
        }

        private static void Do1(Object tracer)
        {
           impl.Tracer tracerTest = (impl.Tracer) tracer;
            tracerTest.StartTrace();
            Thread.Sleep(100);
            tracerTest.StopTrace();
        }
    }
}
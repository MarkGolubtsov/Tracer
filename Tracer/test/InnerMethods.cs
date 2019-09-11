using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using Tracer.tracer.entity;
using Tracer.tracer.output;
using Tracer.tracer.serilize.impl;

namespace Tracer.tracer.test
{
    [TestFixture]
    public class InnerMethods
    {
        [Test]
        public void main()
        {
            impl.Tracer tracer = new impl.Tracer();
            tracer.StartTrace();
            firstMethod(tracer);
            Thread.Sleep(100);
            tracer.StopTrace();
            main2(tracer);
            List<ThreadTracer> TracerResult = tracer.GetResult().GetThreadTracers();
            tracer.GetResult().OutPut(new ConsoleOutPut(), new JsonSerializeImpl());
            Assert.AreEqual("firstMethod",TracerResult[0].methods[1].methods[0].name);
        }

        public void main2(impl.Tracer tracer)
        {
            tracer.StartTrace();
            Thread.Sleep(10);
            tracer.StopTrace();
        }

        public void firstMethod(impl.Tracer tracer)
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            secondMethod(tracer);
            tracer.StopTrace();
        }

        public void secondMethod(impl.Tracer tracer)
        {
            tracer.StartTrace();
            Thread.Sleep(120);
            tracer.StopTrace();
        }


    }
}
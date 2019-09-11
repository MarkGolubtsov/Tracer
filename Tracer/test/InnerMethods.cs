using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using Tracer.tracer.entity;
using Tracer.tracer.serilize.impl;

namespace Tracer.tracer.test
{
    [TestFixture]
    public class InnerMethods
    {
        [Test]
        public void Main()
        {
            var tracer = new impl.Tracer();
            tracer.StartTrace();
            FirstMethod(tracer);
            Thread.Sleep(100);
            tracer.StopTrace();
            Main2(tracer);
            List<ThreadTracer> tracerResult = tracer.GetResult().GetThreadTracers();
            tracer.GetResult().OutPut(new ConsoleOutPut(), new JsonSerializeImpl());
            Assert.AreEqual("FirstMethod",tracerResult[0].Methods[1].Methods[0].Name);
        }
        public void Main2(impl.Tracer tracer)
        {
            tracer.StartTrace();
            Thread.Sleep(10);
            tracer.StopTrace();
        }
        public void FirstMethod(impl.Tracer tracer)
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            SecondMethod(tracer);
            tracer.StopTrace();
        }
        public void SecondMethod(impl.Tracer tracer)
        {
            tracer.StartTrace();
            Thread.Sleep(120);
            tracer.StopTrace();
        }
    }
}

using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using Tracer.tracer.entity;

namespace Tracer.tracer.test
{
    [TestFixture]
    public class OneMethodTest
    {

        [Test]
        public void main()
        {
            tracer.impl.Tracer tracer = new tracer.impl.Tracer();
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
            List<ThreadTracer> TracerResult =tracer.GetResult().GetThreadTracers();
            Assert.AreEqual(1 , TracerResult.Count);
        }
        
    }
}
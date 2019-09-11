using System;
using System.Collections.Generic;
using NUnit.Framework;
using Tracer.tracer.entity;
using Tracer.tracer.output;
using Tracer.tracer.serilize;

namespace Tracer.tracer
{
    public interface IResultTrace
    {
        void OutPut(IOutPutTracerResult outPutTracerResult, ISerializeTracerResult serializeTracerResult);
        List<ThreadTracer> GetThreadTracers();
    }
}
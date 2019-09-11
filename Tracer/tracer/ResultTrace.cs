using System;
using System.Collections.Generic;
using NUnit.Framework;
using Tracer.tracer.entity;
using Tracer.tracer.output;
using Tracer.tracer.serilize;

namespace Tracer.tracer
{
    public interface ResultTrace
    {
        void OutPut(OutPutTracerResult outPutTracerResult, SerializeTracerResult serializeTracerResult);
        List<ThreadTracer> GetThreadTracers();
    }
}
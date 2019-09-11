using System.Collections.Generic;
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
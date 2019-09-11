using System;
using Tracer.tracer.output;
using Tracer.tracer.serilize;

namespace Tracer.tracer
{
    public interface ResultTrace
    {
        void OutPut(OutPutTracerResult outPutTracerResult, SerializeTracerResult serializeTracerResult);
    }
}
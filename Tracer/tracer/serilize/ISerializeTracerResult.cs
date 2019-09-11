using System.Collections.Generic;
using Tracer.tracer.entity;

namespace Tracer.tracer.serilize
{
    public interface ISerializeTracerResult
    {
        string GetString(List<ThreadTracer> list);
    }
}
using System.Collections.Generic;
using Tracer.tracer.entity;

namespace Tracer.tracer.serilize
{
    public interface SerializeTracerResult
    {
        string getString(List<ThreadTracer> list);
    }
}
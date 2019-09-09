using System.Diagnostics;

namespace Tracer.tracer
{
    public interface ITracer
    {
        void StartTrace();
        void StopTrace();
        ResultTrace GetResult();
    }
}
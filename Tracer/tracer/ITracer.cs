namespace Tracer.tracer
{
    public interface ITracer
    {
        void StartTrace();
        void StopTrace();
        IResultTrace GetResult();
    }
}
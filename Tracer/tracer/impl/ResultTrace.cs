using System.Collections.Generic;
using Tracer.tracer.entity;
using Tracer.tracer.output;
using Tracer.tracer.serilize;

namespace Tracer.tracer.impl
{
    public class ResultTrace : IResultTrace
    {
        private List<ThreadTracer> _list;
        public ResultTrace(List<ThreadTracer> list)
        {
            this._list = list;
        }
        public void OutPut(IOutPutTracerResult outPutTracerResult, ISerializeTracerResult serializeTracerResult)
        {
            outPutTracerResult.output(serializeTracerResult.GetString(_list));
        }

        public List<ThreadTracer> GetThreadTracers()
        {
            return _list;
        }
    }
}
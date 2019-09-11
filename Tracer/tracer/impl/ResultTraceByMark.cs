using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Tracer.tracer.entity;
using Tracer.tracer.output;
using Tracer.tracer.serilize;

namespace Tracer.tracer.impl
{
    public class ResultTraceByMark:ResultTrace
    {
        private List<ThreadTracer> list;

        public ResultTraceByMark(List<ThreadTracer> list)
        {
            this.list = list;
        }
        public void OutPut(OutPutTracerResult outPutTracerResult, SerializeTracerResult serializeTracerResult)
        {
            outPutTracerResult.output(serializeTracerResult.getString(list));
        }
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;
using Tracer.tracer.entity;

namespace Tracer.tracer.serilize.impl
{
    public class JsonSerializeImpl : ISerializeTracerResult
    {
        public string GetString(List<ThreadTracer> list)
        {
            return JsonConvert.SerializeObject(list,Formatting.Indented);
        }
    }
}
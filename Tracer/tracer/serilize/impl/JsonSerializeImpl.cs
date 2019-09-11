using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Tracer.tracer.entity;

namespace Tracer.tracer.serilize.impl
{
    public class JsonSerializeImpl : ISerializeTracerResult
    {
        public string getString(List<ThreadTracer> list)
        {
            return JsonConvert.SerializeObject(list,Formatting.Indented);
        }
    }
}
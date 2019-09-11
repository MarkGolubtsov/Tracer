using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Tracer.tracer.entity;

namespace Tracer.tracer.serilize.impl
{
    public class JsonSerializeImpl : SerializeTracerResult
    {
        public string getString(List<ThreadTracer> list)
        {
            var ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<ThreadTracer>));
            ser.WriteObject(ms, ser);
            return ser.ToString();  
        }
    }
}
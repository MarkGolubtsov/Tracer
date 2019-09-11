﻿using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Tracer.tracer.entity;

namespace Tracer.tracer.serilize.impl
{
    public class XmlSerializeImpl : SerializeTracerResult
    {
        public  XmlSerializeImpl(){}
        public string getString(List<ThreadTracer> list)
        {
            StringWriter stream = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(List<ThreadTracer>));
            serializer.Serialize(stream,list);
            return  stream.ToString();
        }
    }
}
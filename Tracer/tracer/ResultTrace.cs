using System;

namespace Tracer.tracer
{
    public interface ResultTrace
    {
        void WriteXmlInFile(string nameFile);
        void WriteXmlInJson(string nameFile)
        string getResultTraceInJson();
        string getResultTraceInXml();
    }
}
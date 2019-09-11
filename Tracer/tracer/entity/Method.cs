using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Tracer.exception;

namespace Tracer.tracer.entity
{
    [Serializable]
    public class Method
    {
        public Method(string name,string methodClass)
        {
            this.methodClass = methodClass;
            stopwatch=new Stopwatch();
            this.name = name;
            methods = new List<Method>();
        }
        public Method()
        {
            this.methodClass = "";
            stopwatch=new Stopwatch();
            this.name = "";
            methods = new List<Method>();}
        public long time { get; set; }

        public void  balanceTime(long delete)
        {
            time = time - delete;
        }

        public void startTimer()
        {
            stopwatch.Start();
        }

        public string methodClass { get; set; }
        public string name { get; set; }
        private Stopwatch stopwatch { get;}
        public List<Method> methods { get; set; }

        public void addMethod(Method method)
        {
            methods.Add(method);
        }

        public Method getMethods()
        {
            Method method = new Method(this.name,this.methodClass);
            method.time = this.time;
            foreach (var innerMethod in this.methods)
            {
                method.addMethod(innerMethod.getMethods());
            }
            return method;
        }
        public void stopTimer()
        {
            stopwatch.Stop();
            time = stopwatch.ElapsedMilliseconds;
           
        }
    }
}
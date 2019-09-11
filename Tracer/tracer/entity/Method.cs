using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tracer.tracer.entity
{
    [Serializable]
    public class Method
    {
        public Method(string name,string methodClass)
        {
            this.MethodClass = methodClass;
            Stopwatch=new Stopwatch();
            this.Name = name;
            Methods = new List<Method>();
        }
        public Method()
        {
            this.MethodClass = "";
            Stopwatch=new Stopwatch();
            this.Name = "";
            Methods = new List<Method>();}
        public long Time { get; private set; }
        public void  BalanceTime(long delete)
        {
            Time = Time - delete;
        }

        public void StartTimer()
        {
            Stopwatch.Start();
        }

        public string MethodClass { get; set; }
        public string Name { get; set; }
        private Stopwatch Stopwatch { get;}
        public List<Method> Methods { get; set; }
        public void AddMethod(Method method)
        {
            Methods.Add(method);
        }

        public Method getMethods()
        {
            Method method = new Method(this.Name,this.MethodClass);
            method.Time = this.Time;
            foreach (var innerMethod in this.Methods)
            {
                method.AddMethod(innerMethod.getMethods());
            }
            return method;
        }
        public void stopTimer()
        {
            Stopwatch.Stop();
            Time = Stopwatch.ElapsedMilliseconds;
           
        }
    }
}
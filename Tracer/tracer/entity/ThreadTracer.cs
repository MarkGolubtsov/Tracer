using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Tracer.exception;

namespace Tracer.tracer.entity
{
    public class ThreadTracer {
        public ThreadTracer(int id) {
            this.id = id;
            methods = new Method[]{};
        }

       public int id { get; set; }
        
        private Method[] methods;
        
        public long time {
            get
            {
                long res = 0;
                foreach (var method in methods)
                {
                    res = res + method.time;
                }
                return res;
            }
        }

        public ThreadTracer clone()
        {
            ThreadTracer threadTracer = new ThreadTracer(this.id);
            Method[] clonedMethods= new Method[methods.Length];
            int i=0;
            foreach (var threadMethod in this.methods)
            {
                clonedMethods[i] = threadMethod.getMethods();
                i++;
            }
            threadTracer.AddMethods(clonedMethods);
            return threadTracer;
        }
         public void AddMethods(Method[] methods) {
             this.methods = methods;
         }
         
    }
}
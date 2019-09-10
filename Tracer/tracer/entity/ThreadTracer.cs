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

        private int id { get; set; }
        
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

         public void AddMethods(Method[] methods) {
             this.methods = methods;
         }
         
    }
}
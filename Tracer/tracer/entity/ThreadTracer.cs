﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using Tracer.exception;

namespace Tracer.tracer.entity
{ 
    [Serializable]
    public class ThreadTracer {
        public ThreadTracer(int id) {
            this.id = id;
            methods = new Method[]{};
        }
        public ThreadTracer() {
            this.id = new Random(1000).Next();
            methods = new Method[]{};
        }
        
       public int id { get; set; }
       
       private long time;
       public long Time
       {
           get
           {
               long res = 0;
               foreach (var method in methods) { 
                   res = res + method.time;
               }
               return res; 
           }
           set {
               time = value;
           }
       }
       
        public Method[] methods { get; set; }
        
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
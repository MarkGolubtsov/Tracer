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
            this.Id = id;
            Methods = new Method[]{};
        }
        public ThreadTracer() {
            this.Id = new Random(1000).Next();
            Methods = new Method[]{};
        }
        public int Id { get; set; }
        private long _time;
       public long Time
       {
           get
           {
               long res = 0;
               foreach (var method in Methods) { 
                   res = res + method.Time;
               }
               return res; 
           }
           set {
               _time = value;
           }
       }
       public Method[] Methods { get; set; }
       public ThreadTracer Clone()
        {
            ThreadTracer threadTracer = new ThreadTracer(this.Id);
            Method[] clonedMethods= new Method[Methods.Length];
            int i=0;
            foreach (var threadMethod in this.Methods)
            {
                clonedMethods[i] = threadMethod.getMethods();
                i++;
            }
            threadTracer.AddMethods(clonedMethods);
            return threadTracer;
        }
         public void AddMethods(Method[] methods) {
             this.Methods = methods;
         }
         
    }
}
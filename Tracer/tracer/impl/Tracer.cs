using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Tracer.exception;
using Tracer.tracer.entity;

namespace Tracer.tracer.impl
{
    public class Tracer : ITracer
    {
        private ConcurrentDictionary<int, Stack<Method>> runThreads;
        private ConcurrentDictionary<int, Stack<Method>>  stopThreadMethod;
        public Tracer() {
            runThreads = new ConcurrentDictionary<int, Stack<Method>>();
            stopThreadMethod = new ConcurrentDictionary<int, Stack<Method>>();
        }
        public void StartTrace()
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            Stack<Method> stack = runThreads.GetOrAdd(id, new Stack<Method>());
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            StackFrame stackFrame = stackFrames[1];
            
            string name = stackFrame.GetMethod().Name;
            string methodClass = getClassName(stackFrame);
            Method method = new Method(name, methodClass);
            stack.Push(method);
            method.startTimer();
        }

        public void StopTrace()
        {
            Stopwatch start = new Stopwatch();
            int id = Thread.CurrentThread.ManagedThreadId; 
            Stack<Method> stackRun = runThreads.GetOrAdd(id, new Stack<Method>());
            Method method = stackRun.Pop();
            method.stopTimer();
            method.balanceTime(start.ElapsedMilliseconds);
            Stack<Method> stackStop = stopThreadMethod.GetOrAdd(id, new Stack<Method>());
            Method parent;
            if (stackRun.TryPeek(out parent)) {
                parent.addMethod(method);
            }
            stackStop.Push(method);
        }

        public ResultTrace GetResult()
        {
            throw new System.NotImplementedException();
        }
        
        private  string getClassName(StackFrame stackFrame) {
            string fullClassName = stackFrame.GetFileName();
            return fullClassName.Substring(fullClassName.LastIndexOf("\\")+1,
                fullClassName.LastIndexOf(".", StringComparison.Ordinal) - fullClassName.LastIndexOf("\\", StringComparison.Ordinal)-1);
            
        }
        
    }
}
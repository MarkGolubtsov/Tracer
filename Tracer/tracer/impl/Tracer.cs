using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Tracer.exception;
using Tracer.tracer.entity;

namespace Tracer.tracer.impl
{
    public class Tracer : ITracer
    {
        private static ConcurrentQueue<Thread> threadsAll = new ConcurrentQueue<Thread>();
        
        private Thread mainThread;
        private ConcurrentDictionary<int, ConcurrentStack<Method>> runThreads;
        private ConcurrentDictionary<int, ConcurrentStack<Method>>  stopThreadMethod;
        public Tracer()
        {
            mainThread =Thread.CurrentThread;
            runThreads = new ConcurrentDictionary<int, ConcurrentStack<Method>>();
            stopThreadMethod = new ConcurrentDictionary<int,ConcurrentStack<Method>>();
        }

        private void addThreadInQueue(Thread thread)
        {
            int id = thread.ManagedThreadId;
            if (!runThreads.ContainsKey(id))
            {
                if (id!=mainThread.ManagedThreadId)
                {
                    threadsAll.Enqueue(thread);
                }
                
            }
        }
        public void StartTrace()
        {
            Thread currentThread=Thread.CurrentThread;
            int id = currentThread.ManagedThreadId;
            addThreadInQueue(currentThread);
            ConcurrentStack<Method> stack = runThreads.GetOrAdd(id, new ConcurrentStack<Method>());
            StackTrace stackTrace = new StackTrace(true);
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
            ConcurrentStack<Method> stackRun = runThreads.GetOrAdd(id, new ConcurrentStack<Method>());
            Method method;
            if (!stackRun.TryPop(out method))
            {
                throw new StopException("You need to start  before you stop!!");
            }
            method.stopTimer();
            method.balanceTime(start.ElapsedMilliseconds);
            ConcurrentStack<Method> stackStop = stopThreadMethod.GetOrAdd(id, new ConcurrentStack<Method>());
            Method parent;
            if (stackRun.TryPeek(out parent)) {
                parent.addMethod(method); 
            } else {
                stackStop.Push(method);
            }
            
        }
        

        public ResultTrace GetResult() {
            List<ThreadTracer> resultTracers = getCloneThreadTracers();
            ResultTrace resultTrace = new ResultTraceByMark();
            return resultTrace;
        }
        
        private List<ThreadTracer> getCloneThreadTracers()
        {
            joinAllThread();
            List<ThreadTracer> clone = new List<ThreadTracer>();
            ICollection<int> threadsMethod = stopThreadMethod.Keys;
            foreach (var id in threadsMethod)
            {
                ThreadTracer thread = new ThreadTracer(id);
                ConcurrentStack<Method> methods = stopThreadMethod.GetOrAdd(id, new ConcurrentStack<Method>());
                thread.AddMethods(methods.ToArray());
                clone.Add(thread.clone());
            }
            return clone;
        }

        private void joinAllThread()
        {
            foreach (var t in threadsAll)
            {
                t.Join();
            }
        }
        
        private  string getClassName(StackFrame stackFrame) {
            string fullClassName = stackFrame.GetFileName();
            return fullClassName.Substring(fullClassName.LastIndexOf("\\")+1,
                fullClassName.LastIndexOf(".", StringComparison.Ordinal) - fullClassName.LastIndexOf("\\", StringComparison.Ordinal)-1);
            
        }
        
        
    }
}
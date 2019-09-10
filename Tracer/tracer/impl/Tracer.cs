using System;
using System.Collections;
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
            stackRun.TryPop(out method);
            method.stopTimer();
            method.balanceTime(start.ElapsedMilliseconds);
            ConcurrentStack<Method> stackStop = stopThreadMethod.GetOrAdd(id, new ConcurrentStack<Method>());
            Method parent;
            if (stackRun.TryPeek(out parent)) {
                parent.addMethod(method);
            }
            else
            {
                stackStop.Push(method);
            }
            
        }
        

        public ResultTrace GetResult()
        {
            throw new System.NotImplementedException();
        }
        
        public List<ThreadTracer> test()
        {
            if (Thread.CurrentThread.ManagedThreadId != mainThread.ManagedThreadId)
                throw new Exception("You must get result in main thread");
            foreach (var t in threadsAll)
            {
                t.Join();
            }
            List<ThreadTracer> threads = new List<ThreadTracer>();
            Console.WriteLine("Key"+stopThreadMethod.Keys);
            ICollection<int> threadsMethod = stopThreadMethod.Keys;
            foreach (var id in threadsMethod)
            {
                Console.WriteLine("Id"+id);
                ThreadTracer thread = new ThreadTracer(id);
                ConcurrentStack<Method> methods = stopThreadMethod.GetOrAdd(id, new ConcurrentStack<Method>());
                thread.AddMethods(methods.ToArray());
                threads.Add(thread);
            }

            return threads;
        }
        
        private  string getClassName(StackFrame stackFrame) {
            string fullClassName = stackFrame.GetFileName();
            return fullClassName.Substring(fullClassName.LastIndexOf("\\")+1,
                fullClassName.LastIndexOf(".", StringComparison.Ordinal) - fullClassName.LastIndexOf("\\", StringComparison.Ordinal)-1);
            
        }
        
    }
}
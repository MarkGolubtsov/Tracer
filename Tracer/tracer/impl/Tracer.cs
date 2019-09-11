using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Tracer.exception;
using Tracer.tracer.entity;

namespace Tracer.tracer.impl
{
    public class Tracer : ITracer
    {
        private ConcurrentDictionary<int, ConcurrentStack<Method>> _runThreads;
        private ConcurrentDictionary<int, ConcurrentStack<Method>>  _stopThread;
        public Tracer()
        {
            _runThreads = new ConcurrentDictionary<int, ConcurrentStack<Method>>();
            _stopThread = new ConcurrentDictionary<int,ConcurrentStack<Method>>();
        }
        public void StartTrace()
        {
            Thread currentThread=Thread.CurrentThread;
            int id = currentThread.ManagedThreadId;
            ConcurrentStack<Method> stack = _runThreads.GetOrAdd(id, new ConcurrentStack<Method>());
            var stackTrace = new StackTrace(true);
            StackFrame[] stackFrames = stackTrace.GetFrames();
            var stackFrame = stackFrames[1];
            
            string name = stackFrame.GetMethod().Name;
            string methodClass = GetClassName(stackFrame);
            Method method = new Method(name, methodClass);
            stack.Push(method);
            method.StartTimer();
        }
        public void StopTrace()
        {
            var start = new Stopwatch();
            var id = Thread.CurrentThread.ManagedThreadId; 
            ConcurrentStack<Method> stackRun = _runThreads.GetOrAdd(id, new ConcurrentStack<Method>());
            if (!stackRun.TryPop(out var method))
            {
                throw new StopException("You need to start  before you stop!!");
            }
            method.stopTimer();
            method.BalanceTime(start.ElapsedMilliseconds);
            ConcurrentStack<Method> stackStop = _stopThread.GetOrAdd(id, new ConcurrentStack<Method>());
            if (stackRun.TryPeek(out var parent)) {
                parent.AddMethod(method); 
            } else {
                stackStop.Push(method);
            }
        }
        public IResultTrace GetResult() {
            List<ThreadTracer> resultTracers = GetCloneThreadTracers();
            ResultTrace resultTrace = new ResultTrace(resultTracers);
            return resultTrace;
        }
        private List<ThreadTracer> GetCloneThreadTracers()
        {
            List<ThreadTracer> clone = new List<ThreadTracer>();
            ICollection<int> threadsMethod = _stopThread.Keys;
            foreach (var id in threadsMethod)
            {
                ThreadTracer thread = new ThreadTracer(id);
                ConcurrentStack<Method> methods = _stopThread.GetOrAdd(id, new ConcurrentStack<Method>());
                thread.AddMethods(methods.ToArray());
                clone.Add(thread.Clone());
            }
            return clone;
        }
        private  string GetClassName(StackFrame stackFrame)
        {
            var declaringType = stackFrame.GetMethod().DeclaringType;
            if (declaringType != null) return declaringType.ToString();
            else
            {
                return "";
            }
        }
    }
}
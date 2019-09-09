using System;
using System.Diagnostics;
using System.Reflection;

namespace ConsoleApp4
{
    struct TraceResult
    {
        public long ms;
        public String methodName;
        public String className;
    }
    interface ITracer
    {
        void StartTrace();
        void StopTrace();
        TraceResult GetTraceResult();
    }


    class Tracer : ITracer
    {
        private Stopwatch time;

        public void StartTrace()
        {
            time = Stopwatch.StartNew();
        }
        public void StopTrace()
        {
            time.Stop();
        }
        public TraceResult GetTraceResult()
        {
            TraceResult traceResult;
            traceResult.ms = time.ElapsedMilliseconds;
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            StackFrame callingFrame = stackFrames[1];
            MethodBase method = callingFrame.GetMethod();


            traceResult.methodName = method.Name;
            traceResult.className = "";
            return traceResult;
        }
    }

    class Program
    {
        static void Method()
        {
            var tracer = new Tracer();
            tracer.StartTrace();
            Method1();
            tracer.StopTrace();
            Console.WriteLine(tracer.GetTraceResult().methodName);
            Console.WriteLine(tracer.GetTraceResult().ms);
        }

        static void Method1()
        {
            var tracer = new Tracer();
            tracer.StartTrace();
            Method2();
            tracer.StopTrace();
            Console.WriteLine(tracer.GetTraceResult().methodName);
            Console.WriteLine(tracer.GetTraceResult().ms);
        }
        static void Method2()
        {
            var tracer = new Tracer();
            tracer.StartTrace();
            for (int i = 0; i < 1000; i++)
            {
                Console.Write(1);
            }
            tracer.StopTrace();
            Console.WriteLine(tracer.GetTraceResult().methodName);
            Console.WriteLine(tracer.GetTraceResult().ms);
        }
        static void Main(string[] args)
        {
            var tracer = new Tracer();
            tracer.StartTrace();
            Method();
            tracer.StopTrace();
            Console.WriteLine(tracer.GetTraceResult().methodName);
            Console.WriteLine(tracer.GetTraceResult().ms);
        }
    }
}

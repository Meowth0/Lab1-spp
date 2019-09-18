using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using TracerDll;

namespace TracerTest
{
    [TestClass]
    public class TracerTests
    {
        public static void Method()
        {
            var tracer = new Tracer();
            tracer.StartTrace();
            Thread.Sleep(10);
            tracer.StopTrace();
            tracer.GetTraceResult();
        }

        public static void Method1()
        {
            var tracer = new Tracer();
            tracer.StartTrace();
            Thread.Sleep(10);
            tracer.StopTrace();
            tracer.GetTraceResult();
        }

        public static int CreateAndStartThread(Action target)
        {
            Thread thread = new Thread(new ThreadStart(target));
            thread.Start();
            return thread.ManagedThreadId;
        }


        [TestMethod]
        public void GetTraceResult_NoCallMethod_ThisFunctionName()
        {
            var tracer = new Tracer();
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            StackFrame callingFrame = stackFrames[0];
            string methodName = callingFrame.GetMethod().Name;

            tracer.StartTrace();
            Thread.Sleep(10);
            tracer.StopTrace();
            TraceResult traceResult = tracer.GetTraceResult();

            Assert.AreEqual(methodName, traceResult.methodName);
        }

        [TestMethod]
        public void GetTraceResult_1CallMethod_1NestedMethod()
        {
            var tracer = new Tracer();
            int methodCount = 1;

            tracer.StartTrace();
            Method();
            tracer.StopTrace();
            TraceResult traceResult = tracer.GetTraceResult();

            Assert.AreEqual(methodCount, traceResult.nestedMethods.Count);
        }

        [TestMethod]
        public void GetTraceResult_NewThreadMethod_0NestedMethods()
        {
            var tracer = new Tracer();
            int nestedMethodsCount = 0;

            tracer.StartTrace();
            CreateAndStartThread(Method);
            tracer.StopTrace();
            TraceResult traceResult = tracer.GetTraceResult();

            Assert.AreEqual(nestedMethodsCount, traceResult.nestedMethods.Count);
        }

        [TestMethod]
        public void GetTraceResult_1MethodCallnMethod_nNestedMethods()
        {
            var tracer = new Tracer();
            int n = 2;

            tracer.StartTrace();
            Method();
            Method1();
            tracer.StopTrace();
            TraceResult traceResult = tracer.GetTraceResult();

            Assert.AreEqual(n, traceResult.nestedMethods.Count);
        }

        [TestMethod]
        public void GetTraceResult_CreateNewThread_DifferentThreadId()
        {
            var tracer = new Tracer();
            int threadId = CreateAndStartThread(Method);

            tracer.StartTrace();
            Method();
            Method1();
            tracer.StopTrace();
            TraceResult traceResult = tracer.GetTraceResult();

            Assert.AreNotEqual(threadId, traceResult.threadId);

        }
    }
}

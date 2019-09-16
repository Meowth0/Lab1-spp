using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using static Main.ConsoleApp;

namespace TracerDll
{
    public class Tracer : ITracer
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
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            StackFrame callingFrame = stackFrames[1];
            MethodBase method = callingFrame.GetMethod();


            TraceResult traceResult = new TraceResult
            {
                ms = time.ElapsedMilliseconds,
                methodName = method.Name,
                className = method.DeclaringType.Name,
                threadId = Thread.CurrentThread.ManagedThreadId,
                nestedMethods = new List<TraceResult>()
            };
            string[] stackNames = new string[stackFrames.Length];
            for (int i = 0; i < stackFrames.Length; i++)
            {
                stackNames[i] = stackFrames[i].GetMethod().Name;
            }

            traceResult.nesting = Array.IndexOf(stackNames, "Main");
            if (traceResult.nesting == -1)
            {
                traceResult.nesting = Array.IndexOf(stackNames, "RunInternal");
                
            }


            try
            {
                TraceResult temp;
                for (var i = 0; i < buffer.Count; i++)
                {
                    if (buffer[i].TryGetValue(traceResult.threadId, out temp))
                    {
                        if (temp.nesting == traceResult.nesting + 1 && temp.threadId == traceResult.threadId)
                        {
                            traceResult.nestedMethods.Add(temp);
                            buffer.Remove(buffer[i]);
                            i--;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (buffer.Count > 0)
            {
                var dic = new Dictionary<int, TraceResult>();
                dic.Add(traceResult.threadId, traceResult);
                for (var i = 0; i < buffer.Count; i++)
                {
                    if ((buffer[i].Keys.ElementAt(0) != traceResult.threadId || buffer[i][buffer[i].Keys.ElementAt(0)].nesting != dic[dic.Keys.ElementAt(0)].nesting))
                    {
                        buffer.Add(dic);
                        break;
                    }
                }
            }
            else
            {
                var dic = new Dictionary<int, TraceResult>();
                dic.Add(traceResult.threadId, traceResult);
                buffer.Add(dic);
            }
            


            

            return traceResult;
        }
    }
}




/*
                       * 
 *                 *  *  *  *
 *                     *
 * 
 * */
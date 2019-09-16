using System;
using System.Collections.Generic;
using System.Reflection;

namespace TracerDll
{
    public class TraceResult
    {
        public long ms;
        public int nesting;
        public string methodName;
        public string className;
        public int threadId;
        public List<TraceResult> nestedMethods;
        


        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", methodName, className, ms, threadId);

        }
    }
    public interface ITracer
    {
        void StartTrace();
        void StopTrace();
        TraceResult GetTraceResult();
    }
}

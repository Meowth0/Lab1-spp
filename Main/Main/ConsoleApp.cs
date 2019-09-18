using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using TracerDll;


namespace Main
{

    public class ConsoleApp
    {
        public static List<Dictionary<int, TraceResult>> buffer = new List<Dictionary<int, TraceResult>>();
        public static List<TraceResult> results = new List<TraceResult>();
        static void Method()
        {
            var tracer = new Tracer();
            tracer.StartTrace();
            CreateAndStartThread(Method2);
            tracer.StopTrace();
            tracer.GetTraceResult(); 
        }

        static void Method1()
        {
            var tracer = new Tracer();
            tracer.StartTrace();
            Thread.Sleep(10);
            Method2();
            Method3();
            tracer.StopTrace();
            tracer.GetTraceResult();
            //new PrintResultToConsole().PrintResult(new TransformDataToJSON().GetFormatData(tracer.GetTraceResult()));   
        }
        static void Method2()
        {
            var tracer = new Tracer();
            tracer.StartTrace();
            Thread.Sleep(10);
            Method3();
            tracer.StopTrace();
            results.Add(tracer.GetTraceResult());
            //new PrintResultToConsole().PrintResult(new TransformDataToJSON().GetFormatData(tracer.GetTraceResult()));
        }

        static void Method3()
        {
            var tracer = new Tracer();
            tracer.StartTrace();
            Thread.Sleep(10);
            tracer.StopTrace();
            tracer.GetTraceResult();
            //new PrintResultToConsole().PrintResult(new TransformDataToJSON().GetFormatData(tracer.GetTraceResult()));
        }


        static void CreateAndStartThread(Action target)
        {
            Thread thread = new Thread(new ThreadStart(target));
            thread.Start();
            thread.Join();
        }

        public class Foo
        {
            public void InnerMethod()
            {
                var tracer = new Tracer();
                tracer.StartTrace();
                Method();
                Method();
                tracer.StopTrace();
                tracer.GetTraceResult();
            }
        }
        static void Main(string[] args)
        {
            var tracer = new Tracer();
            tracer.StartTrace();
            var foo = new Foo();

            //CreateAndStartThread(Method1);

            foo.InnerMethod();
            tracer.StopTrace();
            results.Add(tracer.GetTraceResult());
            new PrintResultToConsole().PrintResult(new TransformDataToJSON().GetFormatData(results));

            //var json = new TransformDataToJSON().GetFormatData(tracer.GetTraceResult());
            //var xml = new TransformDataToXML().GetFormatData(tracer.GetTraceResult());

            //var consoleWriter = new PrintResultToConsole();
            //var fileWriter = new PrintResultToFile();

            //consoleWriter.PrintResult(json);
            //fileWriter.PrintResult(xml);

        }
    }
}

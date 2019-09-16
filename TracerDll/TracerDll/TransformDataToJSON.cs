using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TracerDll
{
    public class TransformDataToJSON : ITransformData
    {
        public string GetFormatData(TraceResult traceResult)
        {
            /*
            Method2 Program 7082 1 Method1
            Method1 Program 7106 1 Method
            Method Program 7106 1 InnerMethod
            InnerMethod Foo 7107 1 Main
            Main Program 7107 1 null
            */
            var json = JsonConvert.SerializeObject(traceResult, Formatting.Indented);


            return json;
        }

    }
}

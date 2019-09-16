using System;
using System.Collections.Generic;
using System.Text;
using TracerDll;

namespace TracerDll
{
    public interface ITransformData
    {
        string GetFormatData(TraceResult traceResult);

    }
}

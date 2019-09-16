using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Main
{
    public class PrintResultToConsole : IPrintResult
    {
        public void PrintResult(string result)
        {
            Console.WriteLine(result);
        }

  
    }
}

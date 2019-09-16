using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Main
{
    public class PrintResultToFile : IPrintResult
    {
        public void PrintResult(string result)
        {
            string PATH = "result.txt";

            using (StreamWriter sw = new StreamWriter(PATH, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(result);
            }
        }
    }
}

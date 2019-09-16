using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TracerDll
{
    public class TransformDataToXML : ITransformData
    {
        public string GetFormatData(TraceResult traceResult)
        {
            var xml = new XmlSerializer(typeof(TraceResult));
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xml.Serialize(writer, traceResult);

                    try
                    {
                        XDocument doc = XDocument.Parse(sww.ToString());
                        return doc.ToString();
                    }
                    catch (Exception)
                    {
                        return sww.ToString();
                    }
                }
            }

        }
    }
}

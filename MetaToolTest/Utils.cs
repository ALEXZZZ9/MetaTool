using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MetaToolTest
{
    internal static class Utils
    {
        public static string Base64Decode(string text)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(text));
        }

        public static T XMLBase64Deserialize<T>(string xml)
        {
            return XMLDeserialize<T>(Base64Decode(xml));
        }
        public static T XMLDeserialize<T>(string xml)
        {
            TextReader reader = new StringReader(xml);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T obj = (T)xmlSerializer.Deserialize(reader);
            return obj;
        }
    }
}

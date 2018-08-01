using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AX9.MetaTool
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    public static class Utils
    {
        public static T ToType<T>(this Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream);

            return reader.ToType<T>();
        }

        public static T ToType<T>(this BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(Marshal.SizeOf(typeof(T)));

            return bytes.ToType<T>();
        }

        public static T ToType<T>(this byte[] bytes)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T structure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return structure;
        }

        public static int RoundUp(int target, uint align)
        {
            return (int)(target + (align - 1u) & (long)~(ulong)(align - 1u));
        }

        public static string XMLSerialize<T>(this T value)
        {
            if (value == null) return string.Empty;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            Utf8StringWriter stringWriter = new Utf8StringWriter();
            XmlSerializerNamespaces emptyNs = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            using (XmlWriter writer = XmlWriter.Create(stringWriter, new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8
            }))
            {
                xmlSerializer.Serialize(writer, value, emptyNs);
                return stringWriter.ToString();
            }
        }

        public static T XMLDeserialize<T>(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return default(T);

            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8, true))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));


                T obj = (T)xmlSerializer.Deserialize(reader);
                reader.Close();
                return obj;
            }
        }

        public static ulong ConvertHexString(string value, string tagName)
        {
            if (value.IndexOf("0x", StringComparison.Ordinal) != 0) throw new ArgumentException($"Invalid hexadecimal number in {tagName}");

            try
            {
                return ulong.Parse(value.Replace("0x", ""), NumberStyles.HexNumber);
            }
            catch
            {
                throw new ArgumentException($"Invalid hexadecimal number in {tagName}");
            }
        }

        public static string ConvertToHexString(ulong value)
        {
            return $"0x{value:X}";
        }

        public static bool ConvertBoolString(string value, string tagName)
        {
            try
            {
                return bool.Parse(value);
            }
            catch
            {
                throw new ArgumentException($"Invalid bool in {tagName}");
            }
        }

        public static ulong ConvertDecimalString(string value, string tagName)
        {
            try
            {
                return ulong.Parse(value);
            }
            catch
            {
                throw new ArgumentException($"Invalid decimal number in {tagName}");
            }
        }
    }
}

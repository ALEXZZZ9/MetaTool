using System;
using System.Xml.Serialization;

namespace AX9.MetaTool.Models
{
    [XmlRoot("KernelVersion")]
    public class KcKernelVersionModel
    {
        [XmlIgnore]
        public ushort MajorVersionVlaue
        {
            get => majorVersionVlaue;
            set
            {
                if (value >= 8192) throw new ArgumentException("KernelVersion/MajorVersion is invalid");

                majorVersion = value.ToString("D");
                majorVersionVlaue = value;
            }
        }

        [XmlElement("MajorVersion", IsNullable = false)]
        public string MajorVersion
        {
            get => majorVersion;
            set
            {
                if (value == null) return;

                majorVersionVlaue = checked((ushort)Utils.ConvertDecimalString(value, "KernelVersion/MajorVersion"));
                majorVersion = value;
            }
        }

        [XmlIgnore]
        public byte MinorVersionVlaue
        {
            get => minorVersionVlaue;
            set
            {
                if (value >= 16) throw new ArgumentException("KernelVersion/MinorVersion is invalid");

                minorVersion = value.ToString("D");
                minorVersionVlaue = value;
            }
        }

        [XmlElement("MinorVersion", IsNullable = false)]
        public string MinorVersion
        {
            get => minorVersion;
            set
            {
                if (value == null) return;
                
                minorVersionVlaue = checked((byte)Utils.ConvertDecimalString(value, "KernelVersion/MinorVersion"));
                minorVersion = value;
            }
        }


        public const int MajorVersionFieldSize = 13;
        public const int MinorVersionFieldSize = 4;

        private ushort majorVersionVlaue;
        private string majorVersion;
        private byte minorVersionVlaue;
        private string minorVersion;

        private readonly KcFlagModel capability = new KcFlagModel { EntryNumber = 14u };


        public uint CalcFlag()
        {
            capability.FieldValue = (uint)(MajorVersionVlaue << 4 | MinorVersionVlaue);
            return capability.Flag;
        }
    }
}

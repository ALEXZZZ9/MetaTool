using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AX9.MetaTool.Models
{
    [XmlRoot("EnableSystemCalls")]
    public class KcEnableSystemCallsModel
    {
        public KcEnableSystemCallsModel() { }
        public KcEnableSystemCallsModel(string name, byte systemCallId)
        {
            Name = name;
            SystemCallIdValue = systemCallId;
        }


        [XmlElement("Name", IsNullable = false)]
        public string Name { get; set; }

        [XmlIgnore]
        public byte SystemCallIdValue
        {
            get => systemCallIdValue;
            set
            {
                systemCallIdValue = value;
                systemCallId = value.ToString("D");
            }
        }

        [XmlElement("SystemCallId", IsNullable = false)]
        public string SystemCallId
        {
            get => systemCallId;
            set
            {
                if (value == null) return;

                systemCallIdValue = checked((byte)Utils.ConvertDecimalString(value, "EnableSystemCalls/SystemCallId"));
                systemCallId = value;
            }
        }

        [XmlIgnore]
        public int Index { get; set; } = 0;


        public const byte MaxSystemCallNum = 128;
        public const int FieldSize = 24;

        private byte systemCallIdValue;
        private string systemCallId;

        private readonly KcFlagModel capability = new KcFlagModel { EntryNumber = 4u };
        private readonly List<byte> systemCallIds = new List<byte>();


        public byte[] GetSystemCallIds() => systemCallIds.ToArray();
        public int GetNumIds() => systemCallIds.Count;


        public void AddSystemCallId(byte id)
        {
            if (id >= 128) throw new ArgumentException($"Specified an invalid value, {id}, for the SystemCall index.");

            int index = (id / 24);

            if (index != Index) throw new IndexOutOfRangeException($"An unexpected index is set. Expectation={Index}, SystemCall={id}, Index={index}");

            systemCallIds.Add(id);
        }

        public uint CalcFlag()
        {
            uint num = 0u;

            foreach (byte b in systemCallIds)
            {
                num |= 1u << (b % FieldSize);
            }
            capability.FieldValue = (uint)(Index << FieldSize | (int)num);

            return capability.Flag;
        }

        public bool CheckSuccessToRead()
        {
            if (Name == null) throw new ArgumentException("Not Found EnableSystemCalls/Name");
            if (SystemCallId == null) throw new ArgumentException("Not Found EnableSystemCalls/SystemCallId");

            return true;
        }
    }
}

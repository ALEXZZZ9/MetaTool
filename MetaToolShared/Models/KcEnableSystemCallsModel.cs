using System;
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


        private byte systemCallIdValue;
        private string systemCallId;


        public bool CheckSuccessToRead()
        {
            if (Name == null) throw new ArgumentException("Not Found EnableSystemCalls/Name");
            if (SystemCallId == null) throw new ArgumentException("Not Found EnableSystemCalls/SystemCallId");

            return true;
        }
    }
}

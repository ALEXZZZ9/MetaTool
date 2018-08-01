using System.Xml.Serialization;

namespace AX9.MetaTool.Models
{
    [XmlRoot("IoMemoryMap")]
    public class KcIoMemoryMapModel
    {
        [XmlIgnore]
        public ulong BeginAddressValue
        {
            get => beginAddressValue;
            set
            {
                beginAddress = Utils.ConvertToHexString(value);
                beginAddressValue = value;
            }
        }

        [XmlElement("BeginAddress", IsNullable = false)]
        public string BeginAddress
        {
            get => beginAddress;
            set
            {
                if (value == null) return;

                beginAddressValue = Utils.ConvertHexString(value, "MemoryMap/BeginAddress");
                beginAddress = value;
            }
        }


        public const int AddrFieldSize = 24;
        public const int PageShift = 12;

        private ulong beginAddressValue;
        private string beginAddress;

        private readonly KcFlagModel capability = new KcFlagModel { EntryNumber = 7u };


        public uint CalcFlag()
        {
            capability.FieldValue = (uint)(BeginAddressValue >> PageShift);
            capability.FieldValue &= 0xFFFFFF;
            return capability.Flag;
        }
    }
}

using System;
using System.Xml.Serialization;

namespace AX9.MetaTool.Models
{
    [XmlRoot("ThreadInfo")]
    public class KcThreadInfoModel
    {
        public KcThreadInfoModel()
        {
            HighestPriorityValue = 59;
            LowestPriorityValue = 28;
            MinCoreNumberValue = 0;
            MaxCoreNumberValue = 2;
        }


        [XmlIgnore]
        public byte LowestPriorityValue
        {
            get => lowestPriorityValue;
            set
            {
                if (value >= 64) throw new ArgumentException("ThreadInfo/LowestPriority is invalid");

                lowestPriority = value.ToString("D");
                lowestPriorityValue = value;
            }
        }

        //[XmlElement("LowestPriority", IsNullable = false)]
        [XmlElement("HighestPriority", IsNullable = false)]
        public string LowestPriority
        {
            get => lowestPriority;
            set
            {
                if (value == null) return;

                lowestPriorityValue = checked((byte)Utils.ConvertDecimalString(value, "ThreadInfo/LowestPriority"));
                lowestPriority = value;
            }
        }

        [XmlIgnore]
        public byte HighestPriorityValue
        {
            get => highestPriorityValue;
            set
            {
                if (value >= 64) throw new ArgumentException("ThreadInfo/HighestPriority is invalid");

                highestPriority = value.ToString("D");
                highestPriorityValue = value;
            }
        }

        //[XmlElement("HighestPriority", IsNullable = false)]
        [XmlElement("LowestPriority", IsNullable = false)]
        public string HighestPriority
        {
            get => highestPriority;
            set
            {
                if (value == null) return;

                highestPriorityValue = checked((byte)Utils.ConvertDecimalString(value, "ThreadInfo/HighestPriority"));
                highestPriority = value;
            }
        }

        [XmlIgnore]
        public byte MinCoreNumberValue
        {
            get => minCoreNumberValue;
            set
            {
                minCoreNumber = value.ToString("D");
                minCoreNumberValue = value;
            }
        }

        [XmlElement("MinCoreNumber", IsNullable = false)]
        public string MinCoreNumber
        {
            get => minCoreNumber;
            set
            {
                if (value == null) return;
                
                minCoreNumberValue = checked((byte)Utils.ConvertDecimalString(value, "ThreadInfo/MinCoreNumber"));
                minCoreNumber = value;
            }
        }

        [XmlIgnore]
        public byte MaxCoreNumberValue
        {
            get => maxCoreNumberValue;
            set
            {
                maxCoreNumber = value.ToString("D");
                maxCoreNumberValue = value;
            }
        }

        [XmlElement("MaxCoreNumber", IsNullable = false)]
        public string MaxCoreNumber
        {
            get => maxCoreNumber;
            set
            {
                if (value == null) return;

                maxCoreNumberValue = checked((byte)Utils.ConvertDecimalString(value, "ThreadInfo/MaxCoreNumber"));
                maxCoreNumber = value;
            }
        }


        public const byte LowestPriorityFieldSize = 6;
        public const byte HighestPriorityFieldSize = 6;
        public const byte MinCoreNumberFieldSize = 8;
        public const byte MaxCoreNumberFieldSize = 8;

        private byte lowestPriorityValue;
        private string lowestPriority;
        private byte highestPriorityValue;
        private string highestPriority;
        private byte minCoreNumberValue;
        private string minCoreNumber;
        private byte maxCoreNumberValue;
        private string maxCoreNumber;

        private readonly KcFlagModel сapability = new KcFlagModel { EntryNumber = 3u };


        public uint CalcFlag()
        {
            сapability.FieldValue |= LowestPriorityValue;
            сapability.FieldValue |= (uint)HighestPriorityValue << LowestPriorityFieldSize;
            сapability.FieldValue |= (uint)MinCoreNumberValue << (LowestPriorityFieldSize + HighestPriorityFieldSize);
            сapability.FieldValue |= (uint)MaxCoreNumberValue << (LowestPriorityFieldSize + HighestPriorityFieldSize + MinCoreNumberFieldSize);
            return сapability.Flag;
        }

        public bool CheckSuccessToRead()
        {
            if (LowestPriority == null) throw new ArgumentException("Not Found ThreadInfo/LowestPriority");
            if (HighestPriority == null) throw new ArgumentException("Not Found ThreadInfo/HighestPriority");
            if (MinCoreNumber == null) throw new ArgumentException("Not Found ThreadInfo/MinCoreNumber");
            if (MaxCoreNumber == null) throw new ArgumentException("Not Found ThreadInfo/MaxCoreNumber");

            return true;
        }
    }
}

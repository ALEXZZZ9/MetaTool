using System.Xml.Serialization;
using AX9.MetaTool.Enums;

namespace AX9.MetaTool.Models
{
    [XmlRoot("MiscParams")]
    public class KcMiscParamsModel
    {
        [XmlIgnore]
        public ProgramTypeEnum ProgramTypeValue
        {
            get => programTypeValue;
            set
            {
                programType = value.ToString("D");
                programTypeValue = value;
            }
        }

        [XmlElement("ProgramType", IsNullable = false)]
        public string ProgramType
        {
            get => programType;
            set
            {
                if (value == null) return;
                
                programTypeValue = (ProgramTypeEnum)Utils.ConvertDecimalString(value, "MiscParams/ProgramType");
                programType = value;
            }
        }
    

        private ProgramTypeEnum programTypeValue;
        private string programType;

        private readonly KernelCapabilityFlag capability = new KernelCapabilityFlag { EntryNumber = 13u };


        public uint CalcFlag()
        {
            capability.FieldValue = (byte)ProgramTypeValue;
            return capability.Flag;
        }

        public bool CheckSuccessToRead()
        {
            return ProgramType != null;
        }
    }
}

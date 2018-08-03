using System;
using System.Xml.Serialization;
using AX9.MetaTool.Enums;

namespace AX9.MetaTool.Models
{
    [XmlRoot("MiscParams")]
    public class KcMiscParamsModel : ICloneable
    {
        public KcMiscParamsModel()
        {
            ProgramTypeValue = ProgramTypeEnum.Application;
        }


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

        private readonly KcFlagModel capability = new KcFlagModel { EntryNumber = 13u };


        public uint CalcFlag()
        {
            capability.FieldValue = (byte)ProgramTypeValue;
            return capability.Flag;
        }

        public bool CheckSuccessToRead()
        {
            return ProgramType != null;
        }

        public object Clone()
        {
            return new KcMiscParamsModel
            {
                ProgramType = ProgramType
            };
        }
    }
}

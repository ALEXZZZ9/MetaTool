using System;
using System.Security.Cryptography;
using System.Xml.Serialization;
using AX9.MetaTool.Enums;

namespace AX9.MetaTool.Models
{
    [XmlRoot("Desc")]
    public class DescModel
    {
        [XmlElement("MemoryRegion")]
        public byte MemoryRegion { get; set; }

        [XmlIgnore]
        public ulong ProgramIdMinValue
        {
            get => programIdMinValue;
            set
            {
                programIdMin = Utils.ConvertToHexString(value);
                programIdMinValue = value;
            }
        }

        [XmlElement("ProgramIdMin")]
        public string ProgramIdMin
        {
            get => programIdMin;
            set
            {
                if (value == null) return;

                programIdMinValue = Utils.ConvertHexString(value, "Desc/ProgramIdMin");
                programIdMin = value;
            }
        }

        [XmlIgnore]
        public ulong ProgramIdMaxValue
        {
            get => programIdMaxValue;
            set
            {
                programIdMax = Utils.ConvertToHexString(value);
                programIdMaxValue = value;
            }
        }

        [XmlElement("ProgramIdMax")]
        public string ProgramIdMax
        {
            get => programIdMax;
            set
            {
                if (value == null) return;
                
                programIdMaxValue = Utils.ConvertHexString(value, "Desc/ProgramIdMax");
                programIdMax = value;
            }
        }

        [XmlElement("FsAccessControlDescriptor")]
        public FaDescriptorModel FsAccessControlDescriptor { get; set; }

        [XmlElement("SrvAccessControlDescriptor")]
        public SaDescriptorModel SrvAccessControlDescriptor { get; set; }

        [XmlElement("KernelCapabilityDescriptor")]
        public KcDescriptorModel KernelCapabilityDescriptor { get; set; }

        [XmlElement("Default", IsNullable = false)]
        public DefaultModel Default { get; set; }

        [XmlElement("RSAKeyValue")]
        public RSAParameters RSAKeyValue { get; set; }

        [XmlElement("Acid", IsNullable = false)]
        public string Acid { get; set; }


        private ulong programIdMinValue;
        private string programIdMin;
        private ulong programIdMaxValue;
        private string programIdMax;


        public bool ShouldSerializeRSAKeyValue()
        {
            return RSAKeyValue.Modulus != null;
        }

        public static DescModel FromNpdm(Npdm npdm)
        {
            DescModel desc = new DescModel
            {
                ProgramIdMin = $"0x{$"{npdm.ACIDData.Header.ProgramIdMin:X}".PadLeft(16, '0')}",
                ProgramIdMax = $"0x{$"{npdm.ACIDData.Header.ProgramIdMax:X}".PadLeft(16, '0')}",
                FsAccessControlDescriptor = npdm.ACIData.FaDescriptor,
                SrvAccessControlDescriptor = npdm.ACIData.SaDescriptor,
                KernelCapabilityDescriptor = npdm.ACIData.KcDescriptor,
                Default = new DefaultModel
                {
                    Is64BitInstructionValue = (npdm.Header.Flags & 1) == 1,
                    ProcessAddressSpace = Enum.GetValues(typeof(ProcessAddressSpacesEnum)).GetValue(((npdm.Header.Flags & 14) >> 1)).ToString(),
                    MainThreadPriorityValue = npdm.Header.MainThreadPriority,
                    MainThreadCoreNumberValue = npdm.Header.MainThreadCoreNumber,
                    MainThreadStackSizeValue = npdm.Header.MainThreadStackSize,
                    FsAccessControlData = new FaDataModel(npdm.ACIData.FaDescriptor),
                    SrvAccessControlData = new SaDataModel(npdm.ACIData.SaDescriptor),
                    KernelCapabilityData = new KcDataModel(npdm.ACIData.KcDescriptor)
                },
                RSAKeyValue = new RSAParameters(),
                Acid = Convert.ToBase64String(npdm.ACIDData.ACID)
            };

            return desc;
        }
    }
}

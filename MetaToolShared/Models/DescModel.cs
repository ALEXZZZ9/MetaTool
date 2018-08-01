using System;
using System.Security.Cryptography;
using System.Xml.Serialization;
using AX9.MetaTool.Enums;

namespace AX9.MetaTool.Models
{
    [XmlRoot("Desc")]
    public class DescModel
    {
        public DescModel()
        {
            MemoryRegion = 0;
            ProgramIdMinValue = 0x0100000000010000;
            ProgramIdMaxValue = 0x01FFFFFFFFFFFFFF;
        }


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
        public FaDescriptorModel FsAccessControlDescriptor { get; set; } = new FaDescriptorModel();

        [XmlElement("SrvAccessControlDescriptor")]
        public SaDescriptorModel SrvAccessControlDescriptor { get; set; } = new SaDescriptorModel();

        [XmlElement("KernelCapabilityDescriptor")]
        public KcDescriptorModel KernelCapabilityDescriptor { get; set; } = new KcDescriptorModel();

        [XmlElement("Default", IsNullable = false)]
        public DefaultModel Default { get; set; } = new DefaultModel();

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

        public static DescModel FromNpdm(NpdmModel npdm)
        {
            DescModel desc = new DescModel
            {
                ProgramIdMin = $"0x{$"{npdm.AcidModel.Header.ProgramIdMin:X}".PadLeft(16, '0')}",
                ProgramIdMax = $"0x{$"{npdm.AcidModel.Header.ProgramIdMax:X}".PadLeft(16, '0')}",
                FsAccessControlDescriptor = npdm.AciModel.FaDescriptor,
                SrvAccessControlDescriptor = npdm.AciModel.SaDescriptor,
                KernelCapabilityDescriptor = npdm.AciModel.KcDescriptor,
                Default = new DefaultModel
                {
                    Is64BitInstructionValue = (npdm.Header.Flags & 1) == 1,
                    ProcessAddressSpace = Enum.GetValues(typeof(ProcessAddressSpacesEnum)).GetValue(((npdm.Header.Flags & 14) >> 1)).ToString(),
                    MainThreadPriorityValue = npdm.Header.MainThreadPriority,
                    MainThreadCoreNumberValue = npdm.Header.MainThreadCoreNumber,
                    MainThreadStackSizeValue = npdm.Header.MainThreadStackSize,
                    FsAccessControlData = new FaDataModel(npdm.AciModel.FaDescriptor),
                    SrvAccessControlData = new SaDataModel(npdm.AciModel.SaDescriptor),
                    KernelCapabilityData = new KcDataModel(npdm.AciModel.KcDescriptor)
                },
                RSAKeyValue = new RSAParameters(),
                Acid = Convert.ToBase64String(npdm.AcidModel.ACID)
            };

            return desc;
        }

        public static DescModel FromFile(string filePath)
        {
            try
            {
                DescModel desc = Utils.XMLDeserialize<DescModel>(filePath);

                return desc;
            }
            catch (Exception ex)
            {
                throw new Exception($"File {filePath} is corrupted or is not a Desc file");
            }
        }
    }
}

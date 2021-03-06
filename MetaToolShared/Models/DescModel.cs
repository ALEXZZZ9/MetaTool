﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AX9.MetaTool.Enums;
using AX9.MetaTool.Structs;

namespace AX9.MetaTool.Models
{
    [XmlRoot("Desc")]
    public class DescModel : ICloneable
    {
        public DescModel() { }
        public DescModel(bool initDefault)
        {
            if (initDefault)
            {
                MemoryRegion = 0;
                ProgramIdMin = "0x0100000000010000";
                ProgramIdMax = "0x01FFFFFFFFFFFFFF";
                Default = new DefaultModel(true);
            }
        }


        [XmlIgnore]
        public bool IsRetail { get; set; } = true;

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

        [XmlElement("Signature")]
        public string Signature { get; set; }

        [XmlIgnore]
        public AciHeader AciHeader;
        [XmlIgnore]
        public AcidHeader AcidHeader;

        private ulong programIdMinValue;
        private string programIdMin;
        private ulong programIdMaxValue;
        private string programIdMax;


        public bool ShouldSerializeRSAKeyValue()
        {
            return RSAKeyValue.Modulus != null;
        }

        public static async Task<DescModel> FromNpdm(string filePatch)
        {
            using (Stream stream = File.OpenRead(filePatch))
            {
                NpdmHeader npdmHeader = stream.ToType<NpdmHeader>();

                byte[] acidBytes = new byte[npdmHeader.AcidSize];
                stream.Seek(npdmHeader.AcidOffset, SeekOrigin.Begin);
                await stream.ReadAsync(acidBytes, 0, (int)npdmHeader.AcidSize);

                byte[] aciBytes = new byte[npdmHeader.AciSize];
                stream.Seek(npdmHeader.AciOffset, SeekOrigin.Begin);
                await stream.ReadAsync(aciBytes, 0, (int)npdmHeader.AciSize);

                stream.Close();

                AciHeader aciHeader = aciBytes.ToType<AciHeader>();
                AcidHeader acidHeader = acidBytes.ToType<AcidHeader>();
                DescModel desc = new DescModel
                {
                    IsRetail = acidHeader.Flags[0] == 1,
                    ProgramIdMin = $"0x{$"{acidHeader.ProgramIdMin:X}".PadLeft(16, '0')}",
                    ProgramIdMax = $"0x{$"{acidHeader.ProgramIdMax:X}".PadLeft(16, '0')}",
                    Default = new DefaultModel
                    {
                        Is64BitInstructionValue = (npdmHeader.Flags & 1) == 1,
                        ProcessAddressSpace = Enum.GetValues(typeof(ProcessAddressSpacesEnum)).GetValue(((npdmHeader.Flags & 14) >> 1)).ToString(),
                        MainThreadPriorityValue = npdmHeader.MainThreadPriority,
                        MainThreadCoreNumberValue = npdmHeader.MainThreadCoreNumber,
                        MainThreadStackSizeValue = npdmHeader.MainThreadStackSize
                    },
                    RSAKeyValue = new RSAParameters { Modulus = acidHeader.Modulus },
                    Acid = Convert.ToBase64String(acidBytes),
                    Signature = Convert.ToBase64String(acidHeader.Signature),
                    AciHeader = aciHeader,
                    AcidHeader = acidHeader
                };

                if (aciHeader.FacSize > 0)
                {
                    byte[] facBytes = new byte[aciHeader.FacSize];
                    Buffer.BlockCopy(aciBytes, (int)aciHeader.FacOffset, facBytes, 0, (int)aciHeader.FacSize);

                    desc.FsAccessControlDescriptor = FaDescriptorModel.FromNpdmBytes(facBytes);
                    desc.Default.FsAccessControlData = new FaDataModel(desc.FsAccessControlDescriptor);
                }
                if (aciHeader.SacSize > 0)
                {
                    byte[] sacBytes = new byte[aciHeader.SacSize];
                    Buffer.BlockCopy(aciBytes, (int)aciHeader.SacOffset, sacBytes, 0, (int)aciHeader.SacSize);

                    desc.SrvAccessControlDescriptor = SaDescriptorModel.FromNpdmBytes(sacBytes);
                    desc.Default.SrvAccessControlData = new SaDataModel(desc.SrvAccessControlDescriptor);
                }
                if (aciHeader.KcSize > 0)
                {
                    byte[] kcBytes = new byte[aciHeader.KcSize];
                    Buffer.BlockCopy(aciBytes, (int)aciHeader.KcOffset, kcBytes, 0, (int)aciHeader.KcSize);

                    desc.KernelCapabilityDescriptor = KcDescriptorModel.FromNpdmBytes(kcBytes);
                    desc.Default.KernelCapabilityData = new KcDataModel(desc.KernelCapabilityDescriptor);
                }


                return desc;
            }
        }

        public static DescModel FromXml(string filePath)
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

        public object Clone()
        {
            return new DescModel
            {
                IsRetail = IsRetail,
                MemoryRegion = MemoryRegion,
                ProgramIdMin = ProgramIdMin,
                ProgramIdMax = ProgramIdMax,
                FsAccessControlDescriptor = (FaDescriptorModel)FsAccessControlDescriptor?.Clone(),
                SrvAccessControlDescriptor = (SaDescriptorModel)SrvAccessControlDescriptor?.Clone(),
                KernelCapabilityDescriptor = (KcDescriptorModel)KernelCapabilityDescriptor?.Clone(),
                Default = (DefaultModel)Default?.Clone(),
                RSAKeyValue = RSAKeyValue,
                Acid = Acid,
                AciHeader = AciHeader,
                AcidHeader = AcidHeader
            };
        }
    }
}

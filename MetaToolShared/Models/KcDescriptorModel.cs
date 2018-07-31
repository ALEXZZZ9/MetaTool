using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using AX9.MetaTool.Enums;

namespace AX9.MetaTool.Models
{
    [XmlRoot("KernelCapabilityDescriptor")]
    public class KcDescriptorModel
    {
        [XmlElement("ThreadInfo")]
        public KcThreadInfoModel ThreadInfo { get; set; }

        [XmlElement("EnableSystemCalls")]
        public List<KcEnableSystemCallsModel> EnableSystemCalls { get; set; }

        [XmlElement("MemoryMap")]
        public List<KcMemoryMapModel> MemoryMap { get; set; }

        //[XmlElement("IoMemoryMap")]
        //public List<KcIoMemoryMapModel> IoMemoryMap { get; set; }

        [XmlElement("EnableInterrupts")]
        public List<string> EnableInterrupts { get; set; }

        [XmlElement("MiscParams")]
        public KcMiscParamsModel MiscParams { get; set; }

        [XmlElement("KernelVersion")]
        public KcKernelVersionModel KernelVersion { get; set; }

        [XmlIgnore]
        public KcHandleTableSizeModel HandleTableSizeValue { get; set; } = new KcHandleTableSizeModel();

        [XmlElement("HandleTableSize")]
        public string HandleTableSize
        {
            get => HandleTableSizeValue.HandleTableSize.ToString("D");
            set
            {
                if (value == null) return;

                HandleTableSizeValue.HandleTableSize = checked((ushort)Utils.ConvertDecimalString(value, "KernelCapabilityDescriptor/HandleTableSize"));
            }
        }

        [XmlElement("MiscFlags")]
        public KcMiscFlags MiscFlags { get; set; }

        [XmlIgnore]
        public List<byte> SystemCallList { get; set; } = new List<byte>();


        // Thanks SciresM for https://github.com/SciresM/hactool/blob/bdbd4f639ec7c1573a74c6c4cfa7b47801f23217/npdm.c#L269
        public static KcDescriptorModel FromNpdmBytes(byte[] bytes)
        {
            KcDescriptorModel kcDescriptor = new KcDescriptorModel();
            int size = 4;
            int descriptorsCount = bytes.Length / size;
            uint[] descriptors = new uint[descriptorsCount];

            for (int i = 0; i < descriptorsCount; i++)
            {
                byte[] descBytes = new byte[size];

                Buffer.BlockCopy(bytes, i * size, descBytes, 0, size);

                descriptors[i] = BitConverter.ToUInt32(descBytes, 0);
            }

            for (int i = 0; i < descriptorsCount; i++)
            {
                uint desc = descriptors[i];

                if (desc == 0xFFFFFFFF) continue;

                int lowBits = 0;
                while ((desc & 1) != 0)
                {
                    desc >>= 1;
                    lowBits += 1;
                }
                desc >>= 1;

                switch (lowBits)
                {
                    case 3: // Kernel flags
                        kcDescriptor.ThreadInfo = new KcThreadInfoModel
                        {
                            HighestPriorityValue = (byte)(desc & 0x3F),
                            LowestPriorityValue = (byte)((desc >>= 6) & 0x3F),
                            MinCoreNumberValue = (byte)(desc >>= 6 & 0xFF),
                            MaxCoreNumberValue = (byte)(desc >>= 8 & 0xFF)
                        };
                        break;
                    case 4: // Syscall mask
                        if (kcDescriptor.EnableSystemCalls == null) kcDescriptor.EnableSystemCalls = new List<KcEnableSystemCallsModel>();
                        if (kcDescriptor.SystemCallList == null) kcDescriptor.SystemCallList = new List<byte>();

                        string[] systemCalls = Enum.GetNames(typeof(SystemCallsEnum));
                        uint syscallBase = (desc >> 24) * 0x18;

                        for (uint sc = 0; sc < 0x18 && syscallBase + sc < 0x80; sc++)
                        {
                            bool enable = (desc & 1) == 1;
                            string systemCallName = systemCalls[syscallBase + sc];

                            if (enable)
                            {
                                KcEnableSystemCallsModel sCI = new KcEnableSystemCallsModel(systemCallName, (byte)(syscallBase + sc));

                                if (!kcDescriptor.EnableSystemCalls.Contains(sCI))
                                {
                                    kcDescriptor.EnableSystemCalls.Add(sCI);
                                    kcDescriptor.SystemCallList.Add(sCI.SystemCallIdValue);
                                }
                            }
                         
                            desc >>= 1;
                        }
                        break;
                    case 6: // Map IO/Normal
                        if (kcDescriptor.MemoryMap == null) kcDescriptor.MemoryMap = new List<KcMemoryMapModel>();

                        KcMemoryMapModel mMap = new KcMemoryMapModel
                        {
                            BeginAddressValue = (desc & 0xFFFFFF) << KcMemoryMapModel.PageShift,
                            PermissionValue = ((desc >> KcMemoryMapModel.AddrFieldSize) == 0) ? MemoryMapPermissionEnum.RW : MemoryMapPermissionEnum.RO
                        };

                        if (i == descriptorsCount - 1) throw new Exception("Invalid Kernel Access Control Descriptors");
                        
                        desc = descriptors[++i];

                        if ((desc & 0x7F) != 0x3F) throw new Exception("Invalid Kernel Access Control Descriptors");
                        
                        desc >>= 7;

                        mMap.SizeValue = (desc & 0xFFFFF) << KcMemoryMapModel.PageShift;
                        mMap.TypeValue = ((desc >> KcMemoryMapModel.SizeFieldSize) == 0) ? MemoryMapTypeEnum.IO : MemoryMapTypeEnum.STATIC;

                        kcDescriptor.MemoryMap.Add(mMap);
                        break;
                    case 7: // Map Normal Page
                        //KcIoMemoryMapModel ioMMap = new KcIoMemoryMapModel { BeginAddressValue = desc << 12 };
                        //kcDescriptor.IoMemoryMap.Add(ioMMap);

                         kcDescriptor.MemoryMap.Add(new KcMemoryMapModel
                         {
                             BeginAddressValue = desc << 12,
                             PermissionValue = MemoryMapPermissionEnum.RW,
                             SizeValue = 4096u,
                         });
                        break;
                    case 11: // IRQ Pair
                        if (kcDescriptor.EnableInterrupts == null) kcDescriptor.EnableInterrupts = new List<string>();

                        uint irq0 = desc & 0x3FF;
                        uint irq1 = (desc >> 10) & 0x3FF;

                        kcDescriptor.EnableInterrupts.Add(Utils.ConvertToHexString(irq0));
                        kcDescriptor.EnableInterrupts.Add(Utils.ConvertToHexString(irq1));
                        break;
                    case 13: // App Type
                        kcDescriptor.MiscParams = new KcMiscParamsModel { ProgramTypeValue = (ProgramTypeEnum)(byte)(desc & 7) };
                        break;
                    case 14: // Kernel Release Version
                        kcDescriptor.KernelVersion = new KcKernelVersionModel
                        {
                            MinorVersionVlaue = (byte)(desc & 0xF),
                            MajorVersionVlaue = (ushort)(desc >> 4 & 0xF)
                        };
                        break;
                    case 15: // Handle Table Size
                        kcDescriptor.HandleTableSizeValue.HandleTableSize = (ushort) desc;
                        break;
                    case 16: // Debug Flags
                        kcDescriptor.MiscFlags = new KcMiscFlags
                        {
                            EnableDebugValue = (desc & 1) == 1,
                            ForceDebugValue = ((desc >> 1) & 1) == 1
                        };
                        break;
                }
            }

            return kcDescriptor;
        }
    }
}

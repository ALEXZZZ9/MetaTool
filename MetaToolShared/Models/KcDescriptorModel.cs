using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using AX9.MetaTool.Enums;

namespace AX9.MetaTool.Models
{
    [XmlRoot("KernelCapabilityDescriptor")]
    public class KcDescriptorModel : ICloneable
    {
        public KcDescriptorModel() { }
        public KcDescriptorModel(KcDataModel data)
        {
            ThreadInfo = data.ThreadInfo;
            EnableSystemCalls = data.EnableSystemCalls;
            AllMemoryMap = data.AllMemoryMap;
            EnableInterrupts = data.EnableInterrupts;
            MiscParams = data.MiscParams;
            KernelVersion = data.KernelVersion;
            HandleTableSizeValue = data.HandleTableSizeValue;
            MiscFlags = data.MiscFlags;
        }


        [XmlElement("ThreadInfo")]
        public KcThreadInfoModel ThreadInfo { get; set; } = new KcThreadInfoModel();

        [XmlIgnore]
        public List<byte> SystemCallList => EnableSystemCalls?.Select((sc) => sc.SystemCallIdValue).ToList();

        [XmlElement("EnableSystemCalls")]
        public List<KcEnableSystemCallsModel> EnableSystemCalls { get; set; } = new List<KcEnableSystemCallsModel>();

        [XmlElement("MemoryMap")]
        public List<KcMemoryMapModel> AllMemoryMap { get; set; }

        [XmlIgnore]
        public List<KcMemoryMapModel> MemoryMap
        {
            get
            {
                if (AllMemoryMap == null || AllMemoryMap.Count == 0) return null;

                return AllMemoryMap
                    .Where((mm) => !(mm.SizeValue == 4096u && mm.Permission.ToUpper() == MemoryMapPermissionEnum.RW.ToString()))
                    .ToList();
            }
        }

        [XmlIgnore]
        public List<KcIoMemoryMapModel> IoMemoryMap {
            get
            {
                if (AllMemoryMap == null || AllMemoryMap.Count == 0) return null;

                return AllMemoryMap
                    .Where((mm) => mm.SizeValue == 4096u && mm.Permission.ToUpper() == MemoryMapPermissionEnum.RW.ToString())
                    .Select((mm) => new KcIoMemoryMapModel { BeginAddressValue = mm.BeginAddressValue })
                    .ToList();
            }
        }

        [XmlElement("EnableInterrupts")]
        public List<string> EnableInterrupts { get; set; }

        [XmlElement("MiscParams")]
        public KcMiscParamsModel MiscParams { get; set; } = new KcMiscParamsModel();

        [XmlElement("KernelVersion")]
        public KcKernelVersionModel KernelVersion { get; set; } = new KcKernelVersionModel();

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


        public List<ushort> GetIntList(bool enableEntireInterrupt = false)
        {
            if (EnableInterrupts == null || EnableInterrupts.Count == 0) return null;

            List<ushort> intList = new List<ushort>();

            foreach (string interrupt in EnableInterrupts)
            {
                ushort interruptValue = (ushort)Utils.ConvertDecimalString(interrupt, "EnableInterrupts");

                if (interruptValue == 0) throw new ArgumentException("Cannot specify 0 for EnableInterrupts.");
                if (interruptValue >= 1024) throw new ArgumentException($"The value {interruptValue} for EnableInterrupts is outside the scope.");
                if (intList.Contains(interruptValue)) throw new ArgumentException($"Within EnableInterrupts, {interruptValue} is specified more than once.");
                if (interruptValue == 1023 && !enableEntireInterrupt) throw new ArgumentException($"The value {interruptValue} for EnableInterrupts is outside the scope.");

                intList.Add(interruptValue);
            }

            return intList;
        }

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
                            LowestPriorityValue = (byte)(desc & 0x3F),
                            HighestPriorityValue = (byte)((desc >>= 6) & 0x3F),
                            MinCoreNumberValue = (byte)(desc >>= 6 & 0xFF),
                            MaxCoreNumberValue = (byte)(desc >>= 8 & 0xFF)
                        };
                        break;
                    case 4: // Syscall mask
                        if (kcDescriptor.EnableSystemCalls == null) kcDescriptor.EnableSystemCalls = new List<KcEnableSystemCallsModel>();

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
                                }
                            }
                         
                            desc >>= 1;
                        }
                        break;
                    case 6: // Map IO/Normal
                        if (kcDescriptor.AllMemoryMap == null) kcDescriptor.AllMemoryMap = new List<KcMemoryMapModel>();

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

                        kcDescriptor.AllMemoryMap.Add(mMap);
                        break;
                    case 7: // Map Normal Page
                        //KcIoMemoryMapModel ioMMap = new KcIoMemoryMapModel { BeginAddressValue = desc << 12 };
                        //kcDescriptor.IoMemoryMap.Add(ioMMap);

                         kcDescriptor.AllMemoryMap.Add(new KcMemoryMapModel
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


        public byte[] ExportBinary()
        {
            List<uint> descriptors = new List<uint>();
            ExportThreadInfoBinary(descriptors);
            ExportEnableSystemCallsBinary(descriptors);
            ExportMemoryMapBinary(descriptors);
            ExportIoMemoryMapBinary(descriptors);
            ExportEnableInterruptsBinary(descriptors);
            ExportMiscParamsBinary(descriptors);
            ExportKernelVersionBinary(descriptors);
            ExportHandleTableSizeBinary(descriptors);
            ExportMiscFlagsBinary(descriptors);

            if (descriptors.Count == 0) return null;

            uint[] descriptorsArray = descriptors.ToArray();

            int length = 4;
            int index = 0;
            byte[] bytes = new byte[descriptors.Count * length];

            foreach (uint descriptor in descriptorsArray)
            {
                Array.Copy(BitConverter.GetBytes(descriptor), 0, bytes, index, length);
                index += length;
            }

            return bytes;
        }

        public void ExportThreadInfoBinary(List<uint> descriptors)
        {
            if (ThreadInfo == null) return;
            
            descriptors.Add(ThreadInfo.CalcFlag());
        }
        public void ExportEnableSystemCallsBinary(List<uint> descriptors)
        {
            if (SystemCallList == null || SystemCallList.Count == 0) return;
           
            SystemCallList.Sort();
            KcEnableSystemCallsModel systemCall = new KcEnableSystemCallsModel();

            foreach (byte b in SystemCallList)
            {
                if (systemCall.Index != (b / 24))
                {
                    if (systemCall.GetNumIds() > 0) descriptors.Add(systemCall.CalcFlag());

                    systemCall = new KcEnableSystemCallsModel { Index = (b / 24) };
                }
                systemCall.AddSystemCallId(b);
            }

            if (systemCall.GetNumIds() > 0) descriptors.Add(systemCall.CalcFlag());
        }
        public void ExportMemoryMapBinary(List<uint> descriptors)
        {
            if (MemoryMap == null || MemoryMap.Count == 0) return;

            foreach (KcMemoryMapModel memoryMap in MemoryMap)
            {
                descriptors.AddRange(memoryMap.CalcFlag());
            }
        }
        public void ExportIoMemoryMapBinary(List<uint> descriptors)
        {
            if (IoMemoryMap == null || IoMemoryMap.Count == 0) return;

            foreach (KcIoMemoryMapModel ioMemoryMap in IoMemoryMap)
            {
                descriptors.Add(ioMemoryMap.CalcFlag());
            }
        }
        public void ExportEnableInterruptsBinary(List<uint> descriptors)
        {
            List<ushort> intList = GetIntList();
            if (intList == null || intList.Count == 0) return;

            int i = 0;

            if (intList.Count % 2 != 0)
            {
                descriptors.Add(new InterruptModel(intList[0]).CalcFlag());
                i++;
            }

            while (i < intList.Count)
            {
                InterruptModel interrupt = new InterruptModel(intList[i]);

                if (interrupt[0] == 1023)
                {
                    interrupt[1] = 1023;
                    descriptors.Add(interrupt.CalcFlag());
                    interrupt[0] = 1023;
                }

                interrupt[1] = intList[i + 1];
                descriptors.Add(interrupt.CalcFlag());

                if (interrupt[1] == 1023) descriptors.Add(new InterruptModel(1023, 1023).CalcFlag());

                i += 2;
            }
        }
        public void ExportMiscParamsBinary(List<uint> descriptors)
        {
            if (MiscParams == null) return;

            descriptors.Add(MiscParams.CalcFlag());
        }
        public void ExportKernelVersionBinary(List<uint> descriptors)
        {
            if (KernelVersion == null) return;

            descriptors.Add(KernelVersion.CalcFlag());
        }
        public void ExportHandleTableSizeBinary(List<uint> descriptors)
        {
            if (HandleTableSizeValue == null) return;

            descriptors.Add(HandleTableSizeValue.CalcFlag());
        }
        public void ExportMiscFlagsBinary(List<uint> descriptors)
        {
            if (MiscFlags == null) return;

            descriptors.Add(MiscFlags.CalcFlag());
        }


        public void CheckCapabilities(KcDataModel data)
        {
            if (data == null) return;
            CheckThreadInfo(data.ThreadInfo);
            CheckEnableSystemCalls(data.SystemCallList);
            CheckMemoryMaps(data.AllMemoryMap);
            CheckEnableInterrupts(data.GetIntList());
            CheckMiscParams(data.MiscParams);
            CheckKernelVersion(data.KernelVersion);
            CheckHandleTableSize(data.HandleTableSizeValue);
            CheckMiscFlags(data.MiscFlags);
        }

        public void CheckThreadInfo(KcThreadInfoModel defaultThreadInfo)
        {
            if (ThreadInfo == null) return;
            if (defaultThreadInfo == null) throw new ArgumentException("ThreadInfo is outside the allowed range.");
            if (ThreadInfo.LowestPriorityValue < defaultThreadInfo.HighestPriorityValue || ThreadInfo.LowestPriorityValue > defaultThreadInfo.LowestPriorityValue)
                throw new ArgumentException("ThreadInfo/LowestPriority is outside the allowed range.");
            if (ThreadInfo.HighestPriorityValue < defaultThreadInfo.HighestPriorityValue || ThreadInfo.HighestPriorityValue > defaultThreadInfo.LowestPriorityValue)
                throw new ArgumentException("ThreadInfo/HighestPriority is outside the allowed range.");
            if (ThreadInfo.MinCoreNumberValue < defaultThreadInfo.MinCoreNumberValue || ThreadInfo.MinCoreNumberValue > defaultThreadInfo.MaxCoreNumberValue)
                throw new ArgumentException("ThreadInfo/MinCoreNumber is outside the allowed range.");
            if (ThreadInfo.MaxCoreNumberValue < defaultThreadInfo.MinCoreNumberValue || ThreadInfo.MaxCoreNumberValue > defaultThreadInfo.MaxCoreNumberValue)
                throw new ArgumentException("ThreadInfo/MaxCoreNumber is outside the allowed range.");
        }
        public void CheckEnableSystemCalls(List<byte> defaultSystemCallList)
        {
            if (defaultSystemCallList == null || SystemCallList == null) return;

            foreach (byte b in SystemCallList)
            {
                if (defaultSystemCallList.IndexOf(b) < 0) throw new ArgumentException($"The {b} value for EnableSystemCalls is not allowed.");
            }
        }
        public void CheckMemoryMaps(List<KcMemoryMapModel> defaultMemoryMap)
        {
            if (MemoryMap == null || MemoryMap.Count == 0) return;

            foreach (KcMemoryMapModel mMap in AllMemoryMap)
            {
                bool flag = false;
                foreach (KcMemoryMapModel dMMap in defaultMemoryMap)
                {
                    if (mMap.BeginAddressValue >= dMMap.BeginAddressValue && 
                        mMap.BeginAddressValue + mMap.SizeValue - 1 <= dMMap.BeginAddressValue + dMMap.SizeValue - 1 && 
                        mMap.PermissionValue == dMMap.PermissionValue && mMap.TypeValue == dMMap.TypeValue)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    throw new ArgumentException($"The KernelCapabilityData/MemoryMap value is outside the allowed range. (BaseAddress={mMap.BeginAddress}, Size={mMap.Size}, Permission={mMap.Permission}, Type={mMap.Type})");
            }
        }
        public void CheckEnableInterrupts(List<ushort> defaultInterrupts)
        {
            List<ushort> intList = GetIntList();
            if (intList == null || intList.Count == 0) return;

            if (defaultInterrupts.IndexOf(1023) >= 0) return;

            foreach (ushort interrupt in intList)
            {
                if (interrupt != 1023 && defaultInterrupts.IndexOf(interrupt) < 0)
                {
                    throw new ArgumentException($"The {interrupt} value for EnableInterrupts is not allowed.");
                }
            }
        }
        public void CheckMiscParams(KcMiscParamsModel defaultMiscParams)
        {
            if (MiscParams == null) return;

            if (defaultMiscParams == null) throw new ArgumentException("MiscParams is outside the allowed range.");
            if (MiscParams.ProgramType != defaultMiscParams.ProgramType) throw new ArgumentException("MiscParams/ProgramType is outside the allowed range.");
        }
        public void CheckKernelVersion(KcKernelVersionModel defaultKernelVersion)
        {
            if (KernelVersion == null) return;

            if (defaultKernelVersion == null || KernelVersion.MajorVersion != defaultKernelVersion.MajorVersion || KernelVersion.MinorVersion != defaultKernelVersion.MinorVersion)
                throw new ArgumentException("KernelVersion is outside the allowed range.");
        }
        public void CheckHandleTableSize(KcHandleTableSizeModel defaultHandleTableSize)
        {
            if (HandleTableSizeValue == null) return;

            if (defaultHandleTableSize == null || HandleTableSizeValue.HandleTableSize > defaultHandleTableSize.HandleTableSize)
                throw new ArgumentException("KernelCapabilityData/HandleTableSize is outside the allowed range.");
        }
        public void CheckMiscFlags(KcMiscFlags defaultMiscFlags)
        {
            if (MiscFlags == null) return;

            if (defaultMiscFlags == null) throw new ArgumentException("MiscFlags is outside the allowed range.");
            if (MiscFlags.EnableDebugValue && !defaultMiscFlags.EnableDebugValue) throw new ArgumentException("MiscFlags/EnableDebug is outside the allowed range.");
            if (MiscFlags.ForceDebugValue && !defaultMiscFlags.ForceDebugValue) throw new ArgumentException("MiscFlags/ForceDebug is outside the allowed range.");
        }

        public object Clone()
        {
            return new KcDescriptorModel
            {
                ThreadInfo = (KcThreadInfoModel)ThreadInfo?.Clone(),
                EnableSystemCalls = (List<KcEnableSystemCallsModel>)EnableSystemCalls?.Clone(),
                AllMemoryMap = (List<KcMemoryMapModel>)AllMemoryMap?.Clone(),
                EnableInterrupts = (List<string>)EnableInterrupts?.Clone(),
                MiscParams = (KcMiscParamsModel)MiscParams?.Clone(),
                KernelVersion = (KcKernelVersionModel)KernelVersion?.Clone(),
                HandleTableSizeValue = (KcHandleTableSizeModel)HandleTableSizeValue?.Clone(),
                MiscFlags = (KcMiscFlags)MiscFlags?.Clone(),
            };
        }
    }
}

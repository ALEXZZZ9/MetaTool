using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using AX9.MetaTool.Enums;

namespace AX9.MetaTool.Models
{
    [XmlRoot("KernelCapabilityData")]
    public class KcDataModel : ICloneable
    {
        public KcDataModel() { }
        public KcDataModel(KcDescriptorModel descriptor)
        {
            ThreadInfo = descriptor.ThreadInfo;
            EnableSystemCalls = descriptor.EnableSystemCalls;
            AllMemoryMap = descriptor.AllMemoryMap;
            EnableInterrupts = descriptor.EnableInterrupts;
            MiscParams = descriptor.MiscParams;
            KernelVersion = descriptor.KernelVersion;
            HandleTableSizeValue = descriptor.HandleTableSizeValue;
            MiscFlags = descriptor.MiscFlags;
        }


        [XmlElement("ThreadInfo")]
        public KcThreadInfoModel ThreadInfo { get; set; }

        [XmlIgnore]
        public List<byte> SystemCallList => EnableSystemCalls?.Select((sc) => sc.SystemCallIdValue).ToList();

        [XmlElement("EnableSystemCalls")]
        public List<KcEnableSystemCallsModel> EnableSystemCalls { get; set; }

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
        public List<KcIoMemoryMapModel> IoMemoryMap
        {
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
        public KcMiscParamsModel MiscParams { get; set; }

        [XmlElement("KernelVersion")]
        public KcKernelVersionModel KernelVersion { get; set; }

        [XmlIgnore]
        public KcHandleTableSizeModel HandleTableSizeValue { get; set; }

        [XmlElement("HandleTableSize")]
        public string HandleTableSize
        {
            get => HandleTableSizeValue?.HandleTableSize.ToString("D");
            set
            {
                if (value == null) return;

                if (HandleTableSizeValue == null) HandleTableSizeValue = new KcHandleTableSizeModel();
                HandleTableSizeValue.HandleTableSize = checked((ushort)Utils.ConvertDecimalString(value, "KernelCapabilityData/HandleTableSize"));
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

        public object Clone()
        {
            return new KcDataModel
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

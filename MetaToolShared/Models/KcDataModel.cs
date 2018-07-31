using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AX9.MetaTool.Models
{
    [XmlRoot("KernelCapabilityData")]
    public class KcDataModel
    {
        public KcDataModel() { }
        public KcDataModel(KcDescriptorModel descriptor)
        {
            ThreadInfo = descriptor.ThreadInfo;
            EnableSystemCalls = descriptor.EnableSystemCalls;
            MemoryMap = descriptor.MemoryMap;
            EnableInterrupts = descriptor.EnableInterrupts;
            MiscParams = descriptor.MiscParams;
            KernelVersion = descriptor.KernelVersion;
            HandleTableSize = descriptor.HandleTableSize;
            MiscFlags = descriptor.MiscFlags;
        }


        [XmlElement("ThreadInfo")]
        public KcThreadInfoModel ThreadInfo { get; set; }

        [XmlElement("EnableSystemCalls")]
        public List<KcEnableSystemCallsModel> EnableSystemCalls { get; set; }

        [XmlElement("MemoryMap")]
        public List<KcMemoryMapModel> MemoryMap { get; set; }

        [XmlElement("EnableInterrupts")]
        public List<string> EnableInterrupts { get; set; }

        [XmlElement("MiscParams")]
        public KcMiscParamsModel MiscParams { get; set; }

        [XmlElement("KernelVersion")]
        public KcKernelVersionModel KernelVersion { get; set; }

        [XmlIgnore]
        public ushort HandleTableSizeValue
        {
            get => handleTableSizeValue;
            set
            {
                handleTableSize = value.ToString("D");
                handleTableSizeValue = value;
            }
        }

        [XmlElement("HandleTableSize")]
        public string HandleTableSize
        {
            get => handleTableSize;
            set
            {
                if (value == null) return;
                
                var size = checked((ushort)Utils.ConvertDecimalString(value, "KernelCapabilityData/HandleTableSize"));

                if (size >= 1024) throw new ArgumentException("KernelCapabilityData/HandleTableSize is invalid");

                handleTableSizeValue = size;
                handleTableSize = value;
            }
        }

        [XmlElement("MiscFlags")]
        public KcMiscFlags MiscFlags { get; set; }


        private ushort handleTableSizeValue;
        private string handleTableSize;
    }
}

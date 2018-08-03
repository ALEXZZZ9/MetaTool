using System;
using System.Linq;
using System.Xml.Serialization;
using AX9.MetaTool.Enums;

namespace AX9.MetaTool.Models
{
    public class DefaultModel
    {
        public DefaultModel() { }
        public DefaultModel(bool initDefault)
        {
            if (initDefault)
            {
                MainThreadPriorityValue = 44;
                MainThreadCoreNumberValue = 0;
                MainThreadStackSizeValue = 0x100000;
                ProcessAddressSpaceValue = ProcessAddressSpacesEnum.AddressSpace64Bit;
            }
        }


        [XmlIgnore]
        public bool Is64BitInstructionValue
        {
            get => is64BitInstructionValue;
            set
            {
                is64BitInstruction = value.ToString();
                is64BitInstructionValue = value;
            }
        }

        [XmlElement("Is64BitInstruction", IsNullable = false)]
        public string Is64BitInstruction
        {
            get => is64BitInstruction;
            set
            {
                if (value == null) return;

                is64BitInstructionValue = Utils.ConvertBoolString(value, "Default/Is64BitInstruction");
                is64BitInstruction = value;
            }
        }

        [XmlIgnore]
        public ProcessAddressSpacesEnum ProcessAddressSpaceValue
        {
            get => processAddressSpaceValue;
            set
            {
                processAddressSpace = value.ToString();
                Is64BitInstructionValue = (processAddressSpaceValue == ProcessAddressSpacesEnum.AddressSpace64Bit || processAddressSpaceValue == ProcessAddressSpacesEnum.AddressSpace64BitOld);
                processAddressSpaceValue = value;
            }
        }

        [XmlElement("ProcessAddressSpace", IsNullable = false)]
        public string ProcessAddressSpace
        {
            get => processAddressSpace;
            set
            {
                if (!Enum.GetNames(typeof(ProcessAddressSpacesEnum)).Contains(value)) throw new ArgumentException($"Specified an invalid string, {value}, in Default/ProcessAddressSpace");

                processAddressSpaceValue = (ProcessAddressSpacesEnum)Enum.Parse(typeof(ProcessAddressSpacesEnum), value);
                Is64BitInstructionValue = (processAddressSpaceValue == ProcessAddressSpacesEnum.AddressSpace64Bit || processAddressSpaceValue == ProcessAddressSpacesEnum.AddressSpace64BitOld);
                processAddressSpace = value;
            }
        }

        [XmlIgnore]
        public byte MainThreadPriorityValue
        {
            get => mainThreadPriorityValue;
            set
            {
                mainThreadPriority = value.ToString("D");
                mainThreadPriorityValue = value;
            }
        }

        [XmlElement("MainThreadPriority", IsNullable = false)]
        public string MainThreadPriority
        {
            get => mainThreadPriority;
            set
            {
                if (value == null) return;

                mainThreadPriorityValue = checked((byte)Utils.ConvertDecimalString(value, "Default/MainThreadPriority"));

                if (MainThreadPriorityValue > 63) throw new ArgumentException("Out of range in Default/MainThreadPriority");

                mainThreadPriority = value;
            }
        }

        [XmlIgnore]
        public byte MainThreadCoreNumberValue
        {
            get => mainThreadCoreNumberValue;
            set
            {
                mainThreadCoreNumber = value.ToString("D");
                mainThreadCoreNumberValue = value;
            }
        }

        [XmlElement("MainThreadCoreNumber", IsNullable = false)]
        public string MainThreadCoreNumber
        {
            get => mainThreadCoreNumber;
            set
            {
                if (value == null) return;

                mainThreadCoreNumberValue = checked((byte)Utils.ConvertDecimalString(value, "Default/MainThreadCoreNumber"));
                mainThreadCoreNumber = value;
            }
        }

        [XmlIgnore]
        public uint MainThreadStackSizeValue
        {
            get => mainThreadStackSizeValue;
            set
            {
                mainThreadStackSize = Utils.ConvertToHexString(value);
                mainThreadStackSizeValue = value;
            }
        }

        [XmlElement("MainThreadStackSize", IsNullable = false)]
        public string MainThreadStackSize
        {
            get => mainThreadStackSize;
            set
            {
                if (value == null) return;

                mainThreadStackSizeValue = checked((uint)Utils.ConvertHexString(value, "Default/MainThreadStackSize"));

                if ((MainThreadStackSizeValue & 4095u) > 0u || MainThreadStackSizeValue == 0u) throw new ArgumentException("Default/MainThreadStackSize is invalid");

                mainThreadStackSize = value;
            }
        }

        [XmlElement("FsAccessControlData")]
        public FaDataModel FsAccessControlData { get; set; }

        [XmlElement("SrvAccessControlData")]
        public SaDataModel SrvAccessControlData { get; set; }

        [XmlElement("KernelCapabilityData")]
        public KcDataModel KernelCapabilityData { get; set; }


        public const byte LowestThreadPriority = 63;
        public const byte HighestThreadPriority = 0;
        public const uint AlignMainThraedStackSize = 4096u;

        private bool is64BitInstructionValue;
        private string is64BitInstruction;
        private ProcessAddressSpacesEnum processAddressSpaceValue;
        private string processAddressSpace;
        private byte mainThreadPriorityValue;
        private string mainThreadPriority;
        private byte mainThreadCoreNumberValue;
        private string mainThreadCoreNumber;
        private uint mainThreadStackSizeValue;
        private string mainThreadStackSize;


        public void CheckReadSuccess()
        {
            if (Is64BitInstruction == null) throw new ArgumentException("Not Found Default/Is64BitInstruction");
            if (ProcessAddressSpace == null) throw new ArgumentException("Not Found Default/ProcessAddressSpace");
            if (MainThreadPriority == null) throw new ArgumentException("Not Found Default/MainThreadPriority");
            if (MainThreadCoreNumber == null) throw new ArgumentException("Not Found Default/MainThreadCoreNumber");
            if (MainThreadStackSize == null) throw new ArgumentException("Not Found Default/MainThreadStackSize");
        }
    }
}

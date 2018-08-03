using System;
using System.Text;
using System.Xml.Serialization;
using AX9.MetaTool.Enums;
using System.Linq;

namespace AX9.MetaTool.Models
{
    [XmlRoot("Core")]
    public class CoreModel : ICloneable
    {
        public CoreModel() { }
        public CoreModel(bool initDefault)
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
        public ulong ProgramIdValue
        {
            get => programIdValue;
            set
            {
                programId = Utils.ConvertToHexString(value);
                programIdValue = value;
            }
        }

        [XmlElement("ProgramId", IsNullable = false)]
        public string ProgramId
        {
            get => programId;
            set
            {
                if (programId != null ) throw new ArgumentException("Either Core/ProgramId or Core/ApplicationId is declared more than once.");

                if (!string.IsNullOrEmpty(value)) programIdValue = Utils.ConvertHexString(value, "Core/ProgramId");
                programId = value;
            }
        }

        [XmlElement("ApplicationId", IsNullable = false)]
        public string ApplicationId
        {
            get => programId;
            set
            {
                if (programId != null) throw new ArgumentException("Either Core/ProgramId or Core/ApplicationId is declared more than once.");

                if (!string.IsNullOrEmpty(value)) programIdValue = Utils.ConvertHexString(value, "Core/ApplicationId");
                programId = value;
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

        [XmlElement("Is64BitInstruction")]
        public string Is64BitInstruction
        {
            get => is64BitInstruction;
            set
            {
                if (!string.IsNullOrEmpty(value)) is64BitInstructionValue = Utils.ConvertBoolString(value, "Core/Is64BitInstruction");
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

        [XmlElement("ProcessAddressSpace")]
        public string ProcessAddressSpace
        {
            get => processAddressSpace;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (!Enum.GetNames(typeof(ProcessAddressSpacesEnum)).Contains(value)) throw new ArgumentException($"Specified an invalid string, {value}, in ProcessAddressSpace");

                    processAddressSpaceValue = (ProcessAddressSpacesEnum)Enum.Parse(typeof(ProcessAddressSpacesEnum), value);
                    Is64BitInstructionValue = (processAddressSpaceValue == ProcessAddressSpacesEnum.AddressSpace64Bit || processAddressSpaceValue == ProcessAddressSpacesEnum.AddressSpace64BitOld);
                }

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

        [XmlElement("MainThreadPriority")]
        public string MainThreadPriority
        {
            get => mainThreadPriority;
            set
            {
                if (!string.IsNullOrEmpty(value))
                { 
                    mainThreadPriorityValue = checked((byte)Utils.ConvertDecimalString(value, "Core/MainThreadPriority"));

                    if (MainThreadPriorityValue > 63) throw new ArgumentException("Out of range in Core/MainThreadPriority");
                }
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

        [XmlElement("MainThreadCoreNumber")]
        public string MainThreadCoreNumber
        {
            get => mainThreadCoreNumber;
            set
            {
                if (!string.IsNullOrEmpty(value)) mainThreadCoreNumberValue = checked((byte)Utils.ConvertDecimalString(value, "Core/MainThreadCoreNumber"));
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

        [XmlElement("MainThreadStackSize")]
        public string MainThreadStackSize
        {
            get => mainThreadStackSize;
            set
            {
                if (!string.IsNullOrEmpty(value))
                { 
                    mainThreadStackSizeValue = checked((uint)Utils.ConvertHexString(value, "Core/MainThreadStackSize"));

                    if ((MainThreadStackSizeValue & 4095u) > 0u || MainThreadStackSizeValue == 0u) throw new ArgumentException("Core/MainThreadStackSize is invalid");
                }
                mainThreadStackSize = value;
            }
        }

        [XmlIgnore]
        public uint VersionValue
        {
            get => versionValue;
            set
            {
                version = value.ToString("D");
                versionValue = value;
            }
        }

        [XmlElement("Version")]
        public string Version
        {
            get => version;
            set
            {
                if (!string.IsNullOrEmpty(value)) versionValue = checked((uint)Utils.ConvertDecimalString(value, "Core/Version"));
                version = value;
            }
        }

        [XmlIgnore]
        public byte[] NameValue
        {
            get => nameValue;
            set
            {
                if (value.Length > 15) throw new ArgumentException("Specified 15 or more characters in Core/Name.");

                name = Encoding.UTF8.GetString(value);
                nameValue = value;
            }
        }

        [XmlElement("Name")]
        public string Name
        {
            get => name;
            set
            {
                if (!string.IsNullOrEmpty(value))
                { 
                    byte[] valueBytes = Encoding.UTF8.GetBytes(value);

                    if (valueBytes.Length > 15) throw new ArgumentException("Specified 15 or more characters in Core/Name.");

                    nameValue = valueBytes;
                }

                name = value;
            }
        }

        [XmlIgnore]
        public byte[] ProductCodeValue
        {
            get => productCodeValue;
            set
            {
                if (value.Length > 15) throw new ArgumentException("Specified 15 or more characters in Core/ProductCode.");

                productCode = Encoding.UTF8.GetString(value);
                productCodeValue = value;
            }
        }

        [XmlElement("ProductCode")]
        public string ProductCode
        {
            get => productCode;
            set
            {
                if (!string.IsNullOrEmpty(value))
                { 
                    byte[] valueBytes = Encoding.UTF8.GetBytes(value);

                    if (valueBytes.Length > 15) throw new ArgumentException("Specified 15 or more characters in Core/ProductCode.");

                    productCodeValue = valueBytes;
                }

                productCode = value;
            }
        }

        [XmlIgnore]
        public uint SystemResourceSizeValue
        {
            get => systemResourceSizeValue;
            set
            {
                systemResourceSize = Utils.ConvertToHexString(value);
                systemResourceSizeValue = value;
            }
        }

        [XmlElement("SystemResourceSize")]
        public string SystemResourceSize
        {
            get => systemResourceSize;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    uint size = checked((uint) Utils.ConvertHexString(value, "Core/SystemResourceSize"));

                    if ((size & 2047u) > 0u) throw new ArgumentException("Core/SystemResourceSize is invalid");
                    if (size > 534773760u) throw new ArgumentException("Out of range in Core/SystemResourceSize");

                    systemResourceSizeValue = size;
                }
                systemResourceSize = value;
            }
        }

        [XmlElement("FsAccessControlData")]
        public FaDataModel FsAccessControlData { get; set; }

        [XmlElement("SrvAccessControlData")]
        public SaDataModel SrvAccessControlData { get; set; }

        [XmlElement("KernelCapabilityData")]
        public KcDataModel KernelCapabilityData { get; set; }


        private ulong programIdValue;
        private string programId;
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
        private uint versionValue;
        private string version;
        private byte[] nameValue;
        private string name;
        private byte[] productCodeValue;
        private string productCode;
        private uint systemResourceSizeValue;
        private string systemResourceSize;


        public object Clone()
        {
            return new CoreModel
            {
                ProgramId = ProgramId,
                Is64BitInstruction = Is64BitInstruction,
                ProcessAddressSpace = ProcessAddressSpace,
                MainThreadPriority = MainThreadPriority,
                MainThreadCoreNumber = MainThreadCoreNumber,
                MainThreadStackSize = MainThreadStackSize,
                Version = Version,
                Name = Name,
                ProductCode = ProductCode,
                SystemResourceSize = SystemResourceSize,
                FsAccessControlData = (FaDataModel)FsAccessControlData?.Clone(),
                SrvAccessControlData = (SaDataModel)SrvAccessControlData?.Clone(),
                KernelCapabilityData = (KcDataModel)KernelCapabilityData?.Clone()
            };
        }
    }
}

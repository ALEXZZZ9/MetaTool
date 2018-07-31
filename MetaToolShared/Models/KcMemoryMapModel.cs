using System;
using System.Linq;
using System.Xml.Serialization;
using AX9.MetaTool.Enums;

namespace AX9.MetaTool.Models
{
    [XmlRoot("MemoryMap")]
    public class KcMemoryMapModel
    {
        [XmlIgnore]
        public ulong BeginAddressValue
        {
            get => beginAddressValue;
            set
            {
                beginAddress = Utils.ConvertToHexString(value);
                beginAddressValue = value;
            }
        }

        [XmlElement("BeginAddress", IsNullable = false)]
        public string BeginAddress
        {
            get => beginAddress;
            set
            {
                if (value == null) return;

                beginAddressValue = Utils.ConvertHexString(value, "MemoryMap/BeginAddress");
                beginAddress = value;
            }
        }

        [XmlIgnore]
        public uint SizeValue
        {
            get => sizeValue;
            set
            {
                size = Utils.ConvertToHexString(value);
                sizeValue = value;
            }
        }

        [XmlElement("Size", IsNullable = false)]
        public string Size
        {
            get => size;
            set
            {
                if (value == null) return;

                sizeValue = checked((uint)Utils.ConvertHexString(value, "MemoryMap/Size"));
                size = value;
            }
        }

        [XmlIgnore]
        public MemoryMapPermissionEnum PermissionValue
        {
            get => permissionValue;
            set
            {
                permission = value.ToString();
                permissionValue = value;
            }
        }

        [XmlElement("Permission", IsNullable = false)]
        public string Permission
        {
            get => permission;
            set
            {
                if (!Enum.GetNames(typeof(MemoryMapPermissionEnum)).Contains(value.ToUpper())) throw new ArgumentException("MemoryMap/Permission is invalid");

                permissionValue = (MemoryMapPermissionEnum)Enum.Parse(typeof(MemoryMapPermissionEnum), value);
                permission = value;
            }
        }

        [XmlIgnore]
        public MemoryMapTypeEnum TypeValue
        {
            get => typeValue;
            set
            {
                type = value.ToString();
                typeValue = value;
            }
        }

        [XmlElement("Type", IsNullable = false)]
        public string Type
        {
            get => type;
            set
            {
                if (!Enum.GetNames(typeof(MemoryMapTypeEnum)).Contains(value.ToUpper())) throw new ArgumentException("MemoryMap/Type is invalid");

                typeValue = (MemoryMapTypeEnum)Enum.Parse(typeof(MemoryMapTypeEnum), value);

                type = value;
            }
        }


        public const int AddrFieldSize = 24;
        public const int SizeFieldSize = 24;
        public const int PageShift = 12;

        private ulong beginAddressValue;
        private string beginAddress;
        private uint sizeValue;
        private string size;
        private MemoryMapPermissionEnum permissionValue;
        private string permission;
        private MemoryMapTypeEnum typeValue;
        private string type;

        private readonly KernelCapabilityFlag addrCapability = new KernelCapabilityFlag { EntryNumber = 6u };
        private readonly KernelCapabilityFlag sizeCapability = new KernelCapabilityFlag { EntryNumber = 6u };


        public uint[] CalcFlag()
        {
            uint[] array = new uint[2];
            addrCapability.FieldValue = (uint)(BeginAddressValue >> PageShift);
            addrCapability.FieldValue &= 0xFFFFFF;

            addrCapability.FieldValue |= (PermissionValue == MemoryMapPermissionEnum.RW ? 0u : 1u) << AddrFieldSize;
            array[0] = addrCapability.Flag;

            sizeCapability.FieldValue = SizeValue >> PageShift;
            sizeCapability.FieldValue &= 0xFFFFF;

            sizeCapability.FieldValue |= (TypeValue == MemoryMapTypeEnum.IO ? 0u : 1u) << SizeFieldSize;
            array[1] = sizeCapability.Flag;

            return array;
        }

        public bool CheckSuccessToRead()
        {
            if (BeginAddress == null) throw new ArgumentException("Not Found MemoryMap/BeginAddress");
            if (Size == null) throw new ArgumentException("Not Found MemoryMap/Size");
            if (Permission == null) throw new ArgumentException("Not Found MemoryMap/Permission");
            if (Type == null) throw new ArgumentException("Not Found MemoryMap/Type");
        
            return true;
        }
    }
}

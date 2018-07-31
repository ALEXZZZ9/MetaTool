using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using AX9.MetaTool.Enums;

namespace AX9.MetaTool.Models
{
    [XmlRoot("FsAccessControlDescriptor")]
    public class FaDescriptorModel
    {
        [XmlIgnore]
        public byte Version = 1;

        [XmlIgnore]
        public ulong FlagPresetsBit;

        [XmlElement("FlagPresets")]
        public List<string> FlagPresets { get; set; } = new List<string>();

        [XmlIgnore]
        public ulong ContentOwnerIdMinValue
        {
            get => contentOwnerIdMinValue;
            set
            {
                contentOwnerIdMin = Utils.ConvertToHexString(value);
                contentOwnerIdMinValue = value;
            }
        }

        [XmlElement("ContentOwnerIdMin")]
        public string ContentOwnerIdMin
        {
            get => contentOwnerIdMin;
            set
            {
                if (value == null) return;

                contentOwnerIdMinValue = Utils.ConvertHexString(value, "FsAccessControlDescriptor/ContentOwnerIdMin");
                contentOwnerIdMin = value;
            }
        }

        [XmlIgnore]
        public ulong ContentOwnerIdMaxValue
        {
            get => contentOwnerIdMaxValue;
            set
            {
                contentOwnerIdMax = Utils.ConvertToHexString(value);
                contentOwnerIdMaxValue = value;
            }
        }

        [XmlElement("ContentOwnerIdMax")]
        public string ContentOwnerIdMax
        {
            get => contentOwnerIdMax;
            set
            {
                if (value == null) return;

                contentOwnerIdMaxValue = Utils.ConvertHexString(value, "FsAccessControlDescriptor/ContentOwnerIdMax");
                contentOwnerIdMax = value;
            }
        }

        [XmlIgnore]
        public ulong SaveDataOwnerIdMinValue
        {
            get => saveDataOwnerIdMinValue;
            set
            {
                saveDataOwnerIdMin = Utils.ConvertToHexString(value);
                saveDataOwnerIdMinValue = value;
            }
        }

        [XmlElement("SaveDataOwnerIdMin")]
        public string SaveDataOwnerIdMin
        {
            get => saveDataOwnerIdMin;
            set
            {
                if (value == null) return;
                
                saveDataOwnerIdMinValue = Utils.ConvertHexString(value, "FsAccessControlDescriptor/SaveDataOwnerIdMin");
                saveDataOwnerIdMin = value;
            }
        }

        [XmlIgnore]
        public ulong SaveDataOwnerIdMaxValue
        {
            get => saveDataOwnerIdMaxValue;
            set
            {
                saveDataOwnerIdMax = Utils.ConvertToHexString(value);
                saveDataOwnerIdMaxValue = value;
            }
        }

        [XmlElement("SaveDataOwnerIdMax")]
        public string SaveDataOwnerIdMax
        {
            get => saveDataOwnerIdMax;
            set
            {
                if (value == null) return;

                saveDataOwnerIdMaxValue = Utils.ConvertHexString(value, "FsAccessControlDescriptor/SaveDataOwnerIdMax");
                saveDataOwnerIdMax = value;
            }
        }

        [XmlIgnore]
        public List<ulong> ContentOwnerIdsValue => ContentOwnerIds.Select((s) => Utils.ConvertHexString(s, "ContentOwnerIds")).ToList();

        [XmlElement("ContentOwnerIds")]
        public List<string> ContentOwnerIds { get; set; } = new List<string>();

        [XmlIgnore]
        public List<ulong> SaveDataOwnerIdsValue => SaveDataOwnerIds.Select((s) => Utils.ConvertHexString(s, "SaveDataOwnerIds")).ToList();

        [XmlElement("SaveDataOwnerIds")]
        public List<string> SaveDataOwnerIds { get; set; } = new List<string>();

        [XmlIgnore]
        public List<SaveDataOwnerId> SaveDataOwnerIdsWithAccesibilities { get; set; } = new List<SaveDataOwnerId>();

        [XmlIgnore]
        public List<byte> SaveDataOwnerIdAccesibilities = new List<byte>();


        private ulong contentOwnerIdMinValue;
        private string contentOwnerIdMin;
        private ulong contentOwnerIdMaxValue;
        private string contentOwnerIdMax;
        private ulong saveDataOwnerIdMinValue;
        private string saveDataOwnerIdMin;
        private ulong saveDataOwnerIdMaxValue;
        private string saveDataOwnerIdMax;


        public static FaDescriptorModel FromNpdmBytes(byte[] bytes)
        {
            ulong flags = BitConverter.ToUInt64(bytes, 4);
            int num = BitConverter.ToInt32(bytes, 12);
            int contentOwnerInfoSize = BitConverter.ToInt32(bytes, 16);
            int saveDataOwnerInfoSize = BitConverter.ToInt32(bytes, 24);

            FaDescriptorModel fACData = new FaDescriptorModel
            {
                Version = bytes[0],
                FlagPresetsBit = flags
            };

            foreach (FsAccessFlagPresetEnum preset in Enum.GetValues(typeof(FsAccessFlagPresetEnum)))
            {
                if ((flags & (ulong)preset) == (ulong)preset)
                {
                    if (!fACData.FlagPresets.Contains(preset.ToString()))
                        fACData.FlagPresets.Add(preset.ToString());
                }
            }

            if (contentOwnerInfoSize > 0)
            {
                int contentOwnerIdsCount = BitConverter.ToInt32(bytes, num);
                num += 4;

                for (int i = 0; i < contentOwnerIdsCount; i++)
                {
                    fACData.ContentOwnerIds.Add(Utils.ConvertToHexString(BitConverter.ToUInt64(bytes, num)));
                    num += 8;
                }
            }

            if (saveDataOwnerInfoSize > 0)
            {
                int saveDataOwnerIdsCount = BitConverter.ToInt32(bytes, num);
                num += 4;

                for (int i = 0; i < saveDataOwnerIdsCount; i++)
                {
                    fACData.SaveDataOwnerIdAccesibilities.Add(bytes[num]);
                    num += 1;
                }

                num = Utils.RoundUp(num, 4u);
                for (int i = 0; i < saveDataOwnerIdsCount; i++)
                {
                    fACData.SaveDataOwnerIds.Add(Utils.ConvertToHexString(BitConverter.ToUInt64(bytes, num)));
                    num += 8;
                }

                for (int i = 0; i < fACData.SaveDataOwnerIdAccesibilities.Count; i++)
                {
                    fACData.SaveDataOwnerIdsWithAccesibilities.Add(new SaveDataOwnerId
                    {
                        AccessibilityValue = (SaveDataAccessibilityEnum)fACData.SaveDataOwnerIdAccesibilities[i],
                        Id = fACData.SaveDataOwnerIds[i]
                    });
                }
            }


            return fACData;
        }
    }
}

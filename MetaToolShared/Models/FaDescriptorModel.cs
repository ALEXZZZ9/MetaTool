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
        public FaDescriptorModel()
        {
            Version = 1;
        }
        public FaDescriptorModel(FaDataModel data)
        {
            FlagPresets = data.FlagPresets;
            ContentOwnerIds = data.ContentOwnerIds;
            SaveDataOwnerIds = data.SaveDataOwnerIds;
        }


        [XmlIgnore]
        public byte Version;

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

        [XmlElement("SaveDataOwnerIds")]
        public List<SaveDataOwnerId> SaveDataOwnerIds { get; set; } = new List<SaveDataOwnerId>();

        private ulong contentOwnerIdMinValue;
        private string contentOwnerIdMin;
        private ulong contentOwnerIdMaxValue;
        private string contentOwnerIdMax;
        private ulong saveDataOwnerIdMinValue;
        private string saveDataOwnerIdMin;
        private ulong saveDataOwnerIdMaxValue;
        private string saveDataOwnerIdMax;
        private int dataSize => 28 + contentOwnerInfoSize + saveDataOwnerInfoSize;
        private int contentOwnerInfoSize => ((ContentOwnerIds.Count != 0) ? (4 + ContentOwnerIds.Count * 8) : 0);
        private int saveDataOwnerInfoSize => ((SaveDataOwnerIds.Count != 0) ? (4 + Utils.RoundUp(SaveDataOwnerIds.Count, 4u) + SaveDataOwnerIds.Count * 8) : 0);


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
                List<string> ids = new List<string>();
                List<byte> accesibilities = new List<byte>();
                int saveDataOwnerIdsCount = BitConverter.ToInt32(bytes, num);
                num += 4;

                for (int i = 0; i < saveDataOwnerIdsCount; i++)
                {
                    accesibilities.Add(bytes[num]);
                    num += 1;
                }

                num = Utils.RoundUp(num, 4u);
                for (int i = 0; i < saveDataOwnerIdsCount; i++)
                {
                    ids.Add(Utils.ConvertToHexString(BitConverter.ToUInt64(bytes, num)));
                    num += 8;
                }

                for (int i = 0; i < accesibilities.Count; i++)
                {
                    fACData.SaveDataOwnerIds.Add(new SaveDataOwnerId
                    {
                        AccessibilityValue = (SaveDataAccessibilityEnum)accesibilities[i],
                        Id = ids[i]
                    });
                }
            }


            return fACData;
        }

        public void UpdateFlagPresetsBit()
        {
            if (FlagPresets != null)
            {
                ulong bit = 0UL;

                FlagPresets.ForEach((flagName) =>
                {
                    if (Enum.TryParse(flagName, out FsAccessFlagPresetEnum flag)) bit |= (ulong)flag;
                });

                FlagPresetsBit = bit;
            }
        }

        public byte[] ExportBinary()
        {
            byte[] array = new byte[dataSize];

            Buffer.BlockCopy(BitConverter.GetBytes(Version), 0, array, 0, 1);

            UpdateFlagPresetsBit();
            Buffer.BlockCopy(BitConverter.GetBytes(FlagPresetsBit), 0, array, 4, 8);

            int num = 28;
            Buffer.BlockCopy(BitConverter.GetBytes(num), 0, array, 12, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(contentOwnerInfoSize), 0, array, 16, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(num + contentOwnerInfoSize), 0, array, 20, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(saveDataOwnerInfoSize), 0, array, 24, 4);

            if (contentOwnerInfoSize != 0)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(ContentOwnerIds.Count), 0, array, num, 4);
                num += 4;
                foreach (ulong value in ContentOwnerIdsValue)
                {
                    Buffer.BlockCopy(BitConverter.GetBytes(value), 0, array, num, 8);
                    num += 8;
                }
            }

            if (saveDataOwnerInfoSize != 0)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(SaveDataOwnerIds.Count), 0, array, num, 4);

                num += 4;
                foreach (SaveDataAccessibilityEnum accessibility in SaveDataOwnerIds.Select((s) => s.AccessibilityValue))
                {
                    Buffer.BlockCopy(BitConverter.GetBytes((short)accessibility), 0, array, num, 1);
                    num++;
                }

                num = Utils.RoundUp(num, 4u);
                foreach (ulong id in SaveDataOwnerIds.Select((s) => Utils.ConvertHexString(s.Id, "SaveDataOwnerIds")))
                {
                    Buffer.BlockCopy(BitConverter.GetBytes(id), 0, array, num, 8);
                    num += 8;
                }
            }

            return array;
        }
    }
}

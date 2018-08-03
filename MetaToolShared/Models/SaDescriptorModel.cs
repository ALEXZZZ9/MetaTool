using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AX9.MetaTool.Models
{
    [XmlRoot("SrvAccessControlDescriptor")]
    public class SaDescriptorModel
    {
        public SaDescriptorModel() { }
        public SaDescriptorModel(SaDataModel data)
        {
            Entries = data.Entries;
        }


        [XmlElement("Entry")]
        public List<SaEntry> Entries { get; set; } = new List<SaEntry>();

        [XmlIgnore]
        public int DescriptorSize => Entries.Sum((s) => s.BinarySize);


        public static SaDescriptorModel FromNpdmBytes(byte[] bytes)
        {
            if (bytes.Length <= 0) return null;

            SaDescriptorModel data = new SaDescriptorModel();

            int num = 0;
            while (num < bytes.Length)
            {
                byte mask = bytes[num];
                byte isServerMask = (1 << SaEntry.IsServerFlag);
                int size = mask + 1;
                bool isServer = false;

                if ((mask & isServerMask) != 0)
                {
                    isServer = true;
                    size -= isServerMask;
                }

                byte[] sEBytes = new byte[size];

                num += 1;
                Buffer.BlockCopy(bytes, num, sEBytes, 0, size);
                num += size;

                SaEntry sACEntry = new SaEntry
                {
                    Name = Encoding.UTF8.GetString(sEBytes),
                    IsServerValue = isServer
                };

                data.Entries.Add(sACEntry);
                //sACData.EntriesSize += sACEntry.BinarySize;
            }

            return data;
        }

        public byte[] ExportBinary()
        {
            if (DescriptorSize == 0) return null;

            byte[] array = new byte[DescriptorSize];
            int num = 0;

            foreach (SaEntry sacEntry in Entries)
            {
                Array.Copy(sacEntry.ExportBinary(), 0, array, num, sacEntry.BinarySize);
                num += sacEntry.BinarySize;
            }

            return array;
        }


        public bool HasWildCard(string name) => name[name.Length - 1] == '*';

        public void CheckCapabilities(DefaultModel @default)
        {
            if (Entries == null || Entries.Count == 0) return;

            foreach (SaEntry entries in Entries)
            {
                bool flag = false;

                foreach (SaEntry dEntries in @default.SrvAccessControlData.Entries)
                {
                    if (entries.IsServer == dEntries.IsServer)
                    {
                        if (HasWildCard(dEntries.Name))
                        {
                            if (entries.Name.IndexOf(dEntries.Name.Substring(0, dEntries.Name.Length - 1), StringComparison.Ordinal) == 0)
                            {
                                flag = true;
                                break;
                            }
                        }
                        else if (dEntries.Name == entries.Name)
                        {
                            flag = true;
                            break;
                        }
                    }
                }

                if (!flag) throw new ArgumentException($"{entries.Name} is not allowed, based on SrvAccessControlDescriptor.");
            }
        }
    }
}

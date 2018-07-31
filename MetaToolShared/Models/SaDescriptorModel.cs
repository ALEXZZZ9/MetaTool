using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AX9.MetaTool.Models
{
    [XmlRoot("SrvAccessControlDescriptor")]
    public class SaDescriptorModel
    {
        [XmlElement("Entry")]
        public List<SaEntry> Entries { get; set; } = new List<SaEntry>();

        [XmlIgnore]
        public int EntriesSize;

        public static SaDescriptorModel FromNpdmBytes(byte[] bytes)
        {
            if (bytes.Length <= 0) return null;

            SaDescriptorModel sACData = new SaDescriptorModel();

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

                sACData.Entries.Add(sACEntry);
                sACData.EntriesSize += sACEntry.BinarySize;
            }

            return sACData;
        }
    }
}

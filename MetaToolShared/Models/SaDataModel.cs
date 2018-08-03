using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace AX9.MetaTool.Models
{
    [XmlRoot("SrvAccessControlData")]
    public class SaDataModel
    {
        public SaDataModel() { }
        public SaDataModel(SaDescriptorModel descriptor)
        {
            Entries = descriptor.Entries;
        }


        [XmlElement("Entry")]
        public List<SaEntry> Entries { get; set; } = new List<SaEntry>();

        [XmlIgnore]
        public int EntriesSize => Entries.Sum((s) => s.BinarySize);
    }
}

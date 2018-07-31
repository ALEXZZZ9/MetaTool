using System.Collections.Generic;
using System.Xml.Serialization;

namespace AX9.MetaTool.Models
{
    [XmlRoot("FsAccessControlData")]
    public class FaDataModel
    {
        public FaDataModel() { }
        public FaDataModel(FaDescriptorModel descriptor)
        {
            FlagPresets = descriptor.FlagPresets;
            ContentOwnerIds = descriptor.ContentOwnerIds;
            SaveDataOwnerIds = descriptor.SaveDataOwnerIdsWithAccesibilities;
        }


        [XmlElement("FlagPresets")]
        public List<string> FlagPresets { get; set; } = new List<string>();

        [XmlElement("ContentOwnerIds")]
        public List<string> ContentOwnerIds { get; set; } = new List<string>();

        [XmlElement("SaveDataOwnerIds")]
        public List<SaveDataOwnerId> SaveDataOwnerIds { get; set; } = new List<SaveDataOwnerId>();
    }
}

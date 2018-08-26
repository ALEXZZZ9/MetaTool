using System;
using System.Xml;
using System.Xml.Serialization;
using AX9.MetaTool.Structs;

namespace AX9.MetaTool.Models
{
    [XmlRoot("Meta")]
    public class MetaModel : ICloneable
    {
        [XmlElement("Core")]
        public CoreModel Core { get; set; }

        [XmlAnyElement]
        public XmlElement[] ApplicationControlData { get; set; }


        public NpdmHeader Header;


        public static MetaModel FromXml(string filePath)
        {
            try
            {
                return Utils.XMLDeserialize<MetaModel>(filePath);
            }
            catch { }

            try
            {
                return Utils.XMLDeserialize<NintendoSdkMetaModel>(filePath);
            }
            catch (Exception)
            {
                throw new Exception($"File {filePath} is corrupted or is not a Meta file");
            }
        }

        public object Clone()
        {
            return new MetaModel
            {
                Core = (CoreModel)Core?.Clone(),
                ApplicationControlData = (XmlElement[])ApplicationControlData?.Clone(),
            };
        }
    }
}

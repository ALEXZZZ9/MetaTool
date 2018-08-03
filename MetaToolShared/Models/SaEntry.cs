using System;
using System.Text;
using System.Xml.Serialization;

namespace AX9.MetaTool.Models
{
    [XmlRoot("Entry")]
    public class SaEntry : ICloneable
    {
        [XmlIgnore]
        public bool IsServerValue
        {
            get => isServerValue;
            set
            {
                isServer = value.ToString();
                isServerValue = value;
            }
        }

        [XmlElement("IsServer", IsNullable = false)]
        public string IsServer
        {
            get => isServer;
            set
            {
                if (value == null) return;
                isServerValue = Utils.ConvertBoolString(value, "Entry/IsServer");
                isServer = value;
            }
        }

        [XmlElement("Name", IsNullable = false)]
        public string Name
        {
            get => name;
            set
            {
                if (value == null) return;

                if (value.Length > 8 || value.Length == 0) throw new ArgumentException("Entry/Name is invalid");
               
                name = value;
            }
        }

        [XmlIgnore]
        public int BinarySize => Name.Length + 1;


        public const int IsServerFlag = 7;

        private bool isServerValue;
        private string isServer;
        private string name;


        public byte[] ExportBinary()
        {
            byte b = (byte)(Name.Length - 1);
            b |= (byte)((IsServerValue ? 1 : 0) << IsServerFlag);
            byte[] array = new byte[BinarySize];
            array[0] = b;
            Array.Copy(Encoding.ASCII.GetBytes(Name), 0, array, 1, Name.Length);
            return array;
        }

        public object Clone()
        {
            return new SaEntry
            {
                IsServer = IsServer,
                Name = Name
            };
        }
    }
}

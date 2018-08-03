using System;
using System.Xml.Serialization;
using AX9.MetaTool.Enums;

namespace AX9.MetaTool.Models
{
    public class SaveDataOwnerId : ICloneable
    {
        [XmlIgnore]
        public SaveDataAccessibilityEnum AccessibilityValue
        {
            get => accessibilityValue;
            set
            {
                if (value == SaveDataAccessibilityEnum.Unknown) throw new ArgumentException("Accessibility is invalid");

                accessibility = value.ToString();
                accessibilityValue = value;
            }
        }

        [XmlElement("Accessibility")]
        public string Accessibility
        {
            get => accessibility;
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                
                accessibilityValue = (SaveDataAccessibilityEnum)Enum.Parse(typeof(SaveDataAccessibilityEnum), value);
                accessibility = value;
            }
        }

        [XmlElement("Id")]
        public string Id { get; set; }


        private SaveDataAccessibilityEnum accessibilityValue;
        private string accessibility;


        public object Clone()
        {
            return new SaveDataOwnerId
            {
                Accessibility = Accessibility,
                Id = Id,
            };
        }
    }
}

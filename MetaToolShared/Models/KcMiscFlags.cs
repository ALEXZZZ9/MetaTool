﻿using System;
using System.Xml.Serialization;

namespace AX9.MetaTool.Models
{
    [XmlRoot("MiscFlags")]
    public class KcMiscFlags : ICloneable
    {
        public KcMiscFlags()
        {
            EnableDebugValue = false;
            ForceDebugValue = false;
        }
        public KcMiscFlags(bool enableDebug, bool forceDebug)
        {
            EnableDebugValue = enableDebug;
            ForceDebugValue = forceDebug;
        }


        [XmlIgnore]
        public bool EnableDebugValue
        {
            get => enableDebugValue;
            set
            {
                enableDebug = value.ToString();
                enableDebugValue = value;
            }
        }

        [XmlElement("EnableDebug")]
        public string EnableDebug
        {
            get => enableDebug;
            set
            {
                if (value == null) return;
                
                enableDebugValue = Utils.ConvertBoolString(value, "MiscFlags/EnableDebug");
                enableDebug = value;
            }
        }

        [XmlIgnore]
        public bool ForceDebugValue
        {
            get => forceDebugValue;
            set
            {
                forceDebug = value.ToString();
                forceDebugValue = value;
            }
        }

        [XmlElement("ForceDebug")]
        public string ForceDebug
        {
            get => forceDebug;
            set
            {
                if (value == null) return;
                
                forceDebugValue = Utils.ConvertBoolString(value, "MiscFlags/ForceDebug");
                forceDebug = value;
            }
        }


        private bool enableDebugValue;
        private string enableDebug;
        private bool forceDebugValue;
        private string forceDebug;

        private readonly KcFlagModel capability = new KcFlagModel { EntryNumber = 16u };


        public uint CalcFlag()
        {
            capability.FieldValue = (EnableDebugValue ? 1u : 0u);
            capability.FieldValue |= (ForceDebugValue ? 1u : 0u) << 1;
            return capability.Flag;
        }

        public bool CheckSuccessToRead()
        {
            return EnableDebug != null || ForceDebug != null;
        }

        public object Clone()
        {
            return new KcMiscFlags
            {
                EnableDebug = EnableDebug,
                ForceDebug = ForceDebug
            };
        }
    }
}

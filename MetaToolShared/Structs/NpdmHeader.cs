using System;
using System.Runtime.InteropServices;
using System.Text;
using AX9.MetaTool.Models;

namespace AX9.MetaTool.Structs
{
    public struct NpdmHeader
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Magic;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Reserved1;

        public byte Flags;

        public byte UnusedData;

        public byte MainThreadPriority;

        public byte MainThreadCoreNumber;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Reserved2;

        public uint Version;

        public uint MainThreadStackSize;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] ProductCode;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        public byte[] Reserved3;

        public uint AciOffset;

        public uint AciSize;

        public uint AcidOffset;

        public uint AcidSize;


        public void Fill(MetaModel meta)
        {
            Magic = Encoding.ASCII.GetBytes("META");
            Reserved1 = new byte[8];
            Flags = (byte)(Flags | (meta.Core.Is64BitInstructionValue ? 1 : 0));
            Flags = (byte)(Flags | (byte)((byte)meta.Core.ProcessAddressSpaceValue << 1));
            UnusedData = 0;
            MainThreadPriority = meta.Core.MainThreadPriorityValue;
            MainThreadCoreNumber = meta.Core.MainThreadCoreNumberValue;
            Reserved2 = new byte[8];
            Version = meta.Core.VersionValue;
            MainThreadStackSize = meta.Core.MainThreadStackSizeValue;
            Name = new byte[16];
            if (!string.IsNullOrEmpty(meta.Core.Name))
            {
                Array.Copy(meta.Core.NameValue, Name, meta.Core.NameValue.Length);
            }
            ProductCode = new byte[16];
            if (!string.IsNullOrEmpty(meta.Core.ProductCode))
            {
                Array.Copy(meta.Core.ProductCodeValue, ProductCode, meta.Core.ProductCodeValue.Length);
            }
            Reserved3 = new byte[48];
        }
    }
}

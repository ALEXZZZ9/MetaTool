using System.Runtime.InteropServices;
using System.Text;
using AX9.MetaTool.Models;
using System;
using AX9.MetaTool.Enums;

namespace AX9.MetaTool.Structs
{
    public struct AcidHeader
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x100)]
        public byte[] Signature;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x100)]
        public byte[] Modulus;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x4)]
        public byte[] Magic;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x4)]
        public byte[] FieldSize;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x4)]
        public byte[] Reserved1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x4)]
        public byte[] Flags;

        public ulong ProgramIdMin;

        public ulong ProgramIdMax;

        public uint FacOffset;

        public uint FacSize;

        public uint SacOffset;

        public uint SacSize;

        public uint KcOffset;

        public uint KcSize;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x8)]
        public byte[] Padding;


        public void Fill(DescModel desc)
        {
            Signature = new byte[0x100];
            Modulus = new byte[0x100];
            if (desc.RSAKeyValue.Modulus != null)
            {
                Array.Copy(desc.RSAKeyValue.Modulus, Modulus, desc.RSAKeyValue.Modulus.Length);
            }
            Magic = Encoding.ASCII.GetBytes("ACID");
            FieldSize = new byte[0x4];
            Reserved1 = new byte[0x4];
            Flags = new byte[]
            {
                (byte)(desc.IsRetail ? 1 : 0), // Is retail
                0, 0, 0 // Unknown
            };
            switch (desc.KernelCapabilityDescriptor.MiscParams.ProgramTypeValue)
            {
                case ProgramTypeEnum.Applet:
                    Flags[2] = 0x0B;
                    Flags[3] = 0x01;
                    break;
                case ProgramTypeEnum.System:
                    Flags[2] = 0x0B;
                    Flags[3] = 0x10;
                    break;
            }
            ProgramIdMin = (string.IsNullOrEmpty(desc.ProgramIdMin)) ? 0x0100000000010000UL : desc.ProgramIdMinValue;
            ProgramIdMax = (string.IsNullOrEmpty(desc.ProgramIdMax)) ? 0x01FFFFFFFFFFFFFFUL : desc.ProgramIdMaxValue;
            Padding = new byte[0x8];
        }
    }
}

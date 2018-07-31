using System.Runtime.InteropServices;

namespace AX9.MetaTool.Structs
{
    public struct AcidHeader
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x100)]
        public byte[] Signature;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x100)]
        public byte[] Modulus;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Magic;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] Unknown;

        public ulong ProgramIdMin;

        public ulong ProgramIdMax;

        public uint FacOffset;

        public uint FacSize;

        public uint SacOffset;

        public uint SacSize;

        public uint KcOffset;

        public uint KcSize;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Reserved3;
    }
}

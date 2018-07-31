using System.Runtime.InteropServices;

namespace AX9.MetaTool.Structs
{
    public struct AciHeader
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Magic;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] Reserved1;

        public ulong ProgramId;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Reserved2;

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

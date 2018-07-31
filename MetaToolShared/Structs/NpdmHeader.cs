using System.Runtime.InteropServices;

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
    }
}

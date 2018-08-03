using System.Runtime.InteropServices;
using System.Text;

namespace AX9.MetaTool.Structs
{
    public struct AciHeader
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x4)]
        public byte[] Magic;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xC)]
        public byte[] Reserved1;

        public ulong ProgramId;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x8)]
        public byte[] Reserved2;

        public uint FacOffset;

        public uint FacSize;

        public uint SacOffset;

        public uint SacSize;

        public uint KcOffset;

        public uint KcSize;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x8)]
        public byte[] Padding;


        public void Fill()
        {
            Magic = Encoding.ASCII.GetBytes("ACI0");
            Reserved1 = new byte[0xC];
            Reserved2 = new byte[0x8];
            Padding = new byte[0x8];
        }
    }
}

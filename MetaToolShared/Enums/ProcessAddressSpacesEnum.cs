using System;

namespace AX9.MetaTool.Enums
{
    [Flags]
    public enum ProcessAddressSpacesEnum : byte
    {
        AddressSpace32Bit = 0,
        AddressSpace64BitOld = 1,
        AddressSpace32BitNoReserved = 2,
        AddressSpace64Bit = 3
    }
}

namespace AX9.MetaTool
{
    public class KernelCapabilityFlag
    {
        public uint FieldValue { get; set; }

        public uint EntryNumber { get; set; }

        public uint Flag => ((EntryNumber < 32u) ? ((1u << (int)EntryNumber) - 1u) : uint.MaxValue) | FieldValue << (int)(EntryNumber + 1u);
    }
}

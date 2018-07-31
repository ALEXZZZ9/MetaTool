using AX9.MetaTool.Structs;

namespace AX9.MetaTool
{
    public class AcidData
    {
        public AcidHeader Header = new AcidHeader();
        public byte[] ACID = new byte[0];


        public static AcidData FromNpdm(byte[] acid)
        {
            AcidData aCIDData = new AcidData
            {
                Header = acid.ToType<AcidHeader>(),
                ACID = acid
            };

            return aCIDData;
        }
    }
}

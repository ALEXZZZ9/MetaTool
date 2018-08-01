using AX9.MetaTool.Structs;

namespace AX9.MetaTool.Models
{
    public class AcidModel
    {
        public AcidHeader Header = new AcidHeader();
        public byte[] ACID = new byte[0];


        public static AcidModel FromNpdm(byte[] acid)
        {
            AcidModel aCidModel = new AcidModel
            {
                Header = acid.ToType<AcidHeader>(),
                ACID = acid
            };

            return aCidModel;
        }
    }
}

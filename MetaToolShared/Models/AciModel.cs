using System;
using AX9.MetaTool.Structs;

namespace AX9.MetaTool.Models
{
    public class AciModel
    {
        public AciHeader Header = new AciHeader();
        public FaDescriptorModel FaDescriptor;
        public SaDescriptorModel SaDescriptor;
        public KcDescriptorModel KcDescriptor;

        public static AciModel FromNpdm(byte[] aci)
        {
            AciModel aCiModel = new AciModel { Header = aci.ToType<AciHeader>() };

            if (aCiModel.Header.FacSize > 0)
            {
                byte[] facBytes = new byte[aCiModel.Header.FacSize];
                Buffer.BlockCopy(aci, (int)aCiModel.Header.FacOffset, facBytes, 0, (int)aCiModel.Header.FacSize);

                aCiModel.FaDescriptor = FaDescriptorModel.FromNpdmBytes(facBytes);
            }

            if (aCiModel.Header.SacSize > 0)
            {
                byte[] sacBytes = new byte[aCiModel.Header.SacSize];
                Buffer.BlockCopy(aci, (int)aCiModel.Header.SacOffset, sacBytes, 0, (int)aCiModel.Header.SacSize);

                aCiModel.SaDescriptor = SaDescriptorModel.FromNpdmBytes(sacBytes);
            }

            if (aCiModel.Header.KcSize > 0)
            {
                byte[] kcBytes = new byte[aCiModel.Header.KcSize];
                Buffer.BlockCopy(aci, (int)aCiModel.Header.KcOffset, kcBytes, 0, (int)aCiModel.Header.KcSize);

                aCiModel.KcDescriptor = KcDescriptorModel.FromNpdmBytes(kcBytes);
            }

            return aCiModel;
        }
    }
}

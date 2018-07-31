using System;
using AX9.MetaTool.Models;
using AX9.MetaTool.Structs;

namespace AX9.MetaTool
{
    public class AciData
    {
        public AciHeader Header = new AciHeader();
        public FaDescriptorModel FaDescriptor;
        public SaDescriptorModel SaDescriptor;
        public KcDescriptorModel KcDescriptor;

        public static AciData FromNpdm(byte[] aci)
        {
            AciData aCIData = new AciData { Header = aci.ToType<AciHeader>() };

            if (aCIData.Header.FacSize > 0)
            {
                byte[] facBytes = new byte[aCIData.Header.FacSize];
                Buffer.BlockCopy(aci, (int)aCIData.Header.FacOffset, facBytes, 0, (int)aCIData.Header.FacSize);

                aCIData.FaDescriptor = FaDescriptorModel.FromNpdmBytes(facBytes);
            }

            if (aCIData.Header.SacSize > 0)
            {
                byte[] sacBytes = new byte[aCIData.Header.SacSize];
                Buffer.BlockCopy(aci, (int)aCIData.Header.SacOffset, sacBytes, 0, (int)aCIData.Header.SacSize);

                aCIData.SaDescriptor = SaDescriptorModel.FromNpdmBytes(sacBytes);
            }

            if (aCIData.Header.KcSize > 0)
            {
                byte[] kcBytes = new byte[aCIData.Header.KcSize];
                Buffer.BlockCopy(aci, (int)aCIData.Header.KcOffset, kcBytes, 0, (int)aCIData.Header.KcSize);

                aCIData.KcDescriptor = KcDescriptorModel.FromNpdmBytes(kcBytes);
            }

            return aCIData;
        }
    }
}

using System.IO;
using System.Threading.Tasks;
using AX9.MetaTool.Structs;

namespace AX9.MetaTool
{
    public class Npdm
    {
        public NpdmHeader Header = new NpdmHeader();
        public AciData ACIData = new AciData();
        public AcidData ACIDData = new AcidData();

        public static async Task<Npdm> FromNpdmFile(string filePatch)
        {
            using (Stream stream = File.OpenRead(filePatch))
            {
                Npdm npdm = new Npdm { Header = stream.ToType<NpdmHeader>() };

                byte[] acidBytes = new byte[npdm.Header.AcidSize];
                stream.Seek(npdm.Header.AcidOffset, SeekOrigin.Begin);
                await stream.ReadAsync(acidBytes, 0, (int)npdm.Header.AcidSize);

                byte[] aciBytes = new byte[npdm.Header.AciSize];
                stream.Seek(npdm.Header.AciOffset, SeekOrigin.Begin);
                await stream.ReadAsync(aciBytes, 0, (int)npdm.Header.AciSize);

                stream.Close();

                npdm.ACIData = AciData.FromNpdm(aciBytes);
                npdm.ACIDData = AcidData.FromNpdm(acidBytes);

                return npdm;
            }
        }
    }
}

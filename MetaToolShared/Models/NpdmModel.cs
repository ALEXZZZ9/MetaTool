using System.IO;
using System.Threading.Tasks;
using AX9.MetaTool.Structs;

namespace AX9.MetaTool.Models
{
    public class NpdmModel
    {
        public NpdmHeader Header = new NpdmHeader();
        public AciModel AciModel = new AciModel();
        public AcidModel AcidModel = new AcidModel();

        public static async Task<NpdmModel> FromNpdmFile(string filePatch)
        {
            using (Stream stream = File.OpenRead(filePatch))
            {
                NpdmModel npdm = new NpdmModel { Header = stream.ToType<NpdmHeader>() };

                byte[] acidBytes = new byte[npdm.Header.AcidSize];
                stream.Seek(npdm.Header.AcidOffset, SeekOrigin.Begin);
                await stream.ReadAsync(acidBytes, 0, (int)npdm.Header.AcidSize);

                byte[] aciBytes = new byte[npdm.Header.AciSize];
                stream.Seek(npdm.Header.AciOffset, SeekOrigin.Begin);
                await stream.ReadAsync(aciBytes, 0, (int)npdm.Header.AciSize);

                stream.Close();

                npdm.AciModel = AciModel.FromNpdm(aciBytes);
                npdm.AcidModel = AcidModel.FromNpdm(acidBytes);

                return npdm;
            }
        }
    }
}

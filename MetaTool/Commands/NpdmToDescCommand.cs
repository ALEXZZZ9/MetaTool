using System;
using System.IO;
using AX9.MetaTool.ExConsole;
using AX9.MetaTool.Models;

namespace AX9.MetaTool.Commands
{
    [Command("NpdmToDesc", "Convert npdm to desc", "NpdmToDesc <npdmFile> <outputDescFile>", new string[] { "n2d" })]
    public class NpdmToDescCommand : Command
    {
        public override string Execute(params string[] args)
        {
            FileInfo npdmFile = new FileInfo(GetValue<string>(args, 0, "npdmFilePath"));
            string descFilePath = GetValue<string>(args, 1);
            FileInfo descFile = new FileInfo(string.IsNullOrEmpty(descFilePath) ? Path.ChangeExtension(npdmFile.FullName, ".desc") : descFilePath);

            if (!npdmFile.Exists) throw new CommandException("Npdm Not Found");

            if (!descFile.Exists && Directory.Exists(descFile.FullName) && descFile.Attributes.HasFlag(FileAttributes.Directory))
            {
                descFile = new FileInfo(Path.Combine(descFile.FullName, Path.ChangeExtension(npdmFile.Name, ".desc")));
            }

            try
            {
                DescModel desc = DescModel.FromNpdm(npdmFile.FullName).Result;

                File.WriteAllText(descFile.FullName, desc.XMLSerialize());
            }
            catch (Exception ex)
            {
                throw new CommandException(ex.Message);
            }

            return $"Done {descFile.FullName}";
        }
    }
}

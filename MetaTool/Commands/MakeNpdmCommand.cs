using System;
using System.IO;
using AX9.MetaTool.ExConsole;
using AX9.MetaTool.Models;

namespace AX9.MetaTool.Commands
{
    [Command("MakeNpdm", "Make npdm from desc and meta", "MakeNpdm <descFile> <metaFile> <outputNpdmFile>")]
    public class MakeNpdmCommand : Command
    {
        public override string Execute(params string[] args)
        {
            FileInfo descFile = new FileInfo(GetValue<string>(args, 0, "descFile"));
            FileInfo metaFile = new FileInfo(GetValue<string>(args, 1, "metaFile"));
            string npdmFilePath = GetValue<string>(args, 2);
            FileInfo npdmFile = new FileInfo(string.IsNullOrEmpty(npdmFilePath) ? Path.ChangeExtension(descFile.FullName, ".npdm") : npdmFilePath);
            bool forceGenerateAcid = false; // GetValue<bool>(args, 3);

            if (!descFile.Exists) throw new CommandException("Desc Not Found");
            if (!metaFile.Exists) throw new CommandException("Meta Not Found");

            if (!npdmFile.Exists && Directory.Exists(npdmFile.FullName) && npdmFile.Attributes.HasFlag(FileAttributes.Directory))
            {
                npdmFile = new FileInfo(Path.Combine(npdmFile.FullName, Path.ChangeExtension(descFile.Name, ".npdm")));
            }

            try
            {
                DescModel desc = DescModel.FromXml(descFile.FullName);
                MetaModel meta = MetaModel.FromXml(metaFile.FullName);
                NpdmGenerator.CreateNpdm(desc, meta, forceGenerateAcid, npdmFile.FullName);
            }
            catch (Exception ex)
            {
                throw new CommandException(ex.Message);
            }

            return $"Done {npdmFile.FullName}";
        }
    }
}

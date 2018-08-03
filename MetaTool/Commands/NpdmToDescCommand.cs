using AX9.MetaTool.ExConsole;
using System;
using System.IO;
using AX9.MetaTool.Models;

namespace AX9.MetaTool.Commands
{
    [Command("NpdmToDesc", "Convert npdm to desc", "NpdmToDesc <npdmFile> <outputDescFile>", new string[] { "n2d" })]
    public class NpdmToDescCommand : Command
    {
        public override string Execute(params string[] args)
        {
            string npdmFilePath = GetValue<string>(args, 0, "npdmFilePath");
            string descFilePath = GetValue<string>(args, 1);

            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            FileInfo inputFile = new FileInfo((Path.IsPathRooted(npdmFilePath)) ? npdmFilePath : Path.Combine(appPath, npdmFilePath));
            if (!inputFile.Exists) throw new CommandException("npdm File Not Found");

            if (string.IsNullOrEmpty(descFilePath))
            {
                descFilePath = Path.ChangeExtension(inputFile.FullName, ".desc");
            }
            else
            {
                try
                {
                    if (File.GetAttributes(descFilePath).HasFlag(FileAttributes.Directory)) descFilePath = Path.Combine(descFilePath, Path.ChangeExtension(inputFile.Name, ".desc"));
                } catch { }

                if (!Path.IsPathRooted(descFilePath)) descFilePath = Path.Combine(appPath, descFilePath);
            }

            if (inputFile.Extension == ".npdm")
            {
                DescModel desc = DescModel.FromNpdm(inputFile.FullName).Result;

                try
                {
                    File.WriteAllText(descFilePath, desc.XMLSerialize());
                }
                catch (Exception ex)
                {
                    throw new CommandException(ex.Message);
                }
            }
            else
            {
                throw new NotImplementedException("The file format is not yet supported");
            }

            return $"Done {descFilePath}";
        }
    }
}

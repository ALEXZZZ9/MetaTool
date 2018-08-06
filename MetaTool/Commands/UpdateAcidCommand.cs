using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AX9.MetaTool.ExConsole;
using AX9.MetaTool.Models;

namespace AX9.MetaTool.Commands
{
    [Command("UpdateAcid", "Update acid in desc", "UpdateAcid <descFile> optional:<metaFile>")]
    public class UpdateAcidCommand : Command
    {
        public override string Execute(params string[] args)
        {
            FileInfo descFile = new FileInfo(GetValue<string>(args, 0, "descFile"));
            string metaFilePath = GetValue<string>(args, 1);

            if (!descFile.Exists) throw new CommandException("Desc Not Found");

            try
            {
                DescModel desc = DescModel.FromXml(descFile.FullName);
                MetaModel meta = (File.Exists(metaFilePath)) ? MetaModel.FromXml(metaFilePath) : null;

                desc.Acid = Convert.ToBase64String(NpdmGenerator.GetAcidBytes(desc, meta));

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

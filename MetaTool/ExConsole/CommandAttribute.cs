using System;

namespace AX9.MetaTool.ExConsole
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CommandAttribute : Attribute
    {
        public CommandAttribute(string name, string description, string usage, string[] alias = null)
        {
            Name = name;
            Description = (string.IsNullOrEmpty(description.Trim()) ? "No description provided" : description);
            Usage = (string.IsNullOrEmpty(usage.Trim()) ? "No usage information provided" : usage);
            if (alias != null) Alias = alias;
        }


        public string Name;
        public string Description;
        public string Usage;
        public Command Command;
        public string[] Alias = new string[0];
    }
}

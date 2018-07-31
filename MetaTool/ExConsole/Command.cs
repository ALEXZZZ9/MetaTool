using System;
using System.Threading.Tasks;

namespace AX9.MetaTool.ExConsole
{
    public class Command
    {
        public Command() { }
        public Command(string name, string description, string usage, CommandCallback callback, string[] alias = null)
        {
            Name = name;
            Description = (string.IsNullOrEmpty(description.Trim()) ? "No description provided" : description);
            Usage = (string.IsNullOrEmpty(usage.Trim()) ? "No usage information provided" : usage);
            Callback = (callback ?? Execute);
            if (alias != null) Alias = alias;
        }


        public string Name;
        public string Description;
        public string Usage;
        public CommandCallback Callback;
        public string[] Alias = new string[0];


        public virtual string Execute(params string[] args)
        {
            throw new NotImplementedException();
        }

        public T GetValue<T>(string[] args, int index, string name = null)
        {
            bool isDefined = args.Length >= index + 1;
            if (!string.IsNullOrEmpty(name) && !isDefined) throw new ArgumentNullException(name);

            return isDefined ? (T) Convert.ChangeType(args[index], typeof(T)) : default(T);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AX9.MetaTool.ExConsole
{
    public static class ConsoleDatabase
    {
        public static IEnumerable<Command> Commands
        {
            get { return Database.OrderBy(kv => kv.Key).Select(kv => kv.Value); }
        }

        private static readonly Dictionary<string, Command> Database = new Dictionary<string, Command>(StringComparer.OrdinalIgnoreCase);


        public static void RegisterCommand(string commandName, CommandCallback callback, string description = null, string usage = null)
        {
            RegisterCommand(commandName, description, usage, callback);
        }
        public static void RegisterCommand(string commandName, string description, string usage, CommandCallback callback)
        {
            RegisterCommand(new Command(commandName, description, usage, callback));
        }
        public static void RegisterCommand(Command command)
        {
            Database[command.Name] = command;
        }

        private static string[] ParsArgs(string command)
        {
            if (string.IsNullOrEmpty(command.Replace(" ", ""))) return new string[] { };

            if (command.Contains('"'))
            {
                List<string> args = new List<string>();
                string[] parts = command.Split('"');
                if (!string.IsNullOrEmpty(parts[0])) args.AddRange(ParsArgs(parts[0]));

                int index = 0;
                for (int i = (!string.IsNullOrEmpty(parts[0])) ? 1 : 0; i < parts.Length; i++)
                {
                    if (index == 0)
                    {
                        args.Add(parts[i]);
                        index++;
                    }
                    else if (index == 1)
                    {
                        index--;
                        args.AddRange(ParsArgs(parts[i]));
                    }
                }
                return args.ToArray();
            }
            return command.Trim().Split(' ') /*.Where(s => s != " ")*/.ToArray();
        }

        public static string ExecuteCommand(string commandName)
        {
            string[] parts = commandName.Split(' ');
            string[] args = (parts.Length > 1) ? ParsArgs(commandName.Remove(0, parts[0].Length)) : new string[] { };

            return ExecuteCommand(parts[0], args);
        }
        public static string ExecuteCommand(string commandName, params string[] args)
        {
            //try
            //{
                Command retrievedCommand = GetCommand(commandName);
                return retrievedCommand.Callback(args);
            //}
            //catch (CommandException e)
            //{
            //    return e.Message;
            //}
        }

        public static List<Command> FindCommands(string commandName)
        {
            if (string.IsNullOrEmpty(commandName)) return null;

            return Database.Values.Where(com => (com.Name.ToUpper().IndexOf(commandName.ToUpper(), StringComparison.Ordinal) > -1) || com.Alias.Any((alias) => alias.ToUpper().IndexOf(commandName.ToUpper(), StringComparison.Ordinal) > -1)).ToList();
        }
        public static Command FindCommand(string commandName)
        {
            if (string.IsNullOrEmpty(commandName)) return null;

            return  Database.Values.FirstOrDefault(com => (com.Name.ToUpper().IndexOf(commandName.ToUpper(), StringComparison.Ordinal) > -1) || com.Alias.Any((alias) => alias.ToUpper().IndexOf(commandName.ToUpper(), StringComparison.Ordinal) > -1));
        }

        public static bool TryGetCommand(string commandName, out Command result)
        {
            try
            {
                result = GetCommand(commandName);
                return true;
            }
            catch (CommandException)
            {
                result = default(Command);
                return false;
            }
        }

        public static Command GetCommand(string commandName)
        {
            if (HasCommand(commandName, out Command command))
            {
                return command;
            }
            else
            {
                throw new CommandException($"Command {commandName} not found.", commandName);
            }
        }

        public static bool HasCommand(string commandName, out Command command)
        {
            command = Database.Values.FirstOrDefault((com) => string.Equals(com.Name, commandName, StringComparison.CurrentCultureIgnoreCase) || com.Alias.Any((alias) => string.Equals(alias, commandName, StringComparison.CurrentCultureIgnoreCase)));

            return command != null;
        }
        public static bool HasCommand(string commandName)
        {
            return Database.ContainsKey(commandName) || Database.Values.Any((com) => com.Alias.Any((alias) => string.Equals(alias, commandName, StringComparison.CurrentCultureIgnoreCase)));
        }

        public static List<Command> FindCommands()
        {
            List<Command> commands = new List<Command>();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    foreach (CommandAttribute commandInfo in type.GetCustomAttributes(typeof(CommandAttribute), false))
                    {
                        Command command = (Activator.CreateInstance(type) as Command);
                        if (command == null) continue;

                        command.Name = commandInfo.Name;
                        command.Description = commandInfo.Description;
                        command.Usage = commandInfo.Usage;
                        command.Callback = command.Execute;
                        command.Alias = commandInfo.Alias;

                        commands.Add(command);
                    }
                }
            }

            commands.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
            return commands;
        }

        public static void LoadCommands()
        {
            foreach (var command in FindCommands()) RegisterCommand(command);
        }
    }
}
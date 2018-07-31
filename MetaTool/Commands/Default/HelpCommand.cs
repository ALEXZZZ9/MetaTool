using AX9.MetaTool.ExConsole;

namespace AX9.MetaTool.Commands.Default
{
    [Command("Help", "Display the list of available commands or details about a specific command", "Help {command}", new []{ "?" })]
    public class HelpCommand : Command
    {
        public override string Execute(params string[] args)
        {
            string commandName = GetValue<string>(args, 0);
            string list = "Available Commands\n";

            if (!string.IsNullOrEmpty(commandName)) return DisplayCommandDetails(commandName);

            foreach (Command command in ConsoleDatabase.Commands)
            {
                list += $"{command.Name}{((command.Alias.Length > 0) ? $", {string.Join(", ", command.Alias)}" : "")} - {command.Description}\n";
            }

            list += "To display details about a specific command, type 'HELP' followed by the command name";

            return list;
        }

        private string DisplayCommandDetails(string commandName)
        {
            try
            {
                Command command = ConsoleDatabase.GetCommand(commandName);
                return $"{command.Name} Command\n{((command.Alias.Length > 0) ? $"Aliases: {string.Join(", ", command.Alias)}\n" : "")}Description: {command.Description}\nUsage: {command.Usage}";
            }
            catch (CommandException exception)
            {
                return $"Cannot find help information about {exception.Command}. Are you sure it is a valid command?";
            }
        }
    }
}
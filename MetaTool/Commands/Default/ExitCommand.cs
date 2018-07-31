using System;
using AX9.MetaTool.ExConsole;

namespace AX9.MetaTool.Commands.Default
{
    [Command("Exit", "Close the app", "Exit", new[] { "Quit", "Close" })]
    public class ExitCommand : Command
    {
        public override string Execute(params string[] args)
        {
            Environment.Exit(0);
            return null;
        }
    }
}
using System;
using System.Linq;
using AX9.MetaTool.ExConsole;
using AX9.MetaTool.ExConsole.Helpers;

namespace AX9.MetaTool
{
    public class Program
    {
        private static ConsoleColor ForegroundColor;

        public static void Main(string[] args)
        {
            Console.Title = "MetaTool By ALEXZZZ9";
            ForegroundColor = Console.ForegroundColor;

            ConsoleDatabase.LoadCommands();


            if (args.Length > 0)
            {
                ConsoleHelpers.WriteLine(ConsoleDatabase.ExecuteCommand(args[0], ((args.Length > 1) ? args.Skip(1).ToArray() : new string[0])), true);
                return;
            }

            ConsoleHelpers.WriteLine(ConsoleDatabase.ExecuteCommand("help"), true);

            while (true)
            {
                var consoleInput = ConsoleHelpers.ReadLine();
                if (string.IsNullOrWhiteSpace(consoleInput)) continue;

                try
                {
                    ConsoleHelpers.WriteLine(ConsoleDatabase.ExecuteCommand(consoleInput), true);
                }
                catch (Exception ex) when (ex is ArgumentException || ex is CommandException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    ConsoleHelpers.WriteLine($"{ex.Message}", true, 4);
                    Console.ForegroundColor = ForegroundColor;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    ConsoleHelpers.WriteLine($"{ex.Message}\n{ex.StackTrace}", true, 4);
                    Console.ForegroundColor = ForegroundColor;
                }
            }
        }
    }
}

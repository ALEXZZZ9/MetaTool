using System;
using System.Linq;
using System.Text;

namespace AX9.MetaTool.ExConsole.Helpers
{
    public static class ConsoleHelpers
    {
        public static string ReadPrompt = "> ";


        public static string ReadLine(string promptMessage = null)
        {
            Console.Write(ReadPrompt + promptMessage);
            return Console.ReadLine();
        }
        public static string ReadPassword(string message)
        {
            StringBuilder result = new StringBuilder();

            Console.Write(message);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            while (keyInfo.Key != ConsoleKey.Enter)
            {
                if (Char.IsLetterOrDigit(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    result.Append(keyInfo.KeyChar);
                }
                else if (result.Length > 0 && keyInfo.Key == ConsoleKey.Backspace)
                {
                    EraseInput(1);
                    result.Remove(result.Length - 1, 1);
                }

                keyInfo = Console.ReadKey(true);
            }
            Console.WriteLine();

            return result.ToString();
        }

        public static void WriteLine(string message, bool indent = false, int indentCount = 2)
        {
            if (!string.IsNullOrEmpty(message)) Console.WriteLine(indent ? message.Indent(indentCount, true) : message);
        }


        public static string Indent(this string text, int count, bool multiline)
        {
            if (multiline)
            {
                string[] lines = text.Split('\n');

                return lines.Aggregate("", (current, line) => current + $"{line.Indent(count)}" + ((lines[lines.Length - 1] == line) ? "" : "\n"));
            }

            return text.Indent(count);
        }
        public static string Indent(this string text, int count)
        {
            return "".PadLeft(count) + text;
        }

        private static void EraseInput(int lengthToErase)
        {
            for (int i = 0; i < lengthToErase; i++)
            {
                Console.Write("\b \b");
            }
        }
    }
}
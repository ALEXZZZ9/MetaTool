using System;
using System.Runtime.Serialization;

namespace AX9.MetaTool.ExConsole
{
    [Serializable]
    public class CommandException : Exception
    {
        public CommandException() { }
        public CommandException(string message) : base(message) { }
        public CommandException(string message, string command) : base(message)
        {
            Command = command;
        }
        public CommandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Command = info.GetString("command");
        }


        public string Command { get; }


        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("command", Command);
        }
    }
}
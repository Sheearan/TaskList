using System;
using TaskManager.UserInterface;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLineInterface cli = new CommandLineInterface();
            cli.Run();
        }
    }
}

﻿using System;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInput.PrintDirections();
            UserInput input;
            TaskListFile file = new TaskListFile();
            TaskList list = new TaskList();
            do
            {
                input = new UserInput(Console.ReadLine());
                switch (input.Action)
                {
                    case UserActions.Create:
                        list.AddTask(input.Arguments);
                        break;
                    case UserActions.Display:
                        list.Display();
                        break;
                    case UserActions.Load:
                        list = file.Load(input.Arguments);
                        break;
                    case UserActions.Save:
                        file.Save(input.Arguments, list);
                        break;
                    case UserActions.Unknown:
                        UserInput.PrintDirections();
                        break;
                }
            } while (!(input.Action == UserActions.Exit));
            file.Save(null, list);
        }
    }
}
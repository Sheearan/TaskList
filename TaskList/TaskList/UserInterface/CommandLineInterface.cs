using System;
using System.Collections.Generic;
using TaskManager.DataStorage;

namespace TaskManager.UserInterface
{
    public class CommandLineInterface
    {
        protected TaskListFile file = new TaskListFile();
        protected ITaskList list = new TaskList();
        protected UserInputParser input = new UserInputParser();

        public void Run()
        {
            PrintDirections();
            do
            {
                input.GetNextAction();
                PerformAction(input);
            } while (!(input.Action == UserActions.Exit));
//            file.Save(null, list);
        }

        protected void PerformAction(UserInputParser input)
        {
            switch (input.Action)
            {
                case UserActions.Complete:
//                    list.CompleteTask(input.Arguments);
                    break;
                case UserActions.Create:
 //                   list.AddTask(input.Arguments);
                    break;
                case UserActions.Display:
                    Display();
                    break;
                case UserActions.Load:
                    list = file.Load(input.Arguments);
                    break;
                case UserActions.Save:
//                    file.Save(input.Arguments, list);
                    break;
                case UserActions.Unknown:
                    PrintDirections();
                    break;
            }
        }

        private void PrintDirections()
        {
            Console.WriteLine("Use one of the following commands:");
            Console.WriteLine("Create <task name> - Creates a new task");
            Console.WriteLine("Complete <task ID number> - Completes a task");
            Console.WriteLine("Display - Displays current list of tasks");
            Console.WriteLine("Load <file path> - Loads task list from file.");
            Console.WriteLine("Save <file path> - Saves task list to file");
            Console.WriteLine("Save - Saves task list to the file it was loaded from or last saved to.");
            Console.WriteLine("Exit - Saves task list (assuming there's a file to save to) and exits the program");
        }

        private void Display()
        {
            TaskFilter filter = DetermineDisplayFilter();
            List<Task> tasksToDisplay = list.GetTasksToDisplay(filter);
            foreach(Task task in tasksToDisplay)
            {
                Console.WriteLine(string.Format("{0} {1}", task.TaskId, task.Title));
            }
        }

        private TaskFilter DetermineDisplayFilter()
        {
            Console.WriteLine("Please select which tasks you'd like to display by entering the corresponding number:");
            return input.GetUserSelectionFromEnum<TaskFilter>(TaskFilter.All);
        }
    }
}

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
        protected bool userNeedsDirections = true;

        public void Run()
        {
            UserActions action;
            do
            {
                action = DetermineAction();
                PerformAction(action);
            } while (!(action == UserActions.Exit));
            //            file.Save(null, list);
        }

        protected void PerformAction(UserActions action)
        {
            switch (action)
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
                    //list = file.Load(input.Arguments);
                    break;
                case UserActions.Save:
                    //                    file.Save(input.Arguments, list);
                    break;
                case UserActions.Help:
                    userNeedsDirections = true;
                    break;
            }
        }

        private void Display()
        {
            TaskFilter filter = DetermineDisplayFilter();
            List<Task> tasksToDisplay = list.GetTasksToDisplay(filter);
            foreach (Task task in tasksToDisplay)
            {
                Console.WriteLine(string.Format("{0} {1}", task.TaskId, task.Title));
            }
        }

        protected UserActions DetermineAction()
        {
            Console.WriteLine("Please select which action you'd like to take by entering the corresponding number:");
            UserActions selectedAction = input.GetUserSelectionFromEnum<UserActions>(UserActions.Help, userNeedsDirections);
            userNeedsDirections = false;
            return selectedAction;
        }

        private TaskFilter DetermineDisplayFilter()
        {
            Console.WriteLine("Please select which tasks you'd like to display by entering the corresponding number:");
            return input.GetUserSelectionFromEnum<TaskFilter>(TaskFilter.All, true);
        }
    }
}

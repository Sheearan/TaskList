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

            file.Save(list, input);
        }

        protected UserActions DetermineAction()
        {
            Console.WriteLine("Please select which action you'd like to take by entering the corresponding number:");
            UserActions selectedAction = input.GetUserSelectionFromEnum<UserActions>(UserActions.Help, userNeedsDirections);
            userNeedsDirections = false;
            return selectedAction;
        }

        protected void PerformAction(UserActions action)
        {
            switch (action)
            {
                case UserActions.Help:
                    userNeedsDirections = true;
                    break;
                case UserActions.Create:
                    CreateTask();
                    break;
                case UserActions.Display:
                    Display();
                    break;
                case UserActions.Complete:
                    CompleteTask();
                    break;
                case UserActions.Load:
                    //list = file.Load(input.Arguments);
                    break;
                case UserActions.Save:
                    file.Save(list, input);
                    break;
            }
        }

        private void CreateTask()
        {
            Console.WriteLine("Please enter the name of your new task:");
            string taskName = input.GetString();
            Task newTask = list.AddTask(taskName);
            Console.WriteLine(string.Format("New task created: {0} {1}", newTask.TaskId, newTask.Title));
        }

        private void Display()
        {
            TaskDisplayFilter filter = DetermineDisplayFilter();
            List<Task> tasksToDisplay = list.GetTasksToDisplay(filter);
            foreach (Task task in tasksToDisplay)
            {
                Console.WriteLine(string.Format("{0} {1}", task.TaskId, task.Title));
            }
        }

        private TaskDisplayFilter DetermineDisplayFilter()
        {
            Console.WriteLine("Please select which tasks you'd like to display by entering the corresponding number:");
            return input.GetUserSelectionFromEnum<TaskDisplayFilter>(TaskDisplayFilter.All, true);
        }

        private void CompleteTask()
        {
            Console.WriteLine("Please enter the ID of the task to complete:");
            int taskId = input.GetInt();
            if (taskId < 0)
            {
                return;
            }

            Task completedTask = list.CompleteTask(taskId);
            if (completedTask != null)
            {
                Console.WriteLine(string.Format("Task Completed: {0} {1}", completedTask.TaskId, completedTask.Title));
            }
            else
            {
                Console.WriteLine(string.Format("Task ID {0} was not found.", taskId));
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace TaskManager
{
    [Serializable]
    public class TaskList
    {
        private List<Task> _taskList;
        int _nextTaskId;

        public int Count
        {
            get
            {
                return _taskList.Count;
            }
        }

        public TaskList()
        {
            _taskList = new List<Task>();
        }

        internal TaskList(List<Task> taskList)
        {
            _taskList = taskList;
        }

        public Task AddTask(string taskTitle)
        {
            Task taskToAdd = new Task(taskTitle, _nextTaskId);
            _taskList.Add(taskToAdd);
            _nextTaskId++;
            return taskToAdd;
        }

        public void Display()
        {
            foreach (Task task in _taskList)
            {
                if (task.CompletionDate == null)
                {
                    Console.WriteLine(string.Format("{0} {1}", task.TaskId, task.Title));
                }
            }
        }

        public void CompleteTask(string taskIdAsString)
        {
            try
            {
                Task taskToComplete = FindTaskById(taskIdAsString);
                if (taskToComplete == null)
                {
                    Console.WriteLine(string.Format("Could not find task {0}.", taskIdAsString));
                    return;
                }

                taskToComplete.CompletionDate = DateTime.Today;
            }
            catch
            {
                Console.WriteLine(string.Format("{0} is not a valid task ID. Task IDs must be positive integers.", taskIdAsString));
            }
        }

        private Task FindTaskById(string taskIdAsString)
        {
            int taskId;
            if (!int.TryParse(taskIdAsString, out taskId))
            {
                throw new ArgumentException();
            }

            Task desiredTask = _taskList.Find(task => task.TaskId == taskId);
            return desiredTask;
        }
    }
}

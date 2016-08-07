using System;
using System.Collections.Generic;

namespace TaskManager
{
    [Serializable]
    public class TaskList : ITaskList
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

        private Task FindTaskById(int taskId)
        {
            Task desiredTask = _taskList.Find(task => task.TaskId == taskId);
            return desiredTask;
        }

        public List<Task> GetTasksToDisplay(TaskDisplayFilter filter)
        {
            switch (filter)
            {
                case TaskDisplayFilter.Incomplete:
                    List<Task> incompleteTasks = new List<Task>();
                    foreach (Task t in _taskList)
                    {
                        if (!t.CompletionDate.HasValue)
                        {
                            incompleteTasks.Add(t);
                        }
                    }
                    return incompleteTasks;
                default:
                    return _taskList;
            }
        }

        public Task CompleteTask(int taskId)
        {
            Task taskToComplete = FindTaskById(taskId);
            if (taskToComplete == null)
            {
                Console.WriteLine(string.Format("Could not find task {0}.", taskId));
                return new Task("", taskId);
            }

            taskToComplete.CompletionDate = DateTime.Today;
            return taskToComplete;
        }
    }
}

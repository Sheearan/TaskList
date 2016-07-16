using System;
using System.Collections.Generic;

namespace TaskManager
{
    [Serializable]
    public class TaskList
    {
        private List<Task> _taskList;

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

        public void AddTask(string taskTitle)
        {
            _taskList.Add(new Task(taskTitle));
        }

        public void Display()
        {
            foreach (Task task in _taskList)
            {
                Console.WriteLine(task.Title);
            }
        }
    }
}

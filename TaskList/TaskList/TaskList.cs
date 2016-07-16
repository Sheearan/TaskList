using System;
using System.Collections.Generic;

namespace TaskManager
{
    [Serializable]
    public class TaskList
    {
        private List<Task> _taskList;
        int _maxTaskId;

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

        public int AddTask(string taskTitle)
        {
            int thisTaskId = _maxTaskId;
            _taskList.Add(new Task(taskTitle, thisTaskId));
            _maxTaskId++;
            return thisTaskId;
        }

        public void Display()
        {
            foreach (Task task in _taskList)
            {
                Console.WriteLine(string.Format("{0} {1}", task.TaskId, task.Title));
            }
        }
    }
}

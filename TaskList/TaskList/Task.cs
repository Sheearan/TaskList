using System;

namespace TaskManager
{
    [Serializable]
    internal class Task
    {
        public string Title { get; set; }

        public Task(string taskTitle)
        {
            Title = taskTitle;
        }
    }
}

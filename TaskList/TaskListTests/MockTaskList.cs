using System.Collections.Generic;
using TaskManager;

namespace TaskListTests
{
    class MockTaskList : ITaskList
    {
        public List<Task> GetTasksToDisplay(TaskDisplayFilter filter)
        {
            List<Task> tasksToDisplay = new List<Task>();
            tasksToDisplay.Add(new Task(string.Format("{0}1", filter.ToString()), 0));
            tasksToDisplay.Add(new Task(string.Format("{0}2", filter.ToString()), 1));
            return tasksToDisplay;
        }
    }
}

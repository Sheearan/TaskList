﻿using System.Collections.Generic;
using TaskManager;

namespace TaskListTests
{
    class MockTaskList : ITaskList
    {
        public Task AddTask(string taskName)
        {
            return new Task(taskName, 0);
        }

        public List<Task> GetTasksToDisplay(TaskDisplayFilter filter)
        {
            List<Task> tasksToDisplay = new List<Task>();
            tasksToDisplay.Add(new Task(string.Format("{0}1", filter.ToString()), 0));
            tasksToDisplay.Add(new Task(string.Format("{0}2", filter.ToString()), 1));
            return tasksToDisplay;
        }

        public Task CompleteTask(int taskId)
        {
            if (taskId == 404)
            {
                return null;
            }

            return new Task("TestCompletion", taskId);
        }
    }
}

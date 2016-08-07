using System.Collections.Generic;

namespace TaskManager
{
    public interface ITaskList
    {
        List<Task> GetTasksToDisplay(TaskDisplayFilter filter);
        Task AddTask(string taskName);
        Task CompleteTask(int taskId);
    }
}
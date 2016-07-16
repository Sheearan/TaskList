using System;

namespace TaskManager
{
    [Serializable]
    internal class Task
    {
        public string Title { get; set; }
        public int TaskId { get; set; }

        // Not implemented yet - I just wanted to get the data fields here to make serialization and deserialization work for the next few features I want to add
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? StartDateRecurrenceFromCompletionInDays { get; set; }
        public int? DueDateRecurrenceFromCompletionInDays { get; set; }

        private Task()
        {
            // People shouldn't be calling this without a taskTitle
        }

        public Task(string taskTitle, int id)
        {
            Title = taskTitle;
            TaskId = id;
        }
    }
}

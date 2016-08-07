using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager;
using System.Collections.Generic;

namespace TaskListTests
{
    [TestClass]
    public class TaskListTest
    {
        [TestMethod]
        public void AddTaskShouldIncreaseTaskCount()
        {
            TaskList tasks = new TaskList();
            tasks.AddTask("Test task");
            Assert.AreEqual(1, tasks.Count);
        }

        [TestMethod]
        public void AddTaskShouldReturnTaskWithId()
        {
            TaskList tasks = new TaskList();
            Task addedTask = tasks.AddTask("Test task");
            int taskId = addedTask.TaskId;
            Assert.AreEqual(0, taskId, string.Format("Task ID should be 1, not {0}", taskId));
        }

        [TestMethod]
        public void AddingTwoTasksShouldResultInSeparateIds()
        {
            TaskList tasks = new TaskList();
            Task firstTask = tasks.AddTask("Test task");
            Assert.AreEqual(0, firstTask.TaskId, string.Format("Task ID should be 0, not {0}", firstTask.TaskId));

            Task secondTask = tasks.AddTask("Test task");
            Assert.AreEqual(1, secondTask.TaskId, string.Format("Task ID should be 1, not {0}", secondTask.TaskId));
        }

        [TestMethod]
        public void GetTasksToDisplayShouldReturnAllTasks()
        {
            TaskList tasks = new TaskList();
            SetupTaskList(tasks);
            List<Task> actual = tasks.GetTasksToDisplay(TaskDisplayFilter.All);

            Assert.AreEqual(2, actual.Count, string.Format("There should be 2 tasks to display, not {0}", actual.Count));
        }

        [TestMethod]
        public void GetTasksToDisplayShouldReturnIncompleteTasks()
        {
            TaskList tasks = new TaskList();
            SetupTaskList(tasks);
            List<Task> actual = tasks.GetTasksToDisplay(TaskDisplayFilter.Incomplete);

            Assert.AreEqual(1, actual.Count, string.Format("There should be 1 task to display, not {0}", actual.Count));
        }


        [TestMethod]
        public void CompleteTaskShouldMarkTaskComplete()
        {
            TaskList tasks = new TaskList();
            Task newTask = tasks.AddTask("Task to complete");
            Assert.IsNull(newTask.CompletionDate, "Tasks should not be completed when they are first added.");

            tasks.CompleteTask(newTask.TaskId);
            Assert.IsNotNull(newTask.CompletionDate, "Completing a task needs to add a completion date.");
            Assert.AreEqual<DateTime>(DateTime.Today, newTask.CompletionDate.Value, "Completion date should be today.");
        }

        [TestMethod]
        public void CompleteTaskShouldDisplayErrorIfTaskIdIsNonexistent()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TaskList tasks = new TaskList();
                tasks.CompleteTask(42);

                string expected = string.Format("Could not find task 42.{0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString(), string.Format("Expected output is {0}, not {1}", expected, sw.ToString()));
            }
        }

        private static void SetupTaskList(TaskList tasks)
        {
            Task firstTask = tasks.AddTask("New Task");
            Task secondTask = tasks.AddTask("Another Task");
            tasks.CompleteTask(firstTask.TaskId);
        }
    }
}

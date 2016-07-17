using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager;

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
        public void DisplayShouldDisplayTasks()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TaskList tasks = new TaskList();
                tasks.AddTask("Test1");
                tasks.AddTask("Test2");

                tasks.Display();

                string expected = string.Format("0 Test1{0}1 Test2{0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString(), string.Format("Expected output is {0}, not {1}", expected, sw.ToString()));
            }
        }

        [TestMethod]
        public void DisplayShouldNotDisplayCompletedTasks()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TaskList tasks = new TaskList();
                Task taskToComplete = tasks.AddTask("Test1");
                tasks.AddTask("Test2");
                tasks.CompleteTask(taskToComplete.TaskId.ToString());

                tasks.Display();

                string expected = string.Format("1 Test2{0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString(), string.Format("Expected output is {0}, not {1}", expected, sw.ToString()));
            }
        }


        [TestMethod]
        public void CompleteTaskShouldMarkTaskComplete()
        {
            TaskList tasks = new TaskList();
            Task newTask = tasks.AddTask("Task to complete");
            Assert.IsNull(newTask.CompletionDate, "Tasks should not be completed when they are first added.");

            tasks.CompleteTask(newTask.TaskId.ToString());
            Assert.IsNotNull(newTask.CompletionDate, "Completing a task needs to add a completion date.");
            Assert.AreEqual<DateTime>(DateTime.Today, newTask.CompletionDate.Value, "Completion date should be today.");
        }

        [TestMethod]
        public void CompleteTaskShouldDisplayErrorIfArgumentIsNotANumber()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TaskList tasks = new TaskList();
                tasks.CompleteTask("This is a string");

                string expected = string.Format("This is a string is not a valid task ID. Task IDs must be positive integers.{0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString(), string.Format("Expected output is {0}, not {1}", expected, sw.ToString()));
            }
        }

        [TestMethod]
        public void CompleteTaskShouldDisplayErrorIfTaskIdIsNonexistent()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TaskList tasks = new TaskList();
                tasks.CompleteTask("42");

                string expected = string.Format("Could not find task 42.{0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString(), string.Format("Expected output is {0}, not {1}", expected, sw.ToString()));
            }
        }

        // TODO: Handle task ID that doesn't exist in list as input
    }
}

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
        public void AddTaskShouldReturnTaskId()
        {
            TaskList tasks = new TaskList();
            int taskId = tasks.AddTask("Test task");
            Assert.AreEqual(0, taskId, string.Format("Task ID should be 1, not {0}", taskId));
        }

        [TestMethod]
        public void AddingSecondTaskShouldChangeTaskId()
        {
            TaskList tasks = new TaskList();
            int taskId = tasks.AddTask("Test task");
            Assert.AreEqual(0, taskId, string.Format("Task ID should be 0, not {0}", taskId));

            int secondTaskId = tasks.AddTask("Test task");
            Assert.AreEqual(1, secondTaskId, string.Format("Task ID should be 1, not {0}", secondTaskId));
        }

        // TODO: Add task ID to display

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
    }
}

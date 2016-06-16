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
        public void DisplayShouldDisplayTasks()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TaskList tasks = new TaskList();
                tasks.AddTask("Test1");
                tasks.AddTask("Test2");

                tasks.Display();

                string expected = string.Format("Test1{0}Test2{0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString(), string.Format("Expected output is {0}, not {1}", expected, sw.ToString()));
            }
        }
    }
}

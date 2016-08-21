using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager;
using TaskManager.DataStorage;
using TaskListTests.UserInterfaceTests;
using System.Collections.Generic;

namespace TaskManagerTests.DataStorageTests
{
    [TestClass]
    public class TaskListFileTest
    {
        [TestMethod]
        public void SaveWithFilePathShouldSaveData()
        {
            TestStorage saveSensor = new TestStorage();
            TaskListFile file = new TaskListFile(saveSensor);

            file.Save(new TaskList(), new TestInputParser(new List<string> { "TestFileName" }));

            Assert.AreEqual("TestFileName", saveSensor.SavedFile, string.Format("Saved file name should be TestFileName, not {0}", saveSensor.SavedFile));
        }

        [TestMethod]
        public void SecondSaveShouldDefaultToSameFilePathAsFirst()
        {
            TestStorage saveSensor = new TestStorage();
            TaskListFile file = new TaskListFile(saveSensor);

            file.Save(new TaskList(), new TestInputParser(new List<string> { "TestFileName" }));

            saveSensor.Clear();
            file.Save(new TaskList(), new TestInputParser(new List<string> { }));
            Assert.AreEqual("TestFileName", saveSensor.SavedFile, string.Format("Saved file name should be TestFileName, not {0}", saveSensor.SavedFile));
        }

        [TestMethod]
        public void SaveAfterLoadShouldDefaultToFileThatWasLoaded()
        {
            TestStorage saveSensor = new TestStorage();
            TaskListFile file = new TaskListFile(saveSensor);

            file.Load("LoadTestFile");

            file.Save(new TaskList(), new TestInputParser(new List<string> { })); //null

            saveSensor.Clear();
            file.Save(new TaskList(), new TestInputParser(new List<string> { })); //string.Empty
            Assert.AreEqual("LoadTestFile", saveSensor.SavedFile, string.Format("Saved file name should be LoadTestFile, not {0}", saveSensor.SavedFile));
        }

        [TestMethod]
        public void ValidLoadShouldLoadFile()
        {
            TestStorage loadSensor = new TestStorage();
            TaskListFile file = new TaskListFile(loadSensor);

            file.Load("TestLoadFile");

            Assert.AreEqual("TestLoadFile", loadSensor.LoadedFile, string.Format("Loaded file name should be TestLoadFile, not {0}", loadSensor.LoadedFile));
        }

        [TestMethod]
        public void NonExistentFileLoadShouldCreateEmptyListAndNotifyUser()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TaskListFile file = new TaskListFile(new TestStorage());

                file.Load("Nonexistent");

                string expected = string.Format("Saved file doesn't exist at Nonexistent.{0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString(), string.Format("Expected output is {0}, not {1}", expected, sw.ToString()));
            }
        }

        [TestMethod]
        public void InvalidLoadShouldCreateEmptyListAndNotifyUser()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TaskListFile file = new TaskListFile(new TestStorage());

                file.Load("Exception");

                string expected = string.Format("There was a problem loading the file.{0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString(), string.Format("Expected output is {0}, not {1}", expected, sw.ToString()));
            }
        }
    }
}

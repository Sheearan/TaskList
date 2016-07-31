using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TaskManager.UserInterface;
using System.Collections.Generic;
using TaskManager;

namespace TaskListTests.UserInterfaceTests
{
    [TestClass]
    public class CommandLineInterfaceTest : CommandLineInterface
    {
        const string displayOptionsOutput = "Please select which tasks you'd like to display by entering the corresponding number:{0}0 - Incomplete{0}1 - All{0}";
        public CommandLineInterfaceTest()
        {
            list = new MockTaskList();
        }

        [TestMethod]
        public void UnknownInputShouldPrintDirections()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                List<string> testInputs = new List<string>();
                testInputs.Add("Meow");
                TestInputParser testParser = new TestInputParser(testInputs);
                testParser.GetNextAction();
                PerformAction(testParser);

                string expected = string.Format("Use one of the following commands:{0}Create <task name> - Creates a new task{0}Complete <task ID number> - Completes a task{0}Display - Displays current list of tasks{0}Load <file path> - Loads task list from file.{0}Save <file path> - Saves task list to file{0}Save - Saves task list to the file it was loaded from or last saved to.{0}Exit - Saves task list (assuming there's a file to save to) and exits the program{0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString(), string.Format("Expected output is {0}, not {1}", expected, sw.ToString()));
            }
        }

        [TestMethod]
        public void DisplayShouldDisplayTasks()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                List<string> testInputs = new List<string>();
                testInputs.Add("Display");
                testInputs.Add(((int) TaskFilter.All).ToString());
                TestInputParser testParser = new TestInputParser(testInputs);
                input = testParser;
                testParser.GetNextAction();
                PerformAction(testParser);

                string expected = string.Format(displayOptionsOutput + "0 All1{0}1 All2{0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString(), string.Format("Expected output is {0}, not {1}", expected, sw.ToString()));
            }
        }

        [TestMethod]
        public void DisplayShouldNotDisplayCompletedTasks()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                List<string> testInputs = new List<string>();
                testInputs.Add("Display");
                testInputs.Add(((int)TaskFilter.Incomplete).ToString());
                TestInputParser testParser = new TestInputParser(testInputs);
                input = testParser;
                testParser.GetNextAction();
                PerformAction(testParser);

                string expected = string.Format(displayOptionsOutput + "0 Incomplete1{0}1 Incomplete2{0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString(), string.Format("Expected output is {0}, not {1}", expected, sw.ToString()));
            }
        }

        [TestMethod]
        public void GetUserSelectionFromEnumShouldDefaultToAllOnNonExistentNumber()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                List<string> testInputs = new List<string>();
                testInputs.Add("Display");
                testInputs.Add("350");
                TestInputParser testParser = new TestInputParser(testInputs);
                input = testParser;
                testParser.GetNextAction();
                PerformAction(testParser);

                string errorString = "Invalid input - defaulting to All{0}";
                string expected = string.Format(displayOptionsOutput + errorString + "0 All1{0}1 All2{0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString(), string.Format("Expected output is {0}, not {1}", expected, sw.ToString()));
            }
        }

        [TestMethod]
        public void GetUserSelectionFromEnumShouldDefaultToAllOnBogusString()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                List<string> testInputs = new List<string>();
                testInputs.Add("Display");
                testInputs.Add("Meow");
                TestInputParser testParser = new TestInputParser(testInputs);
                input = testParser;
                testParser.GetNextAction();
                PerformAction(testParser);

                string errorString = "Invalid input - defaulting to All{0}";
                string expected = string.Format(displayOptionsOutput + errorString + "0 All1{0}1 All2{0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString(), string.Format("Expected output is {0}, not {1}", expected, sw.ToString()));
            }
        }

        [TestMethod]
        public void GetUserSelectionFromEnumShouldRespectValidString()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                List<string> testInputs = new List<string>();
                testInputs.Add("Display");
                testInputs.Add("incomplete");
                TestInputParser testParser = new TestInputParser(testInputs);
                input = testParser;
                testParser.GetNextAction();
                PerformAction(testParser);

                string expected = string.Format(displayOptionsOutput + "0 Incomplete1{0}1 Incomplete2{0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString(), string.Format("Expected output is {0}, not {1}", expected, sw.ToString()));
            }
        }
    }
}

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
            List<string> testInputs = new List<string> { "Meow" };
            string expected = string.Format("Use one of the following commands:{0}Create <task name> - Creates a new task{0}Complete <task ID number> - Completes a task{0}Display - Displays current list of tasks{0}Load <file path> - Loads task list from file.{0}Save <file path> - Saves task list to file{0}Save - Saves task list to the file it was loaded from or last saved to.{0}Exit - Saves task list (assuming there's a file to save to) and exits the program{0}", Environment.NewLine);
            RunTest(testInputs, expected);
        }

        [TestMethod]
        public void DisplayShouldDisplayTasks()
        {
            List<string> testInputs = new List<string> { "Display", ((int)TaskFilter.All).ToString() };
            string expected = string.Format(displayOptionsOutput + "0 All1{0}1 All2{0}", Environment.NewLine);
            RunTest(testInputs, expected);
        }

        [TestMethod]
        public void DisplayShouldNotDisplayCompletedTasks()
        {
            List<string> testInputs = new List<string> { "Display", ((int)TaskFilter.Incomplete).ToString() };
            string expected = string.Format(displayOptionsOutput + "0 Incomplete1{0}1 Incomplete2{0}", Environment.NewLine);
            RunTest(testInputs, expected); 
        }

        [TestMethod]
        public void GetUserSelectionFromEnumShouldDefaultToAllOnNonExistentNumber()
        {
            List<string> testInputs = new List<string> { "Display", "350" };
            string errorString = "Invalid input - defaulting to All{0}";
            string expected = string.Format(displayOptionsOutput + errorString + "0 All1{0}1 All2{0}", Environment.NewLine);
            RunTest(testInputs, expected);
        }

        [TestMethod]
        public void GetUserSelectionFromEnumShouldDefaultToAllOnBogusString()
        {
            List<string> testInputs = new List<string> { "Display", "Meow" };
            string errorString = "Invalid input - defaulting to All{0}";
            string expected = string.Format(displayOptionsOutput + errorString + "0 All1{0}1 All2{0}", Environment.NewLine);
            RunTest(testInputs, expected);
        }

        [TestMethod]
        public void GetUserSelectionFromEnumShouldRespectValidString()
        {
            List<string> testInputs = new List<string> { "Display", "incomplete" };
            string expected = string.Format(displayOptionsOutput + "0 Incomplete1{0}1 Incomplete2{0}", Environment.NewLine);
            RunTest(testInputs, expected);
        }

        private void RunTest(List<string> input, string expectedOutput)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                PerformTestAction(input);
                Assert.AreEqual<string>(expectedOutput, sw.ToString(), string.Format("Expected output is {0}, not {1}", expectedOutput, sw.ToString()));
            }
        }

        private void PerformTestAction(List<string> testInputs)
        {
            TestInputParser testParser = new TestInputParser(testInputs);
            input = testParser;
            testParser.GetNextAction();
            PerformAction(testParser);
        }
    }
}

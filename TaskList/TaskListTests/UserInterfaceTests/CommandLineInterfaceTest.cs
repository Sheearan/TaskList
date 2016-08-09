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
        const string actionOptionsOutput = "Please select which action you'd like to take by entering the corresponding number:{0}0 - Help{0}1 - Create{0}2 - Display{0}3 - Complete{0}4 - Load{0}5 - Save{0}6 - Exit{0}";
        const string createOptionsOutput = "Please enter the name of your new task:{0}";
        const string displayOptionsOutput = "Please select which tasks you'd like to display by entering the corresponding number:{0}0 - Incomplete{0}1 - All{0}";
        const string completeOptionsOutput = "Please enter the ID of the task to complete:{0}";

        public CommandLineInterfaceTest()
        {
            list = new MockTaskList();
        }

        [TestMethod]
        public void GetUserSelectionFromEnumShouldDefaultToAllOnNonExistentNumber()
        {
            List<string> testInputs = new List<string> { "Display", "350" };
            string errorString = "Invalid input - defaulting to All{0}";
            string expected = string.Format(actionOptionsOutput + displayOptionsOutput + errorString + "0 All1{0}1 All2{0}", Environment.NewLine);
            RunTest(testInputs, expected);
        }

        [TestMethod]
        public void GetUserSelectionFromEnumShouldDefaultToAllOnBogusString()
        {
            List<string> testInputs = new List<string> { "Display", "Meow" };
            string errorString = "Invalid input - defaulting to All{0}";
            string expected = string.Format(actionOptionsOutput + displayOptionsOutput + errorString + "0 All1{0}1 All2{0}", Environment.NewLine);
            RunTest(testInputs, expected);
        }

        [TestMethod]
        public void GetUserSelectionFromEnumShouldRespectValidString()
        {
            List<string> testInputs = new List<string> { "Display", "incomplete" };
            string expected = string.Format(actionOptionsOutput + displayOptionsOutput + "0 Incomplete1{0}1 Incomplete2{0}", Environment.NewLine);
            RunTest(testInputs, expected);
        }

        [TestMethod]
        public void UnknownInputShouldPrintDirections()
        {
            List<string> testInputs = new List<string> { "Meow" };
            string errorString = "Invalid input - defaulting to Help{0}";
            string expected = string.Format(actionOptionsOutput + errorString, Environment.NewLine);
            RunTest(testInputs, expected);
        }

        [TestMethod]
        public void CreateShouldDisplayNewTaskIdWithName()
        {
            List<string> testInputs = new List<string> { "Create", "TestCreation" };
            string expected = string.Format(actionOptionsOutput + createOptionsOutput + "New task created: 0 TestCreation{0}", Environment.NewLine);
            RunTest(testInputs, expected);
        }

        [TestMethod]
        public void DisplayAllShouldDisplayTasks()
        {
            List<string> testInputs = new List<string> { "Display", ((int)TaskDisplayFilter.All).ToString() };
            string expected = string.Format(actionOptionsOutput + displayOptionsOutput + "0 All1{0}1 All2{0}", Environment.NewLine);
            RunTest(testInputs, expected);
        }

        [TestMethod]
        public void DisplayIncompleteShouldNotDisplayCompletedTasks()
        {
            List<string> testInputs = new List<string> { "Display", ((int)TaskDisplayFilter.Incomplete).ToString() };
            string expected = string.Format(actionOptionsOutput + displayOptionsOutput + "0 Incomplete1{0}1 Incomplete2{0}", Environment.NewLine);
            RunTest(testInputs, expected); 
        }

        [TestMethod]
        public void CompleteTaskShouldShowCompletedTask()
        {
            List<string> testInputs = new List<string> { "Complete", "0" };
            string expected = string.Format(actionOptionsOutput + completeOptionsOutput + "Task Completed: 0 TestCompletion{0}", Environment.NewLine);
            RunTest(testInputs, expected);
        }

        [TestMethod]
        public void CompleteTaskShouldDisplayErrorIfIdIsNotANumber()
        {
            List<string> testInputs = new List<string> { "Complete", "This is a string" };
            string errorString = "This is a string is not a valid task ID. Task IDs must be positive integers.{0}";
            string expected = string.Format(actionOptionsOutput + completeOptionsOutput + errorString, Environment.NewLine);
            RunTest(testInputs, expected);
        }

        [TestMethod]
        public void CompleteTaskShouldDisplayErrorIfTaskIdIsNonexistent()
        {
            List<string> testInputs = new List<string> { "Complete", "404" };
            string errorString = "Task ID 404 was not found.{0}";
            string expected = string.Format(actionOptionsOutput + completeOptionsOutput + errorString, Environment.NewLine);
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
            UserActions action = DetermineAction();
            PerformAction(action);
        }
    }
}

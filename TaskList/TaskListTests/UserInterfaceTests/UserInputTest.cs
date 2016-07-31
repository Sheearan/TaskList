using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TaskListTests.UserInterfaceTests;
using TaskManager;

namespace TaskManagerTests.UserInterfaceTests
{
    [TestClass]
    public class UserInputTest
    {
        [TestMethod]
        public void NullStringShouldBeUnknownAction()
        {
            CheckUserAction(null, UserActions.Unknown, "Null string should parse as unknown action, not {0}");
        }

        [TestMethod]
        public void EmptyStringShouldBeUnknownAction()
        {
            CheckUserAction(string.Empty, UserActions.Unknown, "Empty string should parse as unknown action, not {0}");
        }

        [TestMethod]
        public void WhiteSpaceStringShouldBeUnknownAction()
        {
            CheckUserAction(" ", UserActions.Unknown, "Whitespace string should parse as unknown action, not {0}");
        }

        [TestMethod]
        public void ExitStringShouldBeExitAction()
        {
            CheckUserAction("Exit", UserActions.Exit, "Exit string should parse as exit action, not {0}");
        }

        [TestMethod]
        public void CreateStringShouldBeCreateAction()
        {
            CheckUserAction("Create Test task", UserActions.Create, "Create string should parse as create action, not {0}");
        }

        [TestMethod]
        public void CreateStringShouldSetArguments()
        {
            CheckArguments("Create Test task", "Test task", "Argument should be Test Task, not {0}");
        }

        [TestMethod]
        public void DisplayStringShouldBeDisplayAction()
        {
            CheckUserAction("Display", UserActions.Display, "Display string should parse as display action, not {0}");
        }

        [TestMethod]
        public void LoadStringShouldBeLoadAction()
        {
            CheckUserAction("Load C:\\TestTaskList", UserActions.Load, "Load string should parse as load action, not {0}");
        }

        [TestMethod]
        public void LoadStringShouldSetArguments()
        {
            CheckArguments("Load C:\\TestTaskList", "C:\\TestTaskList", "Argument should be C:\\TestTaskList, not {0}");
        }

        [TestMethod]
        public void SaveStringShouldBeSaveAction()
        {
            CheckUserAction("Save C:\\TestTaskList", UserActions.Save, "Save string should parse as save action, not {0}");
        }

        [TestMethod]
        public void SaveStringShouldSetArguments()
        {
            CheckArguments("Save C:\\TestTaskList", "C:\\TestTaskList", "Argument should be C:\\TestTaskList, not {0}");
        }

        [TestMethod]
        public void CompleteStringShouldBeCompleteAction()
        {
            CheckUserAction("Complete 0", UserActions.Complete, "Complete string should parse as complete action, not {0}");
        }

        [TestMethod]
        public void CompleteStringShouldSetArguments()
        {
            CheckArguments("Complete 0", "0", "Argument should be 0, not {0}");
        }


        [TestMethod]
        public void OtherStringShouldBeUnknownAction()
        {
            CheckUserAction("Meow", UserActions.Unknown, "Unknown strings should parse as unknown action, not {0}");
        }

        private void CheckUserAction(string userInput, UserActions expectedAction, string errorMessage)
        {
            List<string> testInputs = new List<string>();
            testInputs.Add(userInput);
            TestInputParser testParser = new TestInputParser(testInputs);
            testParser.GetNextAction();

            Assert.AreEqual(expectedAction, testParser.Action, string.Format(errorMessage, testParser.Action));
        }

        private void CheckArguments(string userInput, string expectedArgument, string errorMessage)
        {
            List<string> testInputs = new List<string>();
            testInputs.Add(userInput);
            TestInputParser testParser = new TestInputParser(testInputs);
            testParser.GetNextAction();

            Assert.AreEqual(expectedArgument, testParser.Arguments, string.Format(errorMessage, testParser.Arguments));
        }

    }
}

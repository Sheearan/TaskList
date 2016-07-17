using System;

namespace TaskManager
{
    public class UserInput
    {
        private string _userInput { get; set; }
        private string _userArguments { get; set; }
        private UserActions? _userAction { get; set; }

        public UserActions Action
        {
            get
            {
                if (!_userAction.HasValue)
                {
                    ParseInput();
                }

                return _userAction.Value;
            }
        }

        public string Arguments
        {
            get
            {
                if (_userArguments == null)
                {
                    ParseInput();
                }

                return _userArguments;
            }
        }

        public UserInput(string input)
        {
            _userInput = input;
        }

        // This function is not under test.
        // I decided that there's not really any logic, so it doesn't need to be at this point.
        public static void PrintDirections()
        {
            Console.WriteLine("Use one of the following commands:");
            Console.WriteLine("Create <task name> - Creates a new task");
            Console.WriteLine("Display - Displays current list of tasks");
            Console.WriteLine("Load <file path> - Loads task list from file.");
            Console.WriteLine("Save <file path> - Saves task list to file");
            Console.WriteLine("Save - Saves task list to the file it was loaded from or last saved to.");
            Console.WriteLine("Exit - Saves task list (assuming there's a file to save to) and exits the program");
        }

        protected void ParseInput()
        {
            if (string.IsNullOrWhiteSpace(_userInput))
            {
                _userAction = UserActions.Unknown;
                return;
            }

            int firstSpace = findFirstSpace();
            parseActionType(firstSpace);
            parseArguments(firstSpace);
        }

        private int findFirstSpace()
        {
            _userInput = _userInput.Trim();
            return _userInput.IndexOf(" ");
        }

        private void parseActionType(int indexOfFirstSpace)
        {
            string actionType;
            if (indexOfFirstSpace == -1)
            {
                actionType = _userInput;
            }
            else
            {
                actionType = _userInput.Substring(0, indexOfFirstSpace);
            }

            switch (actionType)
            {
                case "Complete":
                    _userAction = UserActions.Complete;
                    return;
                case "Create":
                    _userAction = UserActions.Create;
                    return;
                case "Display":
                    _userAction = UserActions.Display;
                    return;
                case "Exit":
                    _userAction = UserActions.Exit;
                    return;
                case "Load":
                    _userAction = UserActions.Load;
                    return;
                case "Save":
                    _userAction = UserActions.Save;
                    return;
                default:
                    _userAction = UserActions.Unknown;
                    return;
            }
        }

        private void parseArguments(int indexOfFirstSpace)
        {
            if (indexOfFirstSpace == -1)
            {
                _userArguments = string.Empty;
            }
            else
            {
                _userArguments = _userInput.Substring(indexOfFirstSpace).Trim(' ');
            }
        }
    }
}

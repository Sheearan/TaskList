using System;

namespace TaskManager.UserInterface
{
    public class UserInputParser
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

        public void GetNextAction()
        {
            _userArguments = null;
            _userAction = null;
            _userInput = GetNextInputLine();
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

        internal T GetUserSelectionFromEnum<T>(T defaultSelection)
        {
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine(string.Format("{0} - {1}", (int) value, (T) value));
            }

            string userSelection = GetNextInputLine();
            try
            {
                T enumValue = (T)Enum.Parse(typeof(T), userSelection, true);
                if (Enum.IsDefined(typeof(T), enumValue))
                {
                    return enumValue;
                }
                else
                {
                    Console.WriteLine(string.Format("Invalid input - defaulting to {0}", defaultSelection));
                    return defaultSelection;
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine(string.Format("Invalid input - defaulting to {0}", defaultSelection));
                return defaultSelection;
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

        protected virtual string GetNextInputLine()
        {
            return Console.ReadLine();
        }
    }
}

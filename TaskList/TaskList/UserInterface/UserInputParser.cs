using System;

namespace TaskManager.UserInterface
{
    public class UserInputParser
    {
        internal T GetUserSelectionFromEnum<T>(T defaultSelection, bool showOptions)
        {
            PresentOptions<T>(showOptions);
            string userSelection = GetNextInputLine();
            return ParseUserSelection(defaultSelection, userSelection);
        }

        internal string GetString()
        {
            return GetNextInputLine();
        }

        private static void PresentOptions<T>(bool showOptions)
        {
            if (showOptions)
            {
                foreach (var value in Enum.GetValues(typeof(T)))
                {
                    Console.WriteLine(string.Format("{0} - {1}", (int)value, (T)value));
                }
            }
        }

        private static T ParseUserSelection<T>(T defaultSelection, string userSelection)
        {
            try
            {
                T enumValue = (T)Enum.Parse(typeof(T), userSelection, true);
                if (Enum.IsDefined(typeof(T), enumValue))
                {
                    return enumValue;
                }
                else
                {
                    return HandleInvalidInput(defaultSelection);
                }
            }
            catch (ArgumentException)
            {
                return HandleInvalidInput(defaultSelection);
            }
        }

        private static T HandleInvalidInput<T>(T defaultSelection)
        {
            Console.WriteLine(string.Format("Invalid input - defaulting to {0}", defaultSelection));
            return defaultSelection;
        }

        protected virtual string GetNextInputLine()
        {
            return Console.ReadLine();
        }
    }
}

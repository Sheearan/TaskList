using System;

namespace TaskManager.UserInterface
{
    public class UserInputParser
    {
        internal T GetUserSelectionFromEnum<T>(T defaultSelection, bool showOptions)
        {
            if (showOptions)
            {
                foreach (var value in Enum.GetValues(typeof(T)))
                {
                    Console.WriteLine(string.Format("{0} - {1}", (int)value, (T)value));
                }
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

        protected virtual string GetNextInputLine()
        {
            return Console.ReadLine();
        }
    }
}

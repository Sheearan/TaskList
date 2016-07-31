using System;
using System.Collections.Generic;
using TaskManager.UserInterface;

namespace TaskListTests.UserInterfaceTests
{
    internal class TestInputParser : UserInputParser
    {
        List<string> _stringsToInput;
        int _currentPlaceInInputList;

        internal TestInputParser(List<string> inputs)
        {
            _stringsToInput = inputs;
            _currentPlaceInInputList = 0;
        }

        protected override string GetNextInputLine()
        {
            if (_currentPlaceInInputList < _stringsToInput.Count)
            {
                string nextString = _stringsToInput[_currentPlaceInInputList];
                _currentPlaceInInputList++;
                return nextString;
            }

            throw new InvalidOperationException(string.Format("Insufficient inputs provided for number of times GetNextInputLine was called. Only {0} inputs were provided.", _stringsToInput.Count));
        }
    }
}

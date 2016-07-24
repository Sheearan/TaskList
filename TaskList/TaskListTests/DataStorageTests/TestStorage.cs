using System;
using TaskManager;
using TaskManager.DataStorage;

namespace TaskManagerTests.DataStorageTests
{
    internal class TestStorage : IStorage
    {
        public string LoadedFile { get; internal set; }
        public string SavedFile { get; private set; }

        public void Save(string fileName, TaskList list)
        {
            if (string.Equals(fileName, "Exception"))
            {
                throw new ArgumentException();
            }

            SavedFile = fileName;
        }

        public TaskList Load(string fileName)
        {
            if (string.Equals(fileName, "Nonexistent"))
            {
                throw new System.IO.FileNotFoundException();
            }

            if (string.Equals(fileName, "Exception"))
            {
                throw new ArgumentException();
            }

            LoadedFile = fileName;
            return new TaskList();
        }

        internal void Clear()
        {
            SavedFile = null;
            LoadedFile = null;
        }
    }
}

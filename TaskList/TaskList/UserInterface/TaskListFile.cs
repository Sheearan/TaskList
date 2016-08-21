using System;
using System.IO;
using TaskManager.UserInterface;

namespace TaskManager.DataStorage
{
    public class TaskListFile
    {
        private string _fileName;
        private IStorage _storage;

        public TaskListFile()
        {
            _storage = new FileSystem();
        }

        public TaskListFile(IStorage storageMechanism)
        {
            _storage = storageMechanism;
        }

        public void Save(ITaskList list, UserInputParser input)
        {
            string saveFileName = _fileName;

            while (string.IsNullOrWhiteSpace(saveFileName))
            {
                Console.WriteLine("Please enter the filename that you would like to save to:");
                saveFileName = input.GetString();
            }

            try
            {
                _storage.Save(saveFileName, list);
                _fileName = saveFileName;
                Console.WriteLine(string.Format("Task List saved to {0}.", saveFileName));
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Unable to save task data. {0}", e.Message));
            }
        }

        public TaskList Load(string fileName)
        {
            try
            {
                TaskList list = _storage.Load(fileName);
                _fileName = fileName;
                return list;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(string.Format("Saved file doesn't exist at {0}.", fileName));
                return new TaskList();
            }
            catch (Exception)
            {
                Console.WriteLine("There was a problem loading the file.");
                return new TaskList();
            }
        }
    }
}

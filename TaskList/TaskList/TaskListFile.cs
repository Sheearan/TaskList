using System;
using System.IO;

namespace TaskManager
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

        public void Save(string fileName, TaskList list)
        {
            string saveFileName = fileName;

            if (string.IsNullOrWhiteSpace(fileName))
            {
                if (string.IsNullOrWhiteSpace(_fileName))
                {
                    Console.WriteLine("No file path specified.");
                    return;
                }

                saveFileName = _fileName;
            }

            try
            {
                _storage.Save(saveFileName, list);
                _fileName = saveFileName;
            }
            catch
            {
                Console.WriteLine("Unable to save task data.");
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

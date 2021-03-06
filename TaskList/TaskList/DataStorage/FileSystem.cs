﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TaskManager.DataStorage
{
    internal class FileSystem : IStorage
    {
        public void Save(string fileName, ITaskList list)
        {
            FileStream writerFileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(writerFileStream, list);

            writerFileStream.Close();
        }

        public TaskList Load(string fileName)
        {
            if (File.Exists(fileName))
            {
                FileStream readerFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryFormatter formatter = new BinaryFormatter();
                TaskList taskList = (TaskList)(formatter.Deserialize(readerFileStream));

                readerFileStream.Close();
                return taskList;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}

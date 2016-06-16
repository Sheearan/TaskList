using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TaskManager
{
    internal class FileSystem : IStorage
    {
        public void Save(string fileName, TaskList list)
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
                List<Task> taskList = (List<Task>)(formatter.Deserialize(readerFileStream));

                readerFileStream.Close();
                return new TaskList(taskList);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}

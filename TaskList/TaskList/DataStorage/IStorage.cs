namespace TaskManager.DataStorage
{
    public interface IStorage
    {
        void Save(string fileName, ITaskList list);
        TaskList Load(string fileName);
    }
}

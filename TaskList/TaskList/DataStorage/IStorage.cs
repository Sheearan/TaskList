namespace TaskManager
{
    public interface IStorage
    {
        void Save(string fileName, TaskList list);
        TaskList Load(string fileName);
    }
}

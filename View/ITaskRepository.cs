public interface ITaskRepository
{
    IMyCollection<TaskItem> LoadTasks();

    void SaveTasks(List<TaskItem> tasks);
}
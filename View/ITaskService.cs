public interface ITaskService
{
    IEnumerable<TaskItem> GetAllTasks();
    void AddTask(string description);
    void UpdateTask(int id);
    void RemoveTask(int id);
    void ToggleTaskCompletion(int id);
}
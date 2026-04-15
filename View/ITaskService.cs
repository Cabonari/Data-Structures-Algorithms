public interface ITaskService
{
    IEnumerable<TaskItem> GetAllTasks();
    void AddTask(string priority, string description, string[] assignees, int[] dependencies);
    void UpdateTask(int id);
    void RemoveTask(int id);
    void ToggleTaskCompletion(int id);

    IEnumerable<TaskItem> GetTasksByPriority(string priority);
    IEnumerable<TaskItem> GetTasksByStatus(string status);
    IEnumerable<TaskItem> GetTasksByDateRange(DateTime? from, DateTime? to);

    void ChangeUser(string user);

    string CurrentUser { get; }
}
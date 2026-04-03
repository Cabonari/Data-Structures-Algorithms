public interface ITaskService
{
    IEnumerable<TaskItem> GetAllTasks();
    void AddTask(string priority, string description, string[] assignees);
    void UpdateTask(int id);
    void RemoveTask(int id);
    void ToggleTaskCompletion(int id);

    void ChangeUser(string user); 

    string CurrentUser {get;}
}
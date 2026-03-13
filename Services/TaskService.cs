public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly MyArray<TaskItem> _tasks = new MyArray<TaskItem>();

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
        _tasks = (MyArray<TaskItem>)repository.LoadTasks();
    }

    public IEnumerable<TaskItem> GetAllTasks()
    {
        for (int i = 0; i < _tasks.Count; i++)
        {
            var task = _tasks.FindBy(i, (t, key) => t.Id.CompareTo(key));
            if (task != null)
            {
                yield return task;
            }
        }
    }

    private static string Prompt(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    public void AddTask(string priority, string description)
    {
        int newId = _tasks.Count > 0 ? _tasks.FindBy(_tasks.Count - 1, (t, key) => t.Id.CompareTo(key)).Id + 1 : 1;
        var newTask = new TaskItem
        {
            Id = newId,
            Priority = priority,
            Description = description,
            Completed = false,
            Assignees = Array.Empty<string>()
        };
        _tasks.Add(newTask);
        _repository.SaveTasks(_tasks);
    }

    public void UpdateTask(int id)
    {
        var task = _tasks.FindBy(id, (t, key) => t.Id.CompareTo(key));

        if (task != null)
        {
            string newPriority = Prompt($"\nEnter new priority (was '{task.Priority}'): ");
            if (newPriority != string.Empty) task.Priority = newPriority;

            string newDescription = Prompt("\nEnter new description: ");
            if (newDescription != string.Empty) task.Description = newDescription;

            string newAssignees = Prompt("\nEnter new assignees (use ', ' for multiple assignees): ");
            if (newAssignees != string.Empty)
            {
                string[] newAssigneesList = newAssignees.Split(", ");
                task.Assignees = newAssigneesList.ToArray();
            }

            _repository.SaveTasks(_tasks);
        }
    }

    public void RemoveTask(int id)
    {
        var task = _tasks.FindBy(id, (t, key) => t.Id.CompareTo(key));

        if (task != null)
        {
            _tasks.Remove(task);
            _repository.SaveTasks(_tasks);
        }
    }

    public void ToggleTaskCompletion(int id)
    {
        var task = _tasks.FindBy(id, (t, key) => t.Id.CompareTo(key));

        if (task != null)
        {
            task.Completed = !task.Completed;
            _repository.SaveTasks(_tasks);
        }
    }
}
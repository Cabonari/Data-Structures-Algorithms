public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly List<TaskItem> _tasks;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
        _tasks = repository.LoadTasks();
    }

    public IEnumerable<TaskItem> GetAllTasks()
    {
        return _tasks;
    }

    private string Prompt(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    public void AddTask(string priority, string description)
    {
        int newId = _tasks.Count > 0 
            ? _tasks[_tasks.Count - 1].Id + 1 
            : 1;

        var newTask = new TaskItem
        {
            Id = newId,
            Priority = priority,
            Description = description,
            Assignees = [],
            Completed = false
        };

        _tasks.Add(newTask);
        _repository.SaveTasks(_tasks);
    }

    public void UpdateTask(int id)
    {
        var task = _tasks.Find(t => t.Id == id);

        if (task !=null)
        {
            string newPriority = Prompt($"\nEnter new priority (was '{task.Priority}'): ");
            if(newPriority != string.Empty) task.Priority = newPriority;

            string newDescription = Prompt("\nEnter new description: ");
            if(newDescription != string.Empty) task.Description = newDescription;

            string newAssignees = Prompt("\nEnter new assignees (use ', ' for multiple assignees): ");
            if(newAssignees != string.Empty)
            {
                string[] newAssigneesList = newAssignees.Split(", ");
                task.Assignees = newAssigneesList.ToList();
            }

            _repository.SaveTasks(_tasks);
        }
    }

    public void RemoveTask(int id)
    {
        var task = _tasks.Find(t => t.Id == id);

        if (task != null)
        {
            _tasks.Remove(task);
            _repository.SaveTasks(_tasks);
        }
    }

    public void ToggleTaskCompletion(int id)
    {
        var task = _tasks.Find(t => t.Id == id);

        if (task != null)
        {
            task.Completed = !task.Completed;
            _repository.SaveTasks(_tasks);
        }
    }
}
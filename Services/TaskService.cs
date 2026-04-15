public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly IMyCollection<TaskItem> _tasks = new MyBinarySearchTree<TaskItem>();

    public string CurrentUser { get; private set; } = "";

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
        _tasks = repository.LoadTasks();
    }

    public IEnumerable<TaskItem> GetAllTasks()
    {
        foreach (var task in _tasks)
        {
            yield return task;
        }
    }

    public IEnumerable<TaskItem> GetTasksByPriority(string priority)
    {
        foreach (var task in _tasks)
        {
            if (string.Equals(task.Priority, priority, StringComparison.OrdinalIgnoreCase))
                yield return task;
        }
    }

    public IEnumerable<TaskItem> GetTasksByStatus(string status)
    {
        foreach (var task in _tasks)
        {
            if (string.Equals(task.Row, status, StringComparison.OrdinalIgnoreCase))
                yield return task;
        }
    }

    public IEnumerable<TaskItem> GetTasksByDateRange(DateTime? from, DateTime? to)
    {
        foreach (var task in _tasks)
        {
            if (from.HasValue && task.Date < from.Value)
                continue;
            if (to.HasValue && task.Date > to.Value)
                continue;
            yield return task;
        }
    }

    private static string Prompt(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    public void AddTask(string priority, string description, string[] assignees)
    {
        int newId = 1;
        while (_tasks.FindBy(newId, (t, key) => t.Id.CompareTo(key)) != null) newId++;

        var newTask = new TaskItem
        {
            Id = newId,
            Priority = priority,
            Description = description,
            Date = DateTime.Now,
            Assignees = assignees,
            Row = "TODO"
        };
        _tasks.Add(newTask);
        _repository.SaveTasks(_tasks);
    }

    public void UpdateTask(int id)
    {
        var task = _tasks.FindBy(id, (t, key) => t.Id.CompareTo(key));

        if (task != null)
        {

            // Permission Check 
            if (!task.Assignees.Contains(CurrentUser))
            {
                Console.WriteLine("You are not assigned to this task.");
                Console.ReadKey();
                return;
            }


            string newPriority = Prompt($"\nEnter new priority (was '{task.Priority}'): ");
            if (newPriority != string.Empty) task.Priority = newPriority;

            string newDescription = Prompt($"\nEnter new description (was '{task.Description}'): ");
            if (newDescription != string.Empty) task.Description = newDescription;

            string newAssignees = Prompt($"\nEnter new assignees'): ");
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

        if (task == null)
        {
            Console.WriteLine("Task not found.");
            Console.ReadKey();
            return;
        }

        // Permission check 
        if (!task.Assignees.Contains(CurrentUser))
        {
            Console.WriteLine("You are not assigned to this task.");
            Console.ReadKey();
            return;
        }

        _tasks.Remove(task);
        _repository.SaveTasks(_tasks);

    }

    public void ToggleTaskCompletion(int id)
    {
        var task = _tasks.FindBy(id, (t, key) => t.Id.CompareTo(key));

        if (task == null)
        {
            Console.WriteLine("Task not found.");
            Console.ReadKey();
            return;
        }

        // Permission check 
        if (!task.Assignees.Contains(CurrentUser))
        {
            Console.WriteLine("You are not assigned to this task.");
            Console.ReadKey();
            return;
        }

        switch (task.Row)
        {
            case "TODO":
                task.Row = "Doing";
                break;
            case "Doing":
                task.Row = "Review";
                break;
            case "Review":
                task.Row = "Done";
                break;
            case "Done":
                Console.WriteLine("Task is already in 'Done' state.");
                Console.ReadKey();
                break;
            default:
                Console.WriteLine("Something went wrong :/");
                Console.ReadKey();
                break;
        }

        _repository.SaveTasks(_tasks);
    }

    public void ChangeUser(string user)
    {
        CurrentUser = user;
    }

}
public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private IMyCollection<TaskItem> _tasks = new MyArray<TaskItem>();
    public IMyCollection<TaskItem> _oldTasks = new MyArray<TaskItem>();
    private string _currentDataStructure = "Array";

    public string CurrentDataStructure => _currentDataStructure;

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

    private static bool TryParseStatus(string row, out Status status)
    {
        if (string.Equals(row, "Doing", StringComparison.OrdinalIgnoreCase))
        {
            status = Status.InProgress;
            return true;
        }

        return Enum.TryParse(row, true, out status);
    }

    public void AddTask(string priority, string description, string[] assignees, int[] dependencies)
    {
        if (_tasks.Count >= 20)
        {
            Console.WriteLine("Task limit reached.");
            Console.ReadKey();
            return;
        }

        int newId = 1;
        while (_tasks.FindBy(newId, (t, key) => t.Id.CompareTo(key)) != null) newId++;

        if(checkValidPriority(priority) is not null)
        {
            Console.WriteLine(checkValidPriority(priority));
            Console.ReadKey();
            return;
        }

        if(checkValidDescription(description) is not null)
        {
            Console.WriteLine(checkValidDescription(description));
            Console.ReadKey();
            return;
        }

        if(checkValidAssignees(assignees) is not null)
        {
            Console.WriteLine(checkValidAssignees(assignees));
            Console.ReadKey();
            return;
        }

        if(checkValidDependencies(dependencies) is not null)
        {
            Console.WriteLine(checkValidDependencies(dependencies));
            Console.ReadKey();
            return;
        }

        var newTask = new TaskItem
        {
            Id = newId,
            Priority = priority,
            Description = description,
            Date = DateTime.Now,
            Assignees = assignees,
            Row = "TODO",
            Dependecies = dependencies ?? new int[] { }
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
            if (task.Assignees.Contains(CurrentUser))
            {
                Console.WriteLine("You are not assigned to this task.");
                Console.ReadKey();
                return;
            }

            string newPriority = Prompt($"\nEnter new priority (was '{task.Priority}'): ");
            if (newPriority != string.Empty)
            {
                if(checkValidPriority(newPriority) is not null)
                {
                    Console.WriteLine(checkValidPriority(newPriority));
                    Console.ReadKey();
                    return;
                }

                task.Priority = newPriority;
            }

            string newDescription = Prompt($"\nEnter new description (was '{task.Description}'): ");
            if (newDescription != string.Empty)
            {
                if(checkValidDescription(newDescription) is not null)
                {
                    Console.WriteLine(checkValidDescription(newDescription));
                    Console.ReadKey();
                    return;
                }

                task.Description = newDescription;
            }

            string newAssignees = Prompt($"\nEnter new assignees'): ");
            if (newAssignees != string.Empty)
            {
                string[] newAssigneesList = newAssignees.Split(", ");

                if(checkValidAssignees(newAssigneesList) is not null)
                {
                    Console.WriteLine(checkValidAssignees(newAssigneesList));
                    Console.ReadKey();
                    return;
                }

                task.Assignees = newAssigneesList.ToArray();
            }

            string newDependencies = Prompt($"\nEnter new dependencies (comma-separated, was '{string.Join(", ", task.Dependecies)}'): ");
            if (newDependencies != string.Empty)
            {
                int[] newDependenciesList = newDependencies.Split(", ").Select(int.Parse).ToArray();

                if(checkValidDependencies(newDependenciesList) is not null)
                {
                    Console.WriteLine(checkValidDependencies(newDependenciesList));
                    Console.ReadKey();
                    return;
                }

                task.Dependecies = newDependenciesList;
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
        if (task.Assignees.Contains(CurrentUser))
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
        if (task.Assignees.Contains(CurrentUser))
        {
            Console.WriteLine("You are not assigned to this task.");
            Console.ReadKey();
            return;
        }

        if (!TryParseStatus(task.Row, out var currentStatus))
        {
            Console.WriteLine("Current task status is invalid.");
            Console.ReadKey();
            return;
        }

        // Dependency check
        foreach (int dependencyId in task.Dependecies)
        {
            var dependencyTask = _tasks.FindBy(dependencyId, (t, key) => t.Id.CompareTo(key));
            if (dependencyTask != null)
            {
                if (!TryParseStatus(dependencyTask.Row, out var dependencyStatus))
                {
                    Console.WriteLine($"Dependency task {dependencyId} has an invalid status.");
                    Console.ReadKey();
                    return;
                }

                if (dependencyStatus <= currentStatus)
                {
                    Console.WriteLine($"This task cannot be toggled until dependency task {dependencyId} reaches a higher status.");
                    Console.ReadKey();
                    return;
                }
            }
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

    public void ChooseDataStructure(int choice)
    {
        _oldTasks = _tasks;

        switch (choice)
        {
            case 2:
                _tasks = new MyLinkedList<TaskItem>();
                _currentDataStructure = "Linked List";
                break;
            case 3:
                var comparer = Comparer<TaskItem>.Create((a, b) => a.Id.CompareTo(b.Id));
                _tasks = new MyBinarySearchTree<TaskItem>(comparer);
                _currentDataStructure = "Binary Search Tree";
                break;
            case 4:
                _tasks = new MyHashMap<TaskItem>();
                _currentDataStructure = "HashMap";
                break;
            case 1:
            default:
                _tasks = new MyArray<TaskItem>();
                _currentDataStructure = "Array";
                break;
        }

        foreach (var task in _oldTasks)
        {
            _tasks.Add(task);
        }
    }
}
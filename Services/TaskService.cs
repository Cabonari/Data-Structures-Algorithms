public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly IMyCollection<TaskItem> _tasks = new MyBinarySearchTree<TaskItem>();

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

        if (task != null)
        {
            _tasks.Remove(task);
            _repository.SaveTasks(_tasks);
        }
    }

    public void ToggleTaskCompletion(int id)
    {
        var task = _tasks.FindBy(id, (t, key) => t.Id.CompareTo(key));
        if (task == null) Console.WriteLine("Task not found.");
        else
        {
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
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Something went wrong :/");
                    Console.ReadLine();
                    break;
            }

            _repository.SaveTasks(_tasks);
        }
    }
}
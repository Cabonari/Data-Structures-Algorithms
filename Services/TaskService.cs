public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly IMyCollection<TaskItem> _tasks = new MyArray<TaskItem>();

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
        _tasks = repository.LoadTasks();
    }

    public IEnumerable<TaskItem> GetAllTasks()
    {
        for (int i = 0; i < _tasks.Count; i++)
        {
            var task = _tasks.FindBy(i + 1, (t, key) => t.Id.CompareTo(key));
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
        int newId = _tasks.Count + 1;
        while (_tasks.FindBy(newId, (t, key) => t.Id.CompareTo(key)) != null) newId++;

        var newTask = new TaskItem
        {
            Id = newId,
            Priority = priority,
            Description = description,
            Completed = false,
            Date = DateTime.Now,
            Assignees = [],
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

            int newRow;
            string rowInput;
            do
            {
                rowInput = Prompt($"\nEnter new row (was '{task.Row}'): \n1. TODO\n2. Doing\n3. Review\n4. Done\n");
            }
            while (!int.TryParse(rowInput, out newRow) || newRow < 1 || newRow > 4);

            task.Row = newRow switch
            {
                1 => "TODO",
                2 => "Doing",
                3 => "Review",
                4 => "Done",
                _ => task.Row
            };

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

        if (task != null)
        {
            task.Completed = !task.Completed;
            _repository.SaveTasks(_tasks);
        }
    }
}
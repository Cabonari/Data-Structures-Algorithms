public class ConsoleTaskView : ITaskView
{
    private readonly ITaskService _service;
    private static int windowWidth = Console.WindowWidth;
    private static int windowHeight = Console.WindowHeight;
    private static int colWidth = windowWidth / 4;

    public ConsoleTaskView(ITaskService service)
    {
        _service = service;
    }

    private static void FullClear()
    {
        try { Console.Write("\u001b[2J\u001b[H\u001b[3J"); }
        catch { Console.Clear(); }
    }

    private void DisplayTasks(IEnumerable<TaskItem> tasks)
    {
        FullClear();

        Console.WriteLine("Task List".PadLeft((windowWidth + 9) / 2));
        Console.WriteLine(new string('-', windowWidth));
        Console.WriteLine(new string("TODO".PadLeft((colWidth + 4) / 2).PadRight(colWidth) + "Doing".PadLeft((colWidth + 5) / 2).PadRight(colWidth) + "Review".PadLeft((colWidth + 6) / 2).PadRight(colWidth) + "Done".PadLeft((colWidth + 4) / 2).PadRight(colWidth)));

        int todoCount = 0;
        int doingCount = 0;
        int reviewCount = 0;
        int doneCount = 0;

        foreach (var task in tasks)
        {
            string taskText = $"{task.Id}: {task.Priority} - {task.Description}";
            if (taskText.Length > colWidth - colWidth / 4) taskText = string.Concat(taskText.AsSpan(0, colWidth - colWidth / 4), "...");
            int padding = (colWidth - taskText.Length) / 2;

            switch (task.Row)
            {
                case "TODO":
                    Console.SetCursorPosition(Math.Max(0, padding), 3 + todoCount);
                    todoCount++;
                    break;
                case "Doing":
                    Console.SetCursorPosition(Math.Max(colWidth, colWidth + padding), 3 + doingCount);
                    doingCount++;
                    break;
                case "Review":
                    Console.SetCursorPosition(Math.Max(colWidth * 2, colWidth * 2 + padding), 3 + reviewCount);
                    reviewCount++;
                    break;
                case "Done":
                    Console.SetCursorPosition(Math.Max(colWidth * 3, colWidth * 3 + padding), 3 + doneCount);
                    doneCount++;
                    break;
            }

            Console.WriteLine(taskText);
        }
    }

    private string Prompt(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    private void ShowFilterMenu()
    {
        FullClear();
        Console.WriteLine("Filter Tasks");
        Console.WriteLine(new string('-', windowWidth));
        Console.WriteLine("1. By Priority");
        Console.WriteLine("2. By Status");
        Console.WriteLine("3. By Creation Date");
        Console.WriteLine("4. Back");
        Console.WriteLine();

        string filterOption = Prompt("Select a filter option: ");
        IEnumerable<TaskItem> filteredTasks;

        switch (filterOption)
        {
            case "1":
                filteredTasks = FilterByPriority();
                break;
            case "2":
                filteredTasks = FilterByStatus();
                break;
            case "3":
                filteredTasks = FilterByCreationDate();
                break;
            default:
                return;
        }

        DisplayTasks(filteredTasks);
        Console.WriteLine();
        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }

    private IEnumerable<TaskItem> FilterByPriority()
    {
        string priority = Prompt("Enter task priority to filter by: ");
        if (string.IsNullOrWhiteSpace(priority))
        {
            Console.WriteLine("Priority cannot be empty. Press any key to continue...");
            Console.ReadKey();
            return _service.GetAllTasks();
        }

        return _service.GetTasksByPriority(priority);
    }

    private IEnumerable<TaskItem> FilterByStatus()
    {
        string status = Prompt("Enter task status to filter by (TODO, Doing, Review, Done): ");
        if (string.IsNullOrWhiteSpace(status))
        {
            Console.WriteLine("Status cannot be empty. Press any key to continue...");
            Console.ReadKey();
            return _service.GetAllTasks();
        }

        return _service.GetTasksByStatus(status);
    }

    private IEnumerable<TaskItem> FilterByCreationDate()
    {
        string startInput = Prompt("Enter start date (yyyy-MM-dd) or leave blank: ");
        string endInput = Prompt("Enter end date (yyyy-MM-dd) or leave blank: ");

        DateTime? startDate = null;
        DateTime? endDate = null;

        if (!string.IsNullOrWhiteSpace(startInput) && !DateTime.TryParse(startInput, out DateTime start))
        {
            Console.WriteLine("Invalid start date format. Press any key to continue...");
            Console.ReadKey();
            return _service.GetAllTasks();
        }

        if (!string.IsNullOrWhiteSpace(endInput) && !DateTime.TryParse(endInput, out DateTime end))
        {
            Console.WriteLine("Invalid end date format. Press any key to continue...");
            Console.ReadKey();
            return _service.GetAllTasks();
        }

        if (!string.IsNullOrWhiteSpace(startInput))
            startDate = DateTime.Parse(startInput).Date;

        if (!string.IsNullOrWhiteSpace(endInput))
            endDate = DateTime.Parse(endInput).Date.AddDays(1).AddTicks(-1);
        else if (startDate.HasValue)
            endDate = startDate.Value.Date.AddDays(1).AddTicks(-1);

        return _service.GetTasksByDateRange(startDate, endDate);
    }

    public void Run()
    {
        Console.SetBufferSize(windowWidth, windowHeight);

        while (true)
        {
            DisplayTasks(_service.GetAllTasks());


            // Show user 
            Console.SetCursorPosition(0, windowHeight - 8);
            Console.WriteLine($"Current user: {_service.CurrentUser}");

            // Menu 
            Console.SetCursorPosition(0, windowHeight - 7);
            Console.WriteLine("\n\nOptions:");
            Console.WriteLine("1. Add Task     2. Update Task     3. Remove Task");
            Console.WriteLine("4. Toggle Task  5. Exit            6. Change User");
            Console.WriteLine("7. Filter Tasks");
            Console.WriteLine();
            string option = Prompt("Select an option: ");

            switch (option)
            {
                case "1":
                    string priority = Prompt("Enter task priority: ");
                    string description = Prompt("Enter task description: ");
                    string assigneesInput = Prompt("Enter assignees (comma separated):");

                    string[] assignees = string.IsNullOrWhiteSpace(assigneesInput)
                        ? []
                        : assigneesInput.Split(", ");
                    _service.AddTask(priority, description, assignees);
                    break;

                case "2":
                    string updateIdStr = Prompt("Enter task id to update: ");
                    if (int.TryParse(updateIdStr, out int updateId))
                    {
                        _service.UpdateTask(updateId);
                    }
                    break;

                case "3":
                    string removeIdStr = Prompt("Enter task id to remove: ");
                    if (int.TryParse(removeIdStr, out int removeId))
                    {
                        _service.RemoveTask(removeId);
                    }
                    break;

                case "4":
                    string toggleIdStr = Prompt("Enter task id to toggle: ");
                    if (int.TryParse(toggleIdStr, out int toggleId))
                    {
                        _service.ToggleTaskCompletion(toggleId);
                    }
                    break;

                case "5":
                    return;

                case "6":
                    string user = Prompt("Enter username: ");
                    _service.ChangeUser(user);
                    break;
                case "7":
                    ShowFilterMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;

            }
        }
    }
}
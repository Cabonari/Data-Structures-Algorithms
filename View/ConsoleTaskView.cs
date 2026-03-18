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

    private void DisplayTasks(IEnumerable<TaskItem> tasks)
    {
        Console.Clear();
        Console.WriteLine("Task List".PadLeft((windowWidth + 9) / 2));
        Console.WriteLine(new string('-', windowWidth));
        Console.WriteLine(new string("TODO".PadLeft((colWidth + 4) / 2).PadRight(colWidth) + "Doing".PadLeft((colWidth + 5) / 2).PadRight(colWidth) + "Review".PadLeft((colWidth + 6) / 2).PadRight(colWidth) + "DONE".PadLeft((colWidth + 4) / 2).PadRight(colWidth)));

        // eigen groupBy method moet aangemaakt worden.

        foreach (var task in tasks)
        {
            string status = task.Completed ? "[V]" : "[ ]";
            Console.WriteLine($"{status} {task.Id}: {task.Priority} - {task.Description}");
        }
    }

    private string Prompt(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    public void Run()
    {
        Console.SetBufferSize(windowWidth, windowHeight);

        while (true)
        {
            DisplayTasks(_service.GetAllTasks());

            Console.SetCursorPosition(0, windowHeight - 6);
            Console.WriteLine("\n\nOptions:");
            Console.WriteLine(new string("1. Add Task".PadRight(colWidth - 2) + "2. Update Task".PadRight(colWidth - 2) + "3. Remove Task".PadRight(colWidth - 2) + "4. Toggle Task".PadRight(colWidth - 2) + "5. Exit".PadRight(colWidth - 2)));

            string option = Prompt("Select an option: ");

            switch (option)
            {
                case "1":
                    string priority = Prompt("Enter task priority: ");
                    string description = Prompt("Enter task description: ");
                    _service.AddTask(priority, description);
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

                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
class Program
{
    static void Main()
    {
        // Dependency injection: wiring up our components
        string filePath = "tasks.json";
        ITaskRepository repository = new JsonTaskRepository(filePath);
        ITaskService service = new TaskService(repository);
        ITaskView view = new ConsoleTaskView(service);


        view.Run();

    }
}
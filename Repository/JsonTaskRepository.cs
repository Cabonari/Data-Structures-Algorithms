using System.Text.Json;

public class JsonTaskRepository : ITaskRepository
{
    private readonly string _filePath;

    public JsonTaskRepository(string filePath)
    {
        _filePath = filePath;
    }

    public IMyCollection<TaskItem> LoadTasks()
    {
        if (!File.Exists(_filePath))
            return new MyArray<TaskItem>();

        string json = File.ReadAllText(_filePath);
        var tasks = JsonSerializer.Deserialize<IMyCollection<TaskItem>>(json);
        return tasks ?? new MyArray<TaskItem>();
    }

    public void SaveTasks(IMyCollection<TaskItem> tasks)
    {
        string json = JsonSerializer.Serialize(tasks,
        new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(_filePath, json);
    }
}
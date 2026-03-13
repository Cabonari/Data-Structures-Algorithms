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
        var list = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        var result = new MyArray<TaskItem>(Math.Max(list.Count, 10));
        foreach (var task in list)
            result.Add(task);
        return result;
    }

    public void SaveTasks(IMyCollection<TaskItem> tasks)
    {
        var list = new List<TaskItem>();
        var iterator = tasks.GetMyIterator();
        while (iterator.HasNext())
            list.Add(iterator.Next());

        string json = JsonSerializer.Serialize(list,
        new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(_filePath, json);
    }
}
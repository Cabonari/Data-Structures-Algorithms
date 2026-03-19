public class TaskItem
{
    public int Id { get; set; }
    public required string Priority { get; set; }
    public required string Description { get; set; }
    public string[]? Assignees { get; set; }
    public string Row { get; set; }

    public DateTime Date { get; set; }
}

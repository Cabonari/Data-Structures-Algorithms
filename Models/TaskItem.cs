public class TaskItem {
    public int Id { get; set; }
    public required string Priority { get; set; }
    public required string Description { get; set; }
    public List<string> Assignees { get; set; }

    public bool Completed { get; set; }
}

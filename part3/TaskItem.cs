public class TaskItem
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? ReminderDate { get; set; }
    public bool IsCompleted { get; set; }

    public override string ToString()
    {
        var status = IsCompleted ? "✔ Completed" : "⏳ Pending";
        var reminder = ReminderDate.HasValue ? $" [Reminder: {ReminderDate.Value:g}]" : "";
        return $"{Title} - {Description} ({status}){reminder}";
    }
}

namespace SimpleTaskManager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool Done { get; set; }
        public DateTime? DueDate { get; set; } // uusi kenttä

        public override string ToString()
        {
            string status = Done ? "[X]" : "[ ]";
            string due = DueDate.HasValue ? $" (Due: {DueDate.Value:dd.MM.yyyy})" : "";
            return $"{Id}. {status} {Title}{due}";
        }
    }
}

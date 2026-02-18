using TaskManager.Common.Enums;

namespace TaskManager.DBModels
{ 
    public class TaskDBModel
    {
        public Guid Id { get; }
        public Guid ProjectId { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }

        public TaskDBModel(Guid projectId, string name, string description, Priority priority, DateTime deadline, bool isCompleted) {
            Id = Guid.NewGuid();
            ProjectId = projectId;
            Name = name;
            Description = description;
            Priority = priority;
            Deadline = deadline;
            IsCompleted = isCompleted;
        }

        public TaskDBModel(Guid projectId, string name, Priority priority, DateTime deadline, bool isCompleted)
             : this(projectId, name, string.Empty, priority, deadline, isCompleted)
        {
        }
    }
}
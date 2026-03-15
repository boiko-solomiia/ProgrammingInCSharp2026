using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.TaskDTO
{
    public class TaskDetailsDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Priority Priority { get; }
        public DateTime Deadline { get; }
        public bool IsCompleted { get; }
        public bool IsOverdue { get; }

        public TaskDetailsDTO(Guid id, string name, string description, Priority priority, DateTime deadline, bool isCompleted,  bool isOverdue)
        {
            Id = id;
            Name = name;
            Description = description;
            Priority = priority;
            Deadline = deadline;
            IsCompleted = isCompleted;
            IsOverdue = isOverdue;
        }
    }
}
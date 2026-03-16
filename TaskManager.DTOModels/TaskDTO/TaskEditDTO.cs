using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.TaskDTO
{
    public class TaskEditDTO
    {
        public Guid Id { get; }
        public Guid ProjectId { get; }
        public string Name { get; }
        public string Description { get; }
        public Priority Priority { get; }
        public DateTime Deadline { get; }
        public bool IsCompleted { get; }

        public TaskEditDTO(Guid id, Guid projectId, string name, string description, Priority priority, DateTime deadline, bool isCompleted)
        {
            Id = id;
            ProjectId = projectId;
            Name = name;
            Description = description;
            Priority = priority;
            Deadline = deadline;
            IsCompleted = isCompleted;
        }
    }
}
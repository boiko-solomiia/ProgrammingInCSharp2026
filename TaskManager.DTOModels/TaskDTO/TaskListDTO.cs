using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.TaskDTO
{
    public class TaskListDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public Priority Priority { get; }
        public DateTime Deadline { get; }
        public bool IsCompleted { get; }

        public TaskListDTO(Guid id, string name, Priority priority, DateTime deadline, bool isCompleted)
        {
            Id = id;
            Name = name;
            Priority = priority;
            Deadline = deadline;
            IsCompleted = isCompleted;
        }
    }
}
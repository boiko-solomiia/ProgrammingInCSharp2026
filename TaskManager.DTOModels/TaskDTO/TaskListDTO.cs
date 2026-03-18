using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.TaskDTO
{
    /// <summary>
    /// Data Transfer Object for displaying task in list views.
    /// Contains only the fields needed for task preview in collections
    /// </summary>
    public class TaskListDTO
    {
        /// <summary>
        /// Task ID
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        /// Task name
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Task priority
        /// </summary>
        public Priority Priority { get; }
        
        /// <summary>
        /// Task deadline
        /// </summary>
        public DateTime Deadline { get; }
        
        /// <summary>
        /// Task completion status (true = done, false = not done)
        /// </summary>
        public bool IsCompleted { get; }

        /// <summary>
        /// Creates a new immutable DTO with task data optimized for list display
        /// </summary>
        /// <param name="id">The unique task identifier</param>
        /// <param name="name">The task name</param>
        /// <param name="priority">The task priority</param>
        /// <param name="deadline">The task deadline</param>
        /// <param name="isCompleted">The task completion status</param>
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
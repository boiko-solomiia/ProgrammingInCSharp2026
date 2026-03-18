using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.TaskDTO
{
    /// <summary>
    /// Data Transfer Object for displaying detailed information about a task.
    /// Contains all task fields visible to the user, including computed fields like overdue status.
    /// Used for the task details page.
    /// </summary>
    public class TaskDetailsDTO
    {
        /// <summary>
        /// Gets the unique identifier of the task
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        /// Gets the task name
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Gets the detailed task description
        /// </summary>
        public string Description { get; }
        
        /// <summary>
        /// Gets the task priority level
        /// </summary>
        public Priority Priority { get; }
        
        /// <summary>
        /// Gets the task deadline
        /// </summary>
        public DateTime Deadline { get; }
        
        /// <summary>
        /// Gets task completion status (true = done, false = not done)
        /// </summary>
        public bool IsCompleted { get; }
        
        /// <summary>
        /// Gets task overdue status (true = overdue, false = not overdue)
        /// </summary>
        public bool IsOverdue { get; }

        /// <summary>
        /// Creates an immutable DTO with all task details for display
        /// </summary>
        /// <param name="id">The unique task identifier</param>
        /// <param name="name">The task name</param>
        /// <param name="description">The task description</param>
        /// <param name="priority">The task priority</param>
        /// <param name="deadline">The task deadline</param>
        /// <param name="isCompleted">The task completion status</param>
        /// <param name="isOverdue">The computed overdue status</param>
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
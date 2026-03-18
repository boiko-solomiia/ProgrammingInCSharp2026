using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.TaskDTO
{
    /// <summary>
    /// Data Transfer Object for editing an existing task.
    /// Contains all fields that can be modified by the user in the edit form
    /// </summary>
    public class TaskEditDTO
    {
        /// <summary>
        /// Task ID (cannot be changed)
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        /// ID of the parent project (cannot be changed)
        /// </summary>
        public Guid ProjectId { get; }
        
        /// <summary>
        /// Task name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Task description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Task priority
        /// </summary>
        public Priority Priority { get; set; }
        
        /// <summary>
        /// Task deadline
        /// </summary>
        public DateTime Deadline { get; set; }
        
        /// <summary>
        /// Task completion status (true = done, false = not done)
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Creates a mutable DTO with initial values from the database
        /// </summary>
        /// <param name="id">The unique task identifier (cannot be changed)</param>
        /// <param name="projectId">The parent project identifier (cannot be changed)</param>
        /// <param name="name">The initial task name from database</param>
        /// <param name="description">The initial task description from database</param>
        /// <param name="priority">The initial task priority from database</param>
        /// <param name="deadline">The initial task deadline from database</param>
        /// <param name="isCompleted">The initial task completion status from database</param>
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
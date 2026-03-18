using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.TaskDTO
{
    /// <summary>
    /// Data Transfer Object for creating a new task.
    /// Contains all data needed to create a task, provided by the user in the creation form.
    /// This DTO is immutable - values are set once at creation and not modified afterward
    /// </summary>
    public class TaskCreateDTO
    {
        /// <summary>
        /// Gets the identifier of the parent project to which this task belongs
        /// </summary>
        public Guid ProjectId { get; }
       
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
        /// Gets task completion status (true = done, false = not done).
        /// New tasks are typically created as not completed, but the option is available
        /// </summary>
        public bool IsCompleted { get; }

        /// <summary>
        /// Creates an immutable DTO with all data needed for task creation
        /// </summary>
        /// <param name="projectId">The identifier of the parent project</param>
        /// <param name="name">The task name</param>
        /// <param name="description">The task description</param>
        /// <param name="priority">The task priority</param>
        /// <param name="deadline">The task deadline</param>
        /// <param name="isCompleted">The task completion status (usually false for new tasks)</param>
        public TaskCreateDTO(Guid projectId, string name, string description, Priority priority, DateTime deadline, bool isCompleted)
        {
            ProjectId = projectId;
            Name = name;
            Description = description;
            Priority = priority;
            Deadline = deadline;
            IsCompleted = isCompleted;
        }
    }
}
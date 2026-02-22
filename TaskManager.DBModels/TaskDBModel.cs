using TaskManager.Common.Enums;

namespace TaskManager.DBModels
{ 
    /// <summary>
    /// Database model representing a task entity.
    /// Stores core task data without business logic or computed fields
    /// </summary>
    public class TaskDBModel
    {
        public Guid Id { get; }
        public Guid ProjectId { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Creates a new task with all fields
        /// </summary>
        /// <param name="projectId">ID of the parent project</param>
        /// <param name="name">Task name</param>
        /// <param name="description">Task description</param>
        /// <param name="priority">Task priority</param>
        /// <param name="deadline">Task deadline (will be converted to UTC)</param>
        /// <param name="isCompleted">Task completion status</param>
        public TaskDBModel(Guid projectId, string name, string description, Priority priority, DateTime deadline, bool isCompleted) {
            Id = Guid.NewGuid();
            ProjectId = projectId;
            Name = name;
            Description = description;
            Priority = priority;
            if (deadline.Kind == DateTimeKind.Unspecified)
                deadline = DateTime.SpecifyKind(deadline, DateTimeKind.Local);
            Deadline = deadline.ToUniversalTime();
            IsCompleted = isCompleted;
        }

        /// <summary>
        /// Creates a new task with empty description
        /// </summary>
        /// <param name="projectId">ID of the parent project</param>
        /// <param name="name">Task name</param>
        /// <param name="priority">Task priority</param>
        /// <param name="deadline">Task deadline</param>
        /// <param name="isCompleted">Task completion status</param>
        public TaskDBModel(Guid projectId, string name, Priority priority, DateTime deadline, bool isCompleted)
             : this(projectId, name, string.Empty, priority, deadline, isCompleted)
        {
        }
    }
}
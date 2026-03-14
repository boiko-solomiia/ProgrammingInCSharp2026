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
        
        private TaskDBModel() { }

        /// <summary>
        /// Creates a task with specified identifier.
        /// Used when reconstructing tasks from storage with existing IDs.
        /// </summary>
        /// <param name="id">Task identifier</param>
        /// <param name="projectId">ID of the parent project</param>
        /// <param name="name">Task name</param>
        /// <param name="description">Task description</param>
        /// <param name="priority">Task priority</param>
        /// <param name="deadline">Task deadline (will be converted to UTC)</param>
        /// <param name="isCompleted">Task completion status</param>
        public TaskDBModel(Guid id, Guid projectId, string name, string description, Priority priority, DateTime deadline, bool isCompleted)
        {
            Id = id;
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
        /// Creates a new task with all fields.
        /// Generates a new unique identifier automatically.
        /// </summary>
        /// <param name="projectId">ID of the parent project</param>
        /// <param name="name">Task name</param>
        /// <param name="description">Task description</param>
        /// <param name="priority">Task priority</param>
        /// <param name="deadline">Task deadline (will be converted to UTC)</param>
        /// <param name="isCompleted">Task completion status</param>
        public TaskDBModel(Guid projectId, string name, string description, Priority priority, DateTime deadline, bool isCompleted) 
            : this(Guid.NewGuid(), projectId, name, description, priority, deadline, isCompleted)
        {
        }

        /// <summary>
        /// Creates a new task with empty description
        /// Generates a new unique identifier automatically
        /// </summary>
        /// <param name="projectId">ID of the parent project</param>
        /// <param name="name">Task name</param>
        /// <param name="priority">Task priority</param>
        /// <param name="deadline">Task deadline</param>
        /// <param name="isCompleted">Task completion status</param>
        public TaskDBModel(Guid projectId, string name, Priority priority, DateTime deadline, bool isCompleted)
             : this(Guid.NewGuid(), projectId, name, string.Empty, priority, deadline, isCompleted)
        {
        }
    }
}
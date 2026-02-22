using TaskManager.Common.Enums;
using TaskManager.DBModels;

namespace TaskManager.UIModels.TaskUIModels
{
    /// <summary>
    /// UI model for creating new tasks.
    /// Collects user input and converts to database model via ToDbModel()
    /// </summary>
    public class TaskCreateModel
    {
        private readonly Guid _projectId;
        private string _name;
        private string _description;
        private Priority _priority;
        private DateTime _deadline;
        private bool _isCompleted;

        /// <summary>
        /// ID of the parent project (read-only)
        /// </summary>
        public Guid ProjectId
        {
            get => _projectId;
        }
        
        /// <summary>
        /// Task name
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        /// <summary>
        /// Task description
        /// </summary>
        public string Description
        {
            get => _description;
            set => _description = value;
        }
        
        /// <summary>
        /// Task priority
        /// </summary>
        public Priority Priority
        {
            get => _priority;
            set => _priority = value;
        }
        
        /// <summary>
        /// Task deadline (local time, converted to UTC when saving)
        /// </summary>
        public DateTime Deadline
        {
            get => _deadline;
            set => _deadline = value;
        }
        
        /// <summary>
        /// Task completion status
        /// </summary>
        public bool IsCompleted
        {
            get => _isCompleted;
            set => _isCompleted = value;
        }

        /// <summary>
        /// Creates empty task creation model with default values
        /// </summary>
        public TaskCreateModel() : this(Guid.Empty, string.Empty, string.Empty, Priority.Medium, DateTime.UtcNow.AddDays(7).ToLocalTime(), false)
        {
        }
        
        /// <summary>
        /// Creates task creation model for a specific project
        /// </summary>
        /// <param name="projectId">ID of the parent project</param>
        public TaskCreateModel(Guid projectId) 
            : this(projectId, string.Empty, string.Empty, Priority.Medium, DateTime.UtcNow.AddDays(7).ToLocalTime(), false)
        {
        }
        
        /// <summary>
        /// Creates task creation model with specified values
        /// </summary>
        /// <param name="projectId">ID of the parent project</param>
        /// <param name="name">Task name</param>
        /// <param name="description">Task description</param>
        /// <param name="priority">Task priority</param>
        /// <param name="deadline">Task deadline</param>
        /// <param name="isCompleted">Task completion status</param>
        public TaskCreateModel(Guid projectId, string name, string description, Priority priority, DateTime deadline, bool isCompleted)
        {
            _projectId = projectId;
            _name = name;
            _description = description;
            _priority = priority;
            _deadline = deadline;
            _isCompleted = isCompleted;
        }

        /// <summary>
        /// Converts this creation model to a database model for saving
        /// </summary>
        /// <returns>New TaskDBModel instance with the entered data</returns>
        public TaskDBModel ToDbModel()
        {
            return new TaskDBModel(_projectId, _name, _description, _priority, _deadline.ToUniversalTime(), _isCompleted);
        }
    }
}


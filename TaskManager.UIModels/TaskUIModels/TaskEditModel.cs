using TaskManager.Common.Enums;
using TaskManager.DBModels;

namespace TaskManager.UIModels.TaskUIModels
{
    /// <summary>
    /// UI model for editing existing tasks.
    /// Stores local copy of task data that can be modified before saving
    /// </summary>
    public class TaskEditModel
    {
        private readonly TaskDBModel _dbModel;
        private readonly Guid _projectId;
        private string _name;
        private string _description;
        private Priority _priority;
        private DateTime _deadline;
        private bool _isCompleted;

        /// <summary>
        /// Unique task identifier (read-only, from database)
        /// </summary>
        public Guid Id
        {
            get => _dbModel.Id;
        }
        
        /// <summary>
        /// ID of the parent project (read-only, cannot be changed)
        /// </summary>
        public Guid ProjectId
        {
            get => _projectId;
        }
        
        /// <summary>
        /// Task name (editable)
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        /// <summary>
        /// Task description (editable)
        /// </summary>
        public string Description
        {
            get => _description;
            set => _description = value;
        }
        
        /// <summary>
        /// Task priority (editable)
        /// </summary>
        public Priority Priority
        {
            get => _priority;
            set => _priority = value;
        }
        
        /// <summary>
        /// Task deadline (editable). Displayed in local time, saved as UTC
        /// </summary>
        public DateTime Deadline
        {
            get => _deadline;
            set => _deadline = value;
        }
        
        /// <summary>
        /// Task completion status (editable)
        /// </summary>
        public bool IsCompleted
        {
            get => _isCompleted;
            set => _isCompleted = value;
        }

        /// <summary>
        /// Indicates whether the task is overdue (read-only, computed).
        /// A task is overdue if it's not completed and current time is past the deadline
        /// </summary>
        public bool IsOverdue
        {
            get
            {
                if (_isCompleted) return false;
                return DateTime.UtcNow > _deadline.ToUniversalTime();
            }
        }

        /// <summary>
        /// Creates edit model from existing task
        /// </summary>
        /// <param name="dbModel">Existing task from database</param>
        public TaskEditModel(TaskDBModel dbModel)
        {
            _dbModel = dbModel;
            _projectId = dbModel.ProjectId;
            _name = dbModel.Name;
            _description = dbModel.Description;
            _priority = dbModel.Priority;
            _deadline = dbModel.Deadline.ToLocalTime();
            _isCompleted = dbModel.IsCompleted;
        }

        /// <summary>
        /// Saves all changes to the underlying database model.
        /// Updates task properties and converts deadline back to UTC.
        /// </summary>
        public void SaveChangesToDBModel()
        {
            _dbModel.Name = _name;
            _dbModel.Description = _description;
            _dbModel.Priority = _priority;
            _dbModel.Deadline = _deadline.ToUniversalTime();
            _dbModel.IsCompleted = _isCompleted;
        }

    }
}


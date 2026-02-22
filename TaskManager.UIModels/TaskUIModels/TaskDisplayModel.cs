using System;
using System.Threading.Tasks;
using TaskManager.Common.Enums;
using TaskManager.DBModels;

namespace TaskManager.UIModels.TaskUIModels
{
    /// <summary>
    /// UI model for displaying task information.
    /// Read-only view with computed overdue status
    /// </summary>
    public class TaskDisplayModel
    {
        private readonly TaskDBModel _dbModel;
        private readonly Guid _projectId;
        private readonly string _name;
        private readonly string _description;
        private readonly Priority _priority;
        private readonly DateTime _deadline;
        private readonly bool _isCompleted;

        /// <summary>
        /// Unique task identifier
        /// </summary>
        public Guid Id
        {
            get => _dbModel.Id;
        }
        
        /// <summary>
        /// ID of the parent project
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
        }
        
        /// <summary>
        /// Task description
        /// </summary>
        public string Description
        {
            get => _description;
        }
        
        /// <summary>
        /// Task priority level
        /// </summary>
        public Priority Priority
        {
            get => _priority;
        }
        
        /// <summary>
        /// Task deadline (displayed in local time)
        /// </summary>
        public DateTime Deadline
        {
            get => _deadline;
        }
        
        /// <summary>
        /// Task completion status
        /// </summary>
        public bool IsCompleted
        {
            get => _isCompleted;
        }
        
        /// <summary>
        /// Indicates whether the task is overdue.
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
        /// Creates display model from database task
        /// </summary>
        /// <param name="dbModel">Task from database</param>
        public TaskDisplayModel(TaskDBModel dbModel)
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
        /// Returns string representation of task with all details
        /// </summary>
        public override string ToString()
        {
            return $"TASK: {Name}\n{Description}\n{Priority} priority\nIs complited:  {IsCompleted}\nDue: {Deadline:d}\nIs overdue:  {IsOverdue}\n ";
        }
    }
}

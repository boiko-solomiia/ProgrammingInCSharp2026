using System;
using System.Threading.Tasks;
using TaskManager.Common.Enums;
using TaskManager.DBModels;

namespace TaskManager.UIModels.TaskUIModels
{
    public class TaskDisplayModel
    {
        private TaskDBModel? _dbModel;
        private Guid _projectId;
        private string _name;
        private string _description;
        private Priority _priority;
        private DateTime _deadline;
        private bool _isCompleted;
        private bool _isOverdue;

        public Guid Id
        {
            get => _dbModel.Id;
        }
        public Guid ProjectId 
        {
            get => _projectId;
        }
        public string Name
        { 
            get => _name;
        }
        public string Description
        {
            get => _description;
        }
        public Priority Priority
        {
            get => _priority;
        }
        public DateTime Deadline
        {
            get => _deadline;
        }
        public bool IsCompleted
        {
            get => _isCompleted;
        }
        public bool IsOverdue
        {
            get
            {
                if (_isCompleted) return false;
                return DateTime.UtcNow > _deadline;
            }
        }

        public TaskDisplayModel(TaskDBModel dbModel)
        {
            _dbModel = dbModel;
            _projectId = dbModel.ProjectId;
            _name = dbModel.Name;
            _description = dbModel.Description;
            _priority = dbModel.Priority;
            _deadline = dbModel.Deadline;
            _isCompleted = dbModel.IsCompleted;
        }

        public override string ToString()
        {
            return $"TASK: {Name}\n{Description}\n{Priority} priority\nis complited:  {IsCompleted}\nDue: {Deadline:d}\n ";
        }
    }
}

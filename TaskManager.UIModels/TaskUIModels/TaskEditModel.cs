using TaskManager.Common.Enums;

namespace TaskManager.UIModels.TaskUIModels
{
    public class TaskEditModel
    {

        private TaskDBModel? _dbModel;
        private Guid _projectId;
        private string _name;
        private string _description;
        private Priority _priority;
        private DateTime _deadline;
        private bool _isCompleted;
        private bool _isOverdue;

        public Guid? Id
        {
            get => _dbModel?.Id;
        }
        public Guid ProjectId
        {
            get => _projectId;
        }
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public string Description
        {
            get => _description;
            set => _description = value;
        }
        public Priority Priority
        {
            get => _priority;
            set => _priority = value;
        }
        public DateTime Deadline
        {
            get => _deadline;
            set => _deadline = value;
        }
        public bool IsCompleted
        {
            get => _isCompleted;
            set => _isCompleted = value;
        }
        public bool IsOverdue
        {
            get => _isOverdue;
        }

        public TaskEDitModel(TaskDBModel dbModel)
        {
            _dbModel = dbModel;
            _projectId = dbModel.ProjectId;
            _name = dbModel.Name;
            _description = dbModel.Description;
            _priority = dbModel.Priority;
            _deadline = dbModel.Deadline;
            _isCompleted = dbModel.IsCompleted;
        }

        public void SaveChangesToDBModel()
        {
            if (_dbModel != null)
            {
                _dbModel.Name = _name;
                _dbModel.Description = _description;
                _dbModel.Priority = _priority;
                _dbModel.Deadline = _deadline;
                _dbModel.IsCompleted = _isCompleted;
    }
            else
            {
                _dbModel = new TaskDBModel(_projectId, _name, _description, _priority, _deadline, _isCompleted);
            }
        }


    }
}


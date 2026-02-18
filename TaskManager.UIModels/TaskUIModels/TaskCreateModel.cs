using TaskManager.Common.Enums;
using TaskManager.DBModels;

namespace TaskManager.UIModels.TaskUIModels
{
    public class TaskCreateModel
    {

        private Guid _projectId;
        private string _name;
        private string _description;
        private Priority _priority;
        private DateTime _deadline;
        private bool _isCompleted;

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

        public TaskCreateModel(Guid projectId)
        {
            _projectId = projectId;
        }

        public TaskDBModel ToDbModel()
        {
            return new TaskDBModel(_projectId, _name, _description, _priority, _deadline, _isCompleted);
        }
    }
}


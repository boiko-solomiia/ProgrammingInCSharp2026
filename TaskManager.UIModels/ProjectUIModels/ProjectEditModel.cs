using TaskManager.Common.Enums;
using TaskManager.DBModels;
using TaskManager.UIModels.TaskUIModels;

namespace TaskManager.UIModels.ProjectUIModels
{
    public class ProjectEditModel
    {
        private ProjectDBModel? _dbModel;
        private string _name;
        private string _description;
        private ProjectType _projectType;
        private List<TaskEditModel> _tasks;

        public Guid? Id
        {
            get => _dbModel?.Id;
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

        public ProjectType ProjectType
        {
            get => _projectType;
            set => _projectType = value;
        }

        public IReadOnlyList<TaskEditModel> Tasks
        {
            get => _tasks;
        }

        public ProjectEditModel()
        {
            _tasks = new List<TaskEditModel>();
        }

        public ProjectEditModel(ProjectDBModel dbModel) : this()
        {
            _dbModel = dbModel;
            _name = dbModel.Name;
            _description = dbModel.Description;
            _projectType = dbModel.ProjectType;
        }

        public void SaveChangesToDBModel()
        {
            if (_dbModel != null)
            {
                _dbModel.Name = _name;
                _dbModel.Description = _description;
                _dbModel.ProjectType = _projectType;
            }
            else
            {
                _dbModel = new ProjectDBModel(_name, _description, _projectType);
            }
        }

        public void LoadTasks()
        {
            // завантаження завдань до проєкту
        }
    }
}
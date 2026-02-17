using TaskManager.Common.Enums;
using TaskManager.DBModels;

namespace TaskManager.UIModels.ProjectUIModels
{
    public class ProjectCreateModel
    {
        private string _name;
        private string _description;
        private ProjectType _projectType;

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
        
        public ProjectCreateModel() : this(string.Empty, string.Empty, ProjectType.Personal)
        {
        }

        public ProjectCreateModel(string name, string description, ProjectType projectType)
        {
            _name = name;
            _description = description;
            _projectType = projectType;
        }
        
        public ProjectDBModel ToDbModel()
        {
            return new ProjectDBModel(_name, _description, _projectType);
        }
    }
}

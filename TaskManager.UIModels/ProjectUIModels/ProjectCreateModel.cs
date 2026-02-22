using TaskManager.Common.Enums;
using TaskManager.DBModels;

namespace TaskManager.UIModels.ProjectUIModels
{
    /// <summary>
    /// UI model for creating new projects.
    /// Collects user input and converts to database model via ToDbModel()
    /// </summary>
    public class ProjectCreateModel
    {
        private string _name;
        private string _description;
        private ProjectType _projectType;

        /// <summary>
        /// Project name 
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Project description
        /// </summary>
        public string Description
        {
            get => _description;
            set => _description = value;
        }

        /// <summary>
        /// Project type
        /// </summary>
        public ProjectType ProjectType
        {
            get => _projectType;
            set => _projectType = value;
        }
        
        /// <summary>
        /// Creates empty project creation model with default values
        /// </summary>
        public ProjectCreateModel() : this(string.Empty, string.Empty, ProjectType.Personal)
        {
        }

        /// <summary>
        /// Creates project creation model with specified values
        /// </summary>
        /// <param name="name">Project name</param>
        /// <param name="description">Project description</param>
        /// <param name="projectType">Project type</param>
        public ProjectCreateModel(string name, string description, ProjectType projectType)
        {
            _name = name;
            _description = description;
            _projectType = projectType;
        }
        
        /// <summary>
        /// Converts this creation model to a database model for saving
        /// </summary>
        /// <returns>New ProjectDBModel instance with the entered data</returns>
        public ProjectDBModel ToDbModel()
        {
            return new ProjectDBModel(_name, _description, _projectType);
        }
    }
}

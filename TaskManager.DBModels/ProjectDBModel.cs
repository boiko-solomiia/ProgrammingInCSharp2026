using TaskManager.Common.Enums;

namespace TaskManager.DBModels
{
    /// <summary>
    /// Database model representing a project entity.
    /// Stores core project data without business logic or computed fields
    /// </summary>
    public class ProjectDBModel
    {
        /// <summary>
        /// Unique project identifier. Generated once and cannot be changed.
        /// </summary>
        public Guid Id { get;}
        
        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Detailed project description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Project type/category (Educational, Work, Personal, etc.)
        /// </summary>
        public ProjectType ProjectType { get; set; }
        
        private ProjectDBModel() { }
        
        /// <summary>
        /// Creates a new project with all fields
        /// </summary>
        /// <param name="name">Project name</param>
        /// <param name="description">Project description</param>
        /// <param name="projectType">Project type</param>
        public ProjectDBModel(string name, string description, ProjectType projectType)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ProjectType = projectType;
        }
        
        /// <summary>
        /// Creates a new project with empty description
        /// </summary>
        /// <param name="name">Project name</param>
        /// <param name="projectType">Project type</param>
        public ProjectDBModel(string name, ProjectType projectType) 
            : this(name, string.Empty, projectType)
        {
        }
    }
}
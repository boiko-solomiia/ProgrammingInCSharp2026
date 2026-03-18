using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.ProjectDTO
{
    /// <summary>
    /// Data Transfer Object for creating a new project.
    /// Contains all data needed to create a project, provided by the user in the creation form
    /// </summary>
    public class ProjectCreateDTO
    {
        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Project description
        /// </summary>
        public string Description { get; }
        
        /// <summary>
        /// Project type
        /// </summary>
        public ProjectType ProjectType { get; }
        
        /// <summary>
        /// Creates a new immutable DTO with all data needed for project creation
        /// </summary>
        /// <param name="name">The project name</param>
        /// <param name="description">The project description</param>
        /// <param name="projectType">The project type</param>
        public ProjectCreateDTO(string name, string description, ProjectType projectType)
        {
            Name = name;
            Description = description;
            ProjectType = projectType;
        }
    }
}
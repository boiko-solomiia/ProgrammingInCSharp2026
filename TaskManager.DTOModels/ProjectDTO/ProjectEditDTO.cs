using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.ProjectDTO
{
    /// <summary>
    /// Data Transfer Object for editing an existing project.
    /// Contains all fields that can be modified by the user in the edit form
    /// </summary>
    public class ProjectEditDTO
    {
        /// <summary>
        /// Project ID (cannot be changed)
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Project description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Project type
        /// </summary>
        public ProjectType ProjectType { get; set; }

        /// <summary>
        /// Creates a mutable DTO with initial values from the database
        /// </summary>
        /// <param name="id">The unique project identifier (cannot be changed)</param>
        /// <param name="name">The initial project name from database</param>
        /// <param name="description">The initial project description from database</param>
        /// <param name="projectType">The initial project type from database</param>
        public ProjectEditDTO(Guid id, string name, string description, ProjectType projectType)
        {
            Id = id;
            Name = name;
            Description = description;
            ProjectType = projectType;
        }
    }
}
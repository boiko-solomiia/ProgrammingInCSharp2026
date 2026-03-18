using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.ProjectDTO
{
    /// <summary>
    /// Data Transfer Object for displaying detailed information about a project.
    /// Contains all project fields visible to the user, including computed progress
    /// </summary>
    public class ProjectDetailsDTO
    {
        /// <summary>
        /// Project ID
        /// </summary>
        public Guid Id { get;}
        
        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get;}
        
        /// <summary>
        /// Project description
        /// </summary>
        public string Description { get;}
        
        /// <summary>
        /// Project type
        /// </summary>
        public ProjectType ProjectType { get;}
        
        /// <summary>
        /// Project completion progress (0-100%)
        /// </summary>
        public int Progress { get;}

        /// <summary>
        /// Creates a new immutable DTO with all project details for display
        /// </summary>
        /// <param name="id">The unique project identifier</param>
        /// <param name="name">The project name</param>
        /// <param name="description">The project description</param>
        /// <param name="projectType">The project type</param>
        /// <param name="progress">The completion progress percentage</param>
        public ProjectDetailsDTO(Guid id, string name, string description, ProjectType projectType, int progress)
        {
            Id = id;
            Name = name;
            Description = description;
            ProjectType = projectType;
            Progress = progress;
        }
    }
}
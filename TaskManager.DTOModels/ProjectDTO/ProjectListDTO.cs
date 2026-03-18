using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.ProjectDTO
{
    /// <summary>
    /// Data Transfer Object for displaying project in list views.
    /// Contains project summary with task count and progress for preview in collections
    /// </summary>
    public class ProjectListDTO
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
        public string Description { get; }
        
        /// <summary>
        /// Project type
        /// </summary>
        public ProjectType ProjectType { get;}
        
        /// <summary>
        /// Total number of tasks in the project
        /// </summary>
        public int TaskCount { get;}
        
        /// <summary>
        /// Project completion progress (0-100%)
        /// </summary>
        public int Progress { get;}

        /// <summary>
        /// Formatted progress text for display
        /// </summary>
        public string ProgressDescription
        {
            get => $"Progress: {Progress}%";
        }

        /// <summary>
        /// Creates a new immutable DTO with project data optimized for list display
        /// </summary>
        /// <param name="id">The unique project identifier</param>
        /// <param name="name">The project name</param>
        /// <param name="description">The project description</param>
        /// <param name="projectType">The project type</param>
        /// <param name="taskCount">Total number of tasks</param>
        /// <param name="progress">Completion progress percentage</param>
        public ProjectListDTO(Guid id, string name, string description, ProjectType projectType, int taskCount, int progress)
        {
            Id = id;
            Name = name;
            Description = description;
            ProjectType = projectType;
            TaskCount = taskCount;
            Progress = progress;
        }
    }
}
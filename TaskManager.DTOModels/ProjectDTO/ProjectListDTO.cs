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
        /// Formatted task count text for display (e.g., "11 tasks")
        /// </summary>
        public string TaskCountDescription
        {
            get => TaskCount == 1 ? "1 task" : $"{TaskCount} tasks";
        }

        /// <summary>
        /// Creates a new immutable DTO with project data optimized for list display
        /// </summary>
        /// <param name="id">The unique project identifier</param>
        /// <param name="name">The project name</param>
        /// <param name="projectType">The project type</param>
        /// <param name="taskCount">Total number of tasks</param>
        /// <param name="progress">Completion progress percentage</param>
        public ProjectListDTO(Guid id, string name, ProjectType projectType, int taskCount, int progress)
        {
            Id = id;
            Name = name;
            ProjectType = projectType;
            TaskCount = taskCount;
            Progress = progress;
        }
    }
}
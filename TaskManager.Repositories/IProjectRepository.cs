using TaskManager.DBModels;

namespace TaskManager.Repositories
{
    /// <summary>
    /// Provides data access operations for project entities.
    /// Defines the contract for retrieving project data from the storage layer
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        /// Retrieves all projects from the storage
        /// </summary>
        /// <returns>A collection of all project database models</returns>
        IEnumerable<ProjectDBModel> GetAllProjects();

        /// <summary>
        /// Retrieves a specific project by its unique identifier
        /// </summary>
        /// <param name="projectId">The unique identifier of the project</param>
        /// <returns>The project database model if found; otherwise, null</returns>
        ProjectDBModel GetProject(Guid projectId);

        /// <summary>
        /// Adds a new project to storage
        /// </summary>
        void AddProject(ProjectDBModel project);

        /// <summary>
        /// Updates an existing project in storage
        /// </summary>
        void UpdateProject(ProjectDBModel project);
    }
}
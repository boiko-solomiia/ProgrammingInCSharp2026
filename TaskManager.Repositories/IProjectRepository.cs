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
        /// Retrieves all projects from the storage asynchronously
        /// </summary>
        /// <returns>A collection of all project database models</returns>
        IAsyncEnumerable<ProjectDBModel> GetAllProjectsAsync();

        /// <summary>
        /// Retrieves a specific project by its unique identifier asynchronously
        /// </summary>
        /// <param name="projectId">The unique identifier of the project</param>
        /// <returns>The project database model if found; otherwise, null</returns>
        Task<ProjectDBModel?> GetProjectAsync(Guid projectId);

        /// <summary>
        /// Adds a new project to storage asynchronously
        /// </summary>
        /// <param name="project">The project database model to add</param>
        Task AddProjectAsync(ProjectDBModel project);

        /// <summary>
        /// Updates an existing project in storage asynchronously
        /// </summary>
        /// <param name="project">The project database model with updated values</param>
        Task UpdateProjectAsync(ProjectDBModel project);

        /// <summary>
        /// Deletes a project from storage asynchronously
        /// </summary>
        /// <param name="projectId">The unique identifier of the project to delete</param>
        Task DeleteProjectAsync(Guid projectId);
    }
}
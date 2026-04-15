using TaskManager.DTOModels.ProjectDTO;

namespace TaskManager.Services
{
    /// <summary>
    /// Provides operations for retrieving project data prepared for UI layer
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Returns all projects formatted for list view with progress calculation asynchronously
        /// </summary>
        /// <returns>Collection of projects with list-specific fields and progress</returns>
        IAsyncEnumerable<ProjectListDTO> GetAllProjectsAsync();

        /// <summary>
        /// Returns detailed information about a specific project asynchronously
        /// </summary>
        /// <param name="projectId">ID of the project</param>
        /// <returns>Detailed project information including description and progress</returns>
        Task<ProjectDetailsDTO?> GetProjectAsync(Guid projectId);

        /// <summary>
        /// Gets editable data for a specific project asynchronously
        /// </summary>
        /// <param name="projectId">The unique identifier of the project to edit</param>
        /// <returns>Project data formatted for editing, or null if project not found</returns>
        Task<ProjectEditDTO?> GetProjectForEditAsync(Guid projectId);

        /// <summary>
        /// Creates a new project from the provided DTO asynchronously
        /// </summary>
        /// <param name="projectDto">The project data transfer object containing creation information (name, description, type)</param>
        /// <returns>The unique identifier of the newly created project</returns>
        Task<Guid> CreateProjectAsync(ProjectCreateDTO projectDto);

        /// <summary>
        /// Updates an existing project using the provided DTO asynchronously
        /// </summary>
        /// <param name="projectDto">The project data transfer object containing updated values</param>
        Task UpdateProjectAsync(ProjectEditDTO projectDto);

        /// <summary>
        /// Deletes a project and all tasks that belong to it asynchronously
        /// </summary>
        /// <param name="projectId">The unique identifier of the project to delete</param>
        Task DeleteProjectAsync(Guid projectId);
    }
}
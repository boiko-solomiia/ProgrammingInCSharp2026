using TaskManager.DTOModels.ProjectDTO;

namespace TaskManager.Services
{
    /// <summary>
    /// Provides operations for retrieving project data prepared for UI layer
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Returns all projects formatted for list view with progress calculation
        /// </summary>
        /// <returns>Collection of projects with list-specific fields and progress</returns
        IEnumerable<ProjectListDTO> GetAllProjects();

        /// <summary>
        /// Returns detailed information about a specific project
        /// </summary>
        /// <param name="projectId">ID of the project</param>
        /// <returns>Detailed project information including description and progress</returns>
        ProjectDetailsDTO GetProject(Guid projectId);

        /// <summary>
        /// Gets editable data for a specific project
        /// </summary>
        ProjectEditDTO GetProjectForEdit(Guid projectId);

        /// <summary>
        /// Creates a new project from the provided DTO
        /// </summary>
        Guid CreateProject(ProjectCreateDTO projectDto);

        /// <summary>
        /// Updates an existing project using the provided DTO
        /// </summary>
        void UpdateProject(ProjectEditDTO projectDto);
    }
}
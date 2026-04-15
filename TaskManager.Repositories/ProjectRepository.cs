using TaskManager.DBModels;
using TaskManager.Storage;

namespace TaskManager.Repositories
{
    /// <summary>
    /// Repository implementation for project data access.
    /// Acts as an intermediary between the service layer and the storage context
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        private readonly IStorageContext _storageContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRepository"/> class.
        /// </summary>
        /// <param name="storageContext">The storage context providing access to the underlying data store.</param>
        public ProjectRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        /// <inheritdoc />
        public IAsyncEnumerable<ProjectDBModel> GetAllProjectsAsync()
        {
            return _storageContext.GetAllProjectsAsync();
        }

        /// <inheritdoc />
        public Task<ProjectDBModel?> GetProjectAsync(Guid projectId)
        {
            return _storageContext.GetProjectAsync(projectId);
        }

        /// <inheritdoc />
        public Task AddProjectAsync(ProjectDBModel project)
        {
            return _storageContext.AddProjectAsync(project);
        }

        /// <inheritdoc />
        public Task UpdateProjectAsync(ProjectDBModel project)
        {
            return _storageContext.UpdateProjectAsync(project);
        }

        /// <inheritdoc />
        public Task DeleteProjectAsync(Guid projectId)
        { 
            return _storageContext.DeleteProjectAsync(projectId);
        }
    }
}
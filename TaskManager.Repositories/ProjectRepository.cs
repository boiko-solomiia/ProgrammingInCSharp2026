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
        public IEnumerable<ProjectDBModel> GetAllProjects()
        {
            return _storageContext.GetAllProjects();
        }

        /// <inheritdoc />
        public ProjectDBModel GetProject(Guid projectId)
        {
            return _storageContext.GetProject(projectId);
        }

        /// <inheritdoc />
        public void AddProject(ProjectDBModel project)
        {
            _storageContext.AddProject(project);
        }

        /// <inheritdoc />
        public void UpdateProject(ProjectDBModel project)
        {
            _storageContext.UpdateProject(project);
        }
    }
}
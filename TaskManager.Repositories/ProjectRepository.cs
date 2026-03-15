using TaskManager.DBModels;
using TaskManager.Storage;

namespace TaskManager.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IStorageContext _storageContext;
        
        public ProjectRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }
        
        public IEnumerable<ProjectDBModel> GetAllProjects()
        {
            return _storageContext.GetAllProjects();
        }
    }
}
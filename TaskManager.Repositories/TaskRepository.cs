using TaskManager.DBModels;
using TaskManager.Storage;

namespace TaskManager.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IStorageContext _storageContext;
        
        public TaskRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }
        
        public IEnumerable<TaskDBModel> GetTasksForProject(Guid projectId)
        {
            return _storageContext.GetTasksForProject(projectId);
        }

        public int GetTasksCountForProject(Guid projectId)
        {
            return _storageContext.GetTasksCountForProject(projectId);
        }

        public int GetCompletedTasksCountForProject(Guid projectId)
        {
            return _storageContext.GetCompletedTasksCountForProject(projectId);
        }
    }
}
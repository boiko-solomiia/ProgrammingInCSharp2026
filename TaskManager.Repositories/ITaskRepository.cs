using TaskManager.DBModels;

namespace TaskManager.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TaskDBModel> GetTasksForProject(Guid projectId);
        int GetTasksCountForProject(Guid projectId);
        int GetCompletedTasksCountForProject(Guid projectId);
    }
}
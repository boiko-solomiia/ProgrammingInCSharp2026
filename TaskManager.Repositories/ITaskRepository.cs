using TaskManager.DBModels;

namespace TaskManager.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TaskDBModel> GetTasksForProject(Guid projectId);
        TaskDBModel GetTask(Guid projectId, Guid taskId);
        int GetTasksCountForProject(Guid projectId);
        int GetCompletedTasksCountForProject(Guid projectId);
    }
}
using TaskManager.DBModels;

namespace TaskManager.Storage
{
    public interface IStorageContext
    {
        IEnumerable<ProjectDBModel> GetAllProjects();
        IEnumerable<TaskDBModel> GetTasksForProject(Guid projectId);
        ProjectDBModel GetProject(Guid projectId);
        int GetTasksCountForProject(Guid projectId);
        int GetCompletedTasksCountForProject(Guid projectId);
    }
}
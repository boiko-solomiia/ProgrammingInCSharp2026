using TaskManager.DBModels;

namespace TaskManager.Repositories
{
    public interface IProjectRepository
    {
        IEnumerable<ProjectDBModel> GetAllProjects();
        ProjectDBModel GetProject(Guid projectId);
    }
}
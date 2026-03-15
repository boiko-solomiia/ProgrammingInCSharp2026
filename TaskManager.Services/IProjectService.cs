using TaskManager.DTOModels.ProjectDTO;

namespace TaskManager.Services
{
    public interface IProjectService
    {
        IEnumerable<ProjectListDTO> GetAllProjects();
        ProjectDetailsDTO GetProject(Guid projectId);
    }
}
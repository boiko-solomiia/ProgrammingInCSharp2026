using TaskManager.DTOModels.ProjectDTO;
using TaskManager.Repositories;

namespace TaskManager.Services
{
    /// <summary>
    /// Service for project-related logic
    /// Retrieves data from repositories and transforms it into DTOs for UI layer
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        
        /// <summary>
        /// Initializes a new instance of the ProjectService
        /// </summary>
        /// <param name="projectRepository">Repository for project data access</param>
        /// <param name="taskRepository">Repository for task data access (needed for progress calculation)</param>
        public ProjectService(IProjectRepository projectRepository, ITaskRepository taskRepository)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
        }
        
        /// <inheritdoc />
        public IEnumerable<ProjectListDTO> GetAllProjects()
        {
            foreach (var project in _projectRepository.GetAllProjects())
            {
                var taskCount = _taskRepository.GetTasksCountForProject(project.Id);
                var completedTaskCount = _taskRepository.GetCompletedTasksCountForProject(project.Id);
                var progress = taskCount == 0 ? 0 : (completedTaskCount * 100) / taskCount;
                yield return new ProjectListDTO(project.Id, project.Name, project.ProjectType, taskCount, progress);
            }
        }
        
        /// <inheritdoc />
        public ProjectDetailsDTO GetProject(Guid projectId)
        { 
            var project = _projectRepository.GetProject(projectId);
            if (project == null) return null;
            var taskCount = _taskRepository.GetTasksCountForProject(project.Id);
            var completedTaskCount = _taskRepository.GetCompletedTasksCountForProject(project.Id);
            var progress = taskCount == 0 ? 0 : (completedTaskCount * 100) / taskCount;
            return new ProjectDetailsDTO(project.Id, project.Name, project.Description, project.ProjectType, progress);
        }
    }
}
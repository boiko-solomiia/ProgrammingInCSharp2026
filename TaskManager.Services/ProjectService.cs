using TaskManager.DBModels;
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
                yield return new ProjectListDTO(project.Id, project.Name, project.Description, project.ProjectType,
                    taskCount, progress);
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

        /// <inheritdoc />
        public ProjectEditDTO GetProjectForEdit(Guid projectId)
        {
            var project = _projectRepository.GetProject(projectId);
            if (project == null) return null;
            return new ProjectEditDTO(project.Id, project.Name, project.Description, project.ProjectType);
        }

        /// <inheritdoc />
        public Guid CreateProject(ProjectCreateDTO projectDto)
        {
            var project = new ProjectDBModel(projectDto.Name, projectDto.Description, projectDto.ProjectType);
            _projectRepository.AddProject(project);
            return project.Id;
        }

        /// <inheritdoc />
        public void UpdateProject(ProjectEditDTO projectDto)
        {
            var project = _projectRepository.GetProject(projectDto.Id);
            if (project == null) return;
            project.Name = projectDto.Name;
            project.Description = projectDto.Description;
            project.ProjectType = projectDto.ProjectType;
            _projectRepository.UpdateProject(project);
        }
    }
}
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
            foreach (var project in _projectRepository.GetAllProjectsAsync())
            {
                var taskCount = _taskRepository.GetTasksCountForProjectAsync(project.Id);
                var completedTaskCount = _taskRepository.GetCompletedTasksCountForProjectAsync(project.Id);
                var progress = taskCount == 0 ? 0 : (completedTaskCount * 100) / taskCount;
                yield return new ProjectListDTO(project.Id, project.Name, project.ProjectType,
                    taskCount, progress);
            }
        }

        /// <inheritdoc />
        public ProjectDetailsDTO GetProject(Guid projectId)
        {
            var project = _projectRepository.GetProjectAsync(projectId);
            if (project == null) return null;
            var taskCount = _taskRepository.GetTasksCountForProjectAsync(project.Id);
            var completedTaskCount = _taskRepository.GetCompletedTasksCountForProjectAsync(project.Id);
            var progress = taskCount == 0 ? 0 : (completedTaskCount * 100) / taskCount;
            return new ProjectDetailsDTO(project.Id, project.Name, project.Description, project.ProjectType, progress);
        }

        /// <inheritdoc />
        public ProjectEditDTO GetProjectForEdit(Guid projectId)
        {
            var project = _projectRepository.GetProjectAsync(projectId);
            if (project == null) return null;
            return new ProjectEditDTO(project.Id, project.Name, project.Description, project.ProjectType);
        }

        /// <inheritdoc />
        public Guid CreateProject(ProjectCreateDTO projectDto)
        {
            var project = new ProjectDBModel(projectDto.Name, projectDto.Description, projectDto.ProjectType);
            _projectRepository.AddProjectAsync(project);
            return project.Id;
        }

        /// <inheritdoc />
        public void UpdateProject(ProjectEditDTO projectDto)
        {
            var project = _projectRepository.GetProjectAsync(projectDto.Id);
            if (project == null) return;
            project.Name = projectDto.Name;
            project.Description = projectDto.Description;
            project.ProjectType = projectDto.ProjectType;
            _projectRepository.UpdateProjectAsync(project);
        }

        /// <inheritdoc />
        public void DeleteProject(Guid projectId)
        {
            var project = _projectRepository.GetProjectAsync(projectId);
            if (project == null)
                return;

            var tasks = _taskRepository.GetTasksForProject(projectId).ToList();
            foreach (var task in tasks)
            {
                _taskRepository.DeleteTaskAsync(task.Id);
            }

            _projectRepository.DeleteProjectAsync(projectId);
        }
    }
}
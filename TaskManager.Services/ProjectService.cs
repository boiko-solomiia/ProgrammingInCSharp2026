using TaskManager.Common.Enums;
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
        public async IAsyncEnumerable<ProjectListDTO> GetAllProjectsAsync()
        {
            await foreach (var project in _projectRepository.GetAllProjectsAsync())
            {
                var taskCount = await _taskRepository.GetTasksCountForProjectAsync(project.Id);
                var completedTaskCount = await _taskRepository.GetCompletedTasksCountForProjectAsync(project.Id);
                var progress = taskCount == 0 ? 0 : (completedTaskCount * 100) / taskCount;
                yield return new ProjectListDTO(project.Id, project.Name, project.ProjectType,
                    taskCount, progress);
            }
        }

        /// <inheritdoc />
        public async Task<ProjectDetailsDTO?> GetProjectAsync(Guid projectId)
        {
            var project = await _projectRepository.GetProjectAsync(projectId);
            if (project == null) return null;
            var taskCount = await _taskRepository.GetTasksCountForProjectAsync(project.Id);
            var completedTaskCount = await _taskRepository.GetCompletedTasksCountForProjectAsync(project.Id);
            var progress = taskCount == 0 ? 0 : (completedTaskCount * 100) / taskCount;
            return new ProjectDetailsDTO(project.Id, project.Name, project.Description, project.ProjectType, progress);
        }

        /// <inheritdoc />
        public async Task<ProjectEditDTO?> GetProjectForEditAsync(Guid projectId)
        {
            var project = await _projectRepository.GetProjectAsync(projectId);
            if (project == null) return null;
            return new ProjectEditDTO(project.Id, project.Name, project.Description, project.ProjectType);
        }

        /// <inheritdoc />
        public async Task<Guid> CreateProjectAsync(ProjectCreateDTO projectDto)
        {
            ArgumentNullException.ThrowIfNull(projectDto);
            var project = new ProjectDBModel(projectDto.Name, projectDto.Description, projectDto.ProjectType);
            await _projectRepository.AddProjectAsync(project);
            return project.Id;
        }

        /// <inheritdoc />
        public async Task UpdateProjectAsync(ProjectEditDTO projectDto)
        {
            ArgumentNullException.ThrowIfNull(projectDto);
            var project = await _projectRepository.GetProjectAsync(projectDto.Id);
            if (project == null) return;

            if (project.Name == projectDto.Name &&
                project.Description == projectDto.Description &&
                project.ProjectType == projectDto.ProjectType)
                return;

            project.Name = projectDto.Name;
            project.Description = projectDto.Description;
            project.ProjectType = projectDto.ProjectType;
            await _projectRepository.UpdateProjectAsync(project);
        }

        /// <inheritdoc />
        public async Task DeleteProjectAsync(Guid projectId)
        {
            var project = await _projectRepository.GetProjectAsync(projectId);
            if (project == null)
                return;

            var tasks = await _taskRepository.GetTasksForProjectAsync(projectId);
            foreach (var task in tasks)
            {
                await _taskRepository.DeleteTaskAsync(task.Id);
            }

            await _projectRepository.DeleteProjectAsync(projectId);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ProjectListDTO>> GetProjectsFilteredAsync(string? searchName,
            ProjectType? projectType, ProjectSortOption sortOption = ProjectSortOption.NameDesc)
        {
            var projects = new List<ProjectListDTO>();
            await foreach (var project in GetAllProjectsAsync())
            {
                projects.Add(project);
            }

            IEnumerable<ProjectListDTO> filtered = projects;

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                filtered = filtered.Where(p => p.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase));
            }

            if (projectType.HasValue)
            {
                filtered = filtered.Where(p => p.ProjectType == projectType.Value);
            }

            filtered = sortOption switch
            {
                ProjectSortOption.NameAsc => filtered.OrderBy(p => p.Name),
                ProjectSortOption.NameDesc => filtered.OrderByDescending(p => p.Name),
                ProjectSortOption.ProgressAsc => filtered.OrderBy(p => p.Progress),
                ProjectSortOption.ProgressDesc => filtered.OrderByDescending(p => p.Progress),
                ProjectSortOption.TaskCountAsc => filtered.OrderBy(p => p.TaskCount),
                ProjectSortOption.TaskCountDesc => filtered.OrderByDescending(p => p.TaskCount),
                _ => filtered
            };
            return filtered.ToList();
        }
    }
}
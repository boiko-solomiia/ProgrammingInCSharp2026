using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskManager.DTOModels.ProjectDTO;
using TaskManager.DTOModels.TaskDTO;
using TaskManager.Pages;
using TaskManager.Services;

namespace TaskManager.ViewModels
{
    public partial class ProjectDetailsViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;

        private ProjectDetailsDTO? _currentProject;
        private ObservableCollection<TaskListDTO> _tasks = new();
        private TaskListDTO? _selectedTask;

        public ProjectDetailsDTO? CurrentProject
        {
            get => _currentProject;
            set => SetProperty(ref _currentProject, value);
        }

        public ObservableCollection<TaskListDTO> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }

        public TaskListDTO? SelectedTask
        {
            get => _selectedTask;
            set => SetProperty(ref _selectedTask, value);
        }

        public ProjectDetailsViewModel(IProjectService projectService, ITaskService taskService)
        {
            _projectService = projectService;
            _taskService = taskService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var projectId = (Guid)query["ProjectId"];
            CurrentProject = _projectService.GetProject(projectId);
            Tasks = new ObservableCollection<TaskListDTO>(_taskService.GetTasksForProject(projectId));
        }

        [RelayCommand]
        private async Task LoadTask(TaskListDTO? task)
        {
            if (task == null || CurrentProject == null)
                return;

            await Shell.Current.GoToAsync(nameof(TaskDetailsPage), new Dictionary<string, object>{{"ProjectId", CurrentProject.Id },{"TaskId", task.Id }});
        }
    }
}
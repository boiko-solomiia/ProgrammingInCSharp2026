using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskManager.DTOModels.ProjectDTO;
using TaskManager.DTOModels.TaskDTO;
using TaskManager.Pages;
using TaskManager.Services;

namespace TaskManager.ViewModels
{
    /// <summary>
    /// ViewModel for the Project Details page.
    /// Displays detailed information about a specific project and its associated tasks.
    /// Handles navigation to task details when a task is selected
    /// </summary>
    public partial class ProjectDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;

        private Guid _projectId;

        private ProjectDetailsDTO? _currentProject;
        public ProjectDetailsDTO? CurrentProject
        {
            get => _currentProject;
            set => SetProperty(ref _currentProject, value);
        }
       
        private ObservableCollection<TaskListDTO> _tasks = new();
        public ObservableCollection<TaskListDTO> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }
     
        private TaskListDTO? _selectedTask;
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
            if (query.TryGetValue("ProjectId", out var value) && value is Guid id)
            {
                _projectId = id;
                _ = RefreshData();
            }
        }

        internal async Task RefreshData()
        {
            IsBusy = true;
            try
            {
                CurrentProject = _projectService.GetProject(_projectId);
                Tasks = new ObservableCollection<TaskListDTO>(_taskService.GetTasksForProject(_projectId));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load project details: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Navigates to task details when a task is selected
        /// </summary>
        [RelayCommand]
        private async Task LoadTask(TaskListDTO? task)
        {
            if (task == null || CurrentProject == null)
                return;
            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync(nameof(TaskDetailsPage), new Dictionary<string, object> { { "ProjectId", CurrentProject.Id }, { "TaskId", task.Id }});
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate to task details: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
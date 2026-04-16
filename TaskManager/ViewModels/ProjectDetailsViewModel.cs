using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using TaskManager.Common.Enums;
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
        
        private string? _searchName;
        public string? SearchName
        {
            get => _searchName;
            set => SetProperty(ref _searchName, value);
        }

        private Priority? _selectedPriority;
        public Priority? SelectedPriority
        {
            get => _selectedPriority;
            set => SetProperty(ref _selectedPriority, value);
        }

        private bool _showCompletedOnly;
        public bool ShowCompletedOnly
        {
            get => _showCompletedOnly;
            set => SetProperty(ref _showCompletedOnly, value);
        }

        private TaskSortOption _selectedSortOption = TaskSortOption.PriorityDesc;
        public TaskSortOption SelectedSortOption
        {
            get => _selectedSortOption;
            set => SetProperty(ref _selectedSortOption, value);
        }

        public Array Priorities
        {
            get => Enum.GetValues(typeof(Priority));
        }

        public Array TaskSortOptions
        {
            get => Enum.GetValues(typeof(TaskSortOption));
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
                _ = RefreshDataAsync();
            }
        }

        internal async Task RefreshDataAsync()
        {
            IsBusy = true;
            try
            {
                CurrentProject = await _projectService.GetProjectAsync(_projectId);
                if (CurrentProject == null)
                {
                    await Shell.Current.DisplayAlertAsync("Error", "Project not found.", "OK");
                    await Shell.Current.GoToAsync("..");
                    return;
                }

                var tasks = await _taskService.GetTasksFilteredAsync(_projectId, SearchName, SelectedPriority, ShowCompletedOnly, SelectedSortOption);
                Tasks = new ObservableCollection<TaskListDTO>(tasks);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load data: {ex.Message}", "OK");
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
                await Shell.Current.GoToAsync(nameof(TaskDetailsPage), new Dictionary<string, object> { { "ProjectId", CurrentProject.Id }, { "TaskId", task.Id } });
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

        [RelayCommand]
        private async Task EditTask(TaskListDTO? task)
        {
            if (task == null || CurrentProject == null)
                return;

            await Shell.Current.GoToAsync(nameof(EditTaskPage), new Dictionary<string, object> { { "ProjectId", CurrentProject.Id }, { "TaskId", task.Id } });
        }


        [RelayCommand]
        private async Task DeleteTask(TaskListDTO? task)
        {
            if (task == null || CurrentProject == null)
                return;

            bool confirm = await Shell.Current.DisplayAlertAsync("Delete task", $"Are you sure you want to delete task \"{task.Name}\"?", "Yes", "No");

            if (!confirm)
                return;

            IsBusy = true;
            try
            {
                await _taskService.DeleteTaskAsync(task.Id);
                await RefreshDataAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to delete task: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task AddTask()
        {
            if (CurrentProject == null)
                return;

            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync(nameof(CreateTaskPage), new Dictionary<string, object> { { "ProjectId", CurrentProject.Id } });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate to create task page: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        [RelayCommand]
        private async Task ApplyFilters()
        {
            await RefreshDataAsync();
        }

        [RelayCommand]
        private void ResetFilters()
        {
            SearchName = null;
            SelectedPriority = null;
            ShowCompletedOnly = false;
            SelectedSortOption = TaskSortOption.PriorityDesc;
            _ = ApplyFiltersCommand.ExecuteAsync(null);
        }
    }
}
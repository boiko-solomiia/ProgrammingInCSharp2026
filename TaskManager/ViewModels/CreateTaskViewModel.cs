using CommunityToolkit.Mvvm.Input;
using TaskManager.Common.Enums;
using TaskManager.DTOModels.TaskDTO;
using TaskManager.Services;

namespace TaskManager.ViewModels
{
    /// <summary>
    /// ViewModel for the Create Task page.
    /// Handles user input and task creation within a project
    /// </summary>
    public partial class CreateTaskViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ITaskService _taskService;

        private Guid _projectId;

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private Priority? _priority;
        public Priority? Priority
        {
            get => _priority;
            set => SetProperty(ref _priority, value);
        }

        private DateTime? _deadline;
        public DateTime? Deadline
        {
            get => _deadline;
            set => SetProperty(ref _deadline, value);
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set => SetProperty(ref _isCompleted, value);
        }

        public Array Priorities
        {
            get => Enum.GetValues(typeof(Priority));
        }

        public CreateTaskViewModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("ProjectId", out var value) && value is Guid projectId)
            {
                _projectId = projectId;
            }
        }

        /// <summary>
        /// Creates a new task in the current project using the entered data.
        /// Validates that name, priority, and deadline are provided
        /// </summary>
        [RelayCommand]
        private async Task Create()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlertAsync("Validation error", "Task name is required.", "OK");
                return;
            }

            if (Priority == null)
            {
                await Shell.Current.DisplayAlertAsync("Validation error", "Priority is required.", "OK");
                return;
            }

            if (Deadline == null)
            {
                await Shell.Current.DisplayAlertAsync("Validation error", "Task deadline is required.", "OK");
                return;
            }

            IsBusy = true;
            try
            {
                var dto = new TaskCreateDTO(_projectId, Name.Trim(), Description.Trim(), Priority.Value, Deadline.Value, IsCompleted);
                await _taskService.CreateTaskAsync(dto);
                await Shell.Current.DisplayAlertAsync("Success", "Task created successfully!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to create task: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
using CommunityToolkit.Mvvm.Input;
using TaskManager.Common.Enums;
using TaskManager.DTOModels.TaskDTO;
using TaskManager.Services;

namespace TaskManager.ViewModels
{
    public partial class EditTaskViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ITaskService _taskService;

        private Guid _projectId;
        private Guid _taskId;

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

        private Priority _priority;
        public Priority Priority
        {
            get => _priority;
            set => SetProperty(ref _priority, value);
        }

        private DateTime _deadline;
        public DateTime Deadline
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

        public EditTaskViewModel(ITaskService taskService)
        {
            _taskService = taskService;
            Deadline = DateTime.Today;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("ProjectId", out var pValue) && pValue is Guid pId)
                _projectId = pId;

            if (query.TryGetValue("TaskId", out var tValue) && tValue is Guid tId)
                _taskId = tId;

            _ = RefreshDataAsync();
        }

        private async Task RefreshDataAsync()
        {
            IsBusy = true;
            try
            {
                var task = await _taskService.GetTaskForEditAsync(_taskId);
                if (task == null)
                {
                    await Shell.Current.DisplayAlertAsync("Error", "Task not found.", "OK");
                    await Shell.Current.GoToAsync("..");
                    return;
                }

                Name = task.Name;
                Description = task.Description;
                Priority = task.Priority;
                Deadline = task.Deadline;
                IsCompleted = task.IsCompleted;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load task: {ex.Message}", "OK");
                await Shell.Current.GoToAsync("..");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlertAsync("Validation error", "Task name is required.", "OK");
                return;
            }

            IsBusy = true;
            try
            {
                var dto = new TaskEditDTO(
                    _taskId,
                    _projectId,
                    Name.Trim(),
                    Description.Trim(),
                    Priority,
                    Deadline,
                    IsCompleted);

                await _taskService.UpdateTaskAsync(dto);
                await Shell.Current.DisplayAlertAsync("Success", "Task updated successfully!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to update task: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
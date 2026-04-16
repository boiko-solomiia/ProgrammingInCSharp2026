using TaskManager.DTOModels.TaskDTO;
using TaskManager.Services;

namespace TaskManager.ViewModels
{
    /// <summary>
    /// ViewModel for the Task Details page.
    /// Displays detailed information about a specific task including computed fields like overdue status
    /// </summary>
    public partial class TaskDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ITaskService _taskService;
        
        private Guid _taskId;

        private TaskDetailsDTO? _currentTask;
        public TaskDetailsDTO? CurrentTask
        {
            get => _currentTask;
            set => SetProperty(ref _currentTask, value);
        }

        public TaskDetailsViewModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("TaskId", out var tValue) && tValue is Guid tId)
                _taskId = tId;

            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            IsBusy = true;
            try
            {
                CurrentTask = await _taskService.GetTaskAsync(_taskId);
                if (CurrentTask == null)
                {
                    await Shell.Current.DisplayAlertAsync("Error", "Task not found.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load task details: {ex.Message}", "OK");
                await Shell.Current.GoToAsync("..");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
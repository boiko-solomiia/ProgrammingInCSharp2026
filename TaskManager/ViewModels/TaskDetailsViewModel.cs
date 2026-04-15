using CommunityToolkit.Mvvm.ComponentModel;
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

        private Guid _projectId;
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
            if (query.TryGetValue("ProjectId", out var pValue) && pValue is Guid pId)
                _projectId = pId;

            if (query.TryGetValue("TaskId", out var tValue) && tValue is Guid tId)
                _taskId = tId;

            _ = RefreshData();
        }

        internal async Task RefreshData()
        {
            IsBusy = true;
            try
            {
                CurrentTask = _taskService.GetTaskAsync(_taskId);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load task details: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
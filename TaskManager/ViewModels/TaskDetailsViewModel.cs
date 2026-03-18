using CommunityToolkit.Mvvm.ComponentModel;
using TaskManager.DTOModels.TaskDTO;
using TaskManager.Services;

namespace TaskManager.ViewModels
{
    /// <summary>
    /// ViewModel for the Task Details page.
    /// Displays detailed information about a specific task including computed fields like overdue status
    /// </summary>
    public partial class TaskDetailsViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ITaskService _taskService;
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
            var projectId = (Guid)query["ProjectId"];
            var taskId = (Guid)query["TaskId"];
            CurrentTask = _taskService.GetTask(projectId, taskId);
        }
    }
}
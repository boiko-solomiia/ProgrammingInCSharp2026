using CommunityToolkit.Mvvm.ComponentModel;
using TaskManager.DTOModels.TaskDTO;
using TaskManager.Services;

namespace TaskManager.ViewModels
{
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
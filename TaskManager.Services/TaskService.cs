using TaskManager.DTOModels.TaskDTO;
using TaskManager.Repositories;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        
        public IEnumerable<TaskListDTO> GetTasksForProject(Guid projectId)
        {
            foreach (var task in _taskRepository.GetTasksForProject(projectId))
            {
                yield return new TaskListDTO(task.Id, task.Name,  task.Priority, task.Deadline, task.IsCompleted);
            }
        }

        public TaskDetailsDTO GetTask(Guid projectId, Guid taskId)
        {
            var task = _taskRepository.GetTask(projectId, taskId);
            if (task == null) return null;
            var isOverdue = !task.IsCompleted && DateTime.UtcNow > task.Deadline.ToUniversalTime();
            return new TaskDetailsDTO(task.Id, task.Name, task.Description, task.Priority, task.Deadline, task.IsCompleted, isOverdue);
        }
    }
}